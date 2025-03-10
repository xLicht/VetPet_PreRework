namespace VetPet_
{
    partial class EmpTiposEmpleados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbFliltrar = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.a = new System.Windows.Forms.PictureBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.r = new System.Windows.Forms.PictureBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.dtTipoEmpleado = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.a)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.r)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTipoEmpleado)).BeginInit();
            this.SuspendLayout();
            // 
            // cbFliltrar
            // 
            this.cbFliltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(245)))), ((int)(((byte)(231)))));
            this.cbFliltrar.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFliltrar.FormattingEnabled = true;
            this.cbFliltrar.Items.AddRange(new object[] {
            "Tipo",
            "Modulos de Acceso"});
            this.cbFliltrar.Location = new System.Drawing.Point(150, 85);
            this.cbFliltrar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbFliltrar.Name = "cbFliltrar";
            this.cbFliltrar.Size = new System.Drawing.Size(156, 33);
            this.cbFliltrar.TabIndex = 132;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI Black", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(459, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(214, 37);
            this.label16.TabIndex = 131;
            this.label16.Text = "Tipo Empleado";
            // 
            // a
            // 
            this.a.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.a.BackgroundImage = global::VetPet_.Properties.Resources.EmpTipo2;
            this.a.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.a.Location = new System.Drawing.Point(943, 501);
            this.a.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(50, 47);
            this.a.TabIndex = 129;
            this.a.TabStop = false;
            this.a.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.btnAgregar.Location = new System.Drawing.Point(802, 492);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(205, 63);
            this.btnAgregar.TabIndex = 128;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // r
            // 
            this.r.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.r.BackgroundImage = global::VetPet_.Properties.Resources.VeterinariaAtras;
            this.r.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.r.Location = new System.Drawing.Point(232, 513);
            this.r.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.r.Name = "r";
            this.r.Size = new System.Drawing.Size(53, 42);
            this.r.TabIndex = 127;
            this.r.TabStop = false;
            this.r.Click += new System.EventHandler(this.r_Click);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.btnRegresar.Font = new System.Drawing.Font("Segoe UI Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.btnRegresar.Location = new System.Drawing.Point(73, 501);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(233, 63);
            this.btnRegresar.TabIndex = 126;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // dtTipoEmpleado
            // 
            this.dtTipoEmpleado.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(234)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(234)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtTipoEmpleado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtTipoEmpleado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtTipoEmpleado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.dtTipoEmpleado.EnableHeadersVisualStyles = false;
            this.dtTipoEmpleado.Location = new System.Drawing.Point(73, 126);
            this.dtTipoEmpleado.Name = "dtTipoEmpleado";
            this.dtTipoEmpleado.RowHeadersVisible = false;
            this.dtTipoEmpleado.Size = new System.Drawing.Size(934, 355);
            this.dtTipoEmpleado.TabIndex = 125;
            this.dtTipoEmpleado.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtTipoEmpleado_CellClick);
            this.dtTipoEmpleado.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "IdTipo";
            this.Column3.Name = "Column3";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Tipo";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Modulos de Acceso";
            this.Column2.Name = "Column2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(68, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 30);
            this.label5.TabIndex = 124;
            this.label5.Text = "Filtrar";
            // 
            // EmpTiposEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(219)))), ((int)(((byte)(199)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.cbFliltrar);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.a);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.r);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.dtTipoEmpleado);
            this.Controls.Add(this.label5);
            this.Name = "EmpTiposEmpleados";
            this.Text = "EmpTiposEmpleados";
            this.Load += new System.EventHandler(this.EmpTiposEmpleados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.a)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.r)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTipoEmpleado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbFliltrar;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox a;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.PictureBox r;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.DataGridView dtTipoEmpleado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}