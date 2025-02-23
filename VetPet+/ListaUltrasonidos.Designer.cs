namespace VetPet_
{
    partial class ListaUltrasonidos
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.BtnEliminarCirugía = new System.Windows.Forms.Button();
            this.BtnAgregarCirugía = new System.Windows.Forms.Button();
            this.BtnAgregarTipoDeUltrasonidos = new System.Windows.Forms.Button();
            this.BtnRegresar = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.IndianRed;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 85);
            this.panel1.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(369, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(493, 55);
            this.label2.TabIndex = 20;
            this.label2.Text = "Lista de Ultrasonido";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(661, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(256, 22);
            this.label4.TabIndex = 34;
            this.label4.Text = "Información de Ultrasonido";
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(211)))), ((int)(((byte)(181)))));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.dataGridView2.Location = new System.Drawing.Point(665, 193);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(371, 241);
            this.dataGridView2.TabIndex = 32;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(211)))), ((int)(((byte)(181)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(41, 140);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(483, 294);
            this.dataGridView1.TabIndex = 33;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(211)))), ((int)(((byte)(181)))));
            this.textBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(41, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(885, 29);
            this.textBox1.TabIndex = 31;
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(190)))), ((int)(((byte)(149)))));
            this.BtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnBuscar.Location = new System.Drawing.Point(932, 100);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(104, 35);
            this.BtnBuscar.TabIndex = 26;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.UseVisualStyleBackColor = false;
            // 
            // BtnEliminarCirugía
            // 
            this.BtnEliminarCirugía.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(190)))), ((int)(((byte)(149)))));
            this.BtnEliminarCirugía.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEliminarCirugía.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEliminarCirugía.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnEliminarCirugía.Location = new System.Drawing.Point(874, 440);
            this.BtnEliminarCirugía.Name = "BtnEliminarCirugía";
            this.BtnEliminarCirugía.Size = new System.Drawing.Size(162, 55);
            this.BtnEliminarCirugía.TabIndex = 27;
            this.BtnEliminarCirugía.Text = "Eliminar Ultrasonido";
            this.BtnEliminarCirugía.UseVisualStyleBackColor = false;
            // 
            // BtnAgregarCirugía
            // 
            this.BtnAgregarCirugía.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(190)))), ((int)(((byte)(149)))));
            this.BtnAgregarCirugía.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAgregarCirugía.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregarCirugía.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnAgregarCirugía.Location = new System.Drawing.Point(665, 440);
            this.BtnAgregarCirugía.Name = "BtnAgregarCirugía";
            this.BtnAgregarCirugía.Size = new System.Drawing.Size(203, 55);
            this.BtnAgregarCirugía.TabIndex = 28;
            this.BtnAgregarCirugía.Text = "Agregar Nueva Ultrasonido";
            this.BtnAgregarCirugía.UseVisualStyleBackColor = false;
            // 
            // BtnAgregarTipoDeUltrasonidos
            // 
            this.BtnAgregarTipoDeUltrasonidos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(190)))), ((int)(((byte)(149)))));
            this.BtnAgregarTipoDeUltrasonidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAgregarTipoDeUltrasonidos.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregarTipoDeUltrasonidos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnAgregarTipoDeUltrasonidos.Location = new System.Drawing.Point(41, 440);
            this.BtnAgregarTipoDeUltrasonidos.Name = "BtnAgregarTipoDeUltrasonidos";
            this.BtnAgregarTipoDeUltrasonidos.Size = new System.Drawing.Size(483, 44);
            this.BtnAgregarTipoDeUltrasonidos.TabIndex = 29;
            this.BtnAgregarTipoDeUltrasonidos.Text = "Agregar Tipo de Ultrasonido";
            this.BtnAgregarTipoDeUltrasonidos.UseVisualStyleBackColor = false;
            this.BtnAgregarTipoDeUltrasonidos.Click += new System.EventHandler(this.BtnAgregarTipoDeUltrasonidos_Click);
            // 
            // BtnRegresar
            // 
            this.BtnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(190)))), ((int)(((byte)(149)))));
            this.BtnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegresar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnRegresar.Location = new System.Drawing.Point(42, 496);
            this.BtnRegresar.Name = "BtnRegresar";
            this.BtnRegresar.Size = new System.Drawing.Size(121, 49);
            this.BtnRegresar.TabIndex = 30;
            this.BtnRegresar.Text = "Regresar";
            this.BtnRegresar.UseVisualStyleBackColor = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Tipo de Ultrasonido";
            this.Column1.Name = "Column1";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Tipo de Ultrasonido";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // ListaUltrasonidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(149)))), ((int)(((byte)(112)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.BtnEliminarCirugía);
            this.Controls.Add(this.BtnAgregarCirugía);
            this.Controls.Add(this.BtnAgregarTipoDeUltrasonidos);
            this.Controls.Add(this.BtnRegresar);
            this.Name = "ListaUltrasonidos";
            this.Text = "ListaUltrasonidos";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.Button BtnEliminarCirugía;
        private System.Windows.Forms.Button BtnAgregarCirugía;
        private System.Windows.Forms.Button BtnAgregarTipoDeUltrasonidos;
        private System.Windows.Forms.Button BtnRegresar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}