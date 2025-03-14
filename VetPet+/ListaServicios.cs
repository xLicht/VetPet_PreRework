using System;
using System.Collections;
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
    public partial class ListaServicios : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();


        private Form1 parentForm;
        public ListaServicios()
        {
            InitializeComponent();
            this.Load += ListaServicios_Load;       // Evento Load
            this.Resize += ListaServicios_Resize;
        }
        public ListaServicios(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void ListaServicios_Load(object sender, EventArgs e)
        {
            
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;
            CargarServicios();
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void CargarServicios()
        {
            conexionAlex conexion = new conexionAlex();
            conexion.AbrirConexion();
            string query = "SELECT \r\n    sp.nombre AS NombreServicio, \r\n    cs.nombre AS ClaseServicio, \r\n    " +
                "te.nombre AS TipoEmpleado\r\nFROM ServicioPadre sp\r\nINNER JOIN TipoEmpleado te ON sp.idtipoempleado = " +
                "te.idtipoempleado\r\nINNER JOIN ClaseServicio cs ON sp.idClaseServicio = cs.idClaseServicio WHERE sp.estado = 'A'";
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
        private void ListaServicios_Resize(object sender, EventArgs e)
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

        private void BtnRegresar_Click_1(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new MenuServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarServicios(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void BtnTipoDeServicios_Click(object sender, EventArgs e)
        {
             // Pasamos la referencia de Form1 a AlmacenInventarioProductos
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1) // La columna 2 tiene índice 1
            {
                var valorSeleccionadoColumna2 = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                // Obtener el valor de la celda seleccionada de la columna 1 (primera columna)
                var valorSeleccionadoColumna1 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Guardar el valor de la primera columna en una variable
                string primeraColumna = valorSeleccionadoColumna1;

                // Ahora puedes usar la variable 'primeraColumna' como lo necesites
                conexionAlex conexion = new conexionAlex();
                conexion.AbrirConexion();


                string queryIdServicio = "SELECT idServicioPadre FROM ServicioPadre WHERE nombre = @NombreServicio";

                // Crear el comando para obtener el idServicioEspecificoHijo
                using (SqlCommand cmd = new SqlCommand(queryIdServicio, conexion.GetConexion()))
                {
                    try
                    {
                        // Agregar el parámetro del nombre del ServicioEspecificoHijo
                        cmd.Parameters.AddWithValue("@NombreServicio", primeraColumna);

                        // Ejecutar la consulta y obtener el id
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Convertir el resultado a int (si el id es entero)
                            int idServicioEspecificoNieto = Convert.ToInt32(result);
                            parentForm.formularioHijo(new ModificarServicios(parentForm, idServicioEspecificoNieto));

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
           else if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Asegúrate de que el clic sea dentro de los límites válidos
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Aquí puedes obtener el valor de la celda clickeada
                string valorCelda = cell.Value.ToString();

                // Dependiendo del valor o cualquier otro criterio, puedes abrir el formulario correspondiente
                switch (valorCelda)
                {
                    case "Cirugias":
                        parentForm.formularioHijo(new ListaCirugias(parentForm));
                        break;
                    case "Acicalamiento":
                        parentForm.formularioHijo(new ListaCirugias(parentForm));
                        break;
                    case "Consulta General":
                        parentForm.formularioHijo(new ListaCirugias(parentForm));
                        break;
                    case "Cremacion":
                        parentForm.formularioHijo(new ListaCirugias(parentForm));
                        break;
                    case "Masaje":
                        parentForm.formularioHijo(new ListaCirugias(parentForm));
                        break;
                    case "Rayos X":
                        parentForm.formularioHijo(new ListaRayosX(parentForm));
                        break;
                    case "Estudios de Laboratorio":
                        parentForm.formularioHijo(new ListaPLab(parentForm));
                        break;
                    case "Ultrasonidos":
                        parentForm.formularioHijo(new ListaUltrasonidos(parentForm));
                        break;
                    case "Vacunas":
                        parentForm.formularioHijo(new ListaVacunas(parentForm));
                        break;
                    case "Radiografías":
                        parentForm.formularioHijo(new ListaRadiografias(parentForm));
                        break;
                    // Agrega más casos según los tipos de servicio que tengas
                    default:
                        MessageBox.Show("No se encontró formulario para este servicio.");
                        break;
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
            string query = "SELECT     sp.nombre AS NombreServicio,cs.nombre AS ClaseServicio,\r\n  " +
                "te.nombre AS TipoEmpleado FROM ServicioPadre sp INNER JOIN TipoEmpleado te ON sp.idtipoempleado = \r\n" +
                "te.idtipoempleado INNER JOIN ClaseServicio cs ON sp.idClaseServicio = cs.idClaseServicio" +
                " WHERE sp.nombre LIKE '%"+ patron + "%' AND sp.estado = 'A'";
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
            string query = "SELECT     sp.nombre AS NombreServicio,cs.nombre AS ClaseServicio,\r\n  " +
                "te.nombre AS TipoEmpleado FROM ServicioPadre sp INNER JOIN TipoEmpleado te ON sp.idtipoempleado = \r\n" +
                "te.idtipoempleado INNER JOIN ClaseServicio cs ON sp.idClaseServicio = cs.idClaseServicio" +
                " WHERE sp.nombre LIKE '%" + patron + "%' AND sp.estado = 'A'";
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
