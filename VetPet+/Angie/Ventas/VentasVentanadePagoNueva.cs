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
    public partial class VentasVentanadePagoNueva : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        Mismetodos mismetodos = new Mismetodos();

        int idVenta;
        public VentasVentanadePagoNueva(Form1 parent, int idVenta)
        {
            InitializeComponent();
            this.Load += VentasVentanadePagoNueva_Load;       // Evento Load
            this.Resize += VentasVentanadePagoNueva_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            this.idVenta = idVenta; 
            CargarVenta();
        }

        public void CargarVenta()
        {
            string query = @"
    SELECT 
        ISNULL(V.efectivo, 0) AS totalEfectivo,
        ISNULL(V.tarjeta, 0) AS totalTarjeta,
        V.total,
        P.nombre,
        P.apellidoP
    FROM Venta V
    LEFT JOIN Persona P ON V.idPersona = P.idPersona
    WHERE V.idVenta = @idVenta;";

            using (SqlConnection conexion = mismetodos.GetConexion())
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idVenta", idVenta);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox9.Text = reader["totalEfectivo"].ToString();
                                textBox10.Text = reader["totalTarjeta"].ToString();
                                textBox8.Text = "Subtotal: " + reader["total"].ToString();

                                textBox3.Text = reader["nombre"] != DBNull.Value ? reader["nombre"].ToString() : "";
                                textBox4.Text = reader["apellidoP"] != DBNull.Value ? reader["apellidoP"].ToString() : "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar venta: {ex.Message}");
                }
            }
        }

        private void VentasVentanadePagoNueva_Load(object sender, EventArgs e)
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

        private void VentasVentanadePagoNueva_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new VentasHistorialdeVentas(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ConsultarCita(parentForm)); // Pasamos la referencia de Form1 a 
        }
    }
}
