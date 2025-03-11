using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using VetPet_;
using System.Data.SqlClient;

namespace VetPet_
{
    public partial class DueConsultarDueño : FormPadre
    {
        public int DatoEmpleado { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public DueConsultarDueño(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void DueConsultarDueño_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Dato"+DatoEmpleado);
            MostrarDato();
        }

        public void MostrarDato()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT 
                    p.nombre, p.apellidoP, p.apellidoM, p.celular, 
                    p.correoElectronico, 
                    pais.nombre AS pais, calle.nombre AS calle, 
                    cp.cp, ciudad.nombre AS ciudad, colonia.nombre AS colonia, estado.nombre AS estado  
                FROM 
                    Persona p
                LEFT JOIN 
                    Direccion d ON p.idPersona = d.idPersona
                LEFT JOIN 
                    Pais pais ON d.idPais = pais.idPais
                LEFT JOIN 
                    Calle calle ON d.idCalle = calle.idCalle
                LEFT JOIN 
                    Cp cp ON d.idCp = cp.idCp
                LEFT JOIN 
                    Ciudad ciudad ON d.idCiudad = ciudad.idCiudad
                LEFT JOIN 
                    Colonia colonia ON d.idColonia = colonia.idColonia
                LEFT JOIN 
                    Estado estado ON d.idEstado = estado.idEstado 
                WHERE 
                    p.idPersona = @idPersona";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombre.Text = reader["nombre"].ToString();
                        txtApellidoP.Text = reader["apellidoP"].ToString();
                        txtApellidoM.Text = reader["apellidoM"].ToString();
                        txtCelular.Text = reader["celular"].ToString();
                        txtCorreo.Text = reader["correoElectronico"].ToString();
                        txtPais.Text = reader["pais"].ToString();
                        txtCalle.Text = reader["calle"].ToString();
                        txtCp.Text = reader["cp"].ToString();
                        txtCiudad.Text = reader["ciudad"].ToString();
                        txtColonia.Text = reader["colonia"].ToString();
                        txtEstado.Text = reader["estado"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se pudo obtener los datos del dueño. " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueAtencionAlCliente(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueAtencionAlCliente(parentForm));
        }

        private void btnMostrarMascota_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueMascotadeDue(parentForm));
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
            DueModificarDueño formularioHijo = new DueModificarDueño(parentForm);
            formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
            parentForm.formularioHijo(formularioHijo);

            //parentForm.formularioHijo(new DueModificarDueño(parentForm));
        }
    } 
}
