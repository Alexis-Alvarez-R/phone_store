using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloEntidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; } 
        public string DocumentoUsuario {  get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Rol ORol { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro {  get; set; }

    }
}
