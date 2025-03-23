using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VetPet_
{
    internal class conexionAlex
    {
        public readonly string cadenaConexion = @"Data Source=DESKTOP-GQ6Q9HG\SQLEXPRESS;Initial Catalog=VetPetPlus;Integrated Security=True;";
        private SqlConnection conexion;

        System.Windows.Forms.ComboBox comboBox = new System.Windows.Forms.ComboBox();
        public conexionAlex()
        {
            conexion = new SqlConnection(cadenaConexion);
        }

        public void AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
        }
        public int ObtenerId(string nombre, string tabla)
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

        public void CargarTipodeServicio(DataGridView datg1, string IdServicioPadre)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT     sp.nombre AS TipoDeServicio, te.nombre AS TipoEmpleado\t  FROM ServicioEspecificoHijo" +
                " sp INNER JOIN TipoEmpleado te ON sp.idtipoempleado = \r\n           " +
                "     te.idtipoEmpleado WHERE\r\n                idServicioPadre = "+ IdServicioPadre + " AND sp.estado = 'A';";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    datg1.Rows.Clear();
                    datg1.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    datg1.DataSource = dt;

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
        public void GuardarTipoServicio(TextBox TxtNombre, RichTextBox richTextBox1,int idServicio,int tipoEmpleado)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "INSERT INTO ServicioEspecificoHijo (nombre, descripcion, idServicioPadre,idTipoEmpleado) VALUES (@NOM, @DES, @ISP, @ITP);";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Primero obtenemos los valores
                    string Nombre = TxtNombre.Text;
                    string Descripcion = richTextBox1.Text.Replace("\r", "").Replace("\n", "");


                    // Agregamos los parámetros
                    cmd.Parameters.AddWithValue("@NOM", Nombre);
                    cmd.Parameters.AddWithValue("@DES", Descripcion);
                    cmd.Parameters.AddWithValue("@ISP", idServicio);
                    cmd.Parameters.AddWithValue("@ITP", tipoEmpleado);

                    // Ejecutamos la consulta
                    cmd.ExecuteNonQuery();  // Cambié ExecuteReader por ExecuteNonQuery

                    MessageBox.Show("Nuevo Tipo de Servicio Registrado");
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
        public void CargarInformaciondeServicio(DataGridView datg2, string IdServicioPadre)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT SEN.nombre, SEN.precio, SEN.duracion, SEN.descripcion \r\nFROM ServicioEspecificoNieto " +
                "SEN\r\nINNER JOIN ServicioEspecificoHijo SEH\r\nON SEN.idServicioEspecificoHijo = " +
                "SEH.idServicioEspecificoHijo\r\nWHERE SEH.idServicioPadre = " + IdServicioPadre + " AND SEN.estado = 'A' AND SEH.estado = 'A'";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    datg2.Rows.Clear();
                    datg2.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    datg2.DataSource = dt;

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
        public void Eliminar(int identificador)
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
        public void cargarCombobox(ComboBox comb1,string idSrP)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT nombre FROM ServicioEspecificoHijo WHERE idServicioPadre = "+idSrP+"";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Crear un SqlDataAdapter con la conexión correcta
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar el DataTable como fuente de datos
                    comb1.DataSource = dt;

                    // Asegúrate de que DisplayMember coincida con el nombre exacto de la columna en tu DataTable
                    comb1.DisplayMember = "nombre";  // Nombre de la columna que quieres mostrar en el ComboBox
                    comb1.ValueMember = "nombre";    // El valor del ComboBox será el mismo campo
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
        public void Buscar(DataGridView dt1, TextBox TxtBuscar, string idPadre)
        {
            dt1.DataSource = null;
            dt1.Rows.Clear();
            dt1.Columns.Clear();
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string patron = TxtBuscar.Text;
            string query = " SELECT Se.nombre AS TipoDeServicio,te.nombre AS TipoEmpleado FROM ServicioEspecificoHijo Se  INNER JOIN TipoEmpleado te ON Se.idtipoempleado = \r\n            " +
                "    te.idtipoEmpleado   WHERE idServicioPadre =" + idPadre + " AND Se.nombre LIKE '%" + patron + "%' AND Se.estado = 'A';";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt1.Rows.Clear();
                    dt1.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dt1.DataSource = dt;

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
        public void CerrarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }

        public SqlConnection GetConexion()
        {
            return conexion;
        }
    }
}
