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
    public partial class ModificarPLab : FormPadre
    {
        int identificador;
        public ModificarPLab()
        {
            InitializeComponent();
        }
        public ModificarPLab(Form1 parent,int Id)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
            identificador = Id;
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            
            parentForm.formularioHijo(new ListaPLab(parentForm));
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Consulta para eliminar el registro basado en el ID
            string query = "UPDATE ServicioEspecificoNieto SET estado = 'B' WHERE idServicioEspecificoNieto = @ID";

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

            string nombreServicioHijo = selectedRow["nombre"].ToString();
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT idServicioEspecificoHijo FROM ServicioEspecificoHijo WHERE nombre = @NombreServicioHijo";
            int idServicioHijo = 0;

            using (SqlCommand cmd = new SqlCommand(queryGetIdServicioHijo, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@NombreServicioHijo", nombreServicioHijo);
                    object result = cmd.ExecuteScalar(); // Ejecutar la consulta y obtener el primer valor de la primera columna

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el servicio hijo con ese nombre.");
                        return;
                    }

                    // Ahora que tenemos el idServicioHijo, proceder con la actualización del registro en la tabla ServicioEspecificoNieto

                    string queryUpdate = "UPDATE ServicioEspecificoNieto SET nombre = @NOM, descripcion = @DES, precio = @PRE, duracion = @DUR, idServicioEspecificoHijo = @ISH WHERE idServicioEspecificoNieto = @ID";

                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexion.GetConexion()))
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

                        if (!int.TryParse(TxtDuracion.Text, out int duracion))
                        {
                            MessageBox.Show("Ingrese una duración válida.");
                            return;
                        }

                        // Agregar parámetros a la consulta de actualización
                        cmdUpdate.Parameters.AddWithValue("@NOM", nombre);
                        cmdUpdate.Parameters.AddWithValue("@DES", descripcion);
                        cmdUpdate.Parameters.AddWithValue("@PRE", precio);
                        cmdUpdate.Parameters.AddWithValue("@DUR", duracion);
                        cmdUpdate.Parameters.AddWithValue("@ISH", idServicioHijo); // Aquí pasamos el idServicioEspecificoHijo
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
            string query = "SELECT nombre FROM ServicioEspecificoHijo WHERE idServicioPadre = 8";

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
        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal, y tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;  // Si es un carácter no permitido, no se procesa
            }

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && TxtPrecio.Text.Contains("."))
            {
                e.Handled = true;  // Si ya hay un punto decimal, no permite otro
            }
        }
        private void ModificarPLab_Load(object sender, EventArgs e)
        {
            cargarCombobox();
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            string query = "SELECT nombre, descripcion, precio, duracion FROM ServicioEspecificoNieto WHERE idServicioEspecificoNieto = @ID";

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
                        TxtDuracion.Text = reader["duracion"].ToString();
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
