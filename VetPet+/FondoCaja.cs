using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using VetPet_;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VetPet_
{
    public partial class FondoCaja : FormPadre
    {
        int idUsuario;
        int idEmpleado;

        public FondoCaja(int idUs, int tipoEmp)
        {
            InitializeComponent();
            this.idUsuario = idUs;
            this.idEmpleado = tipoEmp;
        }


        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            


            if (!string.IsNullOrWhiteSpace(TxtFondoCaja.Text))
            {
                int fondoCaja = Convert.ToInt32(TxtFondoCaja.Text);
                string tipoUsuario = ObtenerTipoEmpleado(idEmpleado);
                string nombreUsuario = obtenernombreUsuario(idUsuario);
                Form1 forma = new Form1(idUsuario,idEmpleado,fondoCaja,tipoUsuario,nombreUsuario);
                forma.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Ingrese un fondo de caja, por favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private bool ValidarCredenciales(string usuario)
        //{
        //    // CAMBIAR LA CONSULTA CON EL FONDO DE CAJA
        //    bool valido = false;
        //    conexionAlex conexion = new conexionAlex();
        //    conexion.AbrirConexion();
        //    string query = "SELECT COUNT(1) FROM Empleado WHERE usuario = @usuario";
        //    using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
        //    {
        //        cmd.Parameters.AddWithValue("@usuario", usuario);

        //        try
        //        {
        //            int count = Convert.ToInt32(cmd.ExecuteScalar());
        //            valido = count > 0;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error de conexión: " + ex.Message);
        //        }
        //        finally
        //        {
        //            conexion.CerrarConexion();
        //        }
        //    }
        //    return valido;
        //}
        private string obtenernombreUsuario(int nombre)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT usuario  FROM Empleado WHERE idEmpleado = @NombreServicioHijo";
            string idServicioHijo = "";

            using (SqlCommand cmd = new SqlCommand(queryGetIdServicioHijo, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@NombreServicioHijo", nombre);
                    object result = cmd.ExecuteScalar(); // Ejecutar la consulta y obtener el primer valor de la primera columna

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToString(result);
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
        private string ObtenerTipoEmpleado(int IdNombre)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT nombre FROM TipoEmpleado WHERE idTipoEmpleado = @IDNombre";
            string idServicioHijo = "";

            using (SqlCommand cmd = new SqlCommand(queryGetIdServicioHijo, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@IDNombre", IdNombre);
                    object result = cmd.ExecuteScalar(); // Ejecutar la consulta y obtener el primer valor de la primera columna

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToString(result);
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
        private void TxtFondoCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números del 0 al 9 y la tecla de retroceso (Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // No permitir el carácter ingresado
            }
        }
    }
}
