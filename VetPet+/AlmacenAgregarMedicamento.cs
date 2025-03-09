using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class AlmacenAgregarMedicamento : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        public string ProveedorSeleccionado { get; set; }
        private AlmacenAgregarMedicamento formMedicamento;


        public AlmacenAgregarMedicamento()
        {
            InitializeComponent();
            this.Load += AlmacenAgregarMedicamento_Load;
            this.Resize += AlmacenAgregarMedicamento_Resize;
        }

        public AlmacenAgregarMedicamento(Form1 parent, string proveedor = null)
        {
            InitializeComponent();
            parentForm = parent;
            ProveedorSeleccionado = proveedor;
        }

        private void AlmacenAgregarMedicamento_Load(object sender, EventArgs e)
        {
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }

            txtProveedor.Text = ProveedorSeleccionado;
        }

        private void AlmacenAgregarMedicamento_Resize(object sender, EventArgs e)
        {
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in this.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];
                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);
                    control.Font = new Font(control.Font.FontFamily, info.fontSize * Math.Min(scaleX, scaleY));
                }
            }
        }

        private void btnElegir_Click(object sender, EventArgs e)
        {
            AlmacenProveedor proveedorForm = new AlmacenProveedor(parentForm, this);
            proveedorForm.VieneDeAgregarMedicamento = true;
            parentForm.formularioHijo(proveedorForm);
        }

        public void SetProveedorSeleccionado(string proveedor)
        {
            txtProveedor.Text = proveedor;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Crear una instancia de la clase conexionBrandon
            conexionBrandon conexion = new conexionBrandon();

            // Abrir la conexión usando el método de la clase conexionBrandon
            conexion.AbrirConexion();

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioVenta.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioProveedor.Text) ||
                string.IsNullOrWhiteSpace(txtDosis.Text) ||
                string.IsNullOrWhiteSpace(cmbPresentacion.Text) ||
                string.IsNullOrWhiteSpace(cmbViaAdministracion.Text) ||
                string.IsNullOrWhiteSpace(txtLaboratorio.Text) ||
                string.IsNullOrWhiteSpace(txtMarca.Text) ||
                string.IsNullOrWhiteSpace(txtProveedor.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta) ||
                !decimal.TryParse(txtPrecioProveedor.Text, out decimal precioProveedor))
            {
                MessageBox.Show("Los precios deben ser valores numéricos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "INSERT INTO Medicamentos (Nombre, PrecioVenta, PrecioProveedor, Dosis, ViaAdministracion, Marca, Laboratorio, Presentacion, Proveedor, Descripcion) " +
                           "VALUES (@Nombre, @PrecioVenta, @PrecioProveedor, @Dosis, @ViaAdministracion, @Marca, @Laboratorio, @Presentacion, @Proveedor, @Descripcion)";

            // Usa la conexión ya abierta de conexionBrandon
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // No es necesario abrir de nuevo la conexión aquí, ya está abierta en conexionBrandon
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                    cmd.Parameters.AddWithValue("@PrecioProveedor", precioProveedor);
                    cmd.Parameters.AddWithValue("@Dosis", txtDosis.Text);
                    cmd.Parameters.AddWithValue("@ViaAdministracion", cmbViaAdministracion.Text);
                    cmd.Parameters.AddWithValue("@Marca", txtPrecioProveedor.Text);
                    cmd.Parameters.AddWithValue("@Laboratorio", txtLaboratorio.Text);
                    cmd.Parameters.AddWithValue("@Presentacion", cmbPresentacion.Text);
                    cmd.Parameters.AddWithValue("@Proveedor", txtProveedor.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Medicamento agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm));
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el medicamento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Cerrar la conexión una vez terminado el trabajo
                    conexion.CerrarConexion();
                }
            }
        }

    }
}
