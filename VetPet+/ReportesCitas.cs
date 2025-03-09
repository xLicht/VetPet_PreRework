using Pruebas_PDF;
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
    public partial class ReportesCitas : FormPadre
    {
        public ReportesCitas()
        {
            InitializeComponent();
        }
        public ReportesCitas(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            string idReporte = "R1";
            ReportesCitasManager reporte = new ReportesCitasManager(idReporte);
            reporte.GenerarReporte();

            try
            {
                string DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string carpetaReportes = Path.Combine(DirectorioProyecto, "Reportes-Arch");

                string rutaPDF = Path.Combine(carpetaReportes, $"ReporteCitas_{idReporte}.pdf");

                if (File.Exists(rutaPDF))
                {
                    pdfViewCita.LoadDocument(rutaPDF); // Cargar el PDF en el visor
                }
                else
                {
                    MessageBox.Show("El archivo PDF no se encontró en la ruta especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error: " + er.Message);
            }
        }
    }
}
