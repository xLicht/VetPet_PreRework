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
    public partial class EmpConsultarEmpleado : FormPadre
    {
        public object DatoEmpleado { get; set; }
        public EmpConsultarEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpConsultarEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpModificarEmpleado(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            advertencia();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            advertenciaEliminar();
        }

        private void advertencia()
        { 
                parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
        }
        private void advertenciaEliminar()
        {
            DialogResult resultado = MessageBox.Show("El Empleado se ¿Desea continuar?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
            }
            else
            {

            }
        }
        public void MostrarDato()
        {
            //MessageBox.Show("Dato recibido: " + DatoEmpleado.ToString());
            //label1.Text = "Dato recibido: " + DatoEmpleado.ToString();
        }
        private void r_Click(object sender, EventArgs e)
        {
            advertencia();
        }

        private void e_Click(object sender, EventArgs e)
        {
            advertenciaEliminar();
        }
    }
}
