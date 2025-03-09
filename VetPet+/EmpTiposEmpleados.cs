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
    public partial class EmpTiposEmpleados : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        public EmpTiposEmpleados(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
          
        }

        private void EmpTiposEmpleados_Load(object sender, EventArgs e)
        {

        }



        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpMenuEmpleados(parentForm));
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpAgregarTipoEmpleado(parentForm));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new EmpConsultarTipoEmpleado(parentForm));
        }

        private void r_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpMenuEmpleados(parentForm));
        }

        private void a_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpAgregarTipoEmpleado(parentForm));
        }
    }
}
