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
    public class ReporteAlmacenManager : ReporteBase
    {
        string nombreReporte;
        string fecha1;
        string fecha2;
        string tipoReporte;

        public ReporteAlmacenManager(string nomRep, string fech1, string fech2, string tipoRep) : base(nomRep)
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
            if (tipoReporte == "01") tituloString = "Reporte de Productos más Vendidos";
            else if (tipoReporte == "02") tituloString = "Reporte de Productos menos Vendidos";
            else if (tipoReporte == "03") tituloString = "Reporte de Medicamentos más Vendidos";
            else if (tipoReporte == "04") tituloString = "Reporte de Medicamentos menos Vendidos";
            else if (tipoReporte == "05") tituloString = "Reporte de Productos con Bajo Stock";
            else if (tipoReporte == "06") tituloString = "Reporte de Medicamentos con Bajo Stock";
            else if (tipoReporte == "07") tituloString = "Reporte de Proveedores con más Ventas";
            else if (tipoReporte == "08") tituloString = "Reporte de Proveedores con menos Ventas";

            Paragraph titulo = new Paragraph(tituloString, tituloFont);
            titulo.Alignment = Element.ALIGN_LEFT;
            Documento.Add(titulo);
            string fechaEmi1 = fecha1.Reverse().ToString().Replace("-", "/");
            string fechaEmi2 = fecha2.Reverse().ToString().Replace("-", "/");
            // 🔹 Agregar las fechas y el módulo
            Documento.Add(new Paragraph("Desde: " + fechaEmi1 + " – " + fechaEmi2, textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Módulo: Almacén", textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Emisión: " + DateTime.Now));

            Documento.Add(new Paragraph("\n"));
            Documento.Add(new Paragraph("\n"));


            PdfPTable tabla = null;
            string[,] datos;
            // ESTO ES REDUCIBLE CON UN METODO
            if (tipoReporte == "01")
            {
                datos = ConsultaRep01(ConexionSQL());
                // 🔹 Crear tabla con dos columnas (Razón - Veces)
                tabla = new PdfPTable(5);
                tabla.WidthPercentage = 80;
                tabla.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.SetWidths(new float[] { 1, 1 });
                // Encabezados de la tabla
                PdfPCell header1 = new PdfPCell(new Phrase("Nombre", tablaHeaderFont));
                PdfPCell header2 = new PdfPCell(new Phrase("Descripción", tablaHeaderFont));
                PdfPCell header3 = new PdfPCell(new Phrase("Precio del Proveedor", tablaHeaderFont));
                PdfPCell header4 = new PdfPCell(new Phrase("Precio a la Venta", tablaHeaderFont));
                PdfPCell header5 = new PdfPCell(new Phrase("Cantidad Vendida", tablaHeaderFont));
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
            }
            else if (tipoReporte == "02")
            {
                // 🔹 Crear tabla con dos columnas (Razón - Veces)
                tabla = new PdfPTable(5);
                tabla.WidthPercentage = 80;
                tabla.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.SetWidths(new float[] { 1, 1 });
                datos = ConsultaRep02(ConexionSQL());
                // Encabezados de la tabla
                PdfPCell header1 = new PdfPCell(new Phrase("Nombre", tablaHeaderFont));
                PdfPCell header2 = new PdfPCell(new Phrase("Descripción", tablaHeaderFont));
                PdfPCell header3 = new PdfPCell(new Phrase("Precio del Proveedor", tablaHeaderFont));
                PdfPCell header4 = new PdfPCell(new Phrase("Precio a la Venta", tablaHeaderFont));
                PdfPCell header5 = new PdfPCell(new Phrase("Cantidad Vendida", tablaHeaderFont));
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
            }
            else if (tipoReporte == "03")
            {
                // 🔹 Crear tabla con dos columnas (Razón - Veces)
                tabla = new PdfPTable(6);
                tabla.WidthPercentage = 80;
                tabla.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.SetWidths(new float[] { 1, 1 });
                datos = ConsultaRep03(ConexionSQL());
                // Encabezados de la tabla
                PdfPCell header1 = new PdfPCell(new Phrase("Nombre Generico", tablaHeaderFont));
                PdfPCell header2 = new PdfPCell(new Phrase("Presentación", tablaHeaderFont));
                PdfPCell header3 = new PdfPCell(new Phrase("Vía de Administración", tablaHeaderFont));
                PdfPCell header4 = new PdfPCell(new Phrase("Precio del Proveedor", tablaHeaderFont));
                PdfPCell header5 = new PdfPCell(new Phrase("Precio a la Venta", tablaHeaderFont));
                PdfPCell header6 = new PdfPCell(new Phrase("Cantidad Vendida", tablaHeaderFont));
                header1.HorizontalAlignment = Element.ALIGN_CENTER;
                header2.HorizontalAlignment = Element.ALIGN_CENTER;
                header3.HorizontalAlignment = Element.ALIGN_CENTER;
                header4.HorizontalAlignment = Element.ALIGN_CENTER;
                header5.HorizontalAlignment = Element.ALIGN_CENTER;
                header6.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(header1);
                tabla.AddCell(header2);
                tabla.AddCell(header3);
                tabla.AddCell(header4);
                tabla.AddCell(header5);
                tabla.AddCell(header6);
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
                        celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda4.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda5.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda6.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla.AddCell(celda1);
                        tabla.AddCell(celda2);
                        tabla.AddCell(celda3);
                        tabla.AddCell(celda4);
                        tabla.AddCell(celda5);
                        tabla.AddCell(celda6);
                    }
                }

            }
            else if (tipoReporte == "04")
            {
                // 🔹 Crear tabla con dos columnas (Razón - Veces)
                tabla = new PdfPTable(6);
                tabla.WidthPercentage = 80;
                tabla.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.SetWidths(new float[] { 1, 1 });
                datos = ConsultaRep04(ConexionSQL());
                // Encabezados de la tabla
                PdfPCell header1 = new PdfPCell(new Phrase("Nombre Generico", tablaHeaderFont));
                PdfPCell header2 = new PdfPCell(new Phrase("Presentación", tablaHeaderFont));
                PdfPCell header3 = new PdfPCell(new Phrase("Vía de Administración", tablaHeaderFont));
                PdfPCell header4 = new PdfPCell(new Phrase("Precio del Proveedor", tablaHeaderFont));
                PdfPCell header5 = new PdfPCell(new Phrase("Precio a la Venta", tablaHeaderFont));
                PdfPCell header6 = new PdfPCell(new Phrase("Cantidad Vendida", tablaHeaderFont));
                header1.HorizontalAlignment = Element.ALIGN_CENTER;
                header2.HorizontalAlignment = Element.ALIGN_CENTER;
                header3.HorizontalAlignment = Element.ALIGN_CENTER;
                header4.HorizontalAlignment = Element.ALIGN_CENTER;
                header5.HorizontalAlignment = Element.ALIGN_CENTER;
                header6.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(header1);
                tabla.AddCell(header2);
                tabla.AddCell(header3);
                tabla.AddCell(header4);
                tabla.AddCell(header5);
                tabla.AddCell(header6);
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
                        celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda4.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda5.HorizontalAlignment = Element.ALIGN_CENTER;
                        celda6.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla.AddCell(celda1);
                        tabla.AddCell(celda2);
                        tabla.AddCell(celda3);
                        tabla.AddCell(celda4);
                        tabla.AddCell(celda5);
                        tabla.AddCell(celda6);
                    }
                }
            }
            //else if (tipoReporte == "05")
            //    datos = ConsultaRep05(ConexionSQL());
            //else if (tipoReporte == "06")
            //    datos = ConsultaRep06(ConexionSQL());
            //else if (tipoReporte == "07")
            //    datos = ConsultaRep07(ConexionSQL());
            //else if (tipoReporte == "08")
            //    datos = ConsultaRep08(ConexionSQL());

            

            Documento.Add(tabla);
        }
        public string[,] ConsultaRep01(SqlConnection conex)
        {
            string[,] datos = new string[10, 5];
            try
            {
                conex.Open();
                string q = @"SELECT TOP 10 P.nombre AS Nombre,P.descripcion AS Descripcion, P.precioProveedor AS [Precio del proveedor], P.precioVenta AS [Precio a la Venta], COUNT(VP.idProducto) AS [Cantidad vendida]\r\nFROM Producto P JOIN Venta_Producto VP ON P.idProducto = VP.idProducto JOIN Venta V ON VP.idVenta = V.idVenta\r\nWHERE P.idTipoProducto <> 3 AND V.fechaRegistro BETWEEN @fechaInicio AND @fechaFin\r\nGROUP BY P.nombre, P.descripcion, P.precioProveedor, P.precioVenta ORDER BY [Cantidad vendida] DESC;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string descripcion = lector.GetString(1);
                    decimal precioProveedor = lector.GetDecimal(2);
                    decimal precioVenta = lector.GetDecimal(3);
                    int cantidadVendida = lector.GetInt32(4);

                    datos[i, 0] = nombre;
                    datos[i, 1] = descripcion;
                    datos[i, 2] = precioProveedor.ToString();
                    datos[i, 3] = precioVenta.ToString();
                    datos[i, 4] = cantidadVendida.ToString();
                    
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
                string q = @"SELECT TOP 10 P.nombre AS Nombre,P.descripcion AS Descripcion, P.precioProveedor AS [Precio del proveedor], P.precioVenta AS [Precio a la Venta], COUNT(VP.idProducto) AS [Cantidad vendida]\r\nFROM Producto P JOIN Venta_Producto VP ON P.idProducto = VP.idProducto JOIN Venta V ON VP.idVenta = V.idVenta\r\nWHERE P.idTipoProducto <> 3 AND V.fechaRegistro BETWEEN @fechaInicio AND @fechaFin\r\nGROUP BY P.nombre, P.descripcion, P.precioProveedor, P.precioVenta ORDER BY [Cantidad vendida] ASC;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string descripcion = lector.GetString(1);
                    decimal precioProveedor = lector.GetDecimal(2);
                    decimal precioVenta = lector.GetDecimal(3);
                    int cantidadVendida = lector.GetInt32(4);

                    datos[i, 0] = nombre;
                    datos[i, 1] = descripcion;
                    datos[i, 2] = precioProveedor.ToString();
                    datos[i, 3] = precioVenta.ToString();
                    datos[i, 4] = cantidadVendida.ToString();

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
            string[,] datos = new string[10, 6];
            try
            {
                conex.Open();
                string q = @"SELECT M.nombreGenérico AS [Nombre Generico], P.nombre AS Presentacion, VA.nombre AS [Via de Administracion], PR.precioProveedor AS [Precio del proveedor], PR.precioVenta AS [Precio a la venta], COUNT(VP.idProducto) AS [Cantidad vendida]\r\nFROM Medicamento M JOIN Producto PR ON M.idProducto = PR.idProducto JOIN Presentacion P ON M.idPresentacion = P.idPresentacion JOIN ViaAdministracion VA ON M.idViaAdministracion = VA.idViaAdministracion JOIN  Venta_Producto VP ON PR.idProducto = VP.idProducto JOIN Venta V ON VP.idVenta = V.idVenta\r\nWHERE PR.idTipoProducto = 3 AND V.fechaRegistro BETWEEN @fechaInicio AND @fechaFin\r\nGROUP BY M.nombreGenérico, P.nombre, VA.nombre, PR.precioProveedor, PR.precioVenta ORDER BY [Cantidad vendida] DESC;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombreGen = lector.GetString(0);
                    string presentacion = lector.GetString(1);
                    string viaAdmin = lector.GetString(2);
                    decimal precioProveedor = lector.GetDecimal(3);
                    decimal precioVenta = lector.GetDecimal(4);
                    int cantidadVendida = lector.GetInt32(5);

                    datos[i, 0] = nombreGen;
                    datos[i, 1] = presentacion;
                    datos[i, 2] = viaAdmin;
                    datos[i, 3] = precioProveedor.ToString();
                    datos[i, 4] = precioVenta.ToString();
                    datos[i, 5] = cantidadVendida.ToString();

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
            string[,] datos = new string[10, 6];
            try
            {
                conex.Open();
                string q = @"SELECT M.nombreGenérico AS [Nombre Generico], P.nombre AS Presentacion, VA.nombre AS [Via de Administracion], PR.precioProveedor AS [Precio del proveedor], PR.precioVenta AS [Precio a la venta], COUNT(VP.idProducto) AS [Cantidad vendida]\r\nFROM Medicamento M JOIN Producto PR ON M.idProducto = PR.idProducto JOIN Presentacion P ON M.idPresentacion = P.idPresentacion JOIN ViaAdministracion VA ON M.idViaAdministracion = VA.idViaAdministracion JOIN  Venta_Producto VP ON PR.idProducto = VP.idProducto JOIN Venta V ON VP.idVenta = V.idVenta\r\nWHERE PR.idTipoProducto = 3 AND V.fechaRegistro BETWEEN @fechaInicio AND @fechaFin\r\nGROUP BY M.nombreGenérico, P.nombre, VA.nombre, PR.precioProveedor, PR.precioVenta ORDER BY [Cantidad vendida] ASC;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombreGen = lector.GetString(0);
                    string presentacion = lector.GetString(1);
                    string viaAdmin = lector.GetString(2);
                    decimal precioProveedor = lector.GetDecimal(3);
                    decimal precioVenta = lector.GetDecimal(4);
                    int cantidadVendida = lector.GetInt32(5);

                    datos[i, 0] = nombreGen;
                    datos[i, 1] = presentacion;
                    datos[i, 2] = viaAdmin;
                    datos[i, 3] = precioProveedor.ToString();
                    datos[i, 4] = precioVenta.ToString();
                    datos[i, 5] = cantidadVendida.ToString();

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
