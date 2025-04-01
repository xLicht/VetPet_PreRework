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
using VetPet_.Angie.Ventas;
using static VetPet_.CitaDueños;

namespace VetPet_
{
    public partial class VentasVentanadePago : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;
        public string FormularioOrigen { get; set; }
        private int idCita1;
        private int stock;
        private static int idDueño1;
        private static int idPersona;
        public DateTime fechaRegistro;
        public string nombreRecepcionista;
        public decimal total;
        public decimal? efectivo;
        public decimal? tarjeta;
        decimal nuevoSubtotal = 0;
        decimal totalGeneral = 0;
        public int idVenta;
        public static decimal MontoPagadoE = 0;
        public static decimal MontoPagadoT = 0;
        private static DataTable dtProductos = new DataTable();
        List<Tuple<string, decimal, int>> ListaProductos = new List<Tuple<string, decimal, int>>();

        List<Tuple<string, decimal, int>> ListaServicios = new List<Tuple<string, decimal, int>>();
        Mismetodos mismetodos = new Mismetodos();
        public  decimal montoRestante;

        public VentasVentanadePago(Form1 parent, int idCita, int idDueño, string tabla)
        {
            InitializeComponent();
            this.Load += VentasVentanadePago_Load;       // Evento Load
            this.Resize += VentasVentanadePago_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            idCita1 = idCita;
            CargarServicios(idCita);
            if (tabla == "Empleado")
                idPersona = idDueño;
        }
     public VentasVentanadePago(Form1 parent, int idCita, decimal nuevoSubtotal, DataTable dt, decimal montoPagado, bool tipoPago)
    {
        InitializeComponent();
        this.Load += VentasVentanadePago_Load;
        this.Resize += VentasVentanadePago_Resize;
    
        idCita1 = idCita;
        parentForm = parent;  
        AgregarProductos(dt);
        CargarServicios(idCita);
        this.montoRestante = nuevoSubtotal;
        ActualizarPago(montoPagado, tipoPago);
        if (dtProductos.Columns.Count == 0)
        {
            dtProductos = dt.Clone();
        }
    }

