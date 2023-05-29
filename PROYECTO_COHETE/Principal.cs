using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaCommon;

namespace PROYECTO_COHETE
{
    public partial class Principal : Form
    {
        private Form1 frm1;

        public Principal(Form1 frm1)
        {
            InitializeComponent();
            this.frm1 = frm1;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to log out?", "Warning", MessageBoxButtons.YesNo, 
            MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to close the application?", "Close Application",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Load_UserData();
        }

        private void Load_UserData()
        {
            lbName.Text = UserLoginCache.NOMBRE + " " + UserLoginCache.APELLIDO;
            lbBoleta.Text = UserLoginCache.BOLETA.ToString();
            lbDIVISON.Text = UserLoginCache.ID_DIVISION.ToString();
            lbROL.Text = UserLoginCache.ID_ROL.ToString();
            lbSTATUS.Text = UserLoginCache.ESTATUS;
            lbCAREER.Text = UserLoginCache.CARRERA;
            lbSCHOOL.Text = UserLoginCache.ESCUELA;
            lbSPECIALITY.Text = UserLoginCache.ESPECIALIDADES;
           
        }

        private void Principal_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lbTime.Text = DateTime.Now.ToString("HH:mm:ss");
            lbTime.TextAlign = ContentAlignment.TopCenter;
            this.lbFecha.Text = DateTime.Now.ToLongDateString();
            lbTime.TextAlign = ContentAlignment.TopCenter;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CRUD optionAdministrator = new CRUD(this);
            optionAdministrator.Show();
            this.Hide();
        }  
        
    }
}

/*Tengo un problema
Estoy desarrollando una aplicación de windowsform en c#
tengo 3 formularios
cada uno tiene eventos en sus botones
en el principal que es un login, sería el formulario 1, pide contraseña y usuario para ingresar al otro formulario 
cuando ingresas al nuevo formulario después de que la contraseña y usuario sean correctos, se abre otro que sería el
formulario 2 y tiene un botón para volver a regresar a login u otros botones para acceder a otro formularios que sería el 3. 
Cuando aprieto uno de los botones de ese formulario para ingresar al formulario 3, me manda el formulario 3 y desde el formulario 
3 hay un botón con un evento para regresar al formulario 2.
El problema está cuando me regreso al formulario 2 y desde el formulario 2 quiero regresar al login, aprieto el botón de regresar al
login que sería formulario 1 y se cierra todo sin mandarme al login

Pero si meto usuario y contraseña para que me mande al formulario 2 y sin abrir el formulario 3 aprieto el botón para regresar al login, 
sin problemas me manda a ese formulario.

¿Cuál podría ser el problema?*/