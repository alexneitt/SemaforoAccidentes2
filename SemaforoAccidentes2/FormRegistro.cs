using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;


namespace SemaforoAccidentes2
{
    public partial class FormRegistro : Form
    {
        private string connectionString = @"Server=tcp:192.168.10.42\SQLEXPRESS,1433;Database=DBAccidentes;User Id=appuser;Password=appuser123; TrustServerCertificate=True;";

        public FormRegistro()
        {
            InitializeComponent();
            btnGuardar.Click += BtnGuardar_Click;
            lblHsm.Enabled = false; // Deshabilitar etiqueta HSM
            txthsm.Enabled = false; // Deshabilitar campo de texto HSM

        }


        private FormMain mainForm;

        public FormRegistro(FormMain main)
        {
            InitializeComponent();
            mainForm = main;
            btnGuardar.Click += BtnGuardar_Click;
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

                // Mostrar el semáforo sobre otras ventanas
                mainForm.MostrarAlertaSemaforo();

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
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
    }
}
