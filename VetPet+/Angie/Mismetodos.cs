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

        public void agregarDatosGenerico(string nombreProcedimiento, Dictionary<string, object> parametrosValores)
        {
            try
            {
                AbrirConexion();
                string query = $"Exec {nombreProcedimiento}";

                // Agregar parámetros para cada valor
                foreach (var parametroValor in parametrosValores)
                {
                    query += $", @{parametroValor.Key}";
                }

                query += ", @Resultado OUTPUT";

                using (SqlCommand cmd = new SqlCommand(query, GetConexion()))
                {
                    foreach (var parametroValor in parametrosValores)
                    {
                        cmd.Parameters.AddWithValue($"@{parametroValor.Key}", parametroValor.Value);
                    }

                    SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.VarChar, 100);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);

                   
                    cmd.ExecuteNonQuery();

                    string resultado = resultadoParam.Value.ToString();
                    MessageBox.Show(resultado);
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
                // Cerrar la conexión actual si está abierta
                CerrarConexion();

                // Cambiar a la otra cadena de conexión
                if (usarConexion1)
                {
                    conexion = new SqlConnection(cadenaConexion2);
                    usarConexion1 = false;
                    Console.WriteLine("Cambiando a la segunda cadena de conexión.");
                }
                else
                {
                    conexion = new SqlConnection(cadenaConexion1);
                    usarConexion1 = true;
                    Console.WriteLine("Cambiando a la primera cadena de conexión.");
                }
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
