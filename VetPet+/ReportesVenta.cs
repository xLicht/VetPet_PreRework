using Pruebas_PDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class ReportesVenta : FormPadre
    {
        string modulo = "Almacen";
        string tipoReporte;
        string fecha1;
        string fecha2;
        public ReportesVenta()
        {
            InitializeComponent();

        }
        public ReportesVenta(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;

        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            fecha1 = dateTime1.Value.ToString("yyyy-MM-dd");
            fecha2 = dateTime2.Value.ToString("yyyy-MM-dd");

            // Nombre esta dado por esto:
            // Rep{MODULO}-{TIPO_REPORTE}_{FECHA1-FECHA2}
            string nombreReporte = "Rep" + modulo + "-" + tipoReporte + "_" + fecha1.Replace("-", "") + "-" + fecha2.Replace("-", "");
            ReporteVentasManager reporte = new ReporteVentasManager(nombreReporte, fecha1, fecha2, tipoReporte);
            reporte.GenerarReporte(tipoReporte);

            try
            {
                string DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string carpetaReportes = Path.Combine(DirectorioProyecto, "Reportes-Arch");

                string rutaPDF = Path.Combine(carpetaReportes, nombreReporte + ".pdf");

                if (File.Exists(rutaPDF))
                {
                    pdfViewVent.LoadDocument(rutaPDF); // Cargar el PDF en el visor
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
        private void SwitchControls(object sender, EventArgs e)
        {
            if (sender is Control clickedControl) // Verifica si el emisor es un control
            {
                foreach (Control ctrl in this.Controls) // Itera sobre todos los controles del formulario
                {
                    if (ctrl != clickedControl && ctrl.Tag?.ToString() == "1") // Si no es el botón presionado y tiene Tag == 1
                    {
                        ctrl.Visible = !ctrl.Visible; // Invierte el estado Enabled
                    }
                }
            }
        }
        private void BtnVenMasAlt_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "01";
        }

        private void BtnVentMasBaj_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "02";
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ReportesVenta(parentForm));
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            parentForm.formularioHijo(new MenuReportes(parentForm));
        }
    }
}
