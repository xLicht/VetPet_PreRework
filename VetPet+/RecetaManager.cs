using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace VetPet_
{
    public class RecetaManager
    {
        protected Document Documento { get; private set; }
        protected PdfWriter Writer { get; private set; }
        protected string RutaPDF { get; private set; }
        protected string DirectorioProyecto { get; private set; }

        public RecetaManager(string nombreReceta)
        {
            DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string carpetaReportes = Path.Combine(DirectorioProyecto, "Recetas-Arch");

            if (!Directory.Exists(carpetaReportes))
            {
                Directory.CreateDirectory(carpetaReportes);
            }

            // Genera el nombre dinámico del PDF
            RutaPDF = Path.Combine(carpetaReportes, nombreReceta + ".pdf");

            Documento = new Document(PageSize.A6.Rotate());
        }
        public void GenerarReporte()
        {
            try
            {
                Writer = PdfWriter.GetInstance(Documento, new FileStream(RutaPDF, FileMode.Create));
                Documento.Open();

                AgregarEncabezado();
                //AgregarContenido();
                //AgregarPiePagina();

                MessageBox.Show($"PDF generado correctamente en:\n{RutaPDF}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message);
            }
            finally
            {
                Documento.Close();
            }
        }
        private void AgregarEncabezado()
        {
            try
            {
                Font textoFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9);

                string imagenPath = Path.Combine(DirectorioProyecto, "Resources", "VetPet_Logo1.png");
                if (!File.Exists(imagenPath))
                {
                    MessageBox.Show("La imagen VetPetLogo.png no se encontró en la carpeta Resources.");
                    return;
                }

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagenPath);
                img.ScaleAbsolute(40f, 40f);
                img.Alignment = Element.ALIGN_LEFT;

                PdfPTable tablaEncabezado = new PdfPTable(2);
                tablaEncabezado.WidthPercentage = 100;
                tablaEncabezado.SetWidths(new float[] { 1, 3 });

                PdfPCell celdaImagen = new PdfPCell(img);
                celdaImagen.Border = PdfPCell.NO_BORDER;
                celdaImagen.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaEncabezado.AddCell(celdaImagen);

                PdfPCell celdaTextoEmpresa = new PdfPCell
                (
                    new Phrase("VetPet+ S.A de C.V\n\nLa Paz, Baja California Sur, México\n\nBlvd. Forjadores de Sudcalifornia\n\ncontacto.vetpetplus@vetpet.com\n\n+52 6121948332", textoFont)
                );
                celdaTextoEmpresa.Border = PdfPCell.NO_BORDER;
                celdaTextoEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaEncabezado.AddCell(celdaTextoEmpresa);

                Documento.Add(tablaEncabezado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en AgregarEncabezado: " + ex.Message);
            }
        }
    }
}
