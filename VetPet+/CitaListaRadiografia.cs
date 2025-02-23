using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_;

namespace VetPet_
{
    public partial class CitaListaRadiografia : FormPadre
    {
        public CitaListaRadiografia()
        {
            InitializeComponent();
        }

        public CitaListaRadiografia(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void CitaListaRadiografia_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new CitaAgregarRadiografia(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto

        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaListaServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }
    }
}
