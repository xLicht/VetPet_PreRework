namespace VetPet_
{
    partial class VeterinariaGenerarReceta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VeterinariaGenerarReceta));
            this.label1 = new System.Windows.Forms.Label();
            this.pdfViewReceta = new AxAcroPDFLib.AxAcroPDF();
            this.BtnVolver = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewReceta)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Mono", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.label1.Location = new System.Drawing.Point(591, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 79);
            this.label1.TabIndex = 26;
            this.label1.Text = "Receta";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pdfViewReceta
            // 
            this.pdfViewReceta.Enabled = true;
            this.pdfViewReceta.Location = new System.Drawing.Point(245, 92);
            this.pdfViewReceta.Margin = new System.Windows.Forms.Padding(4);
            this.pdfViewReceta.Name = "pdfViewReceta";
            this.pdfViewReceta.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfViewReceta.OcxState")));
            this.pdfViewReceta.Size = new System.Drawing.Size(945, 484);
            this.pdfViewReceta.TabIndex = 70;
            this.pdfViewReceta.Tag = "1";
            this.pdfViewReceta.Visible = false;
            // 
            // BtnVolver
            // 
            this.BtnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(91)))), ((int)(((byte)(131)))));
            this.BtnVolver.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(171)))), ((int)(((byte)(196)))));
            this.BtnVolver.Location = new System.Drawing.Point(40, 638);
            this.BtnVolver.Margin = new System.Windows.Forms.Padding(4);
            this.BtnVolver.Name = "BtnVolver";
            this.BtnVolver.Size = new System.Drawing.Size(180, 48);
            this.BtnVolver.TabIndex = 71;
            this.BtnVolver.Tag = "1";
            this.BtnVolver.Text = "Volver";
            this.BtnVolver.UseVisualStyleBackColor = false;
            this.BtnVolver.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(200)))), ((int)(((byte)(214)))));
            this.panel1.Location = new System.Drawing.Point(245, 589);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 7);
            this.panel1.TabIndex = 72;
            // 
            // VeterinariaGenerarReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(9)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1443, 710);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnVolver);
            this.Controls.Add(this.pdfViewReceta);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "VeterinariaGenerarReceta";
            this.Text = "VeterinariaGenerarReceta";
            this.Load += new System.EventHandler(this.VeterinariaGenerarReceta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewReceta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private AxAcroPDFLib.AxAcroPDF pdfViewReceta;
        private System.Windows.Forms.Button BtnVolver;
        private System.Windows.Forms.Panel panel1;
    }
}