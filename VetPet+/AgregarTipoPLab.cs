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
    public partial class AgregarTipoPLab : FormPadre
    {
        //Variables SQL
        public SqlConnection conexion;
        public SqlCommand comando;
        public SqlDataReader Lector;
        public string q;
        public string mensaje;
        public AgregarTipoPLab()
        {
            InitializeComponent();
        }
        public AgregarTipoPLab(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaPLab(parentForm));
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion = new SqlConnection(@"Data Source=DESKTOP-GQ6Q9HG\SQLEXPRESS;Initial Catalog=Servicio;Integrated Security=True;");
                conexion.Open();

                int provicional = 2;
                string Nombre = TxtNombre.Text;
                string Descripcion = richTextBox1.Text;
                int ServicioPadre = 2;
                string tabla = "EstudioLaboratorio";

                q = "INSERT INTO "+ tabla + " (IdServicioEspecificoHijo,nombre, descripcion,SegundaID) VALUES (@SEE, @NOM, @DES,@SEP);";
                comando = new SqlCommand(q, conexion);
                comando.Parameters.AddWithValue("@SEE", provicional);
                comando.Parameters.AddWithValue("@NOM", Nombre);
                comando.Parameters.AddWithValue("@DES", Descripcion);
                comando.Parameters.AddWithValue("@SEP", ServicioPadre);


                comando.ExecuteNonQuery();
                conexion.Close();

                mensaje = "Datos insertados correctamente en 'General'.";
                parentForm.formularioHijo(new ListaPLab(parentForm));
            }
            catch (System.Exception ex)
            {
                mensaje = "Error al insertar datos: " + ex.Message;
            }
            finally
            {
                MessageBox.Show(mensaje);
            }

            
        }
    }
}
