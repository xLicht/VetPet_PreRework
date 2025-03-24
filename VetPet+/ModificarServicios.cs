using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class ModificarServicios : Form
    {
        int Identificador;
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        public ModificarServicios()
        {
            
            InitializeComponent();
            this.Load += ModificarServicios_Load;       // Evento Load
            this.Resize += ModificarServicios_Resize;
        }

        public ModificarServicios(Form1 parent, int Id)
        {
            Identificador = Id;
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }


        private void ModificarServicios_Load(object sender, EventArgs e)
            {
            CargarDatos();
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void ModificarServicios_Resize(object sender, EventArgs e)
        {
            // Calcular el factor de escala
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in this.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];

                    // Ajustar las dimensiones
                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);

                    // Ajustar el tamaño de la fuente
                    control.Font = new Font(control.Font.FontFamily, info.fontSize * Math.Min(scaleX, scaleY));
                }
            }
        }
        
        private void BtnTiposDeServicios_Click(object sender, EventArgs e)
        {
            //parentForm.formularioHijo(new ListaPLab(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos

        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            int idTipoServicio;
            string estado = "";



            string queryIdServicio = "UPDATE ServicioPadre SET nombre = @NOM, descripcion = @DES, idClaseServicio = @ICS, estado = @EST WHERE idServicioPadre = @ID";

            // Crear el comando para obtener el idServicioEspecificoHijo
            using (SqlCommand insertcmd = new SqlCommand(queryIdServicio, conexion.GetConexion()))
            {
                try
                {
                    // Obtener los valores de los controles
                    string Nombre = TxtNombre.Text;
                    string Descripcion = richTextBox1.Text.Replace("\r", "").Replace("\n", "");


                    if (RbMédico.Checked)
                    {
                        idTipoServicio = 1; // Si el primer radio button está seleccionado
                    }
                    else if (RbEstetico.Checked)
                    {
                        idTipoServicio = 2; // Si el segundo radio button está seleccionado
                    }
                    else if (rbOtro.Checked)
                    {
                        idTipoServicio = 3; // Si el segundo radio button está seleccionado
                    }
                    else
                    {
                        idTipoServicio = 0; // Ningún radio button seleccionado
                        MessageBox.Show("Por favor, seleccione una opción.");
                    }

                    if (RbActivo.Checked)
                    {
                        estado = "A"; // Si el primer radio button está seleccionado
                    }
                    else if (RbInactivo.Checked)
                    {
                        estado = "B"; // Si el segundo radio button está seleccionado
                    }
                    else
                    {
                        idTipoServicio = 0; // Ningún radio button seleccionado
                        MessageBox.Show("Por favor, seleccione una opción de estado.");
                    }

                    // Agregar los parámetros para la inserción
                    insertcmd.Parameters.AddWithValue("@NOM", Nombre);
                    insertcmd.Parameters.AddWithValue("@DES", Descripcion);
                    insertcmd.Parameters.AddWithValue("@ICS", idTipoServicio);
                    insertcmd.Parameters.AddWithValue("@EST", estado);
                    insertcmd.Parameters.AddWithValue("@ID", Identificador);


                    // Ejecutar la consulta de inserción
                    insertcmd.ExecuteNonQuery();  // Usar ExecuteNonQuery para la inserción

                    MessageBox.Show("Nuevo Tipo de Servicio Registrado");
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
        private void CargarDatos()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();

            int idClaseServicio = 0;
            string estado = "";

            // Consulta para obtener los datos del servicio
            string query = "SELECT nombre, descripcion, idClaseServicio, estado FROM ServicioPadre WHERE idServicioPadre = @ID";

            using (SqlCommand cmd = new SqlCommand(query, conexion.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@ID", Identificador);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Cargar los datos en los controles del formulario
                        TxtNombre.Text = reader["nombre"].ToString();
                        richTextBox1.Text = reader["descripcion"].ToString();                       
                        idClaseServicio = Convert.ToInt32(reader["idClaseServicio"]);
                        estado = reader["estado"].ToString();
                        if (idClaseServicio == 2)
                            RbEstetico.Checked = true;
                        else
                            RbMédico.Checked = true;
                        if(estado == "A")
                            RbActivo.Checked = true;
                        else
                            RbInactivo.Checked = true;

                    }
                    else
                    {
                        MessageBox.Show("No se encontró un servicio con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }
        }
        }
}
