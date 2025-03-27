namespace VetPet_
{
    partial class ReportesVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportesVenta));
            this.label1 = new System.Windows.Forms.Label();
            this.BtnVenMasAlt = new System.Windows.Forms.Button();
            this.lblPreview = new System.Windows.Forms.Label();
            this.BtnVentMasBaj = new System.Windows.Forms.Button();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.BtnGenerar = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dateTime1 = new System.Windows.Forms.DateTimePicker();
            this.dateTime2 = new System.Windows.Forms.DateTimePicker();
            this.lblA = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnMenu = new System.Windows.Forms.Button();
            this.BtnVolver = new System.Windows.Forms.Button();
            this.pdfViewVent = new AxAcroPDFLib.AxAcroPDF();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewVent)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Mono", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(797, 106);
            this.label1.TabIndex = 25;
            this.label1.Text = "Reportes: Ventas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BtnVenMasAlt
            // 
            this.BtnVenMasAlt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnVenMasAlt.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVenMasAlt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnVenMasAlt.Location = new System.Drawing.Point(53, 142);
            this.BtnVenMasAlt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnVenMasAlt.Name = "BtnVenMasAlt";
            this.BtnVenMasAlt.Size = new System.Drawing.Size(349, 55);
            this.BtnVenMasAlt.TabIndex = 26;
            this.BtnVenMasAlt.Tag = "1";
            this.BtnVenMasAlt.Text = "Ventas más Altas";
            this.BtnVenMasAlt.UseVisualStyleBackColor = false;
            this.BtnVenMasAlt.Click += new System.EventHandler(this.BtnVenMasAlt_Click);
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.lblPreview.Location = new System.Drawing.Point(1204, 103);
            this.lblPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(207, 35);
            this.lblPreview.TabIndex = 49;
            this.lblPreview.Tag = "1";
            this.lblPreview.Text = "Vista Previa";
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPreview.Visible = false;
            // 
            // BtnVentMasBaj
            // 
            this.BtnVentMasBaj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnVentMasBaj.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVentMasBaj.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnVentMasBaj.Location = new System.Drawing.Point(53, 236);
            this.BtnVentMasBaj.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnVentMasBaj.Name = "BtnVentMasBaj";
            this.BtnVentMasBaj.Size = new System.Drawing.Size(349, 55);
            this.BtnVentMasBaj.TabIndex = 50;
            this.BtnVentMasBaj.Tag = "1";
            this.BtnVentMasBaj.Text = "Ventas más Bajas";
            this.BtnVentMasBaj.UseVisualStyleBackColor = false;
            this.BtnVentMasBaj.Click += new System.EventHandler(this.BtnVentMasBaj_Click);
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnImprimir.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnImprimir.Location = new System.Drawing.Point(588, 620);
            this.BtnImprimir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(219, 55);
            this.BtnImprimir.TabIndex = 51;
            this.BtnImprimir.Tag = "1";
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.UseVisualStyleBackColor = false;
            this.BtnImprimir.Visible = false;
            // 
            // BtnGenerar
            // 
            this.BtnGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnGenerar.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnGenerar.Location = new System.Drawing.Point(1193, 620);
            this.BtnGenerar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnGenerar.Name = "BtnGenerar";
            this.BtnGenerar.Size = new System.Drawing.Size(219, 55);
            this.BtnGenerar.TabIndex = 52;
            this.BtnGenerar.Tag = "1";
            this.BtnGenerar.Text = "Generar";
            this.BtnGenerar.UseVisualStyleBackColor = false;
            this.BtnGenerar.Visible = false;
            this.BtnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.lblFecha.Location = new System.Drawing.Point(953, 665);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(95, 35);
            this.lblFecha.TabIndex = 54;
            this.lblFecha.Tag = "1";
            this.lblFecha.Text = "Fecha";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblFecha.Visible = false;
            // 
            // dateTime1
            // 
            this.dateTime1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.dateTime1.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime1.Location = new System.Drawing.Point(815, 635);
            this.dateTime1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTime1.Name = "dateTime1";
            this.dateTime1.Size = new System.Drawing.Size(159, 26);
            this.dateTime1.TabIndex = 53;
            this.dateTime1.Tag = "1";
            this.dateTime1.Visible = false;
            // 
            // dateTime2
            // 
            this.dateTime2.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.dateTime2.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime2.Location = new System.Drawing.Point(1027, 635);
            this.dateTime2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTime2.Name = "dateTime2";
            this.dateTime2.Size = new System.Drawing.Size(159, 26);
            this.dateTime2.TabIndex = 55;
            this.dateTime2.Tag = "1";
            this.dateTime2.Visible = false;
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.lblA.Location = new System.Drawing.Point(985, 628);
            this.lblA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(31, 35);
            this.lblA.TabIndex = 56;
            this.lblA.Tag = "1";
            this.lblA.Text = "a";
            this.lblA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblA.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.panel1.Location = new System.Drawing.Point(492, 142);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(7, 554);
            this.panel1.TabIndex = 63;
            // 
            // BtnMenu
            // 
            this.BtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnMenu.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnMenu.Location = new System.Drawing.Point(36, 628);
            this.BtnMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnMenu.Name = "BtnMenu";
            this.BtnMenu.Size = new System.Drawing.Size(180, 48);
            this.BtnMenu.TabIndex = 65;
            this.BtnMenu.Text = "Menu";
            this.BtnMenu.UseVisualStyleBackColor = false;
            this.BtnMenu.Click += new System.EventHandler(this.BtnMenu_Click);
            // 
            // BtnVolver
            // 
            this.BtnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnVolver.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnVolver.Location = new System.Drawing.Point(265, 628);
            this.BtnVolver.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnVolver.Name = "BtnVolver";
            this.BtnVolver.Size = new System.Drawing.Size(180, 48);
            this.BtnVolver.TabIndex = 64;
            this.BtnVolver.Tag = "1";
            this.BtnVolver.Text = "Volver";
            this.BtnVolver.UseVisualStyleBackColor = false;
            this.BtnVolver.Visible = false;
            this.BtnVolver.Click += new System.EventHandler(this.BtnVolver_Click);
            // 
            // pdfViewVent
            // 
            this.pdfViewVent.Enabled = true;
            this.pdfViewVent.Location = new System.Drawing.Point(588, 142);
            this.pdfViewVent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pdfViewVent.Name = "pdfViewVent";
            this.pdfViewVent.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfViewVent.OcxState")));
            this.pdfViewVent.Size = new System.Drawing.Size(823, 441);
            this.pdfViewVent.TabIndex = 69;
            this.pdfViewVent.Tag = "1";
            this.pdfViewVent.Visible = false;
            // 
            // ReportesVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(9)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1443, 710);
            this.Controls.Add(this.pdfViewVent);
            this.Controls.Add(this.BtnMenu);
            this.Controls.Add(this.BtnVolver);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.dateTime2);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dateTime1);
            this.Controls.Add(this.BtnGenerar);
            this.Controls.Add(this.BtnImprimir);
            this.Controls.Add(this.BtnVentMasBaj);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.BtnVenMasAlt);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ReportesVenta";
            this.Text = "ReportesVenta";
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewVent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnVenMasAlt;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button BtnVentMasBaj;
        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.Button BtnGenerar;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dateTime1;
        private System.Windows.Forms.DateTimePicker dateTime2;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnMenu;
        private System.Windows.Forms.Button BtnVolver;
        private AxAcroPDFLib.AxAcroPDF pdfViewVent;
    }
}