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
    public class MD_Rol
    {
        public List<Rol> Listar()
        {
            List<Rol> Lista = new List<Rol>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select Rol_Id,Rol_Desc from Rol");//son los campos de la BD
                
                    SqlCommand cmd = new SqlCommand(consulta.ToString(), OConexion);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            Lista.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(Lector["Rol_Id"]),
                                Descripcion = Lector["Rol_Desc"].ToString(), //son los campos de la BD

                            });

                        }
                    }

                }
                catch (Exception excepcion)
                {
                    Lista = new List<Rol>();
                }

            }

            return Lista;

        }
    }
}

