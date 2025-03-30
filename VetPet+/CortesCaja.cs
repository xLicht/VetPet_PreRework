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
using static VetPet_.Form1;


namespace VetPet_
{
    public partial class CortesCaja : FormPadre
    {
        private ConexionMaestra conexion = new ConexionMaestra();

        private string nombreUsuario;
        private string fondoCaja;


        // Variables para almacenar los valores de los precios
        private double dgv1subirprecio1000 = 0;
        private double dgv1totalprecio1000 = 0;
        private double dgv1subirprecio500 = 0;
        private double dgv1totalprecio500 = 0;
        private double dgv1subirprecio200 = 0;
        private double dgv1totalprecio200 = 0;
        private double dgv1subirprecio100 = 0;
        private double dgv1totalprecio100 = 0;
        private double dgv1subirprecio50 = 0;
        private double dgv1totalprecio50 = 0;
        private double dgv1subirprecio20 = 0;
        private double dgv1totalprecio20 = 0;

        private double dgv2subirprecio20 = 0;
        private double dgv2totalprecio20 = 0;
        private double dgv2subirprecio10 = 0;
        private double dgv2totalprecio10 = 0;
        private double dgv2subirprecio5 = 0;
        private double dgv2totalprecio5 = 0;
        private double dgv2subirprecio2 = 0;
        private double dgv2totalprecio2 = 0;
        private double dgv2subirprecio1 = 0;
        private double dgv2totalprecio1 = 0;
        private double dgv2subirprecio50 = 0;
        private double dgv2totalprecio50 = 0;
        private double dgv2subirpreciocentavos = 0;
        private double dgv2totalpreciocentavos = 0;

        private double dgv1cantidad = 0;
        private double dgv1total = 0;
        private double dgv2cantidad = 0;
        private double dgv2total = 0;

        private double diferencia = 0;


        public CortesCaja()
        {
            InitializeComponent();
        }
        int dineroenlacaja = 0;
        public CortesCaja(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;

            txtTotalDinero.Text = DatosGlobales.FondoCaja.ToString();  // Muestra el FondoCaja
            txtUsuario.Text = DatosGlobales.NombreUsuario;     // Muestra el NombreUsuario
            dineroenlacaja = Convert.ToInt32(txtTotalDinero.Text);
            // Añadir el FondoCaja al total de caja inicial
            totalefectivo = DatosGlobales.FondoCaja; // Inicializamos con el fondo de caja
            txtTotalCaja.Text = totalefectivo.ToString("N2"); // Mostramos el total inicial

            // Establecer valores por defecto para los DateTimePicker
            dtpFechaInicio.Value = DateTime.Today;
            dtpFechaFin.Value = DateTime.Today;
            CargarVentasPorRangoFechas(DateTime.Today, DateTime.Today);
            ConfigurarDataGridViewMontos();
        }

        private void CargarVentasPorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (SqlConnection connection = conexion.CrearConexion())
                {
                    connection.Open();

                    // Ajustar las fechas para incluir todo el día
                    DateTime fechaInicioAjustada = fechaInicio.Date;
                    DateTime fechaFinAjustada = fechaFin.Date.AddDays(1).AddSeconds(-1);

                    string query = @"SELECT 
                        v.idVenta AS 'ID',
                        CASE 
                            WHEN v.efectivo > 0 AND v.tarjeta > 0 THEN 'Mixto'
                            WHEN v.efectivo > 0 THEN 'Efectivo'
                            WHEN v.tarjeta > 0 THEN 'Tarjeta'
                            ELSE 'No especificado'
                        END AS 'Tipo de Pago',
                        v.fechaRegistro AS 'Fecha',
                        COALESCE(v.efectivo, 0) + COALESCE(v.tarjeta, 0) AS 'Monto'
                    FROM Venta v
                    WHERE v.fechaRegistro BETWEEN @fechaInicio AND @fechaFin
                    AND v.estado = 'A'
                    ORDER BY v.fechaRegistro;";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@fechaInicio", fechaInicioAjustada);
                    adapter.SelectCommand.Parameters.AddWithValue("@fechaFin", fechaFinAjustada);

                    DataTable ventas = new DataTable();
                    adapter.Fill(ventas);

                    dataGridView4.DataSource = ventas;
                    ConfigurarDataGridViewCentrado();
                    CalcularTotalesVentas(ventas);

                    // Mostrar rango de fechas
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

