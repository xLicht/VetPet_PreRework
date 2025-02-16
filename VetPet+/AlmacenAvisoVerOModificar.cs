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
    public partial class AlmacenAvisoVerOModificar : Form
    {
        public string Resultado { get; private set; }

        public AlmacenAvisoVerOModificar()
        {
            InitializeComponent();
        }

        private void AlmacenAvisoVerOModificar_Load(object sender, EventArgs e)
        {
            //xd
        }

        public AlmacenAvisoVerOModificar(string nombre)
        {
            InitializeComponent();
            label2.Text = $"¿Qué deseas hacer con {nombre}?";
        }


        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            Resultado = "Modificar";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnVer_Click_1(object sender, EventArgs e)
        {
            Resultado = "Ver";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
