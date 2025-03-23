using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetPet_
{
    internal class conexionBrandon
    {
        //conexion pc brandon
        public readonly string cadenaConexion = @"Data Source=DESKTOP-0434B1E;Initial Catalog=VetPetPlus;Integrated Security=True;";
        //conexion laptop brandon
        //public readonly string cadenaConexion = @"Data Source=BRANDONWROK\SQLEXPRESS;Initial Catalog=VetPetPlus;Integrated Security=True;";
        private SqlConnection conexion;

        public conexionBrandon()
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
