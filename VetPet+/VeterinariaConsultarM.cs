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
using static VetPet_.CitaMascota;

namespace VetPet_
{
    public partial class VeterinariaConsultarM : FormPadre
    {
        public int DatoCita { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        //private List<ServicioSeleccionadoConsulta> listaServicios = new List<ServicioSeleccionadoConsulta>();
        private List<ServicioSeleccionado> listaServicios = new List<ServicioSeleccionado>();
        private List<ServicioCitaKey> serviciosInactivados = new List<ServicioCitaKey>();
        int idConsultaCreada = 0;
        int validador = 0;
        private int guardadosCount = 0;
        public VeterinariaConsultarM(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void btnRecetar_Click(object sender, EventArgs e)
        {
            if (idConsultaCreada == 0)
            {
                MessageBox.Show("Primero se debe guardar la consulta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaRecetar formularioHijo = new VeterinariaRecetar(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            formularioHijo.DatoConsulta = idConsultaCreada;
            parentForm.formularioHijo(formularioHijo);

        }

        private void VeterinariaConsultarM_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("Dato Recibido :" + DatoCita);
            CargarServiciosPadre();
            MostrarDatosBasicosCita();
            //CargarServiciosConsulta();
            CargarEmpleados();
            CargarServiciosCita();
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
                    m.muerto AS Fallecido,
                    mo.nombre AS MotivoConsulta
                FROM Cita c
                INNER JOIN Mascota m ON c.idMascota = m.idMascota
                INNER JOIN Persona p ON m.idPersona = p.idPersona
                INNER JOIN Especie e ON m.idEspecie = e.idEspecie
                INNER JOIN Raza r ON m.idRaza = r.idRaza
                INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
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

                        rtMotivo.Text = reader["MotivoConsulta"].ToString();

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
        
        }
        private void ActualizarDataGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                conexionDB.AbrirConexion();
                string query = @"
            SELECT 
                sc.idCita, 
                sc.hora, 
                sc.idServicioEspecificoNieto,
                sc.idVacuna,
                sc.idEmpleado,
                sc.estado,
                sc.observacion,
                COALESCE(v.nombre, sen.nombre) AS NombreServicio,
                e.usuario AS Empleado
            FROM Servicio_Cita sc
            LEFT JOIN Vacuna v ON sc.idVacuna = v.idVacuna
            LEFT JOIN ServicioEspecificoNieto sen ON sc.idServicioEspecificoNieto = sen.idServicioEspecificoNieto
            INNER JOIN Empleado e ON sc.idEmpleado = e.idEmpleado
            WHERE sc.idCita = @idCita AND sc.estado = 'A'";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios existentes: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }

            // Filtrar los registros que están marcados para inactivación (serviciosInactivados)
            // Se recorre el DataTable y se elimina la fila si coincide con algún servicio marcado
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dt.Rows[i];
                // Se crea un objeto clave a partir de los valores de la fila
                int idCita = Convert.ToInt32(row["idCita"]);
                TimeSpan hora = TimeSpan.Parse(row["hora"].ToString());
                int idEmpleado = Convert.ToInt32(row["idEmpleado"]);
                int? idServicioNieto = row["idServicioEspecificoNieto"] != DBNull.Value ? Convert.ToInt32(row["idServicioEspecificoNieto"]) : (int?)null;
                int? idVacuna = row["idVacuna"] != DBNull.Value ? Convert.ToInt32(row["idVacuna"]) : (int?)null;

                // Si existe un servicio en la lista de eliminados que coincide, se elimina la fila
                bool eliminado = serviciosInactivados.Any(s =>
                                s.IdCita == idCita &&
                                s.Hora == hora &&
                                s.IdEmpleado == idEmpleado &&
                                s.IdServicioEspecificoNieto == idServicioNieto &&
                                s.IdVacuna == idVacuna);
                if (eliminado)
                {
                    dt.Rows.RemoveAt(i);
                }
            }

            // Asegurarse de que existan las columnas necesarias
            if (!dt.Columns.Contains("NombreServicio"))
                dt.Columns.Add("NombreServicio", typeof(string));
            if (!dt.Columns.Contains("Empleado"))
                dt.Columns.Add("Empleado", typeof(string));

            // Agregar columna oculta para identificar el servicio en la lista (solo para servicios nuevos)
            if (!dt.Columns.Contains("Indice"))
                dt.Columns.Add("Indice", typeof(int));

            // Agregar los nuevos servicios (almacenados en listaServicios) al DataTable
            foreach (var servicio in listaServicios)
            {
                DataRow row = dt.NewRow();
                row["idCita"] = DatoCita;
                row["hora"] = DateTime.Now.TimeOfDay; //aqui tambien le movi 
                row["idServicioEspecificoNieto"] = servicio.EsVacuna ? (object)DBNull.Value : (object)0;
                row["idVacuna"] = servicio.EsVacuna ? servicio.IdVacuna : (object)DBNull.Value;
                row["idEmpleado"] = 0;
                row["estado"] = "A";
                row["observacion"] = rtObservacion.Text.Trim(); //aqui le movi
                row["NombreServicio"] = servicio.NombreServicio;
                row["Empleado"] = servicio.Empleado;
                int indice = listaServicios.IndexOf(servicio);
                row["Indice"] = indice;
                dt.Rows.Add(row);
            }

            // Asignar el DataTable al DataGridView y refrescar
            dtServicio.DataSource = dt;
            dtServicio.Refresh();

            // Ocultar columnas de control
            if (dtServicio.Columns.Contains("hora"))
                dtServicio.Columns["hora"].Visible = false;
            if (dtServicio.Columns.Contains("estado"))
                dtServicio.Columns["estado"].Visible = false;
            if (dtServicio.Columns.Contains("idCita"))
                dtServicio.Columns["idCita"].Visible = false;
            if (dtServicio.Columns.Contains("idServicioEspecificoNieto"))
                dtServicio.Columns["idServicioEspecificoNieto"].Visible = false;
            if (dtServicio.Columns.Contains("idVacuna"))
                dtServicio.Columns["idVacuna"].Visible = false;
            if (dtServicio.Columns.Contains("idEmpleado"))
                dtServicio.Columns["idEmpleado"].Visible = false;
            if (dtServicio.Columns.Contains("observacion"))
                dtServicio.Columns["observacion"].Visible = false;
            if (dtServicio.Columns.Contains("Indice"))
                dtServicio.Columns["Indice"].Visible = false;

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
            //AgregarServicioALista();
            AgregarServicioSeleccionado();
        }
        private void InsertarConsulta()
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"INSERT INTO Consulta 
                         (diagnostico, peso, temperatura, idCita, EstudioEspecial) 
                         VALUES (@diagnostico, @peso, @temperatura, @idCita, @estudioEspecial);
                         SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@diagnostico", rtDiagnostico.Text.Trim());
                    cmd.Parameters.AddWithValue("@peso", Convert.ToDecimal(txtPeso.Text));
                    cmd.Parameters.AddWithValue("@temperatura", Convert.ToDecimal(txtTemperatura.Text));
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    cmd.Parameters.AddWithValue("@estudioEspecial",
                        string.IsNullOrWhiteSpace(rtEstudioEspecial.Text) ? (object)DBNull.Value : rtEstudioEspecial.Text.Trim());

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idConsultaCreada = Convert.ToInt32(result);
                        validador ++;
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

        private void LimpiarCampos()
        {
            txtPeso.Text = "";
            txtTemperatura.Text = "";
            rtMotivo.Text = "";
            rtDiagnostico.Text = "";
            rtObservacion.Text = "";
            rtEstudioEspecial.Text = "";
      
            dtServicio.DataSource = null;
            dtServicio.Rows.Clear();
            dtServicio.Refresh();

            listaServicios.Clear();
            cbServicioP.SelectedIndex = 0;
            cbServicioEspecifico.SelectedIndex = -1;
            cbServicioNieto.SelectedIndex = -1;

        }

        private void InsertarServiciosEnConsulta()
        {

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            InsertarConsulta();
            InsertarServiciosEnConsulta();
            if (validador == 2)
            {
                LimpiarCampos();
            }
            
        }

        private void dtServicios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex < guardadosCount)
                {
                    ////MessageBox.Show("No se puede eliminar un servicio ya guardado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //EliminarServicioDeBD();
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

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show("los cambios no guardados se borraran", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                int idMascotaSeleccionada = Convert.ToInt32(DatoCita);
                ConsultarCita formularioHijo = new ConsultarCita(parentForm);
                formularioHijo.DatoCita = DatoCita;
                parentForm.formularioHijo(formularioHijo);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void CargarServiciosConsulta()
        {
            
        }

        private void EliminarServicioDeBD(int idCita, TimeSpan hora, int? idVacuna, int? idServicioNieto)
        {
            try
            {
                conexionDB.AbrirConexion();
                string query = @"
            UPDATE Servicio_Cita 
            SET estado = 'I'
            WHERE idCita = @idCita 
              AND hora = @hora 
              AND estado = 'A'
              AND (
                    (@idVacuna IS NOT NULL AND idVacuna = @idVacuna)
                 OR (@idVacuna IS NULL AND @idServicioNieto IS NOT NULL AND idServicioEspecificoNieto = @idServicioNieto)
              )";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", idCita);
                    cmd.Parameters.AddWithValue("@hora", hora);
                    // Si el servicio es una vacuna, idVacuna tendrá valor; de lo contrario, se utiliza el id del servicio nieto.
                    if (idVacuna.HasValue)
                        cmd.Parameters.AddWithValue("@idVacuna", idVacuna.Value);
                    else
                        cmd.Parameters.AddWithValue("@idVacuna", DBNull.Value);
                    if (idServicioNieto.HasValue)
                        cmd.Parameters.AddWithValue("@idServicioNieto", idServicioNieto.Value);
                    else
                        cmd.Parameters.AddWithValue("@idServicioNieto", DBNull.Value);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Servicio eliminado exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el servicio o ya fue eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void CargarServiciosCita()
        {
            
            try
            {
                conexionDB.AbrirConexion();

                string query = @"
            SELECT 
                sc.idCita, 
                sc.hora, 
                sc.idServicioEspecificoNieto,
                sc.idVacuna,
                sc.idEmpleado,
                sc.estado,
                sc.observacion,
                COALESCE(v.nombre, sen.nombre) AS NombreServicio,
                e.usuario AS Empleado
            FROM Servicio_Cita sc
            LEFT JOIN Vacuna v ON sc.idVacuna = v.idVacuna
            LEFT JOIN ServicioEspecificoNieto sen ON sc.idServicioEspecificoNieto = sen.idServicioEspecificoNieto
            INNER JOIN Empleado e ON sc.idEmpleado = e.idEmpleado
            WHERE sc.idCita = @idCita AND sc.estado = 'A'";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dtServicio.DataSource = dt;

                    // Ocultar columnas de control si no deseas mostrarlas al usuario.
                    if (dtServicio.Columns.Contains("hora"))
                        dtServicio.Columns["hora"].Visible = false;
                    if (dtServicio.Columns.Contains("estado"))
                        dtServicio.Columns["estado"].Visible = false;
                    if (dtServicio.Columns.Contains("idCita"))
                        dtServicio.Columns["idCita"].Visible = false;
                    if (dtServicio.Columns.Contains("idServicioEspecificoNieto"))
                        dtServicio.Columns["idServicioEspecificoNieto"].Visible = false;
                    if (dtServicio.Columns.Contains("idVacuna"))
                        dtServicio.Columns["idVacuna"].Visible = false;
                    if (dtServicio.Columns.Contains("idEmpleado"))
                        dtServicio.Columns["idEmpleado"].Visible = false;
                    //if (dtServicio.Columns.Contains("observacion"))
                    //    dtServicio.Columns["observacion"].Visible = false;
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
        private void AgregarServicioSeleccionado()
        {
            string nombreServicio = "";
            bool esVacuna = false;
            int idVacuna = 0;
            string obser = "";// aqui le Movi

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
                //int idMascota = Convert.ToInt32(cbMascota.SelectedValue);
                //if (VacunaYaAplicada(idMascota, idVacuna))
                //{
                //    MessageBox.Show("La mascota ya tiene aplicada esta vacuna.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
            }
            
            if (!string.IsNullOrEmpty(nombreServicio))
            {
                string empleadoSeleccionado = cbEmpleado.Text;
                if (!listaServicios.Any(s => s.NombreServicio.Equals(nombreServicio, StringComparison.OrdinalIgnoreCase)))
                {
                    listaServicios.Add(new ServicioSeleccionado(nombreServicio, empleadoSeleccionado, obser, esVacuna, idVacuna));
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
                    //MessageBox.Show($"idMascota: {idMascota}, idVacuna: {idVacuna}, count: {count}");
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


    }
    public class ServicioSeleccionadoConsulta
    {
        public string NombreServicio { get; set; }
        public string Empleado { get; set; }
        public bool EsVacuna { get; set; }
        public int IdVacuna { get; set; }
        public string Observacion { get; set; } 

        public ServicioSeleccionadoConsulta(string nombre, string empleado, bool esVacuna = false, int idVacuna = 0, string observacion = "")
        {
            NombreServicio = nombre;
            Empleado = empleado;
            EsVacuna = esVacuna;
            IdVacuna = idVacuna;
            Observacion = observacion;
        }
    }
}
