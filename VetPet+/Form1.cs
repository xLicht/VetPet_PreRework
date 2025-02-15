using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Form formHijo;
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void formularioHijo(Form hijo)
        {
            if (formHijo != null)
            {
                //Al abrir un formulario cerramos el formulario anterior
                formHijo.Close();
            }
            formHijo = hijo;
            hijo.TopLevel = false;
            hijo.FormBorderStyle = FormBorderStyle.None;
            hijo.Dock = DockStyle.Fill;
            pnlForms.Controls.Add(hijo);
            pnlForms.Tag = hijo;
            hijo.BringToFront();
            hijo.Show();
            //lblTitulo.Text = hijo.Text;
        }

        private void BtnAlmacen_Click(object sender, EventArgs e)
        {
            formularioHijo(new AlmacenMenu());
        }

        private void BtnAtencionClient_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}

//MAFUFAFADA