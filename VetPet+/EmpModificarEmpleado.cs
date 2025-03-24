using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetPet_;

namespace VetPet_
{
    public partial class EmpModificarEmpleado : FormPadre
    {
        public int DatoEmpleado { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        int idregreso;
        public EmpModificarEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpModificarEmpleado_Load(object sender, EventArgs e)
        {
            CargarCB();
            MostrarDato();
            idregreso = DatoEmpleado;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            advertencia();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            DialogResult resultado = MessageBox.Show("Los datos no se han guardado. ¿Desea continuar sin guardar?","Advertencia",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
           
            if (resultado == DialogResult.OK)
            {
                EmpConsultarEmpleado consultarEmpleadoForm = new EmpConsultarEmpleado(parentForm);
                consultarEmpleadoForm.DatoEmpleado = idregreso;
                parentForm.formularioHijo(consultarEmpleadoForm);
            }
            else
            {  }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                ActualizarEmpleado();
                EmpConsultarEmpleado consultarEmpleadoForm = new EmpConsultarEmpleado(parentForm);
                consultarEmpleadoForm.DatoEmpleado = idregreso; 
                parentForm.formularioHijo(consultarEmpleadoForm);
            }
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
                //pasarDato();
                EmpConsultarEmpleado consultarEmpleadoForm = new EmpConsultarEmpleado(parentForm);
                consultarEmpleadoForm.DatoEmpleado = idregreso;
                parentForm.formularioHijo(consultarEmpleadoForm);
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
                          p.nombre, p.apellidoP, p.apellidoM, p.celularPrincipal, p.correoElectronico,
                          t.nombre AS tipoEmpleado,
                          pais.nombre AS pais, calle.nombre AS calle, 
                          cp.cp, ciudad.nombre AS ciudad, colonia.nombre AS colonia,
                          estado.nombre AS estado
                   FROM Empleado e
                   JOIN Persona p ON e.idPersona = p.idPersona
                   JOIN TipoEmpleado t ON e.idTipoEmpleado = t.idTipoEmpleado
                   LEFT JOIN Direccion d ON e.idEmpleado = d.idPersona
                   LEFT JOIN Pais pais ON d.idPais = pais.idPais
                   LEFT JOIN Calle calle ON d.idCalle = calle.idCalle
                   LEFT JOIN Cp cp ON d.idCp = cp.idCp
                   LEFT JOIN Ciudad ciudad ON d.idCiudad = ciudad.idCiudad
                   LEFT JOIN Colonia colonia ON d.idColonia = colonia.idColonia
                   LEFT JOIN Estado estado ON d.idEstado = estado.idEstado
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
                        txtCelular.Text = reader["celularPrincipal"].ToString();
                        txtCorreo.Text = reader["correoElectronico"].ToString();
                        cbTipo.Text = reader["tipoEmpleado"].ToString();
                        cbPais.SelectedItem = reader["pais"].ToString();
                        cbCalle.SelectedItem = reader["calle"].ToString();
                        txtCP.Text = reader["cp"].ToString();
                        cbCiudad.SelectedItem = reader["ciudad"].ToString();
                        cbColonia.SelectedItem = reader["colonia"].ToString();
                        cbEstado.SelectedItem = reader["estado"].ToString();

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

        public void pasarDato()
        {
            EmpConsultarEmpleado EmpConsultarEmpleadoForm = new EmpConsultarEmpleado(parentForm);
            EmpConsultarEmpleadoForm.DatoEmpleado = this.DatoEmpleado;
            parentForm.formularioHijo(EmpConsultarEmpleadoForm);
        }

        private bool CaracterValido(char c)
        {
            string caracteresPermitidos = @"^[a-zA-Z0-9._%+-@]+$";
            return Regex.IsMatch(c.ToString(), caracteresPermitidos);
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

        private void GuardarCambios()
        {
            try
            {
                conexionDB.AbrirConexion();

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
                    idEstado = (SELECT idEstado FROM Estado WHERE nombre = @estado)
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

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Datos actualizados correctamente.");
                        parentForm.formularioHijo(new DueConsultarDueño(parentForm)); // Recargar vista
                    }
                    else
                    {
                        MessageBox.Show("No se realizaron cambios.");
                    }
                }
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

        private void ActualizarEmpleado()
        {
            try
            {
                conexionDB.AbrirConexion();

                int idPais = ObtenerORegistrarIdPais(cbPais.Text);
                int idCalle = ObtenerORegistrarIdCalle(cbCalle.Text);
                int idCp = ObtenerORegistrarIdCp(txtCP.Text);
                int idCiudad = ObtenerIdPorNombre("Ciudad", cbCiudad.Text);  
                int idColonia = ObtenerORegistrarIdColonia(cbColonia.Text);
                int idTipoEmpleado = ObtenerIdPorNombre("TipoEmpleado", cbTipo.Text);
                int idEstado = ObtenerORegistrarIdEstado( cbEstado.Text);
                
                string query = @"
                    UPDATE Empleado
                    SET usuario = @usuario, 
                        contraseña = @contraseña, 
                        palabraClave = @palabraClave,
                        idTipoEmpleado = @idTipoEmpleado  
                    WHERE idEmpleado = @idEmpleado;

                    UPDATE Persona
                    SET nombre = @nombre, 
                        apellidoP = @apellidoP, 
                        apellidoM = @apellidoM, 
                        celularPrincipal = @celularPrincipal,
                        correoElectronico = @correo
                    WHERE idPersona = (SELECT idPersona FROM Empleado WHERE idEmpleado = @idEmpleado);

                    UPDATE Direccion
                    SET idPais = @idPais,
                        idCalle = @idCalle,
                        idCp = @idCp,
                        idCiudad = @idCiudad,
                        idColonia = @idColonia,
                        idEstado = @idEstado 
                    WHERE idPersona = (SELECT idPersona FROM Empleado WHERE idEmpleado = @idEmpleado);";

                using (SqlCommand cmd = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    cmd.Parameters.AddWithValue("@idEmpleado", DatoEmpleado);
                    cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@contraseña", txtContraseña.Text);
                    cmd.Parameters.AddWithValue("@palabraClave", txtPalabraClave.Text);
                    cmd.Parameters.AddWithValue("@idTipoEmpleado", idTipoEmpleado);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@apellidoP", txtApellidoP.Text);
                    cmd.Parameters.AddWithValue("@apellidoM", txtApellidoM.Text);
                    cmd.Parameters.AddWithValue("@celularPrincipal", txtCelular.Text);
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@idPais", idPais);
                    cmd.Parameters.AddWithValue("@idCalle", idCalle);
                    cmd.Parameters.AddWithValue("@idCp", idCp);
                    cmd.Parameters.AddWithValue("@idCiudad", idCiudad);
                    cmd.Parameters.AddWithValue("@idColonia", idColonia);
                    cmd.Parameters.AddWithValue("@idEstado", idEstado);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Datos actualizados correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se realizaron cambios.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message);
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

        private void cbCalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void cbColonia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellidoP.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoM.Text) || string.IsNullOrWhiteSpace(txtCelular.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||string.IsNullOrWhiteSpace(txtCP.Text) ||
                string.IsNullOrWhiteSpace(cbPais.Text) || string.IsNullOrWhiteSpace(cbCalle.Text) ||
                string.IsNullOrWhiteSpace(cbCiudad.Text) || string.IsNullOrWhiteSpace(cbColonia.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text) ||  string.IsNullOrWhiteSpace(txtPalabraClave.Text))
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

        private void txtPalabraClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
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
    }
}

