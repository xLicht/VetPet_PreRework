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
    }
}
