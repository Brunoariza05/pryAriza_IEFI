using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryAriza_IEFI
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        OleDbConnection conexion = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\baseDeDatos\\AuditoriaBD.mdb");
        public string usuarioLogueado { get; set; }
        public string FechaIngreso { get; set; }
        public DateTime HoraIngreso { get; set; }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (usuarioLogueado == "admin")
            {
                btnAuditoria.Enabled = true;
                btnUsuarios.Enabled = true;
                stsUsuario.Text = "admin";
                stsFecha.Text = FechaIngreso;
            } 
            else if (usuarioLogueado == "operador")
            {
                btnAuditoria.Enabled = true;
                btnUsuarios.Enabled = false;
                stsUsuario.Text = "operador";
                stsFecha.Text = FechaIngreso;
            }
        }
        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Calcular duración de sesión
            TimeSpan duracionSesion = DateTime.Now - HoraIngreso;
            string tiempoSesion = duracionSesion.Minutes + "m " + duracionSesion.Seconds + "s";

            // Guardar en base de datos
            try
            {
                string consulta = "INSERT INTO Auditoria (Usuario, TiempoSesion) VALUES (?, ?)";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);

                cmd.Parameters.AddWithValue("@Usuario", usuarioLogueado);
                cmd.Parameters.AddWithValue("@TiempoSesion", tiempoSesion);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar auditoría: " + ex.Message);
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            this.Close();
        }

        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            frmAuditoria frmAuditoria = new frmAuditoria();
            frmAuditoria.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios frmUsuarios = new frmUsuarios();
            frmUsuarios.Show();
        }
    }
}
