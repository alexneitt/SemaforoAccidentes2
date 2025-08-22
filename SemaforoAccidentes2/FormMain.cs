using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D; // para GraphicsPath
using Microsoft.Data.SqlClient; 
using System.Data.SqlClient;


//borrar este mensaje



namespace SemaforoAccidentes2
{
    public partial class FormMain : Form
    {

        private string connectionString = @"Server=tcp:192.168.10.42\SQLEXPRESS,1433;Database=DBAccidentes;User Id=appuser;Password=appuser123; TrustServerCertificate=True;";

        private int diasSinAccidentes;
        private int diasSinIncidentes = 0; // Ejemplo inicial
        /*private DateTime ultimoAccidente = new DateTime(2025, 7, 10); // Ejemplo inicial*/
        private DateTime ultimoAccidente;
        private DateTime ultimoIncidente;
        private int hsm = 0; // Ejemplo inicial
        // Reemplaza System.Timers.Timer por System.Windows.Forms.Timer para usar el evento Tick correctamente
        private System.Windows.Forms.Timer timer;

        private readonly TimeSpan horaReinicio = new TimeSpan(12, 0, 0); // 12:00 AM


        // Constantes para mensajes de Windows
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        public FormMain()
        {
            InitializeComponent();
            this.Load += FormMain_Load;
            ActualizarDatos();

            // Asociar evento MouseDown al formulario o al panel superior que sirva de "barra"
            this.MouseDown += FormMain_MouseDown;
            this.LocationChanged += FormMain_LocationChanged;

            // Timer para refrescar cada segundo
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) => ActualizarDatos();
            timer.Start();

            // Timer para verificar reinicio forzoso
            System.Windows.Forms.Timer reinicioTimer = new System.Windows.Forms.Timer();
            reinicioTimer.Interval = 60000; // cada 60 segundos
            reinicioTimer.Tick += ReinicioTimer_Tick;
            reinicioTimer.Start();

        }


        private void ReinicioTimer_Tick(object sender, EventArgs e)
        {
            // Comparar solo horas y minutos
            if (DateTime.Now.Hour == horaReinicio.Hours &&
                DateTime.Now.Minute == horaReinicio.Minutes)
            {
                ReiniciarAplicacion();
            }
        }

        private void ReiniciarAplicacion()
        {
            try
            {
                string exePath = Application.ExecutablePath;

                // Lanzar nueva instancia
                System.Diagnostics.Process.Start(exePath);

                // Cerrar instancia actual
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reiniciar: " + ex.Message);
            }
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Evita el cierre por el botón X o Alt+F4
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Cancela el cierre
                MessageBox.Show("No puedes cerrar esta aplicación");
            }

