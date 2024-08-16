using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloEntidad
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Email { get; set; }
        public string NumeroTelefono { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

    }
}
