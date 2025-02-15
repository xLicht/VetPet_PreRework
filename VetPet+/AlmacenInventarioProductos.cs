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
    public partial class AlmacenInventarioProductos : Form
    {
        public AlmacenInventarioProductos()
        {
            InitializeComponent();
        }

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtProducto_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtProducto.Text == "Buscar nombre de producto") // Si el texto predeterminado está presente
            {
                txtProducto.Text = ""; // Limpia el TextBox
            }
        }

        private void txtProducto_Leave(object sender, EventArgs e)
        {
            // Si el usuario no escribe nada, muestra un texto predeterminado
            if (string.IsNullOrEmpty(txtProducto.Text))
            {
                txtProducto.Text = "Buscar nombre de producto"; // Texto predeterminado
            }
        }
    }
}
