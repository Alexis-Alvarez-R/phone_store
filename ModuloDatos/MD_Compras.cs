using ModuloEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloDatos
{
    public class MD_Compras
    {
        public int ObtenerCorrelativo()
        {
            int IdCorrelativo = 0;

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("select count(*) + 1 from Compra");
                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    IdCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception excepcion)
                {
                    IdCorrelativo = 0;
                }


                return IdCorrelativo;
            }
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCompra", OConexion);
                    cmd.Parameters.AddWithValue("Id_User", obj.OUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("Prov_ID", obj.OProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("Doc_Type", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("Doc_Num", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("total", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleCompra", DetalleCompra);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    cmd.ExecuteNonQuery();

                    

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception excepcion)
                {
                    Respuesta = false;
                    Mensaje = excepcion.Message;
                }


            }

            return Respuesta;
        }

    }
}
