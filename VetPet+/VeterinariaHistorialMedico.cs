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
    public partial class VeterinariaHistorialMedico : FormPadre
    {
        public VeterinariaHistorialMedico(Form1 parent)
        {
            InitializeComponent();
            cbFliltrar.Text = "Filtrar";
            parentForm = parent; 
        }

        private void VeterinariaHistorialMedico_Load(object sender, EventArgs e)
        {

        }

        private void VeterinariaHistorialMedico_Resize(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new VeterianiaGestionarHistorialM(parentForm)); 

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaMenu(parentForm)); 
        }
    }
}
