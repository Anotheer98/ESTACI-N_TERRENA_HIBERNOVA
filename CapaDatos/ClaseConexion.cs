using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public abstract class ClaseConexion
    {
        private readonly string conexionSql;

        public ClaseConexion()
        {
            conexionSql = "Server=ANOTHER-GALAXY\\SQLEXPRESS;DataBase=Usuario;integrated security=true";
        }

        protected SqlConnection getConexionSql()
        {
            return new SqlConnection(conexionSql);
        }
    }
}
