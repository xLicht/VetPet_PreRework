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
    public partial class CitaAgregarRadiografia : FormPadre
    {
        public CitaAgregarRadiografia()
        {
            InitializeComponent();
        }
        public CitaAgregarRadiografia(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgregarServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaListaRadiografia(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto

        }
    }
}
