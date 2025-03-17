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
using VetPet_.Angie;
using VetPet_.Angie.Mascotas;

namespace VetPet_
{
    public partial class VentasVentanadePago : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();
        private Form1 parentForm;
        private int idCita;
        private decimal subtotal;
        private int stock;
        private static decimal totalServicios = -1; // -1 indica que aún no se ha calculado
        private static DataTable dtProductos = new DataTable();

        public VentasVentanadePago(Form1 parent, int idCita, decimal total, DataTable dt)
        {
            InitializeComponent();
            this.Load += VentasVentanadePago_Load;
            this.Resize += VentasVentanadePago_Resize;
            CargarServicios(idCita);
            this.idCita = idCita;
            parentForm = parent;

            // Si es la primera vez, clonar la estructura
            if (dtProductos.Columns.Count == 0)
            {
                dtProductos = dt.Clone();
            }

            // Agregar los nuevos productos o medicamentos sin perder los anteriores
            AgregarProductosAMedicamentos(dt);

            // Vincular dtProductos al DataGridView
            BindingSource bs = new BindingSource();
            bs.DataSource = dtProductos;
            dataGridView2.DataSource = bs;

            // Actualizar la suma total
            ActualizarSumaTotal();
        }

        // 🔹 Método para agregar productos o medicamentos a la tabla sin eliminar los anteriores
        private void AgregarProductosAMedicamentos(DataTable dtNuevos)
        {
            foreach (DataRow row in dtNuevos.Rows)
            {
                int idProducto = row.Field<int>("idProducto");

                // Buscar si el producto ya está en la tabla
                DataRow existingRow = dtProductos.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("idProducto") == idProducto);

                if (existingRow == null)
                {
                    // Si no existe, agregarlo
                    dtProductos.ImportRow(row);
                }
                else
                {
                    // Si ya existe, actualizar el total sumando el nuevo subtotal
                    existingRow["Total"] = Convert.ToDecimal(existingRow["Total"]) + Convert.ToDecimal(row["Total"]);
                }
            }
        }

        private void ActualizarSumaTotal()
        {
            // Sumar el total de productos
            decimal sumaTotalProductos = dtProductos.AsEnumerable()
                .Where(r => r["Total"] != DBNull.Value)
                .Sum(r => r.Field<decimal>("Total"));

            // Calcular la suma total de productos y servicios
            decimal sumaFinal = sumaTotalProductos + (totalServicios > -1 ? totalServicios : 0);

            textBox8.Text = "Subtotal: "+sumaFinal.ToString("0.##");
        }

        public void CargarServicios(int idCita)
        {
            try
            {
                mismetodos.AbrirConexion();

                string query = "EXEC sp_ObtenerServiciosCita @idCita = @idCita;";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    comando.Parameters.AddWithValue("@idCita", idCita);

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);

                        dataGridView1.DataSource = tabla;

                        // Eliminar filas vacías
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsNewRow) continue;

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
                                dataGridView1.Rows.Remove(row);
                            }
                        }

                        // Calcular la suma del total de la consulta solo si no se ha calculado antes
                        if (totalServicios == -1)
                        {
                            totalServicios = tabla.AsEnumerable()
                                .Where(r => r["Precio"] != DBNull.Value)
                                .Sum(r => r.Field<decimal>("Precio"));
                        }

                        // Sumar este total al `textBox8`
                        ActualizarSumaTotal();
                    }
                }

                string queryDueño = @"
                                SELECT 
                            P.nombre AS NombrePersona,
                            P.apellidoP AS ApellidoPaterno, 
                            M.nombre AS NombreMascota
                        FROM 
                            Cita C
                        JOIN 
                            Mascota M ON C.idMascota = M.idMascota
                        JOIN 
                            Persona P ON M.idPersona = P.idPersona
                        WHERE 
                            C.idCita = @idCita;
                            ";

                using (SqlCommand comando2 = new SqlCommand(queryDueño, mismetodos.GetConexion()))
                {
                    comando2.Parameters.AddWithValue("@idCita", idCita);

                    using (SqlDataReader reader = comando2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox3.Text = reader["NombrePersona"].ToString();
                            textBox4.Text = reader["apellidoPaterno"].ToString();
                            textBox5.Text = reader["NombreMascota"].ToString();
                        }
                        else
                        {
                            // Si no se encuentra la cita, mostrar un mensaje o dejar el TextBox vacío
                            textBox3.Text = "No se encontró la cita.";
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
        public VentasVentanadePago(Form1 parent, int idCita)
        {
            InitializeComponent();
            this.Load += VentasVentanadePago_Load;       // Evento Load
            this.Resize += VentasVentanadePago_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            CargarServicios(idCita);
            this.idCita = idCita;
        }
        private void VentasVentanadePago_Load(object sender, EventArgs e)
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

        private void VentasVentanadePago_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new VentasListado(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox15_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ConsultarCita(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox13_Click(object sender, EventArgs e)
        {
            VentasConfirmacionEfectivo VentasConfirmacionEfectivo = new VentasConfirmacionEfectivo(parentForm, subtotal, dtProductos);
            VentasConfirmacionEfectivo.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionEfectivo); // Usar la misma instancia
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            VentasConfirmacionTarjeta VentasConfirmacionTarjeta = new VentasConfirmacionTarjeta(parentForm,subtotal,dtProductos);
            VentasConfirmacionTarjeta.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionTarjeta); // Usar la misma instancia
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            VentasAgregarProducto VentasAgregarProducto = new VentasAgregarProducto(parentForm, idCita, subtotal, stock);
            VentasAgregarProducto.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasAgregarProducto); // Usar la misma instancia
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            VentasAgregarMedicamento ventasAgregarMedicamento = new VentasAgregarMedicamento(parentForm, idCita, subtotal,stock);
            parentForm.formularioHijo(ventasAgregarMedicamento); // Usar la misma instancia
        }

    }
}
