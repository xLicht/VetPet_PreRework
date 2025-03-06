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
    public partial class ListaPLab : FormPadre
    {
        public ListaPLab()
        {
            InitializeComponent();
        }
        public ListaPLab(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ModificarServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos

        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ModificarPLab(parentForm));
        }

        private void BtnAgregarTipoDeCirugia_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarTipoPLab(parentForm));
        }

        private void BtnAgregarCirugía_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarPLab(parentForm));
        }

        private void BtnAgregarPLab_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarPLab(parentForm));
        }
    }
}
