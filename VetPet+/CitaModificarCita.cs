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
    public partial class CitaModificarCita : FormPadre
    {
        public CitaModificarCita()
        {
            InitializeComponent();
        }
        public CitaModificarCita(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CitaModificarCita_Load(object sender, EventArgs e)
        {

        }

        private void btnSi_CheckedChanged(object sender, EventArgs e)
        {
            btnNo.Checked = false;

        }

        private void btnNo_CheckedChanged(object sender, EventArgs e)
        {
            btnSi.Checked = false;

        }

        private void btnMascota_Click(object sender, EventArgs e)
        {
            CitaMascota.formularioAnterior = "CitaModificarCita";
            parentForm.formularioHijo(new CitaMascota(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnDueño_Click(object sender, EventArgs e)
        {
            CitaDueños.formularioAnterior = "CitaModificarCita";
            parentForm.formularioHijo(new CitaDueños(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaConsultarCita(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaEnlistado(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgregarServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se hizo clic en una fila válida (evita encabezados)
            if (e.RowIndex >= 0)
            {
                // Si la fila seleccionada es la primera (índice 0)
                if (e.RowIndex == 0)
                {
                    // Abrir el formulario deseado
                    CitaGestionarServicio.formularioAnterior = "CitaModificarCita";

                    parentForm.formularioHijo(new CitaGestionarServicio(parentForm)); 
                }
            }
        }
    }
}
