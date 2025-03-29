using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class AlmacenAgregarMedicamento : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        public string ProveedorSeleccionado { get; set; }
        private AlmacenAgregarMedicamento formMedicamento;


        public AlmacenAgregarMedicamento()
        {
            InitializeComponent();
            this.Load += AlmacenAgregarMedicamento_Load;
            this.Resize += AlmacenAgregarMedicamento_Resize;
        }

        public AlmacenAgregarMedicamento(Form1 parent, string proveedor = null)
        {
            InitializeComponent();
            parentForm = parent;
            ProveedorSeleccionado = proveedor;
        }

        private void AlmacenAgregarMedicamento_Load(object sender, EventArgs e)
        {
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }

            // Cargar las presentaciones (ya lo hemos hecho)
            CargarComboBoxPresentacion();

            // Cargar las vías de administración
            CargarComboBoxViaAdministracion();

            // Cargar los laboratorios
            CargarComboBoxLaboratorio();

            // Cargar los productos
            CargarComboBoxProducto();
        }
        private void CargarComboBoxPresentacion()
        {
            // Crear la instancia de ConexionMaestra
            ConexionMaestra conexionMaestra = new ConexionMaestra();
            SqlConnection conexion = conexionMaestra.CrearConexion();

            // Crear la consulta para obtener los nombres de las presentaciones
            string query = "SELECT idPresentacion, nombre FROM Presentacion";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar cualquier valor previo del ComboBox
                    cmbPresentacion.Items.Clear();

                    // Llenar el ComboBox con los nombres de las presentaciones
                    while (reader.Read())
                    {
                        // Agregar solo el nombre al ComboBox
                        cmbPresentacion.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.Close(); // Cerrar la conexión
                }
            }
        }

        private void CargarComboBoxViaAdministracion()
        {
            // Crear la instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Crear la consulta para obtener los nombres de las vías de administración
            string query = "SELECT idViaAdministracion, nombre FROM ViaAdministracion";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar cualquier valor previo del ComboBox
                    cmbViaAdministracion.Items.Clear();

                    // Llenar el ComboBox con los nombres de las vías de administración
                    while (reader.Read())
                    {
                        // Agregar solo el nombre al ComboBox
                        cmbViaAdministracion.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las vías de administración: " + ex.Message);
                }
            }
        }
        private void CargarComboBoxLaboratorio()
        {
            // Crear la instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Crear la consulta para obtener los nombres de los laboratorios
            string query = "SELECT idLaboratorio, nombre FROM Laboratorio";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar cualquier valor previo del ComboBox
                    cmbLaboratorio.Items.Clear();

                    // Llenar el ComboBox con los nombres de los laboratorios
                    while (reader.Read())
                    {
                        // Agregar solo el nombre al ComboBox
                        cmbLaboratorio.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los laboratorios: " + ex.Message);
                }
                finally
                {
                    conexion.GetConexion().Close(); // Cerrar la conexión
                }
            }
        }

        private void CargarComboBoxProducto()
        {
            // Crear la instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Crear la consulta para obtener los nombres de los productos
            string query = "SELECT idProducto, nombre FROM Producto WHERE idTipoProducto = '3'";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar cualquier valor previo del ComboBox
                    cmbProducto.Items.Clear();

                    // Llenar el ComboBox con los nombres de los productos
                    while (reader.Read())
                    {
                        // Agregar solo el nombre al ComboBox
                        cmbProducto.Items.Add(reader["nombre"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los productos: " + ex.Message);
                }
                finally
                {
                    conexion.GetConexion().Close(); // Cerrar la conexión
                }
            }
        }


        private void AlmacenAgregarMedicamento_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Crear una instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();

            // Abrir la conexión usando el método de la clase conexionBrandon
            conexion.AbrirConexion();

            // Verificar que todos los campos están completos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDosis.Text) ||
                string.IsNullOrWhiteSpace(txtIntervalo.Text) ||
                string.IsNullOrWhiteSpace(txtIdPresentacion.Text) ||
                string.IsNullOrWhiteSpace(txtIdLaboratorio.Text) ||
                string.IsNullOrWhiteSpace(txtIdViaAdministracion.Text) ||
                string.IsNullOrWhiteSpace(txtIdProducto.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Intentar convertir los campos de texto a enteros
            if (!int.TryParse(txtIdPresentacion.Text, out int IdPresentacion) ||
                !int.TryParse(txtIdLaboratorio.Text, out int IdLaboratorio) ||
                !int.TryParse(txtIdViaAdministracion.Text, out int IdViaAdministracion) ||
                !int.TryParse(txtIdProducto.Text, out int IdProducto))
            {
                MessageBox.Show("Los campos de ID deben ser valores numéricos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO Medicamento (nombreGenérico, dosisRecomendada, intervalo, idPresentacion, idLaboratorio, idViaAdministracion, idProducto) " +
                           "VALUES (@NombreGen, @Dosis, @Intervalo, @IdPres, @IdLab, @IdVia, @IdProd)";

            // Usa la conexión ya abierta de conexionBrandon
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Recuperamos los valores de los TextBox
                    string NombreGenerico = txtNombre.Text;
                    string DosisRecomendada = txtDosis.Text;
                    string Intervalo = txtIntervalo.Text;

                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@NombreGen", NombreGenerico);
                    cmd.Parameters.AddWithValue("@Dosis", DosisRecomendada);
                    cmd.Parameters.AddWithValue("@Intervalo", Intervalo);
                    cmd.Parameters.AddWithValue("@IdPres", IdPresentacion);
                    cmd.Parameters.AddWithValue("@IdLab", IdLaboratorio);
                    cmd.Parameters.AddWithValue("@IdVia", IdViaAdministracion);
                    cmd.Parameters.AddWithValue("@IdProd", IdProducto);

                    // Ejecutar la consulta
                    cmd.ExecuteNonQuery();
                    conexion.GetConexion().Close();

                    // Mensaje de éxito
                    MessageBox.Show("Datos insertados correctamente en 'Medicamento'.");
                }
                catch (System.Exception ex)
                {
                    // Manejo de errores
                    MessageBox.Show("Error al insertar datos: " + ex.Message);
                }
                finally
                {
                    // Cerrar la conexión en el bloque finally por seguridad
                    conexion.GetConexion().Close();
                }
            }
        }

        private void txtIdPresentacion_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbPresentacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado un ítem
            if (cmbPresentacion.SelectedIndex != -1)
            {
                // Obtener el nombre de la presentación seleccionada
                string nombrePresentacion = cmbPresentacion.SelectedItem.ToString();

                // Crear la consulta para obtener el idPresentacion correspondiente
                string query = "SELECT idPresentacion FROM Presentacion WHERE nombre = @Nombre";

                // Crear la instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombrePresentacion);

                    try
                    {
                        // Ejecutar la consulta y obtener el idPresentacion
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Mostrar el id en el TextBox correspondiente
                            txtIdPresentacion.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el id para la presentación seleccionada.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener el id de la presentación: " + ex.Message);
                    }
                    finally
                    {
                        conexion.GetConexion().Close(); // Cerrar la conexión
                    }
                }
            }
        }

        private void cmbViaAdministracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbViaAdministracion.SelectedIndex != -1)
            {
                string nombreVia = cmbViaAdministracion.SelectedItem.ToString();

                string query = "SELECT idViaAdministracion FROM ViaAdministracion WHERE nombre = @Nombre";

                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreVia);

                    try
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            txtIdViaAdministracion.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el id para la vía de administración seleccionada.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener el id de la vía de administración: " + ex.Message);
                    }
                    finally
                    {
                        conexion.GetConexion().Close();
                    }
                }
            }
        }

        private void cmbLaboratorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLaboratorio.SelectedIndex != -1)
            {
                string nombreLaboratorio = cmbLaboratorio.SelectedItem.ToString();

                string query = "SELECT idLaboratorio FROM Laboratorio WHERE nombre = @Nombre";

                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreLaboratorio);

                    try
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            txtIdLaboratorio.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el id para el laboratorio seleccionado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener el id del laboratorio: " + ex.Message);
                    }
                    finally
                    {
                        conexion.GetConexion().Close();
                    }
                }
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex != -1)
            {
                string nombreProducto = cmbProducto.SelectedItem.ToString();

                string query = "SELECT idProducto FROM Producto WHERE nombre = @Nombre";

                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreProducto);

                    try
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            txtIdProducto.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el id para el producto seleccionado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener el id del producto: " + ex.Message);
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
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtNombre.Text == "Nombre de medicamento") // Si el texto predeterminado está presente
            {
                txtNombre.Text = ""; // Limpia el TextBox
            }
        }

        private void txtDosis_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtDosis.Text == "Dosis recomendada") // Si el texto predeterminado está presente
            {
                txtDosis.Text = ""; // Limpia el TextBox
            }
        }

        private void txtIntervalo_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtIntervalo.Text == "Intervalo") // Si el texto predeterminado está presente
            {
                txtIntervalo.Text = ""; // Limpia el TextBox
            }
        }
    }
}
