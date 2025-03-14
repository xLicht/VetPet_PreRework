using iTextSharp.text.pdf;
using iTextSharp.text;
using Pruebas_PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VetPet_
{
    public class ReporteVentasManager : ReporteBase
    {
        string nombreReporte;
        string fecha1;
        string fecha2;
        string tipoReporte;

        public ReporteVentasManager(string nomRep, string fech1, string fech2, string tipoRep) : base(nomRep)
        {
            nombreReporte = nomRep;
            fecha1 = fech1;
            fecha2 = fech2;
            tipoReporte = tipoRep;
        }

        protected override void AgregarContenido(string tipoReporte)
        {
            string tituloString = "";
            Font tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Font textoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Font tablaHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font tablaFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            // 🔹 Agregar el título del reporte
            if (tipoReporte == "01") tituloString = "Reporte de Ventas más Altas";
            else if (tipoReporte == "02") tituloString = "Reporte de Ventas más Bajas";

            Paragraph titulo = new Paragraph(tituloString, tituloFont);
            titulo.Alignment = Element.ALIGN_LEFT;
            Documento.Add(titulo);

            string fechaEmi1 = fecha1.Reverse().ToString().Replace("-", "/");
            string fechaEmi2 = fecha2.Reverse().ToString().Replace("-", "/");
            // 🔹 Agregar las fechas y el módulo
            Documento.Add(new Paragraph("Desde: " + fechaEmi1 + " – " + fechaEmi2, textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Módulo: Ventas", textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Emisión: " + DateTime.Now));

            Documento.Add(new Paragraph("\n"));
            Documento.Add(new Paragraph("\n"));

            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 80;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1 });

            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("IdVenta", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Fecha", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Cliente", tablaHeaderFont));
            PdfPCell header4 = new PdfPCell(new Phrase("Empleado", tablaHeaderFont));
            PdfPCell header5 = new PdfPCell(new Phrase("Importe", tablaHeaderFont));
            header1.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header3.HorizontalAlignment = Element.ALIGN_CENTER;
            header4.HorizontalAlignment = Element.ALIGN_CENTER;
            header5.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(header1);
            tabla.AddCell(header2);
            tabla.AddCell(header3);
            tabla.AddCell(header4);
            tabla.AddCell(header5);

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
                    PdfPCell celda3 = new PdfPCell(new Phrase(datos[i, 2], tablaFont));
                    PdfPCell celda4 = new PdfPCell(new Phrase(datos[i, 3], tablaFont));
                    PdfPCell celda5 = new PdfPCell(new Phrase(datos[i, 4], tablaFont));
                    celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda4.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda5.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda1);
                    tabla.AddCell(celda2);
                    tabla.AddCell(celda3);
                    tabla.AddCell(celda4);
                    tabla.AddCell(celda5);
                }
            }

            Documento.Add(tabla);
        }
        public string[,] ConsultaRep01(SqlConnection conex)
        {
            string[,] datos = new string[10, 5];
            try
            {
                conex.Open();
                string q = @"SELECT V.idVenta AS [IdVenta], V.fechaRegistro AS [Fecha de la Venta], CONCAT(P.nombre, ' ', P.apellidoP, ' ', P.apellidoM) AS Cliente, E.usuario AS Empleado, V.total AS Importe\r\nFROM Venta V LEFT JOIN Persona P ON V.idPersona = P.idPersona LEFT JOIN Empleado E ON V.idEmpleado = E.idEmpleado\r\nWHERE V.fechaRegistro BETWEEN @fechaInicio AND @fechaFin ORDER BY V.total DESC;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    int IdVenta = lector.GetInt32(0);
                    string fechaVenta = lector.GetString(1);
                    string cliente = lector.GetString(2);
                    string empleado = lector.GetString(3);
                    decimal importe = lector.GetDecimal(4);

                    datos[i, 0] = IdVenta.ToString();
                    datos[i, 1] = fechaVenta;
                    datos[i, 2] = cliente;
                    datos[i, 3] = empleado;
                    datos[i, 4] = importe.ToString();
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
            string[,] datos = new string[10, 5];
            try
            {
                conex.Open();
                string q = @"SELECT V.idVenta AS [IdVenta], V.fechaRegistro AS [Fecha de la Venta], CONCAT(P.nombre, ' ', P.apellidoP, ' ', P.apellidoM) AS Cliente, E.usuario AS Empleado, V.total AS Importe\r\nFROM Venta V LEFT JOIN Persona P ON V.idPersona = P.idPersona LEFT JOIN Empleado E ON V.idEmpleado = E.idEmpleado\r\nWHERE V.fechaRegistro BETWEEN @fechaInicio AND @fechaFin ORDER BY V.total ASC;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    int IdVenta = lector.GetInt32(0);
                    string fechaVenta = lector.GetString(1);
                    string cliente = lector.GetString(2);
                    string empleado = lector.GetString(3);
                    decimal importe = lector.GetDecimal(4);

                    datos[i, 0] = IdVenta.ToString();
                    datos[i, 1] = fechaVenta;
                    datos[i, 2] = cliente;
                    datos[i, 3] = empleado;
                    datos[i, 4] = importe.ToString();
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
