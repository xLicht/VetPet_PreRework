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
    public partial class CortesHistorial : FormPadre
    {
        private ConexionMaestra conexion = new ConexionMaestra();

        public CortesHistorial()
        {
            InitializeComponent();
        }

        public CortesHistorial(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            CargarHistorialCortes();
            ConfigurarDataGridView();
        }

        private void CargarHistorialCortes()
        {
            try
            {
                using (SqlConnection connection = conexion.CrearConexion())
                {
                    connection.Open();

                    string query = @"
            SELECT 
                c.idCorte,  -- Incluimos el ID pero no lo mostraremos
                p.nombre + ' ' + p.apellidoP AS 'Usuario',
                c.fechaInicio AS 'Fecha Inicio',
                c.fechaFin AS 'Fecha Fin',
                (c.totalEfectivo + c.totalTarjeta) AS 'Venta Total',
                CASE 
                    WHEN c.correcto = 0 THEN 'Correcto'
                    ELSE 'Incorrecto'
                END AS 'Estado'
            FROM Corte c
            INNER JOIN Empleado e ON c.idEmpleado = e.idEmpleado
            INNER JOIN Persona p ON e.idPersona = p.idPersona
            ORDER BY c.fechaInicio DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable cortes = new DataTable();
                    adapter.Fill(cortes);

                    dataGridView1.DataSource = cortes;

                    // Ocultar la columna idCorte después de cargar los datos
                    if (dataGridView1.Columns["idCorte"] != null)
                    {
                        dataGridView1.Columns["idCorte"].Visible = false;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Error de base de datos: {sqlEx.Message}", "Error SQL",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            // Configuración básica
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Centrar todo el contenido de las celdas
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Formato para columnas específicas
            if (dataGridView1.Columns["Fecha Inicio"] != null)
            {
                dataGridView1.Columns["Fecha Inicio"].DefaultCellStyle.Format = "g"; // Fecha y hora
            }

            if (dataGridView1.Columns["Fecha Fin"] != null)
            {
                dataGridView1.Columns["Fecha Fin"].DefaultCellStyle.Format = "g"; // Fecha y hora
            }

            if (dataGridView1.Columns["Venta Total"] != null)
            {
                dataGridView1.Columns["Venta Total"].DefaultCellStyle.Format = "N2"; // Formato monetario
            }

            // Estilo de las cabeceras
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            // Ocultar encabezados de fila
            dataGridView1.RowHeadersVisible = false;

            // Alternar colores de filas para mejor legibilidad
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void CortesHistorial_Load(object sender, EventArgs e)
        {
            // La carga de datos ahora se hace en el constructor
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesMenus(parentForm));
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int idCorte = Convert.ToInt32(dataGridView1.CurrentRow.Cells["idCorte"].Value);
                parentForm.formularioHijo(new CortesConsultar(parentForm, idCorte));
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                // Si el campo está vacío, cargar todos los cortes
                CargarHistorialCortes();
            }
            else
            {
                // Buscar cortes por nombre de usuario
                BuscarCortesPorUsuario(nombreUsuario);
            }
        }

        private void BuscarCortesPorUsuario(string nombreUsuario)
        {
            try
            {
                using (SqlConnection connection = conexion.CrearConexion())
                {
                    connection.Open();

                    string query = @"
            SELECT 
                c.idCorte,
                p.nombre + ' ' + p.apellidoP AS 'Usuario',
                c.fechaInicio AS 'Fecha Inicio',
                c.fechaFin AS 'Fecha Fin',
                (c.totalEfectivo + c.totalTarjeta) AS 'Venta Total',
                CASE 
                    WHEN c.correcto = 0 THEN 'Correcto'
                    ELSE 'Incorrecto'
                END AS 'Estado'
            FROM Corte c
            INNER JOIN Empleado e ON c.idEmpleado = e.idEmpleado
            INNER JOIN Persona p ON e.idPersona = p.idPersona
            WHERE p.nombre LIKE @nombreUsuario OR p.apellidoP LIKE @nombreUsuario OR p.apellidoM LIKE @nombreUsuario
            ORDER BY c.fechaInicio DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@nombreUsuario", "%" + nombreUsuario + "%");

                    DataTable cortes = new DataTable();
                    adapter.Fill(cortes);

                    dataGridView1.DataSource = cortes;

                    // Ocultar la columna idCorte después de cargar los datos
                    if (dataGridView1.Columns["idCorte"] != null)
                    {
                        dataGridView1.Columns["idCorte"].Visible = false;
                    }

                    // Mostrar mensaje si no se encontraron resultados
                    if (cortes.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron cortes para el usuario especificado", "Búsqueda",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Error de base de datos: {sqlEx.Message}", "Error SQL",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Buscar nombre de usuario")
            {
                txtUsuario.Text = "";
            }
        }
    }
}