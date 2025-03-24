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
    public partial class ModificarRayosX : FormPadre
    {
        int identificador;
        string cirugia;
        public ModificarRayosX()
        {
            InitializeComponent();
        }
        public ModificarRayosX(Form1 parent, int Id, string idcirugia)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
            identificador = Id;
            cirugia = idcirugia;
        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            int idServicio = conexion.ObtenerId("Rayos X", "ServicioPadre");
            parentForm.formularioHijo(new ListaRayosX(parentForm, idServicio));
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            conexion.Eliminar(identificador);
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;

            string nombreServicioHijo = selectedRow["nombre"].ToString();
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            // Obtener el idServicioEspecificoHijo a partir del nombre
            string queryGetIdServicioHijo = "SELECT idServicioEspecificoHijo FROM ServicioEspecificoHijo WHERE nombre = @NombreServicioHijo";
            int idServicioHijo = 0;

            using (SqlCommand cmd = new SqlCommand(queryGetIdServicioHijo, conexion.GetConexion()))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@NombreServicioHijo", nombreServicioHijo);
                    object result = cmd.ExecuteScalar(); // Ejecutar la consulta y obtener el primer valor de la primera columna

                    if (result != null)
                    {
                        idServicioHijo = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el servicio hijo con ese nombre.");
                        return;
                    }

                    // Ahora que tenemos el idServicioHijo, proceder con la actualización del registro en la tabla ServicioEspecificoNieto

                    string queryUpdate = "UPDATE ServicioEspecificoNieto SET nombre = @NOM, descripcion = @DES, precio = @PRE, duracion = @DUR, idServicioEspecificoHijo = @ISH WHERE idServicioEspecificoNieto = @ID";

                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexion.GetConexion()))
                    {
                        // Obtener valores de los controles
                        string nombre = TxtNombre.Text;
                        string descripcion = richTextBox1.Text.Replace("\r", "").Replace("\n", "");

                        // Convertir valores numéricos
                        if (!decimal.TryParse(TxtPrecio.Text, out decimal precio))
                        {
                            MessageBox.Show("Ingrese un precio válido.");
                            return;
                        }

                        if (!int.TryParse(TxtDuracion.Text, out int duracion))
                        {
                            MessageBox.Show("Ingrese una duración válida.");
                            return;
                        }

                        // Agregar parámetros a la consulta de actualización
                        cmdUpdate.Parameters.AddWithValue("@NOM", nombre);
                        cmdUpdate.Parameters.AddWithValue("@DES", descripcion);
                        cmdUpdate.Parameters.AddWithValue("@PRE", precio);
                        cmdUpdate.Parameters.AddWithValue("@DUR", duracion);
                        cmdUpdate.Parameters.AddWithValue("@ISH", idServicioHijo); // Aquí pasamos el idServicioEspecificoHijo
                        cmdUpdate.Parameters.AddWithValue("@ID", identificador); // Este es el ID del registro a actualizar

                        // Ejecutar la consulta de actualización
                        int filasAfectadas = cmdUpdate.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Registro actualizado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un registro con ese ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el registro: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }

        private void ModificarRayosX_Load(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            conexion.cargarCombobox(comboBox1, cirugia);
            string query = "SELECT nombre, descripcion, precio, duracion FROM ServicioEspecificoNieto WHERE idServicioEspecificoNieto = @ID";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@ID", identificador);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) // Si hay resultados
                    {
                        TxtNombre.Text = reader["nombre"].ToString();
                        richTextBox1.Text = reader["descripcion"].ToString();
                        TxtPrecio.Text = Convert.ToDecimal(reader["precio"]).ToString("0.00");
                        TxtDuracion.Text = reader["duracion"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los datos: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
    }
}
