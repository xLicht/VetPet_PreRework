using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Patagames.Pdf.Enums;

namespace VetPet_
{
    public class TicketsManager
    {
        protected Document Documento { get; private set; }
        protected PdfWriter Writer { get; private set; }
        protected string RutaPDF { get; private set; }
        protected string DirectorioProyecto { get; private set; }

        int idVenta;
        int? idDueño;
        string nombreRecepcionista;
        string nombreDueño;
        string nombreMascota;
        string fechaVenta;
        string horaVenta;
        List<Tuple<string, decimal, int>> listaServicios;
        List<Tuple<string, decimal, int>> listaProductos;
        string totalVenta;
        string totalEfectivo;
        string totalTarjeta;

        string idFactura;
        string direccionDueño;
        string celularDueño;
        string correoDueño;
        List<Tuple<string, decimal, int>> listaVenta;

        public TicketsManager(int idVenta, int? idDueño, string nombreTicket, string nombreRecepcionista, string nombreDueño, string nombreMascota, string fechaVenta, List<Tuple<string, decimal, int>> listaServicios, List<Tuple<string, decimal, int>> listaProductos, string totalVenta, string totalEfectivo, string totalTarjeta)
        {
            DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string carpetaReportes = Path.Combine(DirectorioProyecto, "Tickets-Arch");

            if (!Directory.Exists(carpetaReportes))
            {
                Directory.CreateDirectory(carpetaReportes);
            }

            // Genera el nombre dinámico del PDF
            RutaPDF = Path.Combine(carpetaReportes, nombreTicket + ".pdf");

            Documento = new Document(new Rectangle(283.5f, 567f));

            this.idVenta = idVenta;
            this.idDueño = idDueño;
            this.nombreRecepcionista = nombreRecepcionista;
            this.nombreDueño = nombreDueño;
            this.nombreMascota = nombreMascota;
            this.fechaVenta = fechaVenta;
            this.listaServicios = listaServicios;
            this.listaProductos = listaProductos;
            this.totalVenta = totalVenta;
            this.totalEfectivo = totalEfectivo;
            this.totalTarjeta = totalTarjeta;



        }
        public TicketsManager(string NombreFactura, int IdVenta, string IdFactura, string FechaVenta, string NombreDueño, string DireccionDueño, string CelularDueño, string CorreoDueño, List<Tuple<string, decimal, int>> ListaVenta, string Total)
        {
            DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string carpetaReportes = Path.Combine(DirectorioProyecto, "Facturas-Arch");

            if (!Directory.Exists(carpetaReportes))
            {
                Directory.CreateDirectory(carpetaReportes);
            }

            // Genera el nombre dinámico del PDF
            RutaPDF = Path.Combine(carpetaReportes, NombreFactura + ".pdf");

            Documento = new Document(new Rectangle(538.65f, 708.75f).Rotate());

            this.idVenta = IdVenta;
            this.idFactura = IdFactura;
            this.nombreDueño = NombreDueño;
            this.direccionDueño = DireccionDueño;
            this.celularDueño = CelularDueño;
            this.correoDueño = CorreoDueño;
            this.fechaVenta = FechaVenta;
            this.totalVenta = Total;
            this.listaVenta = ListaVenta;
        }
        public void GenerarTicket()
        {
            try
            {
                Writer = PdfWriter.GetInstance(Documento, new FileStream(RutaPDF, FileMode.Create));
                Documento.Open();

                AgregarEncabezadoTicket();
                AgregarContenidoTicket();
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
        public void GenerarFactura()
        {
            try
            {
                
                Writer = PdfWriter.GetInstance(Documento, new FileStream(RutaPDF, FileMode.Create));
                Documento.Open();

                AgregarEncabezadoFactura();
                AgregarContenidoFactura();
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
        #region TicketLogica
        private void AgregarEncabezadoTicket()
        {
            try
            {
                Font textoFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7);

                string imagenPath = Path.Combine(DirectorioProyecto, "Resources", "VetPetLogoNew.png");
                if (!File.Exists(imagenPath))
                {
                    MessageBox.Show("La imagen VetPetLogoNew.png no se encontró en la carpeta Resources.");
                    return;
                }
                Font fontText = FontFactory.GetFont(FontFactory.HELVETICA, 7);
                Font fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagenPath);
                img.ScaleAbsolute(70f, 70f);
                img.Alignment = Element.ALIGN_CENTER;
                Documento.Add(img);

                Paragraph informacion = new Paragraph("VetPet+ S.A de C.V\nLa Paz, Baja California Sur, México\nBlvd. Forjadores de Sudcalifornia\ncontacto.vetpetplus@vetpet.com\n+52 6121948332", fontText);
                informacion.Alignment = Element.ALIGN_CENTER;
                Documento.Add(informacion);
                Documento.Add(new Paragraph("\n"));

                Paragraph titulo = new Paragraph("TICKET DE VENTA", fontBold);
                titulo.Alignment = Element.ALIGN_CENTER;
                Documento.Add(titulo);
                //Documento.Add(new Paragraph("\n"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en AgregarEncabezado: " + ex.Message);
            }
        }
        private void AgregarContenidoTicket()
        {
            Font tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13);
            Font fontText = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

            // Informacion del ticket
            Paragraph idVen = new Paragraph("Id Venta: " + idVenta.ToString(), fontText);
            idVen.Alignment = Element.ALIGN_CENTER;
            Documento.Add(idVen);
            //Documento.Add(new Paragraph("\n"));

            Paragraph recepcionista = new Paragraph("Recepcionista: " + nombreRecepcionista, fontText);
            recepcionista.Alignment = Element.ALIGN_CENTER;
            Documento.Add(recepcionista);
            //Documento.Add(new Paragraph("\n"));

            if (nombreDueño != "")
            {
                Paragraph dueño = new Paragraph("Dueño: " + nombreDueño, fontText);
                dueño.Alignment = Element.ALIGN_CENTER;
                Documento.Add(dueño);
                //Documento.Add(new Paragraph("\n"));
            }
            if (nombreMascota != " ")
            {
                Paragraph mascota = new Paragraph("Mascota: " + nombreMascota, fontText);
                mascota.Alignment = Element.ALIGN_CENTER;
                Documento.Add(mascota);
                //Documento.Add(new Paragraph("\n"));
            }

            Paragraph fechaVen = new Paragraph(fechaVenta, fontText);
            fechaVen.Alignment = Element.ALIGN_CENTER;
            Documento.Add(fechaVen);
            //Documento.Add(new Paragraph("\n"));

            // Tabla de Información
            if (listaServicios != null)
            {
                Paragraph servicios = new Paragraph("Servicios", fontBold);
                servicios.Alignment = Element.ALIGN_CENTER;
                Documento.Add(servicios);
                //Documento.Add(new Paragraph("\n"));

                PdfPTable tablaServ = null;
                CrearTablaServ(ref tablaServ, fontText);
                Documento.Add(tablaServ);
                Documento.Add(new Paragraph("\n"));
            }
            if (listaProductos != null)
            {
                Paragraph productos = new Paragraph("Productos", fontBold);
                productos.Alignment = Element.ALIGN_CENTER;
                Documento.Add(productos);
                //Documento.Add(new Paragraph("\n"));

                PdfPTable tablaProd = null;
                CrearTablaProd(ref tablaProd, fontText);
                Documento.Add(tablaProd);
                Documento.Add(new Paragraph("\n"));
            }

            Paragraph total = new Paragraph("TOTAL: $" + totalVenta + " MXN", fontBold);
            total.Alignment = Element.ALIGN_CENTER;
            Documento.Add(total);
            //Documento.Add(new Paragraph("\n"));

            Paragraph efectivo = new Paragraph("Efectivo: $" + totalEfectivo + " MXN", fontText);
            efectivo.Alignment = Element.ALIGN_LEFT;
            Documento.Add(efectivo);
            //Documento.Add(new Paragraph("\n"));

            Paragraph tarjeta = new Paragraph("Tarjeta: $" + totalTarjeta, fontText);
            tarjeta.Alignment = Element.ALIGN_LEFT;
            Documento.Add(tarjeta);
            //Documento.Add(new Paragraph("\n"));

            Paragraph totalPag = new Paragraph("Total: $" + totalVenta, fontText);
            totalPag.Alignment = Element.ALIGN_LEFT;
            Documento.Add(totalPag);
            Documento.Add(new Paragraph("\n"));

            Paragraph gracias = new Paragraph("¡GRACIAS POR SU PREFERENCIA!", fontBold);
            gracias.Alignment = Element.ALIGN_CENTER;
            Documento.Add(gracias);
        }
        #endregion
        #region FacturaLogica
        private void AgregarEncabezadoFactura()
        {
            try
            {
                Font textoFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                Font fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                Font facturaFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);

                string imagenPath = Path.Combine(DirectorioProyecto, "Resources", "VetPetLogoNew.png");
                if (!File.Exists(imagenPath))
                {
                    MessageBox.Show("La imagen VetPetLogoNew.png no se encontró en la carpeta Resources.");
                    return;
                }

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagenPath);
                img.ScaleAbsolute(70f, 70f);
                img.Alignment = Element.ALIGN_LEFT;

                PdfPTable tablaEncabezado = new PdfPTable(2);
                tablaEncabezado.WidthPercentage = 100;
                tablaEncabezado.SetWidths(new float[] { 3, 1 });

                PdfPCell celdaImagen = new PdfPCell(img);
                celdaImagen.Border = PdfPCell.NO_BORDER;
                celdaImagen.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaEncabezado.AddCell(celdaImagen);

                PdfPCell celdaFactura = new PdfPCell
                (
                    new Phrase("FACTURA", facturaFont)
                );
                celdaFactura.Border = PdfPCell.NO_BORDER;
                celdaFactura.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaEncabezado.AddCell(celdaFactura);

                Documento.Add(tablaEncabezado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en AgregarEncabezado: " + ex.Message);
            }
        }
        private void AgregarContenidoFactura()
        {
            Font fontText = FontFactory.GetFont(FontFactory.HELVETICA, 14);
            Font fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

            PdfPTable tablaInfo = null;
            CrearTablaInfo(ref tablaInfo, fontText, fontBold);
            Documento.Add(tablaInfo);
            Documento.Add(new Paragraph("\n"));

            PdfPTable tablaDesc = null;
            CrearTablaDesc(ref tablaDesc, fontText, fontBold);
            Documento.Add(tablaDesc);
            Documento.Add(new Paragraph("\n"));

            Paragraph total = new Paragraph("Total: $" + totalVenta, fontBold);
            total.Alignment = Element.ALIGN_RIGHT;
            Documento.Add(total);

        }

