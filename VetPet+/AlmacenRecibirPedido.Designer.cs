namespace VetPet_
{
    partial class AlmacenRecibirPedido
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
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.checkboxMedicamento = new System.Windows.Forms.CheckBox();
            this.checkboxProducto = new System.Windows.Forms.CheckBox();
            this.cmbMedicamento = new System.Windows.Forms.ComboBox();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.txtIdMedicamento = new System.Windows.Forms.TextBox();
            this.txtIdProducto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdProveedor = new System.Windows.Forms.TextBox();
            this.fechaRecibidoPicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFactura
            // 
            this.txtFactura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.txtFactura.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFactura.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.txtFactura.Location = new System.Drawing.Point(316, 111);
            this.txtFactura.MaxLength = 32;
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(256, 40);
            this.txtFactura.TabIndex = 81;
            this.txtFactura.Text = "Factura";
            this.txtFactura.Enter += new System.EventHandler(this.txtFactura_Enter);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label12.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label12.Location = new System.Drawing.Point(615, 109);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(436, 40);
            this.label12.TabIndex = 79;
            this.label12.Text = "Pedido a Agregar";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label11.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label11.Location = new System.Drawing.Point(20, 109);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(280, 39);
            this.label11.TabIndex = 78;
            this.label11.Text = "Numero Factura";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label10.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label10.Location = new System.Drawing.Point(20, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(280, 40);
            this.label10.TabIndex = 77;
            this.label10.Text = "Fecha recibido";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.label8.Location = new System.Drawing.Point(20, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(493, 38);
            this.label8.TabIndex = 72;
            this.label8.Text = "Rellene los datos a agregar";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
            this.pictureBox2.Enabled = false;
            this.pictureBox2.Image = global::VetPet_.Properties.Resources.plus;
            this.pictureBox2.Location = new System.Drawing.Point(1008, 500);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 43);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 70;
            this.pictureBox2.TabStop = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
            this.btnGuardar.FlatAppearance.BorderSize = 2;
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(120)))), ((int)(((byte)(136)))));
            this.btnGuardar.Location = new System.Drawing.Point(834, 491);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(228, 60);
            this.btnGuardar.TabIndex = 71;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::VetPet_.Properties.Resources.arrow;
            this.pictureBox1.Location = new System.Drawing.Point(216, 500);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(43, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 68;
            this.pictureBox1.TabStop = false;
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
            this.btnRegresar.FlatAppearance.BorderSize = 2;
            this.btnRegresar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
            this.btnRegresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(212)))), ((int)(((byte)(226)))));
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(120)))), ((int)(((byte)(136)))));
            this.btnRegresar.Location = new System.Drawing.Point(40, 491);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(228, 60);
            this.btnRegresar.TabIndex = 69;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label4.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label4.Location = new System.Drawing.Point(21, 415);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(279, 38);
            this.label4.TabIndex = 64;
            this.label4.Text = "Medicamento";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label1.Location = new System.Drawing.Point(20, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 40);
            this.label1.TabIndex = 61;
            this.label1.Text = "Cantidad";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCantidad
            // 
            this.txtCantidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.txtCantidad.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.txtCantidad.Location = new System.Drawing.Point(316, 207);
            this.txtCantidad.MaxLength = 5;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(256, 40);
            this.txtCantidad.TabIndex = 60;
            this.txtCantidad.Text = "Cantidad";
            this.txtCantidad.Enter += new System.EventHandler(this.txtCantidad_Enter);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label3.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label3.Location = new System.Drawing.Point(20, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(280, 38);
            this.label3.TabIndex = 87;
            this.label3.Text = "Proveedor";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label5.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label5.Location = new System.Drawing.Point(20, 361);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(280, 38);
            this.label5.TabIndex = 89;
            this.label5.Text = "Producto";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label6.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label6.Location = new System.Drawing.Point(21, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(279, 40);
            this.label6.TabIndex = 95;
            this.label6.Text = "Total";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.txtTotal.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.txtTotal.Location = new System.Drawing.Point(316, 258);
            this.txtTotal.MaxLength = 5;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(256, 40);
            this.txtTotal.TabIndex = 94;
            this.txtTotal.Text = "Total";
            this.txtTotal.Enter += new System.EventHandler(this.txtTotal_Enter);
            // 
            // checkboxMedicamento
            // 
            this.checkboxMedicamento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.checkboxMedicamento.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkboxMedicamento.Font = new System.Drawing.Font("Verdana", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkboxMedicamento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.checkboxMedicamento.Location = new System.Drawing.Point(621, 184);
            this.checkboxMedicamento.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkboxMedicamento.Name = "checkboxMedicamento";
            this.checkboxMedicamento.Size = new System.Drawing.Size(231, 36);
            this.checkboxMedicamento.TabIndex = 98;
            this.checkboxMedicamento.Text = "Medicamento";
            this.checkboxMedicamento.UseVisualStyleBackColor = false;
            this.checkboxMedicamento.CheckedChanged += new System.EventHandler(this.checkboxMedicamento_CheckedChanged);
            // 
            // checkboxProducto
            // 
            this.checkboxProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.checkboxProducto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkboxProducto.Font = new System.Drawing.Font("Verdana", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkboxProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.checkboxProducto.Location = new System.Drawing.Point(873, 182);
            this.checkboxProducto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkboxProducto.Name = "checkboxProducto";
            this.checkboxProducto.Size = new System.Drawing.Size(182, 40);
            this.checkboxProducto.TabIndex = 99;
            this.checkboxProducto.Text = "Producto";
            this.checkboxProducto.UseVisualStyleBackColor = false;
            this.checkboxProducto.CheckedChanged += new System.EventHandler(this.checkboxProducto_CheckedChanged);
            // 
            // cmbMedicamento
            // 
            this.cmbMedicamento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.cmbMedicamento.Enabled = false;
            this.cmbMedicamento.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic);
            this.cmbMedicamento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.cmbMedicamento.FormattingEnabled = true;
            this.cmbMedicamento.Items.AddRange(new object[] {
            "Esencial",
            "No Esencial"});
            this.cmbMedicamento.Location = new System.Drawing.Point(316, 418);
            this.cmbMedicamento.Name = "cmbMedicamento";
            this.cmbMedicamento.Size = new System.Drawing.Size(256, 40);
            this.cmbMedicamento.TabIndex = 111;
            this.cmbMedicamento.Text = "NULL";
            this.cmbMedicamento.SelectedIndexChanged += new System.EventHandler(this.cmbMedicamento_SelectedIndexChanged);
            // 
            // cmbProducto
            // 
            this.cmbProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.cmbProducto.Enabled = false;
            this.cmbProducto.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic);
            this.cmbProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Items.AddRange(new object[] {
            "Esencial",
            "No Esencial"});
            this.cmbProducto.Location = new System.Drawing.Point(316, 365);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(256, 40);
            this.cmbProducto.TabIndex = 110;
            this.cmbProducto.Text = "NULL";
            this.cmbProducto.SelectedIndexChanged += new System.EventHandler(this.cmbProducto_SelectedIndexChanged);
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.cmbProveedor.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic);
            this.cmbProveedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Items.AddRange(new object[] {
            "Esencial",
            "No Esencial"});
            this.cmbProveedor.Location = new System.Drawing.Point(316, 311);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(256, 40);
            this.cmbProveedor.TabIndex = 109;
            this.cmbProveedor.SelectedIndexChanged += new System.EventHandler(this.cmbProveedor_SelectedIndexChanged);
            // 
            // txtIdMedicamento
            // 
            this.txtIdMedicamento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.txtIdMedicamento.Enabled = false;
            this.txtIdMedicamento.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdMedicamento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.txtIdMedicamento.Location = new System.Drawing.Point(621, 416);
            this.txtIdMedicamento.Name = "txtIdMedicamento";
            this.txtIdMedicamento.Size = new System.Drawing.Size(430, 40);
            this.txtIdMedicamento.TabIndex = 106;
            this.txtIdMedicamento.Text = "Id Medicamento";
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.txtIdProducto.Enabled = false;
            this.txtIdProducto.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.txtIdProducto.Location = new System.Drawing.Point(621, 364);
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.Size = new System.Drawing.Size(430, 40);
            this.txtIdProducto.TabIndex = 105;
            this.txtIdProducto.Text = "Id Producto";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(153)))), ((int)(((byte)(169)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(55)))), ((int)(((byte)(71)))));
            this.label2.Location = new System.Drawing.Point(617, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(434, 40);
            this.label2.TabIndex = 103;
            this.label2.Text = "Informacion de Id\'s";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.txtIdProveedor.Enabled = false;
            this.txtIdProveedor.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdProveedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.txtIdProveedor.Location = new System.Drawing.Point(621, 309);
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.Size = new System.Drawing.Size(430, 40);
            this.txtIdProveedor.TabIndex = 101;
            this.txtIdProveedor.Text = "Id Proveedor";
            // 
            // fechaRecibidoPicker
            // 
            this.fechaRecibidoPicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(88)))), ((int)(((byte)(104)))));
            this.fechaRecibidoPicker.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.fechaRecibidoPicker.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fechaRecibidoPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaRecibidoPicker.Location = new System.Drawing.Point(316, 156);
            this.fechaRecibidoPicker.Name = "fechaRecibidoPicker";
            this.fechaRecibidoPicker.Size = new System.Drawing.Size(256, 40);
            this.fechaRecibidoPicker.TabIndex = 112;
            // 
            // AlmacenRecibirPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(190)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.fechaRecibidoPicker);
            this.Controls.Add(this.cmbMedicamento);
            this.Controls.Add(this.cmbProducto);
            this.Controls.Add(this.cmbProveedor);
            this.Controls.Add(this.txtIdMedicamento);
            this.Controls.Add(this.txtIdProducto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIdProveedor);
            this.Controls.Add(this.checkboxProducto);
            this.Controls.Add(this.checkboxMedicamento);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCantidad);
            this.Name = "AlmacenRecibirPedido";
            this.Text = "AlmacenRecibirPedido";
            this.Load += new System.EventHandler(this.AlmacenRecibirPedido_Load);
            this.Resize += new System.EventHandler(this.AlmacenRecibirPedido_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.CheckBox checkboxMedicamento;
        private System.Windows.Forms.CheckBox checkboxProducto;
        private System.Windows.Forms.ComboBox cmbMedicamento;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.TextBox txtIdMedicamento;
        private System.Windows.Forms.TextBox txtIdProducto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdProveedor;
        private System.Windows.Forms.DateTimePicker fechaRecibidoPicker;
    }
}