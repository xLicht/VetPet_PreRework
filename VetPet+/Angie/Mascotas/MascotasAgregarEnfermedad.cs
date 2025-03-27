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

namespace VetPet_.Angie.Mascotas
{
    public partial class MascotasAgregarEnfermedad : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodos = new Mismetodos();
        string tipo;
        int idMascota;
        private Form1 parentForm;
        public MascotasAgregarEnfermedad(Form1 parent, string tipo)
        {
            InitializeComponent();
            this.Load += MascotasAgregarEnfermedad_Load;       // Evento Load
            this.Resize += MascotasAgregarEnfermedad_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form
            this.tipo = tipo;
            Cargar();
        }
        public MascotasAgregarEnfermedad(Form1 parent, string tipo, int idMascota)
        {
            InitializeComponent();
            this.Load += MascotasAgregarEnfermedad_Load;       // Evento Load
            this.Resize += MascotasAgregarEnfermedad_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form
            this.tipo = tipo;
            this.idMascota = idMascota;
            Cargar();
        }
        private void Cargar()
        {
            try
            {
                mismetodos.AbrirConexion();

                switch (tipo)
                {
                    case "Especie":
                        string queryEsp = "SELECT nombre FROM Especie ORDER BY nombre";
                        using (SqlCommand comandoEsp = new SqlCommand(queryEsp, mismetodos.GetConexion()))
                        {
                            using (SqlDataReader readerEsp = comandoEsp.ExecuteReader())
                            {
                                // Limpiar el ComboBox antes de agregar nuevos elementos
                                comboBox1.Items.Clear();

                                while (readerEsp.Read())
                                {
                                    label4.Text = "Especie";
                                    label5.Visible = false;
                                    richTextBox1.Visible = false;
                                    comboBox1.Items.Add(readerEsp["nombre"].ToString());
                                }
                            }
                        }
                        string queryEsp2 = "SELECT nombre FROM Enfermedad ORDER BY nombre";
                        using (SqlCommand comandoEsp = new SqlCommand(queryEsp2, mismetodos.GetConexion()))
                        {
                            using (SqlDataReader readerEsp = comandoEsp.ExecuteReader())
                            {
                                // Limpiar el ComboBox antes de agregar nuevos elementos
                                comboBox2.Items.Clear();

                                while (readerEsp.Read())
                                {
                                    comboBox2.Items.Add(readerEsp["nombre"].ToString());
                                }
                            }
                        }
                        break;
                    case "Raza":
                        string queryEsp1 = "SELECT nombre FROM Raza ORDER BY nombre";
                        using (SqlCommand comandoEsp = new SqlCommand(queryEsp1, mismetodos.GetConexion()))
                        {
                            using (SqlDataReader readerEsp = comandoEsp.ExecuteReader())
                            {
                                // Limpiar el ComboBox antes de agregar nuevos elementos
                                comboBox1.Items.Clear();

                                while (readerEsp.Read())
                                {
                                    label4.Text = "Raza";

                                    label5.Visible = false;
                                    richTextBox1.Visible = false;
                                    comboBox1.Items.Add(readerEsp["nombre"].ToString());
                                }
                            }
                        }
                        string queryEsp3 = "SELECT nombre FROM Enfermedad ORDER BY nombre";
                        using (SqlCommand comandoEsp = new SqlCommand(queryEsp3, mismetodos.GetConexion()))
                        {
                            using (SqlDataReader readerEsp = comandoEsp.ExecuteReader())
                            {
                                // Limpiar el ComboBox antes de agregar nuevos elementos
                                comboBox2.Items.Clear();

                                while (readerEsp.Read())
                                {
                                    comboBox2.Items.Add(readerEsp["nombre"].ToString());
                                }
                            }
                        }
                        break;
                    case "Mascota":
                        string queryy = "SELECT nombre FROM Mascota WHERE idMascota = @idMascota;";

                        using (SqlCommand obtenernombre = new SqlCommand(queryy, mismetodos.GetConexion()))
                        {
                            obtenernombre.Parameters.AddWithValue("@idMascota", idMascota);

                            using (SqlDataReader reader = obtenernombre.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string nombreMascota = reader["nombre"].ToString();
                                    label5.Visible = false;
                                    richTextBox1.Visible = false;
                                    label4.Text = "Mascota";
                                    comboBox1.Text = nombreMascota;
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró la mascota con el ID proporcionado.");
                                }
                            }
                        }
                        string query1 = "SELECT nombre FROM Enfermedad ORDER BY nombre";
                        using (SqlCommand comandoEsp = new SqlCommand(query1, mismetodos.GetConexion()))
                        {
                            using (SqlDataReader readerEsp = comandoEsp.ExecuteReader())
                            {
                                // Limpiar el ComboBox antes de agregar nuevos elementos
                                comboBox2.Items.Clear();

                                while (readerEsp.Read())
                                {
                                    comboBox2.Items.Add(readerEsp["nombre"].ToString());
                                }
                            }
                        }
                        break;
                    case "":
                        label4.Visible = false;
                        comboBox1.Visible = false;
                        break;

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
        private void MascotasAgregarEnfermedad_Load(object sender, EventArgs e)
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

        private void MascotasAgregarEnfermedad_Resize(object sender, EventArgs e)
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

        public void AgregarEnfermedad(string nombre, string descripcion, string tipo, string nombreSeleccionado)
        {
            try
            {
                mismetodos.AbrirConexion();
                switch (tipo)
                {
                    case "Especie":
                        try
                        {
                            // Obtener el idSensibilidad basado en el nombre de la sensibilidad seleccionada
                            string querySensibilidad = "SELECT idEnfermedad FROM Enfermedad WHERE nombre = @nombreEnfermedad;";
                            int idEnfermedad;

                            using (SqlCommand obtenerIdSensibilidadCommand = new SqlCommand(querySensibilidad, mismetodos.GetConexion()))
                            {
                                obtenerIdSensibilidadCommand.Parameters.AddWithValue("@nombreSensibilidad", nombreSeleccionado);
                                idEnfermedad = (int)obtenerIdSensibilidadCommand.ExecuteScalar();
                            }

                            // Obtener el idEspecie basado en el nombre de la especie seleccionada
                            string obtenerIdEspecieQuery = "SELECT idEspecie FROM Especie WHERE nombre = @nombreEspecie;";
                            int idEspecie;

                            using (SqlCommand obtenerIdEspecieCommand = new SqlCommand(obtenerIdEspecieQuery, mismetodos.GetConexion()))
                            {
                                obtenerIdEspecieCommand.Parameters.AddWithValue("@nombreEspecie", nombre);
                                idEspecie = (int)obtenerIdEspecieCommand.ExecuteScalar();
                            }

                            // Insertar en la tabla Especie_Sensibilidad
                            string insertEspecieSensibilidadQuery = "INSERT INTO Especie_Enfermedad (idEspecie, idEnfermedad) VALUES (@idEspecie, @idEnfermedad);";

                            using (SqlCommand insertEspecieSensibilidadCommand = new SqlCommand(insertEspecieSensibilidadQuery, mismetodos.GetConexion()))
                            {
                                insertEspecieSensibilidadCommand.Parameters.AddWithValue("@idEspecie", idEspecie);
                                insertEspecieSensibilidadCommand.Parameters.AddWithValue("@idEnfermedad", idEnfermedad);
                                insertEspecieSensibilidadCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Enfermedad asociada a la especie correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al asociar la enfermedad a la especie: {ex.Message}");
                        }
                        break;
                    case "Mascota":
                        try
                        {
                            // Obtener el idSensibilidad basado en el nombre de la sensibilidad seleccionada
                            string querySensibilidad = "SELECT idEnfermedad FROM Enfermedad WHERE nombre = @nombreEnfermedad;";
                            int idEnfermedad;

                            using (SqlCommand obtenerIdSensibilidadCommand = new SqlCommand(querySensibilidad, mismetodos.GetConexion()))
                            {
                                obtenerIdSensibilidadCommand.Parameters.AddWithValue("@nombreEnfermedad", nombreSeleccionado);
                                idEnfermedad = (int)obtenerIdSensibilidadCommand.ExecuteScalar();
                            }

                            // Insertar en la tabla Especie_Sensibilidad
                            string insertEspecieSensibilidadQuery = "INSERT INTO Mascota_Enfermedad (idMascota, idEnfermedad) VALUES (@idMascota, @idEnfermedad);";

                            using (SqlCommand insertEspecieSensibilidadCommand = new SqlCommand(insertEspecieSensibilidadQuery, mismetodos.GetConexion()))
                            {
                                insertEspecieSensibilidadCommand.Parameters.AddWithValue("@idMascota", idMascota);
                                insertEspecieSensibilidadCommand.Parameters.AddWithValue("@idSensibilidad", idEnfermedad);
                                insertEspecieSensibilidadCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Enfermedad asociada a la mascota correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al asociar la Enfermedad a la especie: {ex.Message}");
                        }
                        break;

                    case "Raza":
                        try
                        {
                            // Obtener el idSensibilidad basado en el nombre de la sensibilidad seleccionada
                            string querySensibilidad = "SELECT idEnfermedad FROM Enfermedad WHERE nombre = @nombreEnfermedad;";
                            int idEnfermedad;

                            using (SqlCommand obtenerIdSensibilidadCommand = new SqlCommand(querySensibilidad, mismetodos.GetConexion()))
                            {
                                obtenerIdSensibilidadCommand.Parameters.AddWithValue("@nombreEnfermedad", nombreSeleccionado);
                                idEnfermedad = (int)obtenerIdSensibilidadCommand.ExecuteScalar();
                            }

                            // Obtener el idRaza basado en el nombre de la raza seleccionada
                            string obtenerIdRazaQuery = "SELECT idRaza FROM Raza WHERE nombre = @nombreRaza;";
                            int idRaza;

                            using (SqlCommand obtenerIdRazaCommand = new SqlCommand(obtenerIdRazaQuery, mismetodos.GetConexion()))
                            {
                                obtenerIdRazaCommand.Parameters.AddWithValue("@nombreRaza", nombre);
                                idRaza = (int)obtenerIdRazaCommand.ExecuteScalar();
                            }

                            // Insertar en la tabla Raza_Sensibilidad
                            string insertRazaSensibilidadQuery = "INSERT INTO Raza_Enfermedad (idRaza, idEnfermedad) VALUES (@idRaza, @idEnfermedad);";

                            using (SqlCommand insertRazaSensibilidadCommand = new SqlCommand(insertRazaSensibilidadQuery, mismetodos.GetConexion()))
                            {
                                insertRazaSensibilidadCommand.Parameters.AddWithValue("@idRaza", idRaza);
                                insertRazaSensibilidadCommand.Parameters.AddWithValue("@idSensibilidad", idEnfermedad);
                                insertRazaSensibilidadCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Sensibilidad asociada a la raza correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al asociar la sensibilidad a la raza: {ex.Message}");
                        }
                        break;

                    case "":
                        try
                        {
                            // Insertar en la tabla Sensibilidad
                            string insertSensibilidadQuery = "INSERT INTO Enfermedad (nombre, descripcion) OUTPUT INSERTED.idEnfermedad VALUES (@nombre, @descripcion);";
                            int idEnfermedad;

                            using (SqlCommand insertSensibilidadCommand = new SqlCommand(insertSensibilidadQuery, mismetodos.GetConexion()))
                            {
                                insertSensibilidadCommand.Parameters.AddWithValue("@nombre", nombre);
                                insertSensibilidadCommand.Parameters.AddWithValue("@descripcion", descripcion);
                                idEnfermedad = (int)insertSensibilidadCommand.ExecuteScalar(); // Obtener el idSensibilidad generado
                            }

                            MessageBox.Show("Sensibilidad agregada correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al agregar la sensibilidad: {ex.Message}");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar la sensibilidad: {ex.Message}");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tipo = label4.Text;  // "Especie", "Raza" o ""
            string descripcion = richTextBox1.Text;
            string nombreSeleccionado = comboBox2.SelectedItem?.ToString(); // Nombre seleccionado en el ComboBox

            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                string nombre = comboBox2.Text;
                AgregarEnfermedad(nombre, descripcion, "", nombreSeleccionado);
            }
            else
            {
                string nombre = comboBox1.Text;
                AgregarEnfermedad(nombre, "", tipo, nombreSeleccionado);
            }
            parentForm.formularioHijo(new MascotasVerEnfermedades(parentForm)); // Pasamos la referencia de Form1 a

            if (tipo == "Mascota")
            {
                parentForm.formularioHijo(new MascotasModificar(parentForm, idMascota)); // Pasamos la referencia de Form1 a
            }
        }
    }
}

