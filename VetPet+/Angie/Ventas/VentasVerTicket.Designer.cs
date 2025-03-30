namespace VetPet_.Angie.Ventas
{
    partial class VentasVerTicket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentasVerTicket));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnVolver = new System.Windows.Forms.Button();
            this.pdfViewTicket = new AxAcroPDFLib.AxAcroPDF();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewTicket)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.panel1.Location = new System.Drawing.Point(184, 479);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 6);
            this.panel1.TabIndex = 76;
            // 
            // BtnVolver
            // 
            this.BtnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnVolver.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnVolver.Location = new System.Drawing.Point(30, 518);
            this.BtnVolver.Name = "BtnVolver";
            this.BtnVolver.Size = new System.Drawing.Size(135, 39);
            this.BtnVolver.TabIndex = 75;
            this.BtnVolver.Tag = "1";
            this.BtnVolver.Text = "Volver";
            this.BtnVolver.UseVisualStyleBackColor = false;
            // 
            // pdfViewTicket
            // 
            this.pdfViewTicket.Enabled = true;
            this.pdfViewTicket.Location = new System.Drawing.Point(245, 92);
            this.pdfViewTicket.Name = "pdfViewTicket";
            this.pdfViewTicket.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfViewTicket.OcxState")));
            this.pdfViewTicket.Size = new System.Drawing.Size(945, 484);
            this.pdfViewTicket.TabIndex = 74;
            this.pdfViewTicket.Tag = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Mono", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.label1.Location = new System.Drawing.Point(443, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 63);
            this.label1.TabIndex = 73;
            this.label1.Text = "Ticket";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VentasVerTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(9)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnVolver);
            this.Controls.Add(this.pdfViewTicket);
            this.Controls.Add(this.label1);
            this.Name = "VentasVerTicket";
            this.Text = "VentasVerTicket";
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewTicket)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnVolver;
        private AxAcroPDFLib.AxAcroPDF pdfViewTicket;
        private System.Windows.Forms.Label label1;
    }
}