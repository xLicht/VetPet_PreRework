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
    public partial class AgregarTipoRayosX : FormPadre
    {
        public AgregarTipoRayosX()
        {
            InitializeComponent();
        }
        public AgregarTipoRayosX(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaRayosX(parentForm));
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaRayosX(parentForm));
        }
    }
}
