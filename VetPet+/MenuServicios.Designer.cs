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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnListaServicios
            // 
            this.BtnListaServicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnListaServicios.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnListaServicios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnListaServicios.Location = new System.Drawing.Point(595, 220);
            this.BtnListaServicios.Name = "BtnListaServicios";
            this.BtnListaServicios.Size = new System.Drawing.Size(240, 155);
            this.BtnListaServicios.TabIndex = 0;
            this.BtnListaServicios.Text = "ListaServicios";
            this.BtnListaServicios.UseVisualStyleBackColor = true;
            this.BtnListaServicios.Click += new System.EventHandler(this.BtnListaServicios_Click);
            // 
            // BtnAgregarServicios
            // 
            this.BtnAgregarServicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAgregarServicios.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregarServicios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnAgregarServicios.Location = new System.Drawing.Point(233, 220);
            this.BtnAgregarServicios.Name = "BtnAgregarServicios";
            this.BtnAgregarServicios.Size = new System.Drawing.Size(240, 155);
            this.BtnAgregarServicios.TabIndex = 1;
            this.BtnAgregarServicios.Text = "AgregarServicios";
            this.BtnAgregarServicios.UseVisualStyleBackColor = true;
            this.BtnAgregarServicios.Click += new System.EventHandler(this.BtnAgregarServicios_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.IndianRed;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 85);
            this.panel1.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(329, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(452, 55);
            this.label2.TabIndex = 20;
            this.label2.Text = "Menu de Servicios";
            // 
            // MenuServicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(149)))), ((int)(((byte)(112)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnAgregarServicios);
            this.Controls.Add(this.BtnListaServicios);
            this.Name = "MenuServicios";
            this.Text = "MenuServicios";
            this.Load += new System.EventHandler(this.MenuServicios_Load);
            this.Resize += new System.EventHandler(this.MenuServicios_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnListaServicios;
        private System.Windows.Forms.Button BtnAgregarServicios;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}