using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using VetPet_;

namespace VetPet_
{
    public partial class VeterinariaModificarHistorial : FormPadre
    {

        public VeterinariaModificarHistorial(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        } 

        private void VeterinariaModificarHistorial_Resize(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterianiaGestionarHistorialM(parentForm)); 
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterianiaGestionarHistorialM(parentForm)); 
        }

        private void VeterinariaModificarHistorial_Load(object sender, EventArgs e)
        {

        }
    }
}
