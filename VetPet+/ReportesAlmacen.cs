using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace VetPet_
{
    public partial class ReportesAlmacen : FormPadre
    {
        public ReportesAlmacen()
        {
            InitializeComponent();
        }
        public ReportesAlmacen(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnProdMasVend_Click(object sender, EventArgs e)
        {

        }

        private void BtnDescargar_Click(object sender, EventArgs e)
        {
            string rutaPDF = Path.Combine(Application.StartupPath, "PDF_ReportesAlmacen", "A1. Escaner DML.pdf");

            if (File.Exists(rutaPDF))
            {
                pdfViewAlma.LoadDocument(rutaPDF); // Cargar el PDF en el visor
            }
            else
            {
                MessageBox.Show("El archivo PDF no se encontró en la ruta especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
