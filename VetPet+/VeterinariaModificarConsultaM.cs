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
        //private List<ServicioSeleccionadoConsulta> listaServicios = new List<ServicioSeleccionadoConsulta>();
        private List<ServicioSeleccionado> listaServicios = new List<ServicioSeleccionado>();
        private List<ServicioCitaKey> serviciosInactivados = new List<ServicioCitaKey>();
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
            //CargarServiciosConsulta();
            CargarEmpleados();
            CargarServiciosCita();
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
            InsertarServiciosEnConsulta();

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
                            diagnostico = @diagnostico, 
                            EstudioEspecial = @estudioEspecial
                        WHERE idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@peso", Convert.ToDecimal(txtPeso.Text));
                    cmd.Parameters.AddWithValue("@temperatura", Convert.ToDecimal(txtTemperatura.Text));
                    //cmd.Parameters.AddWithValue("@motivo", txtMotivo.Text);
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
                dtServicio.DataSource = null;
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
                    dtServicio.DataSource = dt;
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
            //AgregarServicioALista();
            AgregarServicioSeleccionado();
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
            //if (dtServicio.Columns.Contains("observacion"))
            //    dtServicio.Columns["observacion"].Visible = false;
            if (dtServicio.Columns.Contains("Indice"))
                dtServicio.Columns["Indice"].Visible = false;
        }

        private void dtServicios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dtServicio.Rows[e.RowIndex].DataBoundItem;
                // Primero, verificamos si es un objeto DataRowView
                if (fila is DataRowView filaDatos)
                {
                    // Verificamos si la columna "Indice" existe y contiene un valor
                    if (filaDatos.Row.Table.Columns.Contains("Indice") && filaDatos["Indice"] != DBNull.Value)
                    {
                        int indice = Convert.ToInt32(filaDatos["Indice"]);
                        // Confirmamos eliminación para servicios nuevos
                        DialogResult resultado = MessageBox.Show(
                            $"¿Deseas eliminar el servicio '{filaDatos["NombreServicio"]}'?",
                            "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            // Eliminamos el servicio de la lista utilizando el índice obtenido
                            // Es importante verificar que el índice sea válido
                            if (indice >= 0 && indice < listaServicios.Count)
                            {
                                listaServicios.RemoveAt(indice);
                                // Actualizamos el DataGrid para reflejar los cambios
                                ActualizarDataGrid();
                            }
                        }
                    }
                    else
                    {
                        // Si la fila no contiene la columna "Indice", se trata de un servicio existente.
                        DialogResult resultado = MessageBox.Show(
                            $"¿Deseas inactivar el servicio '{filaDatos["NombreServicio"]}'?",
                            "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            ServicioCitaKey key = new ServicioCitaKey
                            {
                                IdCita = Convert.ToInt32(filaDatos["idCita"]),
                                Hora = TimeSpan.Parse(filaDatos["hora"].ToString()),
                                IdEmpleado = Convert.ToInt32(filaDatos["idEmpleado"]),
                                IdServicioEspecificoNieto = filaDatos["idServicioEspecificoNieto"] != DBNull.Value
                                                              ? Convert.ToInt32(filaDatos["idServicioEspecificoNieto"])
                                                              : (int?)null,
                                IdVacuna = filaDatos["idVacuna"] != DBNull.Value
                                           ? Convert.ToInt32(filaDatos["idVacuna"])
                                           : (int?)null
                            };

                            if (!serviciosInactivados.Any(s =>
                                    s.IdCita == key.IdCita &&
                                    s.Hora == key.Hora &&
                                    s.IdEmpleado == key.IdEmpleado &&
                                    s.IdServicioEspecificoNieto == key.IdServicioEspecificoNieto &&
                                    s.IdVacuna == key.IdVacuna))
                            {
                                serviciosInactivados.Add(key);
                            }

                            // Remover la fila del DataGrid
                            filaDatos.Row.Delete();
                        }
                    }
                }
            }
        }
        
        //--------------------------------
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

            if (!string.IsNullOrEmpty(nombreServicio))
            {
                string empleadoSeleccionado = cbEmpleado.Text;
                // Aquí se valida si el servicio ya ha sido agregado
                if (!listaServicios.Any(s => s.NombreServicio.Equals(nombreServicio, StringComparison.OrdinalIgnoreCase)))
                {
                    // Se crea el servicio con la observación del control rtObservacion
                    listaServicios.Add(new ServicioSeleccionado(nombreServicio, empleadoSeleccionado, rtObservacion.Text.Trim(), esVacuna, idVacuna));
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

        private void InsertarServiciosEnConsulta()
        {
            SqlTransaction transaction = null;
            try
            {
                conexionDB.AbrirConexion();
                transaction = conexionDB.GetConexion().BeginTransaction();

                // Actualizar a 'I' los servicios existentes marcados para inactivación
                foreach (ServicioCitaKey key in serviciosInactivados)
                {
                    // Armamos la condición dependiendo si es vacuna o no.
                    string queryUpdateInactivo = "UPDATE Servicio_Cita SET estado = 'I' WHERE idCita = @idCita AND hora = @hora AND idEmpleado = @idEmpleado AND ";
                    if (key.IdVacuna.HasValue)
                    {
                        queryUpdateInactivo += "idVacuna = @idVacuna";
                    }
                    else
                    {
                        queryUpdateInactivo += "idServicioEspecificoNieto = @idServicioEspecificoNieto";
                    }

                    using (SqlCommand cmdUpdateInactivo = new SqlCommand(queryUpdateInactivo, conexionDB.GetConexion(), transaction))
                    {
                        cmdUpdateInactivo.Parameters.AddWithValue("@idCita", key.IdCita);
                        cmdUpdateInactivo.Parameters.Add("@hora", SqlDbType.Time).Value = key.Hora;
                        cmdUpdateInactivo.Parameters.AddWithValue("@idEmpleado", key.IdEmpleado);
                        if (key.IdVacuna.HasValue)
                        {
                            cmdUpdateInactivo.Parameters.AddWithValue("@idVacuna", key.IdVacuna.Value);
                        }
                        else
                        {
                            cmdUpdateInactivo.Parameters.AddWithValue("@idServicioEspecificoNieto", key.IdServicioEspecificoNieto.Value);
                        }
                        cmdUpdateInactivo.ExecuteNonQuery();
                    }
                }

                // Insertar los nuevos servicios agregados en listaServicios
                foreach (var servicio in listaServicios)
                {
                    int idEmpleado = ObtenerIdEmpleado2(servicio.Empleado, transaction);
                    string observacionTexto = servicio.Observacion;

                    if (servicio.EsVacuna)
                    {
                        string queryInsertVacuna = @"
                    INSERT INTO Servicio_Cita (idCita, hora, idServicioEspecificoNieto, idVacuna, idEmpleado, estado, observacion)
                    VALUES (@idCita, @hora, NULL, @idVacuna, @idEmpleado, 'A', @observacion)";
                        using (SqlCommand cmdInsert = new SqlCommand(queryInsertVacuna, conexionDB.GetConexion(), transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@idCita", DatoCita);
                            //cmdInsert.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                            cmdInsert.Parameters.Add("@hora", SqlDbType.Time).Value = DateTime.Now.TimeOfDay;
                            cmdInsert.Parameters.AddWithValue("@idVacuna", servicio.IdVacuna);
                            cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmdInsert.Parameters.AddWithValue("@observacion", observacionTexto);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        int idServicioNieto = ObtenerIdServicioNieto2(servicio.NombreServicio, transaction);
                        string queryInsert = @"
                    INSERT INTO Servicio_Cita (idCita, hora, idServicioEspecificoNieto, idVacuna, idEmpleado, estado, observacion)
                    VALUES (@idCita, @hora, @idServicioNieto, NULL, @idEmpleado, 'A', @observacion)";
                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conexionDB.GetConexion(), transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@idCita", DatoCita);
                            //cmdInsert.Parameters.Add("@hora", SqlDbType.Time).Value = hora;
                            cmdInsert.Parameters.Add("@hora", SqlDbType.Time).Value = DateTime.Now.TimeOfDay;
                            cmdInsert.Parameters.AddWithValue("@idServicioNieto", idServicioNieto);
                            cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmdInsert.Parameters.AddWithValue("@observacion", observacionTexto);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                MessageBox.Show("Servicios Guardados Correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try { transaction.Rollback(); } catch { }
                }
                MessageBox.Show("Error al guardar los servicios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private int ObtenerIdEmpleado2(string nombreEmpleado, SqlTransaction transaction)
        {
            string query = "SELECT idEmpleado FROM Empleado WHERE usuario = @usuario";
            SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion(), transaction);
            cmd.Parameters.AddWithValue("@usuario", nombreEmpleado);
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        private int ObtenerIdServicioNieto2(string nombreServicio, SqlTransaction transaction)
        {
            string query = "SELECT idServicioEspecificoNieto FROM ServicioEspecificoNieto WHERE nombre = @nombre";
            SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion(), transaction);
            cmd.Parameters.AddWithValue("@nombre", nombreServicio);
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

    }
}

