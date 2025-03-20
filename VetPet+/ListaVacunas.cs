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
    public partial class ListaVacunas : FormPadre
    {
        //Variables SQL


        public ListaVacunas()
        {
            InitializeComponent();
        }

        public ListaVacunas(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnAgregarTipoDeVacunas_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarTipoVacunas(parentForm));
        }

        private void BtnAgregarVacunas_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarVacunas(parentForm));
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void CargarTipodeServicio()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string id = "3";
            string query = "SELECT \r\n    nombre AS TipoDeServicio\r\n\tFROM ServicioEspecificoHijo \r\n\tWHERE " +
                "idServicioPadre = "+id+" AND estado = 'A';";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
        private void CargarInformaciondeServicio()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string id = "3";
            string query = "SELECT SEN.nombre, SEN.precio, SEN.intervalo, SEN.descripcion FROM Vacuna \r\n" +
                "SEN INNER JOIN ServicioEspecificoHijo SEH ON SEN.idServicioEspecificoHijo = \r\n" +
                "SEH.idServicioEspecificoHijo WHERE SEH.idServicioPadre = "+id+" AND SEN.estado = 'A' AND SEH.estado = 'A'";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    dataGridView2.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView2.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
        private void ListaVacunas_Load(object sender, EventArgs e)
        {
            CargarTipodeServicio();
            CargarInformaciondeServicio();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Asegúrate de que el clic sea dentro de los límites válidos
            {
                if (e.ColumnIndex == 0) // Verificar si es la primera columna
                {
                    // Aquí puedes obtener el valor de la celda clickeada
                    DataGridViewRow filaSeleccionada = dataGridView2.Rows[e.RowIndex];
                    string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();

                    conexionAlex conexion = new conexionAlex();
                    conexion.AbrirConexion();


                    string queryIdServicio = "SELECT idVacuna FROM Vacuna WHERE nombre = @NombreServicio";

                    // Crear el comando para obtener el idServicioEspecificoHijo
                    using (SqlCommand cmd = new SqlCommand(queryIdServicio, conexion.GetConexion()))
                    {
                        try
                        {
                            // Agregar el parámetro del nombre del ServicioEspecificoHijo
                            cmd.Parameters.AddWithValue("@NombreServicio", nombre);

                            // Ejecutar la consulta y obtener el id
                            object result = cmd.ExecuteScalar(); // ExecuteScalar retorna el primer valor de la consulta (idServicioEspecificoHijo)

                            if (result != null)
                            {
                                // Convertir el resultado a int (si el id es entero)
                                int idServicioEspecificoNieto = Convert.ToInt32(result);
                                parentForm.formularioHijo(new ModificarVacunas(parentForm, idServicioEspecificoNieto));

                            }
                            else
                            {
                                MessageBox.Show("No se encontró el Servicio Especificado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                        }
                        finally
                        {
                            conexion.CerrarConexion();
                        }
                    }
                }
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string patron = TxtBuscar.Text;
            string id = "3";
            string query = " SELECT nombre AS TipoDeServicio FROM ServicioEspecificoHijo \r\n  WHERE idServicioPadre = "+id+" " +
                "AND nombre LIKE '%" + patron + "%' AND estado = 'A';";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string patron = TxtBuscar.Text;
            string id = "3";
            string query = " SELECT nombre AS TipoDeServicio FROM ServicioEspecificoHijo \r\n  WHERE idServicioPadre = " + id + " " +
                "AND nombre LIKE '%" + patron + "%' AND estado = 'A';";
            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las presentaciones: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
    }
}
