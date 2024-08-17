using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//referencias agregadas
using System.Data;
using System.Data.SqlClient;
using ModuloEntidad;

namespace ModuloDatos
{
    public class CD_Usuario
    {
        //metodo que lista todos los usuarios de la BD 
        public List<Usuario> Listar()
        {
            List<Usuario> Lista = new List<Usuario>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("select u.Id_User, u.document,u.User_FullName,u.Gmail,u.Pssword,u.User_State,r.Role_Id,r.Rol_Desc from Usuario u");
                    Consulta.AppendLine("inner join Rol r on r.Role_Id = u.Role_Id");
                    //campos de la BD
                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            Lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(Lector["Id_User"]),
                                DocumentoUsuario = Lector["document"].ToString(),
                                NombreUsuario = Lector["User_FullName"].ToString(),
                                Email = Lector["Gmail"].ToString(),
                                Password = Lector["Pssword"].ToString(),
                                Estado = Convert.ToBoolean(Lector["User_State"])
                                ORol = new Rol() { IdRol = Convert.ToInt32(Lector["Role_Id"]),Descripcion = Lector["Rol_Desc"].ToString() }


                            });

                        }
                    }

                }
                catch (Exception excepcion)
                {
                    Lista = new List<Usuario>();
                }

            }

            return Lista;
        }
        
    public int Registrar(Usuario obj, out string Mensaje) {
        int IdUsuarioGenerado =0 ;
        Mensaje = string.Empty;

        try
        {
            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena)){

                SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", OConexion);
                cmd.Parameters.AddWithValue("Document", obj.DocumentoUsuario);
                cmd.Parameters.AddWithValue("User_FullName", obj.NombreUsuario);
                cmd.Parameters.AddWithValue("Gmail", obj.Email);
                cmd.Parameters.AddWithValue("Pssword", obj.Password);
                cmd.Parameters.AddWithValue("Role_Id", obj.ORol.IdRol);
                cmd.Parameters.AddWithValue("User_State", obj.Estado);
                cmd.Parameters.Add("IdUsusarioResultado",SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

            OConexion.Open();

            cmd.ExecuteNonQuery();

            IdUsuarioGenerado = Convert.ToInt32(cmd.Parameters["IdUsusarioResultado"].Value);
            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

        }

    }
    catch (Exception excepcion) {
        IdUsuarioGenerado = 0;
        Mensaje = excepcion.Message;
        


    }

    return IdUsuarioGenerado;
}

public bool Editar(Usuario obj, out string Mensaje)
{
    bool Respuesta = false;
    Mensaje = string.Empty;

    try
    {
        using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
        {

            SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", OConexion);
            cmd.Parameters.AddWithValue("Id_User", obj.IdUsuario);
            cmd.Parameters.AddWithValue("Document", obj.DocumentoUsuario);
            cmd.Parameters.AddWithValue("User_FullName", obj.NombreUsuario); // Son los campos del procedimiento almacenado
            cmd.Parameters.AddWithValue("Gmail", obj.Email);
            cmd.Parameters.AddWithValue("Pssword", obj.Password);
            cmd.Parameters.AddWithValue("Role_Id", obj.ORol.IdRol);
            cmd.Parameters.AddWithValue("User_State", obj.Estado);
            cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
            cmd.CommandType = CommandType.StoredProcedure;

            OConexion.Open();

            cmd.ExecuteNonQuery();

            Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

        }

    }
    catch (Exception excepcion)
    {
        Respuesta = false;
        Mensaje = excepcion.Message;



    }

    return Respuesta;
}

public bool Eliminar(Usuario obj, out string Mensaje)
{
    bool Respuesta = false;
    Mensaje = string.Empty;

    try
    {
        using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
        {

            SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", OConexion);
            cmd.Parameters.AddWithValue("Id_User", obj.IdUsuario);
            cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
            cmd.CommandType = CommandType.StoredProcedure;

            OConexion.Open();

            cmd.ExecuteNonQuery();

            Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

        }

    }
    catch (Exception excepcion)
    {
        Respuesta = false;
        Mensaje = excepcion.Message;

    }

    return Respuesta;
}

        
    }
}
