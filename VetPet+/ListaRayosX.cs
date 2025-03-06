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
    public partial class ListaRayosX : FormPadre
    {
        public ListaRayosX()
        {
            InitializeComponent();
        }
        public ListaRayosX(Form1 parent)
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
            parentForm.formularioHijo(new ModificarRayosX(parentForm));
        }

        private void BtnAgregarTipoDeRayosX_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarTipoRayosX(parentForm));
        }

        private void BtnAgregarRayosX_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarRayosX(parentForm));
        }
    }
}
