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
        string codigo;
        int idUsuario;
        bool correoEncontrado = false;
        public xd()
        {
            InitializeComponent();
            BtnEnviarCodigo.Enabled = false;
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

                MessageBox.Show("Correo enviado correctamente.");
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

            if (TxtCodigo.Text == codigo)
            {
                MessageBox.Show("Código correcto. Puede cambiar su contraseña.");
                CambiarContraseña recuperarForm = new CambiarContraseña(parentForm, idUsuario);
                if (recuperarForm.ShowDialog() == DialogResult.OK)
                {
                    // Lógica adicional si es necesario después de recuperar la contraseña
                }
                // Abrir formulario para nueva contraseña
            }
            else
            {
                MessageBox.Show("Código incorrecto. Intente de nuevo.");
            }

        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {           
            correoEncontrado = false;
            idUsuario = ObtenerIdUsuario(TxtCorreo.Text);
                Random random = new Random();
                codigo = random.Next(100000, 999999).ToString();
                EnviarCodigo(TxtCorreo.Text, codigo);
                BtnEnviarCodigo.Enabled = true;
            if (correoEncontrado)
            {
            }
            else
            {

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
                        MessageBox.Show("No se encontró al empleado asociado con ese Correo Electronico.");
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
        private int ObtenerCorreo(string nombre)
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
                        MessageBox.Show("No se encontró al empleado asociado con ese Correo Electronico.");
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

        private void xd_Load(object sender, EventArgs e)
        {

        }

        //MailMessage mail = new MailMessage();
        //SmtpClient smtp = new SmtpClient("smtp.gmail.com"); // Servidor SMTP
        //smtp.Port = 587; // Puerto (ejemplo: 587 para Gmail)
        //        smtp.Credentials = new NetworkCredential("androidemoc@gmail.com", "trwb eyge kyco pygb");
        //smtp.EnableSsl = true;

        //        mail.From = new MailAddress("androidemoc@gmail.com");
        //mail.To.Add(emailDestino);
        //        mail.Subject = "Código de recuperación de contraseña";
        //        mail.Body = $"Tu código de verificación es: {codigo}";
    }
}
