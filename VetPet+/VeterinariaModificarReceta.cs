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
    public partial class VeterinariaModificarReceta : FormPadre
    {
        public int DatoCita { get; set; }
        public int DatoCita2 { get; set; }
        int DatoCitaT = 0;
        int DatoCita2T = 0;
        private conexionDaniel conexionDB = new conexionDaniel();
        private int datoConsulta;
        private List<Tuple<int, string, int>> listaMedicamentos = new List<Tuple<int, string, int>>();
        public VeterinariaModificarReceta(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaModificarReceta_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Dato Recibido" + DatoCita);
            DatoCitaT = DatoCita;
            DatoCita2T = DatoCita2;

            if (DatoCita == 0)
            {
                DatoCita = DatoCita2;
            }
            ObtenerDatoConsulta();
            if (datoConsulta != 0)
            {
                MostrarDatosMascota();
                MostrarDatosConsulta();
                MostrarReceta();
                CargarMedicamentos();
                CargarMedicamentosRecetados();
            }
            else
            {
                MessageBox.Show("No se encontró una consulta asociada a esta cita.");
               // Close();
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show("Si no se guardan los cambios, se perderán. ¿Desea continuar?","Advertencia",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                int idCitaSeleccionada = Convert.ToInt32(DatoCita);
                VeterinariaConsultarRece formularioHijo = new VeterinariaConsultarRece(parentForm);
                formularioHijo.DatoCita = idCitaSeleccionada;
                parentForm.formularioHijo(formularioHijo);

                // parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
            }
            //int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            //VeterinariaConsultarRece formularioHijo = new VeterinariaConsultarRece(parentForm);
            //formularioHijo.DatoCita = idCitaSeleccionada;
            //parentForm.formularioHijo(formularioHijo);


            // parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarModificaciones(); 
            //parentForm.formularioHijo(new VeterinariaConsultarRece(parentForm));
        }

        private void btnAgregarMedicamentos_Click(object sender, EventArgs e)
        {
           // parentForm.formularioHijo(new VeterinariaVentaMedicamentos(parentForm, "VeterinariaModificarReceta"));
        }
        private void CargarMedicamentos()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = "SELECT idMedicamento, nombreGenerico FROM Medicamento";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbMedicamentos.Items.Clear();

                    while (reader.Read())
                    {
                        cbMedicamentos.Items.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(reader["idMedicamento"]),
                            reader["nombreGenerico"].ToString()
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los medicamentos: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void CargarMedicamentosRecetados()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT rm.idMedicamento, m.nombreGenerico, rm.cantidad
                         FROM Receta_Medicamento rm
                         INNER JOIN Medicamento m ON rm.idMedicamento = m.idMedicamento
                         INNER JOIN Receta r ON rm.idReceta = r.idReceta
                         WHERE r.idConsulta = @idConsulta";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
                    SqlDataReader reader = cmd.ExecuteReader();

                    listaMedicamentos.Clear();

                    while (reader.Read())
                    {
                        int idMedicamento = reader["idMedicamento"] != DBNull.Value ? Convert.ToInt32(reader["idMedicamento"]) : 0;
                        string nombreMedicamento = reader["nombreGenerico"] != DBNull.Value ? reader["nombreGenerico"].ToString() : "Desconocido";
                        int cantidad = reader["cantidad"] != DBNull.Value ? Convert.ToInt32(reader["cantidad"]) : 0;

                        listaMedicamentos.Add(new Tuple<int, string, int>(idMedicamento, nombreMedicamento, cantidad));
                    }

                    ActualizarDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los medicamentos recetados: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void ActualizarDataGrid()
        {

            dtMedicamentos.Rows.Clear();
            dtMedicamentos.Columns.Clear();

            if (dtMedicamentos.Columns.Count == 0)
            {
                dtMedicamentos.Columns.Add("ID", "ID Medicamento");
                dtMedicamentos.Columns.Add("Nombre", "Nombre del Medicamento");
                dtMedicamentos.Columns.Add("Cantidad", "Cantidad");
            }

            foreach (var medicamento in listaMedicamentos)
            {
                dtMedicamentos.Rows.Add(medicamento.Item1, medicamento.Item2, medicamento.Item3);
            }
        }
        private void ObtenerDatoConsulta()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT TOP 1 idConsulta FROM Consulta WHERE idCita = @idCita";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", DatoCita);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        datoConsulta = Convert.ToInt32(reader["idConsulta"]);
                        //MessageBox.Show("Consulta encontrada: " + datoConsulta);
                    }
                    else
                    {
                        datoConsulta = 0; // No se encontró consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el ID de consulta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void MostrarReceta()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT r.indicaciones, m.nombreGenerico, rm.cantidad
                                 FROM Receta r
                                 INNER JOIN Receta_Medicamento rm ON r.idReceta = rm.idReceta
                                 INNER JOIN Medicamento m ON rm.idMedicamento = m.idMedicamento
                                 WHERE r.idConsulta = @idConsulta";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool primeraFila = true;
                    dtMedicamentos.Rows.Clear();

                    while (reader.Read())
                    {
                        if (primeraFila)
                        {
                            rtIndicaciones.Text = reader["indicaciones"].ToString();
                            primeraFila = false;
                        }

                        dtMedicamentos.Rows.Add(reader["nombreGenerico"].ToString(), reader["cantidad"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la receta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void MostrarDatosConsulta()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT diagnostico, peso, temperatura, FechaConsulta 
                         FROM Consulta 
                         WHERE idConsulta = @idConsulta";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        rtDiagnostico.Text = reader["diagnostico"].ToString();
                        txtPeso.Text = reader["peso"].ToString();
                        txtTemperatura.Text = reader["temperatura"].ToString();
                        txtFecha.Text = reader["FechaConsulta"].ToString(); // NUEVO: Mostrar FechaConsulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de la consulta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void MostrarDatosMascota()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT 
                    p.nombre AS NombreCliente, 
                    m.nombre AS NombreMascota, 
                    e.nombre AS Especie, 
                    r.nombre AS Raza
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
                        txtMascota.Text = reader["NombreMascota"].ToString();
                        txtEspecie.Text = reader["Especie"].ToString();
                        txtRaza.Text = reader["Raza"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de la mascota: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void GuardarModificaciones()
        {
            try
            {
                conexionDB.AbrirConexion();
                SqlTransaction transaction = conexionDB.GetConexion().BeginTransaction();

                string updateRecetaQuery = "UPDATE Receta SET indicaciones = @indicaciones WHERE idConsulta = @idConsulta";
                using (SqlCommand cmd = new SqlCommand(updateRecetaQuery, conexionDB.GetConexion(), transaction))
                {
                    cmd.Parameters.AddWithValue("@indicaciones", rtIndicaciones.Text);
                    cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
                    cmd.ExecuteNonQuery();
                }

                int idReceta = 0;
                string selectRecetaQuery = "SELECT idReceta FROM Receta WHERE idConsulta = @idConsulta";
                using (SqlCommand cmd = new SqlCommand(selectRecetaQuery, conexionDB.GetConexion(), transaction))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        idReceta = Convert.ToInt32(reader["idReceta"]);
                    }
                    reader.Close();
                }

                if (idReceta == 0)
                {
                    throw new Exception("No se encontró una receta asociada a la consulta.");
                }

                string deleteQuery = "DELETE FROM Receta_Medicamento WHERE idReceta = @idReceta";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conexionDB.GetConexion(), transaction))
                {
                    cmd.Parameters.AddWithValue("@idReceta", idReceta);
                    cmd.ExecuteNonQuery();
                }

                string insertQuery = "INSERT INTO Receta_Medicamento (idReceta, idMedicamento, cantidad) VALUES (@idReceta, @idMedicamento, @cantidad)";
                foreach (var medicamento in listaMedicamentos)
                {
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conexionDB.GetConexion(), transaction))
                    {
                        cmd.Parameters.AddWithValue("@idReceta", idReceta);
                        cmd.Parameters.AddWithValue("@idMedicamento", medicamento.Item1);
                        cmd.Parameters.AddWithValue("@cantidad", medicamento.Item3);
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                MessageBox.Show("Receta actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la receta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnAgregarMedicamentos_Click_1(object sender, EventArgs e)
        {
            if (cbMedicamentos.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un medicamento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KeyValuePair<int, string> medicamentoSeleccionado = (KeyValuePair<int, string>)cbMedicamentos.SelectedItem;
            int idMedicamento = medicamentoSeleccionado.Key;
            string nombreMedicamento = medicamentoSeleccionado.Value;
            int cantidad = (int)nupCantidad.Value;

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var medicamentoExistente = listaMedicamentos.FirstOrDefault(m => m.Item1 == idMedicamento);

            if (medicamentoExistente != null)
            {
                MessageBox.Show("El medicamento ya está en la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listaMedicamentos.Add(new Tuple<int, string, int>(idMedicamento, nombreMedicamento, cantidad));

            ActualizarDataGrid();

        }

        private void dtMedicamentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dtMedicamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DialogResult confirmacion = MessageBox.Show(
                    "¿Desea eliminar este medicamento?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    listaMedicamentos.RemoveAt(e.RowIndex);
                    ActualizarDataGrid();
                }
            }
        }
    }
}
