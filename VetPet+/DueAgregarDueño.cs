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
    public partial class DueAgregarDueño : FormPadre
    {
        private conexionDaniel conexionDB = new conexionDaniel();
        public DueAgregarDueño(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void DueAgregarDueño_Load(object sender, EventArgs e)
        {
            CargarCB();
        }


        private void AgregarEmpleado()
        {
            try
            {
                conexionDB.AbrirConexion();

                int idPais = ObtenerORegistrarIdPais(cbPais.Text);
                int idCalle = ObtenerORegistrarIdCalle(cbCalle.Text);
                int idCp = ObtenerORegistrarIdCp(txtCP.Text);
                int idCiudad = ObtenerORegistrarIdCiudad(cbCiudad.Text);
                int idColonia = ObtenerORegistrarIdColonia(cbColonia.Text);
                int idEstado = ObtenerORegistrarIdEstado(cbEstado.Text);

                // Insertar nuevo empleado
                string query = @" INSERT INTO Persona (nombre, apellidoP, apellidoM, celularPrincipal, correoElectronico)
                    VALUES (@nombre, @apellidoP, @apellidoM, @celularPrincipal, @correo);
                    SELECT SCOPE_IDENTITY();";

                int idPersona = 0;
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@apellidoP", txtApellidoP.Text);
                    cmd.Parameters.AddWithValue("@apellidoM", txtApellidoM.Text);
                    cmd.Parameters.AddWithValue("@celularPrincipal", txtCelular.Text);
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    idPersona = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Insertar dirección
                query = @"INSERT INTO Direccion (idPersona, idPais, idCalle, idCp, idCiudad, idColonia, idEstado)
                 VALUES (@idPersona, @idPais, @idCalle, @idCp, @idCiudad, @idColonia, @idEstado);";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", idPersona);
                    cmd.Parameters.AddWithValue("@idPais", idPais);
                    cmd.Parameters.AddWithValue("@idCalle", idCalle);
                    cmd.Parameters.AddWithValue("@idCp", idCp);
                    cmd.Parameters.AddWithValue("@idCiudad", idCiudad);
                    cmd.Parameters.AddWithValue("@idColonia", idColonia);
                    cmd.Parameters.AddWithValue("@idEstado", idEstado); // Insertar el ID del estado
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
                string.IsNullOrWhiteSpace(cbCiudad.Text) || string.IsNullOrWhiteSpace(cbColonia.Text))
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

        private int ObtenerORegistrarIdPais(string pais)
        {
            int idPais = ObtenerIdPorNombre("Pais", pais);
            if (idPais == 0)
            {
                string queryInsert = "INSERT INTO Pais (nombre) VALUES (@pais); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(queryInsert, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@pais", pais);
                    object result = cmd.ExecuteScalar();
                    idPais = Convert.ToInt32(result);
                }
            }
            return idPais;
        }

        private int ObtenerORegistrarIdCalle(string calle)
        {
            int idCalle = ObtenerIdPorNombre("Calle", calle);
            if (idCalle == 0)
            {
                string queryInsert = "INSERT INTO Calle (nombre) VALUES (@calle); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(queryInsert, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@calle", calle);
                    object result = cmd.ExecuteScalar();
                    idCalle = Convert.ToInt32(result);
                }
            }
            return idCalle;
        }

        private int ObtenerORegistrarIdColonia(string colonia)
        {
            int idColonia = ObtenerIdPorNombre("Colonia", colonia);
            if (idColonia == 0)
            {
                string queryInsert = "INSERT INTO Colonia (nombre) VALUES (@colonia); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(queryInsert, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@colonia", colonia);
                    object result = cmd.ExecuteScalar();
                    idColonia = Convert.ToInt32(result);
                }
            }
            return idColonia;
        }

        private int ObtenerORegistrarIdCp(string cp)
        {
            int idCp = ObtenerIdPorCodigoPostal(cp);
            if (idCp == 0)
            {
                string queryInsert = "INSERT INTO Cp (cp) VALUES (@cp); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(queryInsert, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@cp", cp);
                    object result = cmd.ExecuteScalar();
                    idCp = Convert.ToInt32(result);
                }
            }
            return idCp;
        }

        private int ObtenerIdPorNombre(string tabla, string nombre)
        {
            string query = $"SELECT id{tabla} FROM {tabla} WHERE nombre = @nombre";
            using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private int ObtenerIdPorCodigoPostal(string cp)
        {
            string query = "SELECT idCp FROM Cp WHERE cp = @cp";
            using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
            {
                cmd.Parameters.AddWithValue("@cp", cp);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private int ObtenerORegistrarIdCiudad(string ciudad)
        {
            int idCiudad = ObtenerIdPorNombre("Ciudad", ciudad);
            if (idCiudad == 0)
            {
                string queryInsert = "INSERT INTO Ciudad (nombre) VALUES (@ciudad); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(queryInsert, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@ciudad", ciudad);
                    object result = cmd.ExecuteScalar();
                    idCiudad = Convert.ToInt32(result);
                }
            }
            return idCiudad;
        }

        private int ObtenerORegistrarIdEstado(string estado)
        {
            int idEstado = ObtenerIdPorNombre("Estado", estado);
            if (idEstado == 0)
            {
                string queryInsert = "INSERT INTO Estado (nombre) VALUES (@estado); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(queryInsert, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@estado", estado);
                    object result = cmd.ExecuteScalar();
                    idEstado = Convert.ToInt32(result);
                }
            }
            return idEstado;
        }




        private bool ValidarCorreo(string correo)
        {
            string patronCorreo = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(correo, patronCorreo);
        }
        private void CargarCB()
        {
            try
            {
                conexionDB.AbrirConexion();
                MostrarCB("SELECT nombre FROM Pais", cbPais);
                MostrarCB("SELECT nombre FROM Ciudad", cbCiudad);
                MostrarCB("SELECT nombre FROM Colonia", cbColonia);
                MostrarCB("SELECT nombre FROM Calle", cbCalle);
                MostrarCB("SELECT nombre FROM Estado", cbEstado);
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

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Los datos ingresados se borraran, guarde para salvar los datos", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                parentForm.formularioHijo(new DueAtencionAlCliente(parentForm));
            }
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            AgregarEmpleado();
            parentForm.formularioHijo(new DueAtencionAlCliente(parentForm));
        }

        private void txtCp_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
