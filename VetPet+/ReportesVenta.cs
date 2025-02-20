using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class ReportesVenta : Form
    {
        private Form1 parentForm;
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        public ReportesVenta()
        {
            InitializeComponent();

        }
        public ReportesVenta(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;

        }

        private void ReportesVenta_Load(object sender, EventArgs e)
        {
            originalWidth = this.Width;
            originalHeight = this.Height;

            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }
    }
}
