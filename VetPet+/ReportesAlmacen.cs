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
using Pruebas_PDF;


namespace VetPet_
{
    public partial class ReportesAlmacen : FormPadre
    {
        string modulo = "Almacen";
        string tipoReporte;
        string fecha1;
        string fecha2;
        private Form formHijo;
        public ReportesAlmacen()
        {
            InitializeComponent();
        }
        public ReportesAlmacen(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            dateTime1.Value = DateTime.Today.AddDays(-7);
        }

        private void BtnProdMasVend_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "01";
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            fecha1 = dateTime1.Value.ToString("yyyy-MM-dd");
            fecha2 = dateTime2.Value.ToString("yyyy-MM-dd");

            // Nombre esta dado por esto:
            // Rep{MODULO}-{TIPO_REPORTE}_{FECHA1-FECHA2}
            string nombreReporte = "Rep" + modulo + "-" + tipoReporte + "_" + fecha1.Replace("-", "") + "-" + fecha2.Replace("-", "");
            ReporteAlmacenManager reporte = new ReporteAlmacenManager(nombreReporte, fecha1, fecha2, tipoReporte);
            reporte.GenerarReporte(tipoReporte);

            try
            {
                string DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string carpetaReportes = Path.Combine(DirectorioProyecto, "Reportes-Arch");

                string rutaPDF = Path.Combine(carpetaReportes, nombreReporte + ".pdf");

                if (File.Exists(rutaPDF))
                {
                    pdfViewAlma.LoadDocument(rutaPDF); // Cargar el PDF en el visor
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

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ReportesAlmacen(parentForm));
        }

        private void BtnProdMenVend_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "02";
        }

        private void BtnMedMasVend_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "03";
        }

        private void BtnMedMenVend_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "04";
        }

        private void BtnProdBajoStk_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "05";
        }

        private void BtnMedBajoStk_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "06";
        }

        private void BtnProvMasVent_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "07";
        }

        private void BtnProvMenVent_Click(object sender, EventArgs e)
        {
            SwitchControls(sender, e);
            tipoReporte = "08";
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            parentForm.formularioHijo(new MenuReportes(parentForm));
        }
    }
}
