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
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        public AlmacenAgregarProducto()
        {
            InitializeComponent();
            this.Load += AlmacenAgregarProducto_Load;       // Evento Load
            this.Resize += AlmacenAgregarProducto_Resize;   // Evento Resize
        }
        public AlmacenAgregarProducto(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent; // Guardamos la referencia de Form1
        }
        private void AlmacenAgregarProducto_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
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

        private void AlmacenAgregarProducto_Resize(object sender, EventArgs e)
        {
            // Calcular el factor de escala
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in this.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];

                    // Ajustar las dimensiones
                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);

                    // Ajustar el tamaño de la fuente
                    control.Font = new Font(control.Font.FontFamily, info.fontSize * Math.Min(scaleX, scaleY));
                }
            }
        }

        private void btnElegir_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }
    }
}
