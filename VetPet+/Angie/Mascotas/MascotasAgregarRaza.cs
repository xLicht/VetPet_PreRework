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
        private int idEspecie;
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
        public MascotasAgregarRaza(Form1 parent, string raza)
        {
            InitializeComponent();
            this.Load += MascotasAgregarRaza_Load;       // Evento Load
            this.Resize += MascotasAgregarRaza_Resize;
            parentForm = parent;  // Guardamos la referencia de Form1
            this.raza = raza;
            CargarMascota();
        }
        private void CargarMascota()
        {
            try
            {
                mismetodos.AbrirConexion();

                // Cargar las especies
                string queryEsp = "SELECT nombre,idEspecie FROM Especie ORDER BY nombre";
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el ComboBox tenga un elemento seleccionado
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecciona una especie.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el nombre de la especie seleccionada en el ComboBox
                string nombreEspecie = comboBox1.SelectedItem.ToString();

                // Validar que el nombre de la raza no esté vacío
                string nombreRaza = textBox1.Text.Trim();
                if (string.IsNullOrEmpty(nombreRaza))
                {
                    MessageBox.Show("Por favor, ingresa un nombre para la raza.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir la conexión a la base de datos
                mismetodos.AbrirConexion();

                // Obtener el idEspecie basado en el nombre de la especie
                int idEspecie = 0;
                string queryIdEsp = "SELECT idEspecie FROM Especie WHERE nombre = @nombre";
                using (SqlCommand comandoIdEsp = new SqlCommand(queryIdEsp, mismetodos.GetConexion()))
                {
                    comandoIdEsp.Parameters.AddWithValue("@nombre", nombreEspecie);
                    var result = comandoIdEsp.ExecuteScalar();
                    if (result != null)
                    {
                        idEspecie = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("La especie seleccionada no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Verificar si la raza ya existe
                string queryExiste = "SELECT COUNT(*) FROM raza WHERE nombre = @nombre AND idEspecie = @idEspecie";
                using (SqlCommand comandoExiste = new SqlCommand(queryExiste, mismetodos.GetConexion()))
                {
                    comandoExiste.Parameters.AddWithValue("@nombre", nombreRaza);
                    comandoExiste.Parameters.AddWithValue("@idEspecie", idEspecie);
                    int existe = Convert.ToInt32(comandoExiste.ExecuteScalar());

                    if (existe == 0)
                    {
                        // Insertar la nueva raza
                        string queryInsertar = "INSERT INTO raza (nombre, idEspecie) VALUES (@nombre, @idEspecie); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand comandoInsertar = new SqlCommand(queryInsertar, mismetodos.GetConexion()))
                        {
                            comandoInsertar.Parameters.AddWithValue("@nombre", nombreRaza);
                            comandoInsertar.Parameters.AddWithValue("@idEspecie", idEspecie);

                            // Ejecutar la inserción y obtener el ID de la raza insertada
                            int idRazaInsertada = Convert.ToInt32(comandoInsertar.ExecuteScalar());

                            if (idRazaInsertada > 0)
                            {
                                MessageBox.Show("Raza creada ", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Redirigir al formulario correspondiente
                                if (nombreMascota != null)
                                {
                                    parentForm.formularioHijo(new MascotasModificar(parentForm, idMascota, nombreMascota));
                                }
                                else
                                {
                                    parentForm.formularioHijo(new MascotasAgregarMascota(parentForm));
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se pudo crear la raza.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("La raza ya existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                mismetodos.CerrarConexion();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasModificar(parentForm, idMascota, nombreMascota));
        }
    }
}
