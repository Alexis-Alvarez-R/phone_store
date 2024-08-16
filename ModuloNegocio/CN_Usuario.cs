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
    }
}
