using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ModuloEntidad;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;


namespace ModuloDatos
{
    public class MD_Negocio
    {
        public Negocio ObtenerDatos()
        {
            Negocio obj = new Negocio();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.Cadena))
                {
                    conexion.Open();

                    string consulta = "select Id_Negocio, Nombre, RUC, Direccion from Negocio where Id_Negocio = 1";
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            obj = new Negocio()
                            {
                                IdNegocio = int.Parse(Lector["Id_Negocio"].ToString()),
                                NombreNegocio = Lector["Nombre"].ToString(),
                                RUC = Lector["RUC"].ToString(),
                                Direccion = Lector["Direccion"].ToString()

                            };

                        }

                    }
                }
            }
            catch
            {
                obj = new Negocio();
            }

            return obj;
        }

        public bool GuardarDatos(Negocio objeto, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.Cadena))
                {
                    conexion.Open();

                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("Update Negocio set Nombre = @Nombre,");
                    consulta.AppendLine("RUC = @RUC,");
                    consulta.AppendLine("Direccion = @Direccion,");
                    consulta.AppendLine("where Id_Negocio = 1;");

                    SqlCommand cmd = new SqlCommand(consulta.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@Nombre", objeto.NombreNegocio);
                    cmd.Parameters.AddWithValue("@RUC", objeto.RUC);
                    cmd.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                    cmd.CommandType = CommandType.Text;

                    if(cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo guardar los datos";
                        respuesta = false;
                    }


                }

            }
            catch (Exception excepcion)
            {
                mensaje = excepcion.Message;
                respuesta = false;

            }

            return respuesta;
        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            obtenido = true;
            byte[] LogoBytes = new byte[0];

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.Cadena))
                {
                    conexion.Open();
                    string consulta = "select Logo from Negocio where Id_Negocio = 1";

                    SqlCommand cmd = new SqlCommand(consulta, conexion);

                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader Lector = cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            LogoBytes = (byte[]) Lector["Logo"];

                        }

                    }


                }

            }
            catch (Exception excepcion)
            {
                obtenido = false;
                LogoBytes = new byte[0];

            }

            return LogoBytes;
        }

        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.Cadena))
                {
                    conexion.Open();

                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("Update Negocio set Logo = @imagen");
                    consulta.AppendLine("where Id_Negocio = 1;");

                    SqlCommand cmd = new SqlCommand(consulta.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@imagen", image);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";
                        respuesta = false;
                    }


                }

            }
            catch (Exception excepcion)
            {
                mensaje = excepcion.Message;
                respuesta = false;

            }

            return respuesta;

        }

        
    }

}
