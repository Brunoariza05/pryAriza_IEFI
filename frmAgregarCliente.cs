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
    public partial class frmAgregarCliente : Form
    {
        public frmAgregarCliente()
        {
            InitializeComponent();
        }
        OleDbConnection conexion = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\bruno\OneDrive\Documentos\Visual Studio 2022\proyectos\pryAriza_IEFI\bin\Debug\ClienteBD.mdb");
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string consulta = "INSERT INTO Datos (DNI, Nombre, Edad, Peso) VALUES (?, ?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);

                cmd.Parameters.AddWithValue("@DNI", txtDni.Text);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Edad", Convert.ToInt32(txtEdad.Text));
                cmd.Parameters.AddWithValue("@Peso", Convert.ToDouble(txtPeso.Text));

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                MessageBox.Show("✅ Cliente agregado correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message);
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }
        private void LimpiarCampos()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtEdad.Clear();
            txtPeso.Clear();
        }
        private void frmAgregarCliente_Load(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            Controlar();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Controlar();
        }

        private void txtEdad_TextChanged(object sender, EventArgs e)
        {
            Controlar();
        }

        private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            Controlar();
        }

        private void Controlar()
        {
            if (txtDni.Text != "" && txtNombre.Text != "" && txtEdad.Text != "" && txtPeso.Text != "")
            {
                btnAgregar.Enabled = true;
            } else
            {
                btnAgregar.Enabled = false;
            }
        }
    }
}
