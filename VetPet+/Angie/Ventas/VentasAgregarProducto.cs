﻿using System;
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
        public int cantidad;
        public VentasAgregarProducto(Form1 parent, int idProducto, decimal subTotal, int stock, int idCita, int cantidad)
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
            this.cantidad = cantidad;
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
        public VentasAgregarProducto(Form1 parent, int idProducto, decimal subTotal, int stock, int cantidad)
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
            this.cantidad = cantidad;
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
    p.idProducto,
    p.nombre AS Producto,
    lp.precioVenta AS Precio,
    SUM(lp.stock) AS Stock
FROM Producto p
INNER JOIN Lote_Producto lp ON p.idProducto = lp.idProducto
INNER JOIN Marca m ON p.idMarca = m.idMarca
WHERE (p.idTipoProducto = 1 OR p.idTipoProducto = 2)
  AND lp.estado = 'A'
  AND (lp.fechaCaducidad IS NULL OR lp.fechaCaducidad >= GETDATE())
GROUP BY p.idProducto, p.nombre, lp.precioVenta;";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter da = new SqlDataAdapter(comando))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dtProductos.Columns.Count == 0)
                    {
                        dtProductos = dt.Clone();
                        dtProductos.Columns.Add("Total", typeof(decimal));
                        dtProductos.Columns.Add("Cantidad", typeof(int));
                    }

                    DataRow existingRow = dtProductos.AsEnumerable()
                        .FirstOrDefault(r => r.Field<int>("idProducto") == idProducto);

                    if (existingRow == null)
                    {
                        DataRow newRow = dt.AsEnumerable()
                            .FirstOrDefault(r => r.Field<int>("idProducto") == idProducto);

                        if (newRow != null)
                        {
                            DataRow rowToAdd = dtProductos.NewRow();
                            rowToAdd.ItemArray = newRow.ItemArray;
                            rowToAdd["Total"] = subTotal;
                            rowToAdd["Cantidad"] = cantidad; 
                            dtProductos.Rows.Add(rowToAdd);
                        }
                    }
                    else
                    {
                        existingRow["Total"] = Convert.ToDecimal(existingRow["Total"]) + subTotal;
                        existingRow["Cantidad"] = Convert.ToInt32(existingRow["Cantidad"]) + cantidad;
                    }

                    BindingSource bs = new BindingSource();
                    bs.DataSource = dtProductos;
                    bs.Filter = "[Total] IS NOT NULL";
                    dataGridView2.DataSource = bs;
                    ActualizarTotal();


                    if (dataGridView2.Columns.Contains("idProducto"))
                        dataGridView2.Columns["idProducto"].Visible = false;

                    if (dataGridView2.Columns.Contains("StockDisponible"))
                        dataGridView2.Columns["StockDisponible"].Visible = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mismetodos.CerrarConexion();
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
    p.idProducto,
    p.nombre AS Producto,
    lp.precioVenta AS Precio,
    SUM(lp.stock) AS Inventario,
    m.nombre AS Marca,
    MIN(lp.fechaCaducidad) AS ProximaCaducidad
FROM 
    Producto p
INNER JOIN 
    Lote_Producto lp ON p.idProducto = lp.idProducto
INNER JOIN 
    Marca m ON p.idMarca = m.idMarca
WHERE 
    (p.idTipoProducto = 1 OR p.idTipoProducto = 2)
    AND lp.estado = 'A'
    AND (lp.fechaCaducidad IS NULL OR lp.fechaCaducidad >= GETDATE())
GROUP BY 
    p.idProducto, p.nombre, lp.precioVenta, m.nombre
ORDER BY 
    p.nombre;";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    foreach (DataRow row in tabla.Rows)
                    {
                        int idProducto = Convert.ToInt32(row["idProducto"]);
                        int stockModificado = StockManager1.ObtenerStockModificado(idProducto);

                        if (stockModificado != -1)
                        {
                            row["Inventario"] = stockModificado;
                        }
                    }

                    dataGridView1.DataSource = tabla;
                    dataGridView1.Columns["idProducto"].Visible = false;

                    // Formatear columna de precio
                    if (dataGridView1.Columns["Precio"] != null)
                    {
                        dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "C2";
                    }

                    // Formatear columna de fecha de caducidad
                    if (dataGridView1.Columns["ProximaCaducidad"] != null)
                    {
                        dataGridView1.Columns["ProximaCaducidad"].DefaultCellStyle.Format = "d";
                    }

                    if (dataGridView2.Columns.Contains("idProducto"))
                        dataGridView2.Columns["idProducto"].Visible = false;

                    if (dataGridView2.Columns.Contains("ProximaCaducidad"))
                        dataGridView2.Columns["ProximaCaducidad"].Visible = false;

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
                        int idMedicamento = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idProducto"].Value);
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

            // Configurar fuente más grande para las celdas
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular); // Tamaño 12

            // Aumentar el alto de las filas para que el texto sea legible
            dataGridView2.RowTemplate.Height = 30; // Altura de fila aumentada

            // Alternar colores de filas
            dataGridView2.DefaultCellStyle.BackColor = Color.White;

            // Color de la selección
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.Pink;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Encabezados más elegantes
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightPink;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold); // Tamaño aumentado a 14

            // Bordes y alineación
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Ajustar el alto de los encabezados (aumentado para la nueva fuente)
            dataGridView2.ColumnHeadersHeight = 40;

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
                dtProductos = new DataTable();
            }
            if (FormularioOrigen == "VentasVentanadePago")
            {
                decimal sumaTotal = dtProductos.AsEnumerable()
               .Where(r => r["Total"] != DBNull.Value)
               .Sum(r => r.Field<decimal>("Total"));
                parentForm.formularioHijo(new VentasVentanadePago(parentForm, idCita, sumaTotal, dtProductos, 0, true)); // Pasamos la referencia de Form1 a
                dtProductos = new DataTable();
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
                dtProductos = new DataTable();

            }
            else
            {
                decimal sumaTotal = dtProductos.AsEnumerable()
               .Where(r => r["Total"] != DBNull.Value)
               .Sum(r => r.Field<decimal>("Total"));
                parentForm.formularioHijo(new VentasNuevaVenta(parentForm, sumaTotal, dtProductos, 0, true)); // Pasamos la referencia de Form1 a
                dtProductos = new DataTable();
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
