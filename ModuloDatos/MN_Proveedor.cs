using ModuloDatos;
using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloNegocio
{
    public class MN_Proveedor
    {
        private MD_Proveedor Objed_Proveedor = new MD_Proveedor();

        public List<Proveedor> Listar()
        {
            return Objed_Proveedor.Listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }
            if (obj.NombreProveedor == "")
            {
                Mensaje += "Es necesario el nombre completo del Proveedor\n";
            }
            if (obj.Email == "")
            {
                Mensaje += "Es necesario el email del Proveedor\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return Objed_Proveedor.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }
            if (obj.NombreProveedor == "")
            {
                Mensaje += "Es necesario el nombre completo del Proveedor\n";
            }
            if (obj.Email == "")
            {
                Mensaje += "Es necesario el email del Proveedor\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return Objed_Proveedor.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return Objed_Proveedor.Eliminar(obj, out Mensaje);
        }
    }
}

