using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

namespace ModuloDatos
{
    public class MD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> Lista = new List<Producto>();

            using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
            {
                try
                {
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("select Prod_Id, Prod_Cod, Prod_Name, Prod_Description, m.Id_Marca,m.Marca_Description,Stock,Purchase_Price,Sale_Price,Prod_State from Producto p");
                    Consulta.AppendLine("inner join Marca m on m.Id_Marca = p.Id_Marca");
                    //campos de la BD

                    SqlCommand cmd = new SqlCommand(Consulta.ToString(), OConexion);
                    cmd.CommandType = CommandType.Text;

                    OConexion.Open();

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {

                            Lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(Lector["Prod_Id"]),
                                Codigo = Lector["Prod_Cod"].ToString(),
                                NombreProducto = Lector["Prod_Name"].ToString(),
                                Descripcion = Lector["Prod_Description"].ToString(),
                                OMarca = new Marca() { IdMarca = Convert.ToInt32(Lector["Id_Marca"]), Descripcion = Lector["Marca_Description"].ToString() },
                                Stock = Convert.ToInt32(Lector["Stock"].ToString()),
                                PrecioCompra = Convert.ToDecimal(Lector["Purchase_Price"].ToString()),
                                PrecioVenta = Convert.ToDecimal(Lector["Sale_Price"].ToString()),
                                Estado = Convert.ToBoolean(Lector["Prod_State"])
      
                            });

                        }
                    }

                }
                catch (Exception excepcion)
                {
                    Lista = new List<Producto>();
                }

            }

            return Lista;

        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int IdProductoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_Registrarproducto", OConexion);
                    cmd.Parameters.AddWithValue("Prod_Cod", obj.Codigo);
                    cmd.Parameters.AddWithValue("Prod_Name", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("Prod_Description", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Id_Marca", obj.OMarca.IdMarca);
                    cmd.Parameters.AddWithValue("Prod_State", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    cmd.ExecuteNonQuery();

                    IdProductoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();




                }

            }
            catch (Exception excepcion)
            {
                IdProductoGenerado = 0;
                Mensaje = excepcion.Message;



            }

            return IdProductoGenerado;
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ModificarProducto", OConexion);
                    cmd.Parameters.AddWithValue("Prod_Id", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Prod_Cod", obj.Codigo);
                    cmd.Parameters.AddWithValue("Prod_Name", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("Prod_Description", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Id_Marca", obj.OMarca.IdMarca);
                    cmd.Parameters.AddWithValue("Prod_State", obj.Estado);
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

        public bool Eliminar(Producto obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection OConexion = new SqlConnection(Conexion.Cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EliminarProducto", OConexion);
                    cmd.Parameters.AddWithValue("Prod_Id", obj.IdProducto);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    OConexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
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
