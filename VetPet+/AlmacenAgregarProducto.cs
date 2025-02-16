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
    public partial class AlmacenAgregarProducto : Form
    {
        private Form1 parentForm;

        public AlmacenAgregarProducto()
        {
            InitializeComponent();
        }
        public AlmacenAgregarProducto(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent; // Guardamos la referencia de Form1
        }
        private void AlmacenAgregarProducto_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm)); // Pasamos la referencia de Form1 a 
        }
    }
}
