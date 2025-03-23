using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
                    int idServicioNieto = ObtenerIdServicioNieto(servicio.NombreServicio, transaction);
                    int idEmpleado = ObtenerIdEmpleado(servicio.Empleado, transaction);

                    string queryServicioCita = @"
                        INSERT INTO Servicio_Cita (idCita, idServicioEspecificoNieto, idEmpleado, hora)
                        VALUES (@idCita, @idServicioNieto, @idEmpleado, @hora);";

                    SqlCommand cmdServicioCita = new SqlCommand(queryServicioCita, conexionDB.GetConexion(), transaction);
                    cmdServicioCita.Parameters.AddWithValue("@idCita", idCita);
                    cmdServicioCita.Parameters.AddWithValue("@idServicioNieto", idServicioNieto);
                    cmdServicioCita.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    cmdServicioCita.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                    cmdServicioCita.ExecuteNonQuery();
                }

                // 4️⃣ Confirmar la transacción
                transaction.Commit();
                MessageBox.Show("Cita agendada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agendar la cita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }

            CitasMedicas formularioHijo = new CitasMedicas(parentForm);
            parentForm.formularioHijo(formularioHijo);
        }
    

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            CitasMedicas formularioHijo = new CitasMedicas(parentForm);
            parentForm.formularioHijo(formularioHijo);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarServicioSeleccionado();
        }

        private void dtServicio_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                string servicio = dtServicio.Rows[e.RowIndex].Cells["NombreServicio"].Value.ToString();
                DialogResult resultado = MessageBox.Show($"¿Deseas eliminar el servicio '{servicio}'?", "Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

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

        public ServicioSeleccionado(string nombre, string empleado)
        {
            NombreServicio = nombre;
            Empleado = empleado;
        }
    }
}
