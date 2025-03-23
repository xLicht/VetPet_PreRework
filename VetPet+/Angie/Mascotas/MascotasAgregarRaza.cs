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
using VetPet_.Angie.Ventas;

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
        private int idEspecie;
        private Form1 parentForm;
        public MascotasAgregarRaza(Form1 parent, string raza,int idMascota, string nombreMascota)
        {
            InitializeComponent();
            this.Load += MascotasAgregarRaza_Load;       // Evento Load
            this.Resize += MascotasAgregarRaza_Resize;
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            listBox2.SelectedIndexChanged += new EventHandler(listBox2_SelectedIndexChanged);
            parentForm = parent; 
            this.raza = raza;
            this.idMascota = idMascota;
            this.nombreMascota = nombreMascota;
            Cargar();
        }
        public MascotasAgregarRaza(Form1 parent)
        {
            InitializeComponent();
            this.Load += MascotasAgregarRaza_Load;
            this.Resize += MascotasAgregarRaza_Resize;
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            listBox2.SelectedIndexChanged += new EventHandler(listBox2_SelectedIndexChanged);
            parentForm = parent;
            parentForm = parent;  
            Cargar();
        }
        private void Cargar()
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

                string query1 = "SELECT nombre FROM Sensibilidad ORDER BY nombre";
                using (SqlCommand comando = new SqlCommand(query1, mismetodos.GetConexion()))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        // Limpiar el ListBox antes de agregar nuevos elementos
                        listBox1.Items.Clear();

                        // Agregar elementos al ListBox
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader["nombre"].ToString());
                        }
                    }
                }

                string query2 = "SELECT nombre FROM Alergia ORDER BY nombre";
                using (SqlCommand comando = new SqlCommand(query2, mismetodos.GetConexion()))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        // Limpiar el ListBox antes de agregar nuevos elementos
                        listBox2.Items.Clear();

                        // Agregar elementos al ListBox
                        while (reader.Read())
                        {
                            listBox2.Items.Add(reader["nombre"].ToString());
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                // Obtener el ítem seleccionado
                string selectedItem = listBox1.SelectedItem.ToString();

                // Agregar el ítem al RichTextBox
                if (!richTextBox3.Text.Contains(selectedItem))
                {
                    if (richTextBox3.Text.Length > 0)
                    {
                        richTextBox3.AppendText(", " + selectedItem);
                    }
                    else
                    {
                        richTextBox3.AppendText(selectedItem);
                    }
                }
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string selectedItem = listBox2.SelectedItem.ToString();

                if (!richTextBox2.Text.Contains(selectedItem))
                {
                    if (richTextBox2.Text.Length > 0)
                    {
                        richTextBox2.AppendText(", " + selectedItem);
                    }
                    else
                    {
                        richTextBox2.AppendText(selectedItem);
                    }
                }
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

        public void InsertarSensibilidades(int idMascota, string sensibilidadesStr)
        {
            try
            {
                mismetodos.AbrirConexion();
                // Dividir el texto del RichTextBox por comas y saltos de línea
                string[] sensibilidades = sensibilidadesStr.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string sensibilidad in sensibilidades)
                {
                    string sensibilidadTrim = sensibilidad.Trim();

                    string getIdQuery = "SELECT idSensibilidad FROM Sensibilidad WHERE nombre = @sensibilidad;";
                    int idSensibilidad;

                    using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, mismetodos.GetConexion()))
                    {
                        getIdCommand.Parameters.AddWithValue("@sensibilidad", sensibilidadTrim);
                        var result = getIdCommand.ExecuteScalar();
                        idSensibilidad = result != null ? Convert.ToInt32(result) : -1;
                    }
                    string insertMascotaSensQuery = "INSERT INTO Mascota_Sensibilidad (idMascota, idSensibilidad) VALUES (@idMascota, @idSensibilidad);";
                    using (SqlCommand insertMascotaSensCommand = new SqlCommand(insertMascotaSensQuery, mismetodos.GetConexion()))
                    {
                        insertMascotaSensCommand.Parameters.AddWithValue("@idMascota", idMascota);
                        insertMascotaSensCommand.Parameters.AddWithValue("@idSensibilidad", idSensibilidad);
                        insertMascotaSensCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar sensibilidades: {ex.Message}");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        public void InsertarAlergias(int idMascota, string alergiasStr)
        {
            try
            {
                mismetodos.AbrirConexion();
                // Dividir el texto del RichTextBox por comas y saltos de línea
                string[] alergias = alergiasStr.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string alergia in alergias)
                {
                    string alergiaTrim = alergia.Trim();

                    // Verificar si la alergia existe y obtener su id
                    string getIdQuery = "SELECT idAlergia FROM Alergia WHERE nombre = @alergia;";
                    int idAlergia;

                    using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, mismetodos.GetConexion()))
                    {
                        getIdCommand.Parameters.AddWithValue("@alergia", alergiaTrim);
                        var result = getIdCommand.ExecuteScalar();
                        idAlergia = result != null ? Convert.ToInt32(result) : -1;
                    }

                    string insertMascotaAlergQuery = "INSERT INTO Mascota_Alergia (idMascota, idAlergia) VALUES (@idMascota, @idAlergia);";
                    using (SqlCommand insertMascotaAlergCommand = new SqlCommand(insertMascotaAlergQuery, mismetodos.GetConexion()))
                    {
                        insertMascotaAlergCommand.Parameters.AddWithValue("@idMascota", idMascota);
                        insertMascotaAlergCommand.Parameters.AddWithValue("@idAlergia", idAlergia);
                        insertMascotaAlergCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar alergias: {ex.Message}");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        public void AgregarRaza(string nombre, string descripcion, string especie, string sensibilidadesStr, string alergiasStr)
        {
            try
            {
                mismetodos.AbrirConexion();

                // Paso 1: Obtener el idEspecie basado en el nombre de la especie seleccionada en el ComboBox
                string obtenerIdEspecieQuery = "SELECT idEspecie FROM Especie WHERE nombre = @nombreEspecie;";
                int idEspecie;

                using (SqlCommand obtenerIdEspecieCommand = new SqlCommand(obtenerIdEspecieQuery, mismetodos.GetConexion()))
                {
                    obtenerIdEspecieCommand.Parameters.AddWithValue("@nombreEspecie", especie);
                    idEspecie = (int)obtenerIdEspecieCommand.ExecuteScalar();
                }

                // Paso 2: Insertar la nueva raza usando el idEspecie obtenido
                string insertRazaQuery = "INSERT INTO Raza (nombre, idEspecie, descripcion) OUTPUT INSERTED.idRaza VALUES (@nombre, @idEspecie, @descripcion);";
                int idRaza;

                using (SqlCommand insertRazaCommand = new SqlCommand(insertRazaQuery, mismetodos.GetConexion()))
                {
                    insertRazaCommand.Parameters.AddWithValue("@nombre", nombre);
                    insertRazaCommand.Parameters.AddWithValue("@idEspecie", idEspecie);
                    insertRazaCommand.Parameters.AddWithValue("@descripcion", descripcion);
                    idRaza = (int)insertRazaCommand.ExecuteScalar();
                }

                // Paso 3: Insertar sensibilidades y alergias
                InsertarSensibilidades(idRaza, sensibilidadesStr);
                InsertarAlergias(idRaza, alergiasStr);

                MessageBox.Show("Raza, sensibilidades y alergias agregadas correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar la raza: {ex.Message}");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string descripcion = richTextBox1.Text;
            string especie = comboBox1.SelectedItem.ToString();

            // Obtener las sensibilidades y alergias desde los RichTextBox
            string sensibilidadesStr = richTextBox3.Text;
            string alergiasStr = richTextBox2.Text;

            // Llamar al método para agregar la especie con sensibilidades y alergias
            AgregarRaza(nombre, descripcion,especie, sensibilidadesStr, alergiasStr);
            parentForm.formularioHijo(new MascotasVerEspecies(parentForm)); // Pasamos la referencia de Form1 a 

        }

        private void button2_Click(object sender, EventArgs e)
        {
                parentForm.formularioHijo(new MascotasVerRazas(parentForm));
        }
    }
}
