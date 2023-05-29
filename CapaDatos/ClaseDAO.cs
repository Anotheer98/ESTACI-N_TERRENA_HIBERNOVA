using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaCommon;

namespace CapaDatos
{
    public class ClaseDAO : ClaseConexion
    {
        //VALIDAR CONTRASEÑA
        public bool Login(string user, string passw)
        {
            string patron = "infinity";

            using(var connection = getConexionSql())
            {
                connection.Open();

                using (var command = new SqlCommand("Validar_Usuarios", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Connection = connection;
                    command.Parameters.Add("@nombre", SqlDbType.VarChar, 80).Value = user;
                    command.Parameters.Add("@clave",  SqlDbType.VarChar, 80).Value = passw;
                    command.Parameters.Add("@patron", SqlDbType.VarChar, 80).Value = patron;

                    SqlDataReader dtReader;

                    dtReader = command.ExecuteReader();

                    if (dtReader.HasRows)
                    {
                        while (dtReader.Read())
                        {
                            UserLoginCache.BOLETA = dtReader.GetInt64(dtReader.GetOrdinal("boleta"));
                            UserLoginCache.NOMBRE = dtReader.GetString(dtReader.GetOrdinal("nombre"));
                            UserLoginCache.INDICATIVO = dtReader.GetString(dtReader.GetOrdinal("indicativo"));
                            UserLoginCache.APELLIDO = dtReader.GetString(dtReader.GetOrdinal("aPaterno"));
                            UserLoginCache.ESCUELA = dtReader.GetString(dtReader.GetOrdinal("escuela"));
                            UserLoginCache.CARRERA = dtReader.GetString(dtReader.GetOrdinal("carrera"));
                            UserLoginCache.ESPECIALIDADES = dtReader.GetString(dtReader.GetOrdinal("especialidades"));
                            UserLoginCache.ESTATUS = dtReader.GetString(dtReader.GetOrdinal("estatus"));
                            UserLoginCache.USERNAME = dtReader.GetString(dtReader.GetOrdinal("userName"));
                            UserLoginCache.ID_ROL = dtReader.GetInt32(dtReader.GetOrdinal("idRol"));
                            UserLoginCache.ID_DIVISION = dtReader.GetInt32(dtReader.GetOrdinal("idDivision"));
                        }
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        public List<Usuarios> verRegistros(string condicion)
        {
            using(var conecction = getConexionSql())
            {
                conecction.Open();

                using (var comando = new SqlCommand("ViewUsers", conecction) { CommandType = CommandType.StoredProcedure })
                {
                    comando.Connection = conecction;
                    comando.CommandText = "ViewUsers";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Condicion", condicion);

                    using(SqlDataReader dtReader = comando.ExecuteReader())
                    {
                        List<Usuarios> ListaUsuarios = new List<Usuarios>();

                        if (dtReader.HasRows)
                        {
                            while (dtReader.Read())
                            {
                                ListaUsuarios.Add(new Usuarios()
                                {
                                    Boleta = dtReader.GetInt64(0),
                                    Nombre = dtReader.GetString(1),
                                    Indicativo = dtReader.GetString(2),
                                    Apellido = dtReader.GetString(3),
                                    Escuela = dtReader.GetString(4),
                                    Carrera = dtReader.GetString(5),
                                    Especialidades = dtReader.GetString(6),
                                    Estatus = dtReader.GetString(7),
                                    Username = dtReader.GetString(8),
                                    Id_rol = dtReader.GetInt32(9),
                                    Id_division = dtReader.GetInt32(10),
                                    Rol = dtReader.GetString(12),
                                    Division = dtReader.GetString(13)
                                });
                            }
                        }
                        return ListaUsuarios;
                    }                 
                }
            }
        }

        public bool Agregar_Usuarios(Usuarios usuario)

        {
            using (var connection = getConexionSql())
            {
                connection.Open();

                using (var comando = new SqlCommand("Agregar_Usuarios", connection) { CommandType = CommandType.StoredProcedure })
                {
                    comando.Connection = connection;
                    comando.CommandText = "Agregar_Usuarios";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@boleta", SqlDbType.BigInt).Value = usuario.Boleta;
                    comando.Parameters.Add("@nombre", SqlDbType.VarChar, 80).Value = usuario.Nombre;
                    comando.Parameters.Add("@indicativo", SqlDbType.VarChar, 80).Value = usuario.Indicativo;
                    comando.Parameters.Add("@aPaterno", SqlDbType.VarChar, 80).Value = usuario.Apellido;
                    comando.Parameters.Add("@escuela", SqlDbType.VarChar, 80).Value = usuario.Escuela;
                    comando.Parameters.Add("@carrera", SqlDbType.VarChar, 80).Value = usuario.Carrera;
                    comando.Parameters.Add("@especialidades", SqlDbType.VarChar, 80).Value = usuario.Especialidades;
                    comando.Parameters.Add("@estatus", SqlDbType.VarChar, 10).Value = usuario.Estatus;
                    comando.Parameters.Add("@userName", SqlDbType.VarChar, 80).Value = usuario.Username;
                    comando.Parameters.Add("@idRol", SqlDbType.Int).Value = usuario.Id_rol;
                    comando.Parameters.Add("@idDivision", SqlDbType.Int).Value = usuario.Id_division;
                    comando.Parameters.Add("@clave", SqlDbType.VarChar, 80).Value = usuario.Clave;
                    comando.Parameters.Add("@patron", SqlDbType.VarChar, 80).Value = usuario.Patron;

                    try
                    {
                        comando.ExecuteNonQuery();

                        return true;
                    }catch(Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public bool Editar_Usuarios(Usuarios usuario)
        {

            using(var connection = getConexionSql())
            {
                connection.Open();

                using(var comando = new SqlCommand("UpdateUsers", connection) { CommandType = CommandType.StoredProcedure })
                {
                    comando.Connection = connection;
                    comando.CommandText = "UpdateUsers";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@boleta", SqlDbType.BigInt).Value = usuario.Boleta;
                    comando.Parameters.Add("@nombre", SqlDbType.VarChar, 80).Value = usuario.Nombre;
                    comando.Parameters.Add("@indicativo", SqlDbType.VarChar, 80).Value = usuario.Indicativo;
                    comando.Parameters.Add("@aPaterno", SqlDbType.VarChar, 80).Value = usuario.Apellido;
                    comando.Parameters.Add("@escuela", SqlDbType.VarChar, 80).Value = usuario.Escuela;
                    comando.Parameters.Add("@carrera", SqlDbType.VarChar, 80).Value = usuario.Carrera;
                    comando.Parameters.Add("@especialidades", SqlDbType.VarChar, 80).Value = usuario.Especialidades;
                    comando.Parameters.Add("@estatus", SqlDbType.VarChar, 10).Value = usuario.Estatus;
                    comando.Parameters.Add("@userName", SqlDbType.VarChar, 80).Value = usuario.Username;
                    comando.Parameters.Add("@idRol", SqlDbType.Int).Value = usuario.Id_rol;
                    comando.Parameters.Add("@idDivision", SqlDbType.Int).Value = usuario.Id_division;
                    comando.Parameters.Add("@clave", SqlDbType.VarChar, 80).Value = usuario.Clave;
                    comando.Parameters.Add("@patron", SqlDbType.VarChar, 80).Value = usuario.Patron;

                    try
                    {
                        comando.ExecuteNonQuery();
                        return true;

                    }catch(Exception ex)
                    {
                        return false;
                    }
                }
            }
        }
    }
}
