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
    public partial class AlmacenInventarioMedicamentos : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;

        //CONEXION
        private void CargarDatos()
        {
            try
            {

                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Definir la consulta
                string query = @"SELECT 
                    p.nombre AS Presentacion,
                    m.nombreGenérico AS Nombre,
                    dp.cantidad AS Inventario,
                    dp.precioVenta AS Precio,
                    pro.nombre
                    FROM Medicamento m
                    JOIN presentacion p ON m.idpresentacion = p.idpresentacion
                    JOIN producto pr ON m.idproducto = pr.idproducto
                    FULL JOIN detalles_pedido dp ON pr.idproducto = dp.idproducto
                    JOIN Proveedor pro ON pr.idProveedor = pro.idProveedor";

                // Crear un SqlDataAdapter usando la conexión obtenida de la clase conexionBrandon
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());
                DataTable dt = new DataTable();

                // Llenar el DataTable con los resultados de la consulta
                da.Fill(dt);

                // Asignar el DataTable al DataGridView
                dataGridView1.DataSource = dt;

                // Asegurarse de que las columnas se generen correctamente
                dataGridView1.AutoGenerateColumns = true; // Esta propiedad debería estar en true por defecto


                // Cerrar la conexión
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        public AlmacenInventarioMedicamentos()
        {
            InitializeComponent();
            this.Load += AlmacenInventarioMedicamentos_Load;       // Evento Load
            this.Resize += AlmacenInventarioMedicamentos_Resize;   // Evento Resize
        }
        public AlmacenInventarioMedicamentos(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
            comboBox1.FlatStyle = FlatStyle.Flat;  // Quita bordes
            comboBox1.DropDownWidth = 150;         // Ancho del desplegable
            CargarDatos();
        }

        private void AlmacenInventarioMedicamentos_Load(object sender, EventArgs e)
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

        private void AlmacenInventarioMedicamentos_Resize(object sender, EventArgs e)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenAgregarMedicamento(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenMenu(parentForm)); // Pasamos la referencia de Form1
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString(); // Obtiene el nombre del medicamento

                // Llamar al formulario de opciones
                using (var opcionesForm = new AlmacenAvisoVerOModificar(nombre, parentForm))
                {
                    if (opcionesForm.ShowDialog() == DialogResult.OK)
                    {
                        if (opcionesForm.Resultado == "Modificar")
                        {
                            parentForm.formularioHijo(new AlmacenModificarMedicamento(parentForm, nombre)); // Pasamos la referencia de Form1 a 
                        }
                        else if (opcionesForm.Resultado == "Salir")
                        {
                            parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm)); // Pasamos la referencia de Form1 a 
                        }
                        else if (opcionesForm.Resultado == "Ver")
                        {
                            // Llamar al formulario AlmacenVerMedicamento y pasarle el nombre del medicamento seleccionado
                            parentForm.formularioHijo(new AlmacenVerMedicamento(parentForm, nombre)); // Pasamos el nombre del medicamento a AlmacenVerMedicamento
                        }
                    }
                }
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el texto del TextBox (nombre del medicamento a buscar)
            string nombreMedicamento = txtProducto.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrEmpty(nombreMedicamento))
            {
                MessageBox.Show("Por favor, ingrese un nombre de medicamento para buscar.");
                return;
            }

            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Definir la consulta con un filtro de búsqueda
                string query = @"SELECT 
                            p.nombre AS Presentacion,
                            m.nombreGenérico AS Nombre,
                            dp.cantidad AS Inventario,
                            dp.precioVenta AS Precio
                        FROM Medicamento m
                        JOIN presentacion p ON m.idpresentacion = p.idpresentacion
                        JOIN producto pr ON m.idproducto = pr.idproducto
                        FULL JOIN detalles_pedido dp ON pr.idproducto = dp.idproducto
                        WHERE m.nombreGenérico LIKE @nombreMedicamento"; // Usar LIKE para hacer la búsqueda

                // Crear un SqlDataAdapter usando la conexión obtenida de la clase conexionBrandon
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());

                // Agregar el parámetro para la búsqueda
                da.SelectCommand.Parameters.AddWithValue("@nombreMedicamento", "%" + nombreMedicamento + "%");

                DataTable dt = new DataTable();

                // Llenar el DataTable con los resultados de la consulta
                da.Fill(dt);

                // Asignar el DataTable al DataGridView
                dataGridView1.DataSource = dt;

                // Asegurarse de que las columnas se generen correctamente
                dataGridView1.AutoGenerateColumns = true; // Esta propiedad debería estar en true por defecto



                // Cerrar la conexión
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el texto seleccionado del ComboBox
            string seleccion = comboBox1.SelectedItem.ToString();

            // Definir el filtro de búsqueda basado en la selección
            string filtro = "";

            // Asignar el filtro según la selección en el ComboBox
            switch (seleccion)
            {
                case "Eliminar Filtro":
                    filtro = "Eliminar Filtro";
                    break;
                case "Antibiótico":
                    filtro = "Antibiótico";
                    break;
                case "Antiparasito":
                    filtro = "Antiparasito";
                    break;
                case "Analgésico":
                    filtro = "Analgésico";
                    break;
                case "Suplemento":
                    filtro = "Suplemento";
                    break;
                case "Vacuna":
                    filtro = "Vacuna";
                    break;
                case "Desparasitante":
                    filtro = "Desparasitante";
                    break;
                case "Antiiflamatorio":
                    filtro = "Antiiflamatorio";
                    break;
                case "Sedante":
                    filtro = "Sedante";
                    break;
                case "Antidiarreico":
                    filtro = "Antidiarreico";
                    break;
                case "Antihistamínico":
                    filtro = "Antihistamínico";
                    break;
                default:
                    filtro = ""; // Si no se selecciona nada, no filtrar
                    break;
            }

            // Llamar a la función de búsqueda con el filtro
            BuscarMedicamentosPorCategoria(filtro);
        }
        private void BuscarMedicamentosPorCategoria(string filtro)
        {
            if (filtro == "Eliminar Filtro")
                CargarDatos();
            else
            {

                try
                {
                    // Crear una instancia de la clase conexionBrandon
                    conexionBrandon conexion = new conexionBrandon();

                    // Abrir la conexión
                    conexion.AbrirConexion();

                    // Definir la consulta básica
                    string query = @"
                    SELECT 
                        p.nombre AS Presentacion,
                        m.nombreGenérico AS Nombre,
                        dp.cantidad AS Inventario,
                        dp.precioVenta AS Precio
                    FROM Medicamento m
                    JOIN presentacion p ON m.idpresentacion = p.idpresentacion
                    JOIN producto pr ON m.idproducto = pr.idproducto
                    FULL JOIN detalles_pedido dp ON pr.idproducto = dp.idproducto";

                    // Si hay un filtro, agregar la cláusula WHERE
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        query += " WHERE pr.nombre LIKE @filtro";  // Filtrar por el nombre en la tabla 'producto'
                    }

                    // Crear un SqlDataAdapter con la conexión obtenida de la clase conexionBrandon
                    SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());

                    // Agregar el parámetro para el filtro
                    da.SelectCommand.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    DataTable dt = new DataTable();

                    // Llenar el DataTable con los resultados de la consulta
                    da.Fill(dt);

                    // Asignar el DataTable al DataGridView
                    dataGridView1.DataSource = dt;

                    // Asegurarse de que las columnas se generen correctamente
                    dataGridView1.AutoGenerateColumns = true;

                    // Cerrar la conexión
                    conexion.CerrarConexion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void txtProducto_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtProducto.Text == "Buscar nombre de medicamento") // Si el texto predeterminado está presente
            {
                txtProducto.Text = ""; // Limpia el TextBox
            }
        }

        private void FiltrarMedicamentosPorFecha(DateTime fechaSeleccionada)
        {
            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Consulta SQL para filtrar medicamentos por fecha de registro
                string query = @"
            SELECT 
                p.nombre AS Presentacion,
                m.nombreGenérico AS Nombre,
                dp.cantidad AS Inventario,
                dp.precioVenta AS Precio
            FROM Medicamento m
            JOIN presentacion p ON m.idpresentacion = p.idpresentacion
            JOIN producto pr ON m.idproducto = pr.idproducto
            FULL JOIN detalles_pedido dp ON pr.idproducto = dp.idproducto
            WHERE CAST(pr.fechaRegistro AS DATE) = @fechaSeleccionada;";

                // Crear un SqlDataAdapter con la conexión
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());

                // Agregar el parámetro de fecha
                da.SelectCommand.Parameters.AddWithValue("@fechaSeleccionada", fechaSeleccionada.Date);

                DataTable dt = new DataTable();

                // Llenar el DataTable con los resultados
                da.Fill(dt);

                // Asignar el DataTable al DataGridView
                dataGridView1.DataSource = dt;
                dataGridView1.AutoGenerateColumns = true;

                // Cerrar la conexión
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FiltrarMedicamentosPorFecha(dateTimePicker1.Value);
        }
    }
}

