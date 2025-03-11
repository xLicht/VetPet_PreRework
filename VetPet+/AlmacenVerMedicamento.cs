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
    public partial class AlmacenVerMedicamento : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;

        private string nombreMedicamento; // Variable para almacenar el nombre del medicamento

        public AlmacenVerMedicamento(string idProducto)
        {
            this.Load += AlmacenVerMedicamento_Load;       // Evento Load
            this.Resize += AlmacenVerMedicamento_Resize;   // Evento Resize
        }
        public AlmacenVerMedicamento(Form parent, string nombreMedicamento)
        {
            InitializeComponent();
            this.parentForm = (Form1)parent; // Asignar la instancia de Form1
            this.nombreMedicamento = nombreMedicamento;
            CargarDatosMedicamento();
        }

        private void CargarDatosMedicamento()
        {
            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Definir la consulta para obtener los datos del medicamento
                string query = @"
                SELECT 
                    m.nombreGenérico AS Nombre,
                    pr.precioVenta AS PrecioVenta,
                    pr.precioProveedor AS PrecioProveedor,
                    m.dosisRecomendada AS DosisRecomendada,
                    va.nombre AS ViaAdministracion,  -- Obtenemos el nombre de ViaAdministracion desde la tabla 'ViaAdministracion'
                    pro.nombre AS Proveedor,         -- Obtenemos el nombre del proveedor desde la tabla 'Proveedor'
                    ma.nombre AS Marca,              -- Obtenemos el nombre de la marca desde la tabla 'Marca'
                    la.nombre AS Laboratorio,        -- Obtenemos el nombre del laboratorio desde la tabla 'Laboratorio'
                    p.nombre AS Presentacion,
                    pr.descripcion AS Descripcion,
                    pr.fechaRegistro AS fechaRegistro
                FROM Medicamento m
                JOIN Producto pr ON m.idProducto = pr.idProducto
                JOIN Presentacion p ON m.idPresentacion = p.idPresentacion
                JOIN ViaAdministracion va ON m.idViaAdministracion = va.idViaAdministracion  -- Unimos con ViaAdministracion para obtener el nombre
                JOIN Proveedor pro ON pr.idProveedor = pro.idProveedor  -- Unimos con Proveedor para obtener el nombre del proveedor
                JOIN Marca ma ON pr.idMarca = ma.idMarca  -- Unimos con Marca para obtener el nombre de la marca
                JOIN Laboratorio la ON m.idLaboratorio = la.idLaboratorio  -- Unimos con Laboratorio para obtener el nombre del laboratorio
                WHERE m.nombreGenérico = @nombreMedicamento;"; // Usamos el nombre del medicamento

                // Crear un SqlCommand con la conexión
                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@nombreMedicamento", nombreMedicamento);

                SqlDataReader reader = cmd.ExecuteReader();

                // Si se encuentra el medicamento, mostrar los datos en los TextBox
                if (reader.Read())
                {
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtPrecioVenta.Text = reader["PrecioVenta"].ToString();
                    txtPrecioProveedor.Text = reader["PrecioProveedor"].ToString();
                    txtDosis.Text = reader["DosisRecomendada"].ToString();
                    txtViaAdministracion.Text = reader["ViaAdministracion"].ToString();
                    txtProveedor.Text = reader["Proveedor"].ToString();
                    txtMarca.Text = reader["Marca"].ToString();
                    txtLaboratorio.Text = reader["Laboratorio"].ToString();
                    txtPresentacion.Text = reader["Presentacion"].ToString();
                    txtDescripcion.Text = reader["Descripcion"].ToString();
                    txtFecha.Text = reader["fechaRegistro"].ToString();
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
        private void AlmacenVerMedicamento_Load(object sender, EventArgs e)
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

        private void AlmacenVerMedicamento_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm));
        }
    }
}
