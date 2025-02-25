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
        //dgv2
        int subirprecio1000 = 0;

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
            dataGridView2.Rows.Add("1000 MXN", subirprecio1000 + "MXN", "");
            dataGridView2.Rows.Add("500 MXN", "", "");
            dataGridView2.Rows.Add("200 MXN", "", "");
            dataGridView2.Rows.Add("100 MXN", "", "");
            dataGridView2.Rows.Add("50 MXN", "", "");
            dataGridView2.Rows.Add("20 MXN", "", "");
            dataGridView2.Rows.Add("Total", "", "");


            // Agregar filas con datos
            dataGridView3.Rows.Add("20 MXN", "", "");
            dataGridView3.Rows.Add("10 MXN", "", "");
            dataGridView3.Rows.Add("5 MXN", "", "");
            dataGridView3.Rows.Add("2 MXN", "", "");
            dataGridView3.Rows.Add("1 MXN", "", "");
            dataGridView3.Rows.Add(".50 MXN", "", "");
            dataGridView3.Rows.Add("Total", "", "");


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

        private void dgv2BajarPrecio_Click(object sender, EventArgs e)
        {
            subirprecio1000 += 1000;
        }
    }
}
