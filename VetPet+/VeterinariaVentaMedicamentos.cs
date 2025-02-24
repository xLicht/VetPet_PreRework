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
    public partial class VeterinariaVentaMedicamentos : FormPadre
    {
        private string origen;
        public VeterinariaVentaMedicamentos(Form1 parent, string origen)
        {
            InitializeComponent();
            parentForm = parent;
            this.origen = origen;
        }

        private void VeterinariaVentaMedicamentos_Load(object sender, EventArgs e)
        {
                
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (origen == "VeterinariaModificarReceta")
            {
                parentForm.formularioHijo(new VeterinariaModificarReceta(parentForm));
            }
            else if (origen == "VeterinariaRecetar")
            {
                parentForm.formularioHijo(new VeterinariaRecetar(parentForm));
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (origen == "VeterinariaModificarReceta")
            {
                parentForm.formularioHijo(new VeterinariaModificarReceta(parentForm));
            }
            else if (origen == "VeterinariaRecetar")
            {
                parentForm.formularioHijo(new VeterinariaRecetar(parentForm));
            }
        }
    }
}
