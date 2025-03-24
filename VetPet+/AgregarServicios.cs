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

namespace VetPet_
{
    public partial class AgregarServicios : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;
        public AgregarServicios()
        {
            InitializeComponent();
            this.Load += AgregarServicios_Load;       // Evento Load
            this.Resize += AgregarServicios_Resize;
        }
        public AgregarServicios(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void AgregarServicios_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void AgregarServicios_Resize(object sender, EventArgs e)
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
        
        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MenuServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            int idTipoServicio;



            string queryIdServicio = "INSERT INTO ServicioPadre (nombre, descripcion,idClaseServicio) VALUES (@NOM, @DES,@CSR);";

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
                    else
                    {
                        idTipoServicio = 0; // Ningún radio button seleccionado
                        MessageBox.Show("Por favor, seleccione una opción.");
                    }

                    // Agregar los parámetros para la inserción
                    insertcmd.Parameters.AddWithValue("@NOM", Nombre);
                    insertcmd.Parameters.AddWithValue("@DES", Descripcion);
                    insertcmd.Parameters.AddWithValue("@CSR", idTipoServicio);


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

        private void BtnModificarServicios_Click(object sender, EventArgs e)
        {
           //parentForm.formularioHijo(new ModificarServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }
    }
}
