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
    public partial class DueAtencionAlCliente : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        public DueAtencionAlCliente(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            cbFliltrar.SelectedIndexChanged += (s, e) => CargarDatos();
            CargarDatos();
        }

        private void DueAtencionAlCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueAgregarDueño(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            
            parentForm.formularioHijo(new MenuAtencionaCliente(parentForm));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos2();
        }

        private void CargarDatos()
        {
            try
            {
                conexionDB.AbrirConexion();

                // Definir la columna por la cual se ordenará
                string ordenColumna = "idPersona";
                if (cbFliltrar.SelectedItem != null)
                {
                    switch (cbFliltrar.SelectedItem.ToString())
                    {
                        case "Nombre":
                            ordenColumna = "nombre";
                            break;
                        case "Apellido Paterno":
                            ordenColumna = "apellidoP";
                            break;
                        case "Apellido Materno":
                            ordenColumna = "apellidoM";
                            break;
                    }
                }

                string filtroNombre = txtBuscar.Text;

                // Consulta SQL corregida para obtener los datos requeridos
                string query = $@"SELECT idPersona, nombre, apellidoP, apellidoM, correoElectronico, celular
                            FROM Persona
                            WHERE estado = 'A'
                            ORDER BY nombre;";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombreFiltro", "%" + filtroNombre + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtDueños.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        dtDueños.Rows.Add(row["idPersona"], row["nombre"], row["apellidoP"], row["apellidoM"], row["correoElectronico"], row["celular"]);
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

      

        private void dtDueños_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtDueños.Rows[e.RowIndex];

                if (row.Cells[0].Value != null)
                {
                    int idEmpleadoSeleccionado = Convert.ToInt32(row.Cells[0].Value);
                    DueConsultarDueño formularioHijo = new DueConsultarDueño(parentForm);
                    formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
                    parentForm.formularioHijo(formularioHijo);
                    
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el ID del empleado.");
                }
            }
        }
        private void CargarDatos2()
        {
            try
            {
                conexionDB.AbrirConexion();

                string ordenColumna = "nombre"; // Orden predeterminado
                string ordenDireccion = "ASC";  // Orden ascendente por defecto

                // Determinar el criterio de ordenamiento seleccionado en cbFiltrar
                if (cbFliltrar.SelectedItem != null)
                {
                    switch (cbFliltrar.SelectedItem.ToString())
                    {
                        case "Nombre":
                            ordenColumna = "nombre";
                            break;
                        case "Apellido Paterno":
                            ordenColumna = "apellidoP";
                            break;
                        case "Apellido Materno":
                            ordenColumna = "apellidoM";
                            break;
                    }
                }

                // Consulta SQL base
                string query = $@"
        SELECT idPersona, nombre, apellidoP, apellidoM, correoElectronico, celular
        FROM Persona
        WHERE estado = 'A'";

                // Agregar filtro de búsqueda si txtBuscar no está vacío
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    query += " AND nombre LIKE @nombreFiltro";
                }

                query += $" ORDER BY {ordenColumna} {ordenDireccion};";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    // Asignar parámetro si hay búsqueda
                    if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                    {
                        cmd.Parameters.AddWithValue("@nombreFiltro", $"%{txtBuscar.Text}%");
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtDueños.Rows.Clear(); // Limpiar antes de agregar nuevas filas

                    foreach (DataRow row in dt.Rows)
                    {
                        dtDueños.Rows.Add(
                            row["idPersona"], row["nombre"], row["apellidoP"],
                            row["apellidoM"], row["correoElectronico"], row["celular"]
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


        private void CargarDatos3()
        {
            try
            {
                conexionDB.AbrirConexion();

                // Definir la columna por la cual se ordenará
                string ordenColumna = "nombre"; // Orden predeterminado por nombre
                if (cbFliltrar.SelectedItem != null)
                {
                    switch (cbFliltrar.SelectedItem.ToString())
                    {
                        case "Nombre":
                            ordenColumna = "nombre";
                            break;
                        case "Apellido Paterno":
                            ordenColumna = "apellidoP";
                            break;
                        case "Apellido Materno":
                            ordenColumna = "apellidoM";
                            break;
                    }
                }

                // Consulta SQL para obtener los datos ordenados según la columna seleccionada
                string query = $@"SELECT idPersona, nombre, apellidoP, apellidoM, correoElectronico, celular
                          FROM Persona
                          WHERE estado = 'A'
                          ORDER BY {ordenColumna};";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dtDueños.Rows.Clear(); // Limpiar el DataGridView antes de agregar nuevos datos

                    // Llenar el DataGridView con los datos obtenidos
                    foreach (DataRow row in dt.Rows)
                    {
                        dtDueños.Rows.Add(
                            row["idPersona"],
                            row["nombre"],
                            row["apellidoP"],
                            row["apellidoM"],
                            row["correoElectronico"],
                            row["celular"]
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

        private void cbFliltrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatos3();
        }

        private void cbFliltrar_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            CargarDatos2();
        }
    }
}

