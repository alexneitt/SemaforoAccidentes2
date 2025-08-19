namespace SemaforoAccidentes2
{
    partial class FormRegistro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistro));
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            btnGuardar = new Button();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblHsm = new Label();
            txthsm = new TextBox();
            chkHsm = new CheckBox();
            SuspendLayout();
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(13, 11);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(61, 25);
            lblFecha.TabIndex = 0;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(128, 11);
            dtpFecha.Margin = new Padding(3, 4, 3, 4);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(251, 31);
            dtpFecha.TabIndex = 1;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(13, 179);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(108, 25);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "Descripcion:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(128, 171);
            txtDescripcion.Margin = new Padding(3, 4, 3, 4);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(523, 96);
            txtDescripcion.TabIndex = 3;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(284, 291);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 38);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click_1;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(13, 64);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(51, 25);
            lblTipo.TabIndex = 5;
            lblTipo.Text = "Tipo:";
            // 
            // cmbTipo
            // 
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(128, 64);
            cmbTipo.Margin = new Padding(3, 4, 3, 4);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(251, 33);
            cmbTipo.TabIndex = 6;
            // 
            // lblHsm
            // 
            lblHsm.AutoSize = true;
            lblHsm.Location = new Point(13, 126);
            lblHsm.Name = "lblHsm";
            lblHsm.Size = new Size(55, 25);
            lblHsm.TabIndex = 7;
            lblHsm.Text = "HSM:";
            // 
            // txthsm
            // 
            txthsm.Location = new Point(128, 119);
            txthsm.Margin = new Padding(3, 4, 3, 4);
            txthsm.Name = "txthsm";
            txthsm.Size = new Size(251, 31);
            txthsm.TabIndex = 8;
            // 
            // chkHsm
            // 
            chkHsm.AutoSize = true;
            chkHsm.Location = new Point(413, 125);
            chkHsm.Margin = new Padding(3, 4, 3, 4);
            chkHsm.Name = "chkHsm";
            chkHsm.Size = new Size(151, 29);
            chkHsm.TabIndex = 9;
            chkHsm.Text = "Registrar HSM";
            chkHsm.UseVisualStyleBackColor = true;
            chkHsm.CheckedChanged += chkHsm_CheckedChanged;
            // 
            // FormRegistro
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(664, 344);
            Controls.Add(chkHsm);
            Controls.Add(txthsm);
            Controls.Add(lblHsm);
            Controls.Add(cmbTipo);
            Controls.Add(lblTipo);
            Controls.Add(btnGuardar);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDescripcion);
            Controls.Add(dtpFecha);
            Controls.Add(lblFecha);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormRegistro";
            Text = "Registrar Accidente";
            Load += FormRegistro_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblHsm;
        private System.Windows.Forms.TextBox txthsm;
        private System.Windows.Forms.CheckBox chkHsm;
    }
}