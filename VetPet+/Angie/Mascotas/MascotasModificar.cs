using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_.Angie.Mascotas;

namespace VetPet_
{
    public partial class MascotasModificar : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private string nombreMascota;
        private Mismetodos mismetodos = new Mismetodos();
        // Variable de clase para almacenar el id de la mascota
        private int idMascota;

        private Form1 parentForm;

        public MascotasModificar()
        {
            InitializeComponent();
            InitializeComponent();
            this.Load += MascotasModificar_Load;       // Evento Load
            this.Resize += MascotasModificar_Resize;   // Evento Resize

            comboBox1.KeyDown += comboBox1_KeyDown;
        }

        public MascotasModificar(Form1 parent, string nombreMascota)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
            this.nombreMascota = nombreMascota;
            CargarMascota();
        }
        private void CargarMascota()
        {
            try
            {
                // Abrir la conexión una vez
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

                    // Consulta para obtener razas según la especie seleccionada
                    string queryRazas = @"
                SELECT nombre FROM Raza ORDER BY nombre";

                    using (SqlCommand comandoRazas = new SqlCommand(queryRazas, mismetodos.GetConexion()))
                    {

                        using (SqlDataReader readerRazas = comandoRazas.ExecuteReader())
                        {
                            while (readerRazas.Read())
                            {
                                comboBox2.Items.Add(readerRazas["nombre"].ToString());
                            }
                        }
                    }
                string query = @"
          SELECT 
               Mascota.nombre AS Nombre,
               Especie.nombre AS Especie,
               Raza.nombre AS Raza,
               Mascota.fechaNacimiento AS FechaNacimiento,
               Mascota.peso AS Peso,
               Mascota.sexo AS Sexo,
               Mascota.esterilizado AS Esterilizado,
               STRING_AGG(Sensibilidad.nombre, ', ') AS Sensibilidades
           FROM 
               Mascota
           INNER JOIN 
               Especie ON Mascota.idEspecie = Especie.idEspecie
           INNER JOIN 
               Raza ON Mascota.idRaza = Raza.idRaza
           LEFT JOIN 
               Mascota_Sensibilidad ON Mascota.idMascota = Mascota_Sensibilidad.idMascota
           LEFT JOIN 
               Sensibilidad ON Mascota_Sensibilidad.idSensibilidad = Sensibilidad.idSensibilidad
           WHERE 
               Mascota.nombre = @nombreMascota
           GROUP BY 
               Mascota.nombre, Especie.nombre, Raza.nombre, Mascota.fechaNacimiento, Mascota.peso, Mascota.sexo, Mascota.esterilizado;
           ";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    comando.Parameters.AddWithValue("@nombreMascota", nombreMascota);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);

                            // Calcular la diferencia de años entre la fecha actual y la fecha de nacimiento
                            int edad = DateTime.Now.Year - fechaNacimiento.Year;

                            // Ajustar si aún no ha cumplido años este año
                            if (DateTime.Now < fechaNacimiento.AddYears(edad))
                            {
                                edad--;
                            }

                            // Mostrar los datos en controles del formulario
                            textBox1.Text = reader["Nombre"].ToString();
                            comboBox1.Text = reader["Especie"].ToString();
                            comboBox2.Text = reader["Raza"].ToString();
                            textBox6.Text = $"{reader["Peso"]} kg";
                            textBox4.Text = $"{edad} años"; // Muestra la edad calculada en el textBox7
                            dateTimePicker1.Value = fechaNacimiento;

                            // Sexo
                            string sexo = reader["Sexo"].ToString();
                            if (sexo == "M") radioButton1.Checked = true; // Masculino
                            if (sexo == "F") radioButton2.Checked = true; // Femenino

                            // Esterilizado
                            string esterilizado = reader["Esterilizado"].ToString();
                            if (esterilizado == "S") radioButton6.Checked = true; // SI
                            if (esterilizado == "N") radioButton5.Checked = true; // NO

                            // Sensibilidades
                            string sensibilidades = reader["Sensibilidades"].ToString();
                            richTextBox1.Text = string.IsNullOrEmpty(sensibilidades)
                                ? "Sin sensibilidades registradas"
                                : sensibilidades;
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron detalles para esta mascota.", "Información");
                        }
                    }
                }
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


        private void MascotasModificar_Load(object sender, EventArgs e)
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

        private void MascotasModificar_Resize(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasEliminarConfirm(parentForm, nombreMascota)); // Pasamos la referencia de Form1 a 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir la conexión
                mismetodos.AbrirConexion();

                // Obtener los valores de los controles del formulario
                string nombreMascota = textBox1.Text;
                string especie = comboBox1.SelectedItem?.ToString();
                string raza = comboBox2.SelectedItem?.ToString();
                decimal peso = decimal.Parse(textBox6.Text.Replace(" kg", ""));
                string sexo = radioButton1.Checked ? "M" : "F";
                string esterilizado = radioButton6.Checked ? "S" : "N";
                DateTime fechaNacimiento = dateTimePicker1.Value; // Obtener la fecha del DateTimePicker
                string sensibilidades = richTextBox1.Text;

                // Actualizar los datos de la mascota en la tabla Mascota
                string queryUpdateMascota = @"
        UPDATE Mascota
        SET 
            nombre = @nombre,
            idEspecie = (SELECT idEspecie FROM Especie WHERE nombre = @especie),
            idRaza = (SELECT idRaza FROM Raza WHERE nombre = @raza),
            fechaNacimiento = @fechaNacimiento,
            peso = @peso,
            sexo = @sexo,
            esterilizado = @esterilizado"; 

                using (SqlCommand comando = new SqlCommand(queryUpdateMascota, mismetodos.GetConexion()))
                {
                    comando.Parameters.AddWithValue("@nombre", nombreMascota);
                    comando.Parameters.AddWithValue("@especie", especie); // Asegúrate de que el nombre del parámetro coincida
                    comando.Parameters.AddWithValue("@raza", raza);
                    comando.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
                    comando.Parameters.AddWithValue("@peso", peso);
                    comando.Parameters.AddWithValue("@sexo", sexo);
                    comando.Parameters.AddWithValue("@esterilizado", esterilizado);

                    int rowsAffected = comando.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Datos de la mascota actualizados correctamente.", "Éxito");
                        parentForm.formularioHijo(new MascotasConsultar(parentForm, nombreMascota)); // Actualizar la referencia si el nombre cambió
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la mascota para actualizar.", "Advertencia");
                    }
                }

                // Actualizar las sensibilidades (código anterior)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar las modificaciones: " + ex.Message, "Error");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasConsultar(parentForm, nombreMascota)); // Pasamos la referencia de Form1 a 
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
                        mismetodos.Insertar("INSERT INTO especie (nombre) VALUES (@nombre)", nuevaEspecie);
                        MessageBox.Show("Especie creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mismetodos.ActualizarComboBox(comboBox1, "SELECT nombre FROM especie", "nombre");
                        CargarMascota();
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
                string nuevaRaza = ValidarYFormatearTexto(comboBox2.Text);

                // Verificar si la especie ya existe en la base de datos
                if (!mismetodos.Existe("SELECT COUNT(*) FROM raza WHERE nombre = @nombre", nuevaRaza))
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
                        mismetodos.Insertar("INSERT INTO raza (nombre) VALUES (@nombre)", nuevaRaza);
                        MessageBox.Show("Raza creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mismetodos.ActualizarComboBox(comboBox1, "SELECT nombre FROM raza", "nombre");
                        CargarMascota();   
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
