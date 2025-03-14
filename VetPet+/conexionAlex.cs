using System;
using System.Collections.Generic;
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
