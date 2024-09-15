using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModuloDatos
{
    public class MD_Reporte
    {
        public List<ReporteCompra> Compra(string fechainicio, string fechafinal, int idproveedor)
        {
            List<ReporteCompra> Lista = new List<ReporteCompra>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {
                try
                {

                    StringBuilder consulta = new StringBuilder();

                    SqlCommand cmd = new SqlCommand("sp_ReporteCompra", OConexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafinal", fechafinal);
                    cmd.Parameters.AddWithValue("idproveedor", idproveedor);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Lista.Add(new ReporteCompra()
                            {
                                FechaRegistro = Lector["Reg_Date"].ToString(),
                                TipoDocumento = Lector["Doc_Type"].ToString(),
                                NumeroDocumento = Lector["Doc_Num"].ToString(),
                                MontoTotal = Lector["Total"].ToString(),
                                UsuarioRegistro = Lector["Usuario_Registro"].ToString(),
                                DocumentoProveedor = Lector["Documento_Prov"].ToString(),
                                NombreProveedor = Lector["Prov_Name"].ToString(),
                                CodigoProducto = Lector["Codigo_Prod"].ToString(),
                                NombreProducto = Lector["Nombre_Prod"].ToString(),
                                Marca = Lector["Marca"].ToString(),
                                PrecioCompra = Lector["Purchase_Price"].ToString(),
                                PrecioVenta = Lector["Sale_Price"].ToString(),
                                Cantidad = Lector["Stock"].ToString(),
                                SubTotal = Lector["Sub_Total"].ToString(),



                            });
                        }
                    }
                }

                catch (Exception excepcion)
                {
                    Lista = new List<ReporteCompra>();
                }


            }

            return Lista;
        }

        public List<ReporteVentas> Venta(string fechainicio, string fechafinal)
        {
            List<ReporteVentas> Lista = new List<ReporteVentas>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {
                try
                {

                    StringBuilder consulta = new StringBuilder();

                    SqlCommand cmd = new SqlCommand("sp_ReporteVenta", OConexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafinal", fechafinal);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Lista.Add(new ReporteVentas()
                            {
                                FechaRegistro = Lector["Reg_Date"].ToString(),
                                TipoDocumento = Lector["Doc_Type"].ToString(),
                                NumeroDocumento = Lector["Doc_Num"].ToString(),
                                MontoTotal = Lector["Total_Amount"].ToString(),
                                UsuarioRegistro = Lector["Usuario_Registro"].ToString(),
                                DocumentoCliente = Lector["Client_Doc"].ToString(),
                                NombreCliente = Lector["Client_Name"].ToString(),
                                CodigoProducto = Lector["Codigo_Prod"].ToString(),
                                NombreProducto = Lector["Nombre_Prod"].ToString(),
                                Marca = Lector["Marca"].ToString(),
                                PrecioVenta = Lector["Sale_Price"].ToString(),
                                Cantidad = Lector["Quantity"].ToString(),
                                SubTotal = Lector["SubTotal"].ToString(),



                            });
                        }
                    }
                }

                catch (Exception excepcion)
                {
                    Lista = new List<ReporteVentas>();
                }


            }

            return Lista;
        }
    }
}
