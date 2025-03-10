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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pdfViewVent = new Patagames.Pdf.Net.Controls.WinForms.PdfViewer();
            this.lblPreview = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.BtnGenerar = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dateTime1 = new System.Windows.Forms.DateTimePicker();
            this.dateTime2 = new System.Windows.Forms.DateTimePicker();
            this.lblA = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnMenu = new System.Windows.Forms.Button();
            this.BtnVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Mono", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(645, 85);
            this.label1.TabIndex = 25;
            this.label1.Text = "Reportes: Ventas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button1.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button1.Location = new System.Drawing.Point(40, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(262, 45);
            this.button1.TabIndex = 26;
            this.button1.Text = "Ventas más Altas";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // pdfViewVent
            // 
            this.pdfViewVent.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pdfViewVent.CurrentIndex = -1;
            this.pdfViewVent.CurrentPageHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.pdfViewVent.Document = null;
            this.pdfViewVent.FormHighlightColor = System.Drawing.Color.Transparent;
            this.pdfViewVent.FormsBlendMode = Patagames.Pdf.Enums.BlendTypes.FXDIB_BLEND_MULTIPLY;
            this.pdfViewVent.LoadingIconText = "Loading...";
            this.pdfViewVent.Location = new System.Drawing.Point(441, 115);
            this.pdfViewVent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pdfViewVent.MouseMode = Patagames.Pdf.Net.Controls.WinForms.MouseModes.Default;
            this.pdfViewVent.Name = "pdfViewVent";
            this.pdfViewVent.OptimizedLoadThreshold = 1000;
            this.pdfViewVent.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.pdfViewVent.PageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pdfViewVent.PageAutoDispose = true;
            this.pdfViewVent.PageBackColor = System.Drawing.Color.White;
            this.pdfViewVent.PageBorderColor = System.Drawing.Color.Black;
            this.pdfViewVent.PageMargin = new System.Windows.Forms.Padding(10);
            this.pdfViewVent.PageSeparatorColor = System.Drawing.Color.Gray;
            this.pdfViewVent.RenderFlags = ((Patagames.Pdf.Enums.RenderFlags)((Patagames.Pdf.Enums.RenderFlags.FPDF_LCD_TEXT | Patagames.Pdf.Enums.RenderFlags.FPDF_NO_CATCH)));
            this.pdfViewVent.ShowCurrentPageHighlight = true;
            this.pdfViewVent.ShowLoadingIcon = true;
            this.pdfViewVent.ShowPageSeparator = true;
            this.pdfViewVent.Size = new System.Drawing.Size(618, 381);
            this.pdfViewVent.SizeMode = Patagames.Pdf.Net.Controls.WinForms.SizeModes.FitToWidth;
            this.pdfViewVent.TabIndex = 37;
            this.pdfViewVent.TextSelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.pdfViewVent.TilesCount = 2;
            this.pdfViewVent.UseProgressiveRender = true;
            this.pdfViewVent.ViewMode = Patagames.Pdf.Net.Controls.WinForms.ViewModes.Vertical;
            this.pdfViewVent.Zoom = 1F;
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.lblPreview.Location = new System.Drawing.Point(903, 84);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(156, 28);
            this.lblPreview.TabIndex = 49;
            this.lblPreview.Text = "Vista Previa";
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button2.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button2.Location = new System.Drawing.Point(40, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(262, 45);
            this.button2.TabIndex = 50;
            this.button2.Text = "Ventas más Bajas";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button3.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button3.Location = new System.Drawing.Point(441, 504);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(164, 45);
            this.button3.TabIndex = 51;
            this.button3.Text = "Imprimir";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // BtnGenerar
            // 
            this.BtnGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnGenerar.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnGenerar.Location = new System.Drawing.Point(895, 504);
            this.BtnGenerar.Name = "BtnGenerar";
            this.BtnGenerar.Size = new System.Drawing.Size(164, 45);
            this.BtnGenerar.TabIndex = 52;
            this.BtnGenerar.Text = "Generar";
            this.BtnGenerar.UseVisualStyleBackColor = false;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.lblFecha.Location = new System.Drawing.Point(715, 540);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(72, 28);
            this.lblFecha.TabIndex = 54;
            this.lblFecha.Text = "Fecha";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dateTime1
            // 
            this.dateTime1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.dateTime1.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime1.Location = new System.Drawing.Point(611, 516);
            this.dateTime1.Name = "dateTime1";
            this.dateTime1.Size = new System.Drawing.Size(120, 23);
            this.dateTime1.TabIndex = 53;
            // 
            // dateTime2
            // 
            this.dateTime2.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.dateTime2.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTime2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime2.Location = new System.Drawing.Point(770, 516);
            this.dateTime2.Name = "dateTime2";
            this.dateTime2.Size = new System.Drawing.Size(120, 23);
            this.dateTime2.TabIndex = 55;
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.lblA.Location = new System.Drawing.Point(739, 510);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(24, 28);
            this.lblA.TabIndex = 56;
            this.lblA.Text = "a";
            this.lblA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.panel1.Location = new System.Drawing.Point(369, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 450);
            this.panel1.TabIndex = 63;
            // 
            // BtnMenu
            // 
            this.BtnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnMenu.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnMenu.Location = new System.Drawing.Point(27, 510);
            this.BtnMenu.Name = "BtnMenu";
            this.BtnMenu.Size = new System.Drawing.Size(135, 39);
            this.BtnMenu.TabIndex = 65;
            this.BtnMenu.Text = "Menu";
            this.BtnMenu.UseVisualStyleBackColor = false;
            // 
            // BtnVolver
            // 
            this.BtnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnVolver.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnVolver.Location = new System.Drawing.Point(199, 510);
            this.BtnVolver.Name = "BtnVolver";
            this.BtnVolver.Size = new System.Drawing.Size(135, 39);
            this.BtnVolver.TabIndex = 64;
            this.BtnVolver.Text = "Volver";
            this.BtnVolver.UseVisualStyleBackColor = false;
            this.BtnVolver.Visible = false;
            // 
            // ReportesVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(9)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.BtnMenu);
            this.Controls.Add(this.BtnVolver);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.dateTime2);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dateTime1);
            this.Controls.Add(this.BtnGenerar);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.pdfViewVent);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "ReportesVenta";
            this.Text = "ReportesVenta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private Patagames.Pdf.Net.Controls.WinForms.PdfViewer pdfViewVent;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button BtnGenerar;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dateTime1;
        private System.Windows.Forms.DateTimePicker dateTime2;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnMenu;
        private System.Windows.Forms.Button BtnVolver;
    }
}