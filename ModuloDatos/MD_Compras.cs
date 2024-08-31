using ModuloEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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

        public Compra ObtenerCompra(string numero)
        {
            Compra obj = new Compra();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {

                try
                {
                    StringBuilder Consulta = new StringBuilder();

                    Consulta.AppendLine("select c.Purchase_Id,");
                    Consulta.AppendLine("u.User_FullName,");
                    Consulta.AppendLine("pr.Documen, pr.Prov_Name,");
                    Consulta.AppendLine("c.Doc_Type, c.Doc_Num, c.Total, convert(char(10), c.Reg_Date, 103)[Reg_Date]");
                    Consulta.AppendLine("from Compra c");
                    Consulta.AppendLine("inner join Usuario u on u.Id_User = c.Id_User");
                    Consulta.AppendLine("inner join Proveedores pr on pr.Prov_Id = c.Prov_ID");
                    Consulta.AppendLine("where c.Doc_Num = '@numero'");
                    //campos de la BD


                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            obj = new Compra()
                            {
                                IdCompra = Convert.ToInt32(Lector["Purchase_Id"]),
                                OUsuario = new Usuario() { NombreUsuario = Lector["User_FullName"].ToString() },
                                OProveedor = new Proveedor() { Documento = Lector["Documen"].ToString(), NombreProveedor = Lector["Prov_Name"].ToString() },
                                TipoDocumento = Lector["Doc_Type"].ToString(),
                                NumeroDocumento = Lector["Doc_Num"].ToString(),
                                MontoTotal = Convert.ToDecimal(Lector["Total"].ToString()),
                                FechaRegistro = Lector["Reg_Date"].ToString()

                            };

                        }
                    }

                }
                catch (Exception excepcion)
                {
                    obj = new Compra();
                }

                
            }

            return obj;

        }

        public List<DetalleCompra> ObtenerDetalleCompra(int IdCompra)
        {
            List<DetalleCompra> OLista = new List<DetalleCompra>();

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {
                    OConexion.Open();
                    StringBuilder Consulta = new StringBuilder();


                    Consulta.AppendLine("select p.Prod_Name, dc.Purchase_Price, dc.Stock, dc.Total from Det_Compra dc");
                    Consulta.AppendLine("inner join Producto p on p.Prod_Id = dc.Prod_Id");
                    Consulta.AppendLine("where dc.Purchase_Id = @IdCompra");
                    //campos de la BD

                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.Parameters.AddWithValue("@IdCompra", IdCompra);
                    cmd.CommandType = System.Data.CommandType.Text;


                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            OLista.Add(new DetalleCompra()
                            {
                                OProducto = new Producto() { NombreProducto = Lector["Prod_Name"].ToString() },
                                PrecioCompra= Convert.ToDecimal(Lector["Purchase_Price"].ToString()),
                                Cantidad = Convert.ToInt32(Lector["Stock"].ToString()),
                                MontoTotal = Convert.ToDecimal(Lector["Total"].ToString()),

                            });

                        }
                    }

                }
            }
            catch (Exception excepcion)
            {
                OLista = new List<DetalleCompra>();
            }

            return OLista;
        }
    }
}
