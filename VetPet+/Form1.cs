using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class Form1 : Form
    {
        protected float originalWidth;
        protected float originalHeight;
        protected Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo
            = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        int IDUsuario;
        int IDTEmpleado;
        int FondoCaja;
        public Form1(int iDUsuario, int iDTEmpleado, int fondoCaja)
        {
            InitializeComponent();
            this.Text = String.Empty;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            IDUsuario = iDUsuario;
            IDTEmpleado = iDTEmpleado;
            FondoCaja = fondoCaja;
        }
        private void StoreControlInfo(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                controlInfo[control] = (
                    control.Width,
                    control.Height,
                    control.Left,
                    control.Top,
                    control.Font.Size
                );

                if (control.HasChildren)
                {
                    StoreControlInfo(control);
                }
            }
        }
        private void ResizeControls(Control parent)
        {
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in parent.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];

                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);

                    float newFontSize = info.fontSize * Math.Min(scaleX, scaleY);
                    newFontSize = Math.Max(newFontSize, 1); // Evita valores menores a 1

                    control.Font = new Font(control.Font.FontFamily, newFontSize);

                }

                if (control.HasChildren)
                {
                    ResizeControls(control);
                }
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private Form formHijo;

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int borderSize = 10;
            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                Point cursorPos = PointToClient(Cursor.Position);
                int width = this.ClientSize.Width;
                int height = this.ClientSize.Height;

                if (cursorPos.X <= borderSize && cursorPos.Y <= borderSize)
                    m.Result = (IntPtr)13; // Esquina superior izquierda
                else if (cursorPos.X >= width - borderSize && cursorPos.Y <= borderSize)
                    m.Result = (IntPtr)14; // Esquina superior derecha
                else if (cursorPos.X <= borderSize && cursorPos.Y >= height - borderSize)
                    m.Result = (IntPtr)16; // Esquina inferior izquierda
                else if (cursorPos.X >= width - borderSize && cursorPos.Y >= height - borderSize)
                    m.Result = (IntPtr)17; // Esquina inferior derecha
                else if (cursorPos.X <= borderSize)
                    m.Result = (IntPtr)10; // Borde izquierdo
                else if (cursorPos.X >= width - borderSize)
                    m.Result = (IntPtr)11; // Borde derecho
                else if (cursorPos.Y <= borderSize)
                    m.Result = (IntPtr)12; // Borde superior
                else if (cursorPos.Y >= height - borderSize)
                    m.Result = (IntPtr)15; // Borde inferior
                else
                    return;
                return;
            }
            base.WndProc(ref m);
        }

        public void formularioHijo(Form hijo)
        {
            if (formHijo != null)
            {
                //Al abrir un formulario cerramos el formulario anterior
                formHijo.Close();
            }
            formHijo = hijo;
            hijo.TopLevel = false;
            hijo.FormBorderStyle = FormBorderStyle.None;
            hijo.Dock = DockStyle.Fill;
            pnlForms.Controls.Add(hijo);
            pnlForms.Tag = hijo;
            hijo.BringToFront();
            hijo.Show();
        }

        private void BtnAlmacen_Click(object sender, EventArgs e)
        {
            formularioHijo(new AlmacenMenu(this));  // Aquí se pasa la referencia de Form1
        }

        private void BtnAtencionClient_Click(object sender, EventArgs e)
        {
            formularioHijo(new MenuAtencionaCliente(this)); // Pasamos la referencia de Form1
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            originalWidth = this.Width;
            originalHeight = this.Height;

            StoreControlInfo(this);

            formularioHijo(new MainMenu(this));
        }
        // Botón btnProductos
        private void BtnProductos_Click(object sender, EventArgs e)
        {
            formularioHijo(new AlmacenInventarioProductos(this)); // Pasamos la referencia de Form1
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnVeterinaria_Click(object sender, EventArgs e)
        {
            formularioHijo(new VeterinariaMenu(this));
        }

        private void BtnServicios_Click(object sender, EventArgs e)
        {
            formularioHijo(new MenuServicios(this));
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            formularioHijo(new MenuReportes(this));
        }

        private void BtnCortes_Click(object sender, EventArgs e)
        {
            formularioHijo(new CortesMenus(this));
        }

        private void BtnVeterinaria_Click_1(object sender, EventArgs e)
        {
            formularioHijo(new VeterinariaMenu(this));
        }

        private void BtnEmpleados_Click(object sender, EventArgs e)
        {
            formularioHijo(new EmpMenuEmpleados(this));
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            formularioHijo(new MainMenu(this));
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = (this.WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeControls(this);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            formularioHijo(new MainMenu(this));
        }
    }
}
//SEXOO
//PENE POLLA

// Me voy a chingar la master causas


//MAFUFAFADA