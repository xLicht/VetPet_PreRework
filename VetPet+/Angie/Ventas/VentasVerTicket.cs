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
        private int? idDueño1;
        private string nombreRecepcionista;
        private string v1;
        private string v2;
        private List<Tuple<string, decimal, int>> listaServicios;
        private List<Tuple<string, decimal, int>> listaProductos;
        private string v3;
        private string v4;
        private string v5;
        private string v6;

        public VentasVerTicket(Form1 parentForm, int idVenta, int? idDueño1, string nombreRecepcionista, 
            string text, string v1, string v2, List<Tuple<string, decimal, int>> listaServicios, List<Tuple<string, decimal, int>> listaProductos, 
            string v3, string v4, string v5, string v6)
        {
            this.parentForm = parentForm;
            this.idVenta = idVenta;
            this.idDueño1 = idDueño1;
            this.nombreRecepcionista = nombreRecepcionista;
            Text = text;
            this.v1 = v1;
            this.v2 = v2;
            this.listaServicios = listaServicios;
            this.listaProductos = listaProductos;
            this.v3 = v3;
            this.v4 = v4;
            this.v5 = v5;
            this.v6 = v6;
        }

    }
}
