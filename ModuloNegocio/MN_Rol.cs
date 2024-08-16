using ModuloDatos;
using ModuloEntidad;
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
    public class MN_Rol
    {
        private MD_Rol objed_Rol = new MD_Rol();

        public List<Rol> Listar()
        {
            return objed_Rol.Listar();
        }
    }
}
