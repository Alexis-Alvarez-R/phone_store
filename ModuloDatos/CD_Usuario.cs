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
                    string Consulta = "select IdUsuario,DocumentoUsuario,NombreUsuario,Email,Password,Estado from Usuario";
                    SqlCommand cmd = new SqlCommand(Consulta, OConexion);
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
