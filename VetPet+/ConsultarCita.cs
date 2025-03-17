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
    public partial class ConsultarCita : FormPadre
    {
        public int DatoCita { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public ConsultarCita(Form1 parent)
        {
            InitializeComponent(); 
            parentForm = parent;  
        }

        private void ConsultarCita_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Dato Recibido :"+ DatoCita);
        }

        private void ConsultarCita_Resize(object sender, EventArgs e)
        {

        }

        private void btnVerConsulta_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultaMedica(parentForm)); 
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultarM(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitasMedicas(parentForm));
        }

        private void ConsultarCita_Load_1(object sender, EventArgs e)
        {
            MessageBox.Show("Dato Recibido :" + DatoCita);
        }
    }
}
