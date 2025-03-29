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

namespace VetPet_
{
    public partial class VeterinariaModificarCita : FormPadre
    {
        public int DatoCita { get; set; }
        private int idMotivoCita;
        private conexionDaniel conexionDB = new conexionDaniel();
        private List<ServicioSeleccionado> listaServicios = new List<ServicioSeleccionado>();
        public VeterinariaModificarCita(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaModificarCita_Load(object sender, EventArgs e)
        {
            dtHora.Format = DateTimePickerFormat.Custom;
            dtHora.CustomFormat = "HH:mm:ss";
            //MessageBox.Show("dato"+ DatoCita);
            CargarDatosCita();
            CargarMotivos();
            CargarEmpleados();
            CargarServiciosPadre();
            CargarServiciosCita();
        }
        private void CargarDatosCita()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"
                SELECT 
                    c.fechaProgramada, 
                    c.hora, 
                    c.duracion, 
                    c.idMotivo,
                    mo.nombre AS Motivo,
                    p.nombre + ' ' + p.apellidoP + ' ' + p.apellidoM AS NombreCompleto, 
                    m.nombre AS NombreMascota
                FROM Cita c
                INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
                INNER JOIN Mascota m ON c.idMascota = m.idMascota
                INNER JOIN Persona p ON m.idPersona = p.idPersona
                WHERE c.idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        dtFecha.Value = Convert.ToDateTime(reader["fechaProgramada"]);
                        TimeSpan hora = (TimeSpan)reader["hora"];
                        dtHora.Value = DateTime.Today.Add(hora);

                        idMotivoCita = Convert.ToInt32(reader["idMotivo"]);
                        cbDueño.Text = reader["NombreCompleto"].ToString();
                        cbMascota.Text = reader["NombreMascota"].ToString();
                        txtDuracion.Text = reader["duracion"].ToString();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de la cita: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void CargarMotivos()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT idMotivo, nombre FROM Motivo";
                SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cbMotivo.DataSource = dt;
                cbMotivo.DisplayMember = "nombre";
                cbMotivo.ValueMember = "idMotivo";

                if (dt.Rows.Count > 0)
                {
                    cbMotivo.SelectedValue = idMotivoCita;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar motivos: " + ex.Message);
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
        private int ObtenerIdServicioNieto(string nombreServicio, SqlTransaction transaction)
        {
            string query = "SELECT idServicioEspecificoNieto FROM ServicioEspecificoNieto WHERE nombre = @nombre";
            SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion(), transaction);
            cmd.Parameters.AddWithValue("@nombre", nombreServicio);
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        private int ObtenerIdEmpleado(string nombreEmpleado, SqlTransaction transaction)
        {
            string query = "SELECT idEmpleado FROM Empleado WHERE usuario = @usuario";
            SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion(), transaction);
            cmd.Parameters.AddWithValue("@usuario", nombreEmpleado);
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
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

                    if (dt.Rows.Count > 0)
                    {
                        cbServicioNieto.DataSource = dt;
                        cbServicioNieto.DisplayMember = "nombre";
                        cbServicioNieto.ValueMember = "idServicioEspecificoNieto";
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
                MessageBox.Show("Error al cargar los servicios nietos: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void CargarEmpleados()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT idEmpleado, usuario FROM Empleado WHERE idTipoEmpleado IN (1,3)";
                SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cbEmpleado.DataSource = dt;
                cbEmpleado.DisplayMember = "usuario";
                cbEmpleado.ValueMember = "idEmpleado";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void AgregarServicioSeleccionado()
        {
            string nombreServicio = "";
            bool esVacuna = false;
            int idVacuna = 0;

            if (cbServicioP.Text.Equals("Vacunas", StringComparison.OrdinalIgnoreCase))
            {
                if (cbServicioNieto.SelectedItem != null)
                {
                    nombreServicio = cbServicioNieto.Text;
                    idVacuna = Convert.ToInt32(cbServicioNieto.SelectedValue);
                    esVacuna = true;
                }
                else
                {
                    MessageBox.Show("Seleccione una vacuna.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (cbServicioNieto.SelectedItem != null)
                    nombreServicio = cbServicioNieto.Text;
                else if (cbServicioEspecifico.SelectedItem != null)
                    nombreServicio = cbServicioEspecifico.Text;
                else if (cbServicioP.SelectedItem != null)
                    nombreServicio = cbServicioP.Text;
            }
            if (esVacuna)
            {
                int idMascota = Convert.ToInt32(cbMascota.SelectedValue);
                if (VacunaYaAplicada(idMascota, idVacuna))
                {
                    MessageBox.Show("La mascota ya tiene aplicada esta vacuna.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(nombreServicio))
            {
                string empleadoSeleccionado = cbEmpleado.Text;
                if (!listaServicios.Any(s => s.NombreServicio.Equals(nombreServicio, StringComparison.OrdinalIgnoreCase)))
                {
                    listaServicios.Add(new ServicioSeleccionado(nombreServicio, empleadoSeleccionado, esVacuna, idVacuna));
                    ActualizarDataGrid();
                }
                else
                {
                    MessageBox.Show("Este servicio ya ha sido agregado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un servicio antes de agregarlo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool VacunaYaAplicada(int idMascota, int idVacuna)
        {
            bool existe = false;
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT COUNT(*) FROM Vacuna_Mascota WHERE idMascota = @idMascota AND idVacuna = @idVacuna";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idMascota", idMascota);
                    cmd.Parameters.AddWithValue("@idVacuna", idVacuna);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    MessageBox.Show($"idMascota: {idMascota}, idVacuna: {idVacuna}, count: {count}");
                    if (count > 0)
                    {
                        existe = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar la vacuna: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
            return existe;
        }

        private void ActualizarDataGrid()
        {
            dtServicio.DataSource = null;
            dtServicio.DataSource = listaServicios;
            dtServicio.Refresh();

            if (dtServicio.Columns.Contains("EsVacuna"))
                dtServicio.Columns["EsVacuna"].Visible = false;
            if (dtServicio.Columns.Contains("IdVacuna"))
                dtServicio.Columns["IdVacuna"].Visible = false;
            if (dtServicio.Columns.Contains("hora"))
                dtServicio.Columns["hora"].Visible = false;
        }
        private void CargarServiciosCita()
        {
            //try
            //{
            //    conexionDB.AbrirConexion();

            //    string query = @"
            //    SELECT sen.nombre AS NombreServicio, e.usuario AS Empleado
            //    FROM Servicio_Cita sc
            //    INNER JOIN ServicioEspecificoNieto sen ON sc.idServicioEspecificoNieto = sen.idServicioEspecificoNieto
            //    INNER JOIN Empleado e ON sc.idEmpleado = e.idEmpleado
            //    WHERE sc.idCita = @idCita";

            //    using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
            //    {
            //        cmd.Parameters.AddWithValue("@idCita", DatoCita);
            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        DataTable dt = new DataTable();
            //        adapter.Fill(dt);
            //        dtServicio.DataSource = dt;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al cargar los servicios de la cita: " + ex.Message);
            //}
            //finally
            //{
            //    conexionDB.CerrarConexion();
            //}
            //try
            //{
            //    conexionDB.AbrirConexion();

            //    string query = @"
            //SELECT 
            //    sc.hora, 
            //    COALESCE(v.nombre, sen.nombre) AS NombreServicio, 
            //    e.usuario AS Empleado
            //FROM Servicio_Cita sc
            //LEFT JOIN Vacuna v ON sc.idVacuna = v.idVacuna
            //LEFT JOIN ServicioEspecificoNieto sen ON sc.idServicioEspecificoNieto = sen.idServicioEspecificoNieto
            //INNER JOIN Empleado e ON sc.idEmpleado = e.idEmpleado
            //WHERE sc.idCita = @idCita";

            //    using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
            //    {
            //        cmd.Parameters.AddWithValue("@idCita", DatoCita);
            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        DataTable dt = new DataTable();
            //        adapter.Fill(dt);
            //        dtServicio.DataSource = dt;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al cargar los servicios de la cita: " + ex.Message);
            //}
            //finally
            //{
            //    conexionDB.CerrarConexion();
            //}
            try
            {
                conexionDB.AbrirConexion();

                string query = @"
            SELECT 
                sc.hora, 
                COALESCE(v.nombre, sen.nombre) AS NombreServicio, 
                e.usuario AS Empleado
            FROM Servicio_Cita sc
            LEFT JOIN Vacuna v ON sc.idVacuna = v.idVacuna
            LEFT JOIN ServicioEspecificoNieto sen ON sc.idServicioEspecificoNieto = sen.idServicioEspecificoNieto
            INNER JOIN Empleado e ON sc.idEmpleado = e.idEmpleado
            WHERE sc.idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtServicio.DataSource = dt;

                    // Ocultar la columna "hora"
                    if (dtServicio.Columns.Contains("hora"))
                    {
                        dtServicio.Columns["hora"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los servicios de la cita: " + ex.Message);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarServicioSeleccionado();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            try
            {
                conexionDB.AbrirConexion();
                transaction = conexionDB.GetConexion().BeginTransaction();

                TimeSpan hora = dtHora.Value.TimeOfDay;

                string queryUpdate = @"
            UPDATE Cita 
            SET fechaProgramada = @fechaProgramada, 
                hora = @hora, 
                duracion = @duracion, 
                idMotivo = @idMotivo
            WHERE idCita = @idCita";
                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexionDB.GetConexion(), transaction))
                {
                    cmdUpdate.Parameters.AddWithValue("@fechaProgramada", dtFecha.Value);
                    cmdUpdate.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                    cmdUpdate.Parameters.AddWithValue("@duracion", Convert.ToInt32(txtDuracion.Text));
                    cmdUpdate.Parameters.AddWithValue("@idMotivo", cbMotivo.SelectedValue);
                    cmdUpdate.Parameters.AddWithValue("@idCita", DatoCita);
                    cmdUpdate.ExecuteNonQuery();
                }


                string queryDelete = "DELETE FROM Servicio_Cita WHERE idCita = @idCita";
                using (SqlCommand cmdDelete = new SqlCommand(queryDelete, conexionDB.GetConexion(), transaction))
                {
                    cmdDelete.Parameters.AddWithValue("@idCita", DatoCita);
                    cmdDelete.ExecuteNonQuery();
                }


                foreach (var servicio in listaServicios)
                {
                    int idEmpleado = ObtenerIdEmpleado(servicio.Empleado, transaction);

                    if (servicio.EsVacuna)
                    {
                        string queryInsertVacuna = @"
                    INSERT INTO Servicio_Cita (idCita, idServicioEspecificoNieto, idVacuna, idEmpleado, hora)
                    VALUES (@idCita, NULL, @idVacuna, @idEmpleado, @hora)";
                        using (SqlCommand cmdInsert = new SqlCommand(queryInsertVacuna, conexionDB.GetConexion(), transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@idCita", DatoCita);
                            cmdInsert.Parameters.AddWithValue("@idVacuna", servicio.IdVacuna);
                            cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmdInsert.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        int idServicioNieto = ObtenerIdServicioNieto(servicio.NombreServicio, transaction);
                        string queryInsert = @"
                    INSERT INTO Servicio_Cita (idCita, idServicioEspecificoNieto, idEmpleado, hora)
                    VALUES (@idCita, @idServicioNieto, @idEmpleado, @hora)";
                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conexionDB.GetConexion(), transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@idCita", DatoCita);
                            cmdInsert.Parameters.AddWithValue("@idServicioNieto", idServicioNieto);
                            cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmdInsert.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                MessageBox.Show("Cita modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try { transaction.Rollback(); } catch { }
                }
                MessageBox.Show("Error al guardar los cambios en la cita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void dtServicio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    string servicio = dtServicio.Rows[e.RowIndex].Cells["NombreServicio"].Value.ToString();
            //    DialogResult resultado = MessageBox.Show($"¿Deseas eliminar el servicio '{servicio}'?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (resultado == DialogResult.Yes)
            //    {
            //        listaServicios.RemoveAt(e.RowIndex);
            //        ActualizarDataGrid();
            //    }
            //}
            if (e.RowIndex >= 0)
            {
                // Obtener el objeto de la fila seleccionada
                ServicioSeleccionado servicioSeleccionado = dtServicio.Rows[e.RowIndex].DataBoundItem as ServicioSeleccionado;
                if (servicioSeleccionado != null)
                {
                    DialogResult resultado = MessageBox.Show($"¿Deseas eliminar el servicio '{servicioSeleccionado.NombreServicio}'?",
                                                              "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        listaServicios.Remove(servicioSeleccionado);
                        ActualizarDataGrid();
                    }
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show($"Los datos no guardados seran eliminados", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                int idCitaSeleccionada = Convert.ToInt32(DatoCita);
                ConsultarCita formularioHijo = new ConsultarCita(parentForm);
                formularioHijo.DatoCita = idCitaSeleccionada;
                parentForm.formularioHijo(formularioHijo);
            }
        }
    }
}