        #endregion
        #region Tablas
        public void CrearTablaInfo(ref PdfPTable tabla, Font fontText, Font fontBold)
        {
            tabla = new PdfPTable(3);
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            float[] columnWidths = { 20, 40, 40 };
            tabla.SetWidths(columnWidths);

            // Crear una tabla anidada para la primera celda
            PdfPTable subTabla = new PdfPTable(1);
            subTabla.WidthPercentage = 100;

            // Agregar las filas individuales
            subTabla.AddCell(new PdfPCell(new Phrase("Número de Factura: \n" + idFactura, fontBold)) { Border = PdfPCell.BOTTOM_BORDER });
            subTabla.AddCell(new PdfPCell(new Phrase("Fecha: \n" + fechaVenta, fontBold)) { Border = PdfPCell.BOTTOM_BORDER });
            subTabla.AddCell(new PdfPCell(new Phrase("Número de Venta: \n" + idVenta, fontBold)) { Border = PdfPCell.NO_BORDER });

            // Crear la celda con la tabla anidada
            PdfPCell celda1 = new PdfPCell(subTabla);
            celda1.Border = PdfPCell.NO_BORDER;

            // Crear las demás celdas
            Phrase informacionDueño = new Phrase(
                "Para\n"
                + nombreDueño + "\n"
                + direccionDueño + "\n"
                + celularDueño + "\n"
                + correoDueño, fontText);

            Phrase informacionVetPet = new Phrase(
                "De\n" +
                "VetPet+, S.A de C.V\n" +
                "La Paz, Baja California Sur\n" +
                "Blvd. Forjadores de Sudcalifornia\n" +
                "contacto.vetpetplus@vetpet.com\n" +
                "+52 6121948332", fontText);

            PdfPCell celda2 = new PdfPCell(informacionVetPet) { Border = PdfPCell.NO_BORDER };
            PdfPCell celda3 = new PdfPCell(informacionDueño) { Border = PdfPCell.NO_BORDER };

            // Agregar celdas a la tabla principal
            celda1.Border = PdfPCell.TOP_BORDER | PdfPCell.BOTTOM_BORDER;
            celda2.Border = PdfPCell.TOP_BORDER | PdfPCell.BOTTOM_BORDER;
            celda3.Border = PdfPCell.TOP_BORDER | PdfPCell.BOTTOM_BORDER;
            celda1.BorderWidth = 2f;
            celda2.BorderWidth = 2f;
            celda3.BorderWidth = 2f;

            tabla.AddCell(celda1);
            tabla.AddCell(celda2);
            tabla.AddCell(celda3);
        }
        public void CrearTablaDesc(ref PdfPTable tabla, Font fontText, Font fontBold)
        {
            tabla = new PdfPTable(3);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_LEFT;
            float[] columnWidths = { 50, 25, 25 };
            tabla.SetWidths(columnWidths);
            // Encabezados de la tabla
            PdfPCell descripcion = new PdfPCell(new Phrase("Descripción", fontBold));
            PdfPCell precio = new PdfPCell(new Phrase("Precio", fontBold));
            PdfPCell cantidad = new PdfPCell(new Phrase("Cantidad", fontBold));
            descripcion.HorizontalAlignment = Element.ALIGN_CENTER;
            cantidad.HorizontalAlignment = Element.ALIGN_CENTER;
            precio.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(descripcion);
            tabla.AddCell(precio);
            tabla.AddCell(cantidad);

            for (int i = 0; i < listaVenta.Count; i++)
            {
                PdfPCell celda1 = new PdfPCell(new Phrase(listaVenta[i].Item1.ToString(), fontText));
                PdfPCell celda2 = new PdfPCell(new Phrase(listaVenta[i].Item2.ToString(), fontText));
                PdfPCell celda3 = new PdfPCell(new Phrase(listaVenta[i].Item3.ToString(), fontText));
                celda1.HorizontalAlignment = Element.ALIGN_CENTER;
                celda2.HorizontalAlignment = Element.ALIGN_CENTER;
                celda3.HorizontalAlignment = Element.ALIGN_CENTER;
                tabla.AddCell(celda1);
                tabla.AddCell(celda2);
                tabla.AddCell(celda3);
            }

        }
        public void CrearTablaServ(ref PdfPTable tabla, Font fontText)
        {
            tabla = new PdfPTable(2);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1});
            // Encabezados de la tabla
            for (int i = 0; i < listaServicios.Count; i++)
            {
                decimal totalServicio = listaServicios[i].Item2 * listaServicios[i].Item3;
                PdfPCell celda1 = new PdfPCell(new Phrase(listaServicios[i].Item1.ToString(), fontText));
                PdfPCell celda2 = new PdfPCell(new Phrase("$"+totalServicio.ToString(), fontText));
                celda1.HorizontalAlignment = Element.ALIGN_LEFT;
                celda2.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda1.Border = PdfPCell.NO_BORDER;
                celda2.Border = PdfPCell.NO_BORDER;
                tabla.AddCell(celda1);
                tabla.AddCell(celda2);
            }

        }
        public void CrearTablaProd(ref PdfPTable tabla, Font fontText)
        {
            tabla = new PdfPTable(2);
            // 🔹 Crear tabla con dos columnas (Razón - Veces)
            tabla.WidthPercentage = 100;
            tabla.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.SetWidths(new float[] { 1, 1 });
            // Encabezados de la tabla
            for (int i = 0; i < listaProductos.Count; i++)
            {
                decimal totalProducto = listaProductos[i].Item2 * listaProductos[i].Item3;
                PdfPCell celda1 = new PdfPCell(new Phrase(listaProductos[i].Item1.ToString(), fontText));
                PdfPCell celda2 = new PdfPCell(new Phrase("$"+totalProducto.ToString(), fontText));
                celda1.HorizontalAlignment = Element.ALIGN_LEFT;
                celda2.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda1.Border = PdfPCell.NO_BORDER;
                celda2.Border = PdfPCell.NO_BORDER;
                tabla.AddCell(celda1);
                tabla.AddCell(celda2);
            }

        }
        #endregion
    }
}
