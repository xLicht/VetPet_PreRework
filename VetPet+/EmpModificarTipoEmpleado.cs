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
    public partial class EmpModificarTipoEmpleado : FormPadre
    {
        public int DatoEmpleado { get; set; }
        private conexionDaniel conexionDB = new conexionDaniel();
        public EmpModificarTipoEmpleado(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void EmpModificarTipoEmpleado_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Dato Recibido: " + DatoEmpleado);
            CargarDatosTipoEmpleado();
            CargarModulosTipoEmpleado();
        }
        private void CargarDatosTipoEmpleado()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT nombre
                    FROM TipoEmpleado
                    WHERE idTipoEmpleado = @idTipoEmpleado";

                using (SqlCommand comandoSQL = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", DatoEmpleado);
                    SqlDataReader lectorSQL = comandoSQL.ExecuteReader();

                    if (lectorSQL.Read())
                    {
                        txtNombre.Text = lectorSQL["nombre"].ToString();
                        // rtDescripcion.Text = lectorSQL["descripcion"].ToString(); // Comentado por ahora
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al obtener el tipo de empleado: " + error.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void CargarModulosTipoEmpleado()
        {
            try
            {
                conexionDB.AbrirConexion();

                string query = @"SELECT Modulo.nombre 
                    FROM TipoEmpleado_Modulo
                    INNER JOIN Modulo ON TipoEmpleado_Modulo.idModulo = Modulo.idModulo
                    WHERE TipoEmpleado_Modulo.idTipoEmpleado = @idTipoEmpleado";

                using (SqlCommand comandoSQL = new SqlCommand(query, conexionDB.GetConexion()))
                {
                    comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", DatoEmpleado);
                    SqlDataReader lectorSQL = comandoSQL.ExecuteReader();

                    while (lectorSQL.Read())
                    {
                        string nombreModulo = lectorSQL["nombre"].ToString();

                        if (nombreModulo == "Ventas")
                        {
                            cbVentas.Checked = true;
                        }
                        else if (nombreModulo == "Historiales Medicos")
                        {
                            cbHistorialMedico.Checked = true;
                        }
                        else if (nombreModulo == "Mascotas")
                        {
                            cbMascotas.Checked = true;
                        }
                        else if (nombreModulo == "Dueños")
                        {
                            cbDueños.Checked = true;
                        }
                        else if (nombreModulo == "Citas")
                        {
                            cbCitas.Checked = true;
                        }
                        else if (nombreModulo == "Citas Medicas")
                        {
                            cbCitasMedicas.Checked = true;
                        }
                        else if (nombreModulo == "Medicamentos")
                        {
                            cbMedicamentos.Checked = true;
                        }
                        else if (nombreModulo == "Servicios")
                        {
                            cbServicios.Checked = true;
                        }
                        else if (nombreModulo == "Cortes")
                        {
                            cbCortes.Checked = true;
                        }
                        else if (nombreModulo == "Proveedores")
                        {
                            cbProvedores.Checked = true;
                        }
                        else if (nombreModulo == "Pedidos")
                        {
                            cbPedidos.Checked = true;
                        }
                        else if (nombreModulo == "Productos")
                        {
                            cbProductos.Checked = true;
                        }
                        else if (nombreModulo == "Empleados")
                        {
                            cbEmpleados.Checked = true;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al obtener los módulos del tipo de empleado: " + error.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void ActualizarTipoEmpleado()
        {
            try
            {
                conexionDB.AbrirConexion();

                string actualizarNombreQuery = @"UPDATE TipoEmpleado 
                SET nombre = @nombre
                WHERE idTipoEmpleado = @idTipoEmpleado";

                using (SqlCommand comandoSQL = new SqlCommand(actualizarNombreQuery, conexionDB.GetConexion()))
                {
                    comandoSQL.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", DatoEmpleado);
                    comandoSQL.ExecuteNonQuery();
                }


                Dictionary<string, bool> modulosAsignados = new Dictionary<string, bool>();

                string obtenerModulosQuery = @"SELECT Modulo.nombre 
                FROM TipoEmpleado_Modulo
                INNER JOIN Modulo ON TipoEmpleado_Modulo.idModulo = Modulo.idModulo
                WHERE TipoEmpleado_Modulo.idTipoEmpleado = @idTipoEmpleado";

                using (SqlCommand comandoSQL = new SqlCommand(obtenerModulosQuery, conexionDB.GetConexion()))
                {
                    comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", DatoEmpleado);
                    SqlDataReader lectorSQL = comandoSQL.ExecuteReader();

                    while (lectorSQL.Read())
                    {
                        modulosAsignados[lectorSQL["nombre"].ToString()] = true;
                    }

                    lectorSQL.Close();
                }

                Dictionary<string, CheckBox> modulosCheckBox = new Dictionary<string, CheckBox>()
                {
                    { "Ventas", cbVentas },
                    { "Historial Médico", cbHistorialMedico },
                    { "Mascotas", cbMascotas },
                    { "Dueños", cbDueños },
                    { "Citas", cbCitas },
                    { "Citas Médicas", cbCitasMedicas },
                    { "Medicamentos", cbMedicamentos },
                    { "Servicios", cbServicios },
                    { "Cortes", cbCortes },
                    { "Proveedores", cbProvedores },
                    { "Pedidos", cbPedidos },
                    { "Productos", cbProductos },
                    { "Empleados", cbEmpleados }
                };

                foreach (var modulo in modulosCheckBox)
                {
                    bool estaMarcado = modulo.Value.Checked;
                    bool yaAsignado = modulosAsignados.ContainsKey(modulo.Key);

                    if (estaMarcado && !yaAsignado)  // Insertar si está marcado y no está asignado
                    {
                        // Obtener idModulo antes de insertar
                        string obtenerIdModuloQuery = "SELECT idModulo FROM Modulo WHERE nombre = @nombreModulo";
                        int? idModulo = null;

                        using (SqlCommand comandoSQL = new SqlCommand(obtenerIdModuloQuery, conexionDB.GetConexion()))
                        {
                            comandoSQL.Parameters.AddWithValue("@nombreModulo", modulo.Key);
                            object resultado = comandoSQL.ExecuteScalar();
                            if (resultado != null)
                            {
                                idModulo = Convert.ToInt32(resultado);
                            }
                        }

                        if (idModulo.HasValue) // Solo insertar si encontró un idModulo válido
                        {
                            string insertarModuloQuery = @"INSERT INTO TipoEmpleado_Modulo (idTipoEmpleado, idModulo) 
                            VALUES (@idTipoEmpleado, @idModulo)";

                            using (SqlCommand comandoSQL = new SqlCommand(insertarModuloQuery, conexionDB.GetConexion()))
                            {
                                comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", DatoEmpleado);
                                comandoSQL.Parameters.AddWithValue("@idModulo", idModulo.Value);
                                comandoSQL.ExecuteNonQuery();
                            }
                        }
                    }
                    else if (!estaMarcado && yaAsignado)  // Eliminar si NO está marcado pero está asignado
                    {
                        string eliminarModuloQuery = @" DELETE FROM TipoEmpleado_Modulo 
                        WHERE idTipoEmpleado = @idTipoEmpleado 
                        AND idModulo = (SELECT idModulo FROM Modulo WHERE nombre = @nombreModulo)";

                        using (SqlCommand comandoSQL = new SqlCommand(eliminarModuloQuery, conexionDB.GetConexion()))
                        {
                            comandoSQL.Parameters.AddWithValue("@idTipoEmpleado", DatoEmpleado);
                            comandoSQL.Parameters.AddWithValue("@nombreModulo", modulo.Key);
                            comandoSQL.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Tipo de empleado actualizado correctamente.");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al actualizar el tipo de empleado: " + error.Message);
            }
            finally
            {
                conexionDB.CerrarConexion();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarTipoEmpleado();
            int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
            EmpConsultarTipoEmpleado formularioHijo = new EmpConsultarTipoEmpleado(parentForm);
            formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
            parentForm.formularioHijo(formularioHijo);

            //parentForm.formularioHijo(new EmpConsultarTipoEmpleado(parentForm));
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            int idEmpleadoSeleccionado = Convert.ToInt32(DatoEmpleado);
            EmpConsultarTipoEmpleado formularioHijo = new EmpConsultarTipoEmpleado(parentForm);
            formularioHijo.DatoEmpleado = idEmpleadoSeleccionado;
            parentForm.formularioHijo(formularioHijo);
            //parentForm.formularioHijo(new EmpConsultarTipoEmpleado(parentForm));
        }
    }
}
