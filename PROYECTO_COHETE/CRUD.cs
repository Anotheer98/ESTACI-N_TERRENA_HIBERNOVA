using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaCommon;
using System.Runtime.InteropServices;
using System.IO;

namespace PROYECTO_COHETE
{
    public partial class CRUD : Form
    {
        // GLOBALES

        byte[] imagen;
        bool editar = false;

        // MOVER EL FORMULARIO
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private Principal principal;
        public CRUD(Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
            principal.Show();
        }

        private void btnClosed_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the application?", "Close Application", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            cargarRegistros("");
        }

        private DataGridView cargarRegistros(string condicion)
        {
            ModeloUsuario modelUser = new ModeloUsuario();
            dataGridView1.DataSource = modelUser.VerRegistros(condicion);

            return dataGridView1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Usuarios parametrosUsuario = new Usuarios();
            long boleta;
            int idRol, idDivision;

           if(editar == false)
            {
                if ((long.TryParse(txtIdBoleta.Text, out boleta) && int.TryParse(txtIdDivison.Text, out idDivision) && int.TryParse(txtIdRol.Text, out idRol)))
                {
                    parametrosUsuario.Boleta = boleta;
                    parametrosUsuario.Nombre = txtNombre.Text;
                    parametrosUsuario.Indicativo = txtIndicativo.Text;
                    parametrosUsuario.Apellido = txtApellido.Text;
                    parametrosUsuario.Escuela = txtEscuela.Text;
                    parametrosUsuario.Carrera = txtCarrera.Text;
                    parametrosUsuario.Especialidades = txtEspecialidad.Text;
                    parametrosUsuario.Estatus = txtEstatus.Text;
                    parametrosUsuario.Username = txtUserName.Text;
                    parametrosUsuario.Id_rol = idRol;
                    parametrosUsuario.Id_division = idDivision;
                    parametrosUsuario.Clave = txtClave.Text;
                    parametrosUsuario.Patron = txtPatron.Text;
                }

                else
                {
                    MessageBox.Show("Please enter valid numeric values in the 'Ballot', 'RoleId' or 'DivisionID' fields", "ALERT", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                if (AgregarUsuarios(parametrosUsuario))
                {
                    MessageBox.Show("User added successfully.");
                }

                else
                {
                    MessageBox.Show("Incorrect registration", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                if ((long.TryParse(txtIdBoleta.Text, out boleta) && int.TryParse(txtIdDivison.Text, out idDivision) && int.TryParse(txtIdRol.Text, out idRol)))
                {
                    parametrosUsuario.Boleta = boleta;
                    parametrosUsuario.Nombre = txtNombre.Text;
                    parametrosUsuario.Indicativo = txtIndicativo.Text;
                    parametrosUsuario.Apellido = txtApellido.Text;
                    parametrosUsuario.Escuela = txtEscuela.Text;
                    parametrosUsuario.Carrera = txtCarrera.Text;
                    parametrosUsuario.Especialidades = txtEspecialidad.Text;
                    parametrosUsuario.Estatus = txtEstatus.Text;
                    parametrosUsuario.Username = txtUserName.Text;
                    parametrosUsuario.Id_rol = idRol;
                    parametrosUsuario.Id_division = idDivision;
                    parametrosUsuario.Clave = txtClave.Text;
                    parametrosUsuario.Patron = txtPatron.Text;
                }

                else
                {
                    MessageBox.Show("Valores inválidos", "ALERT", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                if (EditarUsuarios(parametrosUsuario))
                {
                    MessageBox.Show("Actualización exitosa.");
                }
                else
                {
                    MessageBox.Show("Ocurrió un error");
                }
            }
        }


        private bool AgregarUsuarios(Usuarios usuario)
        {
            ModeloUsuario modelUser = new ModeloUsuario();
            return modelUser.AgregarUsuarios(usuario);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cargarRegistros(txtFilter.Text);
        }

        private void CRUD_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnExamine_Click(object sender, EventArgs e)
        {
            OpenFileDialog examineImage = new OpenFileDialog();
            examineImage.Title = "Examine Image";

            if(examineImage.ShowDialog() == DialogResult.OK)
            {
                PicImage.Image = Image.FromStream(examineImage.OpenFile());

                MemoryStream memory = new MemoryStream();
                PicImage.Image.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);

                imagen = memory.ToArray();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            editar = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtNombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                txtApellido.Text = dataGridView1.CurrentRow.Cells["Apellido"].Value.ToString();
                txtUserName.Text = dataGridView1.CurrentRow.Cells["Username"].Value.ToString();
                txtIdBoleta.Text = dataGridView1.CurrentRow.Cells["Boleta"].Value.ToString();
                txtIndicativo.Text = dataGridView1.CurrentRow.Cells["Indicativo"].Value.ToString();
                txtEstatus.Text = dataGridView1.CurrentRow.Cells["Estatus"].Value.ToString();
                txtCarrera.Text = dataGridView1.CurrentRow.Cells["Carrera"].Value.ToString();
                txtEscuela.Text = dataGridView1.CurrentRow.Cells["Escuela"].Value.ToString();
                txtEspecialidad.Text = dataGridView1.CurrentRow.Cells["Especialidades"].Value.ToString();
                txtIdDivison.Text = dataGridView1.CurrentRow.Cells["Id_division"].Value.ToString();
                txtIdRol.Text = dataGridView1.CurrentRow.Cells["Id_rol"].Value.ToString();          
            }          
        }

        private bool EditarUsuarios(Usuarios parametrosUsuario)
        {
            ModeloUsuario model = new ModeloUsuario();
            return model.EditarUsuarios(parametrosUsuario);
        }
    }
}
