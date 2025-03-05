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
    public partial class ListaUltrasonidos : FormPadre
    {
        public ListaUltrasonidos()
        {
            InitializeComponent();
        }
        public ListaUltrasonidos(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BtnAgregarTipoDeUltrasonidos_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarTipoUltrasonidos(parentForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ModificarUltrasonidos(parentForm));
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ModificarUltrasonidos(parentForm));
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ModificarServicios(parentForm));
        }

        private void BtnAgregarCirugía_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarUltrasonidos(parentForm));
        }

        private void BtnAgregarUltrasonidos_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarUltrasonidos(parentForm));
        }
    }
}