        private void AgregarProductos(DataTable dtNuevos)
        {
            foreach (DataRow row in dtNuevos.Rows)
            {
                int idProducto = row.Field<int>("idProducto");

                DataRow existingRow = dtProductos.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("idProducto") == idProducto);

                if (existingRow == null)
                {
                    // Si no existe, agregarlo
                    dtProductos.ImportRow(row);
                }
                else
                {
                    // Sumar cantidad y total
                    existingRow["cantidad"] = Convert.ToInt32(existingRow["cantidad"]) + Convert.ToInt32(row["cantidad"]);
                    existingRow["Total"] = Convert.ToDecimal(existingRow["Total"]) + Convert.ToDecimal(row["Total"]);
                }
            }
        }


        public void CargarServicios(int idCita)
        {
            try
            {
                mismetodos.AbrirConexion();
                string queryMedicamentos = @"SELECT 
            P.idProducto,
            M.nombreGenérico AS Producto,
            RM.cantidad,
            P.precioVenta AS Precio,
            (P.precioVenta * RM.cantidad) AS Total
        FROM Receta_Medicamento RM
        INNER JOIN Medicamento M ON RM.idMedicamento = M.idMedicamento
        INNER JOIN Producto P ON M.idProducto = P.idProducto
        INNER JOIN Receta R ON RM.idReceta = R.idReceta
        INNER JOIN Consulta C ON R.idConsulta = C.idConsulta
        WHERE C.idCita = @idCita AND RM.estado = 'A'";

                using (SqlDataAdapter da = new SqlDataAdapter(queryMedicamentos, mismetodos.GetConexion()))
                {
                    da.SelectCommand.Parameters.AddWithValue("@idCita", idCita);
                    DataTable tempMedicamentos = new DataTable();
                    da.Fill(tempMedicamentos);

                    if (dtProductos == null || dtProductos.Columns.Count == 0)
                    {
                        // Si dtProductos no existe o está vacío, copia la estructura de tempMedicamentos
                        dtProductos = tempMedicamentos.Clone();

                        AgregarProductos(tempMedicamentos);
                    }
                }

                dataGridView2.DataSource = dtProductos;
                ConfigurarGridMedicamentos(dataGridView2);

                string queryServicios = "EXEC sp_ObtenerServiciosCitaConId @idCita = @idCita;";
                DataTable tablaServicios = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(queryServicios, mismetodos.GetConexion()))
                {
                    da.SelectCommand.Parameters.AddWithValue("@idCita", idCita);
                    da.Fill(tablaServicios);
                }

                dataGridView1.DataSource = tablaServicios;
                ConfigurarGridServicios(dataGridView1);

                // Actualizar lista de servicios
                ActualizarListaServicios(tablaServicios);

                ObtenerInfoDueñoYMascota(idCita);

                // 4. Calcular totales
                CalcularTotalesCompletos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }
        private void ConfigurarGridMedicamentos(DataGridView grid)
        {
          
            if (grid.Columns.Contains("cantidad"))
                grid.Columns["cantidad"].HeaderText = "Cantidad";

            grid.Columns["Precio"].DefaultCellStyle.Format = "C2";
            if (dataGridView2.Columns.Contains("Total"))
            {
                dataGridView2.Columns["Total"].DefaultCellStyle.Format = "C2";
            }

        }

        private void ConfigurarGridServicios(DataGridView grid)
        {
            string[] columnasOcultar = { "idCita", "idServicioRealizado", "ServicioPadre" };
            foreach (string columna in columnasOcultar)
            {
                if (grid.Columns.Contains(columna))
                    grid.Columns[columna].Visible = false;
            }
        }

        private void ActualizarListaServicios(DataTable tablaServicios)
        {
            ListaServicios.Clear();
            foreach (DataRow row in tablaServicios.Rows)
            {
                if (row["Servicio"] != DBNull.Value)
                {
                    string nombre = row["Servicio"].ToString();
                    decimal precio = Convert.ToDecimal(row["Precio"]);
                    ListaServicios.Add(Tuple.Create(nombre, precio, 1));
                }
            }
        }

        private void ObtenerInfoDueñoYMascota(int idCita)
        {
            string query = @"SELECT 
        p.idPersona,
        P.nombre AS NombrePersona,
        P.apellidoP AS ApellidoPaterno, 
        M.nombre AS NombreMascota
    FROM 
        Cita C
    JOIN Mascota M ON C.idMascota = M.idMascota
    JOIN Persona P ON M.idPersona = P.idPersona
    WHERE C.idCita = @idCita";

            using (SqlCommand cmd = new SqlCommand(query, mismetodos.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@idCita", idCita);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        idDueño1 = Convert.ToInt32(reader["idPersona"]);
                        textBox3.Text = reader["NombrePersona"].ToString();
                        textBox4.Text = reader["ApellidoPaterno"].ToString();
                        textBox5.Text = reader["NombreMascota"].ToString();
                    }
                    else
                    {
                        textBox3.Text = "No se encontró la cita";
                    }
                }
            }
        }
        public void ActualizarPago(decimal monto, bool tipoPago)
        {
            if (tipoPago == true)
            {
                MontoPagadoE += monto;
            }
            if (tipoPago ==  false)
            {
                MontoPagadoT += monto;
            }

            CalcularTotalesCompletos(); // Actualizar la interfaz
        }
        private void CalcularTotalesCompletos()
        {
            try
            {
                // Calcular totales
                decimal totalServicios = ListaServicios.Sum(s => s.Item2);
                decimal totalProductos = dtProductos?.AsEnumerable()
                                       .Where(r => r["Total"] != DBNull.Value)
                                       .Sum(r => Convert.ToDecimal(r["Total"])) ?? 0;

               totalGeneral = totalServicios + totalProductos;

                // Actualizar controles
                textBox8.Text = "Subtotal: " + totalGeneral.ToString("C2");
                textBox9.Text = MontoPagadoE.ToString("0.00");
                textBox10.Text = MontoPagadoT.ToString("0.00");

                // Calcular monto restante
                montoRestante = totalGeneral - (MontoPagadoE + MontoPagadoT);
                textBox17.Text = montoRestante.ToString("0.00");

                // Determinar estado del pago
                if (totalGeneral > 0)
                {
                    bool estaPagado = (MontoPagadoE + MontoPagadoT) >= totalGeneral;
                    textBox7.Text = estaPagado ? "Pagado" : "Pendiente";
                    textBox7.BackColor = estaPagado ? Color.LightGreen : Color.LightPink;
                }
                else
                {
                    textBox7.Text = "Sin cargos";
                    textBox7.BackColor = SystemColors.Control;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular totales: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public VentasVentanadePago(Form1 parent, int idCita)
        {
            InitializeComponent();
            this.Load += VentasVentanadePago_Load;       // Evento Load
            this.Resize += VentasVentanadePago_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            CargarServicios(idCita);
            idCita1 = idCita;
        }
        private void VentasVentanadePago_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }

            try
            {
                // Crear instancia de la conexión
                mismetodos.AbrirConexion();

                // Consulta SQL para obtener el nombre y apellidoP de la persona
                string query = "SELECT nombre, apellidoP FROM Persona WHERE idPersona = @idPersona";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    // Agregar parámetro a la consulta
                    comando.Parameters.AddWithValue("@idPersona", idDueño1);

                    // Ejecutar consulta
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read()) // Si hay resultados
                        {
                            textBox3.Text = lector["nombre"].ToString();
                            textBox4.Text = lector["apellidoP"].ToString();
                        }
                    }
                }

                string query1 = "SELECT nombre, apellidoP FROM Persona WHERE idPersona = @idPersona";

                using (SqlCommand comando = new SqlCommand(query1, mismetodos.GetConexion()))
                {
                    // Agregar parámetro a la consulta
                    comando.Parameters.AddWithValue("@idPersona", idPersona);

                    // Ejecutar consulta
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read()) // Si hay resultados
                        {
                            nombreRecepcionista = lector["nombre"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos: " + ex.Message);
            }
            finally
            {
                // Cerrar conexión
                mismetodos.CerrarConexion();
            }

        }
        private void VentasVentanadePago_Resize(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasListado(parentForm)); // Pasamos la referencia de Form1 a 
            mismetodos.CerrarConexion();
            idDueño1 = 0;
            idPersona = 0;
            MontoPagadoE = 0;
            MontoPagadoT = 0;
            montoRestante = 0;
            dtProductos = new DataTable();

            ListaProductos.Clear();
            ListaServicios.Clear();

            nuevoSubtotal = 0;
            efectivo = null;
            tarjeta = null;
        }

        private void textBox15_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ConsultarCita(parentForm,idCita1)); // Pasamos la referencia de Form1 a 
        }

        private void textBox13_Click(object sender, EventArgs e)
        {
            VentasConfirmacionEfectivo VentasConfirmacionEfectivo = new VentasConfirmacionEfectivo(parentForm, montoRestante, dtProductos,idCita1);
            VentasConfirmacionEfectivo.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionEfectivo); // Usar la misma instancia
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            VentasConfirmacionTarjeta VentasConfirmacionTarjeta = new VentasConfirmacionTarjeta(parentForm, montoRestante, dtProductos,idCita1);
            VentasConfirmacionTarjeta.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionTarjeta); // Usar la misma instancia
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            VentasAgregarProducto VentasAgregarProducto = new VentasAgregarProducto(parentForm,0, totalGeneral,stock, idCita1,0);
            VentasAgregarProducto.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasAgregarProducto); // Usar la misma instancia
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            VentasAgregarMedicamento ventasAgregarMedicamento = new VentasAgregarMedicamento(parentForm, 0, totalGeneral, stock,idCita1,0);
            ventasAgregarMedicamento.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(ventasAgregarMedicamento); // Usar la misma instancia
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "Pagado")
            {
                MessageBox.Show("No se ha terminado de pagar");
            }
            else
            {
                try
                {
                    mismetodos.AbrirConexion();

                    fechaRegistro = DateTime.Now;
                    total = totalGeneral;
                    char pagado = 'S';
                    efectivo = string.IsNullOrWhiteSpace(textBox9.Text) ? (decimal?)null : Convert.ToDecimal(textBox9.Text.Trim());
                    tarjeta = string.IsNullOrWhiteSpace(textBox10.Text) ? (decimal?)null : Convert.ToDecimal(textBox10.Text.Trim());
                    char estado = 'A';
                    string insertVenta = @"
                    INSERT INTO Venta (fechaRegistro, total, pagado, efectivo, tarjeta, idCita, idPersona, idEmpleado, estado)
                    VALUES (@fechaRegistro, @total, @pagado, @efectivo, @tarjeta, @idCita, @idPersona, @idEmpleado,@estado);
                    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(insertVenta, mismetodos.GetConexion()))
                    {
                        cmd.Parameters.AddWithValue("@fechaRegistro", fechaRegistro);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@pagado", pagado);
                        cmd.Parameters.AddWithValue("@efectivo", (object)efectivo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tarjeta", (object)tarjeta ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@idCita", (object)idCita1 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@idPersona", (object)idDueño1 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@idEmpleado", idPersona);
                        cmd.Parameters.AddWithValue("@estado", estado);

                        idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    MessageBox.Show($"Venta registrada con éxito. ID: {idVenta}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar la venta: " + ex.Message);
                }
                finally
                {
                    mismetodos.CerrarConexion();
                }

                try
                {
                    mismetodos.AbrirConexion();

                    foreach (DataRow row in dtProductos.Rows)
                    {
                        int idProducto = Convert.ToInt32(row["idProducto"]);
                        decimal precio = Convert.ToDecimal(row["Precio"]);
                        string nombre = Convert.ToString(row["Producto"]);
                        decimal total = Convert.ToDecimal(row["Total"]);

                        // Calcular la cantidad vendida (Total / Precio)
                        int cantidadVendida = (int)Math.Round(total / precio, MidpointRounding.AwayFromZero);

                        ListaProductos.Add(Tuple.Create(nombre, precio, cantidadVendida));

                        // Obtener lotes disponibles ordenados por fecha de caducidad (los que caducan primero)
                        string queryLotes = @"
                        SELECT idLote_Producto, stock 
                        FROM Lote_Producto 
                        WHERE idProducto = @idProducto
                          AND estado = 'A'
                          AND (fechaCaducidad IS NULL OR fechaCaducidad >= GETDATE())
                        ORDER BY ISNULL(fechaCaducidad, '9999-12-31') ASC;";

                        DataTable lotes = new DataTable();
                        using (SqlCommand cmdLotes = new SqlCommand(queryLotes, mismetodos.GetConexion()))
                        {
                            cmdLotes.Parameters.AddWithValue("@idProducto", idProducto);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmdLotes))
                            {
                                da.Fill(lotes);
                            }
                        }

                        int cantidadRestante = cantidadVendida;

                        // Procesar cada lote hasta cubrir la cantidad vendida
                        foreach (DataRow lote in lotes.Rows)
                        {
                            int idLote = Convert.ToInt32(lote["idLote_Producto"]);
                            int stockLote = Convert.ToInt32(lote["stock"]);

                            if (cantidadRestante <= 0) break;

                            int cantidadADescontar = Math.Min(stockLote, cantidadRestante);
                            int nuevoStockLote = stockLote - cantidadADescontar;

                            // Actualizar el lote específico
                            string updateLoteQuery = @"
                            UPDATE Lote_Producto 
                            SET stock = @nuevoStock 
                            WHERE idLote_Producto = @idLote;";

                            using (SqlCommand cmdUpdateLote = new SqlCommand(updateLoteQuery, mismetodos.GetConexion()))
                            {
                                cmdUpdateLote.Parameters.AddWithValue("@nuevoStock", nuevoStockLote);
                                cmdUpdateLote.Parameters.AddWithValue("@idLote", idLote);
                                cmdUpdateLote.ExecuteNonQuery();
                            }

                            cantidadRestante -= cantidadADescontar;
                        }

                        if (cantidadRestante > 0)
                        {
                            MessageBox.Show($"Advertencia: No hay suficiente stock para el producto {nombre}. Faltaron {cantidadRestante} unidades.");
                        }

                        // Insertar en Venta_Producto (relación entre venta y producto)
                        string insertVentaProducto = @"
                        INSERT INTO Venta_Producto (idVenta, idProducto, estado)
                        VALUES (@idVenta, @idProducto, 'A');";

                        using (SqlCommand cmdVentaProducto = new SqlCommand(insertVentaProducto, mismetodos.GetConexion()))
                        {
                            cmdVentaProducto.Parameters.AddWithValue("@idVenta", idVenta);
                            cmdVentaProducto.Parameters.AddWithValue("@idProducto", idProducto);
                            cmdVentaProducto.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Producto: {nombre} - Unidades vendidas: {cantidadVendida}");
                    }


                    string fechaLimpia = DateTime.Now.ToString("dd-MM-yyyy-H-m");
                    string nombreTicket = "Ticket_0-" + fechaLimpia.Replace("-", "");

                    parentForm.formularioHijo(new VentasVerTicket(parentForm, idVenta, idDueño1, nombreTicket, nombreRecepcionista, textBox3.Text, textBox5.Text, fechaRegistro.ToString(),
                       ListaServicios, ListaProductos, total.ToString(), efectivo.ToString(), tarjeta.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el stock: " + ex.Message);
                }
                finally
                {
                    mismetodos.CerrarConexion();
                    idDueño1 = 0;
                    idPersona = 0;
                    MontoPagadoE = 0;
                    MontoPagadoT = 0;
                    montoRestante = 0;
                    dtProductos = new DataTable();

                    ListaProductos.Clear();
                    ListaServicios.Clear();

                    nuevoSubtotal = 0;
                    efectivo = null;
                    tarjeta = null;

                }
            }
        }

    }
}
