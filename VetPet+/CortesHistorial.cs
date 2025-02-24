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
    public partial class CortesHistorial : FormPadre
    {
        public CortesHistorial()
        {
            InitializeComponent();
        }
        public CortesHistorial(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CortesHistorial_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesMenus(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProducto
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesConsultar(parentForm)); // Pasamos la referencia de Form1 a 
        }
    }
}
