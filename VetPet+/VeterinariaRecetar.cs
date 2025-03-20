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
            MessageBox.Show("Dato Recibido :" + DatoConsulta);
            MostrarDatosMacota();
            CargarMedicamentos();
            MostrarDatosConsulta();
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
                MessageBox.Show("Error al obtener los datos básicos de la cita: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultarM(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaConsultarM(parentForm));
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
    }
}
