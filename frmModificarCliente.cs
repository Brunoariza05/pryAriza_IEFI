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
    public partial class frmModificarCliente : Form
    {
        public frmModificarCliente()
        {
            InitializeComponent();
        }
        OleDbConnection conexion = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\bruno\OneDrive\Documentos\Visual Studio 2022\proyectos\pryAriza_IEFI\bin\Debug\ClienteBD.mdb");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string consulta = "SELECT * FROM Datos WHERE DNI = ?";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@DNI", txtDniBusqueda.Text);

                conexion.Open();
                OleDbDataReader lector = cmd.ExecuteReader();

                if (lector.Read())
                {
                    txtNombre.Text = lector["Nombre"].ToString();
                    txtEdad.Text = lector["Edad"].ToString();
                    txtPeso.Text = lector["Peso"].ToString();

                    btnEliminar.Enabled = true;
                    btnModificar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.");
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            txtNombre.Enabled = true;
            txtEdad.Enabled = true;
            txtPeso.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string consulta = "DELETE FROM Datos WHERE DNI=?";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@DNI", txtDniBusqueda.Text);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                MessageBox.Show("Cliente eliminado.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void frmModificarCliente_Load(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void txtDniBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtDniBusqueda.Text != "")
            {
                btnBuscar.Enabled = true;
            } else
            {
                btnBuscar.Enabled = false;
            }
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
            if (txtNombre.Text != "" && txtEdad.Text != "" && txtPeso.Text != "")
            {
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
            }
            else
            {
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
            }
        }
        private void LimpiarCampos()
        {
            txtDniBusqueda.Clear();
            txtNombre.Clear();
            txtEdad.Clear();
            txtPeso.Clear();
            txtNombre.Enabled = false;
            txtEdad.Enabled = false;
            txtPeso.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string consulta = "UPDATE Datos SET Nombre=?, Edad=?, Peso=? WHERE DNI=?";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);

                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Edad", Convert.ToInt32(txtEdad.Text));
                cmd.Parameters.AddWithValue("@Peso", Convert.ToDouble(txtPeso.Text));
                cmd.Parameters.AddWithValue("@DNIOriginal", txtDniBusqueda.Text);

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                MessageBox.Show("Datos actualizados.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }
    }
}
