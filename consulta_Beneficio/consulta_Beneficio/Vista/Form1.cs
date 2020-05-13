using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using consulta_Beneficio.Model;
using System.Runtime.InteropServices;
using consulta_Beneficio.Vista;

namespace consulta_Beneficio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void txtNum_Enter(object sender, EventArgs e)
        {
            if (txtNum.Text== "Ingresa tu numero de DUI")
            {
                txtNum.Text = "";
            }
        }

        private void txtNum_Leave(object sender, EventArgs e)
        {
            if (txtNum.Text=="")
            {
                txtNum.Text= "Ingresa tu numero de DUI";
            }
        }
        
        private void btnConsult_Click(object sender, EventArgs e)
        {
            using (gobierno_svEntities db=new gobierno_svEntities())
            {
                //Inge no pude mostrar el bendito nombre del dui me lleve toda la tarde intendandolo :(
                var listar = from consulta in db.duis
                             where consulta.Dui == txtNum.Text
                             select consulta;
                if (listar.Count()> 0)
                {
                        lblTexto.Visible = true;
                        lblTexto2.Visible = true;
                        lblTexto3.Visible = true;
                    pbSimbolo.Visible = true;
                    pbSimbolo.Image = consulta_Beneficio.Properties.Resources.familia;
                    lblTexto3.Text = "Eres beneficiario del apoyo economico de 200$ para alimentacion de tu familia";
                    lblTexto2.Text = "Puedes pasar a retirar tu bono en cualquiera de nuestras agencias";

                }
                else
                {


                    lblTexto.Visible = false;
                    lblTexto2.Visible = true;
                    lblTexto3.Visible = true;
                    pbSimbolo.Visible = true;
                    pbSimbolo.Image = consulta_Beneficio.Properties.Resources.exclamation_md;
                    lblTexto3.Text = "Este Dui no aparece en nuestro sistema";
                        lblTexto2.Text = "Prueba con el Dui de otro miembro de la familia";
                    
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTexto.Visible = false;
            lblTexto2.Visible = false;
            lblTexto3.Visible = false;
            lblNombre.Visible = false;
            pbSimbolo.Visible = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Admin ad = new Admin();
            ad.Show();
            this.Hide();
        }
    }
}
