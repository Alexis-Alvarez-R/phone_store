using ModuloDatos;
using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloNegocio
{
    public class MN_Marca
    {
        private MD_Marca Objed_Marca = new MD_Marca();

        public List<Marca> Listar()
        {
            return Objed_Marca.Listar();
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la Descripcion de la Marca\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return Objed_Marca.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion de la Marca\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return Objed_Marca.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(Marca obj, out string Mensaje)
        {
            return Objed_Marca.Eliminar(obj, out Mensaje);
        }
    }
}
