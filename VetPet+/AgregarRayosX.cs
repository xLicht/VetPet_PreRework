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
    public partial class AgregarRayosX : FormPadre
    {
        public AgregarRayosX()
        {
            InitializeComponent();
        }
        public AgregarRayosX(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaRayosX(parentForm));
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaRayosX(parentForm));
        }
    }
}
