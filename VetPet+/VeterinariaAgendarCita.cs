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
                    cbServicioEspecifico.ValueMember = "idServicioEspecificoHijo";  // <-- Aquí guardamos el ID
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

        private void cbServicioEspecifico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbServicioEspecifico.SelectedValue != null)
            {
                int idServicioEspecificoHijo;

                // Verifica si el SelectedValue es un DataRowView y extrae el id
                if (cbServicioEspecifico.SelectedValue is DataRowView)
                {
                    DataRowView drv = (DataRowView)cbServicioEspecifico.SelectedValue;
                    idServicioEspecificoHijo = Convert.ToInt32(drv["idServicioEspecificoHijo"]);
                }
                else
                {
                    idServicioEspecificoHijo = Convert.ToInt32(cbServicioEspecifico.SelectedValue);
                }

                // Llamar a la función para cargar los servicios nietos con el ID seleccionado
                CargarServiciosNietos(idServicioEspecificoHijo);
            }
        }

        private void cbDueño_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDueño.SelectedValue != null)
            {
                int idDueño;
                // Si el SelectedValue es un DataRowView, extraemos el valor de la columna "idPersona"
                if (cbDueño.SelectedValue is DataRowView)
                {
                    DataRowView drv = (DataRowView)cbDueño.SelectedValue;
                    idDueño = Convert.ToInt32(drv["idPersona"]);
                }
                else
                {
                    // Si ya es un valor simple (int o string que representa un entero)
                    idDueño = Convert.ToInt32(cbDueño.SelectedValue);
                }
                // Ahora puedes usar el idDueño, por ejemplo para cargar las mascotas
                CargarMascotas(idDueño);
            }
        }

        // Carga las Mascotas del dueño seleccionado
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

        // Carga los motivos en cbMotivo
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

        // Carga los empleados en cbEmpleado (solo los que tengan idTipoEmpleado 1 o 3)
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

            // Verifica si hay un servicio nieto seleccionado
            if (cbServicioNieto.SelectedItem != null)
            {
                nombreServicio = cbServicioNieto.Text;
            }
            else if (cbServicioEspecifico.SelectedItem != null) // Si no hay nieto, intenta con el específico
            {
                nombreServicio = cbServicioEspecifico.Text;
            }
            else if (cbServicioP.SelectedItem != null) // Si no hay específico, intenta con el padre
            {
                nombreServicio = cbServicioP.Text;
            }

            if (!string.IsNullOrEmpty(nombreServicio))
            {
                // Verifica que el servicio no esté duplicado en la lista
                if (!listaServicios.Any(s => s.NombreServicio == nombreServicio))
                {
                    listaServicios.Add(new ServicioSeleccionado(nombreServicio));
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
    }
    public class ServicioSeleccionado
    {
        public string NombreServicio { get; set; }

        public ServicioSeleccionado(string nombre)
        {
            NombreServicio = nombre;
        }
    }
}
