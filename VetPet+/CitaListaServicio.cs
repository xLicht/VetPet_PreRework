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
    public partial class CitaListaServicio : FormPadre
    {
        public CitaListaServicio()
        {
            InitializeComponent();
        }
        public CitaListaServicio(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }
        private void CitaListaServicio_Load(object sender, EventArgs e)
        {
            // Agregar filas con datos
            dataGridView1.Rows.Add("Cirugia", "Médico", "Veterinario");
            dataGridView1.Rows.Add("Vacunación", "Médico", "Veterinario");
            dataGridView1.Rows.Add("Ducha", "Estetico", "Acicalador");
            dataGridView1.Rows.Add("Rayos X", "Médico", "Veterinario");
            dataGridView1.Rows.Add("Radiografias", "Médico", "Veterinario");
            dataGridView1.Rows.Add("Acicalación", "Estetico", "Acicalador");
            dataGridView1.Rows.Add("Pruebas de laboratorio", "Médico", "Veterinario");
            dataGridView1.Rows.Add("Masaje", "Médico", "Acicalador");
            dataGridView1.Rows.Add("Cremación", "Médico", "Acicalador");

            AjustarAlturaFilas();
        }
        private void AjustarAlturaFilas()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int alturaDisponible = dataGridView1.Height - dataGridView1.ColumnHeadersHeight;
                int alturaFila = alturaDisponible / dataGridView1.Rows.Count;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Height = alturaFila;
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgregarServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            AjustarAlturaFilas();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se hizo clic en una fila válida (evita encabezados)
            if (e.RowIndex >= 0)
            {
                // Si la fila seleccionada es la primera (índice 0)
                if (e.RowIndex == 1)
                {
                    //abrir lista de vacunas
                    //parentForm.formularioHijo(new CitaListaVacunas(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
                }
                // Si la fila seleccionada es la primera (índice 0)
                if (e.RowIndex == 3)
                {
                    //abrir lista de rayosx
                    parentForm.formularioHijo(new CitaListaRayosX(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
                }
                // Si la fila seleccionada es la primera (índice 0)
                if (e.RowIndex == 4)
                {
                    //abrir lista de radiografias
                    //parentForm.formularioHijo(new CitaListaRadiografia(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
                }
                if (e.RowIndex == 6)
                {
                    //abrir lista de pruebas de laboratorio
                }
            }
        }
    }
}
