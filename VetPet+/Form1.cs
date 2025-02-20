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
        public void formularioHijo(Form hijo)
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
        }

        private void BtnAlmacen_Click(object sender, EventArgs e)
        {
            formularioHijo(new AlmacenMenu(this));  // Aquí se pasa la referencia de Form1
        }

        private void BtnAtencionClient_Click(object sender, EventArgs e)
        {
            formularioHijo(new MenuAtencionaCliente(this)); // Pasamos la referencia de Form1
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
        // Botón btnProductos
        private void BtnProductos_Click(object sender, EventArgs e)
        {
            formularioHijo(new AlmacenInventarioProductos(this)); // Pasamos la referencia de Form1
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnVeterinaria_Click(object sender, EventArgs e)
        {
            formularioHijo(new VeterinariaMenu(this));
        }

        private void BtnServicios_Click(object sender, EventArgs e)
        {
            formularioHijo(new MenuServicios(this));
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            formularioHijo(new MenuReportes(this));
        }

        private void BtnCortes_Click(object sender, EventArgs e)
        {
            formularioHijo(new CortesMenus(this));
        }
    }
}
//SEXOO
//PENE POLLA

// Me voy a chingar la master causas


//MAFUFAFADA