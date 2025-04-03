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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static VetPet_.CitaDueños;
using static VetPet_.Form1;

namespace VetPet_
{
    public partial class MainMenu : FormPadre
    {
        ConexionMaestra conexionMaestra = new ConexionMaestra();
        SqlConnection conex;
        public MainMenu()
        {
            InitializeComponent();
        }
        public MainMenu(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            DgvCitas.DefaultCellStyle.Font = new Font("Cascadia Code", 12);
            DgvCitas.ColumnHeadersDefaultCellStyle.Font = new Font("Cascadia Code", 12, FontStyle.Bold);
            lblSaludo.Font = new Font("Cascadia Code", 12, FontStyle.Bold);
            CargarDatos();
            CargarUsuario();
        }
        private void CargarDatos()
        {
            conex = conexionMaestra.CrearConexion();
            try
            {
                conex.Open();

                //    string query = @"
                //SELECT c.idCita, c.fechaRegistro, c.fechaProgramada, c.hora, c.duracion, 
                //       m.nombre AS NombreMascota, mo.nombre AS Motivo
                //FROM Cita c
                //INNER JOIN Mascota m ON c.idMascota = m.idMascota
                //INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
                //ORDER BY c.fechaProgramada;";

                string q = @"
                SELECT c.idCita, p.nombre, p.apellidoP, p.apellidoM, m.nombre AS NombreMascota, c.hora, mo.nombre AS Motivo
                FROM Cita c
                INNER JOIN Mascota m ON c.idMascota = m.idMascota
                INNER JOIN Motivo mo ON c.idMotivo = mo.idMotivo
                INNER JOIN Persona p ON m.idPersona = p.idPersona
                WHERE c.estado <> 'I'
                AND CONVERT(DATE, c.fechaProgramada) = CONVERT(DATE, GETDATE());";
                SqlCommand comando = new SqlCommand(q, conex);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    int idCita = lector.GetInt32(0);
                    string nombre = lector.GetString(1);
                    string apeP = lector.GetString(2);
                    string apeM = lector.GetString(3);
                    string mascota = lector.GetString(4);
                    TimeSpan hora = lector.GetTimeSpan(5);
                    string horaFormateada = hora.ToString(@"hh\:mm");
                    string motivo = lector.GetString(6);

                    DgvCitas.Rows.Add(idCita, nombre+" "+apeP+ " " +apeM, mascota, horaFormateada, motivo);

                    i++;
                }
                conex.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se pudo conectar a la BD. " + ex.Message);
            }
            finally
            {
                conex.Close();
            }
        }
        private void CargarUsuario()
        {
            conexionMaestra = new ConexionMaestra();
            conex = conexionMaestra.CrearConexion();

            try
            {
                conex.Open();

                string q = @"select p.nombre, p.apellidoP, p.apellidoM, e.usuario, p.correoElectronico, p.celularPrincipal from Empleado e inner join Persona p on e.idPersona = p.idPersona where e.idEmpleado = @idEmpleado; ";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@idEmpleado", DatosGlobales.IDUsuario);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string apeP = lector.GetString(1);
                    string apeM = lector.GetString(2);

                    lblSaludo.Text = "Hola "+ nombre + " " + apeP + " " + apeM + "!";
                    i++;
                }


                conex.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conex.Close();
            }
        }

        private void BtnNuevoCorte_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CortesCaja(parentForm));
        }

        private void BtnNuevaCita_Click(object sender, EventArgs e)
        {
            VeterinariaAgendarCita formularioHijo = new VeterinariaAgendarCita(parentForm);
            parentForm.formularioHijo(formularioHijo);
        }

        private void BtnNuevaVenta_Click(object sender, EventArgs e)
        {
            //parentForm.formularioHijo(new VentasNuevaVenta(parentForm, 0, dtProductos, 0, true));
        }
    }
}
