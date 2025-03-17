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
            parentForm.formularioHijo(new ConsultarCita(parentForm));
        }

        private void CitasMedicas_Load_1(object sender, EventArgs e)
        {

        }

        private void CargarDatos()
        {
            try
            {
                conexionDB.AbrirConexion();

                string ordenColumna = "c.idCita";
                if (cbFiltrar.SelectedItem != null)
                {
                    switch (cbFiltrar.SelectedItem.ToString())
                    {
                        case "Fecha":
                            ordenColumna = "c.fechaCita";
                            break;
                        case "Mascota":
                            ordenColumna = "m.nombre";
                            break;
                        case "Dueño":
                            ordenColumna = "p.nombre";
                            break;
                    }
                }

                string filtro = txtBuscar.Text;

                string query = $@"
                    SELECT c.idCita, c.fechaCita, c.horaCita, m.nombre AS Mascota, 
                           p.nombre AS Dueño, v.nombre AS Veterinario, c.estado
                    FROM Cita c
                    JOIN Mascota m ON c.idMascota = m.idMascota
                    JOIN Persona p ON m.idPersona = p.idPersona
                    JOIN Empleado e ON c.idVeterinario = e.idEmpleado
                    JOIN Persona v ON e.idPersona = v.idPersona
                    WHERE (m.nombre LIKE @filtro OR p.nombre LIKE @filtro OR c.fechaCita LIKE @filtro)
                    ORDER BY {ordenColumna};";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtCitasMedicas.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dtCitasMedicas.Rows.Add(row["idCita"], row["fechaCita"], row["horaCita"],
                                                row["Mascota"], row["Dueño"], row["Veterinario"], row["estado"]);
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
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = dtCitasMedicas.Rows[e.RowIndex];
            //    if (row.Cells[0].Value != null)
            //    {
            //        int idCitaSeleccionada = Convert.ToInt32(row.Cells[0].Value);
            //        ConsultarCita formularioHijo = new ConsultarCita(parentForm);
            //        formularioHijo.DatoCita = idCitaSeleccionada;
            //        parentForm.formularioHijo(formularioHijo);
            //        formularioHijo.MostrarDato();
            //    }
            //    else
            //    {
            //        MessageBox.Show("No se pudo obtener el ID de la cita.");
            //    }
            //}
        }
    }
   
}
