using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class AlmacenVerProducto : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;

        private string nombreProducto; // Variable para almacenar el nombre del producto

        public AlmacenVerProducto(string idProducto)
        {
            this.Load += AlmacenVerProducto_Load;       // Evento Load
            this.Resize += AlmacenVerProducto_Resize;   // Evento Resize
        }

        public AlmacenVerProducto(Form parent, string nombreProducto)
        {
            InitializeComponent();
            this.parentForm = (Form1)parent; // Asignar la instancia de Form1
            this.nombreProducto = nombreProducto;
            CargarDatosProducto();
        }

        private void CargarDatosProducto()
        {
            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Definir la consulta para obtener los datos del producto
                string query = @"
                SELECT 
                    pr.nombre AS Nombre,
                    pr.precioVenta AS PrecioVenta,
                    pr.precioProveedor AS PrecioProveedor,
                    pr.cantidad AS Cantidad,
                    pr.stock AS Stock,
                    ma.nombre AS Marca,
                    tp.nombre AS TipoProducto,
                    pro.nombre AS Proveedor,
                    pr.descripcion AS Descripcion,
                    ma.idMarca AS idMarca,
                    tp.idTipoProducto AS idTipoProducto,
                    pro.idProveedor AS idProveedor,
                    pr.fechaRegistro AS fechaRegistro,
                    pr.estado AS estado
                FROM Producto pr
                JOIN Marca ma ON pr.idMarca = ma.idMarca
                JOIN TipoProducto tp ON pr.idTipoProducto = tp.idTipoProducto
                JOIN Proveedor pro ON pr.idProveedor = pro.idProveedor
                WHERE pr.nombre = @nombreProducto;"; // Usamos el nombre del producto

                // Crear un SqlCommand con la conexión
                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@nombreProducto", nombreProducto);

                SqlDataReader reader = cmd.ExecuteReader();

                // Si se encuentra el producto, mostrar los datos en los TextBox
                if (reader.Read())
                {
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtPrecioVenta.Text = reader["PrecioVenta"].ToString();
                    txtPrecioProveedor.Text = reader["PrecioProveedor"].ToString();
                    txtCantidad.Text = reader["Cantidad"].ToString();
                    txtStock.Text = reader["Stock"].ToString();
                    txtDescripcion.Text = reader["Descripcion"].ToString();
                    cmbEstado.Text = reader["estado"].ToString();
                    txtFechaRegistro.Text = reader["fechaRegistro"].ToString();
                    txtIdMarca.Text = reader["idMarca"].ToString();
                    txtIdProveedor.Text = reader["idTipoProducto"].ToString();
                    txtIdTipoProducto.Text = reader["idProveedor"].ToString();
                }

                reader.Close();

                // Cerrar la conexión
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AlmacenVerProducto_Load(object sender, EventArgs e)
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

        private void AlmacenVerProducto_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm));
        }
    }
}
