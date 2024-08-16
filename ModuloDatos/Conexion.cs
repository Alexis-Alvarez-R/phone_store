using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//referencias agregadas
using System.Configuration;

namespace ModuloDatos
{
    public class Conexion
    {
        public static string Cadena = ConfigurationManager.ConnectionStrings["cadena_conexion"].ToString();

    }
}
