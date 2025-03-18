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
    public partial class VeterianiaGestionarHistorialM : FormPadre
    {
        public int DatoMascota { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();

        public VeterianiaGestionarHistorialM(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  
            
        }

        private void VeterianiaGestionarHistorialM_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Mensaje recibido :" + DatoMascota);
        }

        private void VeterianiaGestionarHistorialM_Resize(object sender, EventArgs e)
        {
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaModificarHistorial(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitasMascota(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaHistorialMedico(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }
    }
    
}
