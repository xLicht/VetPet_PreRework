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

namespace VetPet_
{
    public partial class AlmacenRecibirPedido : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;

        public AlmacenRecibirPedido()
        {
            InitializeComponent();
            this.Load += AlmacenRecibirPedido_Load;       // Evento Load
            this.Resize += AlmacenRecibirPedido_Resize;   // Evento Resize
        }

        public AlmacenRecibirPedido(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
            CargarCombos();
        }


        private void AlmacenRecibirPedido_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }

            // Inicializar los TextBox de ID con "NULL"
            txtIdProveedor.Text = "NULL";
            txtIdProducto.Text = "NULL";
            txtIdMedicamento.Text = "NULL";

            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedicamento.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void CargarCombos()
        {
            conexionBrandon conexion = new conexionBrandon();
            try
            {
                this.cmbProveedor.SelectedIndexChanged += new System.EventHandler(this.cmbProveedor_SelectedIndexChanged);
                this.cmbProducto.SelectedIndexChanged += new System.EventHandler(this.cmbProducto_SelectedIndexChanged);
                this.cmbMedicamento.SelectedIndexChanged += new System.EventHandler(this.cmbMedicamento_SelectedIndexChanged);

                conexion.AbrirConexion();

                // Cargar Proveedores
                string queryProveedor = "SELECT idProveedor, nombre FROM Proveedor";
                SqlDataAdapter daProveedor = new SqlDataAdapter(queryProveedor, conexion.GetConexion());
                DataTable dtProveedor = new DataTable();
                daProveedor.Fill(dtProveedor);

                cmbProveedor.DataSource = dtProveedor;
                cmbProveedor.DisplayMember = "nombre";  // Se muestra el nombre
                cmbProveedor.ValueMember = "idProveedor"; // Se guarda el ID en SelectedValue

                // Cargar Productos
                string queryProducto = "SELECT idProducto, nombre FROM Producto";
                SqlDataAdapter daProducto = new SqlDataAdapter(queryProducto, conexion.GetConexion());
                DataTable dtProducto = new DataTable();
                daProducto.Fill(dtProducto);

                cmbProducto.DataSource = dtProducto;
                cmbProducto.DisplayMember = "nombre";
                cmbProducto.ValueMember = "idProducto";

                // Cargar Medicamentos
                string queryMedicamento = "SELECT idMedicamento, nombreGenérico FROM Medicamento";
                SqlDataAdapter daMedicamento = new SqlDataAdapter(queryMedicamento, conexion.GetConexion());
                DataTable dtMedicamento = new DataTable();
                daMedicamento.Fill(dtMedicamento);

                cmbMedicamento.DataSource = dtMedicamento;
                cmbMedicamento.DisplayMember = "nombreGenérico";
                cmbMedicamento.ValueMember = "idMedicamento";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }



        private void AlmacenRecibirPedido_Resize(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conexionBrandon conexion = new conexionBrandon();
            try
            {
                // Verificar si ambos campos son "NULL"
                if (txtIdProducto.Text == "NULL" && txtIdMedicamento.Text == "NULL")
                {
                    MessageBox.Show("Debes seleccionar al menos un Producto o un Medicamento para agregar el pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método sin guardar el pedido
                }

                conexion.AbrirConexion();

                // Verificar si el texto en los TextBox es "NULL" y asignar DBNull.Value si es así
                object idProducto = txtIdProducto.Text == "NULL" ? DBNull.Value : (object)int.Parse(txtIdProducto.Text);
                object idMedicamento = txtIdMedicamento.Text == "NULL" ? DBNull.Value : (object)int.Parse(txtIdMedicamento.Text);

                string query = "INSERT INTO Pedido (numFactura, fechaRecibido, cantidad, total, idProducto, idMedicamento, idProveedor) " +
                               "VALUES (@Factura, @FechaRecibido, @Cantidad, @Total, @IdProducto, @IdMedicamento, @IdProveedor)";

                SqlCommand cmd = new SqlCommand(query, conexion.GetConexion());
                cmd.Parameters.AddWithValue("@Factura", txtFactura.Text);

                // Usar la fecha seleccionada en el DateTimePicker
                cmd.Parameters.AddWithValue("@FechaRecibido", fechaRecibidoPicker.Value);  // Aquí tomas el valor de DateTimePicker
                cmd.Parameters.AddWithValue("@Cantidad", int.Parse(txtCantidad.Text));
                cmd.Parameters.AddWithValue("@Total", decimal.Parse(txtTotal.Text));
                cmd.Parameters.AddWithValue("@IdProveedor", int.Parse(txtIdProveedor.Text));
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                cmd.Parameters.AddWithValue("@IdMedicamento", idMedicamento);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Pedido agregado correctamente.");

                // Preguntar si desea agregar otro pedido
                DialogResult resultado = MessageBox.Show("¿Quieres agregar otro pedido?", "Confirmación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.No)
                {
                    parentForm.formularioHijo(new AlmacenMenu(parentForm));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el pedido: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }



        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenMenu(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void checkboxMedicamento_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxMedicamento.Checked)
            {
                cmbProducto.Text = "NULL";
                txtIdProducto.Text = "NULL";
                checkboxProducto.Checked = false;
                cmbMedicamento.Enabled = true;
                cmbProducto.Enabled = false;
            }
            else
            {
                cmbMedicamento.Enabled = false;
            }
        }

        private void checkboxProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxProducto.Checked)
            {
                cmbMedicamento.Text = "NULL";
                txtIdMedicamento.Text = "NULL";
                checkboxMedicamento.Checked = false;
                cmbProducto.Enabled = true;
                cmbMedicamento.Enabled = false;
            }
            else
            {
                cmbProducto.Enabled = false;
            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue != null)
            {
                txtIdProveedor.Text = cmbProveedor.SelectedValue.ToString();
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue != null)
            {
                txtIdProducto.Text = cmbProducto.SelectedValue.ToString();
            }
        }

        private void cmbMedicamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMedicamento.SelectedValue != null)
            {
                txtIdMedicamento.Text = cmbMedicamento.SelectedValue.ToString();
            }
        }

        private void txtFactura_Enter(object sender, EventArgs e)
        {
            if (txtFactura.Text == "Factura") // Si el texto predeterminado está presente
            {
                txtFactura.Text = ""; // Limpia el TextBox
            }
        }


        private void txtCantidad_Enter(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "Cantidad") // Si el texto predeterminado está presente
            {
                txtCantidad.Text = ""; // Limpia el TextBox
            }
        }

        private void txtTotal_Enter(object sender, EventArgs e)
        {
            if (txtTotal.Text == "Total") // Si el texto predeterminado está presente
            {
                txtTotal.Text = ""; // Limpia el TextBox
            }
        }
    }
}
