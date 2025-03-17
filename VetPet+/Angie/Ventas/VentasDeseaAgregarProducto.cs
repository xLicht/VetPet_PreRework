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
    public partial class VentasDeseaAgregarProducto : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();
        private Form1 parentForm;
        int idCita = 0;
        private decimal subtotal;
        private int stock;

        public VentasDeseaAgregarProducto()
        {
            InitializeComponent();
            this.Load += VentasDeseaAgregarProducto_Load;       // Evento Load
            this.Resize += VentasDeseaAgregarProducto_Resize;   // Evento Resize

        }

        public VentasDeseaAgregarProducto(Form1 parent, int idProducto)
        {
            InitializeComponent();
            this.Load += VentasDeseaAgregarProducto_Load;       // Evento Load
            this.Resize += VentasDeseaAgregarProducto_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            CargarMedicamento(idProducto);
        }

        public VentasDeseaAgregarProducto(Form1 parent, int idProducto, int idCita)
        {
            InitializeComponent();
            this.Load += VentasDeseaAgregarProducto_Load;       // Evento Load
            this.Resize += VentasDeseaAgregarProducto_Resize;   // Evento Resize
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
                    idProducto as idProducto,
                    nombre AS NombreMedicamento
                FROM 
                    Producto
                WHERE 
                    idProducto = @idProducto
                    AND idTipoProducto = 1 OR idTipoProducto = 2;
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
        public VentasDeseaAgregarProducto(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
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
                decimal precio = ObtenerPrecioProducto(int.Parse(label3.Text));

                if (precio == -1)
                {
                    MessageBox.Show("No se pudo obtener el precio del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Calcular el total
                decimal total = cantidad * precio;
                mismetodos.AbrirConexion();

                // Consulta para obtener el stock actual del producto
                string queryStock = @"
                    SELECT stock 
                    FROM Producto 
                    WHERE idProducto = @idProducto;
                ";

                int stockActual = 0;

                // Obtener el stock actual del producto
                using (SqlCommand comandoStock = new SqlCommand(queryStock, mismetodos.GetConexion()))
                {
                    comandoStock.Parameters.AddWithValue("@idProducto", label3.Text);

                    using (SqlDataReader reader = comandoStock.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stockActual = Convert.ToInt32(reader["stock"]);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Validar que haya suficiente stock
                if (cantidad > stockActual)
                {
                    MessageBox.Show("No hay suficiente stock disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calcular el nuevo stock
                int nuevoStock = stockActual - cantidad;

                // Consulta para actualizar el stock del producto
                string queryActualizarStock = @"
                    UPDATE Producto 
                    SET stock = @nuevoStock 
                    WHERE idProducto = @idProducto;
                ";

                // Ejecutar la actualización del stock
                using (SqlCommand comandoActualizar = new SqlCommand(queryActualizarStock, mismetodos.GetConexion()))
                {
                    comandoActualizar.Parameters.AddWithValue("@nuevoStock", nuevoStock);
                    comandoActualizar.Parameters.AddWithValue("@idProducto", label3.Text);

                    int filasAfectadas = comandoActualizar.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Stock actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (idCita != 0)
                        {
                            parentForm.formularioHijo(new VentasAgregarProducto(parentForm, int.Parse(label3.Text), total, nuevoStock, idCita));
                        }
                        else
                        {
                            parentForm.formularioHijo(new VentasAgregarProducto(parentForm, int.Parse(label3.Text), total, nuevoStock));
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar el error si ocurre algún problema
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar la conexión al finalizar
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
            WHERE idProducto = @idProducto AND idTipoProducto = 1 OR idTipoProducto = 2";

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
        private void VentasDeseaAgregarProducto_Load(object sender, EventArgs e)
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
        private void VentasDeseaAgregarProducto_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new VentasAgregarProducto(parentForm, idCita,subtotal,stock));
        }

    }
}
