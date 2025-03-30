using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public VentasVerTicket(Form1 parentForm, int IdVenta, int IdDueño, string NombreTicket, string NombreRecepcionista, string NombreDueño, string NombreMascota, string FechaVenta, List<Tuple<string, decimal, int>> ListaServicios, List<Tuple<string, decimal, int>> ListaProductos, string TotalVenta, string TotalEfectivo, string TotalTarjeta)
        {
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

    }
}
