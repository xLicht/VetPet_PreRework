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
    public partial class CitasMedicas : FormPadre
    {
        public int DatoCita { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public CitasMedicas(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            cbFiltrar.SelectedIndexChanged += (s, e) => CargarDatos();
            CargarDatos();
        }

        private void CitasMedicas_Load(object sender, EventArgs e)
        {

        }

        private void CitasMedicas_Resize(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaMenu(parentForm)); 
        }

        private void dtCitasMedicas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //parentForm.formularioHijo(new ConsultarCita(parentForm));
        }

        private void CitasMedicas_Load_1(object sender, EventArgs e)
        {

        }

        private void CargarDatos()
        {
            try
            {
                conexionDB.AbrirConexion();

                //    string query = @"
                //SELECT c.idCita, c.fechaRegistro, c.fechaProgramada, c.hora, c.duracion, 
                //       m.nombre AS NombreMascota, mo.nombre AS Motivo
                //FROM Cita c
                //INNER JOIN Mascota m ON c.idMascota = m.idMascota
                //INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
                //ORDER BY c.fechaProgramada;";

                string query = @"
                SELECT c.idCita, c.fechaRegistro, c.fechaProgramada, c.hora, c.duracion, 
                       m.nombre AS NombreMascota, mo.nombre AS Motivo
                FROM Cita c
                INNER JOIN Mascota m ON c.idMascota = m.idMascota
                INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
                WHERE c.estado <> 'I'
                ORDER BY c.fechaProgramada;";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtCitasMedicas.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dtCitasMedicas.Rows.Add(row["idCita"], row["fechaRegistro"], row["fechaProgramada"],
                                                row["hora"], row["duracion"], row["NombreMascota"], row["Motivo"]);
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

        private void dtCitasMedicas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtCitasMedicas.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    int idCitaSeleccionada = Convert.ToInt32(row.Cells[0].Value);
                    ConsultarCita formularioHijo = new ConsultarCita(parentForm);
                    formularioHijo.DatoCita = idCitaSeleccionada;
                    parentForm.formularioHijo(formularioHijo);
                    
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID de la cita.");
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgendarCita_Click(object sender, EventArgs e)
        {
           VeterinariaAgendarCita formularioHijo = new VeterinariaAgendarCita(parentForm);
            parentForm.formularioHijo(formularioHijo);
        }
    }
   
}
