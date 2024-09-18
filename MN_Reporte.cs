using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloDatos;
using ModuloEntidad;

namespace ModuloNegocio
{
    public class MN_Reporte
    {
        private MD_Reporte Objed_Reporte = new MD_Reporte();

        public List<ReporteCompra> Compra(string fechainicio, string fechafinal, int idproveedor)
        {
            return Objed_Reporte.Compra(fechainicio, fechafinal, idproveedor);
        }

        public List<ReporteVentas> Venta(string fechainicio, string fechafinal, out List<ReporteVentas> topProductos)
        {
          
            return Objed_Reporte.Venta(fechainicio, fechafinal, out topProductos);
        }
    }
}