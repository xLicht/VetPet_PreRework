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

namespace VetPet_.Angie.Mascotas
{
    public partial class MascotasAgregarAlergia : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodos = new Mismetodos();
        string tipo = "";
        int idMascota = 0;
        private Form1 parentForm;
        public MascotasAgregarAlergia(Form1 parent, string tipo, int idMascota)
        {
            InitializeComponent();
            this.Load += MascotasAgregarAlergia_Load;       // Evento Load
            this.Resize += MascotasAgregarAlergia_Resize;   // Evento Resize
            this.tipo = tipo;
            this.idMascota = idMascota;
            parentForm = parent;  // Guardamos la referencia de Form
            Cargar();
        }
        public MascotasAgregarAlergia(Form1 parent, string tipo)
        {
            InitializeComponent();
            this.Load += MascotasAgregarAlergia_Load;       // Evento Load
            this.Resize += MascotasAgregarAlergia_Resize;   // Evento Resize
            this.tipo = tipo;
            parentForm = parent;  // Guardamos la referencia de Form
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
                                    label5.Visible =false;
                                    richTextBox1.Visible = false;
                                    comboBox1.Items.Add(readerEsp["nombre"].ToString());
                                }
                            }
                        }
                        string queryEsp2 = "SELECT nombre FROM Alergia ORDER BY nombre";
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
                        string queryEsp3 = "SELECT nombre FROM Alergia ORDER BY nombre";
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
                        string queryE = "SELECT nombre FROM Alergia ORDER BY nombre";
                        using (SqlCommand comandoEsp = new SqlCommand(queryE, mismetodos.GetConexion()))
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
        private void MascotasAgregarAlergia_Load(object sender, EventArgs e)
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

        private void MascotasAgregarAlergia_Resize(object sender, EventArgs e)
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
        public void AgregarAlergia(string nombre, string descripcion, string tipo, string nombreSeleccionado)
        {
            try
            {
                mismetodos.AbrirConexion();
                switch (tipo)
                {
                    case "Especie":
                        try
                        {
                            // Obtener el idAlergia basado en el nombre de la alergia seleccionada
                            string queryAlergia = "SELECT idAlergia FROM Alergia WHERE nombre = @nombreAlergia;";
                            int idAlergia;

                            using (SqlCommand obtenerIdAlergiaCommand = new SqlCommand(queryAlergia, mismetodos.GetConexion()))
                            {
                                obtenerIdAlergiaCommand.Parameters.AddWithValue("@nombreAlergia", nombreSeleccionado);
                                idAlergia = (int)obtenerIdAlergiaCommand.ExecuteScalar();
                            }

                            // Obtener el idEspecie basado en el nombre de la especie seleccionada
                            string obtenerIdEspecieQuery = "SELECT idEspecie FROM Especie WHERE nombre = @nombreEspecie;";
                            int idEspecie;

                            using (SqlCommand obtenerIdEspecieCommand = new SqlCommand(obtenerIdEspecieQuery, mismetodos.GetConexion()))
                            {
                                obtenerIdEspecieCommand.Parameters.AddWithValue("@nombreEspecie", nombre);
                                idEspecie = (int)obtenerIdEspecieCommand.ExecuteScalar();
                            }

                            // Insertar en la tabla Especie_Alergia
                            string insertEspecieAlergiaQuery = "INSERT INTO Especie_Alergia (idEspecie, idAlergia) VALUES (@idEspecie, @idAlergia);";

                            using (SqlCommand insertEspecieAlergiaCommand = new SqlCommand(insertEspecieAlergiaQuery, mismetodos.GetConexion()))
                            {
                                insertEspecieAlergiaCommand.Parameters.AddWithValue("@idEspecie", idEspecie);
                                insertEspecieAlergiaCommand.Parameters.AddWithValue("@idAlergia", idAlergia);
                                insertEspecieAlergiaCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Alergia asociada a la especie correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al asociar la alergia a la especie: {ex.Message}");
                        }
                        break;

                    case "Raza":
                        try
                        {
                            // Obtener el idAlergia basado en el nombre de la alergia seleccionada
                            string queryAlergia = "SELECT idAlergia FROM Alergia WHERE nombre = @nombreAlergia;";
                            int idAlergia;

                            using (SqlCommand obtenerIdAlergiaCommand = new SqlCommand(queryAlergia, mismetodos.GetConexion()))
                            {
                                obtenerIdAlergiaCommand.Parameters.AddWithValue("@nombreAlergia", nombreSeleccionado);
                                idAlergia = (int)obtenerIdAlergiaCommand.ExecuteScalar();
                            }

                            // Obtener el idRaza basado en el nombre de la raza seleccionada
                            string obtenerIdRazaQuery = "SELECT idRaza FROM Raza WHERE nombre = @nombreRaza;";
                            int idRaza;

                            using (SqlCommand obtenerIdRazaCommand = new SqlCommand(obtenerIdRazaQuery, mismetodos.GetConexion()))
                            {
                                obtenerIdRazaCommand.Parameters.AddWithValue("@nombreRaza", nombre);
                                idRaza = (int)obtenerIdRazaCommand.ExecuteScalar();
                            }

                            // Insertar en la tabla Raza_Alergia
                            string insertRazaAlergiaQuery = "INSERT INTO Raza_Alergia (idRaza, idAlergia) VALUES (@idRaza, @idAlergia);";

                            using (SqlCommand insertRazaAlergiaCommand = new SqlCommand(insertRazaAlergiaQuery, mismetodos.GetConexion()))
                            {
                                insertRazaAlergiaCommand.Parameters.AddWithValue("@idRaza", idRaza);
                                insertRazaAlergiaCommand.Parameters.AddWithValue("@idAlergia", idAlergia);
                                insertRazaAlergiaCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Alergia asociada a la raza correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al asociar la alergia a la raza: {ex.Message}");
                        }
                        break;
                    case "Mascota":
                        try
                        {
                            // Obtener el idSensibilidad basado en el nombre de la sensibilidad seleccionada
                            string querySensibilidad = "SELECT idAlergia FROM Alergia WHERE nombre = @nombre;";
                            int idAlergia;

                            using (SqlCommand obtenerIdSensibilidadCommand = new SqlCommand(querySensibilidad, mismetodos.GetConexion()))
                            {
                                obtenerIdSensibilidadCommand.Parameters.AddWithValue("@nombre", nombreSeleccionado);
                                idAlergia = (int)obtenerIdSensibilidadCommand.ExecuteScalar();
                            }

                            // Insertar en la tabla Especie_Sensibilidad
                            string insertEspecieSensibilidadQuery = "INSERT INTO Mascota_Alergia (idMascota, idAlergia) VALUES (@idMascota, @idAlergia);";

                            using (SqlCommand insertEspecieSensibilidadCommand = new SqlCommand(insertEspecieSensibilidadQuery, mismetodos.GetConexion()))
                            {
                                insertEspecieSensibilidadCommand.Parameters.AddWithValue("@idMascota", idMascota);
                                insertEspecieSensibilidadCommand.Parameters.AddWithValue("@idAlergia", idAlergia);
                                insertEspecieSensibilidadCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Sensibilidad asociada a la mascota correctamente.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al asociar la sensibilidad a la especie: {ex.Message}");
                        }
                        break;
                    case "":
                        try
                        {
                            // Insertar en la tabla Raza_Alergia
                            string insertAlergiaQuery = "INSERT INTO Alergia (nombre, descripcion) OUTPUT INSERTED.idAlergia VALUES (@nombre, @descripcion);";
                            int idAlergia;

                            using (SqlCommand insertAlergiaCommand = new SqlCommand(insertAlergiaQuery, mismetodos.GetConexion()))
                            {
                                insertAlergiaCommand.Parameters.AddWithValue("@nombre", nombre);
                                insertAlergiaCommand.Parameters.AddWithValue("@descripcion", descripcion);
                                idAlergia = (int)insertAlergiaCommand.ExecuteScalar(); // Obtener el idAlergia generado
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al asociar la alergia a la raza: {ex.Message}");
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
        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasVerAlergias(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tipo =label4.Text;  // "Especie", "Raza" o ""
            string descripcion = richTextBox1.Text;
            string nombreSeleccionado = comboBox2.SelectedItem?.ToString(); // Nombre seleccionado en el ComboBox

            if (string.IsNullOrEmpty(nombreSeleccionado))
            {
                string nombre = comboBox2.Text;
                AgregarAlergia(nombre,descripcion, "", nombreSeleccionado);
            }
            else
            {
                string nombre = comboBox1.SelectedItem?.ToString();
                AgregarAlergia(nombre,"", tipo, nombreSeleccionado);
            }
            parentForm.formularioHijo(new MascotasVerAlergias(parentForm)); // Pasamos la referencia de Form1 a
            if (tipo == "Mascota")
            {
                parentForm.formularioHijo(new MascotasModificar(parentForm, idMascota)); // Pasamos la referencia de Form1 a
            }
        }
    }
}
