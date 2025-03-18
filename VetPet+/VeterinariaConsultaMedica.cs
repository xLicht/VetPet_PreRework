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
    public partial class VeterinariaConsultaMedica : FormPadre
    {
        public int DatoCita { get; set; }
        public int DatoCita2 { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        int DatoCitaT = 0;
        int DatoCita2T = 0;
        public VeterinariaConsultaMedica(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaConsultaMedica_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Dato Recibido :"+ DatoCita2);
            DatoCitaT = DatoCita;
            DatoCita2T = DatoCita2;

            if (DatoCita == 0)
            {
                DatoCita = DatoCita2;
            }
            MostrarServicios();
            MostrarDatosCita();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
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
            //parentForm.formularioHijo(new ConsultarCita(parentForm));
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaModificarConsultaM formularioHijo = new VeterinariaModificarConsultaM(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            parentForm.formularioHijo(formularioHijo);



            //parentForm.formularioHijo(new VeterinariaModificarConsultaM(parentForm));
        }

        private void btnRecetar_Click(object sender, EventArgs e)
        {
            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaConsultarRece formularioHijo = new VeterinariaConsultarRece(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            parentForm.formularioHijo(formularioHijo);



            //parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
        }
    
        private void MostrarDatosCita()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT 
                        p.nombre AS NombreCliente, 
                        p.apellidoP AS ApellidoPaterno, 
                        p.celularPrincipal AS Telefono, 
                        c.fechaRegistro AS FechaRegistro, 
                        c.fechaProgramada AS FechaProgramada, 
                        m.nombre AS NombreMascota, 
                        e.nombre AS Especie, 
                        r.nombre AS Raza, 
                        con.peso AS Peso, 
                        con.temperatura AS Temperatura, 
                        con.diagnostico AS Diagnostico, 
                        mo.nombre AS MotivoConsulta, 
                        m.esterilizado AS Esterilizado, 
                        m.muerto AS Fallecido
                    FROM Cita c
                    INNER JOIN Mascota m ON c.idMascota = m.idMascota
                    INNER JOIN Persona p ON m.idPersona = p.idPersona
                    INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
                    INNER JOIN Especie e ON m.idEspecie = e.idEspecie
                    INNER JOIN Raza r ON m.idRaza = r.idRaza
                    LEFT JOIN Consulta con ON c.idCita = con.idCita
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
                        // txtFechaR.Text = reader["FechaRegistro"].ToString();
                        txtFecha.Text = reader["FechaProgramada"].ToString();
                        txtMascota.Text = reader["NombreMascota"].ToString();
                        txtEspecie.Text = reader["Especie"].ToString();
                        txtRaza.Text = reader["Raza"].ToString();
                        txtPeso.Text = reader["Peso"] != DBNull.Value ? reader["Peso"].ToString() : "N/A";
                        txtTemperatura.Text = reader["Temperatura"] != DBNull.Value ? reader["Temperatura"].ToString() : "N/A";
                        txtMotivo.Text = reader["MotivoConsulta"].ToString();
                        txtDiagnostico.Text = reader["Diagnostico"] != DBNull.Value ? reader["Diagnostico"].ToString() : "Sin diagnóstico";

                        cbCastrado.Checked = reader["Esterilizado"] != DBNull.Value && reader["Esterilizado"].ToString() == "S";
                        cbFallecido.Checked = reader["Fallecido"] != DBNull.Value && reader["Fallecido"].ToString() == "S";
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
