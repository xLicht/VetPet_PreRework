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
using System.Runtime.Remoting.Metadata.W3cXsd2001;

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
            Font tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
            Font textoFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font tablaHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            Font tablaFont = FontFactory.GetFont(FontFactory.HELVETICA, 11);

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
            DateTime fechaZ = DateTime.ParseExact(fecha1, "yyyy-MM-dd", null);
            string fechaEmi1 = fechaZ.ToString("dd/MM/yyyy");
            DateTime fechaY = DateTime.ParseExact(fecha2, "yyyy-MM-dd", null);
            string fechaEmi2 = fechaY.ToString("dd/MM/yyyy");
            // 🔹 Agregar las fechas y el módulo
            Documento.Add(new Paragraph("Desde: " + fechaEmi1 + " – " + fechaEmi2, textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Módulo: Almacén", textoFont) { Alignment = Element.ALIGN_LEFT });
            Documento.Add(new Paragraph("Emisión: " + DateTime.Now, textoFont));

            Documento.Add(new Paragraph("\n"));


            PdfPTable tabla = null;
            string[,] datos;
            // ESTO ES REDUCIBLE CON UN METODO
            if (tipoReporte == "01")
            {
                CrearTablasProductos(ConsultaRep01(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            }
            else if (tipoReporte == "02")
            {
                CrearTablasProductos(ConsultaRep02(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            }
            else if (tipoReporte == "03")
            {
                CrearTablasMedicamentos(ConsultaRep03(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);

            }
            else if (tipoReporte == "04")
            {
                CrearTablasMedicamentos(ConsultaRep04(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);

            }
            else if (tipoReporte == "05")
                CrearTablasStock(ConsultaRep05(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            else if (tipoReporte == "06")
                CrearTablasStock(ConsultaRep06(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            else if (tipoReporte == "07")
                CrearTablasProveedores(ConsultaRep07(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);
            else if (tipoReporte == "08")
                CrearTablasProveedores(ConsultaRep08(ConexionSQL()), ref tabla, tablaHeaderFont, tablaFont);


            Documento.Add(tabla);
        }
        #region Consultas
        public string[,] ConsultaRep01(SqlConnection conex)
        {
            string[,] datos = new string[10, 4];
            try
            {
                conex.Open();
                string q = @"EXEC sp_ProductosMasVendidos @fechaInicio, @fechaFin;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string descripcion = lector.GetString(1);
                    decimal precioVenta = lector.GetDecimal(2);
                    int cantidadVendida = lector.GetInt32(3);

                    datos[i, 0] = nombre;
                    datos[i, 1] = descripcion;
                    datos[i, 2] = precioVenta.ToString();
                    datos[i, 3] = cantidadVendida.ToString();
                    
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
            string[,] datos = new string[10, 4];
            try
            {
                conex.Open();
                string q = @"EXEC sp_ProductosMenosVendidos @fechaInicio, @fechaFin;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string descripcion = lector.GetString(1);
                    decimal precioVenta = lector.GetDecimal(2);
                    int cantidadVendida = lector.GetInt32(3);

                    datos[i, 0] = nombre;
                    datos[i, 1] = descripcion;
                    datos[i, 2] = precioVenta.ToString();
                    datos[i, 3] = cantidadVendida.ToString();

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
            string[,] datos = new string[10, 5];
            try
            {
                conex.Open();
                string q = @"EXEC  sp_MedicamentosMasVendidos @fechaInicio, @fechaFin;";
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
                    decimal precioVenta = lector.GetDecimal(3);
                    int cantidadVendida = lector.GetInt32(4);

                    datos[i, 0] = nombreGen;
                    datos[i, 1] = presentacion;
                    datos[i, 2] = viaAdmin;
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
        public string[,] ConsultaRep04(SqlConnection conex)
        {
            string[,] datos = new string[10, 5];
            try
            {
                conex.Open();
                string q = @"EXEC sp_MedicamentosMenosVendidos @fechaInicio, @fechaFin;";
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
                    decimal precioVenta = lector.GetDecimal(3);
                    int cantidadVendida = lector.GetInt32(4);

                    datos[i, 0] = nombreGen;
                    datos[i, 1] = presentacion;
                    datos[i, 2] = viaAdmin;
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
        public string[,] ConsultaRep05(SqlConnection conex)
        {
            string[,] datos = new string[10, 4];
            try
            {
                conex.Open();
                string q = @"exec sp_ProductosBajoStock";
                SqlCommand comando = new SqlCommand(q, conex);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombreGen = lector.GetString(0);
                    string descripcion = lector.GetString(1);
                    decimal precioVenta = lector.GetDecimal(2);
                    int stock = lector.GetInt32(3);

                    datos[i, 0] = nombreGen;
                    datos[i, 1] = descripcion;
                    datos[i, 2] = precioVenta.ToString();
                    datos[i, 3] = stock.ToString();

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
        public string[,] ConsultaRep06(SqlConnection conex)
        {
            string[,] datos = new string[10, 4];
            try
            {
                conex.Open();
                string q = @"exec sp_MedicamentosBajoStock";
                SqlCommand comando = new SqlCommand(q, conex);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombreGen = lector.GetString(0);
                    string descripcion = lector.GetString(1);
                    decimal precioVenta = lector.GetDecimal(2);
                    int stock = lector.GetInt32(3);

                    datos[i, 0] = nombreGen;
                    datos[i, 1] = descripcion;
                    datos[i, 2] = precioVenta.ToString();
                    datos[i, 3] = stock.ToString();

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
        public string[,] ConsultaRep07(SqlConnection conex)
        {
            string[,] datos = new string[10, 5];
            try
            {
                conex.Open();
                string q = @"EXEC sp_ProveedoresMasVentas @fechaInicio, @fechaFin;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombreProv = lector.GetString(0);
                    string nombreConta = lector.GetString(1);
                    string correoElectronico = lector.GetString(2);
                    string celular = lector.GetString(3);
                    int cantidadVenta = lector.GetInt32(4);

                    datos[i, 0] = nombreProv;
                    datos[i, 1] = nombreConta;
                    datos[i, 2] = correoElectronico;
                    datos[i, 3] = celular;
                    datos[i, 4] = cantidadVenta.ToString();

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
        public string[,] ConsultaRep08(SqlConnection conex)
        {
            string[,] datos = new string[10, 5];
            try
            {
                conex.Open();
                string q = @"exec sp_ProveedoresMenosVentas @fechaInicio, @fechaFin;";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@fechaInicio", fecha1);
                comando.Parameters.AddWithValue("@fechaFin", fecha2);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombreProv = lector.GetString(0);
                    string nombreConta = lector.GetString(1);
                    string correoElectronico = lector.GetString(2);
                    string celular = lector.GetString(3);
                    int cantidadVenta = lector.GetInt32(4);

                    datos[i, 0] = nombreProv;
                    datos[i, 1] = nombreConta;
                    datos[i, 2] = correoElectronico;
                    datos[i, 3] = celular;
                    datos[i, 4] = cantidadVenta.ToString();

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
        #endregion
        #region CreacionDeTablas
        public void CrearTablasProductos(string[,] datos, ref PdfPTable tabla, Font tablaHeaderFont, Font tablaFont)
        {
            tabla = new PdfPTable(4);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1, 1});
            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Nombre", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Descripción", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Precio a la Venta", tablaHeaderFont));
            PdfPCell header4 = new PdfPCell(new Phrase("Cantidad Vendida", tablaHeaderFont));
            header1.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header3.HorizontalAlignment = Element.ALIGN_CENTER;
            header4.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(header1);
            tabla.AddCell(header2);
            tabla.AddCell(header3);
            tabla.AddCell(header4);
            for (int i = 0; i < datos.GetLength(0); i++)
            {
                if (datos[i, 0] != null || datos[i, 1] != null)
                {
                    PdfPCell celda1 = new PdfPCell(new Phrase(datos[i, 0], tablaFont));
                    PdfPCell celda2 = new PdfPCell(new Phrase(datos[i, 1], tablaFont));
                    PdfPCell celda3 = new PdfPCell(new Phrase(datos[i, 2], tablaFont));
                    PdfPCell celda4 = new PdfPCell(new Phrase(datos[i, 3], tablaFont));
                    celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda4.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda1);
                    tabla.AddCell(celda2);
                    tabla.AddCell(celda3);
                    tabla.AddCell(celda4);
                }
            }
        }
        public void CrearTablasStock(string[,] datos, ref PdfPTable tabla, Font tablaHeaderFont, Font tablaFont)
        {
            tabla = new PdfPTable(4);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1, 1});
            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Nombre", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Descripción", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Precio a la Venta", tablaHeaderFont));
            PdfPCell header4 = new PdfPCell(new Phrase("Stock Actual", tablaHeaderFont));
            header1.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header3.HorizontalAlignment = Element.ALIGN_CENTER;
            header4.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(header1);
            tabla.AddCell(header2);
            tabla.AddCell(header3);
            tabla.AddCell(header4);
            for (int i = 0; i < datos.GetLength(0); i++)
            {
                if (datos[i, 0] != null || datos[i, 1] != null)
                {
                    PdfPCell celda1 = new PdfPCell(new Phrase(datos[i, 0], tablaFont));
                    PdfPCell celda2 = new PdfPCell(new Phrase(datos[i, 1], tablaFont));
                    PdfPCell celda3 = new PdfPCell(new Phrase(datos[i, 2], tablaFont));
                    PdfPCell celda4 = new PdfPCell(new Phrase(datos[i, 3], tablaFont));
                    celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda4.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda1);
                    tabla.AddCell(celda2);
                    tabla.AddCell(celda3);
                    tabla.AddCell(celda4);
                }
            }
        }
        public void CrearTablasMedicamentos(string[,] datos, ref PdfPTable tabla, Font tablaHeaderFont, Font tablaFont)
        {
            tabla = new PdfPTable(5);
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1, 1, 1});
            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Nombre Generico", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Presentación", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Vía de Administración", tablaHeaderFont));
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
        public void CrearTablasProveedores(string[,] datos, ref PdfPTable tabla, Font tablaHeaderFont, Font tablaFont)
        {
            tabla = new PdfPTable(5);
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1, 1, 1, 1});
            // Encabezados de la tabla
            PdfPCell header1 = new PdfPCell(new Phrase("Nombre del Proveedor", tablaHeaderFont));
            PdfPCell header2 = new PdfPCell(new Phrase("Nombre del Contacto", tablaHeaderFont));
            PdfPCell header3 = new PdfPCell(new Phrase("Correo Electronico", tablaHeaderFont));
            PdfPCell header4 = new PdfPCell(new Phrase("Celular", tablaHeaderFont));
            PdfPCell header5 = new PdfPCell(new Phrase("Numero de Pedidos", tablaHeaderFont));
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
        #endregion

    }
}