        private void ConfigurarDataGridViewCentrado()
        {
            // Configuración básica
            dataGridView4.AutoGenerateColumns = true;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Centrar todo el contenido de las celdas
            dataGridView4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Configuración específica para columnas
            if (dataGridView4.Columns["Monto"] != null)
            {
                dataGridView4.Columns["Monto"].DefaultCellStyle.Format = "N2";
                // Mantenemos el alineamiento a la derecha para montos
                dataGridView4.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dataGridView4.Columns["Hora"] != null)
            {
                dataGridView4.Columns["Hora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Estilo de las cabeceras
            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView4.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView4.Font, FontStyle.Bold);

            // Ocultar encabezados de fila
            dataGridView4.RowHeadersVisible = false;

            // Alternar colores de filas para mejor legibilidad
            dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void CalcularTotalesVentas(DataTable ventas)
        {
            double totalEfectivo = 0;
            double totalTarjeta = 0;
            double totalGeneral = 0;

            foreach (DataRow row in ventas.Rows)
            {
                totalGeneral += Convert.ToDouble(row["Monto"]);

                string tipoPago = row["Tipo de Pago"].ToString();
                if (tipoPago == "Efectivo")
                    totalEfectivo += Convert.ToDouble(row["Monto"]);
                else if (tipoPago == "Tarjeta")
                    totalTarjeta += Convert.ToDouble(row["Monto"]);
                else if (tipoPago == "Mixto")
                {
                    totalEfectivo += Convert.ToDouble(row["Monto"]) / 2;
                    totalTarjeta += Convert.ToDouble(row["Monto"]) / 2;
                }
            }

            txtEfectivoVentas.Text = totalEfectivo.ToString("N2");
            txtDocumentosVentas.Text = totalTarjeta.ToString("N2");
            txtTotalVentas.Text = totalGeneral.ToString("N2");

            // Actualizar diferencia automáticamente
            CalcularDiferencia();
        }

        // Variable para llevar el conteo de montos agregados
        private int contadorMontos = 0;
        private double totalDocumentos = 0;


        private void CortesCaja_Load(object sender, EventArgs e)
        {
            // Inicializar DataGridViews para el corte de caja
            dataGridView2.Rows.Add("1000 MXN", dgv1subirprecio1000, dgv1totalprecio1000 + " MXN");
            dataGridView2.Rows.Add("500 MXN", dgv1subirprecio500, dgv1totalprecio500 + " MXN");
            dataGridView2.Rows.Add("200 MXN", dgv1subirprecio200, dgv1totalprecio200 + " MXN");
            dataGridView2.Rows.Add("100 MXN", dgv1subirprecio100, dgv1totalprecio100 + " MXN");
            dataGridView2.Rows.Add("50 MXN", dgv1subirprecio50, dgv1totalprecio50 + " MXN");
            dataGridView2.Rows.Add("20 MXN", dgv1subirprecio20, dgv1totalprecio20 + " MXN");
            dataGridView2.Rows.Add("Total", dgv1cantidad, dgv1total + " MXN");

            dataGridView3.Rows.Add("20 MXN", dgv2subirprecio20, dgv2totalprecio20 + " MXN");
            dataGridView3.Rows.Add("10 MXN", dgv2subirprecio10, dgv2totalprecio10 + " MXN");
            dataGridView3.Rows.Add("5 MXN", dgv2subirprecio5, dgv2totalprecio5 + " MXN");
            dataGridView3.Rows.Add("2 MXN", dgv2subirprecio2, dgv2totalprecio2 + " MXN");
            dataGridView3.Rows.Add("1 MXN", dgv2subirprecio1, dgv2totalprecio1 + " MXN");
            dataGridView3.Rows.Add(".50 MXN", dgv2subirprecio50, dgv2totalprecio50 + " MXN");
            dataGridView3.Rows.Add("Total", dgv2cantidad, dgv2total + " MXN");
        }
        double totalGeneral = 0;
        double totalefectivo = 0;
        double totaltarjeta = 0;

        private void ActualizarTablas()
        {
            dgv1total = dgv1totalprecio1000 + dgv1totalprecio500 + dgv1totalprecio200 +
                        dgv1totalprecio100 + dgv1totalprecio50 + dgv1totalprecio20;

            if (dataGridView2.Rows.Count >= 7)
            {
                dataGridView2.Rows[0].Cells[1].Value = dgv1subirprecio1000;
                dataGridView2.Rows[0].Cells[2].Value = dgv1totalprecio1000 + " MXN";
                dataGridView2.Rows[1].Cells[1].Value = dgv1subirprecio500;
                dataGridView2.Rows[1].Cells[2].Value = dgv1totalprecio500 + " MXN";
                dataGridView2.Rows[2].Cells[1].Value = dgv1subirprecio200;
                dataGridView2.Rows[2].Cells[2].Value = dgv1totalprecio200 + " MXN";
                dataGridView2.Rows[3].Cells[1].Value = dgv1subirprecio100;
                dataGridView2.Rows[3].Cells[2].Value = dgv1totalprecio100 + " MXN";
                dataGridView2.Rows[4].Cells[1].Value = dgv1subirprecio50;
                dataGridView2.Rows[4].Cells[2].Value = dgv1totalprecio50 + " MXN";
                dataGridView2.Rows[5].Cells[1].Value = dgv1subirprecio20;
                dataGridView2.Rows[5].Cells[2].Value = dgv1totalprecio20 + " MXN";
                dataGridView2.Rows[6].Cells[1].Value = dgv1cantidad;
                dataGridView2.Rows[6].Cells[2].Value = dgv1total + " MXN";
            }

             dgv2total = dgv2totalprecio20 + dgv2totalprecio10 + dgv2totalprecio5 +
                dgv2totalprecio2 + dgv2totalprecio1 + dgv2totalpreciocentavos;

            txtEfectivoCaja.Text = (dgv1total + dgv2total).ToString("N2");
            totalefectivo = dgv1total + dgv2total;

            totalGeneral = totalefectivo + totaltarjeta;

            double totalparacaja = totalGeneral + dineroenlacaja;

            txtTotalCaja.Text = totalparacaja.ToString();




     

            CalcularDiferencia();


            CalcularDiferencia();

            if (dataGridView3.Rows.Count >= 7)
            {
                dataGridView3.Rows[0].Cells[1].Value = dgv2subirprecio20;
                dataGridView3.Rows[0].Cells[2].Value = dgv2totalprecio20 + " MXN";
                dataGridView3.Rows[1].Cells[1].Value = dgv2subirprecio10;
                dataGridView3.Rows[1].Cells[2].Value = dgv2totalprecio10 + " MXN";
                dataGridView3.Rows[2].Cells[1].Value = dgv2subirprecio5;
                dataGridView3.Rows[2].Cells[2].Value = dgv2totalprecio5 + " MXN";
                dataGridView3.Rows[3].Cells[1].Value = dgv2subirprecio2;
                dataGridView3.Rows[3].Cells[2].Value = dgv2totalprecio2 + " MXN";
                dataGridView3.Rows[4].Cells[1].Value = dgv2subirprecio1;
                dataGridView3.Rows[4].Cells[2].Value = dgv2totalprecio1 + " MXN";
                dataGridView3.Rows[5].Cells[1].Value = dgv2subirprecio50;
                dataGridView3.Rows[5].Cells[2].Value = dgv2totalprecio50 + " MXN";
                dataGridView3.Rows[6].Cells[1].Value = dgv2cantidad;
                dataGridView3.Rows[6].Cells[2].Value = dgv2total + " MXN";
            }
        }

        private void CalcularDiferencia()
        {
            // Obtener el total de ventas (convertirlo a double si es necesario)
            double totalVentas = Convert.ToDouble(txtTotalVentas.Text);

            // Calcular diferencia
            diferencia = totalVentas - totalGeneral;

            // Mostrar en el TextBox
            txtDiferencia.Text = diferencia.ToString("N2");

            // Opcional: Cambiar color según si hay sobrante o faltante
            if (diferencia > 0)
            {
                txtDiferencia.BackColor = Color.LightPink; // Faltante
            }
            else if (diferencia < 0)
            {
                txtDiferencia.BackColor = Color.LightGreen; // Sobrante
            }
            else
            {
                txtDiferencia.BackColor = SystemColors.Window; // Exacto
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el idEmpleado desde DatosGlobales (asumo que está disponible)
                int idEmpleado = DatosGlobales.IDUsuario; // Asegúrate de tener esta propiedad

                // Obtener las fechas de los DateTimePicker
                DateTime fechaInicio = dtpFechaInicio.Value;
                DateTime fechaFin = dtpFechaFin.Value;

                // Obtener el fondo de caja
                decimal fondoDeCaja = Convert.ToDecimal(txtTotalDinero.Text);

                // Obtener totales
                decimal totalEfectivo = Convert.ToDecimal(totalefectivo);
                decimal totalTarjeta = Convert.ToDecimal(totaltarjeta);

                // Determinar si el corte es correcto (diferencia = 0)
                bool correcto = (diferencia == 0);

                using (SqlConnection connection = conexion.CrearConexion())
                {
                    connection.Open();

                    string query = @"INSERT INTO Corte (
                            idEmpleado, 
                            fechaInicio, 
                            fechaFin, 
                            fondoDeCaja, 
                            totalEfectivo, 
                            totalTarjeta, 
                            correcto)
                        VALUES (
                            @idEmpleado, 
                            @fechaInicio, 
                            @fechaFin, 
                            @fondoDeCaja, 
                            @totalEfectivo, 
                            @totalTarjeta, 
                            @correcto)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        command.Parameters.AddWithValue("@fechaFin", fechaFin);
                        command.Parameters.AddWithValue("@fondoDeCaja", fondoDeCaja);
                        command.Parameters.AddWithValue("@totalEfectivo", totalEfectivo);
                        command.Parameters.AddWithValue("@totalTarjeta", totalTarjeta);
                        command.Parameters.AddWithValue("@correcto", correcto ? 0 : 1); // 0=correcto, 1=incorrecto

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Corte de caja registrado exitosamente", "Éxito",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Redirigir al menú de cortes
                            parentForm.formularioHijo(new CortesMenus(parentForm));
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar el corte de caja", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Error de base de datos al registrar corte: {sqlEx.Message}", "Error SQL",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al registrar corte: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesMenus(parentForm)); // Pasamos la referencia de Form1 a 
        }
        private bool PuedeReducirEfectivo(double cantidadAReducir)
        {
            double totalTODO = dgv1total + dgv2total + dineroenlacaja;
            double resultante = totalTODO - cantidadAReducir;
            return resultante >= dineroenlacaja;
        }
        private void btnsubir1000_Click(object sender, EventArgs e)
        {
            dgv1subirprecio1000 += 1;
            dgv1totalprecio1000 += 1000;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar1000_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(1000))
            {
                dgv1subirprecio1000 -= 1;
                dgv1totalprecio1000 -= 1000;
                dgv1cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir500_Click(object sender, EventArgs e)
        {
            dgv1subirprecio500 += 1;
            dgv1totalprecio500 += 500;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar500_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(500))
            {
                dgv1subirprecio500 -= 1;
                dgv1totalprecio500 -= 500;
                dgv1cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir200_Click(object sender, EventArgs e)
        {
            dgv1subirprecio200 += 1;
            dgv1totalprecio200 += 200;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar200_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(200))
            {
                dgv1subirprecio200 -= 1;
                dgv1totalprecio200 -= 200;
                dgv1cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir100_Click(object sender, EventArgs e)
        {
            dgv1subirprecio100 += 1;
            dgv1totalprecio100 += 100;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar100_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(100))
            {
                dgv1subirprecio100 -= 1;
                dgv1totalprecio100 -= 100;
                dgv1cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir50_Click(object sender, EventArgs e)
        {
            dgv1subirprecio50 += 1;
            dgv1totalprecio50 += 50;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar50_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(50))
            {
                dgv1subirprecio50 -= 1;
                dgv1totalprecio50 -= 50;
                dgv1cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir20_Click(object sender, EventArgs e)
        {
            dgv1subirprecio20 += 1;
            dgv1totalprecio20 += 20;

            dgv1cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar20_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(20))
            {
                dgv1subirprecio20 -= 1;
                dgv1totalprecio20 -= 20;
                dgv1cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn2subir20_Click(object sender, EventArgs e)
        {
            dgv2subirprecio20 += 1;
            dgv2totalprecio20 += 20;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btn2bajar20_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(20))
            {
                dgv2subirprecio20 -= 1;
                dgv2totalprecio20 -= 20;
                dgv2cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir10_Click(object sender, EventArgs e)
        {
            dgv2subirprecio10 += 1;
            dgv2totalprecio10 += 10;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar10_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(10))
            {
                dgv2subirprecio10 -= 1;
                dgv2totalprecio10 -= 10;
                dgv2cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir5_Click(object sender, EventArgs e)
        {
            dgv2subirprecio5 += 1;
            dgv2totalprecio5 += 5;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar5_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(5))
            {
                dgv2subirprecio5 -= 1;
                dgv2totalprecio5 -= 5;
                dgv2cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir2_Click(object sender, EventArgs e)
        {
            dgv2subirprecio2 += 1;
            dgv2totalprecio2 += 2;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar2_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(2))
            {
                dgv2subirprecio2 -= 1;
                dgv2totalprecio2 -= 2;
                dgv2cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubir1_Click(object sender, EventArgs e)
        {
            dgv2subirprecio1 += 1;
            dgv2totalprecio1 += 1;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajar1_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(1))
            {
                dgv2subirprecio1 -= 1;
                dgv2totalprecio1 -= 1;
                dgv2cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnsubircentavos_Click(object sender, EventArgs e)
        {
            dgv2subirpreciocentavos += 1;
            dgv2totalpreciocentavos += .50;

            dgv2cantidad += 1;
            ActualizarTablas();
        }

        private void btnbajarcentavos_Click(object sender, EventArgs e)
        {
            if (PuedeReducirEfectivo(0.50))
            {
                dgv2subirpreciocentavos -= 1;
                dgv2totalpreciocentavos -= 0.50;
                dgv2cantidad -= 1;
                ActualizarTablas();
            }
            else
            {
                MessageBox.Show("No puede reducir el efectivo por debajo del fondo de caja inicial",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAgregarMonto_Click_1(object sender, EventArgs e)
        {
            // Validar que el TextBox no esté vacío
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("Por favor ingrese un monto válido", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que sea un número válido
            if (!double.TryParse(txtMonto.Text, out double monto))
            {
                MessageBox.Show("Ingrese un valor numérico válido", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonto.Focus();
                return;
            }

            // Incrementar el contador
            contadorMontos++;

            // Agregar nueva fila al DataGridView
            dataGridView1.Rows.Add(contadorMontos, monto);

            // Actualizar el total
            totaltarjeta += monto;
            totalGeneral = totalefectivo + totaltarjeta;

            txtDocumentosCaja.Text = totaltarjeta.ToString();

            double totalparaCaja = totalGeneral + dineroenlacaja;
            txtTotalCaja.Text = totalparaCaja.ToString();
            ActualizarTotalDocumentos();

            CalcularDiferencia();

            // Limpiar el TextBox y prepararlo para nuevo ingreso
            txtMonto.Clear();
            txtMonto.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se hizo clic en una fila válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Confirmar eliminación
                DialogResult result = MessageBox.Show("¿Eliminar este monto?", "Confirmar",
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Obtener el monto de la columna correcta (verificar el nombre exacto)
                        double montoEliminar = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[1].Value); // Usar índice o nombre exacto

                        // Eliminar la fila
                        dataGridView1.Rows.RemoveAt(e.RowIndex);

                        // Actualizar total
                        totaltarjeta -= montoEliminar;
                        ActualizarTotalDocumentos();

                        totalGeneral -= montoEliminar;
                        txtTotalCaja.Text = totalGeneral.ToString();

                        CalcularDiferencia();


                        // Renumerar filas
                        RenumerarFilas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar: {ex.Message}", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ConfigurarDataGridViewMontos()
        {
            // Limpiar columnas existentes primero
            dataGridView1.Columns.Clear();

            // Crear columnas con nombres exactos
            DataGridViewTextBoxColumn colNumero = new DataGridViewTextBoxColumn();
            colNumero.Name = "Numero";
            colNumero.HeaderText = "N°";
            colNumero.Width = 50;
            colNumero.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(colNumero);

            DataGridViewTextBoxColumn colMonto = new DataGridViewTextBoxColumn();
            colMonto.Name = "Monto";
            colMonto.HeaderText = "Monto";
            colMonto.Width = 100;
            colMonto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colMonto.DefaultCellStyle.Format = "N2";
            dataGridView1.Columns.Add(colMonto);

            // Configuración general
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void RenumerarFilas()
        {
            contadorMontos = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                contadorMontos++;
                row.Cells["Numero"].Value = contadorMontos;
            }
        }

        private void ActualizarTotalDocumentos()
        {
            txtDocumentosCaja.Text = totaltarjeta.ToString();
        }

        

        private void LimpiarMontos()
        {
            dataGridView1.Rows.Clear();
            contadorMontos = 0;
            totalDocumentos = 0;
            ActualizarTotalDocumentos();
            txtMonto.Clear();
        }

        private void btnFiltrar_Click_1(object sender, EventArgs e)
        {
            DateTime fechaInicio = dtpFechaInicio.Value;
            DateTime fechaFin = dtpFechaFin.Value;

            // Validar que la fecha final no sea menor que la inicial
            if (fechaFin < fechaInicio)
            {
                MessageBox.Show("La fecha final no puede ser menor que la fecha inicial", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ReiniciarValores();

            CargarVentasPorRangoFechas(fechaInicio, fechaFin);
        }

        private void ReiniciarValores()
        {
            // Reiniciar variables de denominaciones grandes
            dgv1subirprecio1000 = 0;
            dgv1totalprecio1000 = 0;
            dgv1subirprecio500 = 0;
            dgv1totalprecio500 = 0;
            dgv1subirprecio200 = 0;
            dgv1totalprecio200 = 0;
            dgv1subirprecio100 = 0;
            dgv1totalprecio100 = 0;
            dgv1subirprecio50 = 0;
            dgv1totalprecio50 = 0;
            dgv1subirprecio20 = 0;
            dgv1totalprecio20 = 0;
            dgv1cantidad = 0;
            dgv1total = 0;

            // Reiniciar variables de denominaciones pequeñas
            dgv2subirprecio20 = 0;
            dgv2totalprecio20 = 0;
            dgv2subirprecio10 = 0;
            dgv2totalprecio10 = 0;
            dgv2subirprecio5 = 0;
            dgv2totalprecio5 = 0;
            dgv2subirprecio2 = 0;
            dgv2totalprecio2 = 0;
            dgv2subirprecio1 = 0;
            dgv2totalprecio1 = 0;
            dgv2subirprecio50 = 0;
            dgv2totalprecio50 = 0;
            dgv2subirpreciocentavos = 0;
            dgv2totalpreciocentavos = 0;
            dgv2cantidad = 0;
            dgv2total = 0;

            // Reiniciar totales
            totalGeneral = 0;
            totalefectivo = DatosGlobales.FondoCaja; // Mantener solo el fondo de caja
            totaltarjeta = 0;
            diferencia = 0;

            // Limpiar DataGridViews
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView1.Rows.Clear();
            contadorMontos = 0;

            // Reconstruir las filas iniciales
            dataGridView2.Rows.Add("1000 MXN", dgv1subirprecio1000, dgv1totalprecio1000 + " MXN");
            dataGridView2.Rows.Add("500 MXN", dgv1subirprecio500, dgv1totalprecio500 + " MXN");
            dataGridView2.Rows.Add("200 MXN", dgv1subirprecio200, dgv1totalprecio200 + " MXN");
            dataGridView2.Rows.Add("100 MXN", dgv1subirprecio100, dgv1totalprecio100 + " MXN");
            dataGridView2.Rows.Add("50 MXN", dgv1subirprecio50, dgv1totalprecio50 + " MXN");
            dataGridView2.Rows.Add("20 MXN", dgv1subirprecio20, dgv1totalprecio20 + " MXN");
            dataGridView2.Rows.Add("Total", dgv1cantidad, dgv1total + " MXN");

            dataGridView3.Rows.Add("20 MXN", dgv2subirprecio20, dgv2totalprecio20 + " MXN");
            dataGridView3.Rows.Add("10 MXN", dgv2subirprecio10, dgv2totalprecio10 + " MXN");
            dataGridView3.Rows.Add("5 MXN", dgv2subirprecio5, dgv2totalprecio5 + " MXN");
            dataGridView3.Rows.Add("2 MXN", dgv2subirprecio2, dgv2totalprecio2 + " MXN");
            dataGridView3.Rows.Add("1 MXN", dgv2subirprecio1, dgv2totalprecio1 + " MXN");
            dataGridView3.Rows.Add(".50 MXN", dgv2subirprecio50, dgv2totalprecio50 + " MXN");
            dataGridView3.Rows.Add("Total", dgv2cantidad, dgv2total + " MXN");

            // Actualizar controles de texto
            txtEfectivoCaja.Text = "0.00";
            txtDocumentosCaja.Text = "0.00";
            txtTotalCaja.Text = DatosGlobales.FondoCaja.ToString("N2"); // Mostrar solo el fondo de caja
            txtDiferencia.Text = "0.00";
            txtDiferencia.BackColor = SystemColors.Window;
            txtMonto.Clear();
        }
    }
}
