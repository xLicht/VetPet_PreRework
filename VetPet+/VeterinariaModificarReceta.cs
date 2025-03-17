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
    public partial class VeterinariaModificarReceta : FormPadre
    {
        public int DatoCita { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public VeterinariaModificarReceta(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaModificarReceta_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Dato Recibido" + DatoCita);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
        }

        private void btnAgregarMedicamentos_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaVentaMedicamentos(parentForm, "VeterinariaModificarReceta"));
        }
    }
}
