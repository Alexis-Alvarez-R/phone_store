using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloDatos
{
    public class MD_Marca
    {
        //metodo que lista las marcas de la BD 
        public List<Marca> Listar()
        {
            List<Marca> Lista = new List<Marca>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("select Id_Marca, Marca_Description, Marca_State from Marca");
                    //campos de la BD

                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            Lista.Add(new Marca()
                            {
                                IdMarca = Convert.ToInt32(Lector["Id_Marca"]),
                                Descripcion = Lector["Marca_Description"].ToString(),
                                Estado = Convert.ToBoolean(Lector["Marca_State"]),
                             
                            });

                        }
                    }

                }
                catch (Exception excepcion)
                {
                    Lista = new List<Marca>();
                }

            }

            return Lista;

        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            int IdMarcaGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_RegistrarMarca", OConexion);
                    cmd.Parameters.AddWithValue("Marca_Description", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Marca_State", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    cmd.ExecuteNonQuery();

                    IdMarcaGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();




                }

            }
            catch (Exception excepcion)
            {
                IdMarcaGenerado = 0;
                Mensaje = excepcion.Message;



            }

            return IdMarcaGenerado;
        }

        public bool Editar(Marca obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EditarMarca", OConexion);
                    cmd.Parameters.AddWithValue("Id_Marca", obj.IdMarca);
                    cmd.Parameters.AddWithValue("Marca_Description", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Marca_State", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
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

        public bool Eliminar(Marca obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EliminarMarca", OConexion);
                    cmd.Parameters.AddWithValue("Id_Marca", obj.IdMarca);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
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
