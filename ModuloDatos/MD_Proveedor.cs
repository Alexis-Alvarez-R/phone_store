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
    public class MD_Proveedor
    {
        //metodo que lista todos los proveedores de la BD 
        public List<Proveedor> Listar()
        {
            List<Proveedor> Lista = new List<Proveedor>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("select Prov_Id, Documen, Prov_Name, Gmail, Telephone, Prov_State from Proveedores");
                    //campos de la BD
                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            Lista.Add(new Proveedor()
                            {
                                IdProveedor = Convert.ToInt32(Lector["Prov_Id"]),
                                Documento = Lector["Documen"].ToString(),
                                NombreProveedor = Lector["Prov_Name"].ToString(),
                                Email = Lector["Gmail"].ToString(),
                                NumeroTelefono = Lector["Telephone"].ToString(),
                                Estado = Convert.ToBoolean(Lector["Prov_State"]),


                            });

                        }
                    }

                }
                catch (Exception excepcion)
                {
                    Lista = new List<Proveedor>();
                }

            }

            return Lista;
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            int IdProveedorGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_RegistrarProveedor", OConexion);
                    cmd.Parameters.AddWithValue("Documen", obj.Documento);
                    cmd.Parameters.AddWithValue("Prov_Name", obj.NombreProveedor);
                    cmd.Parameters.AddWithValue("Gmail", obj.Email);
                    cmd.Parameters.AddWithValue("Telephone", obj.NumeroTelefono);
                    cmd.Parameters.AddWithValue("Prov_State", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    cmd.ExecuteNonQuery();

                    IdProveedorGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception excepcion)
            {
                IdProveedorGenerado = 0;
                Mensaje = excepcion.Message;



            }

            return IdProveedorGenerado;
        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_ModificarProveedor", OConexion);
                    cmd.Parameters.AddWithValue("Prov_Id", obj.IdProveedor);
                    cmd.Parameters.AddWithValue("Documen", obj.Documento);
                    cmd.Parameters.AddWithValue("Prov_Name", obj.NombreProveedor);
                    cmd.Parameters.AddWithValue("Gmail", obj.Email);
                    cmd.Parameters.AddWithValue("Telephone", obj.NumeroTelefono);
                    cmd.Parameters.AddWithValue("Prov_State", obj.Estado);
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

        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_EliminarProveedorr", OConexion);
                    cmd.Parameters.AddWithValue("Prov_Id", obj.IdProveedor);
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
