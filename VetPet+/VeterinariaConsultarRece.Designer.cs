namespace VetPet_
{
    partial class VeterinariaConsultarRece
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
            this.dtMedicamentos = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnModificar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTemperatura = new System.Windows.Forms.TextBox();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.txtEspecie = new System.Windows.Forms.TextBox();
            this.txtMascota = new System.Windows.Forms.TextBox();
            this.txtRaza = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.rtDiagnostico = new System.Windows.Forms.RichTextBox();
            this.rtIndicaciones = new System.Windows.Forms.RichTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtFechaNacimiento = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnGenerarReceta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtMedicamentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // dtMedicamentos
            // 
            this.dtMedicamentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtMedicamentos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(234)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(234)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtMedicamentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtMedicamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtMedicamentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dtMedicamentos.EnableHeadersVisualStyles = false;
            this.dtMedicamentos.Location = new System.Drawing.Point(25, 280);
            this.dtMedicamentos.Name = "dtMedicamentos";
            this.dtMedicamentos.RowHeadersVisible = false;
            this.dtMedicamentos.RowHeadersWidth = 49;
            this.dtMedicamentos.Size = new System.Drawing.Size(566, 268);
            this.dtMedicamentos.TabIndex = 359;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Id";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "nombre";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "cantidad";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.pictureBox2.BackgroundImage = global::VetPet_.Properties.Resources.VeterinariaAtras;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(988, 505);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(46, 51);
            this.pictureBox2.TabIndex = 354;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.btnRegresar.Font = new System.Drawing.Font("Segoe UI Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.btnRegresar.Location = new System.Drawing.Point(847, 498);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(205, 63);
            this.btnRegresar.TabIndex = 353;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.pictureBox1.BackgroundImage = global::VetPet_.Properties.Resources.VetrinariaAjustes;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(772, 505);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 51);
            this.pictureBox1.TabIndex = 352;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.btnModificar.Location = new System.Drawing.Point(621, 498);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(218, 63);
            this.btnModificar.TabIndex = 351;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(305, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 25);
            this.label1.TabIndex = 349;
            this.label1.Text = "Raza";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(217, 205);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 25);
            this.label11.TabIndex = 337;
            this.label11.Text = "Temperatura";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(24, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 25);
            this.label9.TabIndex = 336;
            this.label9.Text = "Peso";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(28, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 25);
            this.label6.TabIndex = 334;
            this.label6.Text = "Especie";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(306, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 25);
            this.label5.TabIndex = 333;
            this.label5.Text = "Dueño";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI Black", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(420, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(239, 37);
            this.label16.TabIndex = 332;
            this.label16.Text = "Consultar Receta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(20, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 25);
            this.label2.TabIndex = 331;
            this.label2.Text = "Mascota";
            // 
            // txtTemperatura
            // 
            this.txtTemperatura.BackColor = System.Drawing.Color.Silver;
            this.txtTemperatura.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemperatura.Location = new System.Drawing.Point(349, 209);
            this.txtTemperatura.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTemperatura.Name = "txtTemperatura";
            this.txtTemperatura.ReadOnly = true;
            this.txtTemperatura.Size = new System.Drawing.Size(99, 29);
            this.txtTemperatura.TabIndex = 399;
            // 
            // txtPeso
            // 
            this.txtPeso.BackColor = System.Drawing.Color.Silver;
            this.txtPeso.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPeso.Location = new System.Drawing.Point(87, 209);
            this.txtPeso.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.ReadOnly = true;
            this.txtPeso.Size = new System.Drawing.Size(86, 29);
            this.txtPeso.TabIndex = 398;
            // 
            // txtEspecie
            // 
            this.txtEspecie.BackColor = System.Drawing.Color.Silver;
            this.txtEspecie.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEspecie.Location = new System.Drawing.Point(115, 123);
            this.txtEspecie.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEspecie.Name = "txtEspecie";
            this.txtEspecie.ReadOnly = true;
            this.txtEspecie.Size = new System.Drawing.Size(171, 29);
            this.txtEspecie.TabIndex = 397;
            // 
            // txtMascota
            // 
            this.txtMascota.BackColor = System.Drawing.Color.Silver;
            this.txtMascota.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMascota.Location = new System.Drawing.Point(118, 75);
            this.txtMascota.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMascota.Name = "txtMascota";
            this.txtMascota.ReadOnly = true;
            this.txtMascota.Size = new System.Drawing.Size(171, 29);
            this.txtMascota.TabIndex = 396;
            // 
            // txtRaza
            // 
            this.txtRaza.BackColor = System.Drawing.Color.Silver;
            this.txtRaza.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRaza.Location = new System.Drawing.Point(369, 121);
            this.txtRaza.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRaza.Name = "txtRaza";
            this.txtRaza.ReadOnly = true;
            this.txtRaza.Size = new System.Drawing.Size(171, 29);
            this.txtRaza.TabIndex = 395;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.Silver;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(386, 75);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(171, 29);
            this.txtNombre.TabIndex = 394;
            // 
            // rtDiagnostico
            // 
            this.rtDiagnostico.BackColor = System.Drawing.Color.Silver;
            this.rtDiagnostico.Location = new System.Drawing.Point(637, 102);
            this.rtDiagnostico.Name = "rtDiagnostico";
            this.rtDiagnostico.ReadOnly = true;
            this.rtDiagnostico.Size = new System.Drawing.Size(424, 46);
            this.rtDiagnostico.TabIndex = 393;
            this.rtDiagnostico.Text = "";
            // 
            // rtIndicaciones
            // 
            this.rtIndicaciones.BackColor = System.Drawing.Color.Silver;
            this.rtIndicaciones.Location = new System.Drawing.Point(637, 197);
            this.rtIndicaciones.Name = "rtIndicaciones";
            this.rtIndicaciones.ReadOnly = true;
            this.rtIndicaciones.Size = new System.Drawing.Size(423, 196);
            this.rtIndicaciones.TabIndex = 392;
            this.rtIndicaciones.Text = "";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(639, 169);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(133, 25);
            this.label24.TabIndex = 391;
            this.label24.Text = "Indicaciones ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(639, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(123, 25);
            this.label14.TabIndex = 390;
            this.label14.Text = "Diagnostico";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.pictureBox3.BackgroundImage = global::VetPet_.Properties.Resources.DueEliminar;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(772, 429);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 51);
            this.pictureBox3.TabIndex = 402;
            this.pictureBox3.TabStop = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.btnEliminar.Location = new System.Drawing.Point(621, 425);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(218, 63);
            this.btnEliminar.TabIndex = 401;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // txtFechaNacimiento
            // 
            this.txtFechaNacimiento.BackColor = System.Drawing.Color.Silver;
            this.txtFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaNacimiento.Location = new System.Drawing.Point(489, 166);
            this.txtFechaNacimiento.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFechaNacimiento.Name = "txtFechaNacimiento";
            this.txtFechaNacimiento.ReadOnly = true;
            this.txtFechaNacimiento.Size = new System.Drawing.Size(128, 29);
            this.txtFechaNacimiento.TabIndex = 406;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(307, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 25);
            this.label7.TabIndex = 405;
            this.label7.Text = "Fecha Nacimiento";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.Color.Silver;
            this.txtFecha.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(172, 162);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(128, 29);
            this.txtFecha.TabIndex = 404;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(13, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(152, 25);
            this.label8.TabIndex = 403;
            this.label8.Text = "Fecha Consulta";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.pictureBox4.BackgroundImage = global::VetPet_.Properties.Resources.VeterinariaLista;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(985, 432);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(50, 51);
            this.pictureBox4.TabIndex = 408;
            this.pictureBox4.TabStop = false;
            // 
            // btnGenerarReceta
            // 
            this.btnGenerarReceta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.btnGenerarReceta.Font = new System.Drawing.Font("Segoe UI Black", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarReceta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.btnGenerarReceta.Location = new System.Drawing.Point(847, 425);
            this.btnGenerarReceta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGenerarReceta.Name = "btnGenerarReceta";
            this.btnGenerarReceta.Size = new System.Drawing.Size(205, 63);
            this.btnGenerarReceta.TabIndex = 407;
            this.btnGenerarReceta.Text = "Imprimir";
            this.btnGenerarReceta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReceta.UseVisualStyleBackColor = false;
            this.btnGenerarReceta.Click += new System.EventHandler(this.btnGenerarReceta_Click);
            // 
            // VeterinariaConsultarRece
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(219)))), ((int)(((byte)(199)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.btnGenerarReceta);
            this.Controls.Add(this.txtFechaNacimiento);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.txtTemperatura);
            this.Controls.Add(this.txtPeso);
            this.Controls.Add(this.txtEspecie);
            this.Controls.Add(this.txtMascota);
            this.Controls.Add(this.txtRaza);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.rtDiagnostico);
            this.Controls.Add(this.rtIndicaciones);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dtMedicamentos);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label2);
            this.Name = "VeterinariaConsultarRece";
            this.Text = "VeterinariaConsultarRece";
            this.Load += new System.EventHandler(this.VeterinariaConsultarRece_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtMedicamentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtMedicamentos;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTemperatura;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.TextBox txtEspecie;
        private System.Windows.Forms.TextBox txtMascota;
        private System.Windows.Forms.TextBox txtRaza;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.RichTextBox rtDiagnostico;
        private System.Windows.Forms.RichTextBox rtIndicaciones;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtFechaNacimiento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button btnGenerarReceta;
    }
}