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
    public partial class AlmacenAgregarProveedor : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;

        public AlmacenAgregarProveedor()
        {
            InitializeComponent();
            this.Load += AlmacenAgregarProveedor_Load;       // Evento Load
            this.Resize += AlmacenAgregarProveedor_Resize;   // Evento Resize
        }
        public AlmacenAgregarProveedor(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void AlmacenAgregarProveedor_Load(object sender, EventArgs e)
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Verificar que todos los campos están completos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtNombreContacto.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoContacto.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtTelefono.TextLength != 9)
            {
                MessageBox.Show("El campo de numero debe tener 10 digitos");
                return;
            }

            if (txtTelefonoContacto.TextLength != 9)
            {
                MessageBox.Show("El campo de numero debe tener 10 digitos");
                return;
            }

            if (txtCp.TextLength != 5)
            {
                MessageBox.Show("El campo de cp debe tener 5 digitos");
                return;
            }

            // Iniciar transacción para garantizar que ambas inserciones se realicen correctamente
            SqlTransaction transaction = conexion.GetConexion().BeginTransaction();

            try
            {
                // Insertar el proveedor
                string queryProveedor = "INSERT INTO Proveedor (nombre, correoElectronico, nombreContacto) " +
                                       "VALUES (@Nombre, @Correo, @NombreContacto);" +
                                       "SELECT SCOPE_IDENTITY();";

                SqlCommand cmdProveedor = new SqlCommand(queryProveedor, conexion.GetConexion(), transaction);
                cmdProveedor.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmdProveedor.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmdProveedor.Parameters.AddWithValue("@NombreContacto", txtNombreContacto.Text);

                // Obtener el id del proveedor insertado
                int idProveedor = Convert.ToInt32(cmdProveedor.ExecuteScalar());

                // Insertar el número de celular principal
                string queryInsertarCelular = "INSERT INTO Celular (idProveedor, numero) VALUES (@IdProveedor, @Numero);";
                SqlCommand cmdInsertarCelular = new SqlCommand(queryInsertarCelular, conexion.GetConexion(), transaction);
                cmdInsertarCelular.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdInsertarCelular.Parameters.AddWithValue("@Numero", txtTelefono.Text);
                cmdInsertarCelular.ExecuteNonQuery();

                // Insertar el número de celular de contacto
                string queryInsertarCelularContacto = "INSERT INTO CelularContacto (idProveedor, numero) VALUES (@IdProveedor, @Numero);";
                SqlCommand cmdInsertarCelularContacto = new SqlCommand(queryInsertarCelularContacto, conexion.GetConexion(), transaction);
                cmdInsertarCelularContacto.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdInsertarCelularContacto.Parameters.AddWithValue("@Numero", txtTelefonoContacto.Text);
                cmdInsertarCelularContacto.ExecuteNonQuery();

                // Verificar si hay un celular extra y agregarlo
                if (!string.IsNullOrWhiteSpace(txtTelefonoExtra.Text))
                {
                    string queryInsertarCelularExtra = "INSERT INTO Celular (idProveedor, numero) VALUES (@IdProveedor, @Numero);";
                    SqlCommand cmdInsertarCelularExtra = new SqlCommand(queryInsertarCelularExtra, conexion.GetConexion(), transaction);
                    cmdInsertarCelularExtra.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmdInsertarCelularExtra.Parameters.AddWithValue("@Numero", txtTelefonoExtra.Text);
                    cmdInsertarCelularExtra.ExecuteNonQuery();
                }

                // Insertar o obtener los ids de las entidades de la dirección
                int idPais = ObtenerIdDeEntidad(txtPais.Text, "Pais", "Pais", conexion, transaction);
                int idEstado = ObtenerIdDeEntidad(txtEstado.Text, "Estado", "Estado", conexion, transaction);
                int idCiudad = ObtenerIdDeEntidad(txtCiudad.Text, "Ciudad", "Ciudad", conexion, transaction);
                int idColonia = ObtenerIdDeEntidad(txtColonia.Text, "Colonia", "Colonia", conexion, transaction);
                int idCalle = ObtenerIdDeEntidad(txtCalle.Text, "Calle", "Calle", conexion, transaction);
                int idCp = ObtenerIdDeEntidad(txtCp.Text, "Cp", "Cp", conexion, transaction);

                // Insertar la dirección con el idProveedor
                string queryDireccion = "INSERT INTO Direccion (idProveedor, idPais, idEstado, idCiudad, idColonia, idCalle, idCp) " +
                                        "VALUES (@IdProveedor, @IdPais, @IdEstado, @IdCiudad, @IdColonia, @IdCalle, @IdCp)";

                SqlCommand cmdDireccion = new SqlCommand(queryDireccion, conexion.GetConexion(), transaction);
                cmdDireccion.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdDireccion.Parameters.AddWithValue("@IdPais", idPais);
                cmdDireccion.Parameters.AddWithValue("@IdEstado", idEstado);
                cmdDireccion.Parameters.AddWithValue("@IdCiudad", idCiudad);
                cmdDireccion.Parameters.AddWithValue("@IdColonia", idColonia);
                cmdDireccion.Parameters.AddWithValue("@IdCalle", idCalle);
                cmdDireccion.Parameters.AddWithValue("@IdCp", idCp);

                // Ejecutar la inserción de la dirección
                cmdDireccion.ExecuteNonQuery();

                // Commit de la transacción
                transaction.Commit();

                // Confirmación de éxito
                MessageBox.Show("Proveedor, números de celular y dirección insertados correctamente.");

                // Recargar el formulario si es necesario
                if (parentForm != null)
                {
                    parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Recargar formulario
                }
            }
            catch (Exception ex)
            {
                // Si hay algún error, revertir los cambios realizados en la transacción
                transaction.Rollback();
                MessageBox.Show("Error al insertar datos: " + ex.Message);
            }
            finally
            {
                conexion.GetConexion().Close();
            }
        }


        private int ObtenerIdDeEntidad(string nombreEntidad, string tabla, string columna, conexionBrandon conexion, SqlTransaction transaction)
        {
            // Verificar si la tabla tiene la columna "nombre" o si es una tabla diferente (por ejemplo, "Cp")
            string columnaBusqueda = (tabla == "Cp") ? "cp" : "nombre";  // En "Cp" usamos "cp", en las demás usamos "nombre"

            // Crear la consulta dependiendo de si usamos "nombre" o "cp"
            string queryVerificar = $"SELECT Id{columna} FROM {tabla} WHERE {columnaBusqueda} = @NombreEntidad";
            SqlCommand cmdVerificar = new SqlCommand(queryVerificar, conexion.GetConexion(), transaction);
            cmdVerificar.Parameters.AddWithValue("@NombreEntidad", nombreEntidad);

            // Intentar obtener el Id de la entidad
            object resultado = cmdVerificar.ExecuteScalar();

            if (resultado != null) // Si existe, devolver el ID
            {
                return Convert.ToInt32(resultado);
            }
            else // Si no existe, insertar y devolver el ID
            {
                string queryInsertar = $"INSERT INTO {tabla} ({columnaBusqueda}) VALUES (@NombreEntidad);" +
                                       $"SELECT SCOPE_IDENTITY();";
                SqlCommand cmdInsertar = new SqlCommand(queryInsertar, conexion.GetConexion(), transaction);
                cmdInsertar.Parameters.AddWithValue("@NombreEntidad", nombreEntidad);

                return Convert.ToInt32(cmdInsertar.ExecuteScalar());
            }
        }




        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProducto
        }

        private void AlmacenAgregarProveedor_Resize(object sender, EventArgs e)
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y la tecla de retroceso (Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloquea la entrada del carácter
            }
        }

        private void txtTelefonoExtra_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y la tecla de retroceso (Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloquea la entrada del carácter
            }
        }

        private void txtTelefonoContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y la tecla de retroceso (Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloquea la entrada del carácter
            }
        }

        private void txtCp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y la tecla de retroceso (Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloquea la entrada del carácter
            }
        }

        private void txtNombre_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtNombre.Text == "Nombre de proveedor") // Si el texto predeterminado está presente
            {
                txtNombre.Text = ""; // Limpia el TextBox
            }
        }

        private void txtTelefono_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtTelefono.Text == "000000000") // Si el texto predeterminado está presente
            {
                txtTelefono.Text = ""; // Limpia el TextBox
            }
        }

        private void txtTelefonoExtra_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtTelefonoExtra.Text == "000000000") // Si el texto predeterminado está presente
            {
                txtTelefonoExtra.Text = ""; // Limpia el TextBox
            }
        }

        private void txtCorreo_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtCorreo.Text == "nombre@gmail.com") // Si el texto predeterminado está presente
            {
                txtCorreo.Text = ""; // Limpia el TextBox
            }
        }

        private void txtNombreContacto_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtNombreContacto.Text == "Nombre de contacto") // Si el texto predeterminado está presente
            {
                txtNombreContacto.Text = ""; // Limpia el TextBox
            }
        }

        private void txtTelefonoContacto_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtTelefonoContacto.Text == "000000000") // Si el texto predeterminado está presente
            {
                txtTelefonoContacto.Text = ""; // Limpia el TextBox
            }
        }

        private void txtEstado_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtEstado.Text == "Estado") // Si el texto predeterminado está presente
            {
                txtEstado.Text = ""; // Limpia el TextBox
            }
        }

        private void txtCiudad_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtCiudad.Text == "Ciudad") // Si el texto predeterminado está presente
            {
                txtCiudad.Text = ""; // Limpia el TextBox
            }
        }

        private void txtColonia_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtColonia.Text == "Colonia") // Si el texto predeterminado está presente
            {
                txtColonia.Text = ""; // Limpia el TextBox
            }
        }

        private void txtCalle_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtCalle.Text == "Calle") // Si el texto predeterminado está presente
            {
                txtCalle.Text = ""; // Limpia el TextBox
            }
        }

        private void txtCp_MouseEnter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtCp.Text == "Codigo postal") // Si el texto predeterminado está presente
            {
                txtCp.Text = ""; // Limpia el TextBox
            }
        }

        private void txtPais_MouseEnter(object sender, EventArgs e)
        {
            if (txtPais.Text == "Pais") // Si el texto predeterminado está presente
            {
                txtPais.Text = ""; // Limpia el TextBox
            }
        }
    }
}
