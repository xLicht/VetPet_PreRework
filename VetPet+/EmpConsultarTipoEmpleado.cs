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
    public partial class EmpConsultarTipoEmpleado : FormPadre
    {
        public int DatoEmpleado { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public EmpConsultarTipoEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;

            cbVentas.AutoCheck = false;
            cbHistorialMedico.AutoCheck = false;
            cbMascotas.AutoCheck = false;
            cbDueños.AutoCheck = false;
            cbCitas.AutoCheck = false;
            cbCitasMedicas.AutoCheck = false;
            cbMedicamentos.AutoCheck = false;
            cbServicios.AutoCheck = false;
            cbCortes.AutoCheck = false;
            cbProvedores.AutoCheck = false;
            cbPedidos.AutoCheck = false;    
            cbProductos.AutoCheck = false;  
            cbEmpleados.AutoCheck = false;  

        }

        private void EmpConsultarTipoEmpleado_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Dato Recibido: " + DatoEmpleado);
            CargarModulosTipoEmpleado();
        }


        private void CargarModulosTipoEmpleado()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"
                    SELECT Modulo.nombre 
                    FROM TipoEmpleado_Modulo
                    INNER JOIN Modulo ON TipoEmpleado_Modulo.idModulo = Modulo.idModulo
                    WHERE TipoEmpleado_Modulo.idTipoEmpleado = @idTipoEmpleado";

                using (SqlCommand comandoSQL = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", DatoEmpleado);
                    SqlDataReader lectorSQL = comandoSQL.ExecuteReader();

                    cbVentas.Checked = false;
                    cbHistorialMedico.Checked = false;
                    cbMascotas.Checked = false;
                    cbDueños.Checked = false;
                    cbCitas.Checked = false;
                    cbCitasMedicas.Checked = false;
                    cbMedicamentos.Checked = false;
                    cbServicios.Checked = false;
                    cbCortes.Checked = false;
                    cbProvedores.Checked = false;
                    cbPedidos.Checked = false;
                    cbProductos.Checked = false;
                    cbEmpleados.Checked = false;

                    while (lectorSQL.Read())
                    {
                        string nombreModulo = lectorSQL["nombre"].ToString();

                        if (nombreModulo == "Ventas")
                        {
                            cbVentas.Checked = true;
                        }
                        else if (nombreModulo == "Historiales Medicos")
                        {
                            cbHistorialMedico.Checked = true;
                        }
                        else if (nombreModulo == "Mascotas")
                        {
                            cbMascotas.Checked = true;
                        }
                        else if (nombreModulo == "Dueños")
                        {
                            cbDueños.Checked = true;
                        }
                        else if (nombreModulo == "Citas")
                        {
                            cbCitas.Checked = true;
                        }
                        else if (nombreModulo == "Citas Medicas")
                        {
                            cbCitasMedicas.Checked = true;
                        }
                        else if (nombreModulo == "Medicamentos")
                        {
                            cbMedicamentos.Checked = true;
                        }
                        else if (nombreModulo == "Servicios")
                        {
                            cbServicios.Checked = true;
                        }
                        else if (nombreModulo == "Cortes")
                        {
                            cbCortes.Checked = true;
                        }
                        else if (nombreModulo == "Proveedores")
                        {
                            cbProvedores.Checked = true;
                        }
                        else if (nombreModulo == "Pedidos")
                        {
                            cbPedidos.Checked = true;
                        }
                        else if (nombreModulo == "Productos")
                        {
                            cbProductos.Checked = true;
                        }
                        else if (nombreModulo == "Empleados")
                        {
                            cbEmpleados.Checked = true;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al obtener los módulos del tipo de empleado: " + error.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpModificarTipoEmpleado(parentForm));
        }

        private void r_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }

        private void m_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpModificarTipoEmpleado(parentForm));
        }

        private void e_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpTiposEmpleados(parentForm));
        }
    }
}
