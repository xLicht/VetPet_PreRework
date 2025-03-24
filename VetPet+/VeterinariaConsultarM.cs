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
        private List<ServicioSeleccionadoConsulta> listaServicios = new List<ServicioSeleccionadoConsulta>();
        int idConsultaCreada = 0;
        public VeterinariaConsultarM(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void btnRecetar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Debe guardar la consulta antes de recetar. ¿Ya la ha guardado?","Advertencia",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (resultado == DialogResult.No)
            {
                MessageBox.Show("Por favor, guarde la consulta antes de proceder.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaRecetar formularioHijo = new VeterinariaRecetar(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            formularioHijo.DatoConsulta = idConsultaCreada;
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
            if (cbServicioP.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar al menos un servicio padre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPadre = Convert.ToInt32(cbServicioP.SelectedValue);
            string nombrePadre = cbServicioP.Text;

            int idHijo = -1;
            string nombreHijo = "";
            int idNieto = -1;
            string nombreNieto = "";

            if (nombrePadre == "Consulta General")
            {
                idNieto = 4;
                nombreNieto = "Consulta General";
            }
            else
            {
                if (cbServicioEspecifico.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un servicio hijo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                idHijo = Convert.ToInt32(cbServicioEspecifico.SelectedValue);
                nombreHijo = cbServicioEspecifico.Text;

                if (cbServicioNieto.SelectedItem != null)
                {
                    idNieto = Convert.ToInt32(cbServicioNieto.SelectedValue);
                    nombreNieto = cbServicioNieto.Text;
                }
            }


            string observacion = rtObservacion.Text.Trim();

            string nombreServicioFinal = !string.IsNullOrEmpty(nombreNieto) ? nombreNieto : nombrePadre;

     
            string empleado = "Ninguno";

            listaServicios.Add(new ServicioSeleccionadoConsulta(nombreServicioFinal, empleado, false, 0, observacion));

            ActualizarDataGrid();
        }




        //private void ActualizarDataGrid()
        //{
        //    dtServicios.DataSource = null;
        //    dtServicios.DataSource = listaServicios;
        //    dtServicios.Refresh();

        //    if (dtServicios.Columns.Contains("EsVacuna"))
        //        dtServicios.Columns["EsVacuna"].Visible = false;
        //    if (dtServicios.Columns.Contains("IdVacuna"))
        //        dtServicios.Columns["IdVacuna"].Visible = false;
        //    // La columna "Observacion" se dejará visible para mostrar el detalle.
        //}

        private void ActualizarDataGrid()
        {
            dtServicios.DataSource = null;
            dtServicios.DataSource = listaServicios;
            dtServicios.Refresh();

            if (dtServicios.Columns.Contains("EsVacuna"))
                dtServicios.Columns["EsVacuna"].Visible = false;
            if (dtServicios.Columns.Contains("IdVacuna"))
                dtServicios.Columns["IdVacuna"].Visible = false;
            if (dtServicios.Columns.Contains("Empleado"))
                dtServicios.Columns["Empleado"].Visible = false;
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

        //private void InsertarConsulta()
        //{
        //    try
        //    {
        //        conexionDB.AbrirConexion();
        //        //string query = @"INSERT INTO Consulta (diagnostico, peso, temperatura, idCita, FechaConsulta, MotivoConsulta, EstudioEspecial) 
        //        //         VALUES (@diagnostico, @peso, @temperatura, @idCita, @fechaConsulta, @motivoConsulta, @estudioEspecial)";
        //        string query = @"INSERT INTO Consulta (diagnostico, peso, temperatura, idCita, FechaConsulta, MotivoConsulta, EstudioEspecial) 
        //                 VALUES (@diagnostico, @peso, @temperatura, @idCita, @fechaConsulta, @motivoConsulta, @estudioEspecial);
        //                 SELECT SCOPE_IDENTITY();"; 

        //        using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
        //        {
        //            cmd.Parameters.AddWithValue("@diagnostico", rtDiagnostico.Text.Trim());
        //            cmd.Parameters.AddWithValue("@peso", Convert.ToDecimal(txtPeso.Text));
        //            cmd.Parameters.AddWithValue("@temperatura", Convert.ToDecimal(txtTemperatura.Text));
        //            cmd.Parameters.AddWithValue("@idCita", DatoCita);
        //            cmd.Parameters.AddWithValue("@fechaConsulta", dtFechaConsulta.Value.Date);
        //            cmd.Parameters.AddWithValue("@motivoConsulta", rtMotivo.Text.Trim());

        //            if (string.IsNullOrWhiteSpace(rtEstudioEspecial.Text))
        //                cmd.Parameters.AddWithValue("@estudioEspecial", DBNull.Value);
        //            else
        //                cmd.Parameters.AddWithValue("@estudioEspecial", rtEstudioEspecial.Text.Trim());

        //            int filasAfectadas = cmd.ExecuteNonQuery();

        //            if (filasAfectadas > 0)
        //            {
        //                MessageBox.Show("Consulta registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                // Ejecuta la consulta y obtiene el ID de la consulta insertada
        //                object result = cmd.ExecuteScalar();
        //                if (result != null)
        //                {
        //                    idConsultaCreada = Convert.ToInt32(result);
        //                    MessageBox.Show("Consulta guardada con éxito. ID: " + idConsultaCreada, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    //MessageBox.Show("consulta:"+idConsultaCreada);
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("No se pudo registrar la consulta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //    catch (FormatException)
        //    {
        //        MessageBox.Show("Error en el formato de los valores numéricos. Verifique los datos ingresados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al insertar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        conexionDB.CerrarConexion();
        //    }
        //}

        private void InsertarConsulta()
        {
            try
            {
                conexionDB.AbrirConexion();
                // Se eliminan los campos "FechaConsulta" y "MotivoConsulta"
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

                    // Obtener el ID de la consulta insertada
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idConsultaCreada = Convert.ToInt32(result);
                        MessageBox.Show("Consulta guardada con éxito. ID: " + idConsultaCreada, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
      
            dtServicios.DataSource = null;
            dtServicios.Rows.Clear();
            dtServicios.Refresh();

            listaServicios.Clear();
            cbServicioP.SelectedIndex = 0;
            cbServicioEspecifico.SelectedIndex = -1;
            cbServicioNieto.SelectedIndex = -1;

        }

        //private void InsertarServiciosEnConsulta()
        //{
        //    if (listaServicios.Count == 0)
        //        return; // No hay servicios para insertar

        //    try
        //    {
        //        conexionDB.AbrirConexion();

        //        foreach (var servicio in listaServicios)
        //        {
        //            string query = string.Empty;
        //            SqlCommand cmd = null;

        //            if (servicio.EsVacuna)
        //            {
        //                // Para vacunas: se inserta el idVacuna y se deja nulo el idServicioEspecificoNieto
        //                query = @"INSERT INTO Servicio_Consulta (idConsulta, observacion, idServicioEspecificoNieto, idVacuna)
        //                  VALUES (@idConsulta, @observacion, NULL, @idVacuna)";
        //                cmd = new SqlCommand(query, conexionDB.GetConexion());
        //                cmd.Parameters.AddWithValue("@idVacuna", servicio.IdVacuna);
        //            }
        //            else
        //            {
        //                // Para otros servicios: se obtiene el idServicioEspecificoNieto en base al nombre del servicio
        //                int idServicioNieto = ObtenerIdServicioNieto(servicio.NombreServicio);
        //                query = @"INSERT INTO Servicio_Consulta (idConsulta, observacion, idServicioEspecificoNieto, idVacuna)
        //                  VALUES (@idConsulta, @observacion, @idServicioNieto, NULL)";
        //                cmd = new SqlCommand(query, conexionDB.GetConexion());
        //                cmd.Parameters.AddWithValue("@idServicioNieto", idServicioNieto);
        //            }

        //            // Parámetros comunes
        //            cmd.Parameters.AddWithValue("@idConsulta", idConsultaCreada);
        //            cmd.Parameters.AddWithValue("@observacion", servicio.Observacion ?? string.Empty);

        //            cmd.ExecuteNonQuery();
        //        }

        //        MessageBox.Show("Servicios y observaciones registrados en la consulta.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al insertar servicios en consulta: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexionDB.CerrarConexion();
        //    }
        //}
        private void InsertarServiciosEnConsulta()
        {
            if (listaServicios.Count == 0)
                return; // No hay servicios para insertar

            try
            {

                conexionDB.AbrirConexion();

                foreach (var servicio in listaServicios)
                {
                    string query = string.Empty;
                    SqlCommand cmd = null;

                    if (servicio.EsVacuna)
                    {
                        // Para vacunas, se inserta el idVacuna y se deja nulo el idServicioEspecificoNieto
                        query = @"INSERT INTO Servicio_Consulta 
                          (idConsulta, observacion, idServicioEspecificoNieto, idVacuna)
                          VALUES (@idConsulta, @observacion, NULL, @idVacuna)";
                        cmd = new SqlCommand(query, conexionDB.GetConexion());
                        cmd.Parameters.AddWithValue("@idVacuna", servicio.IdVacuna);
                        conexionDB.AbrirConexion();
                    }
                    else
                    {
                        // Para otros servicios, se obtiene el idServicioEspecificoNieto en base al nombre del servicio
                        int idServicioNieto = ObtenerIdServicioNieto(servicio.NombreServicio);
                        query = @"INSERT INTO Servicio_Consulta 
                          (idConsulta, observacion, idServicioEspecificoNieto, idVacuna)
                          VALUES (@idConsulta, @observacion, @idServicioNieto, NULL)";
                        cmd = new SqlCommand(query, conexionDB.GetConexion());
                        cmd.Parameters.AddWithValue("@idServicioNieto", idServicioNieto);
                        conexionDB.AbrirConexion();
                    }

                    // Parámetros comunes
                    cmd.Parameters.AddWithValue("@idConsulta", idConsultaCreada);
                    cmd.Parameters.AddWithValue("@observacion", servicio.Observacion ?? string.Empty);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Servicios y observaciones registrados en la consulta.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar servicios en consulta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            InsertarConsulta();
            InsertarServiciosEnConsulta();
            LimpiarCampos();
        }

        private void dtServicios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DialogResult confirmacion = MessageBox.Show("¿Desea eliminar este servicio?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    listaServicios.RemoveAt(e.RowIndex);
                    ActualizarDataGrid();
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
