using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Pruebas_PDF
{
    public class ReportesCitasManager : ReporteBase
    {
        public ReportesCitasManager(string idReporte) : base("ReporteCitas", idReporte) { }

        protected override void AgregarContenido()
        {
            Font tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Font textoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Font tablaHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font tablaFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            // 🔹 Agregar el título del reporte
            Paragraph titulo = new Paragraph("Reporte de Razón de Cita más Frecuentes", tituloFont);
            titulo.Alignment = Element.ALIGN_LEFT;
            Documento.Add(titulo);

            // 🔹 Agregar las fechas y el módulo
            Documento.Add(new Paragraph("Desde: 01/01/2025 – 01/02/2025", textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Módulo: Citas", textoFont) { Alignment = Element.ALIGN_LEFT });

            Documento.Add(new Paragraph("\n"));
            Documento.Add(new Paragraph("\n"));

            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 80;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1 });

            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Razón", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Veces", tablaHeaderFont));
            header1.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(header1);
            tabla.AddCell(header2);

            // Datos de ejemplo (reemplazar con datos de la BD)
            string[,] datos = {
                { "Consulta General", "15" },
                { "Vacunación", "10" },
                { "Desparasitación", "8" },
                { "Cirugía", "5" }
            };

            for (int i = 0; i < datos.GetLength(0); i++)
            {
                PdfPCell celda1 = new PdfPCell(new Phrase(datos[i, 0], tablaFont));
                PdfPCell celda2 = new PdfPCell(new Phrase(datos[i, 1], tablaFont));
                celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celda1);
                tabla.AddCell(celda2);
            }

            Documento.Add(tabla);
        }
    }
}
