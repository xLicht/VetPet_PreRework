namespace VetPet_
{
    partial class ReportesClientes
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pdfViewClient = new Patagames.Pdf.Net.Controls.WinForms.PdfViewer();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Mono", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(721, 85);
            this.label1.TabIndex = 25;
            this.label1.Text = "Reportes: Clientes";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button1.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button1.Location = new System.Drawing.Point(40, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(262, 45);
            this.button1.TabIndex = 26;
            this.button1.Text = "Dueños Frecuentes";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button2.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button2.Location = new System.Drawing.Point(40, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(262, 45);
            this.button2.TabIndex = 27;
            this.button2.Text = "Mascotas Frecuentes";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button3.Font = new System.Drawing.Font("Cascadia Mono", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button3.Location = new System.Drawing.Point(40, 243);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(262, 45);
            this.button3.TabIndex = 28;
            this.button3.Text = "Dueños menos Frecuentes";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button4.Font = new System.Drawing.Font("Cascadia Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button4.Location = new System.Drawing.Point(40, 307);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(262, 45);
            this.button4.TabIndex = 29;
            this.button4.Text = "Mascotas menos Frecuentes";
            this.button4.UseVisualStyleBackColor = false;
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
            this.pdfViewClient.Location = new System.Drawing.Point(441, 115);
            this.pdfViewClient.MouseMode = Patagames.Pdf.Net.Controls.WinForms.MouseModes.Default;
            this.pdfViewClient.Name = "pdfViewClient";
            this.pdfViewClient.OptimizedLoadThreshold = 1000;
            this.pdfViewClient.Padding = new System.Windows.Forms.Padding(10);
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
            this.pdfViewClient.Size = new System.Drawing.Size(618, 381);
            this.pdfViewClient.SizeMode = Patagames.Pdf.Net.Controls.WinForms.SizeModes.FitToWidth;
            this.pdfViewClient.TabIndex = 37;
            this.pdfViewClient.TextSelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.pdfViewClient.TilesCount = 2;
            this.pdfViewClient.UseProgressiveRender = true;
            this.pdfViewClient.ViewMode = Patagames.Pdf.Net.Controls.WinForms.ViewModes.Vertical;
            this.pdfViewClient.Zoom = 1F;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button5.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button5.Location = new System.Drawing.Point(441, 504);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(164, 45);
            this.button5.TabIndex = 38;
            this.button5.Text = "Imprimir";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.button6.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.button6.Location = new System.Drawing.Point(895, 504);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(164, 45);
            this.button6.TabIndex = 39;
            this.button6.Text = "Descargar";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.label2.Location = new System.Drawing.Point(903, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 28);
            this.label2.TabIndex = 48;
            this.label2.Text = "Vista Previa";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ReportesClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(9)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.pdfViewClient);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "ReportesClientes";
            this.Text = "ReportesClientes";
            this.Load += new System.EventHandler(this.ReportesClientes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private Patagames.Pdf.Net.Controls.WinForms.PdfViewer pdfViewClient;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
    }
}