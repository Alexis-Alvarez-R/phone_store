using ModuloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//referencias agregadas
using ModuloDatos;
using ModuloEntidad;

namespace ModuloNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objed_Usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objed_Usuario.Listar();
        }
        
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(obj.DocumentoUsuario == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }
            if(obj.NombreUsuario == "")
            {
                Mensaje += "Es necesario el nombre completo del usuario\n";
            }
            if(obj.Password == "")
            {
                Mensaje += "Es necesario la clave del usuario\n";
            }
            if(Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return Objed_Usuario.Registrar(obj, out Mensaje);
            }
            
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.DocumentoUsuario == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }
            if (obj.NombreUsuario == "")
            {
                Mensaje += "Es necesario el nombre completo del usuario\n";
            }
            if (obj.Password == "")
            {
                Mensaje += "Es necesario la clave del usuario\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return Objed_Usuario.Editar(obj, out Mensaje);
            }
            
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return Objed_Usuario.Eliminar(obj, out Mensaje);
        }
    }
}

    }
}
