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
    public partial class InicioSesion : FormPadre
    {
        public InicioSesion()
        {
            InitializeComponent();
        }
        public InicioSesion(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {

        }

        private void BtnOlvideMiContraseña_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new OlvideConstraseña(parentForm));
        }
    }
}
