using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class AlmacenAgregarProducto : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        public string ProveedorSeleccionado { get; set; }

        public AlmacenAgregarProducto()
        {
            InitializeComponent();
            this.Load += AlmacenAgregarProducto_Load;
            this.Resize += AlmacenAgregarProducto_Resize;
        }

        public AlmacenAgregarProducto(Form1 parent, string proveedor = null)
        {
            InitializeComponent();
            parentForm = parent;
            ProveedorSeleccionado = proveedor;
        }

        private void AlmacenAgregarProducto_Load(object sender, EventArgs e)
        {
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }

            // Cargar los proveedores, marcas y tipos de producto
            CargarComboBoxProveedor();
            CargarComboBoxMarca();
            CargarComboBoxTipoProducto();
        }

        private void CargarComboBoxProveedor()
        {
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            string query = "SELECT idProveedor, nombre FROM Proveedor";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbIdProveedor.Items.Clear();

                    while (reader.Read())
                    {
                        cmbIdProveedor.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los proveedores: " + ex.Message);
                }
                finally
                {
                    conexion.GetConexion().Close();
                }
            }
        }

        private void CargarComboBoxMarca()
        {
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            string query = "SELECT idMarca, nombre FROM Marca";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbIdMarca.Items.Clear();

                    while (reader.Read())
                    {
                        cmbIdMarca.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las marcas: " + ex.Message);
                }
                finally
                {
                    conexion.GetConexion().Close();
                }
            }
        }

        private void CargarComboBoxTipoProducto()
        {
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            string query = "SELECT idTipoProducto, nombre FROM TipoProducto";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbIdTipoProducto.Items.Clear();

                    while (reader.Read())
                    {
                        cmbIdTipoProducto.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los tipos de producto: " + ex.Message);
                }
                finally
                {
                    conexion.GetConexion().Close();
                }
            }
        }

        private void AlmacenAgregarProducto_Resize(object sender, EventArgs e)
        {
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in this.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];
                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);
                    control.Font = new Font(control.Font.FontFamily, info.fontSize * Math.Min(scaleX, scaleY));
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Verificar que todos los campos están completos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioProveedor.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioVenta.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                string.IsNullOrWhiteSpace(txtIdProveedor.Text) ||
                string.IsNullOrWhiteSpace(txtIdMarca.Text) ||
                string.IsNullOrWhiteSpace(txtIdTipoProducto.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Intentar convertir los campos de texto a valores numéricos
            if (!decimal.TryParse(txtPrecioProveedor.Text, out decimal PrecioProveedor) ||
                !decimal.TryParse(txtPrecioVenta.Text, out decimal PrecioVenta) ||
                !int.TryParse(txtStock.Text, out int Stock) ||
                !int.TryParse(txtIdProveedor.Text, out int IdProveedor) ||
                !int.TryParse(txtIdMarca.Text, out int IdMarca) ||
                !int.TryParse(txtIdTipoProducto.Text, out int IdTipoProducto))
            {
                MessageBox.Show("Los campos numéricos deben tener valores válidos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO Producto (nombre, descripcion, precioProveedor, precioVenta, cantidad, stock, fechaCaducidad, idMarca, idTipoProducto, idProveedor) " +
                           "VALUES (@Nombre, @Descripcion, @PrecioProveedor, @PrecioVenta, @Cantidad, @Stock, @FechaCaducidad, @IdMarca, @IdTipoProducto, @IdProveedor)";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@PrecioProveedor", PrecioProveedor);
                    cmd.Parameters.AddWithValue("@PrecioVenta", PrecioVenta);
                    cmd.Parameters.AddWithValue("@Cantidad", txtCantidad.Text);
                    cmd.Parameters.AddWithValue("@Stock", Stock);
                    cmd.Parameters.AddWithValue("@IdMarca", IdMarca);
                    cmd.Parameters.AddWithValue("@IdTipoProducto", IdTipoProducto);
                    cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    cmd.Parameters.AddWithValue("@FechaCaducidad", fechaVencimientoPicker.Value);

                    cmd.ExecuteNonQuery();
                    conexion.GetConexion().Close();

                    MessageBox.Show("Datos insertados correctamente en 'Producto'.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar datos: " + ex.Message);
                }
                finally
                {
                    conexion.GetConexion().Close();
                }
            }
        }

        private void cmbIdMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdMarca.SelectedIndex != -1)
            {
                string nombreMarca = cmbIdMarca.SelectedItem.ToString();
                string query = "SELECT idMarca FROM Marca WHERE nombre = @Nombre";

                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreMarca);
                    try
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            txtIdMarca.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el id para la marca seleccionada.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener el id de la marca: " + ex.Message);
                    }
                    finally
                    {
                        conexion.GetConexion().Close();
                    }
                }
            }
        }

        private void cmbIdTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdTipoProducto.SelectedIndex != -1)
            {
                string nombreTipoProducto = cmbIdTipoProducto.SelectedItem.ToString();
                string query = "SELECT idTipoProducto FROM TipoProducto WHERE nombre = @Nombre";

                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreTipoProducto);
                    try
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            txtIdTipoProducto.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el id para el tipo de producto seleccionado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener el id del tipo de producto: " + ex.Message);
                    }
                    finally
                    {
                        conexion.GetConexion().Close();
                    }
                }
            }
        }

        private void cmbIdProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdProveedor.SelectedIndex != -1)
            {
                string nombreProveedor = cmbIdProveedor.SelectedItem.ToString();
                string query = "SELECT idProveedor FROM Proveedor WHERE nombre = @Nombre";

                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreProveedor);
                    try
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            txtIdProveedor.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el id para el proveedor seleccionado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener el id del proveedor: " + ex.Message);
                    }
                    finally
                    {
                        conexion.GetConexion().Close();
                    }
                }
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            if (txtNombre.Text == "Nombre de producto") // Si el texto predeterminado está presente
            {
                txtNombre.Text = ""; // Limpia el TextBox
            }
        }

        private void txtCantidad_Enter(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "Cantidad de producto") // Si el texto predeterminado está presente
            {
                txtCantidad.Text = ""; // Limpia el TextBox
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

        private void txtStock_Enter(object sender, EventArgs e)
        {
            if (txtStock.Text == "Stock") // Si el texto predeterminado está presente
            {
                txtStock.Text = ""; // Limpia el TextBox
            }
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "Descripción de producto") // Si el texto predeterminado está presente
            {
                txtDescripcion.Text = ""; // Limpia el TextBox
            }
        }
    }
}
