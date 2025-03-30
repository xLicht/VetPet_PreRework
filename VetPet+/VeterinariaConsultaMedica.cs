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
        int DatoCitaT = 0;
        int DatoCita2T = 0;
        private conexionDaniel conexionDB = new conexionDaniel();
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
                int idMascotaSeleccionada = Convert.ToInt32(DatoCita);
                ConsultarCita formularioHijo = new ConsultarCita(parentForm);
                formularioHijo.DatoCita = DatoCita;
                parentForm.formularioHijo(formularioHijo);
                //parentForm.formularioHijo(new CitasMedicas(parentForm));
            }
            //parentForm.formularioHijo(new ConsultarCita(parentForm));
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaModificarConsultaM formularioHijo = new VeterinariaModificarConsultaM(parentForm);
            if (DatoCitaT == 0 && DatoCita2T != 0)
            {
                formularioHijo.DatoCita2 = idCitaSeleccionada;
            }
            else
            {
                formularioHijo.DatoCita = idCitaSeleccionada;
            }
            parentForm.formularioHijo(formularioHijo);



            //parentForm.formularioHijo(new VeterinariaModificarConsultaM(parentForm));
        }

        private void btnRecetar_Click(object sender, EventArgs e)
        {

            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            int idConsulta = ObtenerIdConsulta(idCitaSeleccionada);

            // Verificar si la consulta tiene receta; si no, se muestra un mensaje y se detiene el proceso.
            if (!ConsultaTieneReceta(idConsulta))
            {
                MessageBox.Show("Primero debe existir una receta creada para poder consultarla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VeterinariaConsultarRece formularioHijo = new VeterinariaConsultarRece(parentForm);
            if (DatoCitaT == 0 && DatoCita2T != 0)
            {
                formularioHijo.DatoCita2 = idCitaSeleccionada;
            }
            else
            {
                formularioHijo.DatoCita = idCitaSeleccionada;
            }
            //formularioHijo.DatoConsulta = idConsulta; // Asigna el id de la consulta obtenida
            parentForm.formularioHijo(formularioHijo);

            //int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            //VeterinariaConsultarRece formularioHijo = new VeterinariaConsultarRece(parentForm);
            //if (DatoCitaT == 0 && DatoCita2T != 0)
            //{
            //    formularioHijo.DatoCita2 = idCitaSeleccionada;
            //}
            //else
            //{
            //    formularioHijo.DatoCita = idCitaSeleccionada;
            //}
            //parentForm.formularioHijo(formularioHijo);

            ////parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
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
                    con.EstudioEspecial AS EstudioEspecial, 
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
                        txtFecha.Text = reader["FechaProgramada"].ToString();
                        txtMascota.Text = reader["NombreMascota"].ToString();
                        txtEspecie.Text = reader["Especie"].ToString();
                        txtRaza.Text = reader["Raza"].ToString();
                        txtPeso.Text = reader["Peso"] != DBNull.Value ? reader["Peso"].ToString() : "N/A";
                        txtTemperatura.Text = reader["Temperatura"] != DBNull.Value ? reader["Temperatura"].ToString() : "N/A";
                        txtMotivo.Text = reader["MotivoConsulta"].ToString();
                        txtDiagnostico.Text = reader["Diagnostico"] != DBNull.Value ? reader["Diagnostico"].ToString() : "Sin diagnóstico";
                        rtEstudioEspecial.Text = reader["EstudioEspecial"] != DBNull.Value ? reader["EstudioEspecial"].ToString() : "Sin estudio especial";

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

            int idConsulta = 0;

            try
            {
                conexionDB.AbrirConexion();
                string queryIdConsulta = "SELECT TOP 1 idConsulta FROM Consulta WHERE idCita = @idCita ORDER BY idConsulta DESC";
                using (SqlCommand cmd = new SqlCommand(queryIdConsulta, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        idConsulta = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el idConsulta: " + ex.Message);
                return;
            }
            finally
            {
                conexionDB.CerrarConexion();
            }

            if (idConsulta == 0)
            {
                dtServicio.DataSource = null;
                return;
            }

            try
            {
                conexionDB.AbrirConexion();

                string query = @"
            SELECT 
                sc.observacion,
                CASE 
                    WHEN sc.idVacuna IS NOT NULL THEN v.nombre 
                    ELSE sen.nombre 
                END AS Servicio,
                CASE 
                    WHEN sc.idVacuna IS NOT NULL THEN 'Vacuna'
                    ELSE 'Servicio'
                END AS Tipo
            FROM Servicio_Consulta sc
            LEFT JOIN Vacuna v ON sc.idVacuna = v.idVacuna
            LEFT JOIN ServicioEspecificoNieto sen ON sc.idServicioEspecificoNieto = sen.idServicioEspecificoNieto
            WHERE sc.idConsulta = @idConsulta";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", idConsulta);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtServicio.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los servicios de la consulta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                conexionDB.AbrirConexion();

                    string query = @"
                UPDATE Consulta
                SET estado = 'I'
                WHERE idConsulta = (
                    SELECT TOP 1 idConsulta 
                    FROM Consulta 
                    WHERE idCita = @idCita
                )";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Consulta eliminada (inactivada) correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la consulta para eliminar.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la consulta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private int ObtenerIdConsulta(int idCita)
        {
            int idConsulta = 0;
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT TOP 1 idConsulta FROM Consulta WHERE idCita = @idCita ORDER BY idConsulta DESC";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", idCita);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idConsulta = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el idConsulta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
            return idConsulta;
        }

        private void btnRecetar_Click_1(object sender, EventArgs e)
        {
            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            int idConsulta = ObtenerIdConsulta(idCitaSeleccionada);
            VeterinariaRecetar formularioHijo = new VeterinariaRecetar(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            formularioHijo.DatoConsulta = idConsulta;
            parentForm.formularioHijo(formularioHijo);
        }
        private bool ConsultaTieneReceta(int idConsulta)
        {
            bool tieneReceta = false;
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT COUNT(*) FROM Receta WHERE idConsulta = @idConsulta";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", idConsulta);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        tieneReceta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar la receta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
            return tieneReceta;
        }
        
    }
}
