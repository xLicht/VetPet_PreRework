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
    public partial class VeterinariaConsultarM : Form
    {
        private Form1 parentForm;
        public VeterinariaConsultarM(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaConsultarM_Load(object sender, EventArgs e)
        {

        }

        private void btnRecetar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaRecetar(parentForm));
        }
    }
}
