using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_.Angie.Mascotas
{
    public partial class MascotasAgregarRaza : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodos = new Mismetodos();
        private string raza;
        private int idMascota;
        private string nombreMascota;
        private Form1 parentForm;
        public MascotasAgregarRaza(Form1 parent, string raza,int idMascota, string nombreMascota)
        {
            InitializeComponent();
            this.Load += MascotasAgregarRaza_Load;       // Evento Load
            this.Resize += MascotasAgregarRaza_Resize; 
            parentForm = parent;  // Guardamos la referencia de Form1
            this.raza = raza;
            this.idMascota = idMascota;
            this.nombreMascota = nombreMascota;
            CargarMascota();
        }
        private void CargarMascota()
        {
            try
            {
                mismetodos.AbrirConexion();

                // Cargar las especies
                string queryEsp = "SELECT nombre FROM Especie ORDER BY nombre";
                using (SqlCommand comandoEsp = new SqlCommand(queryEsp, mismetodos.GetConexion()))
                {
                    using (SqlDataReader readerEsp = comandoEsp.ExecuteReader())
                    {
                        // Limpiar el ComboBox antes de agregar nuevos elementos
                        comboBox1.Items.Clear();

                        while (readerEsp.Read())
                        {                          
                            comboBox1.Items.Add(readerEsp["nombre"].ToString());
                        }
                    }
                }
                textBox1.Text = raza;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }
        
        private void MascotasAgregarRaza_Load(object sender, EventArgs e)
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

        private void MascotasAgregarRaza_Resize(object sender, EventArgs e)
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
        public string ValidarYFormatearTexto(string texto)
        {
            // Verificar si el texto está vacío o es nulo
            if (string.IsNullOrEmpty(texto))
                return texto;

            // Validar que el texto contenga solo letras (incluyendo acentos y espacios)
            if (!Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                throw new ArgumentException("El texto solo puede contener letras y espacios.");
            }

            // Formatear el texto: primera letra en mayúscula y el resto en minúscula
            return char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
        }
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es "Enter"
            if (e.KeyCode == Keys.Enter)
            {
                // Obtener el texto ingresado por el usuario
                string nuevaEspecie = ValidarYFormatearTexto(comboBox1.Text);

                // Verificar si la especie ya existe en la base de datos
                if (!mismetodos.Existe("SELECT COUNT(*) FROM especie WHERE nombre = @nombre", nuevaEspecie))
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
                        mismetodos.Insertar("INSERT INTO especie (nombre) VALUES (@nombre)  SELECT SCOPE_IDENTITY();" , nuevaEspecie);
                        MessageBox.Show("Especie creada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mismetodos.ActualizarComboBox(comboBox1, "SELECT nombre FROM especie", "nombre");
                        CargarMascota();
                    }
                }
                else
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nuevaRaza = ValidarYFormatearTexto(comboBox1.Text);

            // Verificar si la especie ya existe en la base de datos
            if (!mismetodos.Existe("SELECT COUNT(*) FROM raza WHERE nombre = @nombre", nuevaRaza))
            {

                    mismetodos.Insertar("INSERT INTO raza (nombre) VALUES (@nombre)", nuevaRaza);
                    MessageBox.Show("Raza creada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    parentForm.formularioHijo(new MascotasModificar(parentForm, idMascota, nombreMascota));
            }
            else
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasModificar(parentForm, idMascota, nombreMascota));
        }
    }
}
