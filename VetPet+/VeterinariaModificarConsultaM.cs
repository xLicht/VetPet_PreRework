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
    public partial class VeterinariaModificarConsultaM : FormPadre
    {
        public int DatoCita { get; set; }
        public int DatoCita2 { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        int DatoCitaT = 0;
        int DatoCita2T = 0;
        public VeterinariaModificarConsultaM(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaModificarConsultaM_Load(object sender, EventArgs e)
        {
            // MessageBox.Show("Dato Recibido :" + DatoCita);
            DatoCitaT = DatoCita;
            DatoCita2T = DatoCita2;
            if (DatoCita == 0)
            {
                DatoCita = DatoCita2;
            }
            MostrarServicios();
            MostrarDatosCita();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
          DialogResult resultado = MessageBox.Show("Los cambios no guardados se perderán. ¿Está seguro de que desea cancelar?","Advertencia",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                int idCitaSeleccionada = Convert.ToInt32(DatoCita);
                VeterinariaConsultaMedica formularioHijo = new VeterinariaConsultaMedica(parentForm);
                if (DatoCitaT == 0 && DatoCita2T != 0)
                {
                    formularioHijo.DatoCita2 = idCitaSeleccionada;
                }
                else
                {
                    formularioHijo.DatoCita = idCitaSeleccionada;
                }
                parentForm.formularioHijo(formularioHijo);
            }

            //int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            //VeterinariaConsultaMedica formularioHijo = new VeterinariaConsultaMedica(parentForm);
            //formularioHijo.DatoCita = idCitaSeleccionada;
            //parentForm.formularioHijo(formularioHijo);


            //parentForm.formularioHijo(new VeterinariaConsultaMedica(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            ActualizarConsulta();


           // parentForm.formularioHijo(new VeterinariaConsultaMedica(parentForm));
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
        private void ActualizarConsulta()
        {
            //try
            //{
            //    conexionDB.AbrirConexion();

            //    string query = @"UPDATE Consulta 
            //            SET peso = @peso, 
            //                temperatura = @temperatura, 
            //                MotivoConsulta = @motivo, 
            //                diagnostico = @diagnostico, 
            //                EstudioEspecial = @estudioEspecial
            //            WHERE idCita = @idCita";

            //    using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
            //    {
            //        cmd.Parameters.AddWithValue("@peso", Convert.ToDecimal(txtPeso.Text));
            //        cmd.Parameters.AddWithValue("@temperatura", Convert.ToDecimal(txtTemperatura.Text));
            //        cmd.Parameters.AddWithValue("@motivo", txtMotivo.Text);
            //        cmd.Parameters.AddWithValue("@diagnostico", txtDiagnostico.Text);
            //        cmd.Parameters.AddWithValue("@estudioEspecial", rtEstudioEspecial.Text);
            //        cmd.Parameters.AddWithValue("@idCita", DatoCita);

            //        int filasAfectadas = cmd.ExecuteNonQuery();

            //        if (filasAfectadas > 0)
            //        {
            //            MessageBox.Show("Consulta actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("No se encontró la consulta para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al actualizar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    conexionDB.CerrarConexion();
            //}

            try
            {
                conexionDB.AbrirConexion();

                string queryConsulta = @"UPDATE Consulta 
                        SET peso = @peso, 
                            temperatura = @temperatura, 
                            diagnostico = @diagnostico, 
                            EstudioEspecial = @estudioEspecial
                        WHERE idCita = @idCita";
                using (SqlCommand cmdConsulta = new SqlCommand(queryConsulta, conexionDB.GetConexion()))
                {
                    cmdConsulta.Parameters.AddWithValue("@peso", Convert.ToDecimal(txtPeso.Text));
                    cmdConsulta.Parameters.AddWithValue("@temperatura", Convert.ToDecimal(txtTemperatura.Text));
                    cmdConsulta.Parameters.AddWithValue("@diagnostico", txtDiagnostico.Text);
                    cmdConsulta.Parameters.AddWithValue("@estudioEspecial", rtEstudioEspecial.Text);
                    cmdConsulta.Parameters.AddWithValue("@idCita", DatoCita);

                    cmdConsulta.ExecuteNonQuery();
                }


                string queryCita = @"UPDATE Cita 
                             SET idMotivo = (SELECT idMotivo FROM Motivo WHERE nombre = @motivo)
                             WHERE idCita = @idCita";
                using (SqlCommand cmdCita = new SqlCommand(queryCita, conexionDB.GetConexion()))
                {
                    cmdCita.Parameters.AddWithValue("@motivo", txtMotivo.Text);
                    cmdCita.Parameters.AddWithValue("@idCita", DatoCita);

                    int filasAfectadasCita = cmdCita.ExecuteNonQuery();
                    if (filasAfectadasCita == 0)
                    {
                        MessageBox.Show("No se encontró la cita para actualizar el motivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                MessageBox.Show("Consulta actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
       
    }
}
