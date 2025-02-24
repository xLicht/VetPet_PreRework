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
    public partial class CitaDueños : FormPadre
    {
        public static string formularioAnterior; // Guarda el nombre del formulario anterior

        public CitaDueños()
        {
            InitializeComponent();
        }
        public CitaDueños(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CitaDueños_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Verifica de qué formulario vino
            if (formularioAnterior == "CitaAgendar")
            {
                parentForm.formularioHijo(new CitaAgendar(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
            }
            else if (formularioAnterior == "CitaModificarCita")
            {
                parentForm.formularioHijo(new CitaModificarCita(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
            }
        }
    }
}
