using Org.BouncyCastle.Math;
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

namespace VetPet_
{
    public partial class AlmacenHistorial : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;

        public AlmacenHistorial()
        {
            InitializeComponent();
            this.Load += AlmacenHistorial_Load;       // Evento Load
            this.Resize += AlmacenHistorial_Resize;   // Evento Resize
        }

        public AlmacenHistorial(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
            comboBox1.FlatStyle = FlatStyle.Flat;  // Quita bordes
            comboBox1.DropDownWidth = 150;         // Ancho del desplegable
            LlenarDataGridView();
        }
        private void AlmacenHistorial_Load(object sender, EventArgs e)
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
        private void LlenarDataGridView()
        {
            conexionBrandon conexion = new conexionBrandon();
            try
            {
                conexion.AbrirConexion();

                // Consulta SQL actualizada
                string query = @"
                SELECT 
                    p.numFactura AS Factura,
                    pr.nombre AS Proveedor,
                    prd.nombre AS Producto,
                    dp.cantidad AS Cantidad,
                    dp.precioProveedor AS Precio_Proveedor,
                    dp.precioVenta AS Precio_Venta,
                    dp.fechaCaducidad AS Fecha_Caducidad,
                    p.fechaRecibido AS Fecha_Recibido
                FROM Pedido p
                INNER JOIN Detalles_Pedido dp ON p.idPedido = dp.idPedido
                INNER JOIN Producto prd ON dp.idProducto = prd.idProducto
                INNER JOIN Proveedor pr ON p.idProveedor = pr.idProveedor
                ORDER BY p.fechaRecibido DESC"; 
        
        SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de pedidos: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void AlmacenHistorial_Resize(object sender, EventArgs e)
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

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenMenu(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProducto
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Eliminar Filtro":
                    // Mostrar todos los pedidos con sus productos
                    query = @"
            SELECT 
                p.numFactura AS Factura,
                pr.nombre AS Proveedor,
                prod.nombre AS Producto,
                dp.cantidad AS Cantidad,
                dp.precioProveedor AS Precio_Proveedor,
                dp.precioVenta AS Precio_Venta,
                dp.fechaCaducidad AS Fecha_Caducidad,
                p.fechaRecibido AS Fecha_Recibido
            FROM Pedido p
            INNER JOIN Detalles_Pedido dp ON p.idPedido = dp.idPedido
            INNER JOIN Producto prod ON dp.idProducto = prod.idProducto
            INNER JOIN Proveedor pr ON p.idProveedor = pr.idProveedor
            ORDER BY p.fechaRecibido DESC"; 
                    break;

                case "Productos":
                    // Mostrar solo los productos de los pedidos
                    query = @"
            SELECT 
                prod.nombre AS Producto,
                pr.nombre AS Proveedor,
                dp.cantidad AS Cantidad,
                dp.precioProveedor AS Precio_Proveedor,
                dp.precioVenta AS Precio_Venta,
                dp.fechaCaducidad AS Fecha_Caducidad,
                p.fechaRecibido AS Fecha_Recibido
            FROM Pedido p
            INNER JOIN Detalles_Pedido dp ON p.idPedido = dp.idPedido
            INNER JOIN Producto prod ON dp.idProducto = prod.idProducto
            INNER JOIN Proveedor pr ON p.idProveedor = pr.idProveedor
            ORDER BY p.fechaRecibido DESC";
                    break;

                default:
                    return; // Si no se selecciona ninguna opción válida, salir.
            }

            // Cargar los datos en el DataGridView
            CargarDatos(query);
        }
        private void CargarDatos(string query)
        {
            // Crear la conexión a la base de datos
            conexionBrandon conexion = new conexionBrandon();
            try
            {
                conexion.AbrirConexion();

                // Crear el comando SQL
                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());

                // Crear un DataAdapter para llenar el DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                // Llenar el DataTable con los datos de la base de datos
                da.Fill(dt);

                // Asignar el DataTable como la fuente de datos del DataGridView
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void txtProducto_Enter(object sender, EventArgs e)
        {
            if (txtProducto.Text == "Buscar nombre de producto") // Si el texto predeterminado está presente
            {
                txtProducto.Text = ""; // Limpia el TextBox
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreBusqueda = txtProducto.Text.Trim(); // Obtener el texto ingresado

            // Verificar si el campo de búsqueda está vacío
            if (string.IsNullOrEmpty(nombreBusqueda) || nombreBusqueda == "Buscar nombre de producto")
            {
                MessageBox.Show("Por favor, ingresa un nombre para buscar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Consulta SQL corregida para buscar solo en la tabla Producto
            string query = @"
                SELECT 
                    p.numFactura AS Factura,
                    pr.nombre AS Proveedor,
                    prd.nombre AS Producto,
                    dp.cantidad AS Cantidad,
                    dp.precioProveedor AS Precio_Proveedor,
                    dp.precioVenta AS Precio_Venta,
                    dp.fechaCaducidad AS Fecha_Caducidad,
                    p.fechaRecibido AS Fecha_Recibido
                FROM Pedido p
                INNER JOIN Detalles_Pedido dp ON p.idPedido = dp.idPedido
                INNER JOIN Producto prd ON dp.idProducto = prd.idProducto
                INNER JOIN Proveedor pr ON p.idProveedor = pr.idProveedor
                WHERE prd.nombre LIKE @Nombre
                ORDER BY p.fechaRecibido DESC"; 

        // Llamar al método para cargar los datos con la consulta
        CargarDatos(query, "%" + nombreBusqueda + "%");
        }

        private void CargarDatos(string query, string parametro)
        {
            conexionBrandon conexion = new conexionBrandon();
            try
            {
                conexion.AbrirConexion();

                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@Nombre", parametro);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el producto: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

    }
}
