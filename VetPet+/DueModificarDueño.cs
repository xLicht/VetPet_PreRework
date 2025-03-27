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
    public partial class DueModificarDueño : FormPadre
    {
        public int DatoEmpleado { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        private List<string> numerosSecundarios = new List<string>();
        public DueModificarDueño(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void DueModificarDueño_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Mensaje: "+ DatoEmpleado);
            CargarCB();
            MostrarDato();
            CargarNumerosSecundarios();

        }
        private void ActualizarDataGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Número");
            foreach (var num in numerosSecundarios)
            {
                dt.Rows.Add(num);
            }
            dtNumeros.DataSource = dt;
        }
        private void CargarNumerosSecundarios()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Número");

                conexionDB.AbrirConexion();
                string query = "SELECT numero FROM Celular WHERE idPersona = @idPersona AND estado = 'I'";
                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string num = reader["numero"].ToString();
                        numerosSecundarios.Add(num);
                        dt.Rows.Add(num);
                    }
                    reader.Close();
                }
                dtNumeros.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los números secundarios: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        public void MostrarDato()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT 
                        p.nombre, p.apellidoP, p.apellidoM, p.celularPrincipal, 
                        p.correoElectronico, 
                        pais.nombre AS pais, calle.nombre AS calle, 
                        cp.cp, ciudad.nombre AS ciudad, colonia.nombre AS colonia, 
                        estado.nombre AS estado,
                        municipio.nombre AS municipio
                    FROM 
                        Persona p
                    LEFT JOIN 
                        Direccion d ON p.idPersona = d.idPersona
                    LEFT JOIN 
                        Pais pais ON d.idPais = pais.idPais
                    LEFT JOIN 
                        Calle calle ON d.idCalle = calle.idCalle
                    LEFT JOIN 
                        Cp cp ON d.idCp = cp.idCp
                    LEFT JOIN 
                        Ciudad ciudad ON d.idCiudad = ciudad.idCiudad
                    LEFT JOIN 
                        Colonia colonia ON d.idColonia = colonia.idColonia
                    LEFT JOIN 
                        Estado estado ON d.idEstado = estado.idEstado 
                    LEFT JOIN
                        Municipio municipio ON d.idMunicipio = municipio.idMunicipio
                    WHERE 
                        p.idPersona = @idPersona";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombre.Text = reader["nombre"].ToString();
                        txtApellidoP.Text = reader["apellidoP"].ToString();
                        txtApellidoM.Text = reader["apellidoM"].ToString();
                        txtCelular.Text = reader["celularPrincipal"].ToString();
                        txtCorreo.Text = reader["correoElectronico"].ToString();
                        cbPais.Text = reader["pais"].ToString();
                        cbCalle.Text = reader["calle"].ToString();
                        txtCP.Text = reader["cp"].ToString();
                        cbCiudad.Text = reader["ciudad"].ToString();
                        cbColonia.Text = reader["colonia"].ToString();
                        cbEstado.Text = reader["estado"].ToString();
                        cbMunicipio.Text = reader["municipio"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se pudo obtener los datos del dueño. " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }
        private void GuardarCambios()
        {
            try
            {
                conexionDB.AbrirConexion();

                int idPais = ObtenerORegistrarIdPais(cbPais.Text);
                int idCalle = ObtenerORegistrarIdCalle(cbCalle.Text);
                int idCp = ObtenerORegistrarIdCp(txtCP.Text);
                //int idCiudad = ObtenerIdPorNombre("Ciudad", cbCiudad.Text);
                int idCiudad = ObtenerORegistrarIdCiudad(cbCiudad.Text);
                int idColonia = ObtenerORegistrarIdColonia(cbColonia.Text);
                int idEstado = ObtenerORegistrarIdEstado(cbEstado.Text);
                int idMunicipio = ObtenerORegistrarIdMunicipio(cbMunicipio.Text);

                string query = @"
                UPDATE Persona 
                SET nombre = @nombre, 
                    apellidoP = @apellidoP, 
                    apellidoM = @apellidoM, 
                    celularPrincipal = @celularPrincipal, 
                    correoElectronico = @correo
                WHERE idPersona = @idPersona;

                UPDATE Direccion
                SET idPais = (SELECT idPais FROM Pais WHERE nombre = @pais),
                    idCalle = (SELECT idCalle FROM Calle WHERE nombre = @calle),
                    idCp = (SELECT idCp FROM Cp WHERE cp = @cp),
                    idCiudad = (SELECT idCiudad FROM Ciudad WHERE nombre = @ciudad),
                    idColonia = (SELECT idColonia FROM Colonia WHERE nombre = @colonia),
                    idEstado = (SELECT idEstado FROM Estado WHERE nombre = @estado),
                    idMunicipio = (SELECT idMunicipio FROM Municipio WHERE nombre = @municipio)
                WHERE idPersona = @idPersona;
                    ";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@apellidoP", txtApellidoP.Text);
                    cmd.Parameters.AddWithValue("@apellidoM", txtApellidoM.Text);
                    cmd.Parameters.AddWithValue("@celularPrincipal", txtCelular.Text);
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@pais", cbPais.Text);
                    cmd.Parameters.AddWithValue("@calle", cbCalle.Text);
                    cmd.Parameters.AddWithValue("@cp", txtCP.Text);
                    cmd.Parameters.AddWithValue("@ciudad", cbCiudad.Text);
                    cmd.Parameters.AddWithValue("@colonia", cbColonia.Text);
                    cmd.Parameters.AddWithValue("@estado", cbEstado.Text);
                    cmd.Parameters.AddWithValue("@municipio", cbMunicipio.Text);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Datos actualizados correctamente.");
                        parentForm.formularioHijo(new DueConsultarDueño(parentForm)); 
                    }
                    else
                    {
                        MessageBox.Show("No se realizaron cambios.");
                    }
                }

                string queryInactivar = "UPDATE Celular SET estado = 'I' WHERE idPersona = @idPersona";
                using (SqlCommand cmd = new SqlCommand(queryInactivar, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);
                    cmd.ExecuteNonQuery();
                }

                foreach (string num in numerosSecundarios)
                {
                    string queryVerificar = "SELECT COUNT(*) FROM Celular WHERE idPersona = @idPersona AND numero = @numero";
                    int count = 0;
                    using (SqlCommand cmd = new SqlCommand(queryVerificar, conexionDB.GetConexion()))
                    {
                        cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);
                        cmd.Parameters.AddWithValue("@numero", num);
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (count > 0)
                    {
                        string queryActualizar = "UPDATE Celular SET estado = '1' WHERE idPersona = @idPersona AND numero = @numero";
                        using (SqlCommand cmd = new SqlCommand(queryActualizar, conexionDB.GetConexion()))
                        {
                            cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);
                            cmd.Parameters.AddWithValue("@numero", num);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string queryInsert = "INSERT INTO Celular (idPersona, numero, estado) VALUES (@idPersona, @numero, '1')";
                        using (SqlCommand cmd = new SqlCommand(queryInsert, conexionDB.GetConexion()))
                        {
                            cmd.Parameters.AddWithValue("@idPersona", DatoEmpleado);
                            cmd.Parameters.AddWithValue("@numero", num);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
                DueConsultarDueño formularioHijo = new DueConsultarDueño(parentForm);
                formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
                parentForm.formularioHijo(formularioHijo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar datos: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
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
        private int ObtenerORegistrarIdMunicipio(string municipio)
        {
            int idMunicipio = ObtenerIdPorNombre("Municipio", municipio);
            if (idMunicipio == 0)
            {
                string queryInsert = "INSERT INTO Municipio (nombre) VALUES (@municipio); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(queryInsert, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@municipio", municipio);
                    object result = cmd.ExecuteScalar();
                    idMunicipio = Convert.ToInt32(result);
                }
            }
            return idMunicipio;
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
                MostrarCB("SELECT nombre FROM Municipio", cbMunicipio);
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
        private void btnMostrarMascota_Click(object sender, EventArgs e)
        {
            int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
            DueMascotadeDue formularioHijo = new DueMascotadeDue(parentForm, "DueModificarDue");
            formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
            parentForm.formularioHijo(formularioHijo);

            //parentForm.formularioHijo(new DueMascotadeDue(parentForm, "DueModificarDue"));
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (ValidarCampos())
            {
                GuardarCambios();
                //parentForm.formularioHijo(new DueConsultarDueño(parentForm));
            }

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
            DueConsultarDueño formularioHijo = new DueConsultarDueño(parentForm);
            formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
            parentForm.formularioHijo(formularioHijo);
            //parentForm.formularioHijo(new DueConsultarDueño(parentForm));
        }

        private bool CaracterValido(char c)
        {
            string caracteresPermitidos = @"^[a-zA-Z0-9._%+-@]+$";
            return Regex.IsMatch(c.ToString(), caracteresPermitidos);
        }

        private void txtCp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            if (!CaracterValido(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellidoP.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoM.Text) || string.IsNullOrWhiteSpace(txtCelular.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtCP.Text) ||
                string.IsNullOrWhiteSpace(cbPais.Text) || string.IsNullOrWhiteSpace(cbCalle.Text) ||
                string.IsNullOrWhiteSpace(cbCiudad.Text) || string.IsNullOrWhiteSpace(cbColonia.Text)||
                string.IsNullOrWhiteSpace(cbMunicipio.Text))
         
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

        private void btnAgregarNumeroSecundario_Click(object sender, EventArgs e)
        {
            string numero = txtNumSec.Text.Trim();
            if (!string.IsNullOrEmpty(numero))
            {
                numerosSecundarios.Add(numero);
                ActualizarDataGrid();
                txtNumSec.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido.");
            }
        }

        private void dtNumeros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string num = dtNumeros.Rows[e.RowIndex].Cells["Número"].Value.ToString();
                DialogResult result = MessageBox.Show("¿Desea eliminar el número: " + num + "?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    numerosSecundarios.Remove(num);
                    ActualizarDataGrid();
                }
            }
        }
    }
}
