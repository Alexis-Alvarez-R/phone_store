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
    public class MN_Permiso
    {
        private MD_Permiso objed_Permiso = new MD_Permiso();

        public List<Permiso> Listar(int IdUsuario)
        {
            return objed_Permiso.Listar(IdUsuario);
        }
    }
}
