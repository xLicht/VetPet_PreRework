using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_.Angie.Ventas;

namespace VetPet_.Angie.Mascotas
{
    public partial class MascotasVerEnfermedad : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodo = new Mismetodos();
        public int idAlergia;
        private Form1 parentForm;
        public MascotasVerEnfermedad(Form1 parent, int idAlergia)
        {
            InitializeComponent();
            this.Load += MascotasVerEnfermedad_Load;       // Evento Load
            this.Resize += MascotasVerEnfermedad_Resize;
            parentForm = parent;  // Guardamos la referencia de Form         
            if (label1.Text == "Consultar Enfermedad")
            {
                this.idAlergia = idAlergia;
                Consultar();
            }
            button4.Visible = false;
        }
        public void Consultar()
        {
            Dictionary<string, Control> mapeoColumnasControles = new Dictionary<string, Control>
            {
                { "nombre", comboBox2 },        // Mapea la columna "nombre" al TextBox txtNombre
                { "descripcion", richTextBox1 }
            };
            mismetodo.CargarDatosGenerico("Enfermedad", idAlergia, mapeoColumnasControles);
            label4.Visible = false;
            comboBox1.Visible = false;
        }
        public void Modificar()
        {
            string nombre = comboBox2.Text;
            string descripcion = richTextBox1.Text;
            Dictionary<string, object> parametrosValores = new Dictionary<string, object>
            {
                { "nombre", nombre },        // Mapea la columna "nombre" al valor proporcionado
                { "descripcion", descripcion },
            };

            mismetodo.ModificarDatosGenerico("Enfermedad", idAlergia, parametrosValores);
        }
        public void EliminarAlergiaEnCascada()
        {
            List<string> tablasRelacionadas = new List<string>
    {
        "Mascota_Enfermedad",  // Tabla que relaciona mascotas con alergias
        "Especie_Enfermedad",  // Tabla que relaciona especies con alergias
        "Raza_Enfermedad"      // Tabla que relaciona razas con alergias
    };
            mismetodo.EliminarEnCascada("Enfermedad", idAlergia, tablasRelacionadas);
        }
        private void MascotasVerEnfermedad_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void MascotasVerEnfermedad_Resize(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasVerEnfermedades(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "Modificar Enfermedad";
            button4.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Modificar Enfermedad")
            {
                Modificar();
                parentForm.formularioHijo(new MascotasVerEnfermedades(parentForm)); // Pasamos la referencia de Form1 a

            }
            if (label1.Text == "Consultar Enfermedad")
            {
                parentForm.formularioHijo(new MascotasVerEnfermedades(parentForm)); // Pasamos la referencia de Form1 a
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Text = "Eliminar";
            if (button4.Text == "Eliminar")
            {
                EliminarAlergiaEnCascada();
                parentForm.formularioHijo(new MascotasVerEnfermedades(parentForm)); // Pasamos la referencia de Form1 a
            }
        }
    }
}
