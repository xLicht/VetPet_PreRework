using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class InfoUsuario : FormPadre
    {
        Form1 parentFo;
        public InfoUsuario(Form1 parent)
        {
            InitializeComponent();
            parentFo = parent;
            //Timer closeTimer = new Timer();
            //closeTimer.Interval = 400; // Verificar cada 100ms
            //closeTimer.Tick += (s, e) =>
            //{
            //    if (!this.Bounds.Contains(Cursor.Position))
            //    {
            //        this.Close();
            //    }
            //};
            //closeTimer.Start();

            //this.FormClosed += (s, e) => closeTimer.Stop();
        }

        private void InfoUsuario_Load(object sender, EventArgs e)
        {
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            ReiniciarAplicacion();
        }
        void ReiniciarAplicacion()
        {
            string applicationPath = Application.ExecutablePath;
            Process.Start(applicationPath);
            Application.Exit();
        }
    }
}
