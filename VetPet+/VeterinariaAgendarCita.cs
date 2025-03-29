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
    public partial class VeterinariaAgendarCita : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        private List<ServicioSeleccionado> listaServicios = new List<ServicioSeleccionado>();
        public VeterinariaAgendarCita(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaAgendarCita_Load(object sender, EventArgs e)
        {
            dtHora.Format = DateTimePickerFormat.Custom;
            dtHora.CustomFormat = "HH:mm:ss";
            CargarServiciosPadre();
            CargarDueños();
            CargarMotivos();
            CargarEmpleados();
        }
        private void CargarDueños()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT idPersona, nombre + ' ' + apellidoP + ' ' + apellidoM AS NombreCompleto FROM Persona";
                SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cbDueño.DataSource = dt;
                cbDueño.DisplayMember = "NombreCompleto";
                cbDueño.ValueMember = "idPersona";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar dueños: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnAgendar_Click(object sender, EventArgs e)
        {
            try
            {
                conexionDB.AbrirConexion();
                SqlTransaction transaction = conexionDB.GetConexion().BeginTransaction();


                int idDueño = Convert.ToInt32(cbDueño.SelectedValue);
                int idMascota = Convert.ToInt32(cbMascota.SelectedValue);
                int idMotivo = Convert.ToInt32(cbMotivo.SelectedValue);
                DateTime fechaProgramada = dtFecha.Value;
                TimeSpan hora = dtHora.Value.TimeOfDay;
                int duracion = Convert.ToInt32(txtDuracion.Text);


                string queryCita = @"
                    INSERT INTO Cita (fechaProgramada, hora, duracion, idMascota, idMotivo)
                    VALUES (@fechaProgramada, @hora, @duracion, @idMascota, @idMotivo);
                    SELECT SCOPE_IDENTITY();";

                SqlCommand cmdCita = new SqlCommand(queryCita, conexionDB.GetConexion(), transaction);
                cmdCita.Parameters.AddWithValue("@fechaProgramada", fechaProgramada);
                cmdCita.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                cmdCita.Parameters.AddWithValue("@duracion", duracion);
                cmdCita.Parameters.AddWithValue("@idMascota", idMascota);
                cmdCita.Parameters.AddWithValue("@idMotivo", idMotivo);

                int idCita = Convert.ToInt32(cmdCita.ExecuteScalar());

                foreach (var servicio in listaServicios)
                {
                    int idEmpleado = ObtenerIdEmpleado(servicio.Empleado, transaction);

                    if (servicio.IdVacuna > 0)
                    {
                        string queryServicioCita = @"
                        INSERT INTO Servicio_Cita (idCita, idServicioEspecificoNieto, idVacuna, idEmpleado, hora)
                        VALUES (@idCita, NULL, @idVacuna, @idEmpleado, @hora)";
                        using (SqlCommand cmdServicioCita = new SqlCommand(queryServicioCita, conexionDB.GetConexion(), transaction))
                        {
                            cmdServicioCita.Parameters.AddWithValue("@idCita", idCita);
                            cmdServicioCita.Parameters.AddWithValue("@idVacuna", servicio.IdVacuna);
                            cmdServicioCita.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmdServicioCita.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                            cmdServicioCita.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        int idServicioNieto = ObtenerIdServicioNieto(servicio.NombreServicio, transaction);
                        string queryServicioCita = @"
                        INSERT INTO Servicio_Cita (idCita, idServicioEspecificoNieto, idEmpleado, hora)
                        VALUES (@idCita, @idServicioNieto, @idEmpleado, @hora)";
                        using (SqlCommand cmdServicioCita = new SqlCommand(queryServicioCita, conexionDB.GetConexion(), transaction))
                        {
                            cmdServicioCita.Parameters.AddWithValue("@idCita", idCita);
                            cmdServicioCita.Parameters.AddWithValue("@idServicioNieto", idServicioNieto);
                            cmdServicioCita.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmdServicioCita.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                            cmdServicioCita.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                MessageBox.Show("Cita agendada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CitasMedicas formularioHijo = new CitasMedicas(parentForm);
                parentForm.formularioHijo(formularioHijo);
            }
            catch (Exception ex)
            {
                if (txtDuracion.Text == "")
                {
                    MessageBox.Show("Error al agendar la cita: Falta un valor en Duracion de la cita  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Error al agendar la cita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            finally
            {
                conexionDB.CerrarConexion();
            }

            //CitasMedicas formularioHijo = new CitasMedicas(parentForm);
            //parentForm.formularioHijo(formularioHijo);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show($"Si no ha Agendado la Cita se eliminaran los datos ingresados", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                CitasMedicas formularioHijo = new CitasMedicas(parentForm);
                parentForm.formularioHijo(formularioHijo);
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

        private void cbDueño_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDueño.SelectedValue != null)
            {
                int idDueño;
                if (cbDueño.SelectedValue is DataRowView)
                {
                    DataRowView drv = (DataRowView)cbDueño.SelectedValue;
                    idDueño = Convert.ToInt32(drv["idPersona"]);
                }
                else
                {

                    idDueño = Convert.ToInt32(cbDueño.SelectedValue);
                }

                CargarMascotas(idDueño);
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

        private void CargarMascotas(int idDueño)
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = "SELECT idMascota, nombre FROM Mascota WHERE idPersona = @idDueño";
                SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion());
                cmd.Parameters.AddWithValue("@idDueño", idDueño);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cbMascota.DataSource = dt;
                cbMascota.DisplayMember = "nombre";
                cbMascota.ValueMember = "idMascota";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar mascotas: " + ex.Message);
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

        private void ActualizarDataGrid()
        {
            dtServicio.DataSource = null;
            dtServicio.DataSource = listaServicios;
            dtServicio.Refresh();

            if (dtServicio.Columns.Contains("EsVacuna"))
                dtServicio.Columns["EsVacuna"].Visible = false;
            if (dtServicio.Columns.Contains("IdVacuna"))
                dtServicio.Columns["IdVacuna"].Visible = false;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarServicioSeleccionado();
        }

        private void dtServicio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string servicio = dtServicio.Rows[e.RowIndex].Cells["NombreServicio"].Value.ToString();
                DialogResult resultado = MessageBox.Show($"¿Deseas eliminar el servicio '{servicio}'?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    listaServicios.RemoveAt(e.RowIndex);
                    ActualizarDataGrid();
                }
            }
        }
    }
    public class ServicioSeleccionado
    {
        public string NombreServicio { get; set; }
        public string Empleado { get; set; }
        public bool EsVacuna { get; set; }
        public int IdVacuna { get; set; }

        public ServicioSeleccionado(string nombre, string empleado, bool esVacuna = false, int idVacuna = 0)
        {
            NombreServicio = nombre;
            Empleado = empleado;
            EsVacuna = esVacuna;
            IdVacuna = idVacuna;
        }
    }
}
