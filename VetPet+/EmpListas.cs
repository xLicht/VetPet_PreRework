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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace VetPet_
{
    public partial class EmpListas : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        public EmpListas(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            CargarDatos();
        }

        private void EmpListas_Load(object sender, EventArgs e)
        {

        }

        private void CargarDatos()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = $@"
            SELECT e.idEmpleado, e.rfc, p.nombre, p.apellidoP, p.apellidoM, 
                   t.nombre AS tipoEmpleado, p.celularPrincipal
            FROM Empleado e
            JOIN Persona p ON e.idPersona = p.idPersona
            JOIN TipoEmpleado t ON e.idTipoEmpleado = t.idTipoEmpleado
            ORDER BY idEmpleado;";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtEmpleados.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dtEmpleados.Rows.Add(
                            row["idEmpleado"],
                            row["nombre"],
                            row["apellidoP"],
                            row["apellidoM"],
                            row["tipoEmpleado"],
                            row["celularPrincipal"],
                            row["rfc"]
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se pudo conectar a la BD. " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
        }

        private void pRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
        }
    }
}
