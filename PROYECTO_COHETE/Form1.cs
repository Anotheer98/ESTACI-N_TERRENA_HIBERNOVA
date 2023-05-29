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
using CapaNegocio;

namespace PROYECTO_COHETE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // MOVER EL FORMULARIO
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void linkLabel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if(txtUser.Text == "User")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.LightGray;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if(txtUser.Text == "")
            {
                txtUser.Text = "User";
                txtUser.ForeColor = Color.DimGray;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if(txtPass.Text == "Password")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.LightGray;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if(txtPass.Text == "")
            {
                txtPass.Text = "Password";
                txtPass.ForeColor = Color.DimGray;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the application?", "Close Application",
               MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUser.Text != "User")
            {
                if(txtPass.Text != "Password")
                {
                    ModeloUsuario modelUser = new ModeloUsuario();
                    var validarLogin = modelUser.loginUser(txtUser.Text, txtPass.Text);

                    if (validarLogin)
                    {
                        Principal principal = new Principal(this);
                        principal.Show();
                        principal.FormClosed += cerrarSesion;
                        this.Hide();
                    }

                    else
                    {
                        msgError("Incorrect user or password entered");
                        txtPass.Clear();
                        txtUser.Focus();
                    }
                }
                else
                {
                    msgError("Pleace, enter Password");
                }
            }
            else
            {
                msgError("Please, enter User");
            }
        }

        private void msgError(string msg)
        {
            lblErrorMessage.Text = "    " + msg;
            lblErrorMessage.Visible = true;
        }

        private void cerrarSesion(object sender, FormClosedEventArgs e)
        {
            txtUser.Text = "User";
            txtPass.Text = "Password";
            txtPass.UseSystemPasswordChar = false;
            lblErrorMessage.Visible = false;
            this.Show();
        }
    }
}
