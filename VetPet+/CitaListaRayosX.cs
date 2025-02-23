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
{
    public partial class CitaListaRayosX : FormPadre
    {
        public CitaListaRayosX()
        {
            InitializeComponent();
        }
        public CitaListaRayosX(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CitaListaRayosX_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaListaServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgregarRayosX(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }
    }
}
