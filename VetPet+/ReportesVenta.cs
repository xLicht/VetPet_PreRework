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
    public partial class ReportesVenta : FormPadre
    {
        public ReportesVenta()
        {
            InitializeComponent();

        }
        public ReportesVenta(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;

        }
    }
}
