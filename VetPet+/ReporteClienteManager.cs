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
    public class ReporteClienteManager : ReporteBase
    {
        string nombreReporte;
        string fecha1;
        string fecha2;
        string tipoReporte;

        public ReporteClienteManager(string nomRep, string fech1, string fech2, string tipoRep) : base(nomRep)
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
            if (tipoReporte == "01") tituloString = "Reporte de Dueños más Frecuentes";
            else if (tipoReporte == "02") tituloString = "Reporte de Mascotas más Frecuentes";
            else if (tipoReporte == "03") tituloString = "Reporte de Dueños menos Frecuentes";
            else if (tipoReporte == "04") tituloString = "Reporte de Mascotas menos frecuentes";


            Paragraph titulo = new Paragraph(tituloString, tituloFont);
            titulo.Alignment = Element.ALIGN_LEFT;
            Documento.Add(titulo);
            DateTime fechaZ = DateTime.ParseExact(fecha1, "yyyy-MM-dd", null);
            string fechaEmi1 = fechaZ.ToString("dd/MM/yyyy");
            DateTime fechaY = DateTime.ParseExact(fecha2, "yyyy-MM-dd", null);
            string fechaEmi2 = fechaY.ToString("dd/MM/yyyy");
            // 🔹 Agregar las fechas y el módulo
            Documento.Add(new Paragraph("Desde: " + fechaEmi1 + " – " + fechaEmi2, textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Módulo: Clientes", textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Emisión: " + DateTime.Now, textoFont));

            Documento.Add(new Paragraph("\n"));


            PdfPTable tabla = null;
            string[,] datos;
            // ESTO ES REDUCIBLE CON UN METODO
            if (tipoReporte == "01")
            {
                CrearTablasDueños(ConsultaRep01(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            }
            else if (tipoReporte == "02")
            {
                CrearTablasMascotas(ConsultaRep02(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            }
            else if (tipoReporte == "03")
            {
                CrearTablasDueños(ConsultaRep03(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);

            }
            else if (tipoReporte == "04")
            {
                CrearTablasMascotas(ConsultaRep04(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);

            }


            Documento.Add(tabla);
        }

        public string[,] ConsultaRep01(SqlConnection conex)
        {
            string[,] datos = new string[10, 7];
            try
            {
                conex.Open();
                string q = @"exec ObtenerDuenosConMasCitas @fechaInicio, @fechaFin";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string apellidoP = lector.GetString(1);
                    string apellidoM = lector.GetString(2);
                    string celular = lector.GetString(3);
                    string correo = lector.GetString(4);
                    string calle = lector.GetString(5);
                    int numVisitas = lector.GetInt32(6);

                    datos[i, 0] = nombre;
                    datos[i, 1] = apellidoP;
                    datos[i, 2] = apellidoM;
                    datos[i, 3] = celular;
                    datos[i, 4] = correo;
                    datos[i, 5] = calle;
                    datos[i, 6] = numVisitas.ToString();

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
            string[,] datos = new string[10, 7];
            try
            {
                conex.Open();
                string q = @"exec ObtenerMascotasFrecuentes @fechaInicio, @fechaFin;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string dueño = lector.GetString(1);
                    string especie = lector.GetString(2);
                    string raza = lector.GetString(3);
                    string sexo = lector.GetString(4);
                    string fechaNac = lector.GetDateTime(5).ToString("dd/MM/yyyy");
                    int numVisitas = lector.GetInt32(6);

                    datos[i, 0] = nombre;
                    datos[i, 1] = dueño;
                    datos[i, 2] = especie;
                    datos[i, 3] = raza;
                    datos[i, 4] = sexo;
                    datos[i, 5] = fechaNac;
                    datos[i, 6] = numVisitas.ToString();

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
            string[,] datos = new string[10, 7];
            try
            {
                conex.Open();
                string q = @"exec ObtenerDuenosConMenosCitas @fechaInicio, @fechaFin";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string apellidoP = lector.GetString(1);
                    string apellidoM = lector.GetString(2);
                    string celular = lector.GetString(3);
                    string correo = lector.GetString(4);
                    string calle = lector.GetString(5);
                    int numVisitas = lector.GetInt32(6);

                    datos[i, 0] = nombre;
                    datos[i, 1] = apellidoP;
                    datos[i, 2] = apellidoM;
                    datos[i, 3] = celular;
                    datos[i, 4] = correo;
                    datos[i, 5] = calle;
                    datos[i, 6] = numVisitas.ToString();

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
            string[,] datos = new string[10, 7];
            try
            {
                conex.Open();
                string q = @"exec ObtenerMascotasMenosFrecuentes @fechaInicio, @fechaFin;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string dueño = lector.GetString(1);
                    string especie = lector.GetString(2);
                    string raza = lector.GetString(3);
                    string sexo = lector.GetString(4);
                    string fechaNac = lector.GetDateTime(5).ToString("dd/MM/yyyy");
                    int numVisitas = lector.GetInt32(6);

                    datos[i, 0] = nombre;
                    datos[i, 1] = dueño;
                    datos[i, 2] = especie;
                    datos[i, 3] = raza;
                    datos[i, 4] = sexo;
                    datos[i, 5] = fechaNac;
                    datos[i, 6] = numVisitas.ToString();

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
        public void CrearTablasDueños(string[,] datos, ref PdfPTable tabla, Font tablaHeaderFont, Font tablaFont)
        {
            tabla = new PdfPTable(7);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1, 1, 1, 1, 1 });
            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Nombre", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Apellido Paterno", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Apellido Materno", tablaHeaderFont));
            PdfPCell header4 = new PdfPCell(new Phrase("Celular", tablaHeaderFont));
            PdfPCell header5 = new PdfPCell(new Phrase("Correo", tablaHeaderFont));
            PdfPCell header6 = new PdfPCell(new Phrase("Calle", tablaHeaderFont));
            PdfPCell header7 = new PdfPCell(new Phrase("Numero de Visitas", tablaHeaderFont));
            header1.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header3.HorizontalAlignment = Element.ALIGN_CENTER;
            header4.HorizontalAlignment = Element.ALIGN_CENTER;
            header5.HorizontalAlignment = Element.ALIGN_CENTER;
            header6.HorizontalAlignment = Element.ALIGN_CENTER;
            header7.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(header1);
            tabla.AddCell(header2);
            tabla.AddCell(header3);
            tabla.AddCell(header4);
            tabla.AddCell(header5);
            tabla.AddCell(header6);
            tabla.AddCell(header7);
            for (int i = 0; i < datos.GetLength(0); i++)
            {
                if (datos[i, 0] != null || datos[i, 1] != null)
                {
                    PdfPCell celda1 = new PdfPCell(new Phrase(datos[i, 0], tablaFont));
                    PdfPCell celda2 = new PdfPCell(new Phrase(datos[i, 1], tablaFont));
                    PdfPCell celda3 = new PdfPCell(new Phrase(datos[i, 2], tablaFont));
                    PdfPCell celda4 = new PdfPCell(new Phrase(datos[i, 3], tablaFont));
                    PdfPCell celda5 = new PdfPCell(new Phrase(datos[i, 4], tablaFont));
                    PdfPCell celda6 = new PdfPCell(new Phrase(datos[i, 5], tablaFont));
                    PdfPCell celda7 = new PdfPCell(new Phrase(datos[i, 6], tablaFont));
                    celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda4.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda5.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda6.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda7.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda1);
                    tabla.AddCell(celda2);
                    tabla.AddCell(celda3);
                    tabla.AddCell(celda4);
                    tabla.AddCell(celda5);
                    tabla.AddCell(celda6);
                    tabla.AddCell(celda7);
                }
            }
        }

        public void CrearTablasMascotas(string[,] datos, ref PdfPTable tabla, Font tablaHeaderFont, Font tablaFont)
        {
            tabla = new PdfPTable(7);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1, 1, 1, 1, 1 });
            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Nombre", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Dueño", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Especi", tablaHeaderFont));
            PdfPCell header4 = new PdfPCell(new Phrase("Raza", tablaHeaderFont));
            PdfPCell header5 = new PdfPCell(new Phrase("Sexo", tablaHeaderFont));
            PdfPCell header6 = new PdfPCell(new Phrase("Fecha de Nacimiento", tablaHeaderFont));
            PdfPCell header7 = new PdfPCell(new Phrase("Visitas", tablaHeaderFont));
            header1.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header3.HorizontalAlignment = Element.ALIGN_CENTER;
            header4.HorizontalAlignment = Element.ALIGN_CENTER;
            header5.HorizontalAlignment = Element.ALIGN_CENTER;
            header6.HorizontalAlignment = Element.ALIGN_CENTER;
            header7.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(header1);
            tabla.AddCell(header2);
            tabla.AddCell(header3);
            tabla.AddCell(header4);
            tabla.AddCell(header5);
            tabla.AddCell(header6);
            tabla.AddCell(header7);
            for (int i = 0; i < datos.GetLength(0); i++)
            {
                if (datos[i, 0] != null || datos[i, 1] != null)
                {
                    PdfPCell celda1 = new PdfPCell(new Phrase(datos[i, 0], tablaFont));
                    PdfPCell celda2 = new PdfPCell(new Phrase(datos[i, 1], tablaFont));
                    PdfPCell celda3 = new PdfPCell(new Phrase(datos[i, 2], tablaFont));
                    PdfPCell celda4 = new PdfPCell(new Phrase(datos[i, 3], tablaFont));
                    PdfPCell celda5 = new PdfPCell(new Phrase(datos[i, 4], tablaFont));
                    PdfPCell celda6 = new PdfPCell(new Phrase(datos[i, 5], tablaFont));
                    PdfPCell celda7 = new PdfPCell(new Phrase(datos[i, 6], tablaFont));
                    celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda4.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda5.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda6.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda7.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda1);
                    tabla.AddCell(celda2);
                    tabla.AddCell(celda3);
                    tabla.AddCell(celda4);
                    tabla.AddCell(celda5);
                    tabla.AddCell(celda6);
                    tabla.AddCell(celda7);
                }
            }
        }
    }
}
