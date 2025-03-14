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
    public partial class CitaEnlistado : FormPadre
    {
        public CitaEnlistado()
        {
            InitializeComponent();
        }

        public CitaEnlistado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            CargarDatosCita();
        }
         
        private void CitaEnlistado_Load(object sender, EventArgs e)
        {
             
        }

        private void CargarDatosCita()
        {
            try
            {

                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                // Definir la consulta
                string query = @"Select 
	                p.nombre AS nombre,
	                p.apellidoP AS apellidoP,
	                c.numero AS numero,
	                m.nombre AS nombreMascota,
	                ci.fechaProgramada AS fechaProgramada
                FROM Persona p
                JOIN Celular c ON p.idPersona = c.idPersona
                JOIN Mascota m ON p.idPersona = m.idPersona
                JOIN Cita ci ON m.idMascota = ci.idMascota;";

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
            parentForm.formularioHijo(new MenuAtencionaCliente(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void btnAgendarCita_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgendar(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new CitaConsultarCita(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void txtProducto_Enter(object sender, EventArgs e)
        {
            if (txtProducto.Text == "Buscar nombre de dueño") // Si el texto predeterminado está presente
            {
                txtProducto.Text = ""; // Limpia el TextBox
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el texto del TextBox (nombre del medicamento a buscar)
            string nombreDueño = txtProducto.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrEmpty(nombreDueño))
            {
                MessageBox.Show("Por favor, ingrese un nombre de dueño para buscar.");
                return;
            }

            try
            {
                // Crear una instancia de la clase conexionBrandon
                conexionBrandon conexion = new conexionBrandon();

                // Abrir la conexión
                conexion.AbrirConexion();

                string query = @"Select 
	                p.nombre AS nombre,
	                p.apellidoP AS apellidoP,
	                c.numero AS numero,
	                m.nombre AS nombreMascota,
	                ci.fechaProgramada AS fechaProgramada
                FROM Persona p
                JOIN Celular c ON p.idPersona = c.idPersona
                JOIN Mascota m ON p.idPersona = m.idPersona
                JOIN Cita ci ON m.idMascota = ci.idMascota
                WHERE p.nombre LIKE @nombreDueño"; // Usar LIKE para hacer la búsqueda

                // Crear un SqlDataAdapter usando la conexión obtenida de la clase conexionBrandon
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.GetConexion());

                // Agregar el parámetro para la búsqueda
                da.SelectCommand.Parameters.AddWithValue("@nombreDueño", "%" + nombreDueño + "%");

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
    }
}
