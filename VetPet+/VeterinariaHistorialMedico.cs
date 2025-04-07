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
    public partial class VeterinariaHistorialMedico : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        public VeterinariaHistorialMedico(Form1 parent)
        {
            InitializeComponent();
            //cbFliltrar.Text = "Filtrar";
            //parentForm = parent;
            cbFiltrar.SelectedIndexChanged += (s, e) => CargarDatos();
            parentForm = parent;
            CargarDatos();
        }

        private void VeterinariaHistorialMedico_Load(object sender, EventArgs e)
        {

        }

        private void VeterinariaHistorialMedico_Resize(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            //parentForm.formularioHijo(new VeterianiaGestionarHistorialM(parentForm)); 

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new VeterinariaMenu(parentForm)); 
        }

        private void VeterinariaHistorialMedico_Load_1(object sender, EventArgs e)
        {

        }
        private void CargarDatos()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"
                SELECT m.idMascota, m.nombre AS NombreMascota, e.nombre AS Especie, 
                       r.nombre AS Raza, p.nombre AS Dueño, m.fechaRegistro 
                FROM Mascota m
                INNER JOIN Especie e ON m.idEspecie = e.idEspecie
                INNER JOIN Raza r ON m.idRaza = r.idRaza
                INNER JOIN Persona p ON m.idPersona = p.idPersona
                ORDER BY m.fechaRegistro DESC;";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtHistorial.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dtHistorial.Rows.Add(row["idMascota"], row["NombreMascota"], row["Especie"],
                                               row["Raza"], row["Dueño"], row["fechaRegistro"]);
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

        private void dtHistorial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtHistorial.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    int idMascotaSeleccionada = Convert.ToInt32(row.Cells[0].Value);
                    VeterianiaGestionarHistorialM formularioHijo = new VeterianiaGestionarHistorialM(parentForm);
                    formularioHijo.DatoMascota = idMascotaSeleccionada;
                    parentForm.formularioHijo(formularioHijo);
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID de la mascota.");
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim();

            try
            {
                conexionDB.AbrirConexion();

                string query = @"
                SELECT m.idMascota, m.nombre AS NombreMascota, e.nombre AS Especie, 
                       r.nombre AS Raza, p.nombre AS Dueño, m.fechaRegistro 
                FROM Mascota m
                INNER JOIN Especie e ON m.idEspecie = e.idEspecie
                INNER JOIN Raza r ON m.idRaza = r.idRaza
                INNER JOIN Persona p ON m.idPersona = p.idPersona
                WHERE p.nombre LIKE @textoBusqueda
                ORDER BY m.fechaRegistro DESC;";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@textoBusqueda", "%" + textoBusqueda + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtHistorial.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dtHistorial.Rows.Add(row["idMascota"], row["NombreMascota"], row["Especie"],
                                               row["Raza"], row["Dueño"], row["fechaRegistro"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        //private void btnRegresar_Click(object sender, EventArgs e)
        //{
        //    parentForm.formularioHijo(new VeterinariaMenu(parentForm));
        //}
    }
}
    

