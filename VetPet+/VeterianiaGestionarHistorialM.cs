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
    public partial class VeterianiaGestionarHistorialM : FormPadre
    {
        public int DatoMascota { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();

        public VeterianiaGestionarHistorialM(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
           
        }

        private void VeterianiaGestionarHistorialM_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Mensaje recibido :" + DatoMascota);
            MostrarDatosMascota();
            MostrarCitasMascota();
            MostrarConsultas();
            MostrarVacunasMascota();
            MostrarAlergiasMascota();
            MostrarSensibilidadesMascota();
        }

        private void VeterianiaGestionarHistorialM_Resize(object sender, EventArgs e)
        {
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaModificarHistorial(parentForm)); 
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitasMascota(parentForm)); 
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaHistorialMedico(parentForm)); 
        }

        private void MostrarDatosMascota()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT m.nombre AS NombreMascota, 
                                m.esterilizado, 
                                m.muerto, 
                                m.peso, 
                                m.fechaNacimiento, 
                                m.sexo, 
                                p.nombre AS NombreDueño, 
                                p.celularPrincipal AS Telefono, 
                                r.nombre AS Raza, 
                                e.nombre AS Especie
                         FROM Mascota m
                         INNER JOIN Persona p ON m.idPersona = p.idPersona
                         INNER JOIN Raza r ON m.idRaza = r.idRaza
                         INNER JOIN Especie e ON m.idEspecie = e.idEspecie
                         WHERE m.idMascota = @idMascota";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtMascota.Text = reader["NombreMascota"].ToString();
                        txtPeso.Text = reader["Peso"].ToString();
                        txtFechaNac.Text = reader["FechaNacimiento"].ToString();
                        txtSexo.Text = reader["Sexo"].ToString();
                        txtDueño.Text = reader["NombreDueño"].ToString();
                        txtNumero.Text = reader["Telefono"].ToString();
                        txtRaza.Text = reader["Raza"].ToString();
                        txtEspecie.Text = reader["Especie"].ToString();

                        // Marcar los CheckBox
                        cbEsterilizado.Checked = reader["esterilizado"].ToString() == "S";
                        cbMuerto.Checked = reader["muerto"].ToString() == "S";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de la mascota: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void MostrarVacunasMascota()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"
            SELECT v.nombre AS Vacuna
            FROM Vacuna v
            INNER JOIN Vacuna_Mascota vm ON v.idVacuna = vm.idVacuna
            WHERE vm.idMascota = @idMascota";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtVacunas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las vacunas de la mascota: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void MostrarCitasMascota()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"SELECT c.idCita, c.fechaProgramada, c.hora, c.duracion, m.nombre AS Motivo
                                 FROM Cita c
                                 INNER JOIN Motivo m ON c.idMotivo = m.idMotivo
                                 WHERE c.idMascota = @idMascota";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtCitas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las citas de la mascota: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void MostrarConsultas()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT c.idConsulta, c.diagnostico, c.idCita
                         FROM Consulta c
                         INNER JOIN Cita ci ON c.idCita = ci.idCita
                         WHERE ci.idMascota = @idMascota";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtConsultas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el historial de consultas: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void dtCitas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtCitas.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    int idCitaSeleccionada = Convert.ToInt32(row.Cells[0].Value);
                    ConsultarCita formularioHijo = new ConsultarCita(parentForm);
                    formularioHijo.DatoCita2 = idCitaSeleccionada;
                    parentForm.formularioHijo(formularioHijo);

                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID de la cita.");
                }
            }
        }
        private void MostrarAlergiasMascota()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"
            SELECT a.nombre AS Alergia
            FROM Alergia a
            INNER JOIN Mascota_Alergia ma ON a.idAlergia = ma.idAlergia
            WHERE ma.idMascota = @idMascota";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtAlergias.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las alergias de la mascota: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void MostrarSensibilidadesMascota()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"
            SELECT s.nombre AS Sensibilidad
            FROM Sensibilidad s
            INNER JOIN Mascota_Sensibilidad ms ON s.idSensibilidad = ms.idSensibilidad
            WHERE ms.idMascota = @idMascota";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtSensibilidades.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las sensibilidades de la mascota: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void dtConsultas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtConsultas.Rows[e.RowIndex];
                if (row.Cells[2].Value != null)
                {
                    int idConsultaSeleccionada = Convert.ToInt32(row.Cells[0].Value);
                    VeterinariaConsultaMedica formularioHijo = new VeterinariaConsultaMedica(parentForm);
                    formularioHijo.DatoCita2 = idConsultaSeleccionada;
                    parentForm.formularioHijo(formularioHijo);

                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID de la cita.");
                }
            }
        }
    }
    
}
