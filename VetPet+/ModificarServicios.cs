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
    public partial class ModificarServicios : Form
    {
        int Identificador;
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        public ModificarServicios()
        {
            
            InitializeComponent();
            this.Load += ModificarServicios_Load;       // Evento Load
            this.Resize += ModificarServicios_Resize;
        }

        public ModificarServicios(Form1 parent, int Id)
        {
            Identificador = Id;
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }


        private void ModificarServicios_Load(object sender, EventArgs e)
            {
            CargarDatos();
            cargarCombobox();
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void ModificarServicios_Resize(object sender, EventArgs e)
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
        private void cargarCombobox()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT nombre FROM TipoEmpleado";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Crear un SqlDataAdapter con la conexión correcta
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar el DataTable como fuente de datos
                    comboBox1.DataSource = dt;

                    // Asegúrate de que DisplayMember coincida con el nombre exacto de la columna en tu DataTable
                    comboBox1.DisplayMember = "nombre";  // Nombre de la columna que quieres mostrar en el ComboBox
                    comboBox1.ValueMember = "nombre";    // El valor del ComboBox será el mismo campo
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
        private void BtnTiposDeServicios_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaPLab(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos

        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            int idTipoServicio;

            DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;

            string nombreServicio = selectedRow["nombre"].ToString();

            string queryIdServicio = "SELECT idTipoEmpleado FROM TipoEmpleado WHERE nombre = @NombreServicio";

            // Crear el comando para obtener el idServicioEspecificoHijo
            using (SqlCommand cmd = new SqlCommand(queryIdServicio, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@NombreServicio", nombreServicio);

                    // Ejecutar la consulta y obtener el id
                    object result = cmd.ExecuteScalar(); // ExecuteScalar retorna el primer valor de la consulta (idServicioEspecificoHijo)

                    if (result != null)
                    {
                        // Convertir el resultado a int (si el id es entero)
                        int idTipoEmpleado = Convert.ToInt32(result);
                        string query = "UPDATE ServicioPadre SET nombre = @NOM, descripcion = @DES, idClaseServicio =@ICS, idTipoEmpleado = @ITE WHERE idServicioPadre = @ID";

                        using (SqlCommand insertCmd = new SqlCommand(query, conexion.GetConexion()))
                        {
                            try
                            {
                                // Obtener valores de los controles
                                string nombre = TxtNombre.Text;
                                string descripcion = richTextBox1.Text.Replace("\r", "").Replace("\n", "");

                                if (RbMédico.Checked)
                                {
                                    idTipoServicio = 1; // Si el primer radio button está seleccionado
                                }
                                else if (RbEstetico.Checked)
                                {
                                    idTipoServicio = 2; // Si el segundo radio button está seleccionado
                                }
                                else
                                {
                                    idTipoServicio = 0; // Ningún radio button seleccionado
                                    MessageBox.Show("Por favor, seleccione una opción.");
                                }



                                // Agregar parámetros a la consulta
                                insertCmd.Parameters.AddWithValue("@NOM", nombre);
                                insertCmd.Parameters.AddWithValue("@DES", descripcion);
                                insertCmd.Parameters.AddWithValue("@ICS", idTipoServicio);
                                insertCmd.Parameters.AddWithValue("@ITE", idTipoEmpleado);
                                insertCmd.Parameters.AddWithValue("@ID", Identificador);

                                // Ejecutar la consulta de actualización
                                int filasAfectadas = insertCmd.ExecuteNonQuery();
                                if (filasAfectadas > 0)
                                {
                                    MessageBox.Show("Servicio actualizado correctamente.");
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró un servicio con ese ID.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al actualizar el servicio: " + ex.Message);
                            }
                            finally
                            {
                                conexion.CerrarConexion();
                            }
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
        private void CargarDatos()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            int idClaseServicio = 0;

            // Consulta para obtener los datos del servicio
            string query = "SELECT nombre, descripcion, idClaseServicio FROM ServicioPadre WHERE idServicioPadre = @ID";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@ID", Identificador);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Cargar los datos en los controles del formulario
                        TxtNombre.Text = reader["nombre"].ToString();
                        richTextBox1.Text = reader["descripcion"].ToString();                       
                        idClaseServicio = Convert.ToInt32(reader["idClaseServicio"]);
                        if(idClaseServicio == 2)
                            RbEstetico.Checked = true;
                        else
                            RbMédico.Checked = true;

                    }
                    else
                    {
                        MessageBox.Show("No se encontró un servicio con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
        }
}
