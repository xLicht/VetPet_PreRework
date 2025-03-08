using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using VetPet_;
using System.Data.SqlClient;

namespace VetPet_
{
    public partial class EmpListaEmpleados : FormPadre
    {
        private string connectionString = "Server=DESKTOP-7PPM2OB\\SQLEXPRESS;Database=VetPetPlus;Integrated Security=True;";
        public EmpListaEmpleados(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            //CargarDatos();
        }

        private void EmpListaEmpleados_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpAgregarEmpleado(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpMenuEmpleados(parentForm));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new EmpConsultarEmpleado(parentForm));
        }

        private void btnListaEmpleados_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpListas(parentForm));
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    string query = @"SELECT  e.idEmpleado,p.nombre, p.apellidoP,
                    p.apellidoM, t.nombre AS tipoEmpleado, p.Celular
                    FROM Empleado e
                    JOIN Persona p ON e.idPersona = p.idPersona
                    JOIN TipoEmpleado t ON e.idTipoEmpleado = t.idTipoEmpleado;";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    //dtEmpleados.DataSource = dt;
                    // Limpiar el DataGridView antes de cargar nuevos datos
                    dtEmpleados.Rows.Clear();

                    // Llenar manualmente cada fila con los valores de la consulta
                    foreach (DataRow row in dt.Rows)
                    {
                        dtEmpleados.Rows.Add(
                            row["idEmpleado"],
                            row["nombre"],
                            row["apellidoP"],
                            row["apellidoM"],
                            row["tipoEmpleado"],
                            row["celular"]
                        );
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se pudo Conectar la BD " + ex.Message);
                }
            }
        }
    }
}
