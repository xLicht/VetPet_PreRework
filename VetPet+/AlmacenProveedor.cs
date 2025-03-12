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
    public partial class AlmacenProveedor : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;

        //Agregar Medicamento
        public string ProveedorSeleccionado { get; set; }
        public bool VieneDeAgregarMedicamento { get; set; }

          //Agregar medicamento
        private AlmacenAgregarMedicamento formMedicamento; // Nueva variable para almacenar la referencia

        public AlmacenProveedor()
        {
            InitializeComponent();
            this.Load += AlmacenProveedor_Load;       // Evento Load
            this.Resize += AlmacenProveedor_Resize;   // Evento Resize
        }
        public AlmacenProveedor(Form1 parent, AlmacenAgregarMedicamento formMedicamento = null)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1

            this.formMedicamento = formMedicamento;
            CargarDatos();
        }
        //CONEXION
        private void CargarDatos()
        {
            try
            {

                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Definir la consulta
                string query = @"
                SELECT 
                    p.Nombre, 
                    (SELECT TOP 1 c.Numero 
                     FROM Celular c 
                     WHERE c.idProveedor = p.IdProveedor 
                     ORDER BY c.idProveedor ASC) AS Celular,
                    ISNULL(e.Nombre, 'Sin estado') AS Estado
                FROM 
                    Proveedor p
                LEFT JOIN 
                    Direccion d ON p.IdProveedor = d.IdProveedor
                LEFT JOIN 
                    Estado e ON d.IdEstado = e.IdEstado;";


                // Crear un SqlDataAdapter usando la conexión obtenida de la clase conexionBrandon
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());
                DataTable dt = new DataTable();

                // Llenar el DataTable con los resultados de la consulta
                da.Fill(dt);

                // Asignar el DataTable al DataGridView
                dataGridView1.DataSource = dt;

                // Asegurarse de que las columnas se generen correctamente
                dataGridView1.AutoGenerateColumns = true; // Esta propiedad debería estar en true por defecto


                // Cerrar la conexión
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void txtProveedor_Enter(object sender, EventArgs e)
        {
            // Limpia el contenido cuando el usuario hace clic en el TextBox
            if (txtProveedor.Text == "Buscar nombre de proveedor") // Si el texto predeterminado está presente
            {
                txtProveedor.Text = ""; // Limpia el TextBox
            }
        }

        private void AlmacenProveedor_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }

            // Si hay un proveedor seleccionado, actualizamos el TextBox
            if (!string.IsNullOrEmpty(ProveedorSeleccionado))
            {
                txtProveedor.Text = ProveedorSeleccionado;
            }
        }

        private void AlmacenProveedor_Resize(object sender, EventArgs e)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenAgregarProveedor(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AlmacenMenu(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombre = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();

                // Establecer el proveedor seleccionado
                ProveedorSeleccionado = nombre;

                // Si viene desde AlmacenAgregarMedicamento, regresa sin hacer más
                if (VieneDeAgregarMedicamento)
                {
                    parentForm.formularioHijo(new AlmacenAgregarMedicamento(parentForm, nombre)); // Pasamos el proveedor seleccionado
                }
                else
                {
                    // Llamar al formulario de opciones
                    using (var opcionesForm = new AlmacenAvisoVerOModificar(nombre, parentForm))
                    {
                        if (opcionesForm.ShowDialog() == DialogResult.OK)
                        {
                            if (opcionesForm.Resultado == "Modificar")
                            {
                                parentForm.formularioHijo(new AlmacenModificarProveedor(parentForm, nombre)); // Pasamos la referencia de Form1 a 
                            }
                            if (opcionesForm.Resultado == "Salir")
                            {
                                parentForm.formularioHijo(new AlmacenProveedor(parentForm)); // Pasamos la referencia de Form1 a 
                            }
                            else if (opcionesForm.Resultado == "Ver")
                            {
                                parentForm.formularioHijo(new AlmacenVerProveedor(parentForm)); // Pasamos la referencia de Form1 a 
                            }
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void FiltrarProveedoresPorFecha(DateTime fechaSeleccionada)
        {
            try
            {
                conexionBrandon conexion = new conexionBrandon();
                conexion.AbrirConexion();

                string query = @"SELECT 
                            p.Nombre, 
                            p.Celular, 
                            e.nombre
                        FROM 
                            Proveedor p
                        JOIN 
                            Direccion d ON p.IdProveedor = d.IdProveedor
                        JOIN 
                            Estado e ON d.IdEstado = e.IdEstado
                        WHERE 
                            CAST(p.FechaRegistro AS DATE) = @FechaSeleccionada;";

                SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());
                da.SelectCommand.Parameters.AddWithValue("@FechaSeleccionada", fechaSeleccionada.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoGenerateColumns = true;

                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar proveedores: " + ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FiltrarProveedoresPorFecha(dateTimePicker1.Value);
        }
    }
}
