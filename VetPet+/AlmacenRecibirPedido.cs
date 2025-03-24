using FontAwesome.Sharp;
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
    public partial class AlmacenRecibirPedido : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        private int idPedido; // Guardará el ID del pedido creado
        private List<DetallesPedido> listaDetalles = new List<DetallesPedido>(); // Lista de productos agregados

        public class DetallesPedido
        {
            public int IdPedido { get; set; }
            public int IdProducto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioProveedor { get; set; }
            public decimal PrecioVenta { get; set; }
            public DateTime FechaCaducidad { get; set; }
        }

        public AlmacenRecibirPedido()
        {
            InitializeComponent();
            this.Load += AlmacenRecibirPedido_Load;       // Evento Load
            this.Resize += AlmacenRecibirPedido_Resize;   // Evento Resize
        }

        public AlmacenRecibirPedido(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
            CargarCombos();
            CargarProductosEnComboBox();
        }


        private void AlmacenRecibirPedido_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }

            // Inicializar los TextBox de ID con "NULL"
            txtIdProveedor.Text = "NULL";
            txtIdProducto.Text = "NULL";
            txtIdMedicamento.Text = "NULL";

            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void CargarCombos()
        {
            conexionBrandon conexion = new conexionBrandon();
            try
            {
                this.cmbProveedor.SelectedIndexChanged += new System.EventHandler(this.cmbProveedor_SelectedIndexChanged);
                this.cmbProducto.SelectedIndexChanged += new System.EventHandler(this.cmbProducto_SelectedIndexChanged);

                conexion.AbrirConexion();

                // Cargar Proveedores
                string queryProveedor = "SELECT idProveedor, nombre FROM Proveedor";
                SqlDataAdapter daProveedor = new SqlDataAdapter(queryProveedor, conexion.GetConexion());
                DataTable dtProveedor = new DataTable();
                daProveedor.Fill(dtProveedor);

                cmbProveedor.DataSource = dtProveedor;
                cmbProveedor.DisplayMember = "nombre";  // Se muestra el nombre
                cmbProveedor.ValueMember = "idProveedor"; // Se guarda el ID en SelectedValue

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void CargarProductosEnComboBox()
        {
            conexionBrandon conexion = new conexionBrandon();
            try
            {
                conexion.AbrirConexion();

                string query = @"
                SELECT 
                    p.idProducto, 
                    COALESCE(m.nombreGenérico, p.nombre) AS NombreProducto
                FROM Producto p
                LEFT JOIN Medicamento m ON p.idProducto = m.idProducto
                WHERE p.estado = 'A'";

                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbProducto.DataSource = dt;
                cmbProducto.DisplayMember = "NombreProducto";  // Mostrar el nombre en el ComboBox
                cmbProducto.ValueMember = "idProducto"; // Guardar el idProducto internamente
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }


        private void AlmacenRecibirPedido_Resize(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conexionBrandon conexion = new conexionBrandon();
            if (listaDetalles.Count == 0)
            {
                MessageBox.Show("No hay productos en el pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conexion.AbrirConexion();

                foreach (var detalle in listaDetalles)
                {
                    string query = "INSERT INTO Detalles_Pedido (idPedido, idProducto, cantidad, precioProveedor, precioVenta, fechaCaducidad) " +
                                   "VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioProveedor, @PrecioVenta, @FechaCaducidad)";

                    SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                    cmd.Parameters.AddWithValue("@IdPedido", detalle.IdPedido);
                    cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioProveedor", detalle.PrecioProveedor);
                    cmd.Parameters.AddWithValue("@PrecioVenta", detalle.PrecioVenta);
                    cmd.Parameters.AddWithValue("@FechaCaducidad", detalle.FechaCaducidad);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Pedido guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listaDetalles.Clear(); // Limpiar lista
                dataGridView1.Rows.Clear(); // Limpiar DataGridView
                btnCrearPedido.Enabled = true; // Permitir nuevo pedido
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar detalles del pedido: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
                parentForm.formularioHijo(new AlmacenMenu(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
            }
        }


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenMenu(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }


        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue != null)
            {
                txtIdProveedor.Text = cmbProveedor.SelectedValue.ToString();
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue != null)
            {
                txtIdProducto.Text = cmbProducto.SelectedValue.ToString();
            }
        }


        private void txtFactura_Enter(object sender, EventArgs e)
        {
            if (txtFactura.Text == "Factura") // Si el texto predeterminado está presente
            {
                txtFactura.Text = ""; // Limpia el TextBox
            }
        }


        private void txtCantidad_Enter(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "Cantidad") // Si el texto predeterminado está presente
            {
                txtCantidad.Text = ""; // Limpia el TextBox
            }
        }

        private void txtTotal_Enter(object sender, EventArgs e)
        {
            if (txtTotal.Text == "Total") // Si el texto predeterminado está presente
            {
                txtTotal.Text = ""; // Limpia el TextBox
            }
        }

        private void btnCrearPedido_Click(object sender, EventArgs e)
        {
            conexionBrandon conexion = new conexionBrandon();
            try
            {
                conexion.AbrirConexion();

                string query = "INSERT INTO Pedido (numFactura, fechaRecibido, idProveedor) " +
                               "VALUES (@Factura, @FechaRecibido, @IdProveedor); SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@Factura", txtFactura.Text);
                cmd.Parameters.AddWithValue("@FechaRecibido", fechaRecibidoPicker.Value);
                cmd.Parameters.AddWithValue("@IdProveedor", cmbProveedor.SelectedValue);

                idPedido = Convert.ToInt32(cmd.ExecuteScalar()); // Obtener ID del pedido
                MessageBox.Show($"Pedido creado con ID: {idPedido}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnCrearPedido.Enabled = false; // Deshabilitar para evitar duplicados
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el pedido: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        decimal totalidad = 0;
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (idPedido == 0)
            {
                MessageBox.Show("Primero debes crear un pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int idProducto = Convert.ToInt32(cmbProducto.SelectedValue);
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                decimal precioProveedor = Convert.ToDecimal(txtPrecioProveedor.Text);
                decimal precioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                DateTime fechaCaducidad = fechaCaducidadPicker.Value;


                // Agregar a la lista
                listaDetalles.Add(new DetallesPedido
                {
                    IdPedido = idPedido,
                    IdProducto = idProducto,
                    Cantidad = cantidad,
                    PrecioProveedor = precioProveedor,
                    PrecioVenta = precioVenta,
                    FechaCaducidad = fechaCaducidad
                });


                totalidad += (precioProveedor * cantidad);
                txtTotal.Text = totalidad.ToString();
                // Agregar al DataGridView
                dataGridView1.Rows.Add(cmbProducto.Text, cantidad, precioProveedor, precioVenta, fechaCaducidad);

                MessageBox.Show("Producto agregado a la lista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        private void txtPrecioVenta_Enter(object sender, EventArgs e)
        {
            if (txtPrecioVenta.Text == "Precio de venta") // Si el texto predeterminado está presente
            {
                txtPrecioVenta.Text = ""; // Limpia el TextBox
            }
        }

        private void txtPrecioProveedor_Enter(object sender, EventArgs e)
        {
            if (txtPrecioProveedor.Text == "Precio proveedor") // Si el texto predeterminado está presente
            {
                txtPrecioProveedor.Text = ""; // Limpia el TextBox
            }
        }
    }
}
