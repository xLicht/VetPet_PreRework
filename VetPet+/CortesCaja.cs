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
        int dgv1subirprecio1000 = 0;
        int dgv1totalprecio1000 = 0;

        int dgv1subirprecio500 = 0;
        int dgv1totalprecio500 = 0;

        int dgv1subirprecio200 = 0;
        int dgv1totalprecio200 = 0;

        int dgv1subirprecio100 = 0;
        int dgv1totalprecio100 = 0;

        int dgv1subirprecio50 = 0;
        int dgv1totalprecio50 = 0;

        int dgv1subirprecio20 = 0;
        int dgv1totalprecio20 = 0;

        //dgv3
        int dgv2subirprecio20 = 0;
        int dgv2totalprecio20 = 0;

        int dgv2subirprecio10 = 0;
        int dgv2totalprecio10 = 0;

        int dgv2subirprecio5 = 0;
        int dgv2totalprecio5 = 0;

        int dgv2subirprecio2 = 0;
        int dgv2totalprecio2 = 0;

        int dgv2subirprecio1 = 0;
        int dgv2totalprecio1 = 0;

        int dgv2subirprecio50 = 0;
        int dgv2totalprecio50 = 0;


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
            dataGridView2.Rows.Add("1000 MXN", dgv1subirprecio1000, dgv1totalprecio1000 + "MXN");
            dataGridView2.Rows.Add("500 MXN", dgv1subirprecio500, dgv1totalprecio500 + "MXN");
            dataGridView2.Rows.Add("200 MXN", dgv1subirprecio200, dgv1totalprecio200 + "MXN");
            dataGridView2.Rows.Add("100 MXN", dgv1subirprecio100, dgv1totalprecio100 + "MXN");
            dataGridView2.Rows.Add("50 MXN", dgv1subirprecio50, dgv1totalprecio50 + "MXN");
            dataGridView2.Rows.Add("20 MXN", dgv1subirprecio20, dgv1totalprecio20 + "MXN");
            dataGridView2.Rows.Add("Total", "", "" + "MXN");


            // Agregar filas con datos
            dataGridView3.Rows.Add("20 MXN", dgv2subirprecio20, dgv2totalprecio20 + "MXN");
            dataGridView3.Rows.Add("10 MXN", dgv2subirprecio10, dgv2totalprecio10 + "MXN");
            dataGridView3.Rows.Add("5 MXN", dgv2subirprecio5, dgv2totalprecio5 + "MXN");
            dataGridView3.Rows.Add("2 MXN", dgv2subirprecio2, dgv2totalprecio2 + "MXN");
            dataGridView3.Rows.Add("1 MXN", dgv2subirprecio1, dgv2totalprecio1 + "MXN");
            dataGridView3.Rows.Add(".50 MXN", dgv2subirprecio50, dgv2totalprecio50 + "MXN");
            dataGridView3.Rows.Add("Total", "", "" + "MXN");


            AjustarAlturaFilas();
        }
        private void ActualizarTablas()
        {
            // Verificar que DataGridView2 tenga suficientes filas antes de actualizar
            if (dataGridView2.Rows.Count >= 7)
            {
                dataGridView2.Rows[0].Cells[1].Value = dgv1subirprecio1000;
                dataGridView2.Rows[0].Cells[2].Value = dgv1totalprecio1000 + " MXN";

                dataGridView2.Rows[1].Cells[1].Value = dgv1subirprecio500;
                dataGridView2.Rows[1].Cells[2].Value = dgv1totalprecio500 + " MXN";

                dataGridView2.Rows[2].Cells[1].Value = dgv1subirprecio200;
                dataGridView2.Rows[2].Cells[2].Value = dgv1totalprecio200 + " MXN";

                dataGridView2.Rows[3].Cells[1].Value = dgv1subirprecio100;
                dataGridView2.Rows[3].Cells[2].Value = dgv1totalprecio100 + " MXN";

                dataGridView2.Rows[4].Cells[1].Value = dgv1subirprecio50;
                dataGridView2.Rows[4].Cells[2].Value = dgv1totalprecio50 + " MXN";

                dataGridView2.Rows[5].Cells[1].Value = dgv1subirprecio20;
                dataGridView2.Rows[5].Cells[2].Value = dgv1totalprecio20 + " MXN";

                dataGridView2.Rows[6].Cells[2].Value = "" + " MXN"; // Total
            }

            // Verificar que DataGridView3 tenga suficientes filas antes de actualizar
            if (dataGridView3.Rows.Count >= 7)
            {
                dataGridView3.Rows[0].Cells[1].Value = dgv2subirprecio20;
                dataGridView3.Rows[0].Cells[2].Value = dgv2totalprecio20 + " MXN";

                dataGridView3.Rows[1].Cells[1].Value = dgv2subirprecio10;
                dataGridView3.Rows[1].Cells[2].Value = dgv2totalprecio10 + " MXN";

                dataGridView3.Rows[2].Cells[1].Value = dgv2subirprecio5;
                dataGridView3.Rows[2].Cells[2].Value = dgv2totalprecio5 + " MXN";

                dataGridView3.Rows[3].Cells[1].Value = dgv2subirprecio2;
                dataGridView3.Rows[3].Cells[2].Value = dgv2totalprecio2 + " MXN";

                dataGridView3.Rows[4].Cells[1].Value = dgv2subirprecio1;
                dataGridView3.Rows[4].Cells[2].Value = dgv2totalprecio1 + " MXN";

                dataGridView3.Rows[5].Cells[1].Value = dgv2subirprecio50;
                dataGridView3.Rows[5].Cells[2].Value = dgv2totalprecio50 + " MXN";

                dataGridView3.Rows[6].Cells[2].Value = "" + " MXN"; // Total
            }
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
            dgv1subirprecio1000 -= 1;
            dgv1totalprecio1000 -= 1000;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv2SubirPrecio_Click(object sender, EventArgs e)
        {
            dgv1subirprecio1000 += 1000;
            dgv1totalprecio1000 += 1;
        }

        private void btnAumentar1000_Click(object sender, EventArgs e)
        {
            dgv1subirprecio1000 += 1;
            dgv1totalprecio1000 += 1000;
            ActualizarTablas();
        }

        private void btnDisminuir1000_Click(object sender, EventArgs e)
        {
            dgv1subirprecio1000 -= 1;
            dgv1totalprecio1000 -= 1000;
            ActualizarTablas();
        }

        private void btnAumentar500_Click(object sender, EventArgs e)
        {
            dgv1subirprecio500 += 1;
            dgv1totalprecio500 += 500;
            ActualizarTablas();
        }

        private void btnDisminuir500_Click(object sender, EventArgs e)
        {
            dgv1subirprecio500 -= 1;
            dgv1totalprecio500 -= 500;
            ActualizarTablas();
        }

        private void btnDisminuir200_Click(object sender, EventArgs e)
        {
            dgv1subirprecio200 -= 1;
            dgv1totalprecio200 -= 200;
            ActualizarTablas();
        }

        private void btnAumentar200_Click(object sender, EventArgs e)
        {
            dgv1subirprecio200 += 1;
            dgv1totalprecio200 += 200;
            ActualizarTablas();
        }

        private void btnDisminuir100_Click(object sender, EventArgs e)
        {
            dgv1subirprecio100 -= 1;
            dgv1totalprecio100 -= 100;
            ActualizarTablas();
        }

        private void btnAumentar100_Click(object sender, EventArgs e)
        {
            dgv1subirprecio100 += 1;
            dgv1totalprecio100 += 100;
            ActualizarTablas();
        }

        private void btnDisminuir50_Click(object sender, EventArgs e)
        {

        }

        private void btnAumentar50_Click(object sender, EventArgs e)
        {

        }
    }
}
