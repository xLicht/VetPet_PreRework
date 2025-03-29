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

        public VentasVerTicket(Form1 parent,int idVenta, int idDueño, string nombreRecepcionista, string nombreDueño, string nombreMascota,
            string fechaVenta, List<Tuple<string , decimal, int>> ListaServicios, List<Tuple<string, decimal, int>> ListaProductos, 
            string total, decimal? efectivo, string totalEfectivo, string totalTarjeta, string totalPagado)
        {
            InitializeComponent();
        }
    }
}
