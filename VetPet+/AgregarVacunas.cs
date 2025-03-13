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
    public partial class AgregarVacunas : FormPadre
    {
        public AgregarVacunas()
        {
            InitializeComponent();
        }
        public AgregarVacunas(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaVacunas(parentForm));
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;
            DataRowView selectedRow2 = comboBox2.SelectedItem as DataRowView;

            string nombreServicioHijo = selectedRow["nombre"].ToString();
            string nombreViaAdmin = selectedRow2["nombre"].ToString();
            string tabla1 = "ViaAdministracion";
            string tabla2 = "ServicioEspecificoHijo";
            int idViaAdmin = ObtenerId(nombreViaAdmin, tabla1);
            int idSerEsp = ObtenerId(nombreServicioHijo, tabla2);

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string queryUpdate = "INSERT INTO Vacuna (nombre, descripcion, precio, intervalo, frecuencia, edadMinima, idServicioEspecificoHijo, idViaAdministracion)" +
                " VALUES (@NOM, @DES, @PRE, @INT, @FRE, @EDM, @ISH, @IVA)";

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
        private int ObtenerId(string nombre, string tabla)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT id" + tabla + " FROM " + tabla + " WHERE nombre = @NombreServicioHijo";
            int idServicioHijo = 0;

            using (SqlCommand cmd = new SqlCommand(queryGetIdServicioHijo, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@NombreServicioHijo", nombre);
                    object result = cmd.ExecuteScalar(); // Ejecutar la consulta y obtener el primer valor de la primera columna

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el servicio hijo con ese nombre.");

                    }
                    return idServicioHijo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el registro: " + ex.Message);
                    return idServicioHijo;
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
            string id = "3";
            string query = "SELECT nombre FROM ServicioEspecificoHijo WHERE idServicioPadre = " + id + "";

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

        private void AgregarVacunas_Load(object sender, EventArgs e)
        {
            cargarComboboxViaAdmin();
            cargarCombobox();
        }
    }
}
