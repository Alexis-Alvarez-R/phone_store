using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloEntidad
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public Usuario OUsuario { get; set; }
        public Proveedor OProveedor { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoTotal {  get; set; }
        public List<DetalleCompra> ODetalleCompra {  get; set; }
        public string FechaRegistro { get; set; }

    }
}
