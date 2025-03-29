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
    public partial class AlmacenVerProveedor : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;
        private string nombreProveedor;

        public AlmacenVerProveedor()
        {
            this.Load += AlmacenVerProveedor_Load;       // Evento Load
            this.Resize += AlmacenVerProveedor_Resize;   // Evento Resize
        }
        public AlmacenVerProveedor(Form1 parent, string nombreProveedor)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
            this.nombreProveedor = nombreProveedor;
            CargarDatosProveedor();
        }

        private void CargarDatosProveedor()
        {
            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                // Definir la consulta para obtener los datos del producto
                string query = @"
                               WITH CelularesOrdenados AS (
                    SELECT 
                        c.idProveedor, 
                        c.numero, 
                        ROW_NUMBER() OVER (PARTITION BY c.idProveedor ORDER BY c.numero) AS NumeroOrden
                    FROM Celular c
                )
                SELECT 
                    p.nombre AS NombreProveedor,                  
                    p.celularPrincipal AS celularPrincipal,
                    p.celularContactoPrincipal AS celularContactoPrincipal,
                    ce.numero AS CelularProveedorExtra,
                    p.correoElectronico AS CorreoElectronico,
                    p.nombreContacto AS NombreContacto,
                    ca.nombre AS Calle,
                    co.nombre AS Colonia,
                    cp.cp AS CodigoPostal,
                    ci.nombre AS Ciudad,
                    e.nombre AS Estado
                FROM Proveedor p
                INNER JOIN Celular ce ON p.idProveedor = ce.idProveedor
                LEFT JOIN Direccion d ON p.idProveedor = d.idProveedor
                LEFT JOIN Calle ca ON d.idCalle = ca.idCalle
                LEFT JOIN Colonia co ON d.idColonia = co.idColonia
                LEFT JOIN Cp cp ON d.idCp = cp.idCp
                LEFT JOIN Ciudad ci ON d.idCiudad = ci.idCiudad
                LEFT JOIN Estado e ON d.idEstado = e.idEstado
                WHERE p.nombre = @nombreProveedor;"; // Se usa p.nombre correctamente

                // Crear un SqlCommand con la conexión
                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@nombreProveedor", nombreProveedor);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtNombre.Text = reader["NombreProveedor"].ToString();
                    txtTelefono.Text = reader["celularPrincipal"].ToString(); // Primer número
                    txtTelefonoExtra.Text = reader["CelularProveedorExtra"].ToString(); // Segundo número
                    txtCorreo.Text = reader["CorreoElectronico"].ToString();
                    txtNombreContacto.Text = reader["NombreContacto"].ToString();
                    txtTelefonoContacto.Text = reader["celularContactoPrincipal"].ToString();
                    txtCalle.Text = reader["Calle"].ToString();
                    txtColonia.Text = reader["Colonia"].ToString();
                    txtCp.Text = reader["CodigoPostal"].ToString();
                    txtCiudad.Text = reader["Ciudad"].ToString();
                    txtEstado.Text = reader["Estado"].ToString();
                }
                reader.Close();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void AlmacenVerProveedor_Load(object sender, EventArgs e)
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

        private void AlmacenVerProveedor_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }
    }
}
