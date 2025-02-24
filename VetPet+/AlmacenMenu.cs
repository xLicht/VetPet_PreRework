using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class AlmacenMenu : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;

        public AlmacenMenu()
        {
            InitializeComponent();
            this.Load += AlmacenMenu_Load;       // Evento Load
            this.Resize += AlmacenMenu_Resize;   // Evento Resize
            this.Controls.SetChildIndex(pictureBox8, 0); // Índice 0 = Capa superior
        }

        public AlmacenMenu(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void AlmacenMenu_Load(object sender, EventArgs e)
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

        private void AlmacenMenu_Resize(object sender, EventArgs e)
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

        private void btnProductos_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void btnMedicamentos_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenRecibirPedido(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProducto
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenHistorial(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProducto
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProducto
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
