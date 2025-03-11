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
        int idCita;
        private Form1 parentForm;

        public VentasVentanadePago(Form1 parent, int idCita)
        {
            InitializeComponent();
            this.Load += VentasVentanadePago_Load;       // Evento Load
            this.Resize += VentasVentanadePago_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            CargarServicios(idCita);
        }

        public void CargarServicios(int idCita)
        {
            try
            {
                // Crear instancia de Mismetodos
                mismetodos = new Mismetodos();

                // Abrir conexión
                mismetodos.AbrirConexion();

                string query = @"
            SELECT 
                SE.nombre AS Servicio,
                CS.nombre AS Clase,
                E.usuario AS Empleado,
                SE.precio AS Precio
            FROM 
                Cita C
            JOIN 
                ServicioEspecificoNieto SE ON C.idServicioEspecificoNieto = SE.idServicioEspecificoNieto
            JOIN 
                ClaseServicio CS ON SE.idServicioEspecificoHijo = CS.idClaseServicio
            JOIN 
                Empleado E ON C.idEmpleado = E.idEmpleado
            WHERE 
                C.idCita = @idCita;
        ";

                // Usar `using` para asegurar la correcta liberación de recursos
                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    // Agregar el parámetro @idCita
                    comando.Parameters.AddWithValue("@idCita", idCita);

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        // Crear un DataTable y llenar los datos
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);

                        // Asignar el DataTable al DataGridView
                        dataGridView1.DataSource = tabla;

                        // Verificar si hay filas vacías y eliminarlas
                        foreach (DataGridViewRow row in dataGridView1.Rows)
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
                                dataGridView1.Rows.Remove(row);
                            }
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
            VentasConfirmacionEfectivo VentasConfirmacionEfectivo = new VentasConfirmacionEfectivo(parentForm);
            VentasConfirmacionEfectivo.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionEfectivo); // Usar la misma instancia
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            VentasConfirmacionTarjeta VentasConfirmacionTarjeta = new VentasConfirmacionTarjeta(parentForm);
            VentasConfirmacionTarjeta.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionTarjeta); // Usar la misma instancia
            parentForm.formularioHijo(new VentasConfirmacionTarjeta(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            VentasAgregarProducto VentasAgregarProducto = new VentasAgregarProducto(parentForm);
            VentasAgregarProducto.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasAgregarProducto); // Usar la misma instancia
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            VentasAgregarMedicamento ventasAgregarMedicamento = new VentasAgregarMedicamento(parentForm);
            ventasAgregarMedicamento.FormularioOrigen = "VentasVentanadePago"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(ventasAgregarMedicamento); // Usar la misma instancia
        }
    }
}
