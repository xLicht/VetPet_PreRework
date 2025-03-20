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
using VetPet_;

namespace VetPet_
{
    public partial class AgregarRadiografias : FormPadre
    {
        public AgregarRadiografias()
        {
            InitializeComponent();
        }
        public AgregarRadiografias(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;

            string nombreServicio = selectedRow["nombre"].ToString();

            string queryIdServicio = "SELECT idServicioEspecificoHijo FROM ServicioEspecificoHijo WHERE nombre = @NombreServicio";

            // Crear el comando para obtener el idServicioEspecificoHijo
            using (SqlCommand cmd = new SqlCommand(queryIdServicio, conexion.GetConexion()))
            {
                try
                {
                    // Agregar el parámetro del nombre del ServicioEspecificoHijo
                    cmd.Parameters.AddWithValue("@NombreServicio", nombreServicio);

                    // Ejecutar la consulta y obtener el id
                    object result = cmd.ExecuteScalar(); // ExecuteScalar retorna el primer valor de la consulta (idServicioEspecificoHijo)

                    if (result != null)
                    {
                        // Convertir el resultado a int (si el id es entero)
                        int idServicioEspecificoHijo = Convert.ToInt32(result);

                        // Ahora podemos proceder con la inserción de los otros datos en ServicioEspecificoNieto
                        string queryInsert = "INSERT INTO ServicioEspecificoNieto (nombre, descripcion, precio, duracion, idServicioEspecificoHijo) " +
                            "VALUES (@NOM, @DES, @PRE, @DUR, @ISH);";

                        using (SqlCommand insertCmd = new SqlCommand(queryInsert, conexion.GetConexion()))
                        {
                            // Obtener los valores de los controles
                            string Nombre = TxtNombre.Text;
                            string Descripcion = richTextBox1.Text.Replace("\r", "").Replace("\n", "");

                            if (!decimal.TryParse(TxtPrecio.Text, out decimal precio))
                            {
                                MessageBox.Show("No se ingresó correctamente el campo precio.");
                                return; // Salir si el precio no es válido
                            }

                            // Convertir la duración
                            if (!int.TryParse(TxtDuracion.Text, out int duracion))
                            {
                                MessageBox.Show("No se ingresó correctamente la duración.");
                                return; // Salir si la duración no es válida
                            }

                            // Agregar los parámetros para la inserción
                            insertCmd.Parameters.AddWithValue("@NOM", Nombre);
                            insertCmd.Parameters.AddWithValue("@DES", Descripcion);
                            insertCmd.Parameters.AddWithValue("@PRE", precio);
                            insertCmd.Parameters.AddWithValue("@DUR", duracion);
                            insertCmd.Parameters.AddWithValue("@ISH", idServicioEspecificoHijo); // Usar el id obtenido

                            // Ejecutar la consulta de inserción
                            insertCmd.ExecuteNonQuery();  // Usar ExecuteNonQuery para la inserción

                            MessageBox.Show("Nuevo Tipo de Servicio Registrado");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el Servicio Especificado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaRadiografias(parentForm));
        }

        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal, y tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;  // Si es un carácter no permitido, no se procesa
            }

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && TxtPrecio.Text.Contains("."))
            {
                e.Handled = true;  // Si ya hay un punto decimal, no permite otro
            }
        }

        private void AgregarRadiografias_Load(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            conexion.cargarCombobox(comboBox1, "7");
        }
    }
}
