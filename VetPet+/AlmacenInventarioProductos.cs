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
    public partial class AlmacenInventarioProductos : FormPadre
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        public static string formularioAnterior; // Guarda el nombre del formulario anterior

        private Form1 parentForm;

        public AlmacenInventarioProductos()
        {
            InitializeComponent();
            this.Load += AlmacenInventarioProductos_Load;       // Evento Load
            this.Resize += AlmacenInventarioProductos_Resize;   // Evento Resize

            comboBox1.FlatStyle = FlatStyle.Flat;  // Quita bordes
            comboBox1.DropDownWidth = 150;         // Ancho del desplegable
        }
        public AlmacenInventarioProductos(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1

            comboBox1.FlatStyle = FlatStyle.Flat;  // Quita bordes
            comboBox1.DropDownWidth = 150;         // Ancho del desplegable
            CargarDatos();
        }

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
                    p.nombre AS Nombre,
                    dp.precioVenta AS Precio,
                    dp.cantidad AS Cantidad,
                    m.nombre AS Marca
                FROM Producto p
                FULL JOIN detalles_pedido dp ON p.idproducto = dp.idproducto
                JOIN Marca m ON p.idMarca = m.idMarca
                JOIN TipoProducto tp ON p.idTipoProducto = tp.idTipoProducto
                WHERE tp.nombre <> 'Medicamento';
                ";

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

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtProducto_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtProducto.Text == "Buscar nombre de producto") // Si el texto predeterminado está presente
            {
                txtProducto.Text = ""; // Limpia el TextBox
            }
        }

        private void txtProducto_Leave(object sender, EventArgs e)
        {
            // Si el usuario no escribe nada, muestra un texto predeterminado
            if (string.IsNullOrEmpty(txtProducto.Text))
            {
                txtProducto.Text = "Buscar nombre de producto"; // Texto predeterminado
            }
        }

        private void AlmacenInventarioProductos_Load(object sender, EventArgs e)
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

        private void AlmacenInventarioProductos_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new AlmacenAgregarProducto(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenMenu(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombre = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();

                // Llamar al formulario de opciones
                using (var opcionesForm = new AlmacenAvisoVerOModificar(nombre, parentForm))
                {
                    if (opcionesForm.ShowDialog() == DialogResult.OK)
                    {
                        if (opcionesForm.Resultado == "Modificar")
                        {
                            parentForm.formularioHijo(new AlmacenModificarProducto(parentForm, nombre)); // Pasamos la referencia de Form1 a 
                        }
                        if (opcionesForm.Resultado == "Salir")
                        {
                            AlmacenInventarioProductos.formularioAnterior = "AlmacenInventarioProductos";
                            parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm)); // Pasamos la referencia de Form1 a 
                        }   
                        else if (opcionesForm.Resultado == "Ver")
                        {
                            parentForm.formularioHijo(new AlmacenVerProducto(parentForm, nombre)); // Pasamos la referencia de Form1 a 
                        }
                    }
                }
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
                case "Accesorio":
                    filtro = "Accesorio";
                    break;
                case "Comida":
                    filtro = "Comida";
                    break;
                default:
                    filtro = ""; // Si no se selecciona nada, no filtrar
                    break;
            }

            // Llamar a la función de búsqueda con el filtro
            BuscarProductoPorCategoria(filtro);
        }
        private void BuscarProductoPorCategoria(string filtro)
        {
            if (filtro == "Eliminar Filtro")
            {
                CargarDatos();
                return;
            }

            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Definir la consulta básica
                string query = @"
                SELECT 
                p.nombre AS Nombre,
                dp.precioVenta AS Precio,
                dp.cantidad AS Cantidad,
                m.nombre AS Marca
                FROM Producto p
                FULL JOIN detalles_pedido dp ON p.idproducto = dp.idproducto
                JOIN Marca m ON p.idMarca = m.idMarca
                JOIN TipoProducto tp ON p.idTipoProducto = tp.idTipoProducto";

                // Agregar WHERE solo si el filtro no está vacío
                if (!string.IsNullOrEmpty(filtro))
                {
                    query += " WHERE tp.nombre LIKE @filtro";
                }

                // Crear un SqlDataAdapter con la conexión obtenida de la clase conexionBrandon
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());

                // Agregar el parámetro para el filtro solo si hay un filtro
                if (!string.IsNullOrEmpty(filtro))
                {
                    da.SelectCommand.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                }

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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el texto del TextBox (nombre del medicamento a buscar)
            string nombreProducto = txtProducto.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrEmpty(nombreProducto))
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
                    p.nombre AS Nombre,
                    dp.precioVenta AS Precio,
                    dp.cantidad AS Cantidad,
                    m.nombre AS Marca
                FROM Producto p
                FULL JOIN detalles_pedido dp ON p.idproducto = dp.idproducto
                JOIN Marca m ON p.idMarca = m.idMarca
                JOIN TipoProducto tp ON p.idTipoProducto = tp.idTipoProducto
                WHERE p.nombre LIKE @nombreProducto"; // Usar LIKE para hacer la búsqueda

                // Crear un SqlDataAdapter usando la conexión obtenida de la clase conexionBrandon
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());

                // Agregar el parámetro para la búsqueda
                da.SelectCommand.Parameters.AddWithValue("@nombreProducto", "%" + nombreProducto + "%");

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

        private void txtProducto_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtProducto_Enter_1(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtProducto.Text == "Buscar nombre de producto") // Si el texto predeterminado está presente
            {
                txtProducto.Text = ""; // Limpia el TextBox
            }
        }

        private void FiltrarPorFecha(DateTime fechaSeleccionada)
        {
            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Consulta SQL para filtrar productos por fechaRegistro
                string query = @"
                SELECT 
                    p.nombre AS Nombre,
                    dp.precioVenta AS Precio,
                    dp.cantidad AS Cantidad,
                    m.nombre AS Marca
                FROM Producto p
                FULL JOIN detalles_pedido dp ON p.idproducto = dp.idproducto
                JOIN Marca m ON p.idMarca = m.idMarca
                WHERE CAST(p.fechaRegistro AS DATE) = @fechaSeleccionada;";

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
            FiltrarPorFecha(dateTimePicker1.Value);
        }
    }
}
