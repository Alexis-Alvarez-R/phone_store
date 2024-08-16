using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloEntidad
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Documento {  get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public string NumeroTelefono {  get; set; }
        public bool Estado {  get; set; }
        public string FechaRegistro {  get; set; }



    }
}
