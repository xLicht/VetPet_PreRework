namespace VetPet_
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BtnNuevoCorte = new System.Windows.Forms.PictureBox();
            this.BtnNuevaVenta = new System.Windows.Forms.PictureBox();
            this.BtnNuevaCita = new System.Windows.Forms.PictureBox();
            this.lblSaludo = new System.Windows.Forms.Label();
            this.DgvCitas = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNuevoCorte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNuevaVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNuevaCita)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCitas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnNuevoCorte
            // 
            this.BtnNuevoCorte.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnNuevoCorte.BackgroundImage")));
            this.BtnNuevoCorte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnNuevoCorte.Location = new System.Drawing.Point(27, 473);
            this.BtnNuevoCorte.Name = "BtnNuevoCorte";
            this.BtnNuevoCorte.Size = new System.Drawing.Size(296, 92);
            this.BtnNuevoCorte.TabIndex = 2;
            this.BtnNuevoCorte.TabStop = false;
            this.BtnNuevoCorte.Click += new System.EventHandler(this.BtnNuevoCorte_Click);
            // 
            // BtnNuevaVenta
            // 
            this.BtnNuevaVenta.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnNuevaVenta.BackgroundImage")));
            this.BtnNuevaVenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnNuevaVenta.Location = new System.Drawing.Point(27, 335);
            this.BtnNuevaVenta.Name = "BtnNuevaVenta";
            this.BtnNuevaVenta.Size = new System.Drawing.Size(296, 92);
            this.BtnNuevaVenta.TabIndex = 1;
            this.BtnNuevaVenta.TabStop = false;
            this.BtnNuevaVenta.Click += new System.EventHandler(this.BtnNuevaVenta_Click);
            // 
            // BtnNuevaCita
            // 
            this.BtnNuevaCita.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnNuevaCita.BackgroundImage")));
            this.BtnNuevaCita.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnNuevaCita.Location = new System.Drawing.Point(27, 195);
            this.BtnNuevaCita.Name = "BtnNuevaCita";
            this.BtnNuevaCita.Size = new System.Drawing.Size(296, 92);
            this.BtnNuevaCita.TabIndex = 0;
            this.BtnNuevaCita.TabStop = false;
            this.BtnNuevaCita.Click += new System.EventHandler(this.BtnNuevaCita_Click);
            // 
            // lblSaludo
            // 
            this.lblSaludo.AutoSize = true;
            this.lblSaludo.Font = new System.Drawing.Font("Cascadia Code", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaludo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(0)))), ((int)(((byte)(91)))));
            this.lblSaludo.Location = new System.Drawing.Point(12, 35);
            this.lblSaludo.Name = "lblSaludo";
            this.lblSaludo.Size = new System.Drawing.Size(607, 85);
            this.lblSaludo.TabIndex = 3;
            this.lblSaludo.Text = "Hola Juan Perez";
            // 
            // DgvCitas
            // 
            this.DgvCitas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvCitas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvCitas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(0)))), ((int)(((byte)(91)))));
            this.DgvCitas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvCitas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.DgvCitas.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(0)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvCitas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCitas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(103)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvCitas.DefaultCellStyle = dataGridViewCellStyle4;
            this.DgvCitas.EnableHeadersVisualStyles = false;
            this.DgvCitas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.DgvCitas.Location = new System.Drawing.Point(400, 195);
            this.DgvCitas.Margin = new System.Windows.Forms.Padding(2);
            this.DgvCitas.Name = "DgvCitas";
            this.DgvCitas.RowHeadersVisible = false;
            this.DgvCitas.RowHeadersWidth = 60;
            this.DgvCitas.RowTemplate.Height = 24;
            this.DgvCitas.Size = new System.Drawing.Size(641, 350);
            this.DgvCitas.TabIndex = 122;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 50F;
            this.Column1.HeaderText = "IdCita";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Dueño";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 80F;
            this.Column3.HeaderText = "Mascota";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 50F;
            this.Column4.HeaderText = "Hora";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Motivo";
            this.Column5.Name = "Column5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Code", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(0)))), ((int)(((byte)(91)))));
            this.label2.Location = new System.Drawing.Point(392, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 43);
            this.label2.TabIndex = 124;
            this.label2.Text = "Citas de Hoy";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::VetPet_.Properties.Resources.VetPetLogoNew;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(864, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 178);
            this.pictureBox1.TabIndex = 125;
            this.pictureBox1.TabStop = false;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(190)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DgvCitas);
            this.Controls.Add(this.lblSaludo);
            this.Controls.Add(this.BtnNuevoCorte);
            this.Controls.Add(this.BtnNuevaVenta);
            this.Controls.Add(this.BtnNuevaCita);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnNuevoCorte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNuevaVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNuevaCita)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCitas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BtnNuevaCita;
        private System.Windows.Forms.PictureBox BtnNuevaVenta;
        private System.Windows.Forms.PictureBox BtnNuevoCorte;
        private System.Windows.Forms.Label lblSaludo;
        private System.Windows.Forms.DataGridView DgvCitas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}