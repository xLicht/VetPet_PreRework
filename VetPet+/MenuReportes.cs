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

namespace VetPet_
{
    public partial class MenuReportes : FormPadre
    {
        public MenuReportes()
        {
            InitializeComponent();
        }
        public MenuReportes(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void BtnAlmacen_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ReportesAlmacen(parentForm));
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ReportesClientes(parentForm));
        }

        private void BtnServicios_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ReportesServicios(parentForm));
        }

        private void BtnCitas_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ReportesCitas(parentForm));
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ReportesVenta(parentForm));
        }
    }
}
 // Me chingue la master