using Org.BouncyCastle.Crypto.Macs;
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
using VetPet_;

namespace VetPet_
{
    public partial class EmpModificarEmpleado : FormPadre
    {
        public int DatoEmpleado { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public EmpModificarEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpModificarEmpleado_Load(object sender, EventArgs e)
        {
            CargarCB();
            MostrarDato();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            advertencia();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpConsultarEmpleado(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new EmpConsultarEmpleado(parentForm));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            advertencia();
        }

        private void advertencia()
        {
            DialogResult resultado = MessageBox.Show("Se borrarán todos los datos ingresados. ¿Desea continuar?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                parentForm.formularioHijo(new EmpConsultarEmpleado(parentForm));
            }
            else
            { }
        }

        public void MostrarDato()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"
                SELECT e.usuario, e.contraseña, e.palabraClave, 
                       p.nombre, p.apellidoP, p.apellidoM, p.celular, p.correoElectronico,
                       t.nombre AS tipoEmpleado,
                       pais.nombre AS pais, calle.nombre AS calle, 
                       cp.cp, ciudad.nombre AS ciudad, colonia.nombre AS colonia
                FROM Empleado e
                JOIN Persona p ON e.idPersona = p.idPersona
                JOIN TipoEmpleado t ON e.idTipoEmpleado = t.idTipoEmpleado
                LEFT JOIN Direccion d ON e.idEmpleado = d.idPersona
                LEFT JOIN Pais pais ON d.idPais = pais.idPais
                LEFT JOIN Calle calle ON d.idCalle = calle.idCalle
                LEFT JOIN Cp cp ON d.idCp = cp.idCp
                LEFT JOIN Ciudad ciudad ON d.idCiudad = ciudad.idCiudad
                LEFT JOIN Colonia colonia ON d.idColonia = colonia.idColonia
                WHERE e.idEmpleado = @idEmpleado";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idEmpleado", DatoEmpleado);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtUsuario.Text = reader["usuario"].ToString();
                        txtContraseña.Text = reader["contraseña"].ToString();
                        txtPalabraClave.Text = reader["palabraClave"].ToString();
                        txtNombre.Text = reader["nombre"].ToString();
                        txtApellidoP.Text = reader["apellidoP"].ToString();
                        txtApellidoM.Text = reader["apellidoM"].ToString();
                        txtCelular.Text = reader["celular"].ToString();
                        txtCorreo.Text = reader["correoElectronico"].ToString();
                        cbTipo.SelectedItem = reader["tipoEmpleado"].ToString();
                        cbPais.SelectedItem = reader["pais"].ToString();
                        cbCalle.SelectedItem = reader["calle"].ToString();
                        txtCP.Text = reader["cp"].ToString();
                        cbCiudad.SelectedItem = reader["ciudad"].ToString();
                        cbColonia.SelectedItem = reader["colonia"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se pudo obtener los datos del empleado. " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void  CargarCB()
        {
            try
            {
                conexionDB.AbrirConexion();
                MostrarCB("SELECT nombre FROM Pais",cbPais);
                MostrarCB("SELECT nombre FROM Ciudad",cbCiudad);
                MostrarCB("SELECT nombre FROM Colonia", cbColonia);
                MostrarCB("SELECT nombre FROM Calle", cbCalle);
                MostrarCB("SELECT nombre FROM TipoEmpleado", cbTipo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar los datos" + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void MostrarCB(string query, ComboBox comboBox)
        {
            using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader[0].ToString());
                }
                reader.Close();
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

