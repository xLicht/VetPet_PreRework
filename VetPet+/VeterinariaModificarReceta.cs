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
        private List<Tuple<int, string, int>> listaMedicamentos2 = new List<Tuple<int, string, int>>();
        private List<Tuple<int, string, int, string, string>> listaMedicamentos = new List<Tuple<int, string, int, string, string>>();
        private List<Tuple<int, string, int, string, string>> listaMedicamentosOriginal = new List<Tuple<int, string, int, string, string>>();
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
        private void CargarMedicamentosRecetados()
        {
            try
            {
                conexionDB.AbrirConexion();

                // Consulta que incluye dosis y observacion
                string query = @"
            SELECT rm.idMedicamento, m.[nombreGenérico], rm.cantidad, rm.dosis, rm.observacion
            FROM Receta_Medicamento rm
            INNER JOIN Medicamento m ON rm.idMedicamento = m.idMedicamento
            INNER JOIN Receta r ON rm.idReceta = r.idReceta
            WHERE r.idConsulta = @idConsulta and rm.estado = 'A'";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
                    SqlDataReader reader = cmd.ExecuteReader();

                    listaMedicamentos.Clear();
                    listaMedicamentosOriginal.Clear();

                    while (reader.Read())
                    {
                        int idMedicamento = reader["idMedicamento"] != DBNull.Value ? Convert.ToInt32(reader["idMedicamento"]) : 0;
                        string nombreMedicamento = reader["nombreGenérico"] != DBNull.Value ? reader["nombreGenérico"].ToString() : "Desconocido";
                        int cantidad = reader["cantidad"] != DBNull.Value ? Convert.ToInt32(reader["cantidad"]) : 0;
                        string dosis = reader["dosis"] != DBNull.Value ? reader["dosis"].ToString() : "";
                        string observacion = reader["observacion"] != DBNull.Value ? reader["observacion"].ToString() : "";

                        var medicamentoTuple = new Tuple<int, string, int, string, string>(idMedicamento, nombreMedicamento, cantidad, dosis, observacion);
                        // Se guarda en ambas listas
                        listaMedicamentos.Add(medicamentoTuple);
                        listaMedicamentosOriginal.Add(medicamentoTuple);
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
                dtMedicamentos.Columns.Add("Dosis", "Dosis");
                dtMedicamentos.Columns.Add("Observacion", "Observacion");
            }

            foreach (var medicamento in listaMedicamentos)
            {
                dtMedicamentos.Rows.Add(
                    medicamento.Item1,
                    medicamento.Item2,
                    medicamento.Item3,
                    medicamento.Item4,
                    medicamento.Item5
                );
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
            //try
            //{
            //    conexionDB.AbrirConexion();

            //    string query = @"SELECT r.indicaciones, m.nombreGenérico, rm.cantidad
            //                     FROM Receta r
            //                     INNER JOIN Receta_Medicamento rm ON r.idReceta = rm.idReceta
            //                     INNER JOIN Medicamento m ON rm.idMedicamento = m.idMedicamento
            //                     WHERE r.idConsulta = @idConsulta";

            //    using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
            //    {
            //        cmd.Parameters.AddWithValue("@idConsulta", datoConsulta);
            //        SqlDataReader reader = cmd.ExecuteReader();

            //        bool primeraFila = true;
            //        dtMedicamentos.Rows.Clear();

            //        while (reader.Read())
            //        {
            //            if (primeraFila)
            //            {
            //                rtIndicaciones.Text = reader["indicaciones"].ToString();
            //                primeraFila = false;
            //            }

            //            dtMedicamentos.Rows.Add(reader["nombreGenérico"].ToString(), reader["cantidad"].ToString());
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al obtener la receta: " + ex.Message);
            //}
            //finally
            //{
            //    conexionDB.CerrarConexion();
            //}
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

                    // IMPORTANTE: Asegurarte de que existan columnas.
                    dtMedicamentos.Rows.Clear();
                    if (dtMedicamentos.Columns.Count == 0)
                    {
                        dtMedicamentos.Columns.Add("NombreGen", "Nombre del Medicamento");
                        dtMedicamentos.Columns.Add("Cantidad", "Cantidad");
                    }

                    while (reader.Read())
                    {
                        if (primeraFila)
                        {
                            rtIndicaciones.Text = reader["indicaciones"].ToString();
                            primeraFila = false;
                        }

                        // Ahora sí, se pueden agregar filas
                        dtMedicamentos.Rows.Add(
                            reader["nombreGenérico"].ToString(),
                            reader["cantidad"].ToString()
                        );
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
        private void GuardarModificaciones()
        {
            try
            {
                conexionDB.AbrirConexion();
                SqlTransaction transaction = conexionDB.GetConexion().BeginTransaction();

                // Actualizar indicaciones de la receta
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

                // Actualizar los medicamentos que fueron eliminados: están en la lista original pero ya no en la actual.
                foreach (var medOriginal in listaMedicamentosOriginal)
                {
                    bool existeEnActual = listaMedicamentos.Any(m => m.Item1 == medOriginal.Item1);
                    if (!existeEnActual)
                    {
                        string updateRemovedQuery = "UPDATE Receta_Medicamento SET estado = 'I' WHERE idReceta = @idReceta AND idMedicamento = @idMedicamento";
                        using (SqlCommand cmd = new SqlCommand(updateRemovedQuery, conexionDB.GetConexion(), transaction))
                        {
                            cmd.Parameters.AddWithValue("@idReceta", idReceta);
                            cmd.Parameters.AddWithValue("@idMedicamento", medOriginal.Item1);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Procesar los medicamentos actuales: actualizar si ya existen, o insertar si son nuevos.
                foreach (var med in listaMedicamentos)
                {
                    // Primero se intenta actualizar el registro (por si ya existía)
                    string updateCurrentQuery = @"
                UPDATE Receta_Medicamento 
                SET cantidad = @cantidad, dosis = @dosis, observacion = @observacion, estado = 'A' 
                WHERE idReceta = @idReceta AND idMedicamento = @idMedicamento";
                    int rowsAffected = 0;
                    using (SqlCommand cmd = new SqlCommand(updateCurrentQuery, conexionDB.GetConexion(), transaction))
                    {
                        cmd.Parameters.AddWithValue("@cantidad", med.Item3);
                        cmd.Parameters.AddWithValue("@dosis", med.Item4);
                        cmd.Parameters.AddWithValue("@observacion", med.Item5);
                        cmd.Parameters.AddWithValue("@idReceta", idReceta);
                        cmd.Parameters.AddWithValue("@idMedicamento", med.Item1);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    // Si no se actualizó, se inserta como nuevo registro
                    if (rowsAffected == 0)
                    {
                        string insertQuery = @"
                    INSERT INTO Receta_Medicamento (idReceta, idMedicamento, cantidad, dosis, observacion, estado) 
                    VALUES (@idReceta, @idMedicamento, @cantidad, @dosis, @observacion, 'A')";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conexionDB.GetConexion(), transaction))
                        {
                            cmd.Parameters.AddWithValue("@idReceta", idReceta);
                            cmd.Parameters.AddWithValue("@idMedicamento", med.Item1);
                            cmd.Parameters.AddWithValue("@cantidad", med.Item3);
                            cmd.Parameters.AddWithValue("@dosis", med.Item4);
                            cmd.Parameters.AddWithValue("@observacion", med.Item5);
                            cmd.ExecuteNonQuery();
                        }
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
                   listaMedicamentos2, DatoCita
               );
            parentForm.formularioHijo(formularioHijo);
        }

        private void dtMedicamentos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DialogResult confirmacion = MessageBox.Show("¿Desea eliminar este medicamento?","Confirmación", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    // Se elimina de la lista actual (listaMedicamentos)
                    listaMedicamentos.RemoveAt(e.RowIndex);
                    ActualizarDataGrid();
                }
            }
        }

        private void btnAgregarMedicamentos_Click_2(object sender, EventArgs e)
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

            // Obtener datos adicionales: dosis y observaciones.
            if (cbDosis.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una dosis.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string dosis = cbDosis.SelectedItem.ToString();
            string observaciones = dtObservaciones.Text.Trim();

            // Evitar duplicados en la lista.
            var medicamentoExistente = listaMedicamentos.FirstOrDefault(m => m.Item1 == idMedicamento);
            if (medicamentoExistente != null)
            {
                MessageBox.Show("El medicamento ya está en la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar a la lista con los datos completos.
            listaMedicamentos.Add(new Tuple<int, string, int, string, string>(
                idMedicamento,
                nombreMedicamento,
                cantidad,
                dosis,
                observaciones
            ));

            ActualizarDataGrid();

            // Opcional: limpiar controles si lo deseas
            nupCantidad.Value = 1;
            cbDosis.SelectedIndex = -1;
            dtObservaciones.Clear();
        }
    }
}
