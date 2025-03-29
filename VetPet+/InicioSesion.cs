using FontAwesome.Sharp;
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

namespace VetPet_
{
    public partial class InicioSesion : FormPadre
    {

        public InicioSesion()
        {
            InitializeComponent();
        }
        public InicioSesion(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {

        }

        private void BtnOlvideMiContraseña_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new xd());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string usuario = Txtus.Text;
            string contraseña = Txtcontra.Text;
            

            if (ValidarCredenciales(usuario, contraseña))
            {
                int idUsuario = ObtenerIdUsuario(usuario, "Empleado");
                int tipoEmpleado = ObtenerIdTipoEmpleado(idUsuario);


                FondoCaja fondoC = new FondoCaja(idUsuario,tipoEmpleado);
                fondoC.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCredenciales(string usuario, string contraseña)
        {
            bool valido = false;
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT COUNT(1) FROM Empleado WHERE usuario = @usuario AND contraseña = @contraseña";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contraseña", contraseña);

                try
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    valido = count > 0;
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

        private void BtnOlvidarContraseña_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultamos el formulario de login
            xd recuperarForm = new xd();
            if (recuperarForm.ShowDialog() == DialogResult.OK)
            {
                // Lógica adicional si es necesario después de recuperar la contraseña
            }
            this.Show(); // Volver a mostrar el formulario de login si es necesario
        }
        private int ObtenerIdTipoEmpleado(int IdNombre)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT idTipoEmpleado FROM empleado WHERE idEmpleado = @IDNombre";
            int idServicioHijo = 0;

            using (SqlCommand cmd = new SqlCommand(queryGetIdServicioHijo, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@IDNombre", IdNombre);
                    object result = cmd.ExecuteScalar(); // Ejecutar la consulta y obtener el primer valor de la primera columna

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el servicio hijo con ese IdNombre.");

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
        private int ObtenerIdUsuario(string nombre, string tabla)
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
                        MessageBox.Show("No se encontró el servicio hijo con ese IdNombre.");

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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Txtcontra.PasswordChar = (Txtcontra.PasswordChar == '*') ? '\0' : '*';
        }

        private void InicioSesion_Load(object sender, EventArgs e)
        {
            Txtcontra.PasswordChar = '*';
            Txtus.Text = "Juan.PG";
            Txtcontra.Text = "123456789";
        }
    }
}
