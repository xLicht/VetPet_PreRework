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
    public partial class AlmacenModificarProveedor : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        private string nombreProveedor;

        public AlmacenModificarProveedor()
        {
            InitializeComponent();
            this.Load += AlmacenModificarProveedor_Load;       // Evento Load
            this.Resize += AlmacenModificarProveedor_Resize;   // Evento Resize
        }

        public AlmacenModificarProveedor(Form1 parent, string nombreProveedor = null)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
            this.nombreProveedor = nombreProveedor;
            CargarDatosProveedor();
        }

        private void AlmacenModificarProveedor_Load(object sender, EventArgs e)
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
        private void CargarDatosProveedor()
        {
            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                // Definir la consulta para obtener los datos del producto
                string query = @"
                SELECT
                p.nombre AS nombre,
                p.celular AS celular,
                p.correoElectronico AS correoElectronico,
                p.nombreContacto AS nombreContacto,
                p.celularContacto AS celularContacto
                FROM Proveedor p
                WHERE p.nombre = @nombreProveedor;"; // Se usa p.nombre correctamente

                // Crear un SqlCommand con la conexión
                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@nombreProveedor", nombreProveedor);

                SqlDataReader reader = cmd.ExecuteReader();

                // Si se encuentra el producto, mostrar los datos en los TextBox
                if (reader.Read())
                {
                    txtNombre.Text = reader["nombre"].ToString();
                    txtTelefono.Text = reader["celular"].ToString();
                    txtCorreo.Text = reader["correoElectronico"].ToString();
                    txtNombreContacto.Text = reader["nombreContacto"].ToString();
                    txtTelefonoContacto.Text = reader["celularContacto"].ToString();                   
                }

                reader.Close();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AlmacenModificarProveedor_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProducto
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProducto
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Llamar al formulario de opciones
            using (var opcionesForm = new AlmacenAvisoEliminar())
            {
                if (opcionesForm.ShowDialog() == DialogResult.OK)
                {
                    if (opcionesForm.Resultado == "Si")
                    {
                        parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a 
                    }
                    else if (opcionesForm.Resultado == "No")
                    {
                        parentForm.formularioHijo(new AlmacenModificarProveedor(parentForm)); // Pasamos la referencia de Form1 a 
                    }
                }
            }
        }
    }
}
