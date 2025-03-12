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
    public partial class AgregarTipoVacunas : FormPadre
    {
        public AgregarTipoVacunas()
        {
            InitializeComponent();
        }
        public AgregarTipoVacunas(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaVacunas(parentForm));
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "INSERT INTO ServicioEspecificoHijo (nombre, descripcion, idServicioPadre) VALUES (@NOM, @DES, @ISP);";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Primero obtenemos los valores
                    string Nombre = TxtNombre.Text;
                    string Descripcion = richTextBox1.Text.Replace("\r", "").Replace("\n", "");
                    int idServicio = 3;

                    // Agregamos los parámetros
                    cmd.Parameters.AddWithValue("@NOM", Nombre);
                    cmd.Parameters.AddWithValue("@DES", Descripcion);
                    cmd.Parameters.AddWithValue("@ISP", idServicio);

                    // Ejecutamos la consulta
                    cmd.ExecuteNonQuery();  // Cambié ExecuteReader por ExecuteNonQuery

                    MessageBox.Show("Nuevo Tipo de Servicio Registrado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
    }
}
