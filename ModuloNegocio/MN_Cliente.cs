using ModuloDatos;
using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloNegocio
{
    public class MN_Cliente
    {
        private MD_Cliente Objed_Cliente = new MD_Cliente();

        public List<Cliente> Listar()
        {
            return Objed_Cliente.Listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.DocumentoCliente == "")
            {
                Mensaje += "Es necesario el documento del Cliente\n";
            }
            if (obj.NombreCliente == "")
            {
                Mensaje += "Es necesario el nombre completo del Cliente\n";
            }
            if (obj.Email == "")
            {
                Mensaje += "Es necesario el correo del Cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return Objed_Cliente.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.DocumentoCliente == "")
            {
                Mensaje += "Es necesario el documento del Cliente\n";
            }
            if (obj.NombreCliente == "")
            {
                Mensaje += "Es necesario el nombre completo del Cliente\n";
            }
            if (obj.Email == "")
            {
                Mensaje += "Es necesario el correo del Cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return Objed_Cliente.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return Objed_Cliente.Eliminar(obj, out Mensaje);
        }
    }
}
