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
using VetPet_;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace VetPet_
{
    public partial class EmpAgregarEmpleado : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        public EmpAgregarEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpAgregarEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                AgregarEmpleado();
                parentForm.formularioHijo(new EmpConsultarEmpleado(parentForm));
            }
            //parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Se borrarán todos los datos ingresados. ¿Desea continuar?","Advertencia",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
            }
            else
            {
           
               
            }
        }

        private void btnSelecTipo_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpAgregarTipoEmpleado(parentForm));
        }

        private void r_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Se borrarán todos los datos ingresados. ¿Desea continuar?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
            }
            else
            {
              
                
            }
        }

        private void AgregarEmpleado()
        {
            try
            {
                conexionDB.AbrirConexion();

                // Obtener o registrar las IDs necesarias
                int idPais = ObtenerORegistrarIdPais(cbPais.Text);
                int idCalle = ObtenerORegistrarIdCalle(cbCalle.Text);
                int idCp = ObtenerORegistrarIdCp(txtCP.Text);
                int idCiudad = ObtenerIdPorNombre("Ciudad", cbCiudad.Text);
                int idColonia = ObtenerORegistrarIdColonia(cbColonia.Text);
                int idTipoEmpleado = ObtenerIdPorNombre("TipoEmpleado", cbTipo.Text);

                // Insertar nuevo empleado
                string query = @"
            INSERT INTO Persona (nombre, apellidoP, apellidoM, celular, correoElectronico)
            VALUES (@nombre, @apellidoP, @apellidoM, @celular, @correo);
            SELECT SCOPE_IDENTITY();"; // Obtener el ID de la persona insertada

                int idPersona = 0;
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@apellidoP", txtApellidoP.Text);
                    cmd.Parameters.AddWithValue("@apellidoM", txtApellidoM.Text);
                    cmd.Parameters.AddWithValue("@celular", txtCelular.Text);
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    idPersona = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Insertar nuevo empleado
                query = @"
            INSERT INTO Empleado (usuario, contraseña, palabraClave, idTipoEmpleado, idPersona)
            VALUES (@usuario, @contraseña, @palabraClave, @idTipoEmpleado, @idPersona);";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@contraseña", txtContraseña.Text);
                    cmd.Parameters.AddWithValue("@palabraClave", txtPalabraClave.Text);
                    cmd.Parameters.AddWithValue("@idTipoEmpleado", idTipoEmpleado);
                    cmd.Parameters.AddWithValue("@idPersona", idPersona);
                    cmd.ExecuteNonQuery();
                }

                // Insertar dirección
                query = @"
            INSERT INTO Direccion (idPersona, idPais, idCalle, idCp, idCiudad, idColonia)
            VALUES (@idPersona, @idPais, @idCalle, @idCp, @idCiudad, @idColonia);";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", idPersona);
                    cmd.Parameters.AddWithValue("@idPais", idPais);
                    cmd.Parameters.AddWithValue("@idCalle", idCalle);
                    cmd.Parameters.AddWithValue("@idCp", idCp);
                    cmd.Parameters.AddWithValue("@idCiudad", idCiudad);
                    cmd.Parameters.AddWithValue("@idColonia", idColonia);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Empleado agregado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el empleado: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }


        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellidoP.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoM.Text) || string.IsNullOrWhiteSpace(txtCelular.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtCP.Text) ||
                string.IsNullOrWhiteSpace(cbPais.Text) || string.IsNullOrWhiteSpace(cbCalle.Text) ||
                string.IsNullOrWhiteSpace(cbCiudad.Text) || string.IsNullOrWhiteSpace(cbColonia.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text) || string.IsNullOrWhiteSpace(txtPalabraClave.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return false;
            }

            if (!ValidarCorreo(txtCorreo.Text))
            {
                MessageBox.Show("Correo electronico invalido, Por favor, ingrese un correo electrónico válido.");
                return false;
            }
            return true;
        }

        private bool ValidarCorreo(string correo)
        {
            string patronCorreo = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(correo, patronCorreo);
        }

        private void a_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpListaEmpleados(parentForm));
        }

        private void txtCP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bloquea cualquier otro carácter
            }
        }

        private void txtApellidoPat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; //solo una plabra
            }
        }

        private void txtApellidoMat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; //solo una plabra
            }
        }

        private void txtNumContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
