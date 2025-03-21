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
       // private readonly string cadenaConexion = @"Server=LAPTOP-NQM61SRI\SQLEXPRESS;Database=VetPetPlus;Integrated Security=True;";
        private SqlConnection conexion;

       
        public conexionDaniel()
        {
            conexion = new SqlConnection(cadenaConexion);
        }

       
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

       
        public SqlConnection GetConexion()
        {
            return conexion;
        }
    }
}
