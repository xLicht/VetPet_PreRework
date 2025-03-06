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

        public bool Existe(string query, string nombreEspecie)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=127.0.0.1;Integrated Security=SSPI;Initial Catalog=Master"))
                {
                    connection.Open();

                    // Consulta para verificar si la especie existe

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", nombreEspecie);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // Si count es mayor que 0, la especie existe
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al verificar la especie: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void Insertar(string query, string nombreEspecie)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=127.0.0.1;Integrated Security=SSPI;Initial Catalog=Master"))
                {
                    connection.Open();

                    // Consulta para insertar la nueva especie
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", nombreEspecie);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al insertar la especie: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarComboBox(ComboBox comboBox1, string query, string campo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=127.0.0.1;Integrated Security=SSPI;Initial Catalog=Master"))
                {
                    connection.Open();

                    // Consulta para obtener todas las especies
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

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
        }
    }
}
