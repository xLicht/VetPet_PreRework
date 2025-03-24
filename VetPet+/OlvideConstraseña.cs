using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VetPet_
{
    public partial class OlvideConstraseña : FormPadre
    {
        int idUsuario;
        string RFCUsuario;
        public OlvideConstraseña()
        {
            InitializeComponent();
        }
        public OlvideConstraseña(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOlvideMiContraseña_Click(object sender, EventArgs e)
        {
            string usuario = TxtUsuario.Text;

            if (string.IsNullOrWhiteSpace(TxtUsuario.Text))
            {
                MessageBox.Show("Ingrese su usuario");

                return;
            }
            idUsuario = ObtenerIdUsuario(usuario, "Empleado");
            RFCUsuario = ObtenerRFC(idUsuario);
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "UPDATE empleado SET contraseña = @RFC WHERE idEmpleado = @idEmpleado";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    //cmd.Parameters.AddWithValue("@nuevaContraseña", nuevaContraseña);
                    cmd.Parameters.AddWithValue("@RFC", RFCUsuario);
                    cmd.Parameters.AddWithValue("@idEmpleado", idUsuario);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contraseña ha sido cambiada por su RFC", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Cerrar el formulario
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        { 
            string usuario = TxtUsuario.Text;
            string palbraClave = TxtPalabraClave.Text;

            if (ValidarCredenciales(usuario, palbraClave)) 
            {

                CambiarContraseña cambiarContraseñaForm = new CambiarContraseña(idUsuario);
                cambiarContraseñaForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o palabra clave incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCredenciales(string usuario, string palabraClave)
        {
            bool valido = false;
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT COUNT(1) FROM Empleado WHERE usuario = @usuario AND palabraClave = @palabraClave";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@palabraClave", palabraClave);

                try
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    valido = count > 0;
                    idUsuario= ObtenerIdUsuario(usuario, "Empleado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
            return valido;
        }
        public int ObtenerIdUsuario(string nombre, string tabla)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT id" + tabla + " FROM " + tabla + " WHERE usuario = @NombreServicioHijo";
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
        private string ObtenerRFC(int id)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT rfc FROM empleado WHERE idEmpleado = @idEmpleado";
            string idServicioHijo = "";

            using (SqlCommand cmd = new SqlCommand(queryGetIdServicioHijo, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@idEmpleado", id);
                    object result = cmd.ExecuteScalar(); // Ejecutar la consulta y obtener el primer valor de la primera columna

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToString(result);
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

        private void OlvideConstraseña_Load(object sender, EventArgs e)
        {

        }
    }
}
