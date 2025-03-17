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
            string usuario = TxtFondoCaja.Text;


            if (ValidarCredenciales(usuario))
            {
                Form1 forma = new Form1(idUsuario,idEmpleado);
                forma.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarCredenciales(string usuario)
        {
            // CAMBIAR LA CONSULTA CON EL FONDO DE CAJA
            bool valido = false;
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT COUNT(1) FROM Empleado WHERE usuario = @usuario";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario);

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
    }
}
