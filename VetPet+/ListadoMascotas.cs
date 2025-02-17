using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class ListadoMascotas : Form
    {
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        private Form1 parentForm;


        public ListadoMascotas()
        {
            InitializeComponent();
            this.Load += ListadoMascotas_Load;       // Evento Load
            this.Resize += ListadoMascotas_Resize;   // Evento Resize
            this.Controls.SetChildIndex(pictureBox8, 0); // Índice 0 = Capa superior
        }

        private void ListadoMascotas_Load(object sender, EventArgs e)
        {

        }
    }
}
