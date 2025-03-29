using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_.Angie;
using VetPet_.Angie.Mascotas;
using VetPet_.Angie.Ventas;

namespace VetPet_
{
    public partial class VentasNuevaVenta : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;
        public string FormularioOrigen { get; set; }

        private int idCita;
        private int stock;
        private int idDueño1;
        private static int idPersona;
        public DateTime fechaRegistro;
        public string nombreRecepcionista;
        public decimal total;
        public decimal? efectivo;
        public decimal? tarjeta;
        decimal sumaTotalProductos = 0;
        decimal nuevoSubtotal = 0;
        public int idVenta;
        public static decimal MontoPagadoE = 0;
        public static decimal MontoPagadoT = 0;
        private static DataTable dtProductos = new DataTable();
        List<Tuple<string, decimal, int>> ListaProductos = new List<Tuple<string, decimal, int>>();

        List<Tuple<string, decimal, int>> ListaServicios = new List<Tuple<string, decimal, int>>();
        Mismetodos mismetodos = new Mismetodos();
        public VentasNuevaVenta(Form1 parent)
        {
            InitializeComponent();
            this.Load += VentasNuevaVenta_Load;       // Evento Load
            this.Resize += VentasNuevaVenta_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
        }
        public VentasNuevaVenta(Form1 parent, int idDueño, string tabla)
        {
            InitializeComponent();
            this.Load += VentasNuevaVenta_Load;       // Evento Load
            this.Resize += VentasNuevaVenta_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            if (tabla == "Dueño") 
            idDueño1 = idDueño;
            if (tabla == "Empleado")
            idPersona = idDueño;
        }
        public VentasNuevaVenta(Form1 parent, decimal nuevoSubtotal, DataTable dt, decimal montoPagado, bool bandera)
        {
            InitializeComponent();
            this.Load += VentasNuevaVenta_Load;       // Evento Load
            this.Resize += VentasNuevaVenta_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            this.nuevoSubtotal = nuevoSubtotal;
            if (bandera == true)
            {
                MontoPagadoE = montoPagado;
            }
            if (bandera == false)
            {
                MontoPagadoT = montoPagado;
            }
            if (dtProductos.Columns.Count == 0)
            {
                dtProductos = dt.Clone();
            }

            // Agregar los nuevos productos o medicamentos sin perder los anteriores
            AgregarProductosAMedicamentos(dt);
        }
        
        private void AgregarProductosAMedicamentos(DataTable dtNuevos)
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
                    // Si ya existe, actualizar el total sumando el nuevo subtotal
                    existingRow["Total"] = Convert.ToDecimal(existingRow["Total"]) + Convert.ToDecimal(row["Total"]);
                }
            }
        }
        public void ActualizarSumaTotal()
        {
            // Sumar el total de productos
            sumaTotalProductos = dtProductos.AsEnumerable()
                .Where(r => r["Total"] != DBNull.Value)
                .Sum(r => r.Field<decimal>("Total"));

            textBox8.Text = "Subtotal: " + sumaTotalProductos.ToString("0.###");

            textBox9.Text = MontoPagadoE.ToString();
            textBox10.Text = MontoPagadoT.ToString();

            decimal montoRestante = sumaTotalProductos - (MontoPagadoE + MontoPagadoT);
            textBox2.Text = montoRestante.ToString();
            if (MontoPagadoE + MontoPagadoT == sumaTotalProductos)
            {
                textBox7.Text = "Pagado";
            }
        }
        private void VentasNuevaVenta_Load(object sender, EventArgs e)
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
                string query = "SELECT idPersona,nombre, apellidoP FROM Persona WHERE idPersona = @idPersona";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    // Agregar parámetro a la consulta
                    comando.Parameters.AddWithValue("@idPersona", idDueño1);

                    // Ejecutar consulta
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read()) // Si hay resultados
                        {

                            textBox15.Text = lector["idPersona"].ToString();
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

            BindingSource bs = new BindingSource();
            bs.DataSource = dtProductos;
            dataGridView2.DataSource = bs;

            ActualizarSumaTotal();

        }

        private void VentasNuevaVenta_Resize(object sender, EventArgs e)
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
        private void textBox11_Click(object sender, EventArgs e)
        {
            VentasAgregarMedicamento ventasAgregarMedicamento = new VentasAgregarMedicamento(parentForm);
            ventasAgregarMedicamento.FormularioOrigen = "VentasNuevaVenta"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(ventasAgregarMedicamento); // Usar la misma instancia
        }


        private void textBox12_Click(object sender, EventArgs e)
        {
            VentasAgregarProducto VentasAgregarProducto = new VentasAgregarProducto(parentForm,idCita,sumaTotalProductos,stock);
            VentasAgregarProducto.FormularioOrigen = "VentasNuevaVenta"; // Asignar FormularioOri
            parentForm.formularioHijo(VentasAgregarProducto); // Pasamos la referencia de Form1 a 
        }

        private void textBox13_Click(object sender, EventArgs e)
        {
            VentasConfirmacionEfectivo VentasConfirmacionEfectivo = new VentasConfirmacionEfectivo(parentForm, sumaTotalProductos, dtProductos);
            VentasConfirmacionEfectivo.FormularioOrigen = "VentasNuevaVenta";
            parentForm.formularioHijo(VentasConfirmacionEfectivo);
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            VentasConfirmacionTarjeta VentasConfirmacionTarjeta = new VentasConfirmacionTarjeta(parentForm, sumaTotalProductos,dtProductos);
            VentasConfirmacionTarjeta.FormularioOrigen = "VentasNuevaVenta"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionTarjeta); // Usar la misma instancia
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasListado(parentForm));
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
                    total = sumaTotalProductos;
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
                        cmd.Parameters.AddWithValue("@idCita", DBNull.Value);
                        if (idDueño1 != 0)
                        {
                            cmd.Parameters.AddWithValue("@idPersona", idDueño1);
                        }
                        else 
                        { 
                            cmd.Parameters.AddWithValue("@idPersona", DBNull.Value);                             
                        }
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

                // Actualizar stock 

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

                        string queryStock = "SELECT stock FROM Producto WHERE idProducto = @idProducto;";
                        int stockActual;

                        using (SqlCommand cmdStock = new SqlCommand(queryStock, mismetodos.GetConexion()))
                        {
                            cmdStock.Parameters.AddWithValue("@idProducto", idProducto);
                            stockActual = Convert.ToInt32(cmdStock.ExecuteScalar());
                        }

                        // Calcular el nuevo stock
                        int nuevoStock = stockActual - cantidadVendida;

                        // Actualizar el stock en la base de datos
                        string updateQuery = "UPDATE Producto SET stock = @nuevoStock WHERE idProducto = @idProducto;";

                        using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, mismetodos.GetConexion()))
                        {
                            cmdUpdate.Parameters.AddWithValue("@nuevoStock", nuevoStock);
                            cmdUpdate.Parameters.AddWithValue("@idProducto", idProducto);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Producto ID: {idProducto} - Stock actualizado: {nuevoStock} (Se vendieron {cantidadVendida} unidades)");
                    }
                    parentForm.formularioHijo(new VentasVerTicket(parentForm, idVenta, idDueño1, nombreRecepcionista, textBox3.Text, " ", fechaRegistro.ToString(),
                        ListaServicios, ListaProductos, total.ToString(), efectivo.ToString(), tarjeta.ToString(), total.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el stock: " + ex.Message);
                }
                finally
                {
                    mismetodos.CerrarConexion();
                }
            }
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasSeleccionarDueño(parentForm)); // Pasamos la referencia de Form1 a 
        }
    }
}
