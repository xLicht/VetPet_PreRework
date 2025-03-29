namespace VetPet_
{
    partial class ReportesClientes: VetPet_.FormPadre
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
            this.label1 = new System.Windows.Forms.Label();
            this.BtnDueñosMasFrec = new System.Windows.Forms.Button();
            this.BtnMascMasFrec = new System.Windows.Forms.Button();
            this.BtnDueñosMenFrec = new System.Windows.Forms.Button();
            this.BtnMascMenFrec = new System.Windows.Forms.Button();
            this.pdfViewClient = new Patagames.Pdf.Net.Controls.WinForms.PdfViewer();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.BtnGenerar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTime1 = new System.Windows.Forms.DateTimePicker();
            this.dateTime2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnMenu = new System.Windows.Forms.Button();
            this.BtnVolver = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.label1.Size = new System.Drawing.Size(891, 106);
            this.label1.TabIndex = 25;
            this.label1.Text = "Reportes: Clientes";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BtnDueñosMasFrec
            // 
            this.BtnDueñosMasFrec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnDueñosMasFrec.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDueñosMasFrec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnDueñosMasFrec.Location = new System.Drawing.Point(53, 142);
            this.BtnDueñosMasFrec.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDueñosMasFrec.Name = "BtnDueñosMasFrec";
            this.BtnDueñosMasFrec.Size = new System.Drawing.Size(380, 55);
            this.BtnDueñosMasFrec.TabIndex = 26;
            this.BtnDueñosMasFrec.Tag = "1";
            this.BtnDueñosMasFrec.Text = "Dueños Frecuentes";
            this.BtnDueñosMasFrec.UseVisualStyleBackColor = false;
            this.BtnDueñosMasFrec.Click += new System.EventHandler(this.BtnDueñosMasFrec_Click);
            // 
            // BtnMascMasFrec
            // 
            this.BtnMascMasFrec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnMascMasFrec.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMascMasFrec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnMascMasFrec.Location = new System.Drawing.Point(53, 220);
            this.BtnMascMasFrec.Margin = new System.Windows.Forms.Padding(4);
            this.BtnMascMasFrec.Name = "BtnMascMasFrec";
            this.BtnMascMasFrec.Size = new System.Drawing.Size(380, 55);
            this.BtnMascMasFrec.TabIndex = 27;
            this.BtnMascMasFrec.Tag = "1";
            this.BtnMascMasFrec.Text = "Mascotas Frecuentes";
            this.BtnMascMasFrec.UseVisualStyleBackColor = false;
            this.BtnMascMasFrec.Click += new System.EventHandler(this.BtnMascMasFrec_Click);
            // 
            // BtnDueñosMenFrec
            // 
            this.BtnDueñosMenFrec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnDueñosMenFrec.Font = new System.Drawing.Font("Cascadia Mono", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDueñosMenFrec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnDueñosMenFrec.Location = new System.Drawing.Point(53, 299);
            this.BtnDueñosMenFrec.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDueñosMenFrec.Name = "BtnDueñosMenFrec";
            this.BtnDueñosMenFrec.Size = new System.Drawing.Size(380, 55);
            this.BtnDueñosMenFrec.TabIndex = 28;
            this.BtnDueñosMenFrec.Tag = "1";
            this.BtnDueñosMenFrec.Text = "Dueños menos Frecuentes";
            this.BtnDueñosMenFrec.UseVisualStyleBackColor = false;
            this.BtnDueñosMenFrec.Click += new System.EventHandler(this.BtnDueñosMenFrec_Click);
            // 
            // BtnMascMenFrec
            // 
            this.BtnMascMenFrec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnMascMenFrec.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMascMenFrec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnMascMenFrec.Location = new System.Drawing.Point(53, 378);
            this.BtnMascMenFrec.Margin = new System.Windows.Forms.Padding(4);
            this.BtnMascMenFrec.Name = "BtnMascMenFrec";
            this.BtnMascMenFrec.Size = new System.Drawing.Size(380, 55);
            this.BtnMascMenFrec.TabIndex = 29;
            this.BtnMascMenFrec.Tag = "1";
            this.BtnMascMenFrec.Text = "Mascotas menos Frecuentes";
            this.BtnMascMenFrec.UseVisualStyleBackColor = false;
            this.BtnMascMenFrec.Click += new System.EventHandler(this.BtnMascMenFrec_Click);
            // 
            // pdfViewClient
            // 
            this.pdfViewClient.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pdfViewClient.CurrentIndex = -1;
            this.pdfViewClient.CurrentPageHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.pdfViewClient.Document = null;
            this.pdfViewClient.FormHighlightColor = System.Drawing.Color.Transparent;
            this.pdfViewClient.FormsBlendMode = Patagames.Pdf.Enums.BlendTypes.FXDIB_BLEND_MULTIPLY;
            this.pdfViewClient.LoadingIconText = "Loading...";
            this.pdfViewClient.Location = new System.Drawing.Point(588, 142);
            this.pdfViewClient.Margin = new System.Windows.Forms.Padding(5);
            this.pdfViewClient.MouseMode = Patagames.Pdf.Net.Controls.WinForms.MouseModes.Default;
            this.pdfViewClient.Name = "pdfViewClient";
            this.pdfViewClient.OptimizedLoadThreshold = 1000;
            this.pdfViewClient.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pdfViewClient.PageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pdfViewClient.PageAutoDispose = true;
            this.pdfViewClient.PageBackColor = System.Drawing.Color.White;
            this.pdfViewClient.PageBorderColor = System.Drawing.Color.Black;
            this.pdfViewClient.PageMargin = new System.Windows.Forms.Padding(10);
            this.pdfViewClient.PageSeparatorColor = System.Drawing.Color.Gray;
            this.pdfViewClient.RenderFlags = ((Patagames.Pdf.Enums.RenderFlags)((Patagames.Pdf.Enums.RenderFlags.FPDF_LCD_TEXT | Patagames.Pdf.Enums.RenderFlags.FPDF_NO_CATCH)));
            this.pdfViewClient.ShowCurrentPageHighlight = true;
            this.pdfViewClient.ShowLoadingIcon = true;
            this.pdfViewClient.ShowPageSeparator = true;
            this.pdfViewClient.Size = new System.Drawing.Size(824, 469);
            this.pdfViewClient.SizeMode = Patagames.Pdf.Net.Controls.WinForms.SizeModes.FitToWidth;
            this.pdfViewClient.TabIndex = 37;
            this.pdfViewClient.Tag = "1";
            this.pdfViewClient.TextSelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.pdfViewClient.TilesCount = 2;
            this.pdfViewClient.UseProgressiveRender = true;
            this.pdfViewClient.ViewMode = Patagames.Pdf.Net.Controls.WinForms.ViewModes.Vertical;
            this.pdfViewClient.Visible = false;
            this.pdfViewClient.Zoom = 1F;
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnImprimir.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnImprimir.Location = new System.Drawing.Point(588, 620);
            this.BtnImprimir.Margin = new System.Windows.Forms.Padding(4);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(219, 55);
            this.BtnImprimir.TabIndex = 38;
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
            this.BtnGenerar.Margin = new System.Windows.Forms.Padding(4);
            this.BtnGenerar.Name = "BtnGenerar";
            this.BtnGenerar.Size = new System.Drawing.Size(219, 55);
            this.BtnGenerar.TabIndex = 39;
            this.BtnGenerar.Tag = "1";
            this.BtnGenerar.Text = "Generar";
            this.BtnGenerar.UseVisualStyleBackColor = false;
            this.BtnGenerar.Visible = false;
            this.BtnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.label2.Location = new System.Drawing.Point(1204, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 35);
            this.label2.TabIndex = 48;
            this.label2.Tag = "1";
            this.label2.Text = "Vista Previa";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.label3.Location = new System.Drawing.Point(954, 665);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 35);
            this.label3.TabIndex = 50;
            this.label3.Tag = "1";
            this.label3.Text = "Fecha";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label3.Visible = false;
            // 
            // dateTime1
            // 
            this.dateTime1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.dateTime1.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime1.Location = new System.Drawing.Point(815, 635);
            this.dateTime1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTime1.Name = "dateTime1";
            this.dateTime1.Size = new System.Drawing.Size(159, 26);
            this.dateTime1.TabIndex = 49;
            this.dateTime1.Tag = "1";
            this.dateTime1.Visible = false;
            // 
            // dateTime2
            // 
            this.dateTime2.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.dateTime2.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime2.Location = new System.Drawing.Point(1026, 635);
            this.dateTime2.Margin = new System.Windows.Forms.Padding(4);
            this.dateTime2.Name = "dateTime2";
            this.dateTime2.Size = new System.Drawing.Size(159, 26);
            this.dateTime2.TabIndex = 58;
            this.dateTime2.Tag = "1";
            this.dateTime2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.label4.Location = new System.Drawing.Point(985, 628);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 35);
            this.label4.TabIndex = 59;
            this.label4.Tag = "1";
            this.label4.Text = "a";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label4.Visible = false;
            // 
            // BtnMenu
            // 
            this.BtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnMenu.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnMenu.Location = new System.Drawing.Point(36, 628);
            this.BtnMenu.Margin = new System.Windows.Forms.Padding(4);
            this.BtnMenu.Name = "BtnMenu";
            this.BtnMenu.Size = new System.Drawing.Size(180, 48);
            this.BtnMenu.TabIndex = 69;
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
            this.BtnVolver.Margin = new System.Windows.Forms.Padding(4);
            this.BtnVolver.Name = "BtnVolver";
            this.BtnVolver.Size = new System.Drawing.Size(180, 48);
            this.BtnVolver.TabIndex = 68;
            this.BtnVolver.Tag = "1";
            this.BtnVolver.Text = "Volver";
            this.BtnVolver.UseVisualStyleBackColor = false;
            this.BtnVolver.Visible = false;
            this.BtnVolver.Click += new System.EventHandler(this.BtnVolver_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.panel1.Location = new System.Drawing.Point(492, 142);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(7, 554);
            this.panel1.TabIndex = 70;
            // 
            // ReportesClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(9)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1443, 710);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnMenu);
            this.Controls.Add(this.BtnVolver);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTime2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTime1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnGenerar);
            this.Controls.Add(this.BtnImprimir);
            this.Controls.Add(this.pdfViewClient);
            this.Controls.Add(this.BtnMascMenFrec);
            this.Controls.Add(this.BtnDueñosMenFrec);
            this.Controls.Add(this.BtnMascMasFrec);
            this.Controls.Add(this.BtnDueñosMasFrec);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReportesClientes";
            this.Text = "ReportesClientes";
            this.Load += new System.EventHandler(this.ReportesClientes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnDueñosMasFrec;
        private System.Windows.Forms.Button BtnMascMasFrec;
        private System.Windows.Forms.Button BtnDueñosMenFrec;
        private System.Windows.Forms.Button BtnMascMenFrec;
        private Patagames.Pdf.Net.Controls.WinForms.PdfViewer pdfViewClient;
        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.Button BtnGenerar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTime1;
        private System.Windows.Forms.DateTimePicker dateTime2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnMenu;
        private System.Windows.Forms.Button BtnVolver;
        private System.Windows.Forms.Panel panel1;
    }
}