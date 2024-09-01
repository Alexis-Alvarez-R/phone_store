using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloEntidad;
using System.Reflection;

namespace ModuloDatos
{
    public class MD_Ventas
    {
        public int ObtenerCorrelativo()
        {
            int IdCorrelativo = 0;

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("select count(*) + 1 from Venta");
                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    IdCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception excepcion)
                {
                    IdCorrelativo = 0;
                }

            }

            return IdCorrelativo;
        }

        public bool RestarStock(int IdProducto, int Cantidad)
        {
            bool Respuesta = true;

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("update Producto set Stock = Stock - @Cantidad where Prod_Id = @IdProducto");
                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@IdProducto", IdProducto);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    Respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception excepcion)
                {
                    Respuesta = false;
                }

            }

            return Respuesta;
        }

        public bool SumarStock(int IdProducto, int Cantidad)
        {
            bool Respuesta = true;

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("update Producto set Stock = Stock + @Cantidad where Prod_Id = @IdProducto");
                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@IdProducto", IdProducto);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    Respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception excepcion)
                {
                    Respuesta = false;
                }

            }

            return Respuesta;
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVenta", OConexion);
                    cmd.Parameters.AddWithValue("Id_User", obj.OUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("Doc_Type", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("Doc_Num", obj.NumDocumento);
                    cmd.Parameters.AddWithValue("Client_Doc", obj.ClientDocumento);
                    cmd.Parameters.AddWithValue("Client_Name", obj.NombreCliente);
                    cmd.Parameters.AddWithValue("Pay_Amount", obj.MontoPago);
                    cmd.Parameters.AddWithValue("Change_Amount", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("Total_Amount", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
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

        public Venta ObtenerVenta(string numero)
        {
            Venta obj = new Venta();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    OConexion.Open();
                    StringBuilder Consulta = new StringBuilder();

                    Consulta.AppendLine("select v.Sale_Id, u.User_FullName,");
                    Consulta.AppendLine("v.Client_Doc, v.Client_Name,");
                    Consulta.AppendLine("v.Doc_Type, v.Doc_Num,");
                    Consulta.AppendLine("v.Pay_Amount, v.Change_Amount, v.Total_Amount,");
                    Consulta.AppendLine("convert(char(10), v.Reg_Date, 103)[Reg_Date]");
                    Consulta.AppendLine("from Venta v");
                    Consulta.AppendLine("inner join Usuario u on u.Id_User = v.Id_User");
                    Consulta.AppendLine("where v.Doc_Num = @numero");
                    //campos de la BD


                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            obj = new Venta()
                            {
                                IdVenta= Convert.ToInt32(Lector["Sale_Id"]),
                                OUsuario = new Usuario() { NombreUsuario = Lector["User_FullName"].ToString() },
                                ClientDocumento = Lector["Client_Doc"].ToString(),
                                NombreCliente = Lector["Client_Name"].ToString(),
                                TipoDocumento = Lector["Doc_Type"].ToString(),
                                NumDocumento = Lector["Doc_Num"].ToString(),
                                MontoPago = Convert.ToDecimal(Lector["Pay_Amount"].ToString()),
                                MontoCambio = Convert.ToDecimal(Lector["Change_Amount"].ToString()),
                                MontoTotal = Convert.ToDecimal(Lector["Total_Amount"].ToString()),
                                FechaRegistro = Lector["Reg_Date"].ToString()
                                //campos de la BD
                            };

                        }
                    }

                }
                catch
                {
                    obj = new Venta();
                }
            }

            return obj;
        }

        public List<DetalleVenta> ObtenerDetalleVenta(int IdVenta)
        {
            List<DetalleVenta> OLista = new List<DetalleVenta>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {
                try
                {
                    OConexion.Open();
                    StringBuilder Consulta = new StringBuilder();


                    Consulta.AppendLine("select p.Prod_Name, dv.Sale_Price, dv.Quantity, dv.SubTotal");
                    Consulta.AppendLine("from Det_Venta dv");
                    Consulta.AppendLine("inner join Producto p on p.Prod_Id = dv.Prod_Id");
                    Consulta.AppendLine("where dv.Sale_Id = @IdVenta");
                    //campos de la BD

                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                    cmd.CommandType = System.Data.CommandType.Text;


                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            OLista.Add(new DetalleVenta()
                            {
                                OProducto = new Producto() { NombreProducto = Lector["Prod_Name"].ToString() },
                                PrecioVenta = Convert.ToDecimal(Lector["Sale_Price"].ToString()),
                                Cantidad = Convert.ToInt32(Lector["Quantity"].ToString()),
                                SubTotal = Convert.ToDecimal(Lector["SubTotal"].ToString()),

                            });

                        }
                    }

                }
                catch
                {
                    OLista = new List<DetalleVenta>();
                }
            }

            return OLista;
        }

    }
}
