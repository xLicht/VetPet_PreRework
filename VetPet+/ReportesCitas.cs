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
        string modulo = "Citas";
        string tipoReporte;
        string fecha1;
        string fecha2;
        public ReportesCitas()
        {
            InitializeComponent();
        }
        public ReportesCitas(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            dateTime1.Value = DateTime.Today.AddDays(-7);
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            fecha1 = dateTime1.Value.ToString("yyyy-MM-dd");
            fecha2 = dateTime2.Value.ToString("yyyy-MM-dd");

            // Nombre esta dado por esto:
            // Rep{MODULO}-{TIPO_REPORTE}_{FECHA1-FECHA2}
            string nombreReporte = "Rep"+modulo+"-"+tipoReporte+"_"+fecha1.Replace("-", "")+"-"+fecha2.Replace("-", "");
            ReportesCitasManager reporte = new ReportesCitasManager(nombreReporte, fecha1, fecha2, tipoReporte);
            reporte.GenerarReporte(tipoReporte);

            try
            {
                string DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string carpetaReportes = Path.Combine(DirectorioProyecto, "Reportes-Arch");

                string rutaPDF = Path.Combine(carpetaReportes, nombreReporte+".pdf");

                if (File.Exists(rutaPDF))
                {
                    //pdfViewCita.LoadDocument(rutaPDF); // Cargar el PDF en el visor
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

        private void BtnRazonMasFrec_Click(object sender, EventArgs e)
        {
            BtnRazonMenFrec.Visible = false;
            MostrarComp();
            tipoReporte = "01";

        }
        private void MostrarComp()
        {
            pdfViewCita.Visible = true;
            BtnImprimir.Visible = true;
            BtnGenerar.Visible = true;
            lblA.Visible = true;
            lblPreview.Visible = true;
            lblFecha.Visible = true;
            dateTime1.Visible = true;
            dateTime2.Visible = true;
            BtnVolver.Visible = true;
        }

        private void BtnRazonMenFrec_Click(object sender, EventArgs e)
        {
            BtnRazonMasFrec.Visible = false;
            MostrarComp();
            tipoReporte = "02";
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            this.Refresh();
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            parentForm.formularioHijo(new MenuReportes(parentForm));
        }
    }
}
