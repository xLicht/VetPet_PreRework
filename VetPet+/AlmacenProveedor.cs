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
    public partial class AlmacenProveedor : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;

        public AlmacenProveedor()
        {
            InitializeComponent();
            this.Load += AlmacenProveedor_Load;       // Evento Load
            this.Resize += AlmacenProveedor_Resize;   // Evento Resize
        }
        public AlmacenProveedor(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
                                  // Hacer que el ComboBox sea "invisible"
            comboBox1.FlatStyle = FlatStyle.Flat;  // Quita bordes
            comboBox1.BackColor = this.BackColor;  // Igual que el fondo del formulario
            comboBox1.ForeColor = this.BackColor;  // Oculta el texto
            comboBox1.DropDownWidth = 150;         // Ancho del desplegable
        }

        private void txtProveedor_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtProveedor.Text == "Buscar nombre de proveedor") // Si el texto predeterminado está presente
            {
                txtProveedor.Text = ""; // Limpia el TextBox
            }
        }

        private void AlmacenProveedor_Load(object sender, EventArgs e)
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

        private void AlmacenProveedor_Resize(object sender, EventArgs e)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenAgregarProducto(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenAgregarProducto(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.ForeColor = Color.Black;
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            comboBox1.ForeColor = this.BackColor;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
