namespace VetPet_
{
    partial class ListaServicios
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
            this.BtnRegresar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnRegresar
            // 
            this.BtnRegresar.Location = new System.Drawing.Point(107, 227);
            this.BtnRegresar.Name = "BtnRegresar";
            this.BtnRegresar.Size = new System.Drawing.Size(83, 28);
            this.BtnRegresar.TabIndex = 0;
            this.BtnRegresar.Text = "Regresar";
            this.BtnRegresar.UseVisualStyleBackColor = true;
            this.BtnRegresar.Click += new System.EventHandler(this.BtnRegresar_Click);
            // 
            // ListaServicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnRegresar);
            this.Name = "ListaServicios";
            this.Text = "ListaServicios";
            this.Load += new System.EventHandler(this.ListaServicios_Load);
            this.Resize += new System.EventHandler(this.ListaServicios_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnRegresar;
    }
}