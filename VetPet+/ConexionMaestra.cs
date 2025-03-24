using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetPet_
{
    public class ConexionMaestra
    {
        public string cadenaConexion;

        public SqlConnection CrearConexion()
        {
            string nombreDispositivo = Environment.MachineName;
            if (nombreDispositivo == "DESKTOP-7PPM2OB")
                cadenaConexion = "Server=DESKTOP-7PPM2OB\\SQLEXPRESS;Database=VetPetPlus;Integrated Security=True;";
            else if (nombreDispositivo == "DESKTOP-0434B1E")
                cadenaConexion = "Data Source=DESKTOP-0434B1E;Initial Catalog=VetPetPlus;Integrated Security=True;";
            else if (nombreDispositivo == "DESKTOP-GQ6Q9HG")
                cadenaConexion = "Data Source=DESKTOP-GQ6Q9HG\\SQLEXPRESS;Initial Catalog=VetPetPlus;Integrated Security=True;";
            else if (nombreDispositivo == "ROGSTRIXANGIE")
                cadenaConexion = "Data Source=ROGSTRIXANGIE;Initial Catalog=VetPetPlus;Integrated Security=True;";
            else if (nombreDispositivo == "CARLOS-DESKTOP")
                cadenaConexion = "Data Source=CARLOS-DESKTOP;Initial Catalog=VetPetPlus;Integrated Security=True";
            else if (nombreDispositivo == "CARLOS-LAPTOP")
                cadenaConexion = "Data Source=CARLOS-LAPTOP;Initial Catalog=VetPetPlus;Integrated Security=True";
            return new SqlConnection(cadenaConexion);
        }
    }
}
