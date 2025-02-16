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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblvet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.button1.Location = new System.Drawing.Point(137, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(387, 256);
            this.button1.TabIndex = 0;
            this.button1.Text = "Citas Medicas";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(216)))), ((int)(((byte)(177)))));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(125)))));
            this.button2.Location = new System.Drawing.Point(588, 131);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(387, 256);
            this.button2.TabIndex = 1;
            this.button2.Text = "Historial Medico";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = false;
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
            // VeterinariaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(219)))), ((int)(((byte)(199)))));
            this.ClientSize = new System.Drawing.Size(1082, 577);
            this.Controls.Add(this.lblvet);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblvet;
    }
}