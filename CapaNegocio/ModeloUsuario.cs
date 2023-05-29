using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaCommon;
using CapaDatos;

namespace CapaNegocio
{
    public class ModeloUsuario
    {
        ClaseDAO daoUser = new ClaseDAO();
        public bool loginUser(string user, string passw)
        {
            return daoUser.Login(user, passw);
        }

        public List<Usuarios> VerRegistros(string condicion)
        {
           List<Usuarios> listaUsuarios = daoUser.verRegistros(condicion);

            return listaUsuarios;
        }

        public bool AgregarUsuarios(Usuarios usuario)
        {
           return daoUser.Agregar_Usuarios(usuario);
        }

        public bool EditarUsuarios(Usuarios usuario)
        {
            return daoUser.Editar_Usuarios(usuario);
        }
    }
}
