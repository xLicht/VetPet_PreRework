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
    public partial class MascotasVerEspecie : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodos = new Mismetodos();
        string tipo = "";
        private Form1 parentForm;
        int idEspecie;
        public MascotasVerEspecie(Form1 parent, int idEspecie)
        {
            InitializeComponent();
            this.Load += MascotasVerEspecie_Load;       // Evento Load
            this.Resize += MascotasVerEspecie_Resize;
            parentForm = parent;  
            if (label1.Text == "Consultar Especie")
            {
                this.idEspecie = idEspecie;
                Consultar();
            }
            button4.Visible = false;
        }
        public void Consultar()
        {
            Dictionary<string, Control> mapeoColumnasControles = new Dictionary<string, Control>
            {
                { "nombre", textBox1 },        // Mapea la columna "nombre" al TextBox txtNombre
                { "descripcion", richTextBox1 }
            };
            mismetodos.CargarDatosGenerico("Especie", idEspecie, mapeoColumnasControles, listBox2,listBox1);
        }
        public void Modificar()
        {
            string nombre = textBox1.Text;
            string descripcion = richTextBox1.Text;
            Dictionary<string, object> parametrosValores = new Dictionary<string, object>
            {
                { "nombre", nombre },        // Mapea la columna "nombre" al valor proporcionado
                { "descripcion", descripcion },
            };

            mismetodos.ModificarDatosGenerico("Especie", idEspecie, parametrosValores);           
        }

        public void EliminarEnCascada()
        {
            List<string> tablasRelacionadas = new List<string>
        {
            "Mascota",  // Tabla que relaciona mascotas con alergias
            "Raza",  // Tabla que relaciona especies con alergias
            "Especie_Alergia",
            "Especie_Sensibilidad",
        };
            mismetodos.EliminarEnCascada("Especie", idEspecie, tablasRelacionadas);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasVerEspecies(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "Modificar Especie";
            button4.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Modificar Especie")
            {
                Modificar();
                parentForm.formularioHijo(new MascotasVerEspecies(parentForm)); // Pasamos la referencia de Form1 a

            }
            if (label1.Text == "Consultar Especie")
            {
                parentForm.formularioHijo(new MascotasVerEspecies(parentForm)); // Pasamos la referencia de Form1 a
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Text = "Eliminar";
            if (button4.Text == "Eliminar")
            {
                EliminarEnCascada();
                parentForm.formularioHijo(new MascotasVerEspecies(parentForm)); // Pasamos la referencia de Form1 a
            }
        }
        private void MascotasVerEspecie_Load(object sender, EventArgs e)
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

        private void MascotasVerEspecie_Resize(object sender, EventArgs e)
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si se seleccionó un elemento
            if (listBox1.SelectedIndex != -1)
            {
                // Obtener el elemento seleccionado
                string sensibilidadSeleccionada = listBox1.SelectedItem.ToString();

                // Preguntar al usuario si está seguro de eliminar
                DialogResult resultado = MessageBox.Show(
                    $"¿Estás seguro de que deseas eliminar la Alergia '{sensibilidadSeleccionada}'?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                // Si el usuario confirma, eliminar el elemento
                if (resultado == DialogResult.Yes)
                {
                    // Eliminar el elemento seleccionado del ListBox
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                    // Eliminar el elemento de la base de datos
                    mismetodos.EliminarRegistro("Especie_Alergia", "Alergia", sensibilidadSeleccionada);
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si se seleccionó un elemento
            if (listBox2.SelectedIndex != -1)
            {
                // Obtener el elemento seleccionado
                string sensibilidadSeleccionada = listBox2.SelectedItem.ToString();

                // Preguntar al usuario si está seguro de eliminar
                DialogResult resultado = MessageBox.Show(
                    $"¿Estás seguro de que deseas eliminar la sensibilidad '{sensibilidadSeleccionada}'?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                // Si el usuario confirma, eliminar el elemento
                if (resultado == DialogResult.Yes)
                {
                    // Eliminar el elemento seleccionado del ListBox
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);

                    // Eliminar el elemento de la base de datos
                    mismetodos.EliminarRegistro("Especie_Sensibilidad", "Sensibilidad", sensibilidadSeleccionada);
                }
            }
        }
    }
    
}
