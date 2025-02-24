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
    public partial class DueAtencionAlCliente : FormPadre
    {
        public DueAtencionAlCliente(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void DueAtencionAlCliente_Load(object sender, EventArgs e)
        {
  
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueAgregarDueño(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            
            parentForm.formularioHijo(new MenuAtencionaCliente(parentForm));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }
    }
}
