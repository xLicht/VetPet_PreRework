using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_;

namespace VetPet_
{//
    public partial class CortesConsultar : FormPadre
    {
        public CortesConsultar()
        {
            InitializeComponent();
        }
        public CortesConsultar(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CortesConsultar_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {


        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesHistorial(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesHistorial(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
