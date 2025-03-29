using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace VetPet_
{
    public partial class xd : FormPadre
    {
        string cod;
        int idUsuario;
        bool correoEncontrado = false;
        string RFCUsuario;
        public xd()
        {
            InitializeComponent();
            BtnEnviarCodigo.Enabled = false;
            
        }

        private void btnCorreo_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
            panel3.Visible = true;
        }

        private void BtnPalabraClave_Click(object sender, EventArgs e)
        {
            panel4.BringToFront();
            panel4.Visible = true;
        }

        private void xd_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;
        }
        public static void EnviarCodigo(string emailDestino, string codigo)
        {
            try
            {
                SmtpClient smtp = ConfigurarSmtp(emailDestino);
                if (smtp == null)
                {
                    MessageBox.Show("Proveedor de correo no soportado.");
                    return;
                }

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("vetpet96@gmail.com"), // Cambia esto por el tuyo
                    Subject = "Código de recuperación de contraseña",
                    Body = $"Tu código de verificación es: {codigo}"
                };

                mail.To.Add(emailDestino);
                smtp.Send(mail);

                //MessageBox.Show("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo: " + ex.Message);
            }
        }
        private static SmtpClient ConfigurarSmtp(string email)
        {
            string dominio = email.Split('@')[1].ToLower();
            SmtpClient smtp = new SmtpClient();

            switch (dominio)
            {
                case "gmail.com":
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("vetpet96@gmail.com", "adbs vezo bomf pjko");
                    break;

                case "outlook.com":
                case "hotmail.com":
                case "live.com":
                    smtp.Host = "smtp.office365.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("vetpet96@gmail.com", "adbs vezo bomf pjko");
                    break;

                case "yahoo.com":
                    smtp.Host = "smtp.mail.yahoo.com";
                    smtp.Port = 465;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("vetpet96@gmail.com", "adbs vezo bomf pjko");
                    break;

                default:
                    return null; // Si el dominio no es reconocido
            }

            return smtp;
        }

        private void BtnEnviarCodigo_Click(object sender, EventArgs e)
        {
            if (TxtCodigo.Text == cod)
            {
                MessageBox.Show("Codigo Correcto. Puede cambiar su contraseña");
                CambiarContraseña recuperarForm = new CambiarContraseña(parentForm, idUsuario);
                if (recuperarForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
            else
                MessageBox.Show("Codigo Incorrecto. Intente de nuevo");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            correoEncontrado = false;
            idUsuario = ObtenerIdUsuario(TxtCorreo.Text);
            Random random = new Random();
            cod = random.Next(100000, 999999).ToString();
            EnviarCodigo(TxtCorreo.Text, cod);
            BtnEnviarCodigo.Enabled = true;
            if (correoEncontrado)
            {
                MessageBox.Show("Correo enviado correctamente.");
            }
            else
            {
                MessageBox.Show("No se encontró al empleado asociado con ese Correo Electronico.");
            }
        }
        private int ObtenerIdUsuario(string nombre)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT E.idEmpleado \r\nFROM Empleado E\r\nINNER JOIN Persona P ON E.idPersona = P.idPersona\r\nWHERE P.correoElectronico = @correo";
            int idServicioHijo = 0;

            using (SqlCommand cmd = new SqlCommand(queryGetIdServicioHijo, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@correo", nombre);
                    object result = cmd.ExecuteScalar(); // Ejecutar la consulta y obtener el primer valor de la primera columna

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToInt32(result);
                        correoEncontrado = true;
                    }
                    else
                    {
                        //MessageBox.Show("No se encontró al empleado asociado con ese Correo Electronico.");
                        correoEncontrado = false;

                    }
                    return idServicioHijo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado " + ex.Message);
                    return idServicioHijo;
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }

        private void BtnRegresarCorreo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRegresarPalabraC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOlvideMiPalabraClave_Click(object sender, EventArgs e)
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

        private void BtnPalabraClaveIngresar_Click(object sender, EventArgs e)
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
                    idUsuario = ObtenerIdUsuario(usuario, "Empleado");
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

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
