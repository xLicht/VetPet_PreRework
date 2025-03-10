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
    public partial class EmpConsultarTipoEmpleado : FormPadre
    {
        public int DatoEmpleado { get; set; }
        public EmpConsultarTipoEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            cbInventario.AutoCheck = false;
            cbVentas.AutoCheck = false;
            cbReceta.AutoCheck = false;
            cbConsulta.AutoCheck = false;
            cbAdministracion.AutoCheck = false;
            cbHistorialMedico.AutoCheck = false;
        }

        private void EmpConsultarTipoEmpleado_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Dato Recibido:" + DatoEmpleado);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpModificarTipoEmpleado(parentForm));
        }

        private void r_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }

        private void m_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpModificarTipoEmpleado(parentForm));
        }

        private void e_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }
    }
}
