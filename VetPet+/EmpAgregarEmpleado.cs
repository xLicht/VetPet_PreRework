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
    public partial class EmpAgregarEmpleado : FormPadre
    {
        public EmpAgregarEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpAgregarEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Se borrarán todos los datos ingresados. ¿Desea continuar?","Advertencia",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
            }
            else
            {
           
               
            }
        }

        private void btnSelecTipo_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpAgregarTipoEmpleado(parentForm));
        }

        private void r_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Se borrarán todos los datos ingresados. ¿Desea continuar?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
            }
            else
            {
              
                
            }
        }

        private void a_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
        }

        private void txtCP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bloquea cualquier otro carácter
            }
        }

        private void txtApellidoPat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; //solo una plabra
            }
        }

        private void txtApellidoMat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; //solo una plabra
            }
        }

        private void txtNumContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
