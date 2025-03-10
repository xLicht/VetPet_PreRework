using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Pruebas_PDF
{
    public abstract class ReporteBase
    {
        protected Document Documento { get; private set; }
        protected PdfWriter Writer { get; private set; }
        protected string RutaPDF { get; private set; }
        protected string DirectorioProyecto { get; private set; }

        public ReporteBase(string nombreReporte)
        {
            DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string carpetaReportes = Path.Combine(DirectorioProyecto, "Reportes-Arch");

            if (!Directory.Exists(carpetaReportes))
            {
                Directory.CreateDirectory(carpetaReportes);
            }

            // Genera el nombre dinámico del PDF
            RutaPDF = Path.Combine(carpetaReportes, nombreReporte+".pdf");

            Documento = new Document(PageSize.A4.Rotate());
        }

        public void GenerarReporte(string tipoReporte)
        {
            try
            {
                Writer = PdfWriter.GetInstance(Documento, new FileStream(RutaPDF, FileMode.Create));
                Documento.Open();

                AgregarEncabezado();
                AgregarContenido(tipoReporte);
                AgregarPiePagina();

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

        protected virtual void AgregarEncabezado()
        {
            try
            {
                Font textoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                string imagenPath = Path.Combine(DirectorioProyecto, "Resources", "VetPet_Logo1.png");
                if (!File.Exists(imagenPath))
                {
                    MessageBox.Show("La imagen VetPetLogo.png no se encontró en la carpeta Resources.");
                    return;
                }

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagenPath);
                img.ScaleAbsolute(100f, 100f);
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
                Documento.Add(new Paragraph("\n"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en AgregarEncabezado: " + ex.Message);
            }
        }

        protected abstract void AgregarContenido(string tipoReporte);

        public virtual SqlConnection ConexionSQL()
        {
            try
            {
                SqlConnection conexion = new SqlConnection("Data Source=CARLOS-DESKTOP;Initial Catalog=VetPetPlus;Integrated Security=True");
                //MessageBox.Show("Conexión establecida correctamente");
                return conexion;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        protected virtual void AgregarPiePagina()
        {
            Font textoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            PdfPTable tablaFirmas = new PdfPTable(2);
            tablaFirmas.WidthPercentage = 80;
            tablaFirmas.SetWidths(new float[] { 1, 1 });

            PdfPCell celdaFirma1 = new PdfPCell(new Phrase("\n\n\nFirma del Responsable\n\n\n\n________________________", textoFont));
            PdfPCell celdaFirma2 = new PdfPCell(new Phrase("\n\n\nFirma del Administrador\n\n\n\n______________________", textoFont));

            celdaFirma1.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaFirma2.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaFirma1.Border = PdfPCell.NO_BORDER;
            celdaFirma2.Border = PdfPCell.NO_BORDER;

            tablaFirmas.AddCell(celdaFirma1);
            tablaFirmas.AddCell(celdaFirma2);

            Documento.Add(tablaFirmas);
        }
    }
}
