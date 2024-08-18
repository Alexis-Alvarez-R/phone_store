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
    public class MD_Cliente
    {
        //metodo que lista todos los Clientes de la BD 
        public List<Cliente> Listar()
        {
            List<Cliente> Lista = new List<Cliente>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("select Client_Id, Document, Client_fullname, Gmail, Telephone, Client_State from Cliente");
                    //campos de la BD

                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            Lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(Lector["Client_Id"]),
                                DocumentoCliente = Lector["Document"].ToString(),
                                NombreCliente = Lector["Client_FullName"].ToString(),
                                Email = Lector["Gmail"].ToString(),
                                NumeroTelefono = Lector["Telephone"].ToString(),
                                Estado = Convert.ToBoolean(Lector["Client_State"]),


                            });

                        }
                    }

                }
                catch (Exception excepcion)
                {
                    Lista = new List<Cliente>();
                }

            }

            return Lista;

        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            int IdClienteGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", OConexion);
                    cmd.Parameters.AddWithValue("Document", obj.DocumentoCliente);
                    cmd.Parameters.AddWithValue("Client_FullName", obj.NombreCliente);
                    cmd.Parameters.AddWithValue("Gmail", obj.Email);
                    cmd.Parameters.AddWithValue("Telephone", obj.NumeroTelefono);
                    cmd.Parameters.AddWithValue("Client_State", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    cmd.ExecuteNonQuery();

                    IdClienteGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();




                }

            }
            catch (Exception excepcion)
            {
                IdClienteGenerado = 0;
                Mensaje = excepcion.Message;



            }

            return IdClienteGenerado;
        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_NodificarCliente", OConexion);
                    cmd.Parameters.AddWithValue("Client_Id", obj.IdCliente);
                    cmd.Parameters.AddWithValue("Document", obj.DocumentoCliente);
                    cmd.Parameters.AddWithValue("Client_FullName", obj.NombreCliente);
                    cmd.Parameters.AddWithValue("Gmail", obj.Email);
                    cmd.Parameters.AddWithValue("Telephone", obj.NumeroTelefono);
                    cmd.Parameters.AddWithValue("Client_State", obj.Estado);
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

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {
                    SqlCommand cmd = new SqlCommand("delete from Cliente where Client_Id = @Client_Id",OConexion);
                    cmd.Parameters.AddWithValue("@Id_Cliente",obj.IdCliente);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();
                    Respuesta =  cmd.ExecuteNonQuery() > 0 ? true : false;

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
