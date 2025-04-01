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
        private List<ServicioSeleccionadoConsulta> listaServicios = new List<ServicioSeleccionadoConsulta>();
        private int guardadosCount = 0;
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
            CargarServiciosPadre();
            //MostrarServicios();
            CargarServiciosConsulta();
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
            try
            {
                conexionDB.AbrirConexion();

                string query = @"UPDATE Consulta 
                        SET peso = @peso, 
                            temperatura = @temperatura, 
                            MotivoConsulta = @motivo, 
                            diagnostico = @diagnostico, 
                            EstudioEspecial = @estudioEspecial
                        WHERE idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@peso", Convert.ToDecimal(txtPeso.Text));
                    cmd.Parameters.AddWithValue("@temperatura", Convert.ToDecimal(txtTemperatura.Text));
                    cmd.Parameters.AddWithValue("@motivo", txtMotivo.Text);
                    cmd.Parameters.AddWithValue("@diagnostico", txtDiagnostico.Text);
                    cmd.Parameters.AddWithValue("@estudioEspecial", rtEstudioEspecial.Text);
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Consulta actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la consulta para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
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
                dtServicios.DataSource = null;
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
                    dtServicios.DataSource = dt;
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

        private void CargarServiciosConsulta()
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
                WHERE sc.idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dtServicios.DataSource = dt;
                    dtServicios.Refresh();
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
        private void CargarServiciosPadre()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"SELECT sp.idServicioPadre, sp.nombre FROM ServicioPadre sp
                                  INNER JOIN ClaseServicio cs ON sp.idClaseServicio = cs.idClaseServicio
                                  WHERE cs.nombre <> 'Estético'";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbServicioP.DataSource = dt;
                    cbServicioP.DisplayMember = "nombre";
                    cbServicioP.ValueMember = "idServicioPadre";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void cbServicioEspecifico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbServicioEspecifico.SelectedItem != null)
            {
                string nombreServicioEspecifico = cbServicioEspecifico.Text;

                int idServicioEspecificoHijo = ObtenerIdServicioEspecificoHijo(nombreServicioEspecifico);


                if (cbServicioP.Text.Equals("Vacunas", StringComparison.OrdinalIgnoreCase))
                {
                    CargarVacunas(idServicioEspecificoHijo);
                }
                else
                {
                    CargarServiciosNietos(idServicioEspecificoHijo);
                }
            }
        }
        private int ObtenerIdServicioEspecificoHijo(string nombreServicioEspecifico)
        {
            int id = 0;
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT idServicioEspecificoHijo FROM ServicioEspecificoHijo WHERE nombre = @nombre";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreServicioEspecifico);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        id = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el id del servicio específico: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
            return id;
        }
        private void CargarVacunas(int idServicioEspecificoHijo)
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT idVacuna, nombre FROM Vacuna WHERE idServicioEspecificoHijo = @idServicioEspecificoHijo";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idServicioEspecificoHijo", idServicioEspecificoHijo);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cbServicioNieto.DataSource = dt;
                        cbServicioNieto.DisplayMember = "nombre";
                        cbServicioNieto.ValueMember = "idVacuna";
                    }
                    else
                    {
                        cbServicioNieto.DataSource = null;
                        cbServicioNieto.Items.Clear();
                        cbServicioNieto.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las vacunas: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void CargarServiciosNietos(int idServicioHijo)
        {
            string query = "SELECT idServicioEspecificoNieto, nombre FROM ServicioEspecificoNieto WHERE idServicioEspecificoHijo = @idServicioHijo";

            try
            {
                conexionDB.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idServicioHijo", idServicioHijo);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbServicioNieto.DataSource = dt;
                    cbServicioNieto.DisplayMember = "nombre";
                    cbServicioNieto.ValueMember = "idServicioEspecificoNieto";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los servicios nietos: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void cbServicioP_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbServicioEspecifico.Text = "";
            if (cbServicioP.SelectedItem != null)
            {
                string servicioSeleccionado = cbServicioP.Text;

                string query = "SELECT idServicioPadre FROM ServicioPadre WHERE nombre = @nombre";

                try
                {
                    conexionDB.AbrirConexion();
                    using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                    {
                        cmd.Parameters.AddWithValue("@nombre", servicioSeleccionado);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int idServicioPadre = Convert.ToInt32(result);

                            CargarServiciosHijos(idServicioPadre);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el ID del servicio padre: " + ex.Message);
                }
                finally
                {
                    conexionDB.CerrarConexion();
                }
            }
        }
        private void CargarServiciosHijos(int idServicioPadre)
        {
            string query = "SELECT idServicioEspecificoHijo, nombre FROM ServicioEspecificoHijo WHERE idServicioPadre = @idServicioPadre";

            try
            {
                conexionDB.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idServicioPadre", idServicioPadre);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbServicioEspecifico.DataSource = dt;
                    cbServicioEspecifico.DisplayMember = "nombre";
                    cbServicioEspecifico.ValueMember = "idServicioEspecificoHijo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los servicios específicos hijos: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarServicioALista();
        }
        private void AgregarServicioALista()
        {
            if (cbServicioP.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar al menos un servicio padre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPadre = Convert.ToInt32(cbServicioP.SelectedValue);
            string nombrePadre = cbServicioP.Text;
            string empleado = "Ninguno";
            string observacion = rtObservacion.Text.Trim();


            if (nombrePadre.Equals("Vacunas", StringComparison.OrdinalIgnoreCase))
            {
                if (cbServicioNieto.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una vacuna.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idVacuna = Convert.ToInt32(cbServicioNieto.SelectedValue);
                string nombreVacuna = cbServicioNieto.Text;


                listaServicios.Add(new ServicioSeleccionadoConsulta(nombreVacuna, empleado, true, idVacuna, observacion));
            }
            else if (nombrePadre == "Consulta General")
            {

                listaServicios.Add(new ServicioSeleccionadoConsulta("Consulta General", empleado, false, 0, observacion));
            }
            else
            {
                if (cbServicioEspecifico.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un servicio hijo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idHijo = Convert.ToInt32(cbServicioEspecifico.SelectedValue);
                string nombreHijo = cbServicioEspecifico.Text;
                string nombreFinal = nombreHijo;


                if (cbServicioNieto.SelectedItem != null)
                {

                    nombreFinal = cbServicioNieto.Text;
                }

                listaServicios.Add(new ServicioSeleccionadoConsulta(nombreFinal, empleado, false, 0, observacion));
            }

            ActualizarDataGrid();
        }

        private void ActualizarDataGrid()
        {
            DataTable dtGuardados = new DataTable();
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
                WHERE sc.idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtGuardados);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios guardados: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
            guardadosCount = dtGuardados.Rows.Count;

            DataTable dtPendientes = new DataTable();
            dtPendientes.Columns.Add("hora", typeof(TimeSpan));
            dtPendientes.Columns.Add("Servicio", typeof(string));
            dtPendientes.Columns.Add("estado", typeof(string));
            dtPendientes.Columns.Add("observacion", typeof(string));

            foreach (var servicio in listaServicios)
            {
                dtPendientes.Rows.Add(DateTime.Now.TimeOfDay, servicio.NombreServicio, "A", servicio.Observacion);
            }

            DataTable dtCombined = dtGuardados.Clone();
            foreach (DataRow row in dtGuardados.Rows)
            {
                dtCombined.ImportRow(row);
            }
            foreach (DataRow row in dtPendientes.Rows)
            {
                dtCombined.ImportRow(row);
            }

            dtServicios.DataSource = dtCombined;
            dtServicios.Refresh();
        }

        private void dtServicios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex < guardadosCount)
                {
                    MessageBox.Show("No se puede eliminar un servicio ya guardado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int pendingIndex = e.RowIndex - guardadosCount;
                    DialogResult confirmacion = MessageBox.Show("¿Desea eliminar este servicio pendiente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmacion == DialogResult.Yes)
                    {
                        listaServicios.RemoveAt(pendingIndex);
                        ActualizarDataGrid();
                    }
                }
            }
        }
    }
}

