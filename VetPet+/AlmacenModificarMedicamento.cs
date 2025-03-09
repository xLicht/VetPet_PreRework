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
    public partial class AlmacenModificarMedicamento : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;
        private string nombreMedicamento;
        public AlmacenModificarMedicamento()
        {
            this.Load += AlmacenModificarMedicamento_Load;       // Evento Load
            this.Resize += AlmacenModificarMedicamento_Resize;   // Evento Resize
        }
        public AlmacenModificarMedicamento(Form parent, string nombreMedicamento = null)
        {
            InitializeComponent();
            this.parentForm = (Form1)parent;
            this.nombreMedicamento = nombreMedicamento;
            CargarDatosMedicamento();
        }

        private void AlmacenModificarMedicamento_Load(object sender, EventArgs e)
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
            // Crear la instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();
            conexion.AbrirConexion();

            // Crear la consulta para obtener los nombres de las presentaciones
            string query = "SELECT idPresentacion, nombre FROM Presentacion";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
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
                    conexion.GetConexion().Close(); // Cerrar la conexión
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
            string query = "SELECT idProducto, nombre FROM Producto";

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

        private void AlmacenModificarMedicamento_Resize(object sender, EventArgs e)
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
        private void CargarDatosMedicamento()
        {
            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                // Definir la consulta para obtener los datos del medicamento
                string query = @"
                SELECT 
                    m.nombreGenérico AS Nombre,
                    m.dosisRecomendada AS Dosis,
                    m.intervalo AS Intervalo,
                    m.idPresentacion AS IdPresentacion,
                    m.idLaboratorio AS IdLaboratorio,
                    m.idViaAdministracion AS IdViaAdministracion,
                    pr.idProducto AS IdProducto
                FROM Medicamento m
                JOIN Producto pr ON m.idProducto = pr.idProducto
                WHERE m.nombreGenérico = @nombreMedicamento;";

                // Crear un SqlCommand con la conexión
                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@nombreMedicamento", nombreMedicamento);

                SqlDataReader reader = cmd.ExecuteReader();

                // Si se encuentra el medicamento, mostrar los datos en los TextBox
                if (reader.Read())
                {
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtDosis.Text = reader["Dosis"].ToString();
                    txtIntervalo.Text = reader["Intervalo"].ToString();
                    txtIdPresentacion.Text = reader["IdPresentacion"].ToString();
                    txtIdLaboratorio.Text = reader["IdLaboratorio"].ToString();
                    txtIdViaAdministracion.Text = reader["IdViaAdministracion"].ToString();
                    txtIdProducto.Text = reader["IdProducto"].ToString();
                }

                reader.Close();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnElegir_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear la instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                // Definir la consulta de actualización
                string query = @"
            UPDATE Medicamento
            SET 
                nombreGenérico = @Nombre,
                dosisRecomendada = @Dosis,
                intervalo = @Intervalo,
                idPresentacion = @IdPresentacion,
                idLaboratorio = @IdLaboratorio,
                idViaAdministracion = @IdViaAdministracion,
                idProducto = @IdProducto
            WHERE nombreGenérico = @NombreMedicamento";

                // Crear el comando SQL
                using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                {
                    // Agregar los parámetros con los valores de los TextBox
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Dosis", txtDosis.Text);
                    cmd.Parameters.AddWithValue("@Intervalo", txtIntervalo.Text);
                    cmd.Parameters.AddWithValue("@IdPresentacion", txtIdPresentacion.Text);
                    cmd.Parameters.AddWithValue("@IdLaboratorio", txtIdLaboratorio.Text);
                    cmd.Parameters.AddWithValue("@IdViaAdministracion", txtIdViaAdministracion.Text);
                    cmd.Parameters.AddWithValue("@IdProducto", txtIdProducto.Text);
                    cmd.Parameters.AddWithValue("@NombreMedicamento", nombreMedicamento);

                    // Ejecutar el comando de actualización
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Verificar si la actualización fue exitosa
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Los datos del medicamento fueron actualizados correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el medicamento.");
                    }
                }

                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos: " + ex.Message);
            }
        }


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm)); // Pasamos la referencia de Form1 a 
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
                    DELETE FROM Medicamento 
                    WHERE nombreGenérico = @NombreMedicamento";

                        using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
                        {
                            cmd.Parameters.AddWithValue("@NombreMedicamento", nombreMedicamento);

                            try
                            {
                                // Ejecutar la consulta de eliminación
                                int rowsAffected = cmd.ExecuteNonQuery();

                                // Verificar si la eliminación fue exitosa
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("El medicamento fue eliminado correctamente.");
                                    // Redirigir al formulario de inventario después de la eliminación
                                    parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm));
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo eliminar el medicamento.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al eliminar el medicamento: " + ex.Message);
                            }
                            finally
                            {
                                conexion.CerrarConexion(); // Cerrar la conexión
                            }
                        }
                    }
                    else if (opcionesForm.Resultado == "No")
                    {
                        parentForm.formularioHijo(new AlmacenModificarMedicamento(parentForm, nombreMedicamento)); // Regresar a la modificación
                    }
                }
            }
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

        private void txtDosis_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtDosis.Text == "Dosis Recomendada") // Si el texto predeterminado está presente
            {
                txtDosis.Text = ""; // Limpia el TextBox
            }
        }

        private void cmbPresentacion_Enter(object sender, EventArgs e)
        {

        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtNombre.Text == "Nombre de medicamento") // Si el texto predeterminado está presente
            {
                txtNombre.Text = ""; // Limpia el TextBox
            }
        }
    }
}
