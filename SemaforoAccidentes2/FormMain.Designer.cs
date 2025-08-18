namespace SemaforoAccidentes2
{
    partial class FormMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            pnlSemaforo = new Panel();
            luzVerde = new Panel();
            luzAmarilla = new Panel();
            luzRoja = new Panel();
            lblDias = new Label();
            lblFecha = new Label();
            btnRegistrar = new Button();
            lblHora = new Label();
            lblRecord = new Label();
            lblIncidentes = new Label();
            lblFecha2 = new Label();
            lblHsm = new Label();
            pnlSemaforo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSemaforo
            // 
            pnlSemaforo.Controls.Add(luzVerde);
            pnlSemaforo.Controls.Add(luzAmarilla);
            pnlSemaforo.Location = new Point(22, 25);
            pnlSemaforo.Margin = new Padding(3, 4, 3, 4);
            pnlSemaforo.Name = "pnlSemaforo";
            pnlSemaforo.Size = new Size(67, 212);
            pnlSemaforo.TabIndex = 0;
            // 
            // luzVerde
            // 
            luzVerde.Location = new Point(6, 145);
            luzVerde.Margin = new Padding(3, 4, 3, 4);
            luzVerde.Name = "luzVerde";
            luzVerde.Size = new Size(56, 62);
            luzVerde.TabIndex = 2;
            // 
            // luzAmarilla
            // 
            luzAmarilla.Location = new Point(6, 75);
            luzAmarilla.Margin = new Padding(3, 4, 3, 4);
            luzAmarilla.Name = "luzAmarilla";
            luzAmarilla.Size = new Size(56, 62);
            luzAmarilla.TabIndex = 1;
            // 
            // luzRoja
            // 
            luzRoja.Location = new Point(28, 30);
            luzRoja.Margin = new Padding(3, 4, 3, 4);
            luzRoja.Name = "luzRoja";
            luzRoja.Size = new Size(56, 62);
            luzRoja.TabIndex = 0;
            // 
            // lblDias
            // 
            lblDias.AutoSize = true;
            lblDias.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDias.ForeColor = SystemColors.ButtonFace;
            lblDias.Location = new Point(107, 25);
            lblDias.Name = "lblDias";
            lblDias.Size = new Size(188, 21);
            lblDias.TabIndex = 2;
            lblDias.Text = "Dias sin Accidentes:";
            lblDias.Click += lblDias_Click;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFecha.ForeColor = SystemColors.ButtonFace;
            lblFecha.Location = new Point(107, 165);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(147, 21);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "Ultimo Accidente:";
            lblFecha.Click += lblFecha_Click;
            // 
            // btnRegistrar
            // 
            btnRegistrar.AutoSize = true;
            btnRegistrar.BackColor = SystemColors.ControlDarkDark;
            btnRegistrar.ForeColor = SystemColors.ButtonFace;
            btnRegistrar.Location = new Point(14, 385);
            btnRegistrar.Margin = new Padding(3, 4, 3, 4);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(94, 38);
            btnRegistrar.TabIndex = 4;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHora.ForeColor = SystemColors.ButtonFace;
            lblHora.Location = new Point(171, 400);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(129, 21);
            lblHora.TabIndex = 5;
            lblHora.Text = "Hora: 00:00:00";
            // 
            // lblRecord
            // 
            lblRecord.AutoSize = true;
            lblRecord.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRecord.ForeColor = SystemColors.ButtonFace;
            lblRecord.Location = new Point(107, 305);
            lblRecord.Name = "lblRecord";
            lblRecord.Size = new Size(192, 21);
            lblRecord.TabIndex = 6;
            lblRecord.Text = "Record sin accidentes:";
            lblRecord.Click += label1_Click;
            // 
            // lblIncidentes
            // 
            lblIncidentes.AutoSize = true;
            lblIncidentes.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIncidentes.ForeColor = SystemColors.ButtonFace;
            lblIncidentes.Location = new Point(107, 95);
            lblIncidentes.Name = "lblIncidentes";
            lblIncidentes.Size = new Size(182, 21);
            lblIncidentes.TabIndex = 7;
            lblIncidentes.Text = "Dias sin Incidentes:";
            lblIncidentes.Click += label1_Click_1;
            // 
            // lblFecha2
            // 
            lblFecha2.AutoSize = true;
            lblFecha2.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFecha2.ForeColor = SystemColors.ButtonFace;
            lblFecha2.Location = new Point(107, 235);
            lblFecha2.Name = "lblFecha2";
            lblFecha2.Size = new Size(142, 21);
            lblFecha2.TabIndex = 8;
            lblFecha2.Text = "Ultimo Incidente:";
            lblFecha2.Click += lblFecha2_Click;
            // 
            // lblHsm
            // 
            lblHsm.AutoSize = true;
            lblHsm.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHsm.ForeColor = SystemColors.ButtonFace;
            lblHsm.Location = new Point(18, 305);
            lblHsm.Name = "lblHsm";
            lblHsm.Size = new Size(55, 21);
            lblHsm.TabIndex = 9;
            lblHsm.Text = "HSM:";
            lblHsm.Click += lblhsm_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Desktop;
            ClientSize = new Size(328, 438);
            Controls.Add(lblHsm);
            Controls.Add(lblFecha2);
            Controls.Add(lblIncidentes);
            Controls.Add(lblRecord);
            Controls.Add(lblHora);
            Controls.Add(btnRegistrar);
            Controls.Add(lblFecha);
            Controls.Add(lblDias);
            Controls.Add(luzRoja);
            Controls.Add(pnlSemaforo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMain";
            StartPosition = FormStartPosition.Manual;
            Text = "Semaforo ";
            Load += FormMain_Load;
            pnlSemaforo.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSemaforo;
        private System.Windows.Forms.Panel luzVerde;
        private System.Windows.Forms.Panel luzAmarilla;
        private System.Windows.Forms.Panel luzRoja;
        private System.Windows.Forms.Label lblDias;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Label lblIncidentes;
        private System.Windows.Forms.Label lblFecha2;
        private System.Windows.Forms.Label lblHsm;
    }
}

