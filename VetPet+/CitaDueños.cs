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
    public partial class CitaDueños : FormPadre
    {
        public static string formularioAnterior; // Guarda el nombre del formulario anterior
        public static int IdDueñoSeleccionado { get; set; }

        public CitaDueños()
        {
            InitializeComponent();
        }
        public CitaDueños(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            CargarDatos();
        }

        private void CitaDueños_Load(object sender, EventArgs e)
        {
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
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
                string query = @"SELECT 
                p.nombre AS Nombre,
                p.apellidoP AS NombreP,
                p.apellidoM AS NombreM,
                p.celularPrincipal AS Celular
                FROM Persona p;";

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
            if (formularioAnterior == "CitaAgendar")
            {
                parentForm.formularioHijo(new CitaAgendar(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
            }
            else if (formularioAnterior == "CitaModificarCita")
            {
                parentForm.formularioHijo(new CitaModificarCita(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
            }
        }
        public class Dueño
        {
            public string Nombre { get; set; }
            public string ApellidoP { get; set; }
            public string ApellidoM { get; set; }
            public string Celular { get; set; }
        }

        public static Dueño DueñoSeleccionado { get; set; }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que el índice de la fila sea válido
            {
                // Capturar los datos seleccionados
                string nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                string apellidoP = dataGridView1.Rows[e.RowIndex].Cells["NombreP"].Value.ToString();
                string apellidoM = dataGridView1.Rows[e.RowIndex].Cells["NombreM"].Value.ToString();
                string celular = dataGridView1.Rows[e.RowIndex].Cells["Celular"].Value.ToString();

                // Guardar los datos en variables estáticas o en una propiedad de CitaDueños
                CitaDueños.DueñoSeleccionado = new Dueño
                {
                    Nombre = nombre,
                    ApellidoP = apellidoP,
                    ApellidoM = apellidoM,
                    Celular = celular
                };

                // Cerrar el formulario actual y regresar a CitaAgendar
                this.Close();
            }
        }


    }
}
