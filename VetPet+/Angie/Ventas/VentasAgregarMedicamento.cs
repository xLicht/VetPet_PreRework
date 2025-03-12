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

namespace VetPet_.Angie
{
    public partial class VentasAgregarMedicamento : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();   
        private Form1 parentForm;

        public string FormularioOrigen {get;set;}

        public VentasAgregarMedicamento(Form1 parent, int idProducto,int subTotal)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
            this.Load += VentasAgregarMedicamento_Load;       // Evento Load
            this.Resize += VentasAgregarMedicamento_Resize;   // Evento Resize
            dataGridView2.CellMouseEnter += dataGridView1_CellMouseEnter;
            dataGridView2.CellMouseLeave += dataGridView1_CellMouseLeave;
            dataGridView2.DataBindingComplete += dataGridView1_DataBindingComplete;
            PersonalizarDataGridView(dataGridView2);
            PersonalizarDataGridView(dataGridView3);
            Cargar();
             CargarProductosEnDataGridView(idProducto, subTotal);
        }
        public VentasAgregarMedicamento(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
            this.Load += VentasAgregarMedicamento_Load;       // Evento Load
            this.Resize += VentasAgregarMedicamento_Resize;   // Evento Resize
            dataGridView2.CellMouseEnter += dataGridView1_CellMouseEnter;
            dataGridView2.CellMouseLeave += dataGridView1_CellMouseLeave;
            dataGridView2.DataBindingComplete += dataGridView1_DataBindingComplete;
            PersonalizarDataGridView(dataGridView2);
            PersonalizarDataGridView(dataGridView3);
                Cargar();
           
        }
        private void CargarProductosEnDataGridView(int idProducto, int subTotal)
        {
            try
            {
                mismetodos.AbrirConexion();

                string query = @"
            SELECT 
                p.nombre AS Producto,
                m.nombre AS Marca,
                p.precioVenta AS Precio
            FROM Producto p
            INNER JOIN Marca m ON p.idMarca = m.idMarca;";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter da = new SqlDataAdapter(comando))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Agregar una columna extra para el Total
                    dt.Columns.Add("Total", typeof(int));

                    dataGridView3.DataSource = dt;
                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        if (row.Cells["idProducto"].Value != null && Convert.ToInt32(row.Cells["idProducto"].Value) == idProducto)
                        {
                            decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
                            row.Cells["Total"].Value = subTotal;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        public void Cargar()
        {
            try
            {
                // Crear instancia de Mismetodos
                mismetodos = new Mismetodos();

                // Abrir conexión
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
                Marca M ON P.idMarca = M.idMarca  -- Unión con la tabla Marca
            WHERE 
                P.idTipoProducto = 3;  -- Filtrar por tipo de producto 'Medicamento'
        ";

                // Usar `using` para asegurar la correcta liberación de recursos
                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {

                    // Crear un DataTable y llenar los datos
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    // Asignar el DataTable al DataGridView
                    dataGridView2.DataSource = tabla;
                    dataGridView2.Columns["idProducto"].Visible = false; // Oculta la columna

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.IsNewRow) continue; // No borra la fila nueva si AllowUserToAddRows = true

                        bool vacia = true;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                            {
                                vacia = false;
                                break;
                            }
                        }

                        if (vacia)
                        {
                            dataGridView2.Rows.Remove(row);
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                // Manejar el error si ocurre algún problema
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión al finalizar
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
                    int idMedicamento = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["idProducto"].Value);
                    //string nombreMascota = dataGridView1.Rows[e.RowIndex].Cells["Mascota"].Value.ToString();

                    // Abrir el formulario de detalles de la mascota con el idMascota correcto
                   parentForm.formularioHijo(new VentasDeseaAgregarMedicamento(parentForm,idMedicamento,0));
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

                dataGridView2.DataSource = dt;
                dataGridView2.Columns["idProducto"].Visible = false; // Oculta la columna ID
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

            // Ocultar la columna del ID
            if (dataGridView2.Columns.Contains("idMascota"))
            {
                dataGridView2.Columns["idMascota"].Visible = false;
            }
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

        private void VentasAgregarMedicamento_Load(object sender, EventArgs e)
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

        private void VentasAgregarMedicamento_Resize(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (FormularioOrigen == "VentasNuevaVenta")
            {
                parentForm.formularioHijo(new VentasNuevaVenta(parentForm)); // Pasamos la referencia de Form1 a
            }
            if (FormularioOrigen == "VentasVentanadePago")
            {
                //parentForm.formularioHijo(new VentasVentanadePago(parentForm, idCita)); // Pasamos la referencia de Form1 a
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (FormularioOrigen == "VentasNuevaVenta")
            {
                parentForm.formularioHijo(new VentasNuevaVenta(parentForm)); // Pasamos la referencia de Form1 a
            }
            if (FormularioOrigen == "VentasVentanadePago")
            {
              //  parentForm.formularioHijo(new VentasVentanadePago(parentForm, idCita)); // Pasamos la referencia de Form1 a
            }
        }


    }
}
