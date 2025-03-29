using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetPet_
{
    internal class conexionBrandon : ConexionMaestra
    {
        private SqlConnection conexion;

        public conexionBrandon()
        {
            conexion = CrearConexion();
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
