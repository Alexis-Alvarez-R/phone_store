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

        public List<ReporteVentas> Venta(string fechainicio, string fechafinal, out List<ReporteVentas> topProductos)
        {
            List<ReporteVentas> Lista = new List<ReporteVentas>();
            topProductos = new List<ReporteVentas>(); // Inicializar la lista del top 5

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteVenta", OConexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafinal", fechafinal);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        // Leer primer conjunto de resultados (reporte de ventas)
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
                                TotalVendido = null // Para el reporte de ventas regular no se asigna TotalVendido
                            });
                        }

                        // Avanzar al siguiente conjunto de resultados (top 5 productos más vendidos)
                        if (Lector.NextResult())
                        {
                            while (Lector.Read())
                            {
                                topProductos.Add(new ReporteVentas()
                                {
                                    CodigoProducto = Lector["Codigo_Prod"].ToString(),
                                    NombreProducto = Lector["Nombre_Prod"].ToString(),
                                    TotalVendido = Convert.ToInt32(Lector["Total_Vendido"]), // Este campo es específico para el top 5
                                                                                             // Los demás campos se dejan vacíos o nulos porque no son relevantes para el top
                                    FechaRegistro = null,
                                    TipoDocumento = null,
                                    NumeroDocumento = null,
                                    MontoTotal = null,
                                    UsuarioRegistro = null,
                                    DocumentoCliente = null,
                                    NombreCliente = null,
                                    Marca = null,
                                    PrecioVenta = null,
                                    Cantidad = null,
                                    SubTotal = null
                                });
                            }
                        }
                    }
                }
                catch (Exception excepcion)
                {
                    Lista = new List<ReporteVentas>();
                    topProductos = new List<ReporteVentas>(); 
                }
            }

            return Lista;
        }



    }
}