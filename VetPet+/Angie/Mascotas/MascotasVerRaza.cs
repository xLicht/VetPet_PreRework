using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using VetPet_.Angie.Ventas;

namespace VetPet_.Angie.Mascotas
{
    public partial class MascotasVerRaza : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodos = new Mismetodos();
        string tipo = "";
        private Form1 parentForm;
        int idRaza;
        public MascotasVerRaza(Form1 parent, int idRaza)
        {
            InitializeComponent();
            this.Load += MascotasVerRaza_Load;       // Evento Load
            this.Resize += MascotasVerRaza_Resize;
            parentForm = parent;  // Guardamos la referencia de Form
            if (label1.Text == "Consultar Raza")
            {
                this.idRaza = idRaza;
                Consultar();
            }
            button4.Visible = false;
        }

        public void Consultar()
        {
            try
            {
                // Crear el diccionario de mapeo para la tabla Raza
                Dictionary<string, Control> mapeoColumnasControles = new Dictionary<string, Control>
        {
            { "nombre", textBox1 },        // Mapea la columna "nombre" al TextBox textBox1
            { "idEspecie", comboBox1 },
            { "descripcion", richTextBox1 } // Mapea la columna "descripcion" al RichTextBox richTextBox1
        };

                // Llamar al método CargarDatosGenerico para cargar los datos de la raza
                mismetodos.CargarDatosGenerico("Raza", idRaza, mapeoColumnasControles,listBox2,listBox1);

                mismetodos.AbrirConexion();
                // Obtener el idEspecie del ComboBox
                int idEspecie = Convert.ToInt32(comboBox1.Text);

                // Consultar el nombre de la especie basado en el idEspecie
                string queryEspecie = "SELECT nombre FROM Especie WHERE idEspecie = @idEspecie";

                using (SqlCommand cmd = new SqlCommand(queryEspecie, mismetodos.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idEspecie", idEspecie);


                    // Ejecutar la consulta y obtener el nombre de la especie
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombreEspecie = reader["nombre"].ToString();

                            // Mostrar el nombre de la especie en el ComboBox
                            comboBox1.Text = nombreEspecie;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la especie asociada a la raza.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los datos: {ex.Message}");
            }
            finally
            {
                mismetodos.CerrarConexion();
            }
        }
        public void Modificar()
        {
            string nombre = textBox1.Text;
            string descripcion = richTextBox1.Text;
            string especie = comboBox1.Text;    
            try
            {
                // Obtener el idEspecie basado en el nombre de la especie
                int idEspecie = ObtenerIdEspeciePorNombre(especie);

                if (idEspecie == -1)
                {
                    MessageBox.Show("No se encontró la especie con el nombre proporcionado.");
                    return;
                }

                // Crear el diccionario de valores a modificar
                Dictionary<string, object> parametrosValores = new Dictionary<string, object>
        {
            { "nombre", nombre },
             { "idEspecie", idEspecie },
            { "descripcion", descripcion } 
        };

                // Llamar al método ModificarDatosGenerico para actualizar la especie
                mismetodos.ModificarDatosGenerico("Raza", idRaza, parametrosValores);

                MessageBox.Show("Especie modificada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la especie: {ex.Message}");
            }
        }

        private int ObtenerIdEspeciePorNombre(string nombreEspecie)
        {
            try
            {
                mismetodos.AbrirConexion();
                // Consulta SQL para obtener el idEspecie basado en el nombre
                string query = "SELECT idEspecie FROM Especie WHERE nombre = @Nombre";

                using (SqlCommand cmd = new SqlCommand(query, mismetodos.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreEspecie);

                    // Ejecutar la consulta y obtener el idEspecie
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1; // Retornar -1 si no se encuentra la especie
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el idEspecie: {ex.Message}");
                return -1;
            }
            finally 
            {
                mismetodos.CerrarConexion();
            } 
        }

        public void EliminarRazaEnCascada()
        {
            mismetodos.EliminarRazaEnCascada(idRaza);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MascotasVerRazas(parentForm)); // Pasamos la referencia de Form1 a
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "Modificar Raza";
            button4.Visible = true;
            textBox1.Enabled = true;
            listBox1.Enabled = true;
            listBox2.Enabled = true;
            richTextBox1.Enabled = true;
            try
            {
                mismetodos.AbrirConexion();
                // Cargar las especies
                string queryEsp = "SELECT nombre FROM Especie ORDER BY nombre";
                using (SqlCommand comandoEsp = new SqlCommand(queryEsp, mismetodos.GetConexion()))
                {
                    using (SqlDataReader readerEsp = comandoEsp.ExecuteReader())
                    {
                        // Limpiar el ComboBox antes de agregar nuevos elementos
                        comboBox1.Items.Clear();

                        while (readerEsp.Read())
                        {
                            comboBox1.Items.Add(readerEsp["nombre"].ToString());
                        }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Modificar Raza")
            {
                Modificar();
                parentForm.formularioHijo(new MascotasVerRazas(parentForm)); // Pasamos la referencia de Form1 a

            }
            if (label1.Text == "Consultar Raza")
            {
                parentForm.formularioHijo(new MascotasVerRazas(parentForm)); // Pasamos la referencia de Form1 a
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Text = "Eliminar";
            if (button4.Text == "Eliminar")
            {
                EliminarRazaEnCascada();
                parentForm.formularioHijo(new MascotasVerRazas(parentForm)); // Pasamos la referencia de Form1 a
            }
        }
        private void MascotasVerRaza_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void MascotasVerRaza_Resize(object sender, EventArgs e)
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

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                // Obtener el elemento seleccionado
                string sensibilidadSeleccionada = listBox2.SelectedItem.ToString();

                // Preguntar al usuario si está seguro de eliminar
                DialogResult resultado = MessageBox.Show(
                    $"¿Estás seguro de que deseas eliminar la sensibilidad '{sensibilidadSeleccionada}'?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                // Si el usuario confirma, eliminar el elemento
                if (resultado == DialogResult.Yes)
                {
                    // Eliminar el elemento seleccionado del ListBox
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);

                    // Eliminar el elemento de la base de datos
                    mismetodos.EliminarRegistro("Raza_Sensibilidad", "Sensibilidad", sensibilidadSeleccionada);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si se seleccionó un elemento
            if (listBox1.SelectedIndex != -1)
            {
                // Obtener el elemento seleccionado
                string sensibilidadSeleccionada = listBox1.SelectedItem.ToString();

                // Preguntar al usuario si está seguro de eliminar
                DialogResult resultado = MessageBox.Show(
                    $"¿Estás seguro de que deseas eliminar la Alergia '{sensibilidadSeleccionada}'?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                // Si el usuario confirma, eliminar el elemento
                if (resultado == DialogResult.Yes)
                {
                    // Eliminar el elemento seleccionado del ListBox
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                    // Eliminar el elemento de la base de datos
                    mismetodos.EliminarRegistro("Raza_Alergia", "Alergia", sensibilidadSeleccionada);
                }
            }
        }
    }
}

