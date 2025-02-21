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
    public partial class AgregarTipoCirugia : FormPadre
    {
        public AgregarTipoCirugia()
        {
            InitializeComponent();
        }
        public AgregarTipoCirugia(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }
        private void AgregarTipoCirugia_Load(object sender, EventArgs e)
        {

        }
    }
}
