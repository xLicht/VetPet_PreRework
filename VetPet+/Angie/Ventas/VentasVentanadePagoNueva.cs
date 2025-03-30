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
    public partial class VentasVentanadePagoNueva : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        Mismetodos mismetodos = new Mismetodos();

        int idVenta;
        public VentasVentanadePagoNueva(Form1 parent, int idVenta)
        {
            InitializeComponent();
            this.Load += VentasVentanadePagoNueva_Load;       // Evento Load
            this.Resize += VentasVentanadePagoNueva_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            this.idVenta = idVenta; 
            CargarVenta();
        }

        public void CargarVenta()
        {
            string query = @"
    SELECT 
        -- Datos de la venta
        ISNULL(V.efectivo, 0) AS totalEfectivo,
        ISNULL(V.tarjeta, 0) AS totalTarjeta,
        V.total,
        V.fechaRegistro,
        
        -- Datos del cliente
        P.nombre AS nombreCliente,
        P.apellidoP AS apellidoCliente,
        
        -- Datos de la mascota
        M.nombre AS nombreMascota,
        
        -- Datos de productos vendidos
        VP.idProducto,
        PR.nombre AS nombreProducto,
        MA.nombre AS marcaProducto,
        PR.precioVenta AS precioProducto
    FROM Venta V
    LEFT JOIN Persona P ON V.idPersona = P.idPersona
    LEFT JOIN Marca MA ON MA.idMarca = P.idMarca
    LEFT JOIN Cita C ON C.idCita = V.idVenta
    LEFT JOIN Mascota M ON M.idMascota = C.idMascota
    LEFT JOIN Venta_Producto VP ON V.idVenta = VP.idVenta
    LEFT JOIN Producto PR ON VP.idProducto = PR.idProducto
    WHERE V.idVenta = @idVenta
    ORDER BY PR.nombre;";

            using (SqlConnection conexion = mismetodos.GetConexion())
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idVenta", idVenta);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Limpiar controles existentes
                            textBox9.Text = "0";
                            textBox10.Text = "0";
                            textBox8.Text = "Subtotal: 0";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";

                            // Lista para productos
                            var productos = new List<string>();
                            decimal totalCalculado = 0;

                            while (reader.Read())
                            {
                                // Solo asignar una vez los datos de venta/cliente/mascota
                                if (!reader.HasRows || reader.IsDBNull(0))
                                {
                                    MessageBox.Show("No se encontró la venta con el ID especificado.");
                                    return;
                                }

                                // Datos de pago (solo primera fila)
                                if (productos.Count == 0)
                                {
                                    textBox9.Text = reader["totalEfectivo"].ToString();
                                    textBox10.Text = reader["totalTarjeta"].ToString();
                                    textBox8.Text = $"Subtotal: {reader["total"]}";

                                    // Datos del cliente
                                    textBox3.Text = reader["nombreCliente"] != DBNull.Value ?
                                                   reader["nombreCliente"].ToString() : "";
                                    textBox4.Text = reader["apellidoCliente"] != DBNull.Value ?
                                                  reader["apellidoCliente"].ToString() : "";

                                    // Datos de la mascota
                                    textBox5.Text = reader["nombreMascota"] != DBNull.Value ?
                                                         reader["nombreMascota"].ToString() : "Sin mascota";
                                }

                                // Procesar productos (todas las filas)
                                if (reader["idProducto"] != DBNull.Value)
                                {
                                    string productoInfo = $"{reader["nombreProducto"]} ({reader["marcaProducto"]}) - " +
                                                       $"${reader["precioProducto"]}";
                                    productos.Add(productoInfo);
                                    totalCalculado += Convert.ToDecimal(reader["precioProducto"]);
                                }
                            }

                            // Mostrar productos en un ListBox o DataGridView
                            dataGridView1.DataSource = productos;
                            // O alternativamente en un TextBox multilínea:
                            // textBoxProductos.Text = string.Join(Environment.NewLine, productos);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar venta: {ex.Message}\n\nDetalles:\n{ex.StackTrace}");
                }
            }
        }
        private void VentasVentanadePagoNueva_Load(object sender, EventArgs e)
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

        private void VentasVentanadePagoNueva_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new VentasHistorialdeVentas(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ConsultarCita(parentForm)); // Pasamos la referencia de Form1 a 
        }
    }
}
