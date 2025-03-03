using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_.Angie;

namespace VetPet_
{
    public partial class VentasVentanadePago : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;


        public VentasVentanadePago()
        {
            InitializeComponent();
            this.Load += VentasVentanadePago_Load;       // Evento Load
            this.Resize += VentasVentanadePago_Resize;   // Evento Resize

        }

        public VentasVentanadePago(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
        }

        private void VentasVentanadePago_Load(object sender, EventArgs e)
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

        private void VentasVentanadePago_Resize(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasListado(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox15_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ConsultarCita(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox13_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasConfirmacionEfectivo(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasConfirmacionTarjeta(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasAgregarProducto(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            VentasAgregarMedicamento ventasAgregarMedicamento = new VentasAgregarMedicamento(parentForm);
            ventasAgregarMedicamento.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(ventasAgregarMedicamento); // Usar la misma instancia
        }
    }
}
