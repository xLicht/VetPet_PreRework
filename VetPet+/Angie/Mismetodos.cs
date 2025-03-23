using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_.Angie.Mascotas
{
    internal class Mismetodos
    {
            // Definir las dos cadenas de conexión
            private readonly string cadenaConexion1 = @"Data Source=127.0.0.1;Initial Catalog=VetPetPlus;Integrated Security=True;";
            private readonly string cadenaConexion2 = @"Server=DESKTOP-7PPM2OB\SQLEXPRESS;Database=VetPetPlus;Integrated Security=True;";

            private SqlConnection conexion;
            private bool usarConexion1 = true; // Bandera para alternar entre las conexiones

            public Mismetodos()
            {
                // Inicializar la conexión con la primera cadena de conexión
                conexion = new SqlConnection(cadenaConexion1);
            }

        public void EliminarRazaEnCascada(int idRaza)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("EliminarRazaEnCascada", GetConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.AddWithValue("@idRaza", idRaza);

                    // Parámetro de salida
                    SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.NVarChar, -1);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);


                    cmd.ExecuteNonQuery();

                    // Obtener el resultado
                    string resultado = resultadoParam.Value.ToString();
                    MessageBox.Show(resultado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
            }
            finally 
            {
                CerrarConexion();   
            }
        }
        public void EliminarRegistro(string nombreTabla, string nombreColumna, string nombreRegistro)
        {
            try
            {
                AbrirConexion();
                string query = $@"
                DELETE FROM {nombreTabla}
                WHERE id{nombreColumna} = (SELECT id{nombreColumna} FROM {nombreColumna} WHERE nombre = @Nombre)";

                using (SqlCommand cmd = new SqlCommand(query, GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreRegistro);

                    // Ejecutar la consulta
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"{nombreColumna} eliminado correctamente de la base de datos.");
                    }
                    else
                    {
                        MessageBox.Show($"No se encontró el {nombreColumna} en la base de datos.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el {nombreColumna}: {ex.Message}");
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void CargarDatosGenerico(string nombreTabla, int id, Dictionary<string, Control> mapeoColumnasControles, ListBox listBoxSensibilidades = null, ListBox listBoxAlergias = null)
        {
            try
            {
                // Construir la consulta SQL de forma segura
                string query = $"SELECT {string.Join(", ", mapeoColumnasControles.Keys)} FROM {nombreTabla} WHERE id{nombreTabla} = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Asegurarse de que la conexión esté abierta
                    if (conexion.State != System.Data.ConnectionState.Open)
                    {
                        AbrirConexion();
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Recorrer el diccionario y cargar los datos en los controles
                            foreach (var columnaControl in mapeoColumnasControles)
                            {
                                string columna = columnaControl.Key;
                                Control control = columnaControl.Value;

                                int ordinal = reader.GetOrdinal(columna); // Obtener índice de la columna
                                if (!reader.IsDBNull(ordinal)) // Verificar si no es NULL
                                {
                                    if (control is System.Windows.Forms.TextBox txt)
                                    {
                                        txt.Text = reader[ordinal].ToString();
                                    }
                                    else if (control is System.Windows.Forms.RichTextBox rtb)
                                    {
                                        rtb.Text = reader[ordinal].ToString();
                                    }
                                    else if (control is System.Windows.Forms.ComboBox cmb)
                                    {
                                        cmb.Text = reader[ordinal].ToString();
                                    }
                                    // Agrega más controles aquí según sea necesario
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No se encontró un registro con el ID {id} en la tabla {nombreTabla}.");
                        }
                    }
                }

                // Cargar las sensibilidades asociadas a la especie
                if (listBoxSensibilidades != null && nombreTabla == "Especie")
                {
                    string querySensibilidades = @"
                SELECT s.nombre 
                FROM Sensibilidad s
                INNER JOIN Especie_Sensibilidad es ON s.idSensibilidad = es.idSensibilidad
                WHERE es.idEspecie = @ID";

                    using (SqlCommand cmdSensibilidades = new SqlCommand(querySensibilidades, conexion))
                    {
                        cmdSensibilidades.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader readerSensibilidades = cmdSensibilidades.ExecuteReader())
                        {
                            listBoxSensibilidades.Items.Clear(); // Limpiar el ListBox antes de agregar nuevos elementos

                            while (readerSensibilidades.Read())
                            {
                                listBoxSensibilidades.Items.Add(readerSensibilidades["nombre"].ToString());
                            }
                        }
                    }
                }

                if (listBoxSensibilidades != null && nombreTabla == "Raza")
                {
                    string querySensibilidades = @"
                SELECT s.nombre 
                FROM Sensibilidad s
                INNER JOIN Raza_Sensibilidad es ON s.idSensibilidad = es.idSensibilidad
                WHERE es.idRaza = @ID";

                    using (SqlCommand cmdSensibilidades = new SqlCommand(querySensibilidades, conexion))
                    {
                        cmdSensibilidades.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader readerSensibilidades = cmdSensibilidades.ExecuteReader())
                        {
                            listBoxSensibilidades.Items.Clear(); // Limpiar el ListBox antes de agregar nuevos elementos

                            while (readerSensibilidades.Read())
                            {
                                listBoxSensibilidades.Items.Add(readerSensibilidades["nombre"].ToString());
                            }
                        }
                    }
                }

                if (listBoxAlergias != null && nombreTabla == "Especie")
                {
                    string queryAlergias = @"
                SELECT s.nombre 
                FROM Alergia s
                INNER JOIN Especie_Alergia es ON s.idAlergia = es.idAlergia
                WHERE es.idEspecie = @ID";

                    using (SqlCommand cmdSensibilidades = new SqlCommand(queryAlergias, conexion))
                    {
                        cmdSensibilidades.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader readerSensibilidades = cmdSensibilidades.ExecuteReader())
                        {
                            listBoxAlergias.Items.Clear(); // Limpiar el ListBox antes de agregar nuevos elementos

                            while (readerSensibilidades.Read())
                            {
                                listBoxAlergias.Items.Add(readerSensibilidades["nombre"].ToString());
                            }
                        }
                    }
                }

                if (listBoxAlergias != null && nombreTabla == "Raza")
                {
                    string queryAlergias = @"
                SELECT s.nombre 
                FROM Alergia s
                INNER JOIN Raza_Alergia es ON s.idAlergia = es.idAlergia
                WHERE es.idRaza = @ID";

                    using (SqlCommand cmdSensibilidades = new SqlCommand(queryAlergias, conexion))
                    {
                        cmdSensibilidades.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader readerSensibilidades = cmdSensibilidades.ExecuteReader())
                        {
                            listBoxAlergias.Items.Clear(); // Limpiar el ListBox antes de agregar nuevos elementos

                            while (readerSensibilidades.Read())
                            {
                                listBoxAlergias.Items.Add(readerSensibilidades["nombre"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos de la tabla {nombreTabla}: {ex.Message}");
            }
            finally
            {
                CerrarConexion();
            }
        }
        
        public void ModificarDatosGenerico(string nombreTabla, int id, Dictionary<string, object> parametrosValores)
        {
            try
            {
                // Construir la consulta SQL dinámicamente
                string query = $"UPDATE {nombreTabla} SET ";

                // Agregar parámetros para cada valor
                foreach (var parametroValor in parametrosValores)
                {
                    query += $"{parametroValor.Key} = @{parametroValor.Key}, ";
                }

                // Eliminar la última coma y espacio
                query = query.TrimEnd(',', ' ');

                // Agregar la condición WHERE
                query += $" WHERE id{nombreTabla} = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Agregar los valores de los parámetros
                    foreach (var parametroValor in parametrosValores)
                    {
                        cmd.Parameters.AddWithValue($"@{parametroValor.Key}", parametroValor.Value);
                    }

                    // Asegurarse de que la conexión esté abierta
                    if (conexion.State != System.Data.ConnectionState.Open)
                    {
                        AbrirConexion();
                    }

                    // Ejecutar la consulta
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Datos modificados correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún registro con el ID proporcionado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar los datos: {ex.Message}");
            }
            finally
            {
                CerrarConexion();
            }
        }

        public List<string> ObtenerTablasRelacionadas(string nombreTablaPrincipal)
        {
            List<string> tablasRelacionadas = new List<string>();

            try
            {
                AbrirConexion();
                // Consulta SQL para obtener las tablas relacionadas con la tabla principal
                string query = @"
SELECT 
    OBJECT_NAME(fk.parent_object_id) AS TablaRelacionada
FROM 
    sys.foreign_keys fk
INNER JOIN 
    sys.tables t ON fk.referenced_object_id = t.object_id
WHERE 
    t.name = @NombreTablaPrincipal";

                using (SqlCommand cmd = new SqlCommand(query, GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@NombreTablaPrincipal", nombreTablaPrincipal);

                    // Ejecutar la consulta y leer los resultados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tablaRelacionada = reader["TablaRelacionada"].ToString();
                            tablasRelacionadas.Add(tablaRelacionada);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las tablas relacionadas: {ex.Message}");
            }
            finally 
            {   
                CerrarConexion();
            }

            return tablasRelacionadas;
        }

      
        public void EliminarEnCascada(string nombreTablaPrincipal, int id, List<string> tablasRelacionadas)
        {
            // Preguntar al usuario si está seguro de eliminar
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este registro y todos sus registros relacionados?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Si el usuario no confirma, cancelar la operación
            if (resultado != DialogResult.Yes)
            {
                MessageBox.Show("Eliminación cancelada.");
                return;
            }

            SqlTransaction transaction = null;

            try
            {
                // Abrir la conexión
                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    AbrirConexion();
                }

                // Iniciar una transacción
                transaction = conexion.BeginTransaction();

                // Eliminar registros en las tablas relacionadas (hijas)
                foreach (var tabla in tablasRelacionadas)
                {
                    string query = $"DELETE FROM {tabla} WHERE id{nombreTablaPrincipal} = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conexion, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Eliminar el registro en la tabla principal
                string queryPrincipal = $"DELETE FROM {nombreTablaPrincipal} WHERE id{nombreTablaPrincipal} = @ID";
                using (SqlCommand cmdPrincipal = new SqlCommand(queryPrincipal, conexion, transaction))
                {
                    cmdPrincipal.Parameters.AddWithValue("@ID", id);
                    int rowsAffected = cmdPrincipal.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Eliminación en cascada realizada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún registro con el ID proporcionado.");
                    }
                }

                // Confirmar la transacción
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Revertir la transacción en caso de error
                transaction?.Rollback();
                MessageBox.Show($"Error al eliminar en cascada: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                CerrarConexion();
            }
        }
        public void AbrirConexion()
        {
            try
            {
                if (conexion == null)
                {
                    throw new InvalidOperationException("La conexión no está inicializada.");
                }

                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
            }
            catch (Exception ex)
            {
                // Si hay un error, intentar con la otra conexión
                Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                AlternarConexion(); // Cambiar a la otra cadena de conexión
                conexion.Open();   // Intentar abrir la nueva conexión
            }
        }

        public void CerrarConexion()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }

        public SqlConnection GetConexion()
        {
            return conexion;
        }

        private void AlternarConexion()
        {
            usarConexion1 = !usarConexion1;
            conexion = new SqlConnection(usarConexion1 ? cadenaConexion1 : cadenaConexion2);
        }
       
        public bool Existe(string query, string nombreEspecie)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand comandoEsp = new SqlCommand(query, GetConexion()))
                {
;
                    comandoEsp.Parameters.AddWithValue("@nombre", nombreEspecie);

                    int count = Convert.ToInt32(comandoEsp.ExecuteScalar());

                    // Si count es mayor que 0, la especie existe
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al verificar la especie: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally 
                { CerrarConexion(); }
        }

        public void Insertar(string query, string nombreEspecie)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand comandoEsp = new SqlCommand(query, GetConexion()))
                {

                    comandoEsp.Parameters.AddWithValue("@nombre", nombreEspecie);

                    comandoEsp.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al insertar la especie: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { CerrarConexion(); }
        }

        public void ActualizarComboBox(ComboBox comboBox1, string query, string campo)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand comandoEsp = new SqlCommand(query, GetConexion()))
                {

                    SqlDataReader reader = comandoEsp.ExecuteReader();

                    // Limpiar los elementos actuales del ComboBox
                    comboBox1.Items.Clear();

                    // Agregar los elementos al ComboBox
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[campo].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al actualizar el ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { CerrarConexion(); }
        }
    }
}
