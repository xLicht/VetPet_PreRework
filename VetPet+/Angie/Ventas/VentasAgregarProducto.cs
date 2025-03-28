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
using VetPet_.Angie.Mascotas;
using static VetPet_.Angie.VentasAgregarProducto;

namespace VetPet_.Angie
{
    public partial class VentasAgregarProducto : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();
        private Form1 parentForm;
        private static DataTable dtProductos = new DataTable();
        public string FormularioOrigen { get; set; }
        public int idCita;
        public VentasAgregarProducto(Form1 parent, int idProducto, decimal subTotal, int stock, int idCita)
        {
            InitializeComponent();
            parentForm = parent;
            this.idCita = idCita;
            this.Load += VentasAgregarProducto_Load;
            this.Resize += VentasAgregarProducto_Resize;
            dataGridView2.CellMouseEnter += dataGridView1_CellMouseEnter;
            dataGridView2.CellMouseLeave += dataGridView1_CellMouseLeave;
            dataGridView2.DataBindingComplete += dataGridView1_DataBindingComplete;
            PersonalizarDataGridView(dataGridView1);
            PersonalizarDataGridView(dataGridView2);
            Cargar();
            CargarProductosEnDataGridView(idProducto, subTotal);

            if (dtProductos.Rows.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dtProductos;
                bs.Filter = "[Total] IS NOT NULL";
                dataGridView2.DataSource = bs;
            }

        }
        public VentasAgregarProducto(Form1 parent, int idProducto, decimal subTotal, int stock)
        {
            InitializeComponent();
            parentForm = parent;
            this.Load += VentasAgregarProducto_Load;
            this.Resize += VentasAgregarProducto_Resize;
            dataGridView2.CellMouseEnter += dataGridView1_CellMouseEnter;
            dataGridView2.CellMouseLeave += dataGridView1_CellMouseLeave;
            dataGridView2.DataBindingComplete += dataGridView1_DataBindingComplete;
            PersonalizarDataGridView(dataGridView1);
            PersonalizarDataGridView(dataGridView2);
            Cargar();
            CargarProductosEnDataGridView(idProducto, subTotal);

            if (dtProductos.Rows.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dtProductos;
                bs.Filter = "[Total] IS NOT NULL";
                dataGridView2.DataSource = bs;
            }

        }
        public VentasAgregarProducto(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            this.Load += VentasAgregarProducto_Load;
            this.Resize += VentasAgregarProducto_Resize;
            dataGridView2.CellMouseEnter += dataGridView1_CellMouseEnter;
            dataGridView2.CellMouseLeave += dataGridView1_CellMouseLeave;
            dataGridView2.DataBindingComplete += dataGridView1_DataBindingComplete;
            PersonalizarDataGridView(dataGridView1);
            PersonalizarDataGridView(dataGridView2);
            Cargar();

            if (dtProductos.Rows.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dtProductos;
                bs.Filter = "[Total] IS NOT NULL";
                dataGridView2.DataSource = bs;
            }

        }
        private void CargarProductosEnDataGridView(int idProducto, decimal subTotal)
        {
            try
            {
                mismetodos.AbrirConexion();

                string query = @"
        SELECT 
            p.idProducto AS idProducto,
            p.nombre AS Producto,
            p.precioVenta AS Precio
        FROM Producto p
        INNER JOIN Marca m ON p.idMarca = m.idMarca
        WHERE P.idTipoProducto = 1 OR P.idTipoProducto = 2;";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter da = new SqlDataAdapter(comando))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Si es la primera vez que se usa, inicializar dtProductos y agregar la columna "Total"
                    if (dtProductos.Columns.Count == 0)
                    {
                        dtProductos = dt.Clone(); // Clonar estructura sin datos
                        dtProductos.Columns.Add("Total", typeof(decimal)); // Agregar columna Total
                    }

                    // Buscar si el producto ya existe en dtProductos
                    DataRow existingRow = dtProductos.AsEnumerable()
                        .FirstOrDefault(r => r.Field<int>("idProducto") == idProducto);

                    if (existingRow == null)
                    {
                        // Buscar el producto en dt y agregarlo a dtProductos
                        DataRow newRow = dt.AsEnumerable()
                            .FirstOrDefault(r => r.Field<int>("idProducto") == idProducto);

                        if (newRow != null)
                        {
                            DataRow rowToAdd = dtProductos.NewRow();
                            rowToAdd.ItemArray = newRow.ItemArray;
                            rowToAdd["Total"] = subTotal; // Asignar el total
                            dtProductos.Rows.Add(rowToAdd);
                        }
                    }
                    else
                    {
                        // Si el producto ya está en la tabla, sumarle el subtotal
                        existingRow["Total"] = Convert.ToDecimal(existingRow["Total"]) + subTotal;
                    }

                    // Filtrar y actualizar el DataGridView
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dtProductos;
                    bs.Filter = "[Total] IS NOT NULL";
                    dataGridView2.DataSource = bs;
                    ActualizarTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Método para calcular la suma y mostrarla en textBox2
        private void ActualizarTotal()
        {
            decimal sumaTotal = dtProductos.AsEnumerable()
                .Where(r => r["Total"] != DBNull.Value)
                .Sum(r => r.Field<decimal>("Total"));

            textBox2.Text = "Subtotal: " + sumaTotal.ToString("0.00"); // Mostrar con 2 decimales
        }

        public void Cargar()
        {
            try
            {
                mismetodos.AbrirConexion();
                string query = @"
            SELECT 
                P.idProducto,
                P.nombre AS Producto,
                P.precioVenta AS Precio,
                P.stock AS Inventario,
                M.nombre AS Marca
            FROM 
                Producto P
            JOIN 
                Marca M ON P.idMarca = M.idMarca
            WHERE 
                (P.idTipoProducto = 1 OR P.idTipoProducto = 2);";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    // Verificar si hay cambios de stock en memoria
                    foreach (DataRow row in tabla.Rows)
                    {
                        int idProducto = Convert.ToInt32(row["idProducto"]);
                        int stockModificado = StockManager1.ObtenerStockModificado(idProducto);

                        if (stockModificado != -1) // Si hay cambios en el stock
                        {
                            row["Inventario"] = stockModificado; // Actualizar el stock en la tabla
                        }
                    }

                    // Asignar el DataTable al DataGridView
                    dataGridView1.DataSource = tabla;
                    dataGridView1.Columns["idProducto"].Visible = false; // Oculta la columna
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    // Obtener el idMascota y nombre de la mascota seleccionada
                    int idMedicamento = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idProducto"].Value);
                    //string nombreMascota = dataGridView1.Rows[e.RowIndex].Cells["Mascota"].Value.ToString();

                    // Abrir el formulario de detalles de la mascota con el idMascota correcto
                    parentForm.formularioHijo(new VentasDeseaAgregarProducto(parentForm, idMedicamento,idCita));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }
        private void FiltrarDatos()
        {
            try
            {
                mismetodos.AbrirConexion(); // Abre la conexión usando Mismetodos
                string filtroTexto = textBox1.Text.Trim();
                string columnaSeleccionada = comboBox1.SelectedItem?.ToString(); // Puede ser null

                if (string.IsNullOrEmpty(filtroTexto))
                {
                    Cargar();
                    return; // No hace nada si el campo de búsqueda está vacío
                }

                // Mapeo de nombres visibles a nombres reales de columnas
                Dictionary<string, string> mapaColumnas = new Dictionary<string, string>
        {
            { "Producto", "P.nombre" },  // Nombre del producto
            { "Precio", "P.PrecioVenta" },  // Precio de venta del producto
            { "Inventario", "P.stock" },  // Stock del producto
            { "Marca", "M.nombre" }  // Nombre de la marca
        };

                string query;

                if (string.IsNullOrEmpty(columnaSeleccionada) || !mapaColumnas.ContainsKey(columnaSeleccionada))
                {
                    // Si no hay columna seleccionada, busca en todas las columnas relevantes
                    query = @"
                SELECT 
                    P.idProducto,
                    P.nombre AS Producto,
                    P.PrecioVenta AS Precio,
                    P.stock AS Inventario,
                    M.nombre AS Marca
                FROM 
                    Producto P
                INNER JOIN 
                    Marca M ON P.idMarca = M.idMarca
                WHERE 
                    P.nombre LIKE @filtro 
                    OR P.PrecioVenta LIKE @filtro 
                    OR P.stock LIKE @filtro 
                    OR M.nombre LIKE @filtro";
                }
                else
                {
                    string columnaReal = mapaColumnas[columnaSeleccionada];

                    query = $@"
                SELECT 
                    P.idProducto,
                    P.nombre AS Producto,
                    P.PrecioVenta AS Precio,
                    P.stock AS Inventario,
                    M.nombre AS Marca
                FROM 
                    Producto P
                INNER JOIN 
                    Marca M ON P.idMarca = M.idMarca
                WHERE 
                    {columnaReal} LIKE @filtro";
                }

                // Ejecutar la consulta
                SqlDataAdapter adaptador = new SqlDataAdapter(query, mismetodos.GetConexion());
                adaptador.SelectCommand.Parameters.AddWithValue("@filtro", "%" + filtroTexto + "%");

                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["idProducto"].Visible = false; // Oculta la columna ID
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
            finally
            {
                mismetodos.CerrarConexion(); // Cierra la conexión para evitar bloqueos en la base de datos
            }
        }

        public void PersonalizarDataGridView(DataGridView dataGridView2)
        {
            dataGridView2.BorderStyle = BorderStyle.None; // Elimina bordes
            dataGridView2.BackgroundColor = Color.White; // Fondo blanco

            // Alternar colores de filas
            dataGridView2.DefaultCellStyle.BackColor = Color.White;

            // Color de la selección
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.Pink;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Encabezados más elegantes
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightPink;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            // Bordes y alineación
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Ajustar el alto de los encabezados
            dataGridView2.ColumnHeadersHeight = 30;

            // Autoajustar el tamaño de las columnas
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FiltrarDatos();
        }
        private void VentasAgregarProducto_Load(object sender, EventArgs e)
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

        private void VentasAgregarProducto_Resize(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (FormularioOrigen == "VentasNuevaVenta")
            {
                decimal sumaTotal = dtProductos.AsEnumerable()
              .Where(r => r["Total"] != DBNull.Value)
              .Sum(r => r.Field<decimal>("Total"));
                parentForm.formularioHijo(new VentasNuevaVenta(parentForm, sumaTotal ,dtProductos,0,true)); // Pasamos la referencia de Form1 a
            }
            if (FormularioOrigen == "VentasVentanadePago")
            {
                decimal sumaTotal = dtProductos.AsEnumerable()
               .Where(r => r["Total"] != DBNull.Value)
               .Sum(r => r.Field<decimal>("Total"));
                parentForm.formularioHijo(new VentasVentanadePago(parentForm, idCita, sumaTotal, dtProductos, 0, true)); // Pasamos la referencia de Form1 a
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (idCita != 0)
            {
                decimal sumaTotal = dtProductos.AsEnumerable()
              .Where(r => r["Total"] != DBNull.Value)
              .Sum(r => r.Field<decimal>("Total"));
                parentForm.formularioHijo(new VentasVentanadePago(parentForm, idCita, sumaTotal, dtProductos, 0, true));

            }
            else
            {
                decimal sumaTotal = dtProductos.AsEnumerable()
               .Where(r => r["Total"] != DBNull.Value)
               .Sum(r => r.Field<decimal>("Total"));
                parentForm.formularioHijo(new VentasNuevaVenta(parentForm, sumaTotal, dtProductos, 0, true)); // Pasamos la referencia de Form1 a
            }
        }

        public static class StockManager1
        {
            // Diccionario para almacenar los cambios de stock
            public static Dictionary<int, int> StockModificado { get; set; } = new Dictionary<int, int>();

            // Método para actualizar el stock
            public static void ActualizarStock(int idProducto, int nuevoStock)
            {
                if (StockModificado.ContainsKey(idProducto))
                {
                    StockModificado[idProducto] = nuevoStock;
                }
                else
                {
                    StockModificado.Add(idProducto, nuevoStock);
                }
            }

            // Método para obtener el stock modificado
            public static int ObtenerStockModificado(int idProducto)
            {
                if (StockModificado.ContainsKey(idProducto))
                {
                    return StockModificado[idProducto];
                }
                return -1; // Retorna -1 si no hay cambios en el stock
            }

            // Método para limpiar el diccionario (opcional)
            public static void LimpiarStockModificado()
            {
                StockModificado.Clear();
            }
        }
    }
}
