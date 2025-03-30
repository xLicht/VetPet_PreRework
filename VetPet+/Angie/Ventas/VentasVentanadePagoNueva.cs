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

        decimal totalCalculado = 0;
        decimal totalProducto = 0;
        decimal totalServicio = 0;
        public VentasVentanadePagoNueva(Form1 parent, int idVenta)
        {
            InitializeComponent();
            this.Load += VentasVentanadePagoNueva_Load;       // Evento Load
            this.Resize += VentasVentanadePagoNueva_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            this.idVenta = idVenta;
            CargarVentaCompleta();
        }
        public void CargarVentaCompleta()
        {
            try
            {
                mismetodos.AbrirConexion();

                // 1. Primero cargar los datos básicos de la venta
                CargarDatosVenta();

                // 2. Luego cargar los servicios asociados (si existe cita)
                CargarServiciosDeCita();

                // 3. Finalmente cargar los productos vendidos
                CargarProductosVenta(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar venta completa: {ex.Message}");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        private void CargarDatosVenta()
        {
            string query = @"
SELECT 
    ISNULL(V.efectivo, 0) AS totalEfectivo,
    ISNULL(V.tarjeta, 0) AS totalTarjeta,
    V.total,
    V.fechaRegistro,
    P.nombre AS nombreCliente,
    P.apellidoP AS apellidoCliente,
    M.nombre AS nombreMascota
FROM Venta V
LEFT JOIN Persona P ON V.idPersona = P.idPersona
LEFT JOIN Cita C ON C.idCita = V.idCita
LEFT JOIN Mascota M ON M.idMascota = C.idMascota
WHERE V.idVenta = @idVenta";

            using (SqlCommand cmd = new SqlCommand(query, mismetodos.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@idVenta", idVenta);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox9.Text = reader["totalEfectivo"].ToString();
                        textBox10.Text = reader["totalTarjeta"].ToString();
                        textBox8.Text = $"Subtotal: {reader["total"]}";
                        totalProducto = Convert.ToDecimal(reader["total"]);

                        textBox3.Text = reader["nombreCliente"] != DBNull.Value
                            ? reader["nombreCliente"].ToString() : "";
                        textBox4.Text = reader["apellidoCliente"] != DBNull.Value
                            ? reader["apellidoCliente"].ToString() : "";
                        textBox5.Text = reader["nombreMascota"] != DBNull.Value
                            ? reader["nombreMascota"].ToString() : "Sin mascota";
                    }
                }
            }
        }

        private void CargarServiciosDeCita()
        {
            string queryCita = "SELECT idCita FROM Venta WHERE idVenta = @idVenta";
            int? idCita = null;

            using (SqlCommand cmd = new SqlCommand(queryCita, mismetodos.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@idVenta", idVenta);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    idCita = Convert.ToInt32(result);
                }
            }

            if (idCita.HasValue)
            {
                using (SqlCommand cmd = new SqlCommand("EXEC sp_ObtenerServiciosCitaConId @idCita", mismetodos.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idCita", idCita.Value);

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    dataGridView1.DataSource = dt;
                    if (dt.Columns.Contains("NombreServicio"))
                        dataGridView1.Columns["NombreServicio"].HeaderText = "Servicio";
                    if (dt.Columns.Contains("Precio"))
                        dataGridView1.Columns["Precio"].HeaderText = "Precio";
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void CargarProductosVenta()
        {
            string query = @"
SELECT 
    PR.nombre AS Producto,
    MA.nombre AS Marca,
    PR.precioVenta AS Precio
FROM Venta_Producto VP
JOIN Producto PR ON VP.idProducto = PR.idProducto
LEFT JOIN Marca MA ON PR.idMarca = MA.idMarca
WHERE VP.idVenta = @idVenta";

            using (SqlCommand cmd = new SqlCommand(query, mismetodos.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@idVenta", idVenta);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                dataGridView2.DataSource = dt;

                // Formatear columna de precio
                if (dataGridView2.Columns["Precio"] != null)
                {
                    dataGridView2.Columns["Precio"].DefaultCellStyle.Format = "C2";
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
