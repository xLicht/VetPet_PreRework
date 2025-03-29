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

        string NombreDueño;
        string NombreMascota;
        string Especie;
        string Raza;
        string FechaNacimiento;
        string Diagnostico;
        string Peso;
        string Temperatura;
        string Indicaciones;
        List<Tuple<int, string, int>> ListaMedicamentos;

        public RecetaManager(string nombreReceta, string nombreDueño, string nombreMascota, string especie, string raza, string fechaNacimiento,
        string diagnostico, string peso, string temperatura, string indicaciones,
        List<Tuple<int, string, int>> listaMedicamentos)
        {
            DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string carpetaReportes = Path.Combine(DirectorioProyecto, "Recetas-Arch");

            if (!Directory.Exists(carpetaReportes))
            {
                Directory.CreateDirectory(carpetaReportes);
            }

            // Genera el nombre dinámico del PDF
            RutaPDF = Path.Combine(carpetaReportes, nombreReceta + ".pdf");

            Documento = new Document(PageSize.CROWN_OCTAVO.Rotate());

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
        }
        public void GenerarReporte()
        {
            try
            {
                Writer = PdfWriter.GetInstance(Documento, new FileStream(RutaPDF, FileMode.Create));
                Documento.Open();

                AgregarEncabezado();
                AgregarContenido();
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
                Font textoFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7);

                string imagenPath = Path.Combine(DirectorioProyecto, "Resources", "VetPet_Logo1.png");
                if (!File.Exists(imagenPath))
                {
                    MessageBox.Show("La imagen VetPetLogo.png no se encontró en la carpeta Resources.");
                    return;
                }

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagenPath);
                img.ScaleAbsolute(70f, 70f);
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
        private void AgregarContenido()
        {
            string tituloString = "Receta Médica";
            Font tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13);
            Font fontText = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

            // Título
            Paragraph titulo = new Paragraph(tituloString, tituloFont);
            titulo.Alignment = Element.ALIGN_CENTER;
            Documento.Add(titulo);
            Documento.Add(new Paragraph("\n"));

            // Tabla de Información
            PdfPTable tablaInfo = null;
            CrearTablaInfo(ref tablaInfo, fontBold, fontText);
            Documento.Add(tablaInfo);
            Documento.Add(new Paragraph("\n"));

            // Diagnóstico
            Paragraph diagnostico = new Paragraph("Diagnóstico: " + Diagnostico, fontText);
            diagnostico.Alignment = Element.ALIGN_LEFT;
            Documento.Add(diagnostico);
            Documento.Add(new Paragraph("\n"));

            // Tabla para la sección de Medicamentos e Indicaciones
            PdfPTable tablaContenedor = new PdfPTable(2);
            tablaContenedor.WidthPercentage = 100;
            float[] columnWidths = { 50, 50 };
            tablaContenedor.SetWidths(columnWidths);

            // Crear la tabla de medicamentos
            PdfPTable tablaMedic = null;
            CrearTablaMedic(ref tablaMedic, fontBold, fontText);
            PdfPCell celdaMedic = new PdfPCell(tablaMedic);
            celdaMedic.Border = PdfPCell.NO_BORDER;
            tablaContenedor.AddCell(celdaMedic);

            // Crear celda de indicaciones
            Paragraph indicaciones = new Paragraph("Indicaciones: " + Indicaciones, fontText);
            PdfPCell celdaIndicaciones = new PdfPCell(indicaciones);
            celdaIndicaciones.Border = PdfPCell.NO_BORDER;
            celdaIndicaciones.VerticalAlignment = Element.ALIGN_TOP;
            celdaIndicaciones.PaddingLeft = 30f; // Espacio de 10 puntos entre la tabla y el texto

            tablaContenedor.AddCell(celdaIndicaciones);

            Documento.Add(tablaContenedor);

            PdfPTable tablaFirmas = new PdfPTable(2);
            tablaFirmas.WidthPercentage = 100;
            tablaFirmas.SetWidths(new float[] { 1, 1 });

            PdfPCell celdaFirma1 = new PdfPCell(new Phrase("\n\nDoctor: ______________________", fontText));
            PdfPCell celdaFirma2 = new PdfPCell(new Phrase("\n\nFirma del Doctor: ______________________", fontText));

            celdaFirma1.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaFirma2.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaFirma1.Border = PdfPCell.NO_BORDER;
            celdaFirma2.Border = PdfPCell.NO_BORDER;

            tablaFirmas.AddCell(celdaFirma1);
            tablaFirmas.AddCell(celdaFirma2);

            Documento.Add(tablaFirmas);
        }
        public void CrearTablaInfo(ref PdfPTable tabla, Font fontBold, Font fontText)
        {
            tabla = new PdfPTable(6);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1, 1, 1, 1});
            // Encabezados de la tabla
            PdfPCell mascotaH = new PdfPCell(new Phrase("Mascota: ", fontBold));
            PdfPCell mascotaI = new PdfPCell(new Phrase(NombreMascota, fontText));
            PdfPCell pesoH = new PdfPCell(new Phrase("Peso: ", fontBold));
            PdfPCell pesoI = new PdfPCell(new Phrase(Peso, fontText));
            PdfPCell fecNacH = new PdfPCell(new Phrase("Fecha Nacimiento: ", fontBold));
            PdfPCell fecNacI = new PdfPCell(new Phrase(FechaNacimiento, fontText));
            PdfPCell dueñoH = new PdfPCell(new Phrase("Dueño: ", fontBold));
            PdfPCell dueñoI = new PdfPCell(new Phrase(NombreDueño, fontText));
            PdfPCell espeRazaH = new PdfPCell(new Phrase("Especie y\nRaza: ", fontBold));
            PdfPCell espeRazaI = new PdfPCell(new Phrase(Especie+"\n"+Raza, fontText));
            PdfPCell fecConH = new PdfPCell(new Phrase("Fecha Consulta: ", fontBold));
            PdfPCell fecConI = new PdfPCell(new Phrase(DateTime.Now.ToString("dd/MM/yy"), fontText));
            mascotaH.HorizontalAlignment = Element.ALIGN_RIGHT;
            mascotaI.HorizontalAlignment = Element.ALIGN_LEFT;
            pesoH.HorizontalAlignment = Element.ALIGN_RIGHT;
            pesoI.HorizontalAlignment = Element.ALIGN_LEFT;
            fecNacH.HorizontalAlignment = Element.ALIGN_RIGHT;
            fecNacI.HorizontalAlignment = Element.ALIGN_LEFT;
            dueñoH.HorizontalAlignment = Element.ALIGN_RIGHT;
            dueñoI.HorizontalAlignment = Element.ALIGN_LEFT;
            espeRazaH.HorizontalAlignment = Element.ALIGN_RIGHT;
            espeRazaI.HorizontalAlignment = Element.ALIGN_LEFT;
            fecConH.HorizontalAlignment = Element.ALIGN_RIGHT;
            fecConI.HorizontalAlignment = Element.ALIGN_LEFT;
            tabla.AddCell(mascotaH);
            tabla.AddCell(mascotaI);
            tabla.AddCell(pesoH);
            tabla.AddCell(pesoI);
            tabla.AddCell(fecNacH);
            tabla.AddCell(fecNacI);
            tabla.AddCell(dueñoH);
            tabla.AddCell(dueñoI);
            tabla.AddCell(espeRazaH);
            tabla.AddCell(espeRazaI);
            tabla.AddCell(fecConH);
            tabla.AddCell(fecConI);
            
        }
        public void CrearTablaMedic(ref PdfPTable tabla, Font fontBold, Font fontText)
        {
            tabla = new PdfPTable(3);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 45;
            tabla.HorizontalAlignment = Element.ALIGN_LEFT;
            tabla.SetWidths(new float[] { 1, 1, 1});
            // Encabezados de la tabla
            PdfPCell idMed = new PdfPCell(new Phrase("Id", fontBold));
            PdfPCell nombre = new PdfPCell(new Phrase("Medicamento", fontBold));
            PdfPCell cantidad = new PdfPCell(new Phrase("Cantidad", fontBold));
            idMed.HorizontalAlignment = Element.ALIGN_CENTER;
            nombre.HorizontalAlignment = Element.ALIGN_CENTER;
            cantidad.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(idMed);
            tabla.AddCell(nombre);
            tabla.AddCell(cantidad);

            for (int i = 0; i < ListaMedicamentos.Count; i++)
            {
                PdfPCell celda1 = new PdfPCell(new Phrase(ListaMedicamentos[i].Item1.ToString(), fontText));
                PdfPCell celda2 = new PdfPCell(new Phrase(ListaMedicamentos[i].Item2.ToString(), fontText));
                PdfPCell celda3 = new PdfPCell(new Phrase(ListaMedicamentos[i].Item3.ToString(), fontText));
                celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celda1);
                tabla.AddCell(celda2);
                tabla.AddCell(celda3);
            }

        }
    }
}
