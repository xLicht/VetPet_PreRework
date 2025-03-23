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
            MessageBox.Show("dato"+ DatoCita);
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
                        cbMotivo.SelectedValue = Convert.ToInt32(reader["idMotivo"]);
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

            if (cbServicioNieto.SelectedItem != null)
                nombreServicio = cbServicioNieto.Text;
            else if (cbServicioEspecifico.SelectedItem != null)
                nombreServicio = cbServicioEspecifico.Text;
            else if (cbServicioP.SelectedItem != null)
                nombreServicio = cbServicioP.Text;

            if (!string.IsNullOrEmpty(nombreServicio))
            {
                string empleadoSeleccionado = cbEmpleado.Text;

                if (!listaServicios.Any(s => s.NombreServicio == nombreServicio))
                {
                    listaServicios.Add(new ServicioSeleccionado(nombreServicio, empleadoSeleccionado));
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
        private void ActualizarDataGrid()
        {
            dtServicio.DataSource = null;
            dtServicio.DataSource = listaServicios;
            dtServicio.Refresh();
        }
        private void CargarServiciosCita()
        {
            try
            {
                conexionDB.AbrirConexion();
                // Se realiza un JOIN para obtener el nombre del servicio (por ejemplo, desde ServicioEspecificoNieto) y el usuario del empleado.
                string query = @"
            SELECT sen.nombre AS NombreServicio, e.usuario AS Empleado
            FROM Servicio_Cita sc
            INNER JOIN ServicioEspecificoNieto sen ON sc.idServicioEspecificoNieto = sen.idServicioEspecificoNieto
            INNER JOIN Empleado e ON sc.idEmpleado = e.idEmpleado
            WHERE sc.idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
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
            if (cbServicioEspecifico.SelectedValue != null)
            {
                int idServicioEspecificoHijo;

                if (cbServicioEspecifico.SelectedValue is DataRowView)
                {
                    DataRowView drv = (DataRowView)cbServicioEspecifico.SelectedValue;
                    idServicioEspecificoHijo = Convert.ToInt32(drv["idServicioEspecificoHijo"]);
                }
                else
                {
                    idServicioEspecificoHijo = Convert.ToInt32(cbServicioEspecifico.SelectedValue);
                }

                CargarServiciosNietos(idServicioEspecificoHijo);
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
                    int idServicioNieto = ObtenerIdServicioNieto(servicio.NombreServicio, transaction);
                    int idEmpleado = ObtenerIdEmpleado(servicio.Empleado, transaction);

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
    }
}
