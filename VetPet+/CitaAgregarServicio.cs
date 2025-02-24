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
    public partial class CitaAgregarServicio : FormPadre
    {
        public static string formularioAnterior; // Guarda el nombre del formulario anterior

        public CitaAgregarServicio()
        {
            InitializeComponent();
        }
        public CitaAgregarServicio(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CitaAgregarServicio_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgendar(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void button2_Click(object sender, EventArgs e)
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
            else if (formularioAnterior == "CitaListaServicio")
            {
                parentForm.formularioHijo(new CitaListaServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
            }
        }

        private void btnServicio_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaListaServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            CitaEmpleadosDisponibles.formularioAnterior = "CitaAgregarServicio";
            parentForm.formularioHijo(new CitaEmpleadosDisponibles(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }
    }
}
