using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetPet_
{
    internal class conexionDaniel
    {
        private readonly string cadenaConexion = @"Server=DESKTOP-7PPM2OB\SQLEXPRESS;Database=VetPetPlus;Integrated Security=True;";
        private SqlConnection conexion;

        // Constructor
        public conexionDaniel()
        {
            conexion = new SqlConnection(cadenaConexion);
        }

        // Método para abrir la conexión
        public void AbrirConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al abrir la conexión: " + ex.Message);
            }
        }

        // Método para cerrar la conexión
        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cerrar la conexión: " + ex.Message);
            }
        }

        // Método para obtener la conexión
        public SqlConnection GetConexion()
        {
            return conexion;
        }
    }
}
