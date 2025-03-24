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
            conexionBrandon conexion = new conexionBrandon();
            SqlTransaction transaction = null;
            try
            {
                conexion.AbrirConexion();
                transaction = conexion.GetConexion().BeginTransaction();

                // ID del proveedor
                int idProveedor = ObtenerIdProveedorPorNombre(nombreProveedor);

                // Actualizar los datos del proveedor
                string queryProveedor = "UPDATE Proveedor SET nombre = @Nombre, celularPrincipal = @CelularPrincipal, correoElectronico = @Correo, nombreContacto = @NombreContacto, celularContactoPrincipal = @CelularContactoPrincipal WHERE idProveedor = @IdProveedor;";
                SqlCommand cmdProveedor = new SqlCommand(queryProveedor, conexion.GetConexion(), transaction);
                cmdProveedor.Parameters.AddWithValue("@Nombre", string.IsNullOrWhiteSpace(txtNombre.Text) ? (object)DBNull.Value : txtNombre.Text);
                cmdProveedor.Parameters.AddWithValue("@CelularPrincipal", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text);
                cmdProveedor.Parameters.AddWithValue("@Correo", string.IsNullOrWhiteSpace(txtCorreo.Text) ? (object)DBNull.Value : txtCorreo.Text);
                cmdProveedor.Parameters.AddWithValue("@NombreContacto", string.IsNullOrWhiteSpace(txtNombreContacto.Text) ? (object)DBNull.Value : txtNombreContacto.Text);
                cmdProveedor.Parameters.AddWithValue("@CelularContactoPrincipal", string.IsNullOrWhiteSpace(txtTelefonoContacto.Text) ? (object)DBNull.Value : txtTelefonoContacto.Text);
                cmdProveedor.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdProveedor.ExecuteNonQuery();

                // Actualizar el celular extra
                string queryActualizarCelularPrincipal = "UPDATE Celular SET numero = @Numero WHERE idProveedor = @IdProveedor AND idCelular = (SELECT TOP 1 idCelular FROM Celular WHERE idProveedor = @IdProveedor ORDER BY idCelular ASC);";
                SqlCommand cmdActualizarCelularPrincipal = new SqlCommand(queryActualizarCelularPrincipal, conexion.GetConexion(), transaction);
                cmdActualizarCelularPrincipal.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdActualizarCelularPrincipal.Parameters.AddWithValue("@Numero", string.IsNullOrWhiteSpace(txtTelefono.Text) ? (object)DBNull.Value : txtTelefono.Text);
                cmdActualizarCelularPrincipal.ExecuteNonQuery();


                string queryActualizarCelularContacto = @"
                UPDATE CelularContacto 
                SET numero = @Numero 
                WHERE idProveedor = @IdProveedor;";

                SqlCommand cmdActualizarCelularContacto = new SqlCommand(queryActualizarCelularContacto, conexion.GetConexion(), transaction);
                cmdActualizarCelularContacto.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdActualizarCelularContacto.Parameters.AddWithValue("@Numero", string.IsNullOrWhiteSpace(txtTelefonoContacto.Text) ? (object)DBNull.Value : txtTelefono.Text);
                cmdActualizarCelularContacto.ExecuteNonQuery();


                // Actualizar el nombre del país
                string queryActualizarPais = @"
                   UPDATE Pais
                   SET nombre = @NombrePais
                   WHERE IdPais IN (SELECT IdPais FROM Direccion WHERE IdProveedor = @IdProveedor)";
                 SqlCommand cmdActualizarPais = new SqlCommand(queryActualizarPais, conexion.GetConexion(), transaction);
                cmdActualizarPais.Parameters.AddWithValue("@NombrePais", txtPais.Text); // Asume que txtPais es el TextBox con el nombre del país
                cmdActualizarPais.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdActualizarPais.ExecuteNonQuery();

                // Actualizar el nombre del estado
                string queryActualizarEstado = @"
        UPDATE Estado
        SET nombre = @NombreEstado
        WHERE IdEstado IN (SELECT IdEstado FROM Direccion WHERE IdProveedor = @IdProveedor)";
                SqlCommand cmdActualizarEstado = new SqlCommand(queryActualizarEstado, conexion.GetConexion(), transaction);
                cmdActualizarEstado.Parameters.AddWithValue("@NombreEstado", txtEstado.Text); // Asume que txtEstado es el TextBox con el nombre del estado
                cmdActualizarEstado.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdActualizarEstado.ExecuteNonQuery();


                // Actualizar el nombre de la ciudad
                string queryActualizarCiudad = @"
        UPDATE Ciudad
        SET nombre = @NombreCiudad
        WHERE IdCiudad IN (SELECT IdCiudad FROM Direccion WHERE IdProveedor = @IdProveedor)";
                SqlCommand cmdActualizarCiudad = new SqlCommand(queryActualizarCiudad, conexion.GetConexion(), transaction);
                cmdActualizarCiudad.Parameters.AddWithValue("@NombreCiudad", txtCiudad.Text); // Asume que txtCiudad es el TextBox con el nombre de la ciudad
                cmdActualizarCiudad.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdActualizarCiudad.ExecuteNonQuery();

                // Actualizar el código postal
                string queryActualizarCp = @"
        UPDATE Cp
        SET cp = @Cp
        WHERE IdCp IN (SELECT IdCp FROM Direccion WHERE IdProveedor = @IdProveedor)";
                SqlCommand cmdActualizarCp = new SqlCommand(queryActualizarCp, conexion.GetConexion(), transaction);
                cmdActualizarCp.Parameters.AddWithValue("@Cp", txtCp.Text); // Asume que txtCp es el TextBox con el código postal
                cmdActualizarCp.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdActualizarCp.ExecuteNonQuery();

                // Actualizar el nombre de la calle
                string queryActualizarCalle = @"
        UPDATE Calle
        SET nombre = @NombreCalle
        WHERE IdCalle IN (SELECT IdCalle FROM Direccion WHERE IdProveedor = @IdProveedor)";
                SqlCommand cmdActualizarCalle = new SqlCommand(queryActualizarCalle, conexion.GetConexion(), transaction);
                cmdActualizarCalle.Parameters.AddWithValue("@NombreCalle", txtCalle.Text); // Asume que txtCalle es el TextBox con el nombre de la calle
                cmdActualizarCalle.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmdActualizarCalle.ExecuteNonQuery();

                // Confirmar la transacción
                transaction.Commit();
                MessageBox.Show("Proveedor y direcciones actualizados correctamente.");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, revertir la transacción
                transaction?.Rollback();
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
            finally
            {
                if (conexion.GetConexion().State == ConnectionState.Open)
                {
                    conexion.GetConexion().Close();
                }
            }
        }

        private int ObtenerIdProveedorPorNombre(string nombreProveedor)
        {
            conexionBrandon conexion = new conexionBrandon();
            int idProveedor = 0;
            try
            {
                string query = "SELECT idProveedor FROM Proveedor WHERE nombre = @nombreProveedor;";
                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@nombreProveedor", nombreProveedor);

                conexion.AbrirConexion();
                idProveedor = Convert.ToInt32(cmd.ExecuteScalar());
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el id del proveedor: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión
                if (conexion.GetConexion().State == ConnectionState.Open)
                {
                    conexion.GetConexion().Close();
                }
            }
            return idProveedor;
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
                        // Crear la instancia de la clase conexionBrandon
                        conexionBrandon conexion = new conexionBrandon();
                        conexion.AbrirConexion();

                        // Consulta SQL para eliminar el medicamento
                        string query = @"
                        DELETE FROM Celular WHERE idProveedor IN (SELECT idProveedor FROM Proveedor WHERE nombre = @NombreProveedor);
                        DELETE FROM Direccion WHERE idProveedor IN (SELECT idProveedor FROM Proveedor WHERE nombre = @NombreProveedor);
                        DELETE FROM Colonia WHERE idColonia IN (SELECT idColonia FROM Direccion WHERE idProveedor IN (SELECT idProveedor FROM Proveedor WHERE nombre = @NombreProveedor));
                        DELETE FROM Calle WHERE idCalle IN (SELECT idCalle FROM Direccion WHERE idProveedor IN (SELECT idProveedor FROM Proveedor WHERE nombre = @NombreProveedor));
                        DELETE FROM Cp WHERE idCp IN (SELECT idCp FROM Direccion WHERE idProveedor IN (SELECT idProveedor FROM Proveedor WHERE nombre = @NombreProveedor));
                        DELETE FROM Ciudad WHERE idCiudad IN (SELECT idCiudad FROM Direccion WHERE idProveedor IN (SELECT idProveedor FROM Proveedor WHERE nombre = @NombreProveedor));
                        DELETE FROM Estado WHERE idEstado IN (SELECT idEstado FROM Direccion WHERE idProveedor IN (SELECT idProveedor FROM Proveedor WHERE nombre = @NombreProveedor));
                        DELETE FROM Pais WHERE idPais IN (SELECT idPais FROM Direccion WHERE idProveedor IN (SELECT idProveedor FROM Proveedor WHERE nombre = @NombreProveedor));
                        DELETE FROM Proveedor WHERE nombre = @NombreProveedor;";




                        using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                        {
                            cmd.Parameters.AddWithValue("@NombreProveedor", nombreProveedor);

                            try
                            {
                                // Ejecutar la consulta de eliminación
                                int rowsAffected = cmd.ExecuteNonQuery();

                                // Verificar si la eliminación fue exitosa
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("El Proveedor fue eliminado correctamente.");
                                    // Redirigir al formulario de inventario después de la eliminación
                                    parentForm.formularioHijo(new AlmacenProveedor(parentForm));
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo eliminar el Proveedor.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al eliminar el Proveedor: " + ex.Message);
                            }
                            finally
                            {
                                conexion.CerrarConexion(); // Cerrar la conexión
                            }
                        }
                    }
                    else if (opcionesForm.Resultado == "No")
                    {
                        parentForm.formularioHijo(new AlmacenModificarProveedor(parentForm, nombreProveedor)); // Regresar a la modificación
                    }
                }
            }
        }
    }
}
