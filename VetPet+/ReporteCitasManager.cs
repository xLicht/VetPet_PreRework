using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pruebas_PDF
{
    public class ReportesCitasManager : ReporteBase
    {
        string nombreReporte;
        string fecha1;
        string fecha2;
        string tipoReporte;
        public ReportesCitasManager(string nomRep, string fech1, string fech2, string tipoRep) : base(nomRep)
        {
            nombreReporte = nomRep;
            fecha1 = fech1;
            fecha2 = fech2;
            tipoReporte = tipoRep;
        }

        protected override void AgregarContenido(string tipoReporte)
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
            Documento.Add(new Paragraph("Emisión: " + DateTime.Now));

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

            string[,] datos = null;
            if (tipoReporte == "01")
                datos = ConsultaRep01(ConexionSQL());
            else if (tipoReporte == "02")
                datos = ConsultaRep02(ConexionSQL());

            for (int i = 0; i < datos.GetLength(0); i++)
            {
                if (datos[i, 0] != null || datos[i, 1] != null)
                {
                    PdfPCell celda1 = new PdfPCell(new Phrase(datos[i, 0], tablaFont));
                    PdfPCell celda2 = new PdfPCell(new Phrase(datos[i, 1], tablaFont));
                    celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda1);
                    tabla.AddCell(celda2);
                }
            }

            Documento.Add(tabla);
        }
        
        public string[,] ConsultaRep01(SqlConnection conex)
        {
            string[,] datos = new string[10, 2];
            try
            {
                conex.Open();
                string q = "SELECT TOP 10 m.nombre AS Razon, COUNT(c.idMotivo) AS cantidad\r\nFROM Cita c\r\nJOIN Motivo m ON c.idMotivo = m.idMotivo\r\nWHERE c.fechaRegistro BETWEEN '2025-03-01' AND '2025-03-08'\r\nGROUP BY m.nombre\r\nORDER BY cantidad DESC;";
                SqlCommand comando = new SqlCommand(q, conex);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string razon = lector.GetString(0);
                    int cantidad = lector.GetInt32(1);

                    datos[i, 0] = razon;
                    datos[i, 1] = cantidad.ToString();
                    i++;
                }
                conex.Close();
                return datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conex.Close();
                return datos;
            }
        }
        public string[,] ConsultaRep02(SqlConnection conex)
        {
            string[,] datos = new string[10, 2];
            try
            {
                conex.Open();
                string q = "SELECT TOP 10 m.nombre AS Razon, COUNT(c.idMotivo) AS cantidad\r\nFROM Cita c\r\nJOIN Motivo m ON c.idMotivo = m.idMotivo\r\nWHERE c.fechaRegistro BETWEEN '2025-03-01' AND '2025-03-08'\r\nGROUP BY m.nombre\r\nORDER BY cantidad ASC;";
                SqlCommand comando = new SqlCommand(q, conex);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string razon = lector.GetString(0);
                    int cantidad = lector.GetInt32(1);

                    datos[i, 0] = razon;
                    datos[i, 1] = cantidad.ToString();
                    i++;
                }
                conex.Close();
                return datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conex.Close();
                return datos;
            }
        }

    }
}
