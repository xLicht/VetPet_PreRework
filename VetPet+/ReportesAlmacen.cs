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
    public partial class ReportesAlmacen : Form
    {
        private Form1 parentForm;
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        public ReportesAlmacen()
        {
            InitializeComponent();
            this.Load += ReportesAlmacen_Load;       // Evento Load
            this.Resize += ReportesAlmacen_Resize;   // Evento Resize
        }
        public ReportesAlmacen(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ReportesAlmacen_Load(object sender, EventArgs e)
        {
            originalWidth = this.Width;
            originalHeight = this.Height;

            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void ReportesAlmacen_Resize(object sender, EventArgs e)
        {
            // Calcular el factor de escala
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in this.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];

                    // Ajustar las dimensiones
                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);

                    // Ajustar el tamaño de la fuente
                    control.Font = new Font(control.Font.FontFamily, info.fontSize * Math.Min(scaleX, scaleY));
                }
            }
        }

        private void BtnProdMasVend_Click(object sender, EventArgs e)
        {

        }

        private void BtnDescargar_Click(object sender, EventArgs e)
        {
            string rutaPDF = Path.Combine(Application.StartupPath, "PDF_ReportesAlmacen", "A1. Escaner DML.pdf");
            webBrowser1.Navigate(rutaPDF);

        }
    }
}
