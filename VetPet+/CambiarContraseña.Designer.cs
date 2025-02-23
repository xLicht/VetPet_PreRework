namespace VetPet_
{
    partial class CambiarContraseña
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnIngresar = new System.Windows.Forms.Button();
            this.TxtConfirmarContraseña = new System.Windows.Forms.TextBox();
            this.TxtCambiarContraseña = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Controls.Add(this.BtnIngresar);
            this.panel1.Controls.Add(this.TxtConfirmarContraseña);
            this.panel1.Controls.Add(this.TxtCambiarContraseña);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(316, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 354);
            this.panel1.TabIndex = 2;
            // 
            // BtnIngresar
            // 
            this.BtnIngresar.BackColor = System.Drawing.Color.LightCoral;
            this.BtnIngresar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnIngresar.Location = new System.Drawing.Point(130, 287);
            this.BtnIngresar.Name = "BtnIngresar";
            this.BtnIngresar.Size = new System.Drawing.Size(198, 46);
            this.BtnIngresar.TabIndex = 3;
            this.BtnIngresar.Text = "Ingresar";
            this.BtnIngresar.UseVisualStyleBackColor = false;
            this.BtnIngresar.Click += new System.EventHandler(this.BtnIngresar_Click);
            // 
            // TxtConfirmarContraseña
            // 
            this.TxtConfirmarContraseña.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtConfirmarContraseña.Location = new System.Drawing.Point(73, 178);
            this.TxtConfirmarContraseña.Name = "TxtConfirmarContraseña";
            this.TxtConfirmarContraseña.Size = new System.Drawing.Size(297, 29);
            this.TxtConfirmarContraseña.TabIndex = 2;
            // 
            // TxtCambiarContraseña
            // 
            this.TxtCambiarContraseña.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCambiarContraseña.Location = new System.Drawing.Point(70, 75);
            this.TxtCambiarContraseña.Name = "TxtCambiarContraseña";
            this.TxtCambiarContraseña.Size = new System.Drawing.Size(301, 29);
            this.TxtCambiarContraseña.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(68, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Confirmar Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cambiar Contraseña";
            // 
            // CambiarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Coral;
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.panel1);
            this.Name = "CambiarContraseña";
            this.Text = "CambiarContraseña";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnIngresar;
        private System.Windows.Forms.TextBox TxtConfirmarContraseña;
        private System.Windows.Forms.TextBox TxtCambiarContraseña;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}