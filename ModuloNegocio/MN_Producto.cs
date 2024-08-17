using ModuloDatos;
using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloNegocio
{
    public class MN_Producto
    {
        private MD_Producto Objed_Producto = new MD_Producto();

        public List<Producto> Listar()
        {
            return Objed_Producto.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el codigo del Producto\n";
            }
            if (obj.NombreProducto == "")
            {
                Mensaje += "Es necesario el nombre  del Producto\n";
            }
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la Descripcion  del Producto\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return Objed_Producto.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el codigo del Producto\n";
            }
            if (obj.NombreProducto == "")
            {
                Mensaje += "Es necesario el nombre  del Producto\n";
            }
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la Descripcion  del Producto\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return Objed_Producto.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(Producto obj, out string Mensaje)
        {
            return Objed_Producto.Eliminar(obj, out Mensaje);
        }
    }
}
