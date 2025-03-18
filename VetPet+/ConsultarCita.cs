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
    public partial class ConsultarCita : FormPadre
    {
        public int DatoCita { get; set; }
        public int DatoCita2 { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        int DatoCitaT = 0;
        int DatoCita2T = 0; 
        public ConsultarCita(Form1 parent)
        {
            InitializeComponent(); 
            parentForm = parent;  
        }

        private void ConsultarCita_Load(object sender, EventArgs e)
        {
     
            MostrarDatosCita();
            MostrarServicios();
        }

        private void ConsultarCita_Resize(object sender, EventArgs e)
        {

        }

        private void btnVerConsulta_Click(object sender, EventArgs e)
        {
            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaConsultaMedica formularioHijo = new VeterinariaConsultaMedica(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            parentForm.formularioHijo(formularioHijo);

           // parentForm.formularioHijo(new VeterinariaConsultaMedica(parentForm)); 
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaConsultarM formularioHijo = new VeterinariaConsultarM(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            parentForm.formularioHijo(formularioHijo);



            //parentForm.formularioHijo(new VeterinariaConsultarM(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //parentForm.formularioHijo(new CitasMedicas(parentForm));

            if (DatoCitaT == 0 && DatoCita2 != 0)
            {
                int idMascotaSeleccionada = Convert.ToInt32(DatoCita);
                VeterianiaGestionarHistorialM formularioHijo = new VeterianiaGestionarHistorialM(parentForm);
                formularioHijo.DatoMascota = idMascotaSeleccionada;
                parentForm.formularioHijo(formularioHijo);
            }
            else
            {
                parentForm.formularioHijo(new CitasMedicas(parentForm));
            }

        }

        private void ConsultarCita_Load_1(object sender, EventArgs e)
        {
            // MessageBox.Show("Dato Recibido :" + DatoCita);
            //MessageBox.Show("Dato Recibido :" + DatoCita2);
            DatoCitaT = DatoCita;
            DatoCita2T = DatoCita2;

            if (DatoCita == 0)
            {
                DatoCita = DatoCita2;
            }
            MostrarDatosCita();
            MostrarServicios();
        }

        private void MostrarDatosCita()
        {
            try
            {
                conexionDB.AbrirConexion();
              
                string query = @"SELECT p.nombre AS NombreCliente, p.apellidoP AS ApellidoPaterno, p.celularPrincipal AS Telefono, 
                                c.fechaRegistro AS FechaRegistro, c.fechaProgramada AS FechaProgramada, 
                                m.nombre AS NombreMascota, c.hora AS Hora, mo.nombre AS Motivo
                        FROM Cita c
                        INNER JOIN Mascota m ON c.idMascota = m.idMascota
                        INNER JOIN Persona p ON m.idPersona = p.idPersona
                        INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
                        WHERE c.idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombre.Text = reader["NombreCliente"].ToString();
                        txtApellidoPat.Text = reader["ApellidoPaterno"].ToString();
                        txtTelefono.Text = reader["Telefono"].ToString();
                        txtFechaR.Text = reader["FechaRegistro"].ToString();
                        txtFechaP.Text = reader["FechaProgramada"].ToString();
                        txtMascota.Text = reader["NombreMascota"].ToString();
                        txtHora.Text = reader["Hora"].ToString();
                        rtMotivo.Text = reader["Motivo"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de la cita: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void MostrarServicios()
        {
            try
            {
                conexionDB.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("EXEC sp_ObtenerServiciosCita @idCita", conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtServicio.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los servicios de la cita: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
    }
}