            base.OnFormClosing(e);
        }


        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }


        private void FormMain_LocationChanged(object sender, EventArgs e)
        {
            Rectangle areaTrabajo = Screen.GetWorkingArea(this); // área visible sin barra tareas

            int x = this.Location.X;
            int y = this.Location.Y;

            // Limitar posición horizontal para que no salga fuera del área visible
            if (x < areaTrabajo.Left)
                x = areaTrabajo.Left;
            if (x + this.Width > areaTrabajo.Right)
                x = areaTrabajo.Right - this.Width;

            // Limitar posición vertical para que no salga fuera del área visible
            if (y < areaTrabajo.Top)
                y = areaTrabajo.Top;
            if (y + this.Height > areaTrabajo.Bottom)
                y = areaTrabajo.Bottom - this.Height;

            // Aplicar posición corregida si es diferente
            if (x != this.Location.X || y != this.Location.Y)
            {
                this.Location = new Point(x, y);
            }
        }


        private DateTime ObtenerUltimoAccidente()
        {
            DateTime fechaUltimoAccidente = DateTime.MinValue;

            string query = "SELECT TOP 1 fecha FROM Registros WHERE tipo = 'Accidente' ORDER BY fecha DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        fechaUltimoAccidente = Convert.ToDateTime(result);
                    }
                    else
                    {
                        // No hay accidentes registrados
                        fechaUltimoAccidente = DateTime.Now; // O alguna otra fecha default
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener último accidente: " + ex.Message);
                fechaUltimoAccidente = DateTime.Now; // fallback
            }

            return fechaUltimoAccidente;
        }


        private DateTime ObtenerUltimoIncidente()
        {
            DateTime fechaUltimoIncidente = DateTime.MinValue;
            string query = "SELECT TOP 1 fecha FROM Registros WHERE tipo = 'Incidente' ORDER BY fecha DESC";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        fechaUltimoIncidente = Convert.ToDateTime(result);
                    }
                    else
                    {
                        // No hay incidentes registrados
                        fechaUltimoIncidente = DateTime.Now; // O alguna otra fecha default
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener último incidente: " + ex.Message);
                fechaUltimoIncidente = DateTime.Now; // fallback
            }
            return fechaUltimoIncidente;
        }


        private int ObtenerRecordSinAccidentes()
        {
            int record = 0;

            string query = @"
            WITH AccidentesOrdenados AS (
            SELECT fecha, 
                   LAG(fecha) OVER (ORDER BY fecha) AS fecha_anterior
            FROM Registros
            WHERE tipo = 'Accidente'
            )
            SELECT MAX(DATEDIFF(day, fecha_anterior, fecha)) AS MaxDiasSinAccidente
            FROM AccidentesOrdenados
            WHERE fecha_anterior IS NOT NULL";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        record = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el record sin accidentes: " + ex.Message);
            }

            return record;
        }


        private int HorasSegurasMensual()
        {
            int hsm = 0;
            string query = "SELECT TOP 1 HSM FROM TableHSM ORDER BY id DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        hsm = Convert.ToInt32(result);
                    }
                }
            }

            return hsm;

        }


        private void HacerPanelRedondo(Panel panel)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = Math.Min(panel.Width, panel.Height);
            path.AddEllipse(0, 0, diameter, diameter);
            panel.Region = new Region(path);
        }


        private void FormMain_Load(object sender, EventArgs e) //usuario registrar AlanSH
        {
            HacerPanelRedondo(luzRoja);
            HacerPanelRedondo(luzAmarilla);
            HacerPanelRedondo(luzVerde);

            luzRoja.Paint += luzRoja_Paint;
            luzAmarilla.Paint += luzAmarilla_Paint;
            luzVerde.Paint += luzVerde_Paint;

            ActualizarDatos();

            // Permitir btnRegistrar solo para un usuario específico
            string[] usuariosPermitidos = { "compu1", "AlanSH" };
            if (usuariosPermitidos.Contains(Environment.UserName, StringComparer.OrdinalIgnoreCase))
            {
                btnRegistrar.Enabled = true;
                btnRegistrar.Visible = true;
            }
            else
            {
                btnRegistrar.Enabled = false;
                btnRegistrar.Visible = false;
            }

            SuscribirseNotificaciones();

        }


        public void MostrarAlertaSemaforo()
        {
            this.TopMost = true;
            this.BringToFront();

            // Opcional: volver a normal después de 5 segundos
            var t = new System.Windows.Forms.Timer();
            t.Interval = 5000; // 5 segundos
            t.Tick += (s, e) =>
            {
                this.TopMost = false;
                t.Stop();
                t.Dispose();
            };
            t.Start();
        }


        private void SuscribirseNotificaciones()
        {
            SqlDependency.Stop(connectionString);
            SqlDependency.Start(connectionString);

            ConsultarRegistros();
        }

        private void ConsultarRegistros()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT fecha, tipo FROM dbo.Registros"; // la tabla que quieres monitorear

                SqlCommand cmd = new SqlCommand(query, conn);

                // Asociamos la dependencia
                SqlDependency dependency = new SqlDependency(cmd);
                dependency.OnChange += Dependency_OnChange;

                conn.Open();
                cmd.ExecuteReader().Dispose(); // ejecutar la query para enganchar la notificación
            }
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    // Sobresale el semáforo y refresca
                    MostrarAlertaSemaforo();
                    ActualizarDatos();
                }));

                // Es necesario volver a suscribirse (las notificaciones son de un solo uso)
                ConsultarRegistros();
            }
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            FormRegistro formRegistro = new FormRegistro(this); // pasamos referencia
            formRegistro.Show();

            // Después de cerrar el registro, actualiza semáforo
            ActualizarDatos();
        }


        private void ActualizarDatos()
        {
            // Datos de la tabla Registros
            ultimoAccidente = ObtenerUltimoAccidente();
            ultimoIncidente = ObtenerUltimoIncidente();
            diasSinAccidentes = (int)(DateTime.Now - ultimoAccidente).TotalDays;
            diasSinIncidentes = (int)(DateTime.Now - ultimoIncidente).TotalDays;
            int record = ObtenerRecordSinAccidentes();

            // Datos de la tabla TableHSM
            hsm = HorasSegurasMensual();

            // Actualización de UI
            lblHora.Text = $"Hora: {DateTime.Now:HH:mm:ss}";
            lblDias.Text = $"Días sin accidentes:\n{diasSinAccidentes}";
            lblFecha.Text = $"Último accidente:\n{ultimoAccidente:dd/MM/yyyy}";
            lblIncidentes.Text = $"Días sin incidentes:\n{diasSinIncidentes}";
            lblFecha2.Text = $"Último incidente:\n{ultimoIncidente:dd/MM/yyyy}";
            lblRecord.Text = $"Récord sin accidentes:\n{record} días";
            lblHsm.Text = $"HSM:\n{hsm}";

            // Actualiza semáforo visual
            ActualizarColorSemaforo();
        }


        private void luzRoja_Paint(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;

            Color encendido = Color.FromArgb(250, 90, 90);
            Color apagado = Color.FromArgb(74, 0, 0); // color apagado
            Color colorBorde = (c.BackColor == encendido) ? encendido : apagado;

            using (Pen p = new Pen(colorBorde, 1))
            {
                e.Graphics.DrawRectangle(p, 0, 0, c.Width - 1, c.Height - 1);
            }
        }


        private void luzAmarilla_Paint(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;

            Color encendido = Color.FromArgb(250, 0, 0);
            Color apagado = Color.FromArgb(50, 50, 0); // color apagado
            Color colorBorde = (c.BackColor == encendido) ? encendido : apagado;

            using (Pen p = new Pen(colorBorde, 1))
            {
                e.Graphics.DrawRectangle(p, 0, 0, c.Width - 1, c.Height - 1);
            }
        }


        private void luzVerde_Paint(object sender, PaintEventArgs e)
        {
            Control c = (Control)sender;

            Color encendido = Color.FromArgb(0, 255, 0);
            Color apagado = Color.FromArgb(0, 64, 0); // color apagado
            Color colorBorde = (c.BackColor == encendido) ? encendido : apagado;

            using (Pen p = new Pen(colorBorde, 1))
            {
                e.Graphics.DrawRectangle(p, 0, 0, c.Width - 1, c.Height - 1);
            }
        }


        private void ActualizarColorSemaforo()
        {
            // Ejemplo para un LED rojo
            Color encendidoRojo = Color.FromArgb(255, 0, 0);
            Color apagadoRojo = Color.FromArgb(74, 0, 0);

            // Para un LED verde
            Color encendidoVerde = Color.FromArgb(0, 200, 0);
            Color apagadoVerde = Color.FromArgb(0, 64, 0);

            // Para un LED azul
            Color encendidoAmarillo = Color.FromArgb(255, 255, 0);
            Color apagadoAmarillo = Color.FromArgb(50, 50, 0);


            if (diasSinAccidentes <= 7)
            {
                luzRoja.BackColor = encendidoRojo;
                luzAmarilla.BackColor = apagadoAmarillo;
                luzVerde.BackColor = apagadoVerde;
            }
            else if (diasSinAccidentes > 7 && diasSinAccidentes <= 30)
            {
                luzRoja.BackColor = apagadoRojo;
                luzAmarilla.BackColor = encendidoAmarillo;
                luzVerde.BackColor = apagadoVerde;
            }
            else
            {
                luzRoja.BackColor = apagadoRojo;
                luzAmarilla.BackColor = apagadoAmarillo;
                luzVerde.BackColor = encendidoVerde;
            }
        }


        private void lblDias_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblFecha_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblFecha2_Click(object sender, EventArgs e)
        {

        }

        private void lblhsm_Click(object sender, EventArgs e)
        {

        }
    }
}
