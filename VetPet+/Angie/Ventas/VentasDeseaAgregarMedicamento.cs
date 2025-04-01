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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static VetPet_.Angie.VentasAgregarMedicamento;

namespace VetPet_.Angie
{
    public partial class VentasDeseaAgregarMedicamento : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();
        private Form1 parentForm;
        int idCita = 0;

        private static Dictionary<int, int> _stockModificado = new Dictionary<int, int>();
        public VentasDeseaAgregarMedicamento(Form1 parent, int idProducto )
        {
            InitializeComponent();
            this.Load += VentasDeseaAgregarMedicamento_Load;       // Evento Load
            this.Resize += VentasDeseaAgregarMedicamento_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            CargarMedicamento(idProducto);        
        }

        public VentasDeseaAgregarMedicamento(Form1 parent, int idProducto, int idCita)
        {
            InitializeComponent();
            this.Load += VentasDeseaAgregarMedicamento_Load;       // Evento Load
            this.Resize += VentasDeseaAgregarMedicamento_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            CargarMedicamento(idProducto);
            this.idCita = idCita;   
        }
        public void CargarMedicamento(int idMedicamento)
        {
            try
            {
                mismetodos.AbrirConexion();
                string query = @"
                            SELECT 
                    idProducto AS idProducto,
                    nombre AS NombreMedicamento
                FROM 
                    Producto
                WHERE 
                    idProducto = @idProducto
                    AND idTipoProducto = 3 ;
                        ";

                // Usar `using` para asegurar la correcta liberación de recursos
                using (SqlCommand comando2 = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    comando2.Parameters.AddWithValue("@idProducto", idMedicamento);

                    using (SqlDataReader reader = comando2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox5.Text = reader["NombreMedicamento"].ToString();
                            label3.Text = reader["idProducto"].ToString();
                        }
                        else
                        {
                            // Si no se encuentra la cita, mostrar un mensaje o dejar el TextBox vacío
                            textBox5.Text = "No se encontró la cita.";
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
        private void VentasDeseaAgregarMedicamento_Load(object sender, EventArgs e)
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

        private void VentasDeseaAgregarMedicamento_Resize(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBox4.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProducto = int.Parse(label3.Text);
                decimal precio = ObtenerPrecioProducto(idProducto);

                if (precio == -1)
                {
                    MessageBox.Show("No se pudo obtener el precio del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Calcular el total
                decimal total = cantidad * precio;

                // Obtener el stock actual del producto
                int stockActual = ObtenerStockActual(idProducto);

                if (stockActual == -1)
                {
                    MessageBox.Show("No se encontró el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar que haya suficiente stock
                if (cantidad > stockActual)
                {
                    MessageBox.Show("No hay suficiente stock disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calcular el nuevo stock
                int nuevoStock = stockActual - cantidad;

                // Actualizar el stock en memoria usando StockManager
                StockManager.ActualizarStock(idProducto, nuevoStock);

                // Mostrar el nuevo stock en la interfaz de usuario
                MessageBox.Show($"Stock actual: {stockActual}\nStock después de la venta: {nuevoStock}", "Stock Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Pasar al siguiente formulario
                if (idCita != 0)
                {
                    parentForm.formularioHijo(new VentasAgregarMedicamento(parentForm, idProducto, total, nuevoStock, idCita, cantidad));
                }
                else
                {
                    parentForm.formularioHijo(new VentasAgregarMedicamento(parentForm, idProducto, total, nuevoStock, cantidad));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerStockActual(int idProducto)
        {
            try
            {
                mismetodos.AbrirConexion();
                string query = @"
SELECT SUM(stock) 
FROM Lote_Producto 
WHERE idProducto = @idProducto
  AND estado = 'A'
  AND (fechaCaducidad IS NULL OR fechaCaducidad >= GETDATE());";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    comando.Parameters.AddWithValue("@idProducto", idProducto);
                    object result = comando.ExecuteScalar();

                    // Si hay stock disponible (puede ser NULL si no hay lotes válidos)
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0; // Retorna 0 si no hay lotes válidos
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el stock: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Retorna -1 si hay un error
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }

        private decimal ObtenerPrecioProducto(int idProducto)
        {
            decimal precio = 0;

            try
            {
                // Abrir conexión usando los métodos existentes
                mismetodos.AbrirConexion();

                string query = @"
            SELECT precioVenta 
            FROM Producto 
            WHERE idProducto = @idProducto AND idTipoProducto = 3;";

                // Usar `using` para manejar recursos correctamente
                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    comando.Parameters.AddWithValue("@idProducto", idProducto);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            precio = reader.GetDecimal(0); // Obtiene el valor del primer campo (precio)
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el precio: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión al finalizar
                mismetodos.CerrarConexion();
            }

            return precio;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasAgregarMedicamento(parentForm)); // Pasamos la referencia de Form1 a
        }
    }
    
}
