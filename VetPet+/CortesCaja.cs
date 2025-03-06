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
        //Variables
        // Variables para almacenar los valores de los precios
        private double dgv1subirprecio1000 = 0;
        private double dgv1totalprecio1000 = 0;

        private double dgv1subirprecio500 = 0;
        private double dgv1totalprecio500 = 0;

        private double dgv1subirprecio200 = 0;
        private double dgv1totalprecio200 = 0;

        private double dgv1subirprecio100 = 0;
        private double dgv1totalprecio100 = 0;

        private double dgv1subirprecio50 = 0;
        private double dgv1totalprecio50 = 0;

        private double dgv1subirprecio20 = 0;
        private double dgv1totalprecio20 = 0;

        private double dgv2subirprecio20 = 0;
        private double dgv2totalprecio20 = 0;

        private double dgv2subirprecio10 = 0;
        private double dgv2totalprecio10 = 0;

        private double dgv2subirprecio5 = 0;
        private double dgv2totalprecio5 = 0;

        private double dgv2subirprecio2 = 0;
        private double dgv2totalprecio2 = 0;

        private double dgv2subirprecio1 = 0;
        private double dgv2totalprecio1 = 0;

        private double dgv2subirprecio50 = 0;
        private double dgv2totalprecio50 = 0;


        private double dgv2subirpreciocentavos = 0;
        private double dgv2totalpreciocentavos = 0;


        private double dgv1cantidad = 0;
        private double dgv1total = 0;


        private double dgv2cantidad = 0;
        private double dgv2total = 0;

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
            dataGridView2.Rows.Add("Total", dgv1cantidad, dgv1total + "MXN");

            // Agregar filas con datos
            dataGridView3.Rows.Add("20 MXN", dgv2subirprecio20, dgv2totalprecio20 + "MXN");
            dataGridView3.Rows.Add("10 MXN", dgv2subirprecio10, dgv2totalprecio10 + "MXN");
            dataGridView3.Rows.Add("5 MXN", dgv2subirprecio5, dgv2totalprecio5 + "MXN");
            dataGridView3.Rows.Add("2 MXN", dgv2subirprecio2, dgv2totalprecio2 + "MXN");
            dataGridView3.Rows.Add("1 MXN", dgv2subirprecio1, dgv2totalprecio1 + "MXN");
            dataGridView3.Rows.Add(".50 MXN", dgv2subirprecio50, dgv2totalprecio50 + "MXN");
            dataGridView3.Rows.Add("Total", dgv2cantidad, dgv2total + "MXN");
        }
        private void ActualizarTablas()
        {
            dgv1total = dgv1totalprecio1000 + dgv1totalprecio500 + dgv1totalprecio200 + dgv1totalprecio100 + dgv1totalprecio50 + dgv1totalprecio20;

            
            // Verificar que DataGridView2 tenga suficientes filas antes de actualizar
            if (dataGridView2.Rows.Count >= 0)
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

                dataGridView2.Rows[6].Cells[1].Value = dgv1cantidad; // Total
                dataGridView2.Rows[6].Cells[2].Value = dgv1total + " MXN"; // Total
            }
            dgv2total = dgv2totalprecio20 + dgv2totalprecio10 + dgv2totalprecio5 + dgv2totalprecio2 + dgv2totalprecio1 + dgv2totalpreciocentavos;
            // Verificar que DataGridView3 tenga suficientes filas antes de actualizar

            txtTotalDinero.Text = (dgv1total + dgv2total).ToString() + "MXN";
            if (dataGridView3.Rows.Count >= 0)
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

                dataGridView3.Rows[5].Cells[1].Value = dgv2subirpreciocentavos;
                dataGridView3.Rows[5].Cells[2].Value = dgv2totalpreciocentavos + " MXN";

                dataGridView3.Rows[6].Cells[1].Value = dgv2cantidad; // Total
                dataGridView3.Rows[6].Cells[2].Value = dgv2total + " MXN"; // Total
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

        private void btnsubir1000_Click(object sender, EventArgs e)
        {
            dgv1subirprecio1000 += 1;
            dgv1totalprecio1000 += 1000;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar1000_Click(object sender, EventArgs e)
        {
            dgv1subirprecio1000 -= 1;
            dgv1totalprecio1000 -= 1000;

            dgv1cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir500_Click(object sender, EventArgs e)
        {
            dgv1subirprecio500 += 1;
            dgv1totalprecio500 += 500;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar500_Click(object sender, EventArgs e)
        {
            dgv1subirprecio500 -= 1;
            dgv1totalprecio500 -= 500;

            dgv1cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir200_Click(object sender, EventArgs e)
        {
            dgv1subirprecio200 += 1;
            dgv1totalprecio200 += 200;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar200_Click(object sender, EventArgs e)
        {
            dgv1subirprecio200 -= 1;
            dgv1totalprecio200 -= 200;

            dgv1cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir100_Click(object sender, EventArgs e)
        {
            dgv1subirprecio100 += 1;
            dgv1totalprecio100 += 100;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar100_Click(object sender, EventArgs e)
        {
            dgv1subirprecio100 -= 1;
            dgv1totalprecio100 -= 100;

            dgv1cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir50_Click(object sender, EventArgs e)
        {
            dgv1subirprecio50 += 1;
            dgv1totalprecio50 += 50;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar50_Click(object sender, EventArgs e)
        {
            dgv1subirprecio50 -= 1;
            dgv1totalprecio50 -= 50;

            dgv1cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir20_Click(object sender, EventArgs e)
        {
            dgv1subirprecio20 += 1;
            dgv1totalprecio20 += 20;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar20_Click(object sender, EventArgs e)
        {
            dgv1subirprecio20 -= 1;
            dgv1totalprecio20 -= 20;

            dgv1cantidad -= 1;
            ActualizarTablas();
        }

        private void btn2subir20_Click(object sender, EventArgs e)
        {
            dgv2subirprecio20 += 1;
            dgv2totalprecio20 += 20;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btn2bajar20_Click(object sender, EventArgs e)
        {
            dgv2subirprecio20 -= 1;
            dgv2totalprecio20 -= 20;

            dgv2cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir10_Click(object sender, EventArgs e)
        {
            dgv2subirprecio10 += 1;
            dgv2totalprecio10 += 10;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar10_Click(object sender, EventArgs e)
        {
            dgv2subirprecio10 -= 1;
            dgv2totalprecio10 -= 10;

            dgv2cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir5_Click(object sender, EventArgs e)
        {
            dgv2subirprecio5 += 1;
            dgv2totalprecio5 += 5;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar5_Click(object sender, EventArgs e)
        {
            dgv2subirprecio5 -= 1;
            dgv2totalprecio5 -= 5;

            dgv2cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir2_Click(object sender, EventArgs e)
        {
            dgv2subirprecio2 += 1;
            dgv2totalprecio2 += 2;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar2_Click(object sender, EventArgs e)
        {
            dgv2subirprecio2 -= 1;
            dgv2totalprecio2 -= 2;

            dgv2cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubir1_Click(object sender, EventArgs e)
        {
            dgv2subirprecio1 += 1;
            dgv2totalprecio1 += 1;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar1_Click(object sender, EventArgs e)
        {
            dgv2subirprecio1 -= 1;
            dgv2totalprecio1 -= 1;

            dgv2cantidad -= 1;
            ActualizarTablas();
        }

        private void btnsubircentavos_Click(object sender, EventArgs e)
        {
            dgv2subirpreciocentavos += 1;
            dgv2totalpreciocentavos += .50;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajarcentavos_Click(object sender, EventArgs e)
        {
            dgv2subirpreciocentavos -= 1;
            dgv2totalpreciocentavos -= .50;

            dgv2cantidad -= 1;
            ActualizarTablas();
        }
    }
}
