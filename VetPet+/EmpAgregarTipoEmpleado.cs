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
    public partial class EmpAgregarTipoEmpleado : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        public EmpAgregarTipoEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpAgregarTipoEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            AgregarTipoEmpleado();
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }

        private void AgregarTipoEmpleado()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(rtDescripcion.Text))
                {
                    MessageBox.Show("Por favor, completa todos los campos antes de continuar.");
                    return;
                }

                conexionDB.AbrirConexion();

                string insertarTipoEmpleadoQuery = @"INSERT INTO TipoEmpleado (nombre, descripcion, estado) 
                VALUES (@nombre, @descripcion, @estado);
                SELECT SCOPE_IDENTITY();";

                int nuevoIdTipoEmpleado;

                using (SqlCommand comandoSQL = new SqlCommand(insertarTipoEmpleadoQuery, conexionDB.GetConexion()))
                {
                    comandoSQL.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    comandoSQL.Parameters.AddWithValue("@descripcion", rtDescripcion.Text);
                    comandoSQL.Parameters.AddWithValue("@estado", 'A');  
                    object resultado = comandoSQL.ExecuteScalar();
                    nuevoIdTipoEmpleado = Convert.ToInt32(resultado);
                }

                Dictionary<string, CheckBox> modulosCheckBox = new Dictionary<string, CheckBox>()
                {
                    { "Ventas", cbVentas },
                    { "Historiales Medicos", cbHistorialMedico },
                    { "Mascotas", cbMascotas },
                    { "Dueños", cbDueños },
                    { "Citas", cbCitas },
                    { "Citas Medicas", cbCitasMedicas },
                    { "Medicamentos", cbMedicamentos },
                    { "Servicios", cbServicios },
                    { "Cortes", cbCortes },
                    { "Proveedores", cbProvedores },
                    { "Pedidos", cbPedidos },
                    { "Productos", cbProductos },
                    { "Empleados", cbEmpleados }
                };

                string obtenerIdModulosQuery = "SELECT idModulo, nombre FROM Modulo";
                Dictionary<string, int> modulosExistentes = new Dictionary<string, int>();

                using (SqlCommand comandoSQL = new SqlCommand(obtenerIdModulosQuery, conexionDB.GetConexion()))
                {
                    using (SqlDataReader reader = comandoSQL.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            modulosExistentes[reader["nombre"].ToString()] = Convert.ToInt32(reader["idModulo"]);
                        }
                    }
                }
                foreach (var modulo in modulosCheckBox)
                {
                    if (modulo.Value.Checked && modulosExistentes.ContainsKey(modulo.Key))
                    {
                        int idModulo = modulosExistentes[modulo.Key];

                        string insertarModuloQuery = @"INSERT INTO TipoEmpleado_Modulo (idTipoEmpleado, idModulo) 
                        VALUES (@idTipoEmpleado, @idModulo)";

                        using (SqlCommand comandoSQL = new SqlCommand(insertarModuloQuery, conexionDB.GetConexion()))
                        {
                            comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", nuevoIdTipoEmpleado);
                            comandoSQL.Parameters.AddWithValue("@idModulo", idModulo);
                            comandoSQL.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Nuevo Tipo de Empleado agregado correctamente.");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al agregar el tipo de empleado: " + error.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
    }
}
