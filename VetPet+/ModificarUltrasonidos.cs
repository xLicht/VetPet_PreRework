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
    public partial class ModificarUltrasonidos : FormPadre
    {
        public ModificarUltrasonidos()
        {
            InitializeComponent();
        }
        public ModificarUltrasonidos(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaVacunas(parentForm));
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaVacunas(parentForm));
        }
    }
}
