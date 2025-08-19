using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;


namespace SemaforoAccidentes2
{
    public partial class FormRegistro : Form
    {
        private string connectionString = @"Server=tcp:192.168.10.42\SQLEXPRESS,1433;Database=DBAccidentes;User Id=appuser;Password=appuser123; TrustServerCertificate=True;";

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
                        int hsm = 0;

                        // Obtener el último HSM
                        string queryHSM = "SELECT TOP 1 HSM FROM TableHSM ORDER BY id DESC";
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        using (SqlCommand cmd = new SqlCommand(queryHSM, conn))
                        {
                            conn.Open();
                            var result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                                hsm = Convert.ToInt32(result);
                        }

                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Registro");

                            // Encabezados
                            worksheet.Cell(1, 1).Value = "Fecha";
                            worksheet.Cell(1, 2).Value = "Tipo";
                            worksheet.Cell(1, 3).Value = "Descripción";
                            worksheet.Cell(1, 4).Value = "HSM";

                            // Valores del formulario
                            worksheet.Cell(2, 1).Value = dtpFecha.Value.ToString("dd/MM/yyyy");
                            worksheet.Cell(2, 2).Value = cmbTipo.SelectedItem?.ToString() ?? "";
                            worksheet.Cell(2, 3).Value = txtDescripcion.Text.Trim();
                            worksheet.Cell(2, 4).Value = hsm;

                            worksheet.Columns().AdjustToContents();
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
