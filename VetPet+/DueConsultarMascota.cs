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
    public partial class DueConsultarMascota : FormPadre
    {
        public DueConsultarMascota(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;

            //Desactivacion de las Modificaciones

            fechaNac.ShowCheckBox = true;
            fechaNac.Checked = false;

            cbMacho.AutoCheck = false;
            cbHembra.AutoCheck = false;
            cbSi.AutoCheck = false;
            cbNo.AutoCheck = false;
            cbSiMuerto.AutoCheck = false;
            cbNoMuerto.AutoCheck = false;
        }

        private void DueConsultarMascota_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueMascotadeDue(parentForm));
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueModificarMascota(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueMascotadeDue(parentForm));
        }

        private void cbEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
