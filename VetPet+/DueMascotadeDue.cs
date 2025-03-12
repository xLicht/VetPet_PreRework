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
    public partial class DueMascotadeDue : FormPadre
    {
        public int DatoEmpleado { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public DueMascotadeDue(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;

        }

        private void DueMascotadeDue_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("dato: "+DatoEmpleado);
            CargarDatos();
        }


        private void CargarDatos()
        {
            try
            {
                conexionDB.AbrirConexion();

                string ordenColumna = "nombre";
                if (cbFiltrar.SelectedItem != null)
                {
                    switch (cbFiltrar.SelectedItem.ToString())
                    {
                        case "Peso":
                            ordenColumna = "peso";
                            break;
                        case "Fecha de Nacimiento":
                            ordenColumna = "fechaNacimiento";
                            break;
                    }
                }

                string query = $@"
                    SELECT 
                        m.idMascota, 
                        m.nombre, 
                        r.nombre AS Raza,
                        e.nombre AS Especie,
                        m.esterilizado, 
                        m.muerto, 
                        m.peso, 
                        m.fechaNacimiento, 
                        m.sexo
                    FROM Mascota m
                    INNER JOIN Raza r ON m.idRaza = r.idRaza
                    INNER JOIN Especie e ON m.idEspecie = e.idEspecie
                    WHERE m.estado = 'A' AND m.idPersona = @idPersona
                    ORDER BY {ordenColumna};";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtMascotas.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dtMascotas.Rows.Add(
                            row["idMascota"], row["nombre"], row["Raza"], row["Especie"],
                            row["esterilizado"], row["muerto"], row["peso"],
                            row["fechaNacimiento"], row["sexo"]
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

        private void btnAgregarMascota_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueAgregarMascota(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new DueConsultarMascota(parentForm));
        }
    } 
}
