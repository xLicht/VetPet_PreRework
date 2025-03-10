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
                conexionDB.AbrirConexion();

                string insertarTipoEmpleadoQuery = @"
                    INSERT INTO TipoEmpleado (nombre) 
                    VALUES (@nombre);
                    SELECT SCOPE_IDENTITY();"; 

                int nuevoIdTipoEmpleado;

                using (SqlCommand comandoSQL = new SqlCommand(insertarTipoEmpleadoQuery, conexionDB.GetConexion()))
                {
                    comandoSQL.Parameters.AddWithValue("@nombre", txtNombre.Text);
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

                foreach (var modulo in modulosCheckBox)
                {
                    if (modulo.Value.Checked) 
                    {
                        string obtenerIdModuloQuery = "SELECT idModulo FROM Modulo WHERE nombre = @nombreModulo";
                        int? idModulo = null;

                        using (SqlCommand comandoSQL = new SqlCommand(obtenerIdModuloQuery, conexionDB.GetConexion()))
                        {
                            comandoSQL.Parameters.AddWithValue("@nombreModulo", modulo.Key);
                            object resultado = comandoSQL.ExecuteScalar();
                            if (resultado != null)
                            {
                                idModulo = Convert.ToInt32(resultado);
                            }
                        }

                        if (idModulo.HasValue) 
                        {
                            string insertarModuloQuery = @" INSERT INTO TipoEmpleado_Modulo (idTipoEmpleado, idModulo) 
                            VALUES (@idTipoEmpleado, @idModulo)";

                            using (SqlCommand comandoSQL = new SqlCommand(insertarModuloQuery, conexionDB.GetConexion()))
                            {
                                comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", nuevoIdTipoEmpleado);
                                comandoSQL.Parameters.AddWithValue("@idModulo", idModulo.Value);
                                comandoSQL.ExecuteNonQuery();
                            }
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
