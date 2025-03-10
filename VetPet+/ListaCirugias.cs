using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class ListaCirugias : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();
        //Variables SQL
        public SqlConnection conexion;
        public SqlCommand comando;
        public SqlDataReader Lector;
        public string q;
        public string mensaje;

        private Form1 parentForm;
        public ListaCirugias()
        {
            InitializeComponent();
            this.Load += ListaCirugias_Load;       // Evento Load
            this.Resize += ListaCirugias_Resize;
        }
        public ListaCirugias(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;  // Guardamos la referencia del formulario principal
        }

        private void ListaCirugias_Load(object sender, EventArgs e)
        {
            // Guardar el tamaño original del formulario
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;
            originalWidth = this.ClientSize.Width;
            originalHeight = this.ClientSize.Height;

            //Consultas SQL
            conexion = new SqlConnection(@"Data Source=DESKTOP-GQ6Q9HG\SQLEXPRESS;Initial Catalog=Servicio;Integrated Security=True;");
            conexion.Open();

            //Primera Tabla
            q = "SELECT NombreServicio FROM Servicios";
            comando = new SqlCommand(q, conexion);
            Lector = comando.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(Lector);
            dataGridView1.DataSource = dt;

            //Segunda Tabla
            q = "SELECT ID FROM Servicios";
            comando = new SqlCommand(q, conexion);
            Lector = comando.ExecuteReader();

            DataTable dt2 = new DataTable();
            dt2.Load(Lector);
            dataGridView2.DataSource = dt2;

            conexion.Close();

            // Guardar información original de cada control
            foreach (Control control in this.Controls)
            {
                controlInfo[control] = (control.Width, control.Height, control.Left, control.Top, control.Font.Size);
            }
        }

        private void ListaCirugias_Resize(object sender, EventArgs e)
        {
            // Calcular el factor de escala
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in this.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];

                    // Ajustar las dimensiones
                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);

                    // Ajustar el tamaño de la fuente
                    control.Font = new Font(control.Font.FontFamily, info.fontSize * Math.Min(scaleX, scaleY));
                }
            }
        }
        

        private void BtnAgregarCirugía_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarCirugias(parentForm));
        }

        private void BtnEliminarCirugía_Click(object sender, EventArgs e)
        {
            
        }


        private void BtnAgregarTipoDeCirugia_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new AgregarTipoCirugia(parentForm));
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void BtnRegresar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ListaServicios(parentForm));
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new ModificarCirugias(parentForm));
        }
    }
}
