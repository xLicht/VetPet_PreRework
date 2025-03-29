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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace VetPet_
{
    public partial class VeterinariaConsultarRece : FormPadre
    {
        public int DatoCita { get; set; }
        public int DatoCita2 { get; set; }
        int DatoCitaT = 0;
        int DatoCita2T = 0;
        private int datoConsulta;
        private List<Tuple<int, string, int>> listaMedicamentos = new List<Tuple<int, string, int>>();
        private conexionDaniel conexionDB = new conexionDaniel();

        public VeterinariaConsultarRece(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        
        }

        private void VeterinariaConsultarRece_Load(object sender, EventArgs e)
        {
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
                CargarMedicamentosRecetados();
            }
            else
            {
                MessageBox.Show("No se encontró una consulta asociada a esta cita.");
                //Close();
            }
            //MessageBox.Show("Dato Recibido"+ DatoCita);
        }
        private void CargarMedicamentosRecetados()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT rm.idMedicamento, m.nombreGenérico, rm.cantidad
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
                        string nombreMedicamento = reader["nombreGenérico"] != DBNull.Value ? reader["nombreGenérico"].ToString() : "Desconocido";
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

                string query = @"SELECT r.indicaciones, m.nombreGenérico, rm.cantidad
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

                        dtMedicamentos.Rows.Add(reader["nombreGenérico"].ToString(), reader["cantidad"].ToString());
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

                // Se realiza un join entre Consulta y Cita para obtener la fecha de la cita
                string query = @"
                SELECT 
                    con.diagnostico, 
                    con.peso, 
                    con.temperatura,
                    c.fechaProgramada AS FechaCita
                FROM Consulta con
                INNER JOIN Cita c ON con.idCita = c.idCita
                WHERE con.idConsulta = @idConsulta";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        rtDiagnostico.Text = reader["diagnostico"].ToString();
                        txtPeso.Text = reader["peso"].ToString();
                        txtTemperatura.Text = reader["temperatura"].ToString();
                        // Se muestra la fecha obtenida de la cita en lugar de FechaConsulta de la consulta
                        txtFecha.Text = reader["FechaCita"].ToString();
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



        private void btnRegresar_Click(object sender, EventArgs e)
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

            //parentForm.formularioHijo(new VeterinariaConsultaMedica(parentForm));
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            int idCitaSeleccionada = Convert.ToInt32(DatoCita);
            VeterinariaModificarReceta formularioHijo = new VeterinariaModificarReceta(parentForm);
            if (DatoCitaT == 0 && DatoCita2T != 0)
            {
                formularioHijo.DatoCita2 = idCitaSeleccionada;
            }
            else
            {
                formularioHijo.DatoCita = idCitaSeleccionada;
            }
            parentForm.formularioHijo(formularioHijo);

           // parentForm.formularioHijo(new VeterinariaModificarReceta(parentForm));
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                conexionDB.AbrirConexion();

                        string query = @"
                    UPDATE Receta 
                    SET estado = 'I'
                    WHERE idReceta = (
                        SELECT TOP 1 idReceta 
                        FROM Receta 
                        WHERE idConsulta = @idConsulta
                    )";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Receta eliminada (inactivada) correctamente.");
                        rtIndicaciones.Text = "";
                        listaMedicamentos.Clear();
                        dtMedicamentos.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la receta para eliminar.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la receta: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
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
