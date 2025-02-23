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
    public partial class CitasMascota : FormPadre
    {
        public CitasMascota(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent; 
        }

        private void CitasMascota_Load(object sender, EventArgs e)
        {

        }

        private void CitasMascota_Resize(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterianiaGestionarHistorialM(parentForm));
        }

        private void CitasMascota_Load_1(object sender, EventArgs e)
        {

        }
    }
}
