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
    public partial class DueModificarMascota : FormPadre
    {
        public DueModificarMascota(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void DueModificarMascota_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueConsultarMascota(parentForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueConsultarMascota(parentForm));
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueConsultarMascota(parentForm));
        }
    }
}
