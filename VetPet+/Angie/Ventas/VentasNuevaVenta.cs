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
    public partial class VentasNuevaVenta : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        Mismetodos mismetodos = new Mismetodos();
        private Form1 parentForm;
        private static DataTable dtProductos = new DataTable();
        decimal sumaTotalProductos = 0;
        decimal nuevoSubtotal = 0;
        decimal montoPagado = 0;

        private int idCita;
        private decimal subtotal;
        private int stock;
        public VentasNuevaVenta(Form1 parent)
        {
            InitializeComponent();
            this.Load += VentasNuevaVenta_Load;       // Evento Load
            this.Resize += VentasNuevaVenta_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
        }
        public VentasNuevaVenta(Form1 parent, decimal nuevoSubtotal, decimal montoPagado, DataTable dt)
        {
            InitializeComponent();
            this.Load += VentasNuevaVenta_Load;       // Evento Load
            this.Resize += VentasNuevaVenta_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            this.nuevoSubtotal = nuevoSubtotal;
            this.montoPagado = montoPagado;

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
        public VentasNuevaVenta(Form1 parent, decimal total, DataTable dt)
        {
            InitializeComponent();
            this.Load += VentasNuevaVenta_Load;
            this.Resize += VentasNuevaVenta_Resize;
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
            sumaTotalProductos = dtProductos.AsEnumerable()
                .Where(r => r["Total"] != DBNull.Value)
                .Sum(r => r.Field<decimal>("Total"));

            textBox8.Text = "Subtotal: " + sumaTotalProductos.ToString("0.###");
            textBox2.Text = "Monto restante: " + nuevoSubtotal.ToString("0.###");
            textBox9.Text = montoPagado.ToString();
        }
        private void VentasNuevaVenta_Load(object sender, EventArgs e)
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

        private void VentasNuevaVenta_Resize(object sender, EventArgs e)
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
        private void textBox11_Click(object sender, EventArgs e)
        {
            VentasAgregarMedicamento ventasAgregarMedicamento = new VentasAgregarMedicamento(parentForm);
            ventasAgregarMedicamento.FormularioOrigen = "VentasNuevaVenta"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(ventasAgregarMedicamento); // Usar la misma instancia
        }


        private void textBox12_Click(object sender, EventArgs e)
        {
            VentasAgregarProducto VentasAgregarProducto = new VentasAgregarProducto(parentForm,idCita,sumaTotalProductos,stock);
            VentasAgregarProducto.FormularioOrigen = "VentasNuevaVenta"; // Asignar FormularioOri
            parentForm.formularioHijo(VentasAgregarProducto); // Pasamos la referencia de Form1 a 
        }

        private void textBox13_Click(object sender, EventArgs e)
        {
            VentasConfirmacionEfectivo VentasConfirmacionEfectivo = new VentasConfirmacionEfectivo(parentForm, sumaTotalProductos, dtProductos);
            VentasConfirmacionEfectivo.FormularioOrigen = "VentasNuevaVenta"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionEfectivo); // Usar la misma instancia
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            VentasConfirmacionTarjeta VentasConfirmacionTarjeta = new VentasConfirmacionTarjeta(parentForm, sumaTotalProductos);
            VentasConfirmacionTarjeta.FormularioOrigen = "VentasNuevaVenta"; // Asignar FormularioOrigen a la instancia correcta
            parentForm.formularioHijo(VentasConfirmacionTarjeta); // Usar la misma instancia
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VentasListado(parentForm)); // Pasamos la referencia de Form1 a 
        }


    }
}
