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
using VetPet_.Angie.Mascotas;

namespace VetPet_
{
    public partial class DueMascotadeDue : FormPadre
    {
        public string origen;
        public int DatoEmpleado { get; set; }
        public static int DatoEmpleadoGlobal { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public DueMascotadeDue(Form1 parent, string origen)
        {
            InitializeComponent();
            parentForm = parent;
            this.origen = origen;
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
            DatoEmpleadoGlobal = DatoEmpleado;

            parentForm.formularioHijo(new MascotasAgregarMascota(parentForm));

            //parentForm.formularioHijo(new DueAgregarMascota(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
           // parentForm.formularioHijo(new VeterinariaVentaMedicamentos(parentForm, "VeterinariaModificarReceta"));
            if (origen == "DueConsultarDue")
            {
                int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
                DueConsultarDueño formularioHijo = new DueConsultarDueño(parentForm);
                formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
                parentForm.formularioHijo(formularioHijo);
            }
            if (origen == "DueModificarDue")
            {
                int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
                DueModificarDueño formularioHijo = new DueModificarDueño(parentForm);
                formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
                parentForm.formularioHijo(formularioHijo);
            }

            //parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            parentForm.formularioHijo(new DueConsultarMascota(parentForm));
        }

        private void dtMascotas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtMascotas.Rows[e.RowIndex];

                if (row.Cells[0].Value != null)
                {


                    //Pantallas de La Angie 
                    int idMascota = Convert.ToInt32(row.Cells[0].Value);
                    string nombreMascota = Convert.ToString(row.Cells[1].Value);
                    //PASARLE EL ID DE DE DUEÑO 
                    //int idDueño = Convert.ToInt32(DatoEmpleado);
                    DatoEmpleadoGlobal = DatoEmpleado; // Guarda el dato globalmente
                    //int idMascota = Convert.ToInt32(dtMascotas.Rows[e.RowIndex].Cells["idMascota"].Value);
                    //string nombreMascota = dtMascotas.Rows[e.RowIndex].Cells["Mascota"].Value.ToString();
                    parentForm.formularioHijo(new MascotasConsultar(parentForm, idMascota, nombreMascota));
                  //  MascotasConsultar formularioHijo = new MascotasConsultar(parentForm, idMascotaSeleccionada);
                   // formularioHijo.DatoMascota = idEmpleadoSeleccionado;
                    //parentForm.formularioHijo(formularioHijo);

                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID del empleado.");
                }
            }
        }
    } 
}
