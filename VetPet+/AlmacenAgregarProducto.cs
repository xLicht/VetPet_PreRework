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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtNombre.Text == "Nombre de producto") // Si el texto predeterminado está presente
            {
                txtNombre.Text = ""; // Limpia el TextBox
            }
        }

        private void txtMarca_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtMarca.Text == "Marca de producto") // Si el texto predeterminado está presente
            {
                txtMarca.Text = ""; // Limpia el TextBox
            }
        }

        private void txtCantidad_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtCantidad.Text == "Cantidad de producto") // Si el texto predeterminado está presente
            {
                txtCantidad.Text = ""; // Limpia el TextBox
            }
        }

        private void txtProveedor_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtProveedor.Text == "Proveedor") // Si el texto predeterminado está presente
            {
                txtProveedor.Text = ""; // Limpia el TextBox
            }
        }

        private void txtPrecio_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtPrecio.Text == "Precio de producto") // Si el texto predeterminado está presente
            {
                txtPrecio.Text = ""; // Limpia el TextBox
            }
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtDescripcion.Text == "Descripción de producto") // Si el texto predeterminado está presente
            {
                txtDescripcion.Text = ""; // Limpia el TextBox
            }
        }
    }
}
