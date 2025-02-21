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
    public partial class VeterinariaConsultaMedica : Form
    {
        private Form1 parentForm;
        public VeterinariaConsultaMedica(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaConsultaMedica_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ConsultarCita(parentForm));
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaModificarConsultaM(parentForm));
        }

        private void btnRecetar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
        }
    }
}
