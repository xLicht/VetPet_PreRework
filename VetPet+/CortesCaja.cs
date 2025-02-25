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
    public partial class CortesCaja : FormPadre
    {
        public CortesCaja()
        {
            InitializeComponent();
        }
        public CortesCaja(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }
        private void CortesCaja_Load(object sender, EventArgs e)
        {
            // Agregar filas con datos
            dataGridView2.Rows.Add("", "", "");
            dataGridView2.Rows.Add("", "", "");
            dataGridView2.Rows.Add("", "", "");
            dataGridView2.Rows.Add("", "", "");
            dataGridView2.Rows.Add("", "", "");
            dataGridView2.Rows.Add("", "", "");
            dataGridView2.Rows.Add("", "", "");
            dataGridView2.Rows.Add("", "", "");
            dataGridView2.Rows.Add("", "", "");

            AjustarAlturaFilas();
        }
        private void AjustarAlturaFilas()
        {
            if (dataGridView2.Rows.Count > 0)
            {
                int alturaDisponible = dataGridView2.Height - dataGridView2.ColumnHeadersHeight;
                int alturaFila = alturaDisponible / dataGridView2.Rows.Count;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    row.Height = alturaFila;
                }
            }
            if (dataGridView3.Rows.Count > 0)
            {
                int alturaDisponible = dataGridView3.Height - dataGridView3.ColumnHeadersHeight;
                int alturaFila = alturaDisponible / dataGridView3.Rows.Count;

                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    row.Height = alturaFila;
                }
            }
        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesMenus(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesMenus(parentForm)); // Pasamos la referencia de Form1 a 
        }
    }
}
