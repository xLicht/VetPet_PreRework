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
using VetPet_.Angie;
using VetPet_.Angie.Mascotas;

namespace VetPet_
{
    public partial class MascotasConsultar : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Mismetodos mismetodos = new Mismetodos();
        private Form1 parentForm;
        private int idMascota;
        private string nombreMascota;
        int idDueño = DueMascotadeDue.DatoEmpleadoGlobal;
        public MascotasConsultar()
        {
            InitializeComponent();
            InitializeComponent();
            this.Load += MascotasConsultar_Load;       // Evento Load
            this.Resize += MascotasConsultar_Resize;   // Evento Resize
            //comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
        }

        public MascotasConsultar(Form1 parent, int idMascota)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia de Form1
            mismetodos = new Mismetodos();
            this.idMascota = idMascota;
            CargarDetallesMascota();
        }

        private void CargarDetallesMascota()
        {
            try
            {
                string query = @" SELECT 
            Mascota.idMascota,
            Mascota.nombre AS Nombre,
            Especie.nombre AS Especie,
            Raza.nombre AS Raza,
            Mascota.fechaNacimiento AS FechaNacimiento,
            Mascota.peso AS Peso,
            Mascota.sexo AS Sexo,
            Mascota.esterilizado AS Esterilizado,
            STUFF((
                SELECT DISTINCT ', ' + Sensibilidad.nombre
                FROM (
                    SELECT Sensibilidad.nombre
                    FROM Mascota_Sensibilidad
                    INNER JOIN Sensibilidad ON Mascota_Sensibilidad.idSensibilidad = Sensibilidad.idSensibilidad
                    WHERE Mascota_Sensibilidad.idMascota = Mascota.idMascota
                    UNION
                    SELECT Sensibilidad.nombre
                    FROM Especie_Sensibilidad
                    INNER JOIN Sensibilidad ON Especie_Sensibilidad.idSensibilidad = Sensibilidad.idSensibilidad
                    WHERE Especie_Sensibilidad.idEspecie = Especie.idEspecie
                    UNION
                    SELECT Sensibilidad.nombre
                    FROM Raza_Sensibilidad
                    INNER JOIN Sensibilidad ON Raza_Sensibilidad.idSensibilidad = Sensibilidad.idSensibilidad
                    WHERE Raza_Sensibilidad.idRaza = Raza.idRaza
                ) AS Sensibilidad
                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Sensibilidades,
			STUFF((
                SELECT DISTINCT ', ' + Enfermedad.nombre
                FROM (
                    SELECT Enfermedad.nombre
                    FROM Mascota_Enfermedad
                    INNER JOIN Enfermedad ON Mascota_Enfermedad.idEnfermedad = Enfermedad.idEnfermedad
                    WHERE Mascota_Enfermedad.idMascota = Mascota.idMascota
                    UNION
                    SELECT Enfermedad.nombre
                    FROM Especie_Enfermedad
                    INNER JOIN Enfermedad ON Especie_Enfermedad.idEnfermedad = Enfermedad.idEnfermedad
                    WHERE Especie_Enfermedad.idEspecie = Especie.idEspecie
                    UNION
                    SELECT Enfermedad.nombre
                    FROM Raza_Enfermedad
                    INNER JOIN Enfermedad ON Raza_Enfermedad.idEnfermedad = Enfermedad.idEnfermedad
                    WHERE Raza_Enfermedad.idRaza = Raza.idRaza
                ) AS Enfermedad
                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Enfermedades,
            STUFF((
                SELECT DISTINCT ', ' + Alergia.nombre
                FROM (
                    SELECT Alergia.nombre
                    FROM Mascota_Alergia
                    INNER JOIN Alergia ON Mascota_Alergia.idAlergia = Alergia.idAlergia
                    WHERE Mascota_Alergia.idMascota = Mascota.idMascota
                    UNION
                    SELECT Alergia.nombre
                    FROM Especie_Alergia
                    INNER JOIN Alergia ON Especie_Alergia.idAlergia = Alergia.idAlergia
                    WHERE Especie_Alergia.idEspecie = Especie.idEspecie
                    UNION
                    SELECT Alergia.nombre
                    FROM Raza_Alergia
                    INNER JOIN Alergia ON Raza_Alergia.idAlergia = Alergia.idAlergia
                    WHERE Raza_Alergia.idRaza = Raza.idRaza
                ) AS Alergia
                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Alergias
        FROM 
            Mascota
        INNER JOIN 
            Especie ON Mascota.idEspecie = Especie.idEspecie
        INNER JOIN 
            Raza ON Mascota.idRaza = Raza.idRaza
        WHERE 
            Mascota.idMascota = @idMascota
        GROUP BY 
            Mascota.idMascota, 
            Mascota.nombre, 
            Especie.nombre, 
            Raza.nombre, 
            Mascota.fechaNacimiento, 
            Mascota.peso, 
            Mascota.sexo, 
            Mascota.esterilizado,
            Especie.idEspecie,  -- Añadido al GROUP BY
            Raza.idRaza;        -- Añadido al GROUP BY

       ";

                // Obtener la conexión desde metodosDeConexion
                using (SqlConnection cn = mismetodos.GetConexion())
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Asignar los parámetros
                    cmd.Parameters.AddWithValue("@idMascota", idMascota);

                    // Agregar el parámetro de salida
                    SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.VarChar, 100);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);

                    // Abrir la conexión si no está abierta
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    // Ejecutar la consulta y leer los datos
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Almacenar el idMascota
                            int idMascota = Convert.ToInt32(reader["idMascota"]);

                            DateTime fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);
                            int edad = DateTime.Now.Year - fechaNacimiento.Year;
                            if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;

                            // Mostrar los datos en los controles
                            textBox1.Text = reader["Nombre"].ToString();
                            textBox2.Text = reader["Especie"].ToString();
                            textBox3.Text = reader["Raza"].ToString();
                            textBox6.Text = $"{reader["Peso"]} kg";
                            textBox4.Text = $"{edad} años";
                            textBox5.Text= fechaNacimiento.ToString();

                            string sexo = reader["Sexo"].ToString();
                            if (sexo == "M") radioButton1.Checked = true;
                            if (sexo == "F") radioButton2.Checked = true;

                            string esterilizado = reader["Esterilizado"].ToString();
                            if (esterilizado == "S") radioButton6.Checked = true;
                            if (esterilizado == "N") radioButton5.Checked = true;

                            string sensibilidades = reader["Sensibilidades"].ToString();
                            richTextBox1.Text = string.IsNullOrEmpty(sensibilidades)
                                ? "Sin sensibilidades registradas"
                                : sensibilidades;
                            string alergias = reader["Alergias"].ToString();
                            richTextBox2.Text = string.IsNullOrEmpty(alergias)
                                ? "Sin alergias registradas"
                                : alergias;
                            string enfermedades = reader["Enfermedades"].ToString();
                            richTextBox3.Text = string.IsNullOrEmpty(enfermedades)
                                ? "Sin enfermedades registradas"
                                : enfermedades;
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron detalles para esta mascota.", "Información");
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
        private void MascotasConsultar_Load(object sender, EventArgs e)
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

        private void MascotasConsultar_Resize(object sender, EventArgs e)
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
            parentForm.formularioHijo(new MascotasModificar(parentForm,idMascota)); // Pasamos la referencia de Form1 a 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (idDueño != 0)
            {
                int idEmpleadoSeleccionado = Convert.ToInt32(idDueño);
                DueMascotadeDue formularioHijo = new DueMascotadeDue(parentForm, "DueConsultarDue");
                formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
                parentForm.formularioHijo(formularioHijo);
            }
            else
            {
                parentForm.formularioHijo(new MascotasListado(parentForm)); // Pasamos la referencia de Form1 a 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaAgendar(parentForm)); // Pasamos la referencia de Form1 a 
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
