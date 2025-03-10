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
using VetPet_.Angie.Mascotas;

namespace VetPet_.Angie
{
    public partial class MascotasListado : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodos;

        private Form1 parentForm;
        public MascotasListado()
        {
            InitializeComponent();
            this.Load += MascotasListado_Load;       // Evento Load
            this.Resize += MascotasListado_Resize;   // Evento Resize
        }
        public MascotasListado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form
            CargarMascota();
        }
        public void CargarMascota()
        {
            try
            {
                // Crear instancia de Mismetodos
                mismetodos = new Mismetodos();

                // Abrir conexión
                mismetodos.AbrirConexion();

                string query = @"
                SELECT 
                    Mascota.idMascota, 
                    Mascota.nombre AS Mascota,
                    Persona.nombre AS Dueño,
                    Especie.nombre AS Especie,
                    Mascota.fechaNacimiento AS Fecha_Nacimiento
                FROM 
                    Mascota
                INNER JOIN 
                    Persona ON Mascota.idPersona = Persona.idPersona
                INNER JOIN 
                    Especie ON Mascota.idEspecie = Especie.idEspecie
                WHERE 
                    Mascota.estado <> 'D'; 
                ";


                // Usar `using` para asegurar la correcta liberación de recursos
                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    // Crear un DataTable y llenar los datos
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    // Asignar el DataTable al DataGridView
                    dataGridView1.DataSource = tabla;
                    dataGridView1.Columns["idMascota"].Visible = false; // Oculta la columna

                }
            }
            catch (Exception ex)
            {
                // Manejar el error si ocurre algún problema
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión al finalizar
                mismetodos.CerrarConexion();
            }
        }

        private void MascotasListado_Load(object sender, EventArgs e)
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

        private void MascotasListado_Resize(object sender, EventArgs e)
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    // Obtener el idMascota y nombre de la mascota seleccionada
                    int idMascota = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idMascota"].Value);
                    string nombreMascota = dataGridView1.Rows[e.RowIndex].Cells["Mascota"].Value.ToString();

                    // Abrir el formulario de detalles de la mascota con el idMascota correcto
                    parentForm.formularioHijo(new MascotasConsultar(parentForm, idMascota, nombreMascota));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MenuAtencionaCliente(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void FiltrarDatos()
        {
            try
            {
                mismetodos.AbrirConexion(); // Abre la conexión usando Mismetodos
                string filtroTexto = textBox1.Text.Trim();
                string columnaSeleccionada = comboBox1.SelectedItem?.ToString(); // Puede ser null

                if (string.IsNullOrEmpty(filtroTexto))
                {
                    CargarMascota();
                    return; // No hace nada si el campo de búsqueda está vacío
                }

                // Mapeo de nombres visibles a nombres reales de columnas
                Dictionary<string, string> mapaColumnas = new Dictionary<string, string>
        {
            { "Mascota", "Mascota.nombre" },
            { "Dueño", "Persona.nombre" },
            { "Especie", "Especie.nombre" },
            { "Fecha_Nacimiento", "Mascota.fechaNacimiento" }
        };

                string query;

                if (string.IsNullOrEmpty(columnaSeleccionada) || !mapaColumnas.ContainsKey(columnaSeleccionada))
                {
                    // Si no hay columna seleccionada, busca en todas las columnas relevantes
                    query = @"
            SELECT 
                Mascota.idMascota, 
                Mascota.nombre AS Mascota,
                Persona.nombre AS Dueño,
                Especie.nombre AS Especie,
                Mascota.fechaNacimiento AS Fecha_Nacimiento
            FROM 
                Mascota
            INNER JOIN 
                Persona ON Mascota.idPersona = Persona.idPersona
            INNER JOIN 
                Especie ON Mascota.idEspecie = Especie.idEspecie
            WHERE 
                Mascota.nombre LIKE @filtro 
                OR Persona.nombre LIKE @filtro 
                OR Especie.nombre LIKE @filtro 
                OR CONVERT(VARCHAR, Mascota.fechaNacimiento, 103) LIKE @filtro";
                }
                else
                {
                    string columnaReal = mapaColumnas[columnaSeleccionada];

                    if (columnaSeleccionada == "Fecha_Nacimiento")
                    {
                        query = $@"
                SELECT 
                    Mascota.idMascota, 
                    Mascota.nombre AS Mascota,
                    Persona.nombre AS Dueño,
                    Especie.nombre AS Especie,
                    Mascota.fechaNacimiento AS Fecha_Nacimiento
                FROM 
                    Mascota
                INNER JOIN 
                    Persona ON Mascota.idPersona = Persona.idPersona
                INNER JOIN 
                    Especie ON Mascota.idEspecie = Especie.idEspecie
                WHERE 
                    CONVERT(VARCHAR, {columnaReal}, 103) LIKE @filtro";
                    }
                    else
                    {
                        query = $@"
                SELECT 
                    Mascota.idMascota, 
                    Mascota.nombre AS Mascota,
                    Persona.nombre AS Dueño,
                    Especie.nombre AS Especie,
                    Mascota.fechaNacimiento AS Fecha_Nacimiento
                FROM 
                    Mascota
                INNER JOIN 
                    Persona ON Mascota.idPersona = Persona.idPersona
                INNER JOIN 
                    Especie ON Mascota.idEspecie = Especie.idEspecie
                WHERE 
                    {columnaReal} LIKE @filtro";
                    }
                }

                // Ejecutar la consulta
                SqlDataAdapter adaptador = new SqlDataAdapter(query, mismetodos.GetConexion());
                adaptador.SelectCommand.Parameters.AddWithValue("@filtro", "%" + filtroTexto + "%");

                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["idMascota"].Visible = false; // Oculta la columna ID
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
            finally
            {
                mismetodos.CerrarConexion(); // Cierra la conexión para evitar bloqueos en la base de datos
            }
        }


    }
}

