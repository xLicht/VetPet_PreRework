namespace VetPet_
{
    partial class VeterinariaMenu
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
            this.btnCitasMedicas = new System.Windows.Forms.Button();
            this.btnHistorialMedico = new System.Windows.Forms.Button();
            this.lblvet = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCitasMedicas
            // 
            this.btnCitasMedicas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.btnCitasMedicas.Font = new System.Drawing.Font("Segoe UI", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCitasMedicas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.btnCitasMedicas.Location = new System.Drawing.Point(137, 131);
            this.btnCitasMedicas.Name = "btnCitasMedicas";
            this.btnCitasMedicas.Size = new System.Drawing.Size(387, 256);
            this.btnCitasMedicas.TabIndex = 0;
            this.btnCitasMedicas.Text = "Citas Medicas";
            this.btnCitasMedicas.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCitasMedicas.UseVisualStyleBackColor = false;
            this.btnCitasMedicas.Click += new System.EventHandler(this.btnCitasMedicas_Click);
            // 
            // btnHistorialMedico
            // 
            this.btnHistorialMedico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.btnHistorialMedico.Font = new System.Drawing.Font("Segoe UI", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialMedico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.btnHistorialMedico.Location = new System.Drawing.Point(588, 131);
            this.btnHistorialMedico.Name = "btnHistorialMedico";
            this.btnHistorialMedico.Size = new System.Drawing.Size(387, 256);
            this.btnHistorialMedico.TabIndex = 1;
            this.btnHistorialMedico.Text = "Historial Medico";
            this.btnHistorialMedico.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHistorialMedico.UseVisualStyleBackColor = false;
            this.btnHistorialMedico.Click += new System.EventHandler(this.btnHistorialMedico_Click_1);
            // 
            // lblvet
            // 
            this.lblvet.AutoSize = true;
            this.lblvet.Font = new System.Drawing.Font("Segoe UI Black", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.lblvet.Location = new System.Drawing.Point(366, 28);
            this.lblvet.Name = "lblvet";
            this.lblvet.Size = new System.Drawing.Size(392, 86);
            this.lblvet.TabIndex = 4;
            this.lblvet.Text = "Veterinaria";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.pictureBox1.BackgroundImage = global::VetPet_.Properties.Resources.cita2;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(251, 196);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 167);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.pictureBox2.BackgroundImage = global::VetPet_.Properties.Resources.historia;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(715, 196);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(153, 167);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // VeterinariaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(219)))), ((int)(((byte)(199)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.lblvet);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnHistorialMedico);
            this.Controls.Add(this.btnCitasMedicas);
            this.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Name = "VeterinariaMenu";
            this.Text = "VeterinariaMenu";
            this.Load += new System.EventHandler(this.VeterinariaMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCitasMedicas;
        private System.Windows.Forms.Button btnHistorialMedico;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblvet;
    }
}