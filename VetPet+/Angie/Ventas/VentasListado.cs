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

namespace VetPet_
{
    public partial class VentasListado : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();   

        private Form1 parentForm;
        public VentasListado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
            this.Load += VentasListado_Load;       // Evento Load
            this.Resize += VentasListado_Resize;   // Evento Resize
            Cargar();
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
                    P.nombre AS Persona,
                    P.apellidoP AS Apellido_Paterno,
                    C.numero AS Telefono,
                    M.nombre AS Mascota,
                    CI.fechaProgramada AS Fecha_Cita
                FROM 
                    Cita CI
                JOIN 
                    Mascota M ON CI.idMascota = M.idMascota
                JOIN 
                    Persona P ON M.idPersona = P.idPersona
                JOIN 
                    Celular C ON P.idPersona = C.idPersona;; 
                ";


                // Usar `using` para asegurar la correcta liberación de recursos
                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {

                    // Crear un DataTable y llenar los datos
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    // Asignar el DataTable al DataGridView
                    dataGridView1.DataSource = tabla;

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
        private void VentasListado_Load(object sender, EventArgs e)
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

        private void VentasListado_Resize(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasHistorialdeVentas(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasNuevaVenta(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MenuAtencionaCliente(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new VentasVentanadePago(parentForm)); // Pasamos la referencia de Form1 a
        }
    }
}
