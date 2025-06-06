using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryAriza_IEFI
{
    public partial class frmAuditoria : Form
    {
        public frmAuditoria()
        {
            InitializeComponent();
        }

        OleDbConnection conexion = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\baseDeDatos\\AuditoriaBD.mdb");

        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            lstDatosLogin.Items.Clear();
            
            try
            {
                string consulta = "SELECT * FROM Auditoria";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);

                conexion.Open();
                OleDbDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    string usuario = lector["Usuario"].ToString();
                    string tiempo = lector["TiempoSesion"].ToString();
                    lstDatosLogin.Items.Add("El usuario " + usuario + " estuvo en la sesion " + tiempo);
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar auditoría: " + ex.Message);
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

    }
}
