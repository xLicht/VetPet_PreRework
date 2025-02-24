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
    public partial class CitaGestionarServicio : FormPadre
    {
        public static string formularioAnterior; // Guarda el nombre del formulario anterior

        public CitaGestionarServicio()
        {
            InitializeComponent();
        }
        public CitaGestionarServicio(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }
        private void CitaGestionarServicio_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Verifica de qué formulario vino
            if (formularioAnterior == "CitaModificarCita")
            {
                parentForm.formularioHijo(new CitaModificarCita(parentForm));

            }
            else if (formularioAnterior == "CitaAgregarCirugia")
            {
                parentForm.formularioHijo(new CitaAgregarCirugia(parentForm));
            }

            this.Close(); // Cierra el formulario actual
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
                        parentForm.formularioHijo(new CitaModificarCita(parentForm)); // Pasamos la referencia de Form1 a 
                    }
                    else if (opcionesForm.Resultado == "No")
                    {
                        parentForm.formularioHijo(new CitaGestionarServicio(parentForm)); // Pasamos la referencia de Form1 a 
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (formularioAnterior == "CitaModificarCita")
            {
                parentForm.formularioHijo(new CitaModificarCita(parentForm)); // Pasamos la referencia de Form1 a 
            }
            else if (formularioAnterior == "")
            {
            }
        }

        private void btnVerServicio_Click(object sender, EventArgs e)
        {
            CitaConsultarServicio.formularioAnterior = "CitaGestionarServicio";
            parentForm.formularioHijo(new CitaConsultarServicio(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            CitaEmpleadosDisponibles.formularioAnterior = "CitaGestionarServicio";
            parentForm.formularioHijo(new CitaEmpleadosDisponibles(parentForm)); // Pasamos la referencia de Form1 a 
        }
    }
}
