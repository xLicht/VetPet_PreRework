using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_;

namespace VetPet_
{
    public partial class CambiarContraseña : FormPadre
    {
        int idUsuario;
        public CambiarContraseña(int usuario)
        {
            InitializeComponent();
            this.idUsuario = usuario;
        }
        public CambiarContraseña(Form1 parent, int usuario)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
            this.idUsuario = usuario;
        }
        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new InicioSesion(parentForm));
        }
        private bool ValidarContraseña(string contraseña)
        {
            // Expresión regular para validar la contraseña
            string patron = @"^(?=.*[A-Z])(?=.*\d).{8,}$";

            return Regex.IsMatch(contraseña, patron);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = TxtContraseña.Text;
            string confirmarContraseña = TxtConfiMiContraseña.Text;

            if (string.IsNullOrWhiteSpace(nuevaContraseña) || string.IsNullOrWhiteSpace(confirmarContraseña))
            {
                MessageBox.Show("Ambos campos son obligatorios");

                return;
            }
            else if (nuevaContraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden");
                return;
            }
            if (ValidarContraseña(nuevaContraseña))
            {
                conexionAlex conexion = new conexionAlex();
                conexion.AbrirConexion();
                string query = "UPDATE empleado SET contraseña = @nuevaContraseña WHERE idEmpleado = @idEmpleado";
                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@nuevaContraseña", nuevaContraseña);
                        cmd.Parameters.AddWithValue("@idEmpleado", idUsuario);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Contraseña cambiada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres, una mayúscula y un número.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            TxtContraseña.PasswordChar = (TxtContraseña.PasswordChar == '*') ? '\0' : '*';
            TxtConfiMiContraseña.PasswordChar = (TxtConfiMiContraseña.PasswordChar == '*') ? '\0' : '*';
        }

        private void CambiarContraseña_Load(object sender, EventArgs e)
        {
            TxtContraseña.PasswordChar = '*';
            TxtConfiMiContraseña.PasswordChar = '*';
        }

        private void TxtContraseña_TextChanged(object sender, EventArgs e)
        {
            int progreso = 0;
            string contraseña = TxtContraseña.Text;

            // Comprobar cada condición y aumentar la barra de progreso
            if (contraseña.Length >= 8) progreso += 33;   // Longitud mínima de 8 caracteres
            if (contraseña.Any(char.IsUpper)) progreso += 33; // Al menos una mayúscula
            if (contraseña.Any(char.IsDigit)) progreso += 34; // Al menos un número

            // Limitar el valor máximo de la barra de progreso a 100
            if (progreso == 0)
            {
                label4.Text = "Debil";
                label4.ForeColor = Color.Red;
            }
            else if (progreso <= 34)
            {
                label4.Text = "Moderada";
                label4.ForeColor = Color.OrangeRed;
            }
            else if (progreso <= 67)
            {
                label4.Text = "Buena";
                label4.ForeColor = Color.GreenYellow;
            }
            else if (progreso <= 100)
            {
                label4.Text = "Excelente";
                label4.ForeColor = Color.Green;
            }
            progressBarSeguridad.Value = progreso;
        }
    }
}
