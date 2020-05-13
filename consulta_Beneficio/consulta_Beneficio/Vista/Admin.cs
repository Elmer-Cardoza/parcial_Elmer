using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using consulta_Beneficio.Model;

namespace consulta_Beneficio.Vista
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        void mostrarDatos()
        {
            using (gobierno_svEntities db = new gobierno_svEntities())
            {
                dtvDui.DataSource = db.duis.ToList();
            }
        }
        private void txtNombre_Enter(object sender, EventArgs e)
        {
            if (txtNombre.Text=="NOMBRE")
            {
                txtNombre.Text = "";
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                txtNombre.Text = "NOMBRE";
            }
        }

        private void txtDui_Enter(object sender, EventArgs e)
        {
            if (txtDui.Text=="DUI")
            {
                txtDui.Text = "";
            }
        }

        private void txtDui_Leave(object sender, EventArgs e)
        {
            if (txtDui.Text == "")
            {
                txtDui.Text = "DUI";
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (gobierno_svEntities db = new gobierno_svEntities())
            {
                duis du = new duis();

                du.Nombre = txtNombre.Text;
                du.Dui = txtDui.Text;

                db.duis.Add(du);
                db.SaveChanges();
            }
            MessageBox.Show("Datos guardados correctamente...\nPulce aceptar para continuar");
            mostrarDatos();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            mostrarDatos();
        }

        private void dtvDui_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String nombre = dtvDui.CurrentRow.Cells[1].Value.ToString();
            String dui = dtvDui.CurrentRow.Cells[2].Value.ToString();



            txtNombre.Text = nombre;
            txtDui.Text = dui;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
