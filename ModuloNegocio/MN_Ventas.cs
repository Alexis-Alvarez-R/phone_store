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
    public class MN_Ventas
    {
        private MD_Ventas Objed_Ventas = new MD_Ventas();

        public bool RestarStock(int IdProducto, int Cantidad)
        {
            return Objed_Ventas.RestarStock(IdProducto, Cantidad);
        }
        public bool SumarStock(int IdProducto, int Cantidad)
        {
            return Objed_Ventas.SumarStock(IdProducto, Cantidad);   
        }
        public int ObtenerCorrelativo()
        {
            return Objed_Ventas.ObtenerCorrelativo();
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return Objed_Ventas.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public Venta ObtenerVenta(string numero)
        {
            Venta OVenta = Objed_Ventas.ObtenerVenta(numero);

            if (OVenta.IdVenta != 0)
            {
                List<DetalleVenta> ODetalleVenta = Objed_Ventas.ObtenerDetalleVenta(OVenta.IdVenta);

                OVenta.ODetalleVentas = ODetalleVenta;
            }

            return OVenta;
        }

    }
}
