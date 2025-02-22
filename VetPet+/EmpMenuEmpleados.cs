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
    public partial class EmpMenuEmpleados : FormPadre
    {
        public EmpMenuEmpleados(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpMenuEmpleados_Load(object sender, EventArgs e)
        {

        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
        }

        private void btnTipoEmpleado_Click(object sender, EventArgs e)
        {

        }
    }
}
