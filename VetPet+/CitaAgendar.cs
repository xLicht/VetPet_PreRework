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
    public partial class CitaAgendar : FormPadre
    {
        public CitaAgendar()
        {
            InitializeComponent();
        }

        public CitaAgendar(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            CargarDatosCita();
        }

        private void CitaAgendar_Load(object sender, EventArgs e)
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
                string query = @"SELECT DISTINCT 
                    sp.nombre AS nombre,
                    cs.nombre AS nombreClase,
                    sen.precio AS precio,
                    e.usuario AS usuario    
                FROM ServicioPadre sp
                JOIN ClaseServicio cs ON sp.idClaseServicio = cs.idClaseServicio
                JOIN Empleado e ON sp.idTipoEmpleado = e.idTipoEmpleado
                LEFT JOIN ServicioSencilloHijo ssh ON sp.idServicioPadre = ssh.idServicioPadre
                LEFT JOIN ServicioEspecificoHijo seh ON sp.idServicioPadre = seh.idServicioPadre
                LEFT JOIN ServicioEspecificoNieto sen ON seh.idServicioEspecificoHijo = sen.idServicioEspecificoHijo
                WHERE e.idTipoEmpleado = 1;";

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
        private void btnMascota_Click(object sender, EventArgs e)
        {
            CitaMascota.formularioAnterior = "CitaAgendar";
            parentForm.formularioHijo(new CitaMascota(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnDueño_Click(object sender, EventArgs e)
        {
            CitaDueños.formularioAnterior = "CitaAgendar";
            parentForm.formularioHijo(new CitaDueños(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            CitaAgregarServicio.formularioAnterior = "CitaAgendar";
            parentForm.formularioHijo(new CitaAgregarServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaEnlistado(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaEnlistado(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void btnVeterinario_Click(object sender, EventArgs e)
        {
        }
    }
}
