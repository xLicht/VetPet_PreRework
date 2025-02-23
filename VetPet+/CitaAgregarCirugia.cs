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
    public partial class CitaAgregarCirugia : FormPadre
    {
        public static string formularioAnterior; // Guarda el nombre del formulario anterior

        public CitaAgregarCirugia()
        {
            InitializeComponent();
        }
        public CitaAgregarCirugia(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CitaGestionarServicio.formularioAnterior = "CitaAgregarCirugia";
            parentForm.formularioHijo(new CitaGestionarServicio(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new CitaListaCirugia(parentForm)); // Pasamos la referencia de Form1 a AlmacenInventarioAgregarProducto
        }
    }
}
