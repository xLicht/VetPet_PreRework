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
using VetPet_.Angie;
using VetPet_.Angie.Mascotas;

namespace VetPet_
{
    public partial class VentasHistorialdeVentas : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();
        private Form1 parentForm;
        public int idVenta;

        public VentasHistorialdeVentas(Form1 parent)
        {
            InitializeComponent();
            this.Load += VentasHistorialdeVentas_Load;       // Evento Load
            this.Resize += VentasHistorialdeVentas_Resize;   // Event
            PersonalizarDataGridView();
            parentForm = parent;  // Guardamos la referencia de Form1
            CargarVentas();
            PersonalizarDataGridView();
        }

        public void CargarVentas()
        {
            try
            {
                mismetodos = new Mismetodos();
                mismetodos.AbrirConexion();

                string query = @"
        SELECT 
            V.idVenta,
            V.fechaRegistro, 
            V.total, 
            P.nombre AS Cliente
        FROM 
            Venta V
        LEFT JOIN 
            Persona P ON V.idPersona = P.idPersona;";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    dataGridView1.DataSource = tabla;

                    // Configurar nombres de columnas
                    dataGridView1.Columns["fechaRegistro"].HeaderText = "Fecha";
                    dataGridView1.Columns["total"].HeaderText = "Total";
                    dataGridView1.Columns["Cliente"].HeaderText = "Cliente";

                    // Ocultar columna idVenta (opcional)
                    dataGridView1.Columns["idVenta"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        private void VentasHistorialdeVentas_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void VentasHistorialdeVentas_Resize(object sender, EventArgs e)
        {
            // Calcular el factor de escala
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in this.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];

                    // Ajustar las dimensiones
                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);

                    // Ajustar el tamaño de la fuente
                    control.Font = new Font(control.Font.FontFamily, info.fontSize * Math.Min(scaleX, scaleY));
                }
            }
        }
        public void PersonalizarDataGridView()
        {
            dataGridView1.BorderStyle = BorderStyle.None; // Elimina bordes
            dataGridView1.BackgroundColor = Color.White; // Fondo blanco

            // Configurar fuente más grande para las celdas
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular); // Tamaño 12

            // Aumentar el alto de las filas para que el texto sea legible
            dataGridView1.RowTemplate.Height = 30; // Altura de fila aumentada

            // Alternar colores de filas
            dataGridView1.DefaultCellStyle.BackColor = Color.White;

            // Color de la selección
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Pink;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Encabezados más elegantes
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightPink;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold); // Tamaño aumentado a 14

            // Bordes y alineación
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.ColumnHeadersHeight = 40;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasListado(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                idVenta = Convert.ToInt32(fila.Cells["idVenta"].Value);

            }
            parentForm.formularioHijo(new VentasVentanadePagoNueva(parentForm,idVenta)); // Pasamos la referencia de Form1 a 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si se seleccionó la opción de fecha
            if (comboBox1.SelectedItem?.ToString() == "Fecha")
            {
                MostrarSelectorFechas();

                // Opcional: Limpiar el TextBox de filtro
                textBox1.Text = "";
                textBox1.Enabled = false; // Deshabilitar ya que usaremos calendarios
            }
            else
            {
                textBox1.Enabled = true;
            }
        }

        private void MostrarSelectorFechas()
        {
            using (var formFechas = new Form())
            {
                formFechas.Text = "Seleccione el rango de fechas";
                formFechas.StartPosition = FormStartPosition.CenterParent;
                formFechas.FormBorderStyle = FormBorderStyle.FixedDialog;
                formFechas.MaximizeBox = false;
                formFechas.MinimizeBox = false;
                formFechas.ClientSize = new Size(300, 150);

                var dtpInicio = new DateTimePicker()
                {
                    Location = new Point(20, 30),
                    Width = 260,
                    Format = DateTimePickerFormat.Short,
                    Value = DateTime.Today.AddMonths(-1)
                };

                var dtpFin = new DateTimePicker()
                {
                    Location = new Point(20, 70),
                    Width = 260,
                    Format = DateTimePickerFormat.Short,
                    Value = DateTime.Today
                };

                var btnAceptar = new Button()
                {
                    Text = "Filtrar",
                    DialogResult = DialogResult.OK,
                    Location = new Point(120, 110)
                };

                formFechas.Controls.Add(new Label()
                {
                    Text = "Fecha inicial:",
                    Location = new Point(20, 10)
                });
                formFechas.Controls.Add(dtpInicio);

                formFechas.Controls.Add(new Label()
                {
                    Text = "Fecha final:",
                    Location = new Point(20, 50)
                });
                formFechas.Controls.Add(dtpFin);

                formFechas.Controls.Add(btnAceptar);
                formFechas.AcceptButton = btnAceptar;

                if (formFechas.ShowDialog(this) == DialogResult.OK)
                {
                    FiltrarVentasConCitasPorFecha(dtpInicio.Value, dtpFin.Value);
                }
            }
        }

        private void FiltrarVentasConCitasPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                // Ajustar las fechas para cubrir todo el día
                fechaInicio = fechaInicio.Date;
                fechaFin = fechaFin.Date.AddDays(1).AddSeconds(-1);

                mismetodos.AbrirConexion();

                string query = @"SELECT 
                        V.idVenta,
                        PE.nombre AS Cliente,
                        V.total AS Total,
                        V.fechaRegistro AS Fecha
                    FROM 
                        Venta V
                        INNER JOIN Persona PE ON V.idPersona = PE.idPersona
                    WHERE 
                        V.fechaRegistro BETWEEN @fechaInicio AND @fechaFin
                        AND V.estado = 'A'
                    ORDER BY 
                        V.fechaRegistro DESC";

                using (SqlCommand cmd = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                    DataTable dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);

                    dataGridView1.DataSource = dt;
                    if (dataGridView1.Columns.Contains("idVenta"))
                        dataGridView1.Columns["idVenta"].Visible = false;

                    if (dataGridView1.Columns.Contains("Fecha"))
                        dataGridView1.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    MessageBox.Show($"Filtrado: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar ventas: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        private void FiltrarDatos()
        {
            try
            {
                mismetodos.AbrirConexion();

                string columnaSeleccionada = comboBox1.SelectedItem?.ToString();

                string query = @"
        SELECT 
            V.idVenta,
            PE.nombre AS Cliente,
            V.total AS Total,
            V.fechaRegistro AS Fecha
        FROM 
            Venta V
            INNER JOIN Persona PE ON V.idPersona = PE.idPersona
        WHERE 
            V.estado = 'A'";

                // Diccionario para mapear columnas
                Dictionary<string, string> mapaColumnas = new Dictionary<string, string>
        {
            { "Cliente", "PE.nombre" },
            { "Total", "V.total" },
            { "Fecha", " V.fechaRegistro" }
        };

                SqlCommand cmd = new SqlCommand("", mismetodos.GetConexion());

                // Filtro por texto (si hay texto y columna seleccionada)
                if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(columnaSeleccionada))
                    {
                        if (columnaSeleccionada == "Fecha")
                        {
                            MostrarSelectorFechas();
                            return;
                        }
                        else if (mapaColumnas.ContainsKey(columnaSeleccionada))
                        {
                            query += $" AND {mapaColumnas[columnaSeleccionada]} LIKE @filtro";
                            cmd.Parameters.AddWithValue("@filtro", "%" + textBox1.Text.Trim() + "%");
                        }
                    }
                }

                query += " ORDER BY V.fechaRegistro DESC";
                cmd.CommandText = query;

                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem?.ToString() == "FechaCita")
            {
                MostrarSelectorFechas();
            }
            else
            {
                FiltrarDatos();
            }
        }
    }
}

