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

    public partial class EmpTiposEmpleados : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        public int DatoEmpleado { get; set; }
        public EmpTiposEmpleados(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            CargarModulos();
        }

        private void EmpTiposEmpleados_Load(object sender, EventArgs e)
        {

        }
        private void CargarModulos()
        {
            try
            {
                conexionDB.AbrirConexion();

                //string query = @"
                //    SELECT te.idTipoEmpleado, te.nombre AS TipoEmpleado, m.nombre AS Modulo
                //    FROM TipoEmpleado te
                //    JOIN TipoEmpleado_Modulo tem ON te.idTipoEmpleado = tem.idTipoEmpleado
                //    JOIN Modulo m ON tem.idModulo = m.idModulo
                //    ORDER BY te.idTipoEmpleado, m.nombre;";

                string query = @"SELECT te.idTipoEmpleado, te.nombre AS TipoEmpleado, m.nombre AS Modulo
                FROM TipoEmpleado te
                JOIN TipoEmpleado_Modulo tem ON te.idTipoEmpleado = tem.idTipoEmpleado
                JOIN Modulo m ON tem.idModulo = m.idModulo
                WHERE te.estado = 'A' 
                ORDER BY te.idTipoEmpleado, m.nombre;";


                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtTipoEmpleado.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dtTipoEmpleado.Rows.Add(row["idTipoEmpleado"], row["TipoEmpleado"], row["Modulo"]);
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
            parentForm.formularioHijo(new EmpMenuEmpleados(parentForm));
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpAgregarTipoEmpleado(parentForm));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new EmpConsultarTipoEmpleado(parentForm));
        }

        private void r_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpMenuEmpleados(parentForm));
        }

        private void a_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpAgregarTipoEmpleado(parentForm));
        }

        private void dtTipoEmpleado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtTipoEmpleado.Rows[e.RowIndex];

                if (row.Cells[0].Value != null)
                {
                    int idEmpleadoSeleccionado = Convert.ToInt32(row.Cells[0].Value);
                    EmpConsultarTipoEmpleado formularioHijo = new EmpConsultarTipoEmpleado(parentForm);
                    formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
                    parentForm.formularioHijo(formularioHijo);
                    //formularioHijo.MostrarDato();
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID del empleado.");
                }
            }
        }
    }
}
