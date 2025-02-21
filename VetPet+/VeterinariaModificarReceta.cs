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

namespace VetPet_
{
    public partial class VeterinariaModificarReceta : Form
    {
        private Form1 parentForm;
        public VeterinariaModificarReceta(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaModificarReceta_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
        }
    }
}
