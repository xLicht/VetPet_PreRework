namespace VetPet_
{
    partial class MenuServicios
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
            this.BtnListaServicios = new System.Windows.Forms.Button();
            this.BtnAgregarServicios = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnListaServicios
            // 
            this.BtnListaServicios.Location = new System.Drawing.Point(139, 95);
            this.BtnListaServicios.Name = "BtnListaServicios";
            this.BtnListaServicios.Size = new System.Drawing.Size(102, 36);
            this.BtnListaServicios.TabIndex = 0;
            this.BtnListaServicios.Text = "ListaServicios";
            this.BtnListaServicios.UseVisualStyleBackColor = true;
            this.BtnListaServicios.Click += new System.EventHandler(this.BtnListaServicios_Click);
            // 
            // BtnAgregarServicios
            // 
            this.BtnAgregarServicios.Location = new System.Drawing.Point(399, 110);
            this.BtnAgregarServicios.Name = "BtnAgregarServicios";
            this.BtnAgregarServicios.Size = new System.Drawing.Size(140, 44);
            this.BtnAgregarServicios.TabIndex = 1;
            this.BtnAgregarServicios.Text = "AgregarServicios";
            this.BtnAgregarServicios.UseVisualStyleBackColor = true;
            this.BtnAgregarServicios.Click += new System.EventHandler(this.BtnAgregarServicios_Click);
            // 
            // MenuServicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnAgregarServicios);
            this.Controls.Add(this.BtnListaServicios);
            this.Name = "MenuServicios";
            this.Text = "MenuServicios";
            this.Load += new System.EventHandler(this.MenuServicios_Load);
            this.Resize += new System.EventHandler(this.MenuServicios_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnListaServicios;
        private System.Windows.Forms.Button BtnAgregarServicios;
    }
}