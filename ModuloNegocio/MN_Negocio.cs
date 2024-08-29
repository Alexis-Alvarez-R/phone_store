using ModuloDatos;
using ModuloEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloNegocio
{
    public class MN_Negocio
    {
        private MD_Negocio Objed_negocio = new MD_Negocio();

        public Negocio ObtenerDatos()
        {
            return Objed_negocio.ObtenerDatos();
        }

        public bool GuardarDatos(Negocio obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.NombreNegocio == "")
            {
                Mensaje += "Es Necesario el Nombre del Negocio\n";
            }
            if (obj.RUC == "")
            {
                Mensaje += "Es Necesario el RUC del Negocio\n";
            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es Necesario la Direccion del Negocio\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return Objed_negocio.GuardarDatos(obj, out Mensaje);
            }

        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            return Objed_negocio.ObtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            return Objed_negocio.ActualizarLogo(imagen, out mensaje);
        }

       

      }

 }
