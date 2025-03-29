using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace VetPet_
{
    public partial class VeterinariaGenerarReceta : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();

        public string NombreDueño { get; set; }
        public string NombreMascota { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public string FechaNacimiento { get; set; }
        public string Diagnostico { get; set; }
        public string Peso { get; set; }
        public string Temperatura { get; set; }
        public string Indicaciones { get; set; }
        public List<Tuple<int, string, int>> ListaMedicamentos { get; set; }

        public VeterinariaGenerarReceta(Form1 parent, 
        string nombreDueño, string nombreMascota, string especie, string raza, string fechaNacimiento,
        string diagnostico, string peso, string temperatura, string indicaciones,
        List<Tuple<int, string, int>> listaMedicamentos)
        string fecha = DateTime.Now.ToString("dd-MM-yyyy");
        string hora = DateTime.Now.ToString("m-H");
        public VeterinariaGenerarReceta()
        {
            InitializeComponent();
            parentForm = parent;

            // Asignar las variables a las propiedades (o directamente a los controles)
            NombreDueño = nombreDueño;
            NombreMascota = nombreMascota;
            Especie = especie;
            Raza = raza;
            FechaNacimiento = fechaNacimiento;
            Diagnostico = diagnostico;
            Peso = peso;
            Temperatura = temperatura;
            Indicaciones = indicaciones;
            ListaMedicamentos = listaMedicamentos;

            //foreach (var med in ListaMedicamentos)
            //{
            //    dtMedicamentos.Rows.Add(med.Item1, med.Item2, med.Item3);
            //}
        }

        private void VeterinariaGenerarReceta_Load(object sender, EventArgs e)
        {
            string nombreReceta = "Receta_" + fecha.Replace("-", "") + "-" + hora.Replace("-", "");
            RecetaManager receta = new RecetaManager(nombreReceta);
            receta.GenerarReporte();

            try
            {
                string DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string carpetaReportes = Path.Combine(DirectorioProyecto, "Recetas-Arch");

                string rutaPDF = Path.Combine(carpetaReportes, nombreReceta + ".pdf");

                if (File.Exists(rutaPDF))
                {
                    pdfViewReceta.LoadFile(rutaPDF); // Cargar el PDF en el visor
                }
                else
                {
                    MessageBox.Show("El archivo PDF no se encontró en la ruta especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error: " + er.Message);
            }
        }
    }
}
