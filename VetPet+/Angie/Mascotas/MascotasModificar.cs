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

        public MascotasModificar(Form1 parent, int idMascota)
        {
            InitializeComponent();
            this.Load += MascotasModificar_Load;       // Evento Load
            this.Resize += MascotasModificar_Resize;   // Evento Resize
            comboBox1.KeyDown += comboBox1_KeyDown;
            parentForm = parent; 
            this.idMascota = idMascota;
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
                Mascota.idMascota,
                Mascota.nombre AS Nombre,
                Especie.nombre AS Especie,
                Raza.nombre AS Raza,
                Mascota.fechaNacimiento AS FechaNacimiento,
                Mascota.peso AS Peso,
                Mascota.sexo AS Sexo,
                Mascota.esterilizado AS Esterilizado,
                STUFF((
                    SELECT DISTINCT ', ' + Sensibilidad.nombre
                    FROM (
                        SELECT Sensibilidad.nombre
                        FROM Mascota_Sensibilidad
                        INNER JOIN Sensibilidad ON Mascota_Sensibilidad.idSensibilidad = Sensibilidad.idSensibilidad
                        WHERE Mascota_Sensibilidad.idMascota = Mascota.idMascota
                        UNION
                        SELECT Sensibilidad.nombre
                        FROM Especie_Sensibilidad
                        INNER JOIN Sensibilidad ON Especie_Sensibilidad.idSensibilidad = Sensibilidad.idSensibilidad
                        WHERE Especie_Sensibilidad.idEspecie = Especie.idEspecie
                        UNION
                        SELECT Sensibilidad.nombre
                        FROM Raza_Sensibilidad
                        INNER JOIN Sensibilidad ON Raza_Sensibilidad.idSensibilidad = Sensibilidad.idSensibilidad
                        WHERE Raza_Sensibilidad.idRaza = Raza.idRaza
                    ) AS Sensibilidad
                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Sensibilidades,
                STUFF((
                    SELECT DISTINCT ', ' + Alergia.nombre
                    FROM (
                        SELECT Alergia.nombre
                        FROM Mascota_Alergia
                        INNER JOIN Alergia ON Mascota_Alergia.idAlergia = Alergia.idAlergia
                        WHERE Mascota_Alergia.idMascota = Mascota.idMascota
                        UNION
                        SELECT Alergia.nombre
                        FROM Especie_Alergia
                        INNER JOIN Alergia ON Especie_Alergia.idAlergia = Alergia.idAlergia
                        WHERE Especie_Alergia.idEspecie = Especie.idEspecie
                        UNION
                        SELECT Alergia.nombre
                        FROM Raza_Alergia
                        INNER JOIN Alergia ON Raza_Alergia.idAlergia = Alergia.idAlergia
                        WHERE Raza_Alergia.idRaza = Raza.idRaza
                    ) AS Alergia
                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Alergias
            FROM 
                Mascota
            INNER JOIN 
                Especie ON Mascota.idEspecie = Especie.idEspecie
            INNER JOIN 
                Raza ON Mascota.idRaza = Raza.idRaza
            WHERE 
                Mascota.idMascota = @idMascota
            GROUP BY 
                Mascota.idMascota, 
                Mascota.nombre, 
                Especie.nombre, 
                Raza.nombre, 
                Mascota.fechaNacimiento, 
                Mascota.peso, 
                Mascota.sexo, 
                Mascota.esterilizado,
                Especie.idEspecie,  -- Añadido al GROUP BY
                Raza.idRaza;        -- Añadido al GROUP BY
                ";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    comando.Parameters.AddWithValue("@idMascota", idMascota);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Almacenar el idMascota
                            idMascota = Convert.ToInt32(reader["idMascota"]);

                            DateTime fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);
                            int edad = DateTime.Now.Year - fechaNacimiento.Year;
                            if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;

                            // Mostrar los datos en los controles
                            textBox1.Text = reader["Nombre"].ToString();
                            comboBox1.Text = reader["Especie"].ToString();
                            comboBox2.Text = reader["Raza"].ToString();
                            textBox6.Text = $"{reader["Peso"]} kg";
                            textBox4.Text = $"{edad} años";
                            dateTimePicker1.Value = fechaNacimiento;

                            string sexo = reader["Sexo"].ToString();
                            if (sexo == "M") radioButton1.Checked = true;
                            if (sexo == "F") radioButton2.Checked = true;

                            string esterilizado = reader["Esterilizado"].ToString();
                            if (esterilizado == "S") radioButton6.Checked = true;
                            if (esterilizado == "N") radioButton5.Checked = true;

                            string sensibilidades = reader["Sensibilidades"].ToString();
                            richTextBox1.Text = string.IsNullOrEmpty(sensibilidades)
                                ? "Sin sensibilidades registradas"
                                : sensibilidades;

                            string alergias = reader["Alergias"].ToString();
                            richTextBox2.Text = string.IsNullOrEmpty(sensibilidades)
                                ? "Sin alergias registradas"
                                : alergias;
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
            parentForm.formularioHijo(new MascotasEliminarConfirm(parentForm,idMascota, textBox1.Text)); // Pasamos la referencia de Form1 a 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mismetodos.AbrirConexion();

                // Actualizar los datos básicos de la mascota
                string query = @"
                UPDATE Mascota
                SET 
                    nombre = @nombre,
                    idEspecie = (SELECT idEspecie FROM Especie WHERE nombre = @especie),
                    idRaza = (SELECT idRaza FROM Raza WHERE nombre = @raza),
                    fechaNacimiento = @fechaNacimiento,
                    peso = @peso,
                    sexo = @sexo,
                    esterilizado = @esterilizado
                WHERE 
                    idMascota = @idMascota;";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    comando.Parameters.AddWithValue("@idMascota", idMascota);
                    comando.Parameters.AddWithValue("@nombre", textBox1.Text); 
                    comando.Parameters.AddWithValue("@especie", comboBox1.Text);
                    comando.Parameters.AddWithValue("@raza", comboBox2.Text);
                    comando.Parameters.AddWithValue("@fechaNacimiento", dateTimePicker1.Value);
                    comando.Parameters.AddWithValue("@peso", decimal.Parse(textBox6.Text.Replace("kg", "")));
                    comando.Parameters.AddWithValue("@sexo", radioButton1.Checked ? "M" : "F");
                    comando.Parameters.AddWithValue("@esterilizado", radioButton6.Checked ? "S" : "N");

                    int rowsAffected = comando.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Eliminar las sensibilidades antiguas
                        string deleteQuery = "DELETE FROM Mascota_Sensibilidad WHERE idMascota = @idMascota;";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, mismetodos.GetConexion()))
                        {
                            deleteCommand.Parameters.AddWithValue("@idMascota", idMascota);
                            deleteCommand.ExecuteNonQuery();
                        }
                        string deleteAQuery = "DELETE FROM Mascota_Alergia WHERE idMascota = @idMascota;";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteAQuery, mismetodos.GetConexion()))
                        {
                            deleteCommand.Parameters.AddWithValue("@idMascota", idMascota);
                            deleteCommand.ExecuteNonQuery();
                        }
                        string[] sensibilidades = richTextBox1.Text.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string sensibilidad in sensibilidades)
                        {
                            string sensibilidadTrim = sensibilidad.Trim();

                            // Verificar si la sensibilidad existe y obtener su id
                            string getIdQuery = "SELECT idSensibilidad FROM Sensibilidad WHERE nombre = @sensibilidad;";
                            int idSensibilidad;

                            using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, mismetodos.GetConexion()))
                            {
                                getIdCommand.Parameters.AddWithValue("@sensibilidad", sensibilidadTrim);
                                var result = getIdCommand.ExecuteScalar();
                                idSensibilidad = result != null ? Convert.ToInt32(result) : -1;
                            }

                            // Si la sensibilidad no existe, insertarla y obtener su nuevo id
                            if (idSensibilidad == -1)
                            {
                                string insertSensQuery = "INSERT INTO Sensibilidad (nombre) OUTPUT INSERTED.idSensibilidad VALUES (@sensibilidad);";
                                using (SqlCommand insertSensCommand = new SqlCommand(insertSensQuery, mismetodos.GetConexion()))
                                {
                                    insertSensCommand.Parameters.AddWithValue("@sensibilidad", sensibilidadTrim);
                                    idSensibilidad = (int)insertSensCommand.ExecuteScalar();
                                }
                            }

                            // Insertar en Mascota_Sensibilidad
                            string insertMascotaSensQuery = "INSERT INTO Mascota_Sensibilidad (idMascota, idSensibilidad) VALUES (@idMascota, @idSensibilidad);";
                            using (SqlCommand insertMascotaSensCommand = new SqlCommand(insertMascotaSensQuery, mismetodos.GetConexion()))
                            {
                                insertMascotaSensCommand.Parameters.AddWithValue("@idMascota", idMascota);
                                insertMascotaSensCommand.Parameters.AddWithValue("@idSensibilidad", idSensibilidad);
                                insertMascotaSensCommand.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Datos de la mascota y sensibilidades actualizados correctamente.", "Éxito");
                        parentForm.formularioHijo(new MascotasConsultar(parentForm, idMascota));

                        string[] alergias = richTextBox2.Text.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string alergia in alergias)
                        {
                            string alergiastrim = alergia.Trim();

                            string getIdQuery = "SELECT idAlergia FROM Alergia WHERE nombre = @alergia;";
                            int idAlergia;

                            using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, mismetodos.GetConexion()))
                            {
                                getIdCommand.Parameters.AddWithValue("@alergia", alergiastrim);
                                var result = getIdCommand.ExecuteScalar();
                                idAlergia = result != null ? Convert.ToInt32(result) : -1;
                            }

                            // Si la sensibilidad no existe, insertarla y obtener su nuevo id
                            if (idAlergia == -1)
                            {
                                string insertSensQuery = "INSERT INTO Alergia (nombre) OUTPUT INSERTED.idAlergia VALUES (@alergia);";
                                using (SqlCommand insertSensCommand = new SqlCommand(insertSensQuery, mismetodos.GetConexion()))
                                {
                                    insertSensCommand.Parameters.AddWithValue("@alergia", alergiastrim);
                                    idAlergia = (int)insertSensCommand.ExecuteScalar();
                                }
                            }

                            // Insertar en Mascota_Sensibilidad
                            string insertMascotaSensQuery = "INSERT INTO Mascota_Alergia (idMascota, idAlergia) VALUES (@idMascota, @idAlergia);";
                            using (SqlCommand insertMascotaSensCommand = new SqlCommand(insertMascotaSensQuery, mismetodos.GetConexion()))
                            {
                                insertMascotaSensCommand.Parameters.AddWithValue("@idMascota", idMascota);
                                insertMascotaSensCommand.Parameters.AddWithValue("@idAlergia", idAlergia);
                                insertMascotaSensCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la mascota para actualizar.", "Información");
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


        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasConsultar(parentForm, idMascota)); // Pasamos la referencia de Form1 a 
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
        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es "Enter"
            if (e.KeyCode == Keys.Enter)
            {
                // Obtener el texto ingresado por el usuario
                
                string nuevaEspecie = ValidarYFormatearTexto(comboBox2.Text);

                // Verificar si la especie ya existe en la base de datos
                if (!mismetodos.Existe("SELECT COUNT(*) FROM raza WHERE nombre = @nombre", nuevaEspecie))
                {
                    // Preguntar al usuario si desea crear la nueva especie
                    DialogResult result = MessageBox.Show(
                        $"La raza '{nuevaEspecie}' no existe. ¿Desea crearla?",
                        "Crear nueva raza",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    // Si el usuario elige "Sí", insertar la nueva especie en la base de datos
                    if (result == DialogResult.Yes)
                    {
                        parentForm.formularioHijo(new MascotasAgregarRaza(parentForm, nuevaEspecie, idMascota,nombreMascota));
                    }
                }
                else
                {

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasAgregarSensibilidad (parentForm,"Mascota", idMascota));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasAgregarAlergia(parentForm, "Mascota",idMascota));
        }
    }
}
