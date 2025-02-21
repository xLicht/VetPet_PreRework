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
    public partial class CitaEmpleadosDisponibles : FormPadre
    {
        public CitaEmpleadosDisponibles()
        {
            InitializeComponent();
        }
        public CitaEmpleadosDisponibles(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CitaEmpleadosDisponibles_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgregarServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }
    }
}
