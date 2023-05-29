using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaCommon
{
    public static class UserLoginCache
    {
        public static long BOLETA { get; set; }
        public static string NOMBRE { get; set; }
        public static string INDICATIVO { get; set; }
        public static string APELLIDO { get; set; }
        public static string ESCUELA { get; set; }
        public static string CARRERA { get; set; }
        public static string ESPECIALIDADES { get; set; }
        public static string ESTATUS { get; set; }
        public static string USERNAME { get; set; }
        public static int ID_ROL { get; set; }
        public static int ID_DIVISION { get; set; }
        public static string CLAVE { get; set; }
        public static string PATRON { get; set; }

    }

    public class Usuarios
    {
        long boleta;
        string nombre;
        string indicativo;
        string apellido;
        string escuela;
        string carrera;
        string especialidades;
        string estatus;
        string username;
        int id_rol;
        int id_division;
        string rol;
        string division;
        string clave;
        string patron;

        public long Boleta { get => boleta; set => boleta = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Indicativo { get => indicativo; set => indicativo = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Escuela { get => escuela; set => escuela = value; }
        public string Carrera { get => carrera; set => carrera = value; }
        public string Especialidades { get => especialidades; set => especialidades = value; }
        public string Estatus { get => estatus; set => estatus = value; }
        public string Username { get => username; set => username = value; }
        public int Id_rol { get => id_rol; set => id_rol = value; }
        public int Id_division { get => id_division; set => id_division = value; }
        public string Rol { get => rol; set => rol = value; }
        public string Division { get => division; set => division = value; }
        public string Clave { get => clave; set => clave = value; }
        public string Patron { get => patron; set => patron = value; }
    }
}

  
