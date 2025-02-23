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
    public partial class CitaConsultarCita : FormPadre
    {
        public CitaConsultarCita()
        {
            InitializeComponent();
        }
        public CitaConsultarCita(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CitaConsultarCita_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaEnlistado(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Llamar al formulario de opciones
            using (var opcionesForm = new AlmacenAvisoEliminar())
            {
                if (opcionesForm.ShowDialog() == DialogResult.OK)
                {
                    if (opcionesForm.Resultado == "Si")
                    {
                        parentForm.formularioHijo(new CitaAgendar(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
                    }
                    else if (opcionesForm.Resultado == "No")
                    {
                        parentForm.formularioHijo(new CitaConsultarCita(parentForm)); // Pasamos la referencia de Form1 a 
                    }
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaModificarCita(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnVerReceta_Click(object sender, EventArgs e)
        {
            //parentForm.formularioHijo(new CitaReceta(parentForm)); // Pasamos la referencia de Form1 a 
        }
    }
}
