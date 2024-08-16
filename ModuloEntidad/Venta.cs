using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloEntidad
{
    public class Venta
    {
        public int IdVenta {  get; set; }
        public Usuario OUsuario { get; set; }
        public string TipoDocumento { get; set; }
        public string NumDocumento { get; set; }
        public string ClientDocumento { get; set; }
        public  string NombreVenta {  get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoCambio {  get; set; }
        public decimal MontoTotal { get; set; }
        public List<DetalleVenta> ODetalleVentas { get; set; }
        public string FechaRegistro { get; set; }


    }
}
