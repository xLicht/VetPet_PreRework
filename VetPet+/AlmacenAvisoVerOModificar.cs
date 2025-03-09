using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace VetPet_
{
    public partial class AlmacenAvisoVerOModificar : FormPadre
    {
        public string Resultado { get; private set; }
        public static string formularioAnterior; // Guarda el nombre del formulario anterior

        private void AlmacenAvisoVerOModificar_Load(object sender, EventArgs e)
        {
        }

        public AlmacenAvisoVerOModificar(string nombre, Form1 parent)
        {
            InitializeComponent();
            label2.Text = nombre;
            parentForm = parent;
        }


        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            Resultado = "Modificar";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnVer_Click_1(object sender, EventArgs e)
        {
            Resultado = "Ver";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Resultado = "Salir";
            this.DialogResult = DialogResult.OK;

            // Verifica de qué formulario vino
            if (formularioAnterior == "AlmacenInventarioMedicamentos")
            {
                parentForm.formularioHijo(new AlmacenInventarioMedicamentos(parentForm));

            }
            else if (formularioAnterior == "AlmacenInventarioProductos")
            {
                parentForm.formularioHijo(new AlmacenInventarioProductos(parentForm));
            }
            else if (formularioAnterior == "AlmacenProveedor")
            {
                parentForm.formularioHijo(new AlmacenProveedor(parentForm));
            }

            this.Close(); // Cierra el formulario actual
        }
    }
}
