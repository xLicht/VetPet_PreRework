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
        public int DatoCita = 0;
        public int DatoConsulta = 0;
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
            //MostrarCitasMascota();
            MostrarConsultas();
            //MostrarVacunasMascota();
            //MostrarAlergiasMascota();
            //MostrarSensibilidadesMascota();
            MostrarConsultaDes();
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
        //private void MostrarVacunasMascota()
        //{
        //    try
        //    {
        //        conexionDB.AbrirConexion();
        //        string query = @"
        //    SELECT v.nombre AS Vacuna
        //    FROM Vacuna v
        //    INNER JOIN Vacuna_Mascota vm ON v.idVacuna = vm.idVacuna
        //    WHERE vm.idMascota = @idMascota";
        //        using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
        //        {
        //            cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
        //            dtVacunas.DataSource = dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al obtener las vacunas de la mascota: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexionDB.CerrarConexion();
        //    }
        //}

        //private void MostrarCitasMascota()
        //{
        //    try
        //    {
        //        conexionDB.AbrirConexion();
        //        string query = @"SELECT c.idCita, c.fechaProgramada, c.hora, c.duracion, m.nombre AS Motivo
        //                         FROM Cita c
        //                         INNER JOIN Motivo m ON c.idMotivo = m.idMotivo
        //                         WHERE c.idMascota = @idMascota";

        //        using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
        //        {
        //            cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
        //            dtCitas.DataSource = dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al obtener las citas de la mascota: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexionDB.CerrarConexion();
        //    }
        //}

        private void MostrarConsultas()
        {

        }

        private void dtCitas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = dtCitas.Rows[e.RowIndex];
            //    if (row.Cells[0].Value != null)
            //    {
            //        int idCitaSeleccionada = Convert.ToInt32(row.Cells[0].Value);
            //        ConsultarCita formularioHijo = new ConsultarCita(parentForm);
            //        formularioHijo.DatoCita2 = idCitaSeleccionada;
            //        parentForm.formularioHijo(formularioHijo);

            //    }
            //    else
            //    {
            //        MessageBox.Show("No se pudo obtener el ID de la cita.");
            //    }
            //}
        }

        //private void MostrarAlergiasMascota()
        //{
        //    try
        //    {
        //        conexionDB.AbrirConexion();
        //        string query = @"
        //    SELECT a.nombre AS Alergia
        //    FROM Alergia a
        //    INNER JOIN Mascota_Alergia ma ON a.idAlergia = ma.idAlergia
        //    WHERE ma.idMascota = @idMascota";
        //        using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
        //        {
        //            cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
        //            dtAlergias.DataSource = dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al obtener las alergias de la mascota: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexionDB.CerrarConexion();
        //    }
        //}

        //private void MostrarSensibilidadesMascota()
        //{
        //    try
        //    {
        //        conexionDB.AbrirConexion();
        //        string query = @"
        //    SELECT s.nombre AS Sensibilidad
        //    FROM Sensibilidad s
        //    INNER JOIN Mascota_Sensibilidad ms ON s.idSensibilidad = ms.idSensibilidad
        //    WHERE ms.idMascota = @idMascota";
        //        using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
        //        {
        //            cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
        //            dtSensibilidades.DataSource = dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al obtener las sensibilidades de la mascota: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexionDB.CerrarConexion();
        //    }
        //}
        private void dtConsultas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MostrarConsultaDes()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"
        SELECT 
            c.idCita, 
            con.idConsulta, 
            c.fechaProgramada, 
            mo.nombre AS Motivo, 
            con.diagnostico, 
            con.peso, 
            con.temperatura, 
            con.EstudioEspecial
        FROM Cita c
        INNER JOIN Consulta con ON c.idCita = con.idCita
        INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
        WHERE c.idMascota = @idMascota";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idMascota", DatoMascota);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Crear un DataTable para el DataGridView con tres columnas:
                    DataTable dtGrid = new DataTable();
                    dtGrid.Columns.Add("idCita", typeof(int));
                    dtGrid.Columns.Add("idConsulta", typeof(int));
                    dtGrid.Columns.Add("Consultas", typeof(string));

                    // Recorrer cada registro y armar el string con los saltos de línea:
                    foreach (DataRow row in dt.Rows)
                    {
                        int idCita = Convert.ToInt32(row["idCita"]);
                        int idConsulta = Convert.ToInt32(row["idConsulta"]);
                        string fecha = row["fechaProgramada"].ToString();
                        string motivo = row["Motivo"].ToString();
                        string diagnostico = row["diagnostico"].ToString();
                        string peso = row["peso"].ToString();
                        string temperatura = row["temperatura"].ToString();
                        string estudioEspecial = row["EstudioEspecial"].ToString();

                        string formato = $"Fecha: {fecha}{Environment.NewLine}{Environment.NewLine}" +
                                         $"Motivo: {motivo}{Environment.NewLine}{Environment.NewLine}" +
                                         $"Diagnóstico: {diagnostico}{Environment.NewLine}{Environment.NewLine}" +
                                         $"Peso: {peso}{Environment.NewLine}{Environment.NewLine}" +
                                         $"Temperatura: {temperatura}{Environment.NewLine}{Environment.NewLine}" +
                                         $"Estudio Especial: {estudioEspecial}";

                        DataRow newRow = dtGrid.NewRow();
                        newRow["idCita"] = idCita;
                        newRow["idConsulta"] = idConsulta;
                        newRow["Consultas"] = formato;
                        dtGrid.Rows.Add(newRow);
                    }

                    // Asignar el DataTable al DataGridView:
                    dtConsultaDes.DataSource = dtGrid;

                    // Ocultar las columnas de idCita e idConsulta
                    dtConsultaDes.Columns["idCita"].Visible = false;
                    dtConsultaDes.Columns["idConsulta"].Visible = false;

                    // Configurar la columna "Consultas" para que se ajuste y muestre los saltos de línea:
                    dtConsultaDes.Columns["Consultas"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dtConsultaDes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar la consulta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void dtConsultaDes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void MostrarServicios()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"
                 SELECT 
                     sc.hora,
                     CASE 
                         WHEN sc.idVacuna IS NOT NULL THEN v.nombre 
                         WHEN sc.idServicioEspecificoNieto IS NOT NULL THEN sen.nombre 
                         ELSE 'Sin servicio'
                     END AS Servicio,
                     sc.estado,
                     sc.observacion
                 FROM Servicio_Cita sc
                 LEFT JOIN Vacuna v ON sc.idVacuna = v.idVacuna
                 LEFT JOIN ServicioEspecificoNieto sen ON sc.idServicioEspecificoNieto = sen.idServicioEspecificoNieto
                 WHERE sc.idCita = @idCita and sc.estado = 'A'";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Asignar el DataTable al DataGridView:
                    dtServicio.DataSource = dt;
                    dtServicio.Refresh();

                    // Mostrar mensaje según la cantidad de servicios encontrados:
                    if (dt.Rows.Count > 0)
                    {
                        //MessageBox.Show("La cita tiene " + dt.Rows.Count + " servicio(s).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("La cita no tiene servicios.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los servicios: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void dtConsultaDes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se haya hecho clic en una fila válida.
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtConsultaDes.Rows[e.RowIndex];

                // Validar si la celda que contiene el idCita está vacía o es nula.
                if (row.Cells["idCita"].Value != null && !string.IsNullOrWhiteSpace(row.Cells["idCita"].Value.ToString()))
                {
                    int idCitaSeleccionada = Convert.ToInt32(row.Cells["idCita"].Value);
                    DatoConsulta = Convert.ToInt32(row.Cells["idConsulta"].Value);


                    // Asignar el idCita obtenido a la variable DatoCita.
                    DatoCita = idCitaSeleccionada;

                    // Llamar al método que carga los servicios utilizando el idCita asignado.
                    MostrarServicios();
                }
                else
                {
                    MessageBox.Show("No hay consultas en esta fila.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnVerReceta_Click(object sender, EventArgs e)
        {
            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            int idConsulta = DatoConsulta;

           
            if (!ConsultaTieneReceta(idConsulta))
            {
                MessageBox.Show("No hay Recta en esta consulta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            VeterinariaConsultarRece formularioHijo = new VeterinariaConsultarRece(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
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
