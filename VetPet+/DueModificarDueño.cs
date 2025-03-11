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
    public partial class DueModificarDueño : FormPadre
    {
        public int DatoEmpleado { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public DueModificarDueño(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void DueModificarDueño_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Mensaje: "+ DatoEmpleado);
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
                        cbPais.Text = reader["pais"].ToString();
                        cbCalle.Text = reader["calle"].ToString();
                        txtCp.Text = reader["cp"].ToString();
                        cbCiudad.Text = reader["ciudad"].ToString();
                        cbColonia.Text = reader["colonia"].ToString();
                        cbEstado.Text = reader["estado"].ToString();
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
        private void GuardarCambios()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"
                UPDATE Persona 
                SET nombre = @nombre, 
                    apellidoP = @apellidoP, 
                    apellidoM = @apellidoM, 
                    celular = @celular, 
                    correoElectronico = @correo
                WHERE idPersona = @idPersona;

                UPDATE Direccion
                SET idPais = (SELECT idPais FROM Pais WHERE nombre = @pais),
                    idCalle = (SELECT idCalle FROM Calle WHERE nombre = @calle),
                    idCp = (SELECT idCp FROM Cp WHERE cp = @cp),
                    idCiudad = (SELECT idCiudad FROM Ciudad WHERE nombre = @ciudad),
                    idColonia = (SELECT idColonia FROM Colonia WHERE nombre = @colonia),
                    idEstado = (SELECT idEstado FROM Estado WHERE nombre = @estado)
                WHERE idPersona = @idPersona;
        ";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@apellidoP", txtApellidoP.Text);
                    cmd.Parameters.AddWithValue("@apellidoM", txtApellidoM.Text);
                    cmd.Parameters.AddWithValue("@celular", txtCelular.Text);
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@pais", cbPais.Text);
                    cmd.Parameters.AddWithValue("@calle", cbCalle.Text);
                    cmd.Parameters.AddWithValue("@cp", txtCp.Text);
                    cmd.Parameters.AddWithValue("@ciudad", cbCiudad.Text);
                    cmd.Parameters.AddWithValue("@colonia", cbColonia.Text);
                    cmd.Parameters.AddWithValue("@estado", cbEstado.Text);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Datos actualizados correctamente.");
                        parentForm.formularioHijo(new DueConsultarDueño(parentForm)); // Recargar vista
                    }
                    else
                    {
                        MessageBox.Show("No se realizaron cambios.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar datos: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnMostrarMascota_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueMascotadeDue(parentForm));
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
            DueConsultarDueño formularioHijo = new DueConsultarDueño(parentForm);
            formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
            parentForm.formularioHijo(formularioHijo);

            //parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
            DueConsultarDueño formularioHijo = new DueConsultarDueño(parentForm);
            formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
            parentForm.formularioHijo(formularioHijo);



            //parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }
    }
}
