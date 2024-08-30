using ModuloDatos;
using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloNegocio
{
    public class MN_Compras
    {
        private MD_Compras Objed_Compras = new MD_Compras();

        public int ObtenerCorrelativo()
        {
            return Objed_Compras.ObtenerCorrelativo();
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            return Objed_Compras.Registrar(obj,DetalleCompra, out Mensaje);
        }

    }
}
