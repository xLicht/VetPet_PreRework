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
    public partial class AgregarTipoPLab : FormPadre
    {
        int ServicioID;

        public AgregarTipoPLab()
        {
            InitializeComponent();
        }
        public AgregarTipoPLab(Form1 parent, int idS)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
            ServicioID = idS;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            int idServicio = conexion.ObtenerId("Estudios de Laboratorio", "ServicioPadre");
            parentForm.formularioHijo(new ListaPLab(parentForm, idServicio));
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;

            string nombreServicio = selectedRow["nombre"].ToString();

            string queryIdServicio = "SELECT idTipoEmpleado FROM TipoEmpleado WHERE nombre = @NombreServicio";

            // Crear el comando para obtener el idServicioEspecificoHijo
            using (SqlCommand cmd = new SqlCommand(queryIdServicio, conexion.GetConexion()))
            {
                try
                {
                    // Agregar el parámetro del nombre del ServicioEspecificoHijo
                    cmd.Parameters.AddWithValue("@NombreServicio", nombreServicio);

                    // Ejecutar la consulta y obtener el id
                    object result = cmd.ExecuteScalar(); // ExecuteScalar retorna el primer valor de la consulta (idServicioEspecificoHijo)

                    if (result != null)
                    {
                        // Convertir el resultado a int (si el id es entero)
                        int idTipoEmpleado = Convert.ToInt32(result);

                        conexion.GuardarTipoServicio(TxtNombre, richTextBox1, ServicioID, idTipoEmpleado);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el Servicio Especificado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }


        }

        private void AgregarTipoPLab_Load(object sender, EventArgs e)
        {
            cargarCombobox();
        }
        private void cargarCombobox()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT nombre FROM TipoEmpleado";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    // Crear un SqlDataAdapter con la conexión correcta
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Asignar el DataTable como fuente de datos
                    comboBox1.DataSource = dt;

                    // Asegúrate de que DisplayMember coincida con el nombre exacto de la columna en tu DataTable
                    comboBox1.DisplayMember = "nombre";  // Nombre de la columna que quieres mostrar en el ComboBox
                    comboBox1.ValueMember = "nombre";    // El valor del ComboBox será el mismo campo
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
    }
}
