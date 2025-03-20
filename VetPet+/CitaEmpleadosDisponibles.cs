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
using VetPet_;

namespace VetPet_
{
    public partial class CitaEmpleadosDisponibles : FormPadre
    {
        public static string formularioAnterior; // Guarda el nombre del formulario anterior

        public CitaEmpleadosDisponibles()
        {
            InitializeComponent();
        }
        public CitaEmpleadosDisponibles(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CitaEmpleadosDisponibles_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            try
            {

                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Definir la consulta
                string query = @"SELECT 
                p.nombre AS Nombre,
                p.apellidoP AS NombreP,
                p.apellidoM AS NombreM,
                p.celularPrincipal AS celular
                FROM Empleado e
                JOIN Persona p ON e.idPersona = p.idPersona
                JOIN TipoEmpleado tp ON e.idTipoEmpleado = tp.idTipoEmpleado
                WHERE e.idTipoEmpleado = '1' or e.idTipoEmpleado = '3';";

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
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Verifica de qué formulario vino
            if (formularioAnterior == "CitaGestionarServicio")
            {
                parentForm.formularioHijo(new CitaGestionarServicio(parentForm));

            }
            else if (formularioAnterior == "CitaAgregarServicio")
            {
                parentForm.formularioHijo(new CitaAgregarServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
            }

        }
        public static string PersonalSeleccionado = "";

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CitaEmpleadosDisponibles.PersonalSeleccionado = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();

                // Regresar a CitaAgregarServicio
                parentForm.formularioHijo(new CitaAgregarServicio(parentForm));
            }
        }
    }
}