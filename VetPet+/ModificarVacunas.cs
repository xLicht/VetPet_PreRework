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
using VetPet_;

namespace VetPet_
{
    public partial class ModificarVacunas : FormPadre
    {
        int identificador;
        string cirugia;
        public ModificarVacunas()
        {
            InitializeComponent();
        }
        public ModificarVacunas(Form1 parent,int Id, string idcirugia)
        {
            InitializeComponent();
            parentForm = parent;// Guardamos la referencia del formulario principal
            identificador = Id;
            cirugia = idcirugia;
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            int idServicio = conexion.ObtenerId("Vacunas", "ServicioPadre");
            parentForm.formularioHijo(new ListaVacunas(parentForm, idServicio));
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Consulta para eliminar el registro basado en el ID
            string query = "UPDATE Vacuna SET estado = 'B' WHERE idVacuna = @ID";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Agregar el parámetro de ID a la consulta
                    cmd.Parameters.AddWithValue("@ID", identificador);

                    // Ejecutar la consulta de eliminación
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Registro eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un registro con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el registro: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;
            DataRowView selectedRow2 = comboBox2.SelectedItem as DataRowView;

            string nombreServicioHijo = selectedRow["nombre"].ToString();
            string nombreViaAdmin = selectedRow2["nombre"].ToString();
            string tabla1 = "ViaAdministracion";
            string tabla2 = "ServicioEspecificoHijo";
            

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            int idViaAdmin = conexion.ObtenerId(nombreViaAdmin, tabla1);
            int idSerEsp = conexion.ObtenerId(nombreServicioHijo, tabla2);
            string queryUpdate = "UPDATE Vacuna SET nombre = @NOM, descripcion = @DES, precio = @PRE, intervalo = @INT, " +
                "frecuencia = @FRE, edadMinima =@EDM, idServicioEspecificoHijo = @ISH, idViaAdministracion = @IVA " +
                "WHERE idVacuna = @ID";

            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexion.GetConexion()))
            {
                try
                {
                    // Obtener valores de los controles
                    string nombre = TxtNombre.Text;
                    string descripcion = richTextBox1.Text.Replace("\r", "").Replace("\n", "");

                    // Convertir valores numéricos
                    if (!decimal.TryParse(TxtPrecio.Text, out decimal precio))
                    {
                        MessageBox.Show("Ingrese un precio válido.");
                        return;
                    }
                    string intervalo = TxtIntDosis.Text;
                    string frecuencia = TxtFrecVacuna.Text;
                    string edadMinima = TxtEdadAplicacion.Text;




                    // Agregar parámetros a la consulta de actualización
                    cmdUpdate.Parameters.AddWithValue("@NOM", nombre);
                    cmdUpdate.Parameters.AddWithValue("@DES", descripcion);
                    cmdUpdate.Parameters.AddWithValue("@PRE", precio);
                    cmdUpdate.Parameters.AddWithValue("@INT", intervalo);
                    cmdUpdate.Parameters.AddWithValue("@FRE", frecuencia); // Aquí pasamos el idServicioEspecificoHijo
                    cmdUpdate.Parameters.AddWithValue("@EDM", edadMinima);
                    cmdUpdate.Parameters.AddWithValue("@ISH", idSerEsp);
                    cmdUpdate.Parameters.AddWithValue("@IVA", idViaAdmin);
                    cmdUpdate.Parameters.AddWithValue("@ID", identificador); // Este es el ID del registro a actualizar

                    // Ejecutar la consulta de actualización
                    int filasAfectadas = cmdUpdate.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Registro actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un registro con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el registro: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }

            }

        }
        

        private void cargarCombobox()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string id = cirugia;
            string query = "SELECT nombre FROM ServicioEspecificoHijo WHERE idServicioPadre = "+id+"";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Crear un SqlDataAdapter con la conexión correcta
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar el DataTable como fuente de datos
                    comboBox1.DataSource = dt;

                    // Asegúrate de que DisplayMember coincida con el nombre exacto de la columna en tu DataTable
                    comboBox1.DisplayMember = "nombre";  // Nombre de la columna que quieres mostrar en el ComboBox
                    comboBox1.ValueMember = "nombre";    // El valor del ComboBox será el mismo campo
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
        private void cargarComboboxViaAdmin()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT nombre FROM ViaAdministracion";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Crear un SqlDataAdapter con la conexión correcta
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar el DataTable como fuente de datos
                    comboBox2.DataSource = dt;

                    // Asegúrate de que DisplayMember coincida con el nombre exacto de la columna en tu DataTable
                    comboBox2.DisplayMember = "nombre";  // Nombre de la columna que quieres mostrar en el ComboBox
                    comboBox2.ValueMember = "nombre";    // El valor del ComboBox será el mismo campo
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
        private void ModificarVacunas_Load(object sender, EventArgs e)
        {
            cargarCombobox();
            cargarComboboxViaAdmin();
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            string query = "SELECT nombre, descripcion, precio, intervalo, frecuencia, edadMinima FROM Vacuna WHERE idVacuna = @ID";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@ID", identificador);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) // Si hay resultados
                    {
                        TxtNombre.Text = reader["nombre"].ToString();
                        richTextBox1.Text = reader["descripcion"].ToString();
                        TxtPrecio.Text = Convert.ToDecimal(reader["precio"]).ToString("0.00");
                        TxtIntDosis.Text = reader["intervalo"].ToString();
                        TxtFrecVacuna.Text = reader["frecuencia"].ToString();
                        TxtEdadAplicacion.Text = reader["edadMinima"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los datos: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
    }
}
