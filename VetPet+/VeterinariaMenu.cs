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
    public partial class VeterinariaMenu : FormPadre
    {
        public VeterinariaMenu(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent; 
         
        }

        private void VeterinariaMenu_Load(object sender, EventArgs e)
        {

        }

        private void VeterinariaMenu_Resize(object sender, EventArgs e)
        {
            
        }

        private void btnHistorialMedico_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaHistorialMedico(parentForm)); 
        }

        private void btnHistorialMedico_Click_1(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaHistorialMedico(parentForm)); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaHistorialMedico(parentForm)); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitasMedicas(parentForm)); 
        }

        private void btnCitasMedicas_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitasMedicas(parentForm)); 
        }

        private void VeterinariaMenu_Load_1(object sender, EventArgs e)
        {

        }
    }
}
