namespace VetPet_
{
    partial class ReportesCitas
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
            this.BtnRazonMasFrec = new System.Windows.Forms.Button();
            this.BtnRazonMenFrec = new System.Windows.Forms.Button();
            this.pdfViewCita = new Patagames.Pdf.Net.Controls.WinForms.PdfViewer();
            this.lblPreview = new System.Windows.Forms.Label();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.BtnGenerar = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dateTime1 = new System.Windows.Forms.DateTimePicker();
            this.dateTime2 = new System.Windows.Forms.DateTimePicker();
            this.lblA = new System.Windows.Forms.Label();
            this.BtnVolver = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Mono", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 85);
            this.label1.TabIndex = 25;
            this.label1.Text = "Reportes: Citas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BtnRazonMasFrec
            // 
            this.BtnRazonMasFrec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnRazonMasFrec.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRazonMasFrec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnRazonMasFrec.Location = new System.Drawing.Point(40, 115);
            this.BtnRazonMasFrec.Name = "BtnRazonMasFrec";
            this.BtnRazonMasFrec.Size = new System.Drawing.Size(262, 66);
            this.BtnRazonMasFrec.TabIndex = 26;
            this.BtnRazonMasFrec.Text = "Razón de Cita más Frecuente";
            this.BtnRazonMasFrec.UseVisualStyleBackColor = false;
            this.BtnRazonMasFrec.Click += new System.EventHandler(this.BtnRazonMasFrec_Click);
            // 
            // BtnRazonMenFrec
            // 
            this.BtnRazonMenFrec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnRazonMenFrec.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRazonMenFrec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnRazonMenFrec.Location = new System.Drawing.Point(40, 197);
            this.BtnRazonMenFrec.Name = "BtnRazonMenFrec";
            this.BtnRazonMenFrec.Size = new System.Drawing.Size(262, 66);
            this.BtnRazonMenFrec.TabIndex = 27;
            this.BtnRazonMenFrec.Text = "Razón de Cita menos Frecuente";
            this.BtnRazonMenFrec.UseVisualStyleBackColor = false;
            this.BtnRazonMenFrec.Click += new System.EventHandler(this.BtnRazonMenFrec_Click);
            // 
            // pdfViewCita
            // 
            this.pdfViewCita.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pdfViewCita.CurrentIndex = -1;
            this.pdfViewCita.CurrentPageHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.pdfViewCita.Document = null;
            this.pdfViewCita.FormHighlightColor = System.Drawing.Color.Transparent;
            this.pdfViewCita.FormsBlendMode = Patagames.Pdf.Enums.BlendTypes.FXDIB_BLEND_MULTIPLY;
            this.pdfViewCita.LoadingIconText = "Loading...";
            this.pdfViewCita.Location = new System.Drawing.Point(441, 115);
            this.pdfViewCita.Margin = new System.Windows.Forms.Padding(4);
            this.pdfViewCita.MouseMode = Patagames.Pdf.Net.Controls.WinForms.MouseModes.Default;
            this.pdfViewCita.Name = "pdfViewCita";
            this.pdfViewCita.OptimizedLoadThreshold = 1000;
            this.pdfViewCita.Padding = new System.Windows.Forms.Padding(10);
            this.pdfViewCita.PageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pdfViewCita.PageAutoDispose = true;
            this.pdfViewCita.PageBackColor = System.Drawing.Color.White;
            this.pdfViewCita.PageBorderColor = System.Drawing.Color.Black;
            this.pdfViewCita.PageMargin = new System.Windows.Forms.Padding(10);
            this.pdfViewCita.PageSeparatorColor = System.Drawing.Color.Gray;
            this.pdfViewCita.RenderFlags = ((Patagames.Pdf.Enums.RenderFlags)((Patagames.Pdf.Enums.RenderFlags.FPDF_LCD_TEXT | Patagames.Pdf.Enums.RenderFlags.FPDF_NO_CATCH)));
            this.pdfViewCita.ShowCurrentPageHighlight = true;
            this.pdfViewCita.ShowLoadingIcon = true;
            this.pdfViewCita.ShowPageSeparator = true;
            this.pdfViewCita.Size = new System.Drawing.Size(618, 381);
            this.pdfViewCita.SizeMode = Patagames.Pdf.Net.Controls.WinForms.SizeModes.FitToWidth;
            this.pdfViewCita.TabIndex = 36;
            this.pdfViewCita.TextSelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.pdfViewCita.TilesCount = 2;
            this.pdfViewCita.UseProgressiveRender = true;
            this.pdfViewCita.ViewMode = Patagames.Pdf.Net.Controls.WinForms.ViewModes.Vertical;
            this.pdfViewCita.Visible = false;
            this.pdfViewCita.Zoom = 1F;
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.lblPreview.Location = new System.Drawing.Point(903, 84);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(156, 28);
            this.lblPreview.TabIndex = 47;
            this.lblPreview.Text = "Vista Previa";
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPreview.Visible = false;
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnImprimir.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnImprimir.Location = new System.Drawing.Point(441, 504);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(164, 45);
            this.BtnImprimir.TabIndex = 48;
            this.BtnImprimir.Text = "Imprimir";
            this.BtnImprimir.UseVisualStyleBackColor = false;
            this.BtnImprimir.Visible = false;
            // 
            // BtnGenerar
            // 
            this.BtnGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnGenerar.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnGenerar.Location = new System.Drawing.Point(895, 504);
            this.BtnGenerar.Name = "BtnGenerar";
            this.BtnGenerar.Size = new System.Drawing.Size(164, 45);
            this.BtnGenerar.TabIndex = 49;
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
            this.lblFecha.Location = new System.Drawing.Point(715, 540);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(72, 28);
            this.lblFecha.TabIndex = 51;
            this.lblFecha.Text = "Fecha";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblFecha.Visible = false;
            // 
            // dateTime1
            // 
            this.dateTime1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.dateTime1.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime1.Location = new System.Drawing.Point(611, 516);
            this.dateTime1.Name = "dateTime1";
            this.dateTime1.Size = new System.Drawing.Size(120, 23);
            this.dateTime1.TabIndex = 50;
            this.dateTime1.Visible = false;
            // 
            // dateTime2
            // 
            this.dateTime2.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.dateTime2.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime2.Location = new System.Drawing.Point(770, 516);
            this.dateTime2.Name = "dateTime2";
            this.dateTime2.Size = new System.Drawing.Size(120, 23);
            this.dateTime2.TabIndex = 52;
            this.dateTime2.Visible = false;
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.lblA.Location = new System.Drawing.Point(739, 510);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(24, 28);
            this.lblA.TabIndex = 60;
            this.lblA.Text = "a";
            this.lblA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblA.Visible = false;
            // 
            // BtnVolver
            // 
            this.BtnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnVolver.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnVolver.Location = new System.Drawing.Point(199, 510);
            this.BtnVolver.Name = "BtnVolver";
            this.BtnVolver.Size = new System.Drawing.Size(135, 39);
            this.BtnVolver.TabIndex = 61;
            this.BtnVolver.Text = "Volver";
            this.BtnVolver.UseVisualStyleBackColor = false;
            this.BtnVolver.Visible = false;
            this.BtnVolver.Click += new System.EventHandler(this.BtnVolver_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.panel1.Location = new System.Drawing.Point(369, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 450);
            this.panel1.TabIndex = 62;
            // 
            // BtnMenu
            // 
            this.BtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnMenu.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnMenu.Location = new System.Drawing.Point(27, 510);
            this.BtnMenu.Name = "BtnMenu";
            this.BtnMenu.Size = new System.Drawing.Size(135, 39);
            this.BtnMenu.TabIndex = 63;
            this.BtnMenu.Text = "Menu";
            this.BtnMenu.UseVisualStyleBackColor = false;
            this.BtnMenu.Click += new System.EventHandler(this.BtnMenu_Click);
            // 
            // ReportesCitas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(9)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.BtnMenu);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnVolver);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.dateTime2);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dateTime1);
            this.Controls.Add(this.BtnGenerar);
            this.Controls.Add(this.BtnImprimir);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.pdfViewCita);
            this.Controls.Add(this.BtnRazonMenFrec);
            this.Controls.Add(this.BtnRazonMasFrec);
            this.Controls.Add(this.label1);
            this.Name = "ReportesCitas";
            this.Text = "ReportesCitas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnRazonMasFrec;
        private System.Windows.Forms.Button BtnRazonMenFrec;
        private Patagames.Pdf.Net.Controls.WinForms.PdfViewer pdfViewCita;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.Button BtnGenerar;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dateTime1;
        private System.Windows.Forms.DateTimePicker dateTime2;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Button BtnVolver;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnMenu;
    }
}