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
    public partial class VeterinariaConsultarM : FormPadre
    {
        public int DatoCita { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        private List<ServicioSeleccionado> listaServicios = new List<ServicioSeleccionado>();
        public VeterinariaConsultarM(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void btnRecetar_Click(object sender, EventArgs e)
        {
            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaRecetar formularioHijo = new VeterinariaRecetar(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            parentForm.formularioHijo(formularioHijo);

            //parentForm.formularioHijo(new VeterinariaRecetar(parentForm));
        }

        private void VeterinariaConsultarM_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("Dato Recibido :" + DatoCita);
            CargarServiciosPadre();
            MostrarDatosBasicosCita();
        }
        private void MostrarDatosBasicosCita()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT 
                p.nombre AS NombreCliente, 
                p.apellidoP AS ApellidoPaterno, 
                p.celularPrincipal AS Telefono, 
                m.nombre AS NombreMascota, 
                e.nombre AS Especie, 
                r.nombre AS Raza, 
                m.esterilizado AS Esterilizado, 
                m.muerto AS Fallecido
            FROM Cita c
            INNER JOIN Mascota m ON c.idMascota = m.idMascota
            INNER JOIN Persona p ON m.idPersona = p.idPersona
            INNER JOIN Especie e ON m.idEspecie = e.idEspecie
            INNER JOIN Raza r ON m.idRaza = r.idRaza
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
                        txtMascota.Text = reader["NombreMascota"].ToString();
                        txtEspecie.Text = reader["Especie"].ToString();
                        txtRaza.Text = reader["Raza"].ToString();

                        cbCastrado.Checked = reader["Esterilizado"] != DBNull.Value && reader["Esterilizado"].ToString() == "S";
                        cbFallecido.Checked = reader["Fallecido"] != DBNull.Value && reader["Fallecido"].ToString() == "S";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos básicos de la cita: " + ex.Message);
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
            string query = "SELECT nombre FROM ServicioEspecificoHijo WHERE idServicioPadre = @idServicioPadre";

            try
            {
                conexionDB.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idServicioPadre", idServicioPadre);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbServicioEspecifico.Items.Clear(); // Limpiar ComboBox antes de cargar nuevos valores

                    while (reader.Read())
                    {
                        cbServicioEspecifico.Items.Add(reader["nombre"].ToString());
                    }

                    reader.Close();
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
            string query = "SELECT nombre FROM ServicioEspecificoNieto WHERE idServicioEspecificoHijo = @idServicioHijo";

            try
            {
                conexionDB.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idServicioHijo", idServicioHijo);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbServicioNieto.Items.Clear(); // Limpiar ComboBox antes de cargar nuevos valores

                    while (reader.Read())
                    {
                        cbServicioNieto.Items.Add(reader["nombre"].ToString());
                    }

                    reader.Close();
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

        private void cbServicioEspecifico_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbServicioNieto.Text = "";
            if (cbServicioEspecifico.SelectedItem != null)
            {
                string servicioHijoSeleccionado = cbServicioEspecifico.Text; 

                string query = "SELECT idServicioEspecificoHijo FROM ServicioEspecificoHijo WHERE nombre = @nombre";

                try
                {
                    conexionDB.AbrirConexion();
                    using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                    {
                        cmd.Parameters.AddWithValue("@nombre", servicioHijoSeleccionado);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int idServicioHijo = Convert.ToInt32(result);

                            CargarServiciosNietos(idServicioHijo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el ID del servicio hijo: " + ex.Message);
                }
                finally
                {
                    conexionDB.CerrarConexion();
                }
            }
        }

        private void cbServicioNieto_SelectedIndexChanged(object sender, EventArgs e)
        {

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

      
        private void AgregarServicioALista()
        {
            string servicioSeleccionado = cbServicioP.Text; // Obtener el nombre del servicio padre seleccionado

            // Permitir agregar "Consulta General" sin necesidad de servicios hijo o nieto
            if (servicioSeleccionado == "Consulta General")
            {
                int idPadre = Convert.ToInt32(cbServicioP.SelectedValue);

                listaServicios.Add(new ServicioSeleccionado
                {
                    IdServicioPadre = idPadre,
                    NombreServicioPadre = servicioSeleccionado,
                    IdServicioHijo = -1,  // Indicar que no tiene hijo
                    NombreServicioHijo = "",
                    IdServicioNieto = -1, // Indicar que no tiene nieto
                    NombreServicioNieto = "",
                    Observacion = rtObservacion.Text
                });

                ActualizarDataGridView();
                return; // Salir de la función sin validar más
            }

            // Validación para otros servicios
            if (cbServicioP.SelectedItem == null || cbServicioEspecifico.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar al menos un servicio padre y un servicio hijo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPadreNormal = Convert.ToInt32(cbServicioP.SelectedValue);
            string nombrePadreNormal = cbServicioP.Text;

            int idHijo = ObtenerIdServicioHijo(cbServicioEspecifico.Text);
            string nombreHijo = cbServicioEspecifico.Text;

            int idNieto = -1;
            string nombreNieto = "";

            if (cbServicioNieto.SelectedItem != null)
            {
                idNieto = ObtenerIdServicioNieto(cbServicioNieto.Text);
                nombreNieto = cbServicioNieto.Text;
            }

            string observacion = rtObservacion.Text;

            listaServicios.Add(new ServicioSeleccionado
            {
                IdServicioPadre = idPadreNormal,
                NombreServicioPadre = nombrePadreNormal,
                IdServicioHijo = idHijo,
                NombreServicioHijo = nombreHijo,
                IdServicioNieto = idNieto,
                NombreServicioNieto = nombreNieto,
                Observacion = observacion
            });

            ActualizarDataGridView();
        }
        private void ActualizarDataGridView()
        {
            dtServicios.DataSource = null;
            dtServicios.DataSource = listaServicios;

            dtServicios.Columns["IdServicioPadre"].Visible = false;
            dtServicios.Columns["IdServicioHijo"].Visible = false;
            dtServicios.Columns["IdServicioNieto"].Visible = false;
        }
        private int ObtenerIdServicioNieto(string nombreServicioNieto)
        {
            string query = "SELECT idServicioEspecificoNieto FROM ServicioEspecificoNieto WHERE nombre = @nombre";
            try
            {
                conexionDB.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreServicioNieto);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el ID del servicio nieto: " + ex.Message);
                return -1;
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private int ObtenerIdServicioHijo(string nombreServicioHijo)
        {
            int idServicioHijo = -1; 

            string query = "SELECT idServicioEspecificoHijo FROM ServicioEspecificoHijo WHERE nombre = @nombre";

            try
            {
                conexionDB.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreServicioHijo);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el ID del servicio hijo: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }

            return idServicioHijo;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarServicioALista();
        }

        private void InsertarConsulta()
        {
            try
            {
                // Abrimos la conexión
                conexionDB.AbrirConexion();

                // Consulta SQL para la inserción
                string query = @"INSERT INTO Consulta (diagnostico, peso, temperatura, idCita, FechaConsulta, MotivoConsulta, EstudioEspecial) 
                         VALUES (@diagnostico, @peso, @temperatura, @idCita, @fechaConsulta, @motivoConsulta, @estudioEspecial)";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    // Asignación de parámetros desde los controles del formulario
                    cmd.Parameters.AddWithValue("@diagnostico", rtDiagnostico.Text.Trim());
                    cmd.Parameters.AddWithValue("@peso", Convert.ToDecimal(txtPeso.Text));
                    cmd.Parameters.AddWithValue("@temperatura", Convert.ToDecimal(txtTemperatura.Text));
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    cmd.Parameters.AddWithValue("@fechaConsulta", dtFechaConsulta.Value.Date);
                    cmd.Parameters.AddWithValue("@motivoConsulta", rtMotivo.Text.Trim());

                    // Si el campo EstudioEspecial está vacío, se inserta NULL en la base de datos
                    if (string.IsNullOrWhiteSpace(rtEstudioEspecial.Text))
                        cmd.Parameters.AddWithValue("@estudioEspecial", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@estudioEspecial", rtEstudioEspecial.Text.Trim());

                    // Ejecutar la consulta
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Consulta registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar la consulta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Error en el formato de los valores numéricos. Verifique los datos ingresados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void InsertarServiciosEnCita()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"INSERT INTO Servicio_Cita (idCita, hora, idServicioSencilloHijo, idServicioEspecificoNieto, idEmpleado, observaciones)
                         VALUES (@idCita, @hora, @idServicioSencilloHijo, @idServicioEspecificoNieto, @idEmpleado, @observaciones)";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    foreach (var servicio in listaServicios)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idCita", DatoCita);
                        cmd.Parameters.AddWithValue("@hora", DateTime.Now.ToString("HH:mm:ss")); // Usa la hora actual o ajusta según necesites
                        cmd.Parameters.AddWithValue("@idEmpleado", 1); // Reemplaza con tu lógica para obtener el ID del empleado
                        cmd.Parameters.AddWithValue("@observaciones", string.IsNullOrEmpty(servicio.Observacion) ? DBNull.Value : (object)servicio.Observacion);

                        if (servicio.IdServicioHijo == 4) // Solo "Consulta General" tiene idServicioSencilloHijo = 4
                        {
                            cmd.Parameters.AddWithValue("@idServicioSencilloHijo", 4);
                            cmd.Parameters.AddWithValue("@idServicioEspecificoNieto", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@idServicioSencilloHijo", DBNull.Value);
                            cmd.Parameters.AddWithValue("@idServicioEspecificoNieto", servicio.IdServicioNieto);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Servicios agregados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar los servicios en la cita: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            InsertarConsulta();
            InsertarServiciosEnCita();
        }
    }
    public class ServicioSeleccionado
    {
        public int IdServicioPadre { get; set; }
        public string NombreServicioPadre { get; set; }
        public int IdServicioHijo { get; set; }
        public string NombreServicioHijo { get; set; }
        public int IdServicioNieto { get; set; }
        public string NombreServicioNieto { get; set; }
        public string Observacion { get; set; }
    }
}
