using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using VetPet_;

namespace VetPet_
{
    public partial class ListaPLab : FormPadre
    {
        //Variables SQL
        int identificadorEnviado;
        public ListaPLab()
        {
            InitializeComponent();
        }
        public ListaPLab(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos

        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            //parentForm.formularioHijo(new ModificarPLab(parentForm));
        }

        private void BtnAgregarTipoDeCirugia_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarTipoPLab(parentForm));
        }

        private void BtnAgregarCirugía_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarPLab(parentForm));
        }

        private void BtnAgregarPLab_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarPLab(parentForm));
        }

        private void ListaPLab_Load(object sender, EventArgs e)
        {

            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            conexion.CargarTipodeServicio(dataGridView1, "8");
            conexion.CargarInformaciondeServicio(dataGridView2, "8");


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


                    string queryIdServicio = "SELECT idServicioEspecificoNieto FROM ServicioEspecificoNieto WHERE nombre = @NombreServicio";

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
                                parentForm.formularioHijo(new ModificarPLab(parentForm, idServicioEspecificoNieto));

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
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            conexion.Buscar(dataGridView1, TxtBuscar,"8");
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            conexion.Buscar(dataGridView1, TxtBuscar, "8");
        }
    }
}
