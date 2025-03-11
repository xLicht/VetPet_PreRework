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
using VetPet_.Angie;
using VetPet_.Angie.Mascotas;

namespace VetPet_
{
    public partial class MascotasEliminarConfirm : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private int idMascota;
        private string nombreMascota;
        private Form1 parentForm;
        private Mismetodos mismetodos = new Mismetodos();
        public MascotasEliminarConfirm(Form1 parent, int idMascota,string nombreMascota)
        {
            InitializeComponent();
            this.Load += MascotasEliminarConfirm_Load;       // Evento Load
            this.Resize += MascotasEliminarConfirm_Resize;   // Evento Resize
            parentForm = parent;  // Guardamos la referencia de Form1
            textBox4.Text = nombreMascota;
            this.idMascota = idMascota;
            this.nombreMascota = nombreMascota;
        }

        private void MascotasEliminarConfirm_Load(object sender, EventArgs e)
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

        private void MascotasEliminarConfirm_Resize(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasListado(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            try
            {
                mismetodos.AbrirConexion();

                // Consulta SQL para actualizar el campo estado a "D" (eliminación lógica)
                string query = @"
                UPDATE Mascota
                SET 
                    estado = 'D'
                WHERE 
                    idMascota = @idMascota;"
                ;

                using (SqlCommand comando = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    // Agregar el parámetro para el ID de la mascota
                    comando.Parameters.AddWithValue("@idMascota", idMascota);

                    // Ejecutar la consulta
                    int rowsAffected = comando.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Mascota eliminada correctamente.", "Éxito");
                        // Redirigir al formulario de consulta o cualquier otra acción necesaria
                        parentForm.formularioHijo(new MascotasListado(parentForm));
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la mascota para eliminar.", "Información");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }
    }
}
