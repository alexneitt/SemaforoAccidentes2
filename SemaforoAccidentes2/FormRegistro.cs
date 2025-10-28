using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;


namespace SemaforoAccidentes2
{
    public partial class FormRegistro : Form
    {
        private string connectionString =
            @"Server=WIN-0CBQ8A7ROUG\DBACCIDENTES,1433;
            Database=DBAccidentes;
            User Id=AAdmin;
            Password=AAdmin12;
            TrustServerCertificate=True;";

        public FormRegistro()
        {
            InitializeComponent();
            btnGuardar.Click += BtnGuardar_Click;
            btnExportar.Click += btnExportar_Click;
            lblHsm.Enabled = false; // Deshabilitar etiqueta HSM
            txthsm.Enabled = false; // Deshabilitar campo de texto HSM

        }


        private FormMain mainForm;

        public FormRegistro(FormMain main)
        {
            InitializeComponent();
            mainForm = main;
            btnGuardar.Click += BtnGuardar_Click;
            btnExportar.Click += btnExportar_Click;
            lblHsm.Enabled = false;
            txthsm.Enabled = false;
        }


        private void FormRegistro_Load(object sender, EventArgs e)
        {
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("Accidente");
            cmbTipo.Items.Add("Incidente");
            cmbTipo.Items.Add("Observación");
            cmbTipo.SelectedIndex = 0; // Seleccionar la primera opción por defecto
        }


        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (chkHsm.Checked)
                    {
                        // Validar campo HSM
                        string horas = txthsm.Text.Trim();
                        DateTime fecha = dtpFecha.Value;
                        if (string.IsNullOrWhiteSpace(horas))
                        {
                            MessageBox.Show("Por favor, ingrese las horas de HSM.");
                            return;
                        }

                        // Insertar en TableHSM
                        string query = "INSERT INTO TableHSM (hsm, fecha) VALUES (@hsm, @fecha)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@hsm", horas);
                            command.Parameters.AddWithValue("@fecha", fecha);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insertar en Registros
                        string tipo = cmbTipo.SelectedItem?.ToString() ?? "";
                        string descripcion = txtDescripcion.Text.Trim();
                        DateTime fecha = dtpFecha.Value;

                        string query = "INSERT INTO Registros (fecha, tipo, descripcion) VALUES (@fecha, @tipo, @descripcion)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@fecha", fecha);
                            command.Parameters.AddWithValue("@tipo", tipo);
                            command.Parameters.AddWithValue("@descripcion", descripcion);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Registro guardado correctamente.");

                this.Close();

                // Después de cerrarse, programa la alerta en el Main
                mainForm.BeginInvoke((MethodInvoker)(() =>
                {
                    mainForm.MostrarAlertaSemaforo();
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
            }
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Guardar archivo Excel"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                conn.Open();

                                // Exportar Registros
                                var wsRegistros = workbook.Worksheets.Add("Registros");

                                wsRegistros.Cell(1, 1).Value = "ID";
                                wsRegistros.Cell(1, 2).Value = "Fecha";
                                wsRegistros.Cell(1, 3).Value = "Tipo";
                                wsRegistros.Cell(1, 4).Value = "Descripción";

                                string queryRegistros = "SELECT * FROM Registros ORDER BY id DESC";
                                using (SqlCommand cmd = new SqlCommand(queryRegistros, conn))
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    int fila = 2;
                                    while (reader.Read())
                                    {
                                        wsRegistros.Cell(fila, 1).Value = Convert.ToInt32(reader["id"]);
                                        wsRegistros.Cell(fila, 2).Value = Convert.ToDateTime(reader["fecha"]).ToString("dd/MM/yyyy");
                                        wsRegistros.Cell(fila, 3).Value = reader["tipo"]?.ToString() ?? "";
                                        wsRegistros.Cell(fila, 4).Value = reader["descripcion"]?.ToString() ?? "";
                                        fila++;
                                    }
                                }

                                wsRegistros.Columns().AdjustToContents();

                                // Exportar TableHSM
                                var wsHSM = workbook.Worksheets.Add("TableHSM");

                                wsHSM.Cell(1, 1).Value = "ID";
                                wsHSM.Cell(1, 2).Value = "HSM";
                                wsHSM.Cell(1, 3).Value = "Fecha";

                                string queryHSM = "SELECT * FROM TableHSM ORDER BY id DESC";
                                using (SqlCommand cmdHSM = new SqlCommand(queryHSM, conn))
                                using (SqlDataReader readerHSM = cmdHSM.ExecuteReader())
                                {
                                    int fila = 2;
                                    while (readerHSM.Read())
                                    {
                                        wsHSM.Cell(fila, 1).Value = Convert.ToInt32(readerHSM["id"]);
                                        wsHSM.Cell(fila, 2).Value = Convert.ToInt32(readerHSM["hsm"]);
                                        wsHSM.Cell(fila, 3).Value = Convert.ToDateTime(readerHSM["fecha"]).ToString("dd/MM/yyyy");
                                        fila++;
                                    }
                                }

                                wsHSM.Columns().AdjustToContents();
                            }

                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Exportación completada con éxito.", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar: " + ex.Message, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void chkHsm_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHsm.Checked)
            {
                // Activar HSM y desactivar los otros campos
                lblHsm.Enabled = true;
                txthsm.Enabled = true;

                lblTipo.Enabled = false;
                cmbTipo.Enabled = false;
                lblDescripcion.Enabled = false;
                txtDescripcion.Enabled = false;
            }
            else
            {
                // Activar campos normales y desactivar HSM
                lblHsm.Enabled = false;
                txthsm.Enabled = false;

                lblTipo.Enabled = true;
                cmbTipo.Enabled = true;
                lblDescripcion.Enabled = true;
                txtDescripcion.Enabled = true;
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}