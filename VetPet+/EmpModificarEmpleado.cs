using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_;

namespace VetPet_
{
    public partial class EmpModificarEmpleado : FormPadre
    {
        public EmpModificarEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpModificarEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            advertencia();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpConsultarEmpleado(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpConsultarEmpleado(parentForm));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            advertencia();
        }

        private void advertencia()
        {
            DialogResult resultado = MessageBox.Show("Se borrarán todos los datos ingresados. ¿Desea continuar?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                parentForm.formularioHijo(new EmpConsultarEmpleado(parentForm));
            }
            else
            {


            }
        }
    }
}
