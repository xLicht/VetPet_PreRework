using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_.Angie.Mascotas
{
    public partial class MascotasVerSensibilidad : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodo = new Mismetodos();
        public int idSensibilidad;
        private Form1 parentForm;
        public MascotasVerSensibilidad(Form1 parent, int idSensibilidad)
        {
            InitializeComponent();
            this.Load += MascotasVerSensibilidad_Load;       // Evento Load
            this.Resize += MascotasVerSensibilidad_Resize;
            parentForm = parent;  // Guardamos la referencia de Form         
            if (label1.Text == "Consultar Sensibilidad")
            {
                this.idSensibilidad = idSensibilidad;
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
            mismetodo.CargarDatosGenerico("Sensibilidad", idSensibilidad, mapeoColumnasControles);
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

            mismetodo.ModificarDatosGenerico("Sensibilidad", idSensibilidad, parametrosValores);
        }
        public void EliminarEnCascadaSensibilidad()
        {
            List<string> tablasRelacionadas = new List<string>
    {
        "Mascota_Sensibilidad",  // Tabla que relaciona mascotas con alergias
        "Especie_Sensibilidad",  // Tabla que relaciona especies con alergias
        "Raza_Sensibilidad"      // Tabla que relaciona razas con alergias
    };
            mismetodo.EliminarEnCascada("Sensibilidad", idSensibilidad, tablasRelacionadas);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasVerSensibilidades(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "Modificar Sensibilidad";
            button4.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Modificar Sensibilidad")
            {
                Modificar();
                parentForm.formularioHijo(new MascotasVerSensibilidades(parentForm)); // Pasamos la referencia de Form1 a

            }
            if (label1.Text == "Consultar Sensibilidad")
            {
                parentForm.formularioHijo(new MascotasVerSensibilidades(parentForm)); // Pasamos la referencia de Form1 a
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Text = "Eliminar";
            if (button4.Text == "Eliminar")
            {
                EliminarEnCascadaSensibilidad();
                parentForm.formularioHijo(new MascotasVerSensibilidades(parentForm)); // Pasamos la referencia de Form1 a
            }
        }
        private void MascotasVerSensibilidad_Load(object sender, EventArgs e)
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

        private void MascotasVerSensibilidad_Resize(object sender, EventArgs e)
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

    }
}

