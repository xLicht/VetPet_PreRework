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
    public partial class AlmacenModificarProducto : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;
        private string nombreProducto;

        public AlmacenModificarProducto()
        {
            this.Load += AlmacenModificarProducto_Load;       // Evento Load
            this.Resize += AlmacenModificarProducto_Resize;   // Evento Resize
        }
        public AlmacenModificarProducto(Form1 parent, string nombreProducto = null)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
            this.nombreProducto = nombreProducto;
            CargarDatosProducto();
        }
        private void AlmacenModificarProducto_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }

            // Cargar las presentaciones (ya lo hemos hecho)
            CargarComboBoxMarca();

            // Cargar las vías de administración
            CargarComboxTipoProducto();

            // Cargar los laboratorios
            CargarComboBoxProveedor();
        }
        private void CargarComboBoxMarca()
        {
            // Crear la instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Crear la consulta para obtener los nombres de las presentaciones
            string query = "SELECT idMarca, nombre FROM Marca";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar cualquier valor previo del ComboBox
                    cmbIdMarca.Items.Clear();

                    // Llenar el ComboBox con los nombres de las presentaciones
                    while (reader.Read())
                    {
                        // Agregar solo el nombre al ComboBox
                        cmbIdMarca.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las Marcas: " + ex.Message);
                }
                finally
                {
                    conexion.GetConexion().Close(); // Cerrar la conexión
                }
            }
        }

        private void CargarComboxTipoProducto()
        {
            // Crear la instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Crear la consulta para obtener los nombres de las vías de administración
            string query = "SELECT idTipoProducto, nombre FROM TipoProducto";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar cualquier valor previo del ComboBox
                    cmbIdTipoProducto.Items.Clear();

                    // Llenar el ComboBox con los nombres de las vías de administración
                    while (reader.Read())
                    {
                        // Agregar solo el nombre al ComboBox
                        cmbIdTipoProducto.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el tipo de producto: " + ex.Message);
                }
            }
        }
        private void CargarComboBoxProveedor()
        {
            // Crear la instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Crear la consulta para obtener los nombres de los laboratorios
            string query = "SELECT idProveedor, nombre FROM Proveedor";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar cualquier valor previo del ComboBox
                    cmbIdProveedor.Items.Clear();

                    // Llenar el ComboBox con los nombres de los laboratorios
                    while (reader.Read())
                    {
                        // Agregar solo el nombre al ComboBox
                        cmbIdProveedor.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los proveedores: " + ex.Message);
                }
                finally
                {
                    conexion.GetConexion().Close(); // Cerrar la conexión
                }
            }
        }
        private void CargarDatosProducto()
        {
            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                // Definir la consulta para obtener los datos del producto
                string query = @"
            SELECT 
                p.nombre AS Nombre,
                p.descripcion AS Descripcion,
                p.precioProveedor AS PrecioProveedor,
                p.precioVenta AS PrecioVenta,
                p.cantidad AS Cantidad,
                p.stock AS Stock,
                p.fechaCaducidad AS FechaCaducidad,
                p.idMarca AS IdMarca,
                p.idTipoProducto AS IdTipoProducto,
                p.idProveedor AS IdProveedor
            FROM Producto p
            WHERE p.nombre = @nombreProducto;"; // Se usa p.nombre correctamente

                // Crear un SqlCommand con la conexión
                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@nombreProducto", nombreProducto);

                SqlDataReader reader = cmd.ExecuteReader();

                // Si se encuentra el producto, mostrar los datos en los TextBox
                if (reader.Read())
                {
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtDescripcion.Text = reader["Descripcion"].ToString();
                    txtPrecioProveedor.Text = reader["PrecioProveedor"].ToString();
                    txtPrecioVenta.Text = reader["PrecioVenta"].ToString();
                    txtCantidad.Text = reader["Cantidad"].ToString();
                    txtStock.Text = reader["Stock"].ToString();
                    fechaVencimientoPicker.Text = reader["FechaCaducidad"].ToString();

                    // Aquí los valores se asignan correctamente como string
                    txtIdMarca.Text = reader["IdMarca"].ToString(); // Debería ser VARCHAR(30), no int
                    txtIdTipoProducto.Text = reader["IdTipoProducto"].ToString(); // Debería ser VARCHAR(30), no int
                    txtIdProveedor.Text = reader["IdProveedor"].ToString(); // Debería ser VARCHAR(30), no int
                }

                reader.Close();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AlmacenModificarProducto_Resize(object sender, EventArgs e)
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

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtNombre.Text == "Nombre de producto") // Si el texto predeterminado está presente
            {
                txtNombre.Text = ""; // Limpia el TextBox
            }
        }

        private void txtCantidad_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtCantidad.Text == "Cantidad de producto") // Si el texto predeterminado está presente
            {
                txtCantidad.Text = ""; // Limpia el TextBox
            }
        }

        private void txtPrecioVenta_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtPrecioVenta.Text == "Precio de venta") // Si el texto predeterminado está presente
            {
                txtPrecioVenta.Text = ""; // Limpia el TextBox
            }
        }

        private void txtProducto_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtPrecioProveedor.Text == "Precio de compra") // Si el texto predeterminado está presente
            {
                txtPrecioProveedor.Text = ""; // Limpia el TextBox
            }
        }

        private void txtProveedor_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtStock.Text == "Proveedor") // Si el texto predeterminado está presente
            {
                txtStock.Text = ""; // Limpia el TextBox
            }
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtDescripcion.Text == "Descripción de producto") // Si el texto predeterminado está presente
            {
                txtDescripcion.Text = ""; // Limpia el TextBox
            }
        }

        private void btnElegir_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Llamar al formulario de opciones
            using (var opcionesForm = new AlmacenAvisoEliminar())
            {
                if (opcionesForm.ShowDialog() == DialogResult.OK)
                {
                    if (opcionesForm.Resultado == "Si")
                    {
                        // Crear la instancia de la clase conexionBrandon
                        conexionBrandon conexion = new conexionBrandon();
                        conexion.AbrirConexion();

                        // Consulta SQL para eliminar el medicamento
                        string query = @"
                        DELETE FROM Producto
                        WHERE nombre = @NombreProducto";

                        using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                        {
                            cmd.Parameters.AddWithValue("@NombreProducto", nombreProducto);

                            try
                            {
                                // Ejecutar la consulta de eliminación
                                int rowsAffected = cmd.ExecuteNonQuery();

                                // Verificar si la eliminación fue exitosa
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("El Producto fue eliminado correctamente.");
                                    // Redirigir al formulario de inventario después de la eliminación
                                    parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm));
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo eliminar el Producto.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al eliminar el Producto: " + ex.Message);
                            }
                            finally
                            {
                                conexion.CerrarConexion(); // Cerrar la conexión
                            }
                        }
                    }
                    else if (opcionesForm.Resultado == "No")
                    {
                        parentForm.formularioHijo(new AlmacenModificarProducto(parentForm, nombreProducto)); // Regresar a la modificación
                    }
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear la instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                // Convertir los valores numéricos de los TextBox a su tipo correspondiente
                decimal precioProveedor = 0;
                decimal precioVenta = 0;
                int stock = 0;

                // Validar y convertir los valores
                if (!decimal.TryParse(txtPrecioProveedor.Text, out precioProveedor))
                {
                    MessageBox.Show("El precio del proveedor no es válido.");
                    return;
                }

                if (!decimal.TryParse(txtPrecioVenta.Text, out precioVenta))
                {
                    MessageBox.Show("El precio de venta no es válido.");
                    return;
                }

                if (!int.TryParse(txtStock.Text, out stock))
                {
                    MessageBox.Show("El stock no es válido.");
                    return;
                }

                // Definir la consulta de actualización
                string query = @"
                UPDATE Producto
                SET 
                    nombre = @Nombre,
                    descripcion = @Descripcion,
                    precioProveedor = @PrecioProveedor,
                    precioVenta = @PrecioVenta,
                    cantidad = @Cantidad,
                    stock = @Stock,
                    fechaCaducidad = @FechaVencimiento,
                    idMarca = @IdMarca,
                    idTipoProducto = @IdTipoProducto,
                    idProveedor = @IdProveedor
                WHERE nombre = @NombreProducto";

                // Crear el comando SQL
                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    // Agregar los parámetros con los valores de los TextBox
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@PrecioProveedor", precioProveedor);
                    cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                    cmd.Parameters.AddWithValue("@Cantidad", txtCantidad.Text); // Se mantiene como VARCHAR
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", fechaVencimientoPicker.Value);
                    cmd.Parameters.AddWithValue("@IdMarca", txtIdMarca.Text);
                    cmd.Parameters.AddWithValue("@IdTipoProducto", txtIdTipoProducto.Text);
                    cmd.Parameters.AddWithValue("@IdProveedor", txtIdProveedor.Text);
                    cmd.Parameters.AddWithValue("@NombreProducto", nombreProducto);

                    // Ejecutar el comando de actualización
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Verificar si la actualización fue exitosa
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Los datos del producto fueron actualizados correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el producto.");
                    }
                }

                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos: " + ex.Message);
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

    }
}
