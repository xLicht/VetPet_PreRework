using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_.Angie.Mascotas
{
    internal class Mismetodos
    {
       // public readonly string cadenaConexion = @"Data Source=127.0.0.1;Initial Catalog=VetPetPlus;Integrated Security=True;";
        public readonly string cadenaConexion = @"Server=DESKTOP-7PPM2OB\SQLEXPRESS;Database=VetPetPlus;Integrated Security=True;";

        public SqlConnection conexion;

        public Mismetodos()
        {
            conexion = new SqlConnection(cadenaConexion);
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
                Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                throw;
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
