using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//referencias agregadas
using System.Data;
using System.Data.SqlClient;
using ModuloEntidad;

namespace ModuloDatos
{
    public class MD_Permiso
    {
        //metodo que lista todos los usuarios de la BD 
        public List<Permiso> Listar(int IdUsuario)
        {
            List<Permiso> Lista = new List<Permiso>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select p.Rol_Id, p.Menu_Name from Permiso p");
                    consulta.AppendLine("inner join Rol r on r.Role_Id = p.Rol_Id");
                    consulta.AppendLine("inner join Usuario u on u.Role_Id = r.Role_Id");
                    consulta.AppendLine("where u.Id_User = @IdUsuario");

                    
                    SqlCommand cmd = new SqlCommand(consulta.ToString(), OConexion);
                    cmd.Parameters.AddWithValue("@IdUsuario",IdUsuario);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            Lista.Add(new Permiso()
                            {
                                ORol = new Rol(){ IdRol = Convert.ToInt32(Lector["Rol_Id"]) },
                                NombreMenu = Lector["Menu_Name"].ToString(),

                            });

                        }
                    }

                }
                catch (Exception excepcion)
                {
                    Lista = new List<Permiso>();
                }

            }

            return Lista;

        }
    }
}

