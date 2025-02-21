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

    }
}
