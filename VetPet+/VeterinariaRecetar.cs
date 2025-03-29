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
    public partial class VeterinariaRecetar : FormPadre
    {
        public int DatoCita { get; set; }
        public int DatoConsulta { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        private List<Tuple<int, string, int>> listaMedicamentos = new List<Tuple<int, string, int>>();

        public VeterinariaRecetar(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void VeterinariaRecetar_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Dato Recibido :" + DatoCita);
            //MessageBox.Show("Dato Recibido :" + DatoConsulta);
            MostrarDatosMacota();
            CargarMedicamentos();
            MostrarDatosConsulta();
        }
        private void CargarMedicamentos()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = "SELECT idMedicamento, nombreGenérico FROM Medicamento";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbMedicamentos.Items.Clear(); 

                    while (reader.Read())
                    {
                        cbMedicamentos.Items.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(reader["idMedicamento"]),
                            reader["nombreGenérico"].ToString()
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
        private void MostrarDatosConsulta()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT diagnostico, peso, temperatura 
                         FROM Consulta 
                         WHERE idConsulta = @idConsulta";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", DatoConsulta);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        rtDiagnostico.Text = reader["diagnostico"].ToString();
                        txtPeso.Text = reader["peso"].ToString();
                        txtTemperatura.Text = reader["temperatura"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró información para esta consulta.");
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
        private void MostrarDatosMacota()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT 
                            p.nombre AS NombreCliente, 
                            m.nombre AS NombreMascota, 
                            e.nombre AS Especie, 
                            r.nombre AS Raza,
                            CONVERT(varchar, c.fechaProgramada, 103) AS FechaCita,
                            CONVERT(varchar, m.fechaNacimiento, 103) AS FechaNacimiento
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
                        txtFecha.Text = reader["FechaCita"].ToString();
                        txtFechaNacimiento.Text = reader["FechaNacimiento"].ToString();
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtIndicaciones.Text))
            {
                MessageBox.Show("Las indicaciones no pueden estar vacías.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conexionDB.AbrirConexion();

                string queryReceta = @"INSERT INTO Receta (indicaciones, idConsulta) 
                                       VALUES (@indicaciones, @idConsulta);
                                       SELECT SCOPE_IDENTITY();";

                int idReceta;

                using (SqlCommand cmd = new SqlCommand(queryReceta, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@indicaciones", rtIndicaciones.Text.Trim());
                    cmd.Parameters.AddWithValue("@idConsulta", DatoConsulta);
                    idReceta = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (listaMedicamentos.Count > 0)
                {
                    foreach (var medicamento in listaMedicamentos)
                    {
                        string queryMedicamento = @"INSERT INTO Receta_Medicamento (idReceta, idMedicamento, cantidad) 
                                                    VALUES (@idReceta, @idMedicamento, @cantidad);";

                        using (SqlCommand cmd = new SqlCommand(queryMedicamento, conexionDB.GetConexion()))
                        {
                            cmd.Parameters.AddWithValue("@idReceta", idReceta);
                            cmd.Parameters.AddWithValue("@idMedicamento", medicamento.Item1);
                            cmd.Parameters.AddWithValue("@cantidad", medicamento.Item3);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Receta guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                listaMedicamentos.Clear();
                ActualizarDataGrid();
                rtIndicaciones.Clear();
                int idCitaSeleccionada = Convert.ToInt32(DatoCita);
                ConsultarCita formularioHijo = new ConsultarCita(parentForm);
                formularioHijo.DatoCita = idCitaSeleccionada;
                parentForm.formularioHijo(formularioHijo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la receta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }

            //parentForm.formularioHijo(new VeterinariaConsultarM(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            ConsultarCita formularioHijo = new ConsultarCita(parentForm);
            formularioHijo.DatoCita = idCitaSeleccionada;
            parentForm.formularioHijo(formularioHijo);

            //parentForm.formularioHijo(new VeterinariaConsultarM(parentForm));
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
        private void btnAgregarMedicamentos_Click(object sender, EventArgs e)
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

            //parentForm.formularioHijo(new VeterinariaVentaMedicamentos(parentForm, "VeterinariaRecetar"));
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

        private void btnGenerarReceta_Click(object sender, EventArgs e)
        {
            string nombreDueño = txtNombre.Text;
            string nombreMascota = txtMascota.Text;
            string especie = txtEspecie.Text;
            string raza = txtRaza.Text;
            string fechaNacimiento = txtFechaNacimiento.Text;
            string diagnostico = rtDiagnostico.Text;
            string peso = txtPeso.Text;
            string temperatura = txtTemperatura.Text;
            string indicaciones = rtIndicaciones.Text;

           
            //VeterinariaGenerarReceta formularioHijo = new VeterinariaGenerarReceta(
            //    parentForm,
            //    nombreDueño, nombreMascota, especie, raza, fechaNacimiento,
            //    diagnostico, peso, temperatura, indicaciones,
            //    listaMedicamentos 
            //);

            //VeterinariaGenerarReceta formularioHijo = new VeterinariaGenerarReceta(parentForm);
            VeterinariaGenerarReceta formularioHijo = new VeterinariaGenerarReceta(
                   parentForm,
                   nombreDueño,
                   nombreMascota,
                   especie,
                   raza,
                   fechaNacimiento,
                   diagnostico,
                   peso,
                   temperatura,
                   indicaciones,
                   listaMedicamentos
               );
            parentForm.formularioHijo(formularioHijo);
        }
    }
}
