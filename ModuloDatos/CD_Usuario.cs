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
                                IdUsuario = Convert.ToInt32(Lector["IdUsuario"]),
                                DocumentoUsuario = Lector["DocumentoUsuario"].ToString(),
                                NombreUsuario = Lector["NombreUsuario"].ToString(),
                                Email = Lector["Email"].ToString(),
                                Password = Lector["Password"].ToString(),
                                Estado = Convert.ToBoolean(Lector["Estado"])
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
    }
}
