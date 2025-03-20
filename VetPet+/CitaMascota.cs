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
    public partial class CitaMascota : FormPadre
    {
        public static string formularioAnterior; // Guarda el nombre del formulario anterior

        public CitaMascota()
        {
            InitializeComponent();

        }

        public CitaMascota(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            CargarDatos();
        }

        private void CitaMascota_Load(object sender, EventArgs e)
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
                m.nombre AS Nombre,
                e.nombre AS NombreEspecie,
                r.nombre AS NombreRaza,
                m.sexo AS nombreSexo
                FROM Mascota m
                JOIN Especie e ON m.idEspecie = e.idEspecie
                JOIN Raza r ON m.idRaza = r.idRaza;";

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
        public class Mascota
        {
            public string Nombre { get; set; }
            public string Especie { get; set; }
            public string Raza { get; set; }
            public string Sexo { get; set; }
        }

        public static Mascota MascotaSeleccionada { get; set; }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que el índice de la fila sea válido
            {
                // Capturar los datos seleccionados
                string nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                string especie = dataGridView1.Rows[e.RowIndex].Cells["NombreEspecie"].Value.ToString();
                string raza = dataGridView1.Rows[e.RowIndex].Cells["NombreRaza"].Value.ToString();
                string sexo = dataGridView1.Rows[e.RowIndex].Cells["nombreSexo"].Value.ToString();

                // Guardar los datos en variables estáticas o en una propiedad de CitaMascota
                CitaMascota.MascotaSeleccionada = new Mascota
                {
                    Nombre = nombre,
                    Especie = especie,
                    Raza = raza,
                    Sexo = sexo
                };

                // Cerrar el formulario actual y regresar a CitaAgendar
                this.Close();
            }
        }
    }
}
