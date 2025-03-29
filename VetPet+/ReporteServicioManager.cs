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
    public class ReporteServicioManager : ReporteBase
    {
        string nombreReporte;
        string fecha1;
        string fecha2;
        string tipoReporte;

        public ReporteServicioManager(string nomRep, string fech1, string fech2, string tipoRep) : base(nomRep)
        {
            nombreReporte = nomRep;
            fecha1 = fech1;
            fecha2 = fech2;
            tipoReporte = tipoRep;
        }

        protected override void AgregarContenido(string tipoReporte)
        {
            string tituloString = "";
            Font tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
            Font textoFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font tablaHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            Font tablaFont = FontFactory.GetFont(FontFactory.HELVETICA, 11);

            // 🔹 Agregar el título del reporte
            if (tipoReporte == "01") tituloString = "Reporte de Cirugias más Realizadas";
            else if (tipoReporte == "02") tituloString = "Reporte de Cirugias menos Realizadas";
            else if (tipoReporte == "03") tituloString = "Reporte de Servicios más Frecuentes";
            else if (tipoReporte == "04") tituloString = "Reporte de Servicios menos Frecuentes";


            Paragraph titulo = new Paragraph(tituloString, tituloFont);
            titulo.Alignment = Element.ALIGN_LEFT;
            Documento.Add(titulo);
            DateTime fechaZ = DateTime.ParseExact(fecha1, "yyyy-MM-dd", null);
            string fechaEmi1 = fechaZ.ToString("dd/MM/yyyy");
            DateTime fechaY = DateTime.ParseExact(fecha2, "yyyy-MM-dd", null);
            string fechaEmi2 = fechaY.ToString("dd/MM/yyyy");
            // 🔹 Agregar las fechas y el módulo
            Documento.Add(new Paragraph("Desde: " + fechaEmi1 + " – " + fechaEmi2, textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Módulo: Servicios", textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Emisión: " + DateTime.Now, textoFont));

            Documento.Add(new Paragraph("\n"));


            PdfPTable tabla = null;
            string[,] datos;
            // ESTO ES REDUCIBLE CON UN METODO
            if (tipoReporte == "01")
            {
                CrearTablasCirugias(ConsultaRep01(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            }
            else if (tipoReporte == "02")
            {
                CrearTablasCirugias(ConsultaRep02(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            }
            else if (tipoReporte == "03")
            {
                CrearTablasServicios(ConsultaRep03(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);

            }
            else if (tipoReporte == "04")
            {
                CrearTablasServicios(ConsultaRep04(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);

            }


            Documento.Add(tabla);
        }

        public string[,] ConsultaRep01(SqlConnection conex)
        {
            string[,] datos = new string[10, 3];
            try
            {
                conex.Open();
                string q = @"exec sp_CirugiasMasSolicitadas @fechaInicio, @fechaFin";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string cirugia = lector.GetString(0);
                    string tipoCirugia = lector.GetString(1);
                    int veces = lector.GetInt32(2);

                    datos[i, 0] = cirugia;
                    datos[i, 1] = tipoCirugia;
                    datos[i, 2] = veces.ToString();

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
            string[,] datos = new string[10, 3];
            try
            {
                conex.Open();
                string q = @"exec sp_CirugiasMenosSolicitadas @fechaInicio, @fechaFin";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string cirugia = lector.GetString(0);
                    string tipoCirugia = lector.GetString(1);
                    int veces = lector.GetInt32(2);

                    datos[i, 0] = cirugia;
                    datos[i, 1] = tipoCirugia;
                    datos[i, 2] = veces.ToString();

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
        public string[,] ConsultaRep03(SqlConnection conex)
        {
            string[,] datos = new string[10, 3];
            try
            {
                conex.Open();
                string q = @"exec ObtenerServiciosMasFrecuentes @fechaInicio, @fechaFin";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string servicio = lector.GetString(0);
                    string tipoServicio = lector.GetString(1);
                    int veces = lector.GetInt32(2);

                    datos[i, 0] = servicio;
                    datos[i, 1] = tipoServicio;
                    datos[i, 2] = veces.ToString();

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
        public string[,] ConsultaRep04(SqlConnection conex)
        {
            string[,] datos = new string[10, 3];
            try
            {
                conex.Open();
                string q = @"exec ObtenerServiciosMenosFrecuentes @fechaInicio, @fechaFin";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string servicio = lector.GetString(0);
                    string tipoServicio = lector.GetString(1);
                    int veces = lector.GetInt32(2);

                    datos[i, 0] = servicio;
                    datos[i, 1] = tipoServicio;
                    datos[i, 2] = veces.ToString();

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
        public void CrearTablasCirugias(string[,] datos, ref PdfPTable tabla, Font tablaHeaderFont, Font tablaFont)
        {
            tabla = new PdfPTable(3);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1});
            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Cirugía", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Tipo de Cirugía", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Veces", tablaHeaderFont));
            header1.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header3.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(header1);
            tabla.AddCell(header2);
            tabla.AddCell(header3);
            for (int i = 0; i < datos.GetLength(0); i++)
            {
                if (datos[i, 0] != null || datos[i, 1] != null)
                {
                    PdfPCell celda1 = new PdfPCell(new Phrase(datos[i, 0], tablaFont));
                    PdfPCell celda2 = new PdfPCell(new Phrase(datos[i, 1], tablaFont));
                    PdfPCell celda3 = new PdfPCell(new Phrase(datos[i, 2], tablaFont));
                    celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda1);
                    tabla.AddCell(celda2);
                    tabla.AddCell(celda3);
                }
            }
        }
        public void CrearTablasServicios(string[,] datos, ref PdfPTable tabla, Font tablaHeaderFont, Font tablaFont)
        {
            tabla = new PdfPTable(3);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1});
            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Servicio", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Tipo de Servicio", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Veces", tablaHeaderFont));
            header1.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header3.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(header1);
            tabla.AddCell(header2);
            tabla.AddCell(header3);
            for (int i = 0; i < datos.GetLength(0); i++)
            {
                if (datos[i, 0] != null || datos[i, 1] != null)
                {
                    PdfPCell celda1 = new PdfPCell(new Phrase(datos[i, 0], tablaFont));
                    PdfPCell celda2 = new PdfPCell(new Phrase(datos[i, 1], tablaFont));
                    PdfPCell celda3 = new PdfPCell(new Phrase(datos[i, 2], tablaFont));
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
}
