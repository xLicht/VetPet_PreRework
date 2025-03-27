using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class InfoUsuario : FormPadre
    {
        Form1 parentFo;
        int idUsuario;
        ConexionMaestra conexMas;
        SqlConnection conex;

        public InfoUsuario(Form1 parent, int IdUsuario)
        {
            InitializeComponent();
            parentFo = parent;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            idUsuario = IdUsuario;
            //Timer closeTimer = new Timer();
            //closeTimer.Interval = 400; // Verificar cada 100ms
            //closeTimer.Tick += (s, e) =>
            //{
            //    if (!this.Bounds.Contains(Cursor.Position))
            //    {
            //        this.Close();
            //    }
            //};
            //closeTimer.Start();

            //this.FormClosed += (s, e) => closeTimer.Stop();
        }

        private void InfoUsuario_Load(object sender, EventArgs e)
        {
            conexMas = new ConexionMaestra();
            conex = conexMas.CrearConexion();

            try
            {
                conex.Open();

                string q = @"select p.nombre, p.apellidoP, p.apellidoM, e.usuario, p.correoElectronico, p.celularPrincipal from Empleado e inner join Persona p on e.idPersona = p.idPersona where e.idEmpleado = @idEmpleado; ";
                SqlCommand comando = new SqlCommand(q, conex);
                comando.Parameters.AddWithValue("@idEmpleado", idUsuario);
                SqlDataReader lector = comando.ExecuteReader();
                int i = 0;
                while (lector.Read())
                {
                    string nombre = lector.GetString(0);
                    string apeP = lector.GetString(1);
                    string apeM = lector.GetString(2);
                    string usuario = lector.GetString(3);
                    string correo = lector.GetString(4);
                    string celular = lector.GetString(5);

                    lblNombre.Text = nombre + " " + apeP + " " + apeM;
                    lblUser.Text = usuario;
                    lblCorreo.Text = correo;
                    lblNumero.Text = celular;
                    i++;
                }

                
                conex.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conex.Close();
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            ReiniciarAplicacion();
        }
        void ReiniciarAplicacion()
        {
            string applicationPath = Application.ExecutablePath;
            Process.Start(new ProcessStartInfo
            {
                FileName = applicationPath,
                UseShellExecute = true
            });
            Application.Exit();
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            EmpModificarEmpleado empModificarEmpleado = new EmpModificarEmpleado(parentFo);
            empModificarEmpleado.DatoEmpleado = idUsuario;

            parentFo.formularioHijo(empModificarEmpleado);
        }
    }
}
