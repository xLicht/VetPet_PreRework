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
            if (!string.IsNullOrEmpty(CitaListaServicio.ServicioSeleccionado))
            {
                txtServicio.Text = CitaListaServicio.ServicioSeleccionado;
                txtClaseServicio.Text = CitaListaServicio.CategoriaSeleccionada;
            }
            if (!string.IsNullOrEmpty(CitaEmpleadosDisponibles.PersonalSeleccionado))
            {
                txtPersonal.Text = CitaEmpleadosDisponibles.PersonalSeleccionado;
            }
        }


        public static string ServicioSeleccionado { get; set; }
        public static string ClaseServicioSeleccionada { get; set; }
        public static string PersonalSeleccionado { get; set; }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            CitaAgregarServicio.ServicioSeleccionado = txtServicio.Text;
            CitaAgregarServicio.ClaseServicioSeleccionada = txtClaseServicio.Text;
            CitaAgregarServicio.PersonalSeleccionado = txtPersonal.Text;

            parentForm.formularioHijo(new CitaAgendar(parentForm));
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
            CitaListaServicio.VieneDeCitaAgregarServicio = true;
            parentForm.formularioHijo(new CitaListaServicio(parentForm));
        }


        private void btnPersonal_Click(object sender, EventArgs e)
        {
            CitaEmpleadosDisponibles.formularioAnterior = "CitaAgregarServicio";
            parentForm.formularioHijo(new CitaEmpleadosDisponibles(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }
    }
}