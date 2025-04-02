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
    public partial class VentasHistorialdeVentas : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();
        private Form1 parentForm;
        public int idVenta;

        public VentasHistorialdeVentas(Form1 parent)
        {
            InitializeComponent();
            this.Load += VentasHistorialdeVentas_Load;       // Evento Load
            this.Resize += VentasHistorialdeVentas_Resize;   // Event
            PersonalizarDataGridView();
            parentForm = parent;  // Guardamos la referencia de Form1
            CargarVentas();
            PersonalizarDataGridView();
        }

        public void CargarVentas()
        {
            try
            {
                mismetodos = new Mismetodos();
                mismetodos.AbrirConexion();

                string query = @"
        SELECT 
            V.idVenta,
            V.fechaRegistro, 
            V.total, 
            P.nombre AS Cliente
        FROM 
            Venta V
        LEFT JOIN 
            Persona P ON V.idPersona = P.idPersona;";

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    dataGridView1.DataSource = tabla;

                    // Configurar nombres de columnas
                    dataGridView1.Columns["fechaRegistro"].HeaderText = "Fecha";
                    dataGridView1.Columns["total"].HeaderText = "Total";
                    dataGridView1.Columns["Cliente"].HeaderText = "Cliente";

                    // Ocultar columna idVenta (opcional)
                    dataGridView1.Columns["idVenta"].Visible = false;
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

        private void VentasHistorialdeVentas_Load(object sender, EventArgs e)
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

        private void VentasHistorialdeVentas_Resize(object sender, EventArgs e)
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
        public void PersonalizarDataGridView()
        {
            dataGridView1.BorderStyle = BorderStyle.None; // Elimina bordes
            dataGridView1.BackgroundColor = Color.White; // Fondo blanco

            // Configurar fuente más grande para las celdas
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular); // Tamaño 12

            // Aumentar el alto de las filas para que el texto sea legible
            dataGridView1.RowTemplate.Height = 30; // Altura de fila aumentada

            // Alternar colores de filas
            dataGridView1.DefaultCellStyle.BackColor = Color.White;

            // Color de la selección
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Pink;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Encabezados más elegantes
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightPink;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold); // Tamaño aumentado a 14

            // Bordes y alineación
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.ColumnHeadersHeight = 40;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasListado(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                idVenta = Convert.ToInt32(fila.Cells["idVenta"].Value);

            }
            parentForm.formularioHijo(new VentasVentanadePagoNueva(parentForm,idVenta)); // Pasamos la referencia de Form1 a 
        }

    }
}
