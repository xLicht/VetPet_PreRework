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
    public partial class AlmacenAvisoEliminar : Form
    {
        public string Resultado { get; private set; }

        public AlmacenAvisoEliminar()
        {
            InitializeComponent();
        }

        private void AlmacenAvisoEliminar_Load(object sender, EventArgs e)
        {

        }

        private void btnSi_Click(object sender, EventArgs e)
        {
            Resultado = "Si";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Resultado = "No";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
