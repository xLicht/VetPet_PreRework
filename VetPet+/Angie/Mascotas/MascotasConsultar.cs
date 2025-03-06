using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_.Angie;
using VetPet_.Angie.Mascotas;

namespace VetPet_
{
    public partial class MascotasConsultar : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos metodos = new Mismetodos();

        private Form1 parentForm;
        public MascotasConsultar()
        {
            InitializeComponent();
            InitializeComponent();
            this.Load += MascotasConsultar_Load;       // Evento Load
            this.Resize += MascotasConsultar_Resize;   // Evento Resize
            //comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox1.KeyDown += comboBox1_KeyDown;
        }

        public MascotasConsultar(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
        }

        private void MascotasConsultar_Load(object sender, EventArgs e)
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

        private void MascotasConsultar_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new MascotasModificar(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasListado(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgendar(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es "Enter"
            if (e.KeyCode == Keys.Enter)
            {
                // Obtener el texto ingresado por el usuario
                string nuevaEspecie = comboBox1.Text;

                // Verificar si la especie ya existe en la base de datos
                if (!metodos.Existe ("SELECT COUNT(*) FROM especie WHERE nombre = @nombre",nuevaEspecie))
                {
                    // Preguntar al usuario si desea crear la nueva especie
                    DialogResult result = MessageBox.Show(
                        $"La especie '{nuevaEspecie}' no existe. ¿Desea crearla?",
                        "Crear nueva especie",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    // Si el usuario elige "Sí", insertar la nueva especie en la base de datos
                    if (result == DialogResult.Yes)
                    {
                        metodos.Insertar("INSERT INTO especie (nombre) VALUES (@nombre)", nuevaEspecie);
                        MessageBox.Show("Especie creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        metodos.ActualizarComboBox(comboBox1,"SELECT nombre FROM especie", "nombre");   
                    }
                }
                else
                {
                    MessageBox.Show("La especie ya existe.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es "Enter"
            if (e.KeyCode == Keys.Enter)
            {
                // Obtener el texto ingresado por el usuario
                string nuevaRaza = comboBox1.Text;

                // Verificar si la especie ya existe en la base de datos
                if (!metodos.Existe("SELECT COUNT(*) FROM raza WHERE nombre = @nombre", nuevaRaza))
                {
                    // Preguntar al usuario si desea crear la nueva especie
                    DialogResult result = MessageBox.Show(
                        $"La raza '{nuevaRaza}' no existe. ¿Desea crearla?",
                        "Crear nueva raza",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    // Si el usuario elige "Sí", insertar la nueva especie en la base de datos
                    if (result == DialogResult.Yes)
                    {
                        metodos.Insertar("INSERT INTO raza (nombre) VALUES (@nombre)", nuevaRaza);
                        MessageBox.Show("Raza creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        metodos.ActualizarComboBox(comboBox1, "SELECT nombre FROM raza", "nombre");
                    }
                }
                else
                {
                    MessageBox.Show("La raza ya existe.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
