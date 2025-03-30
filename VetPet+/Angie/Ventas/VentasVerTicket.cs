using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VetPet_.Angie.Ventas
{
    public partial class VentasVerTicket : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        private Form1 parentForm;

        private int idVenta;
        private int? idDueño;
        private string nombreTicket;
        private string nombreRecepcionista;
        private string nombreDueño;
        private string nombreMascota;
        private string fechaVenta;
        private List<Tuple<string, decimal, int>> listaServicios;
        private List<Tuple<string, decimal, int>> listaProductos;
        private string totalVenta;
        private string totalEfectivo;
        private string totalTarjeta;

        private string direccionDueño;
        private string celularDueño;
        private string correoDueño;

        public VentasVerTicket(Form1 parentForm, int IdVenta, int IdDueño, string NombreTicket, string NombreRecepcionista, string NombreDueño, string NombreMascota, string FechaVenta, List<Tuple<string, decimal, int>> ListaServicios, List<Tuple<string, decimal, int>> ListaProductos, string TotalVenta, string TotalEfectivo, string TotalTarjeta)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.idVenta = IdVenta;
            this.idDueño = IdDueño;
            this.nombreTicket = NombreTicket;
            this.nombreRecepcionista = NombreRecepcionista;
            this.nombreDueño = NombreDueño;
            this.nombreMascota = NombreMascota;
            this.fechaVenta = FechaVenta;
            this.listaServicios = ListaServicios;
            this.listaProductos = ListaProductos;
            this.totalVenta = TotalVenta;
            this.totalTarjeta = TotalTarjeta;
            this.totalEfectivo = TotalEfectivo;
        }

        private void VentasVerTicket_Load(object sender, EventArgs e)
        {
            GenerarTicket();
        }
        private void GenerarTicket()
        {
            TicketsManager ticket = new TicketsManager(idVenta, idDueño, nombreTicket, nombreRecepcionista, nombreDueño, nombreMascota, fechaVenta, listaServicios,
                listaProductos, totalVenta, totalEfectivo, totalTarjeta);
            ticket.GenerarTicket();

            try
            {
                string DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string carpetaReportes = Path.Combine(DirectorioProyecto, "Tickets-Arch");

                string rutaPDF = Path.Combine(carpetaReportes, nombreTicket + ".pdf");

                if (File.Exists(rutaPDF))
                {
                    pdfViewTicket.LoadFile(rutaPDF); // Cargar el PDF en el visor
                }
                else
                {
                    MessageBox.Show("El archivo PDF no se encontró en la ruta especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error: " + er.Message);
            }
        }

        private void BtnFactura_Click(object sender, EventArgs e)
        {
            GenerarFactura();
        }
        private void GenerarFactura()
        {
            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            string hora = DateTime.Now.ToString("H-m");
            string nombreFactura = "FAC_" + fecha.Replace("-", "") + "-" + hora.Replace("-", "");
            string idFactura = fecha.Replace("-", "") + "_" + hora.Replace("-", "");

            InformacionDueño(idDueño);
            List<Tuple<string, decimal, int>> listaConjunta = new List<Tuple<string, decimal, int>>();
            listaConjunta.AddRange(listaServicios);
            listaConjunta.AddRange(listaProductos);

            TicketsManager factura = new TicketsManager(nombreFactura, idVenta, idFactura, fechaVenta, nombreDueño, direccionDueño, celularDueño, correoDueño, listaConjunta, totalVenta);
            factura.GenerarFactura();

            try
            {
                string DirectorioProyecto = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string carpetaReportes = Path.Combine(DirectorioProyecto, "Facturas-Arch");

                string rutaPDF = Path.Combine(carpetaReportes, nombreFactura + ".pdf");

                if (File.Exists(rutaPDF))
                {
                    pdfViewTicket.LoadFile(rutaPDF); // Cargar el PDF en el visor
                }
                else
                {
                    MessageBox.Show("El archivo PDF no se encontró en la ruta especificada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error: " + er.Message);
            }
        }
        private void InformacionDueño(int? idDueño)
        {
            ConexionMaestra conexionMaestra = new ConexionMaestra();
            SqlConnection conex = conexionMaestra.CrearConexion();
            if (idDueño != null)
            {
                try
                {
                    conex.Open();
                    string q = @"SELECT 
                                    p.nombre,
	                                p.apellidoP,
	                                p.apellidoM,
                                    pa.nombre AS pais,
                                    e.nombre AS estado,
                                    m.nombre AS municipio,
                                    c.nombre AS ciudad,
                                    col.nombre AS colonia,
                                    cal.nombre AS calle,
                                    p.celularPrincipal,
                                    p.correoElectronico
                                FROM 
                                    Persona p
                                JOIN 
                                    Direccion d ON p.idPersona = d.idPersona
                                JOIN 
                                    Pais pa ON d.idPais = pa.idPais
                                JOIN 
                                    Estado e ON d.idEstado = e.idEstado
                                JOIN 
                                    Municipio m ON d.idMunicipio = m.idMunicipio
                                JOIN 
                                    Ciudad c ON d.idCiudad = c.idCiudad
                                JOIN 
                                    Colonia col ON d.idColonia = col.idColonia
                                JOIN 
                                    Calle cal ON d.idCalle = cal.idCalle
                                WHERE 
                                    p.idPersona = @idDueño;";
                    SqlCommand comando = new SqlCommand(q, conex);
                    comando.Parameters.AddWithValue("@idDueño", idDueño);
                    SqlDataReader lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        nombreDueño = lector.GetString(0) + " " + lector.GetString(1) + " " + lector.GetString(2);
                        direccionDueño = lector.GetString(3) + ", " + lector.GetString(4) + ", " + lector.GetString(5) + ", " + lector.GetString(6) + ", " + lector.GetString(7) + ", " + lector.GetString(8);
                        celularDueño = lector.GetString(9);
                        correoDueño = lector.GetString(10);
                        i++;
                    }
                    conex.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    conex.Close();
                }
            }
        }
    }
}
