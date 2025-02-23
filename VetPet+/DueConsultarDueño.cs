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
    public partial class DueConsultarDueño : FormPadre
    {
        public DueConsultarDueño(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void DueConsultarDueño_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueAtencionAlCliente(parentForm));
        }

        private void btnMostrarMascota_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueAtencionAlCliente(parentForm));
        }
    }
}
