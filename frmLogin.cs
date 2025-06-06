using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryAriza_IEFI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        OleDbConnection conexion = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\baseDeDatos\\UsuariosBD.mdb");
        private void frmLogin_Load(object sender, EventArgs e)
        {
            btnIniciarSesion.Enabled = false;
        }

        public void HabilitarBoton()
        {
            if (txtUsuario.Text != "" && txtContraseña.Text != "") 
            {
                btnIniciarSesion.Enabled = true;
            } else
            {
                btnIniciarSesion.Enabled = false;
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            HabilitarBoton();
        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            HabilitarBoton();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string consulta = "SELECT COUNT(*) FROM Users WHERE Usuario = ? AND Contraseña = ?";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);

                cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@Contraseña", txtContraseña.Text);

                conexion.Open();

                int coincidencias = (int)cmd.ExecuteScalar();

                conexion.Close();

                if (coincidencias > 0)
                {
                    frmPrincipal Principal = new frmPrincipal();
                    Principal.usuarioLogueado = txtUsuario.Text;
                    Principal.FechaIngreso = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    Principal.HoraIngreso = DateTime.Now;
                    Principal.Show();
                }
                else
                {
                    MessageBox.Show("❌ Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
    }
}
