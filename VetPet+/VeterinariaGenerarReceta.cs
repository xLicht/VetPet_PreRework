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

namespace VetPet_
{
    public partial class VeterinariaGenerarReceta : FormPadre
    {
        string fecha = DateTime.Now.ToString("dd-MM-yyyy");
        string hora = DateTime.Now.ToString("m-H");
        public VeterinariaGenerarReceta()
        {
            InitializeComponent();
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
