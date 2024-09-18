using ModuloEntidad;
using ModuloNegocio;
using ModuloPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModuloPresentacion
{
    public partial class FormReportesVentas : Form
    {
        public FormReportesVentas()
        {
            InitializeComponent();
        }

        private void FormReportesVentas_Load(object sender, EventArgs e)
        {
            //foreach (DataGridViewColumn columna in dgvdata.Columns)
            //{
            //    cmbBusqueda.Items.Add(new OpcionCombo() { valor = columna.Name, Texto = columna.HeaderText });
            //    MessageBox.Show(columna.Name);
            //}
            //cmbBusqueda.DisplayMember = "Texto";
            //cmbBusqueda.ValueMember = "Valor";
            //cmbBusqueda.SelectedIndex = 0;
            //No sirve

        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            List<ReporteVentas> lista = new List<ReporteVentas>();

            // Crear una lista para el top 5 de productos más vendidos
            List<ReporteVentas> topProductos = new List<ReporteVentas>();

            lista = new MN_Reporte().Venta(fechaInicio.Value.ToString(), fechaFin.Value.ToString(), out topProductos);

            dgvdata.Rows.Clear();

            foreach (ReporteVentas rv in lista)
            {
                dgvdata.Rows.Add(new object[]
                {
                    rv.FechaRegistro,
                    rv.TipoDocumento,
                    rv.NumeroDocumento,
                    rv.MontoTotal,
                    rv.UsuarioRegistro,
                    rv.DocumentoCliente,
                    rv.NombreCliente,
                    rv.CodigoProducto,
                    rv.NombreProducto,
                    rv.Marca,
                    rv.PrecioVenta,
                    rv.Cantidad,
                    rv.SubTotal
                });
            }

            //mostrar también el top 5 de productos más vendidos, puedes procesar 'topProductos'

            foreach (ReporteVentas topProducto in topProductos)
            {
                Console.WriteLine($"Top Producto: {topProducto.NombreProducto}, Total Vendido: {topProducto.TotalVendido}");
            }
        }

        private void btnBucar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = "";
            //string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).valor.ToString();
            if(cmbBusqueda.SelectedIndex == 0)
            {
                columnaFiltro = "fechaRegistro";
            }
            if (cmbBusqueda.SelectedIndex == 1)
            {
                columnaFiltro = "tipoDoc";
            }
            if (cmbBusqueda.SelectedIndex == 2)
            {
                columnaFiltro = "numeroDoc";
            }
            if (cmbBusqueda.SelectedIndex == 3)
            {
                columnaFiltro = "montoTotal";
            }
            if (cmbBusqueda.SelectedIndex == 4)
            {
                columnaFiltro = "usuarioRegistro";
            }
            if (cmbBusqueda.SelectedIndex == 5)
            {
                columnaFiltro = "docCliente";
            }
            if (cmbBusqueda.SelectedIndex == 6)
            {
                columnaFiltro = "nombreCliente";
            }
            if (cmbBusqueda.SelectedIndex == 7)
            {
                columnaFiltro = "codigoProducto";
            }
            if (cmbBusqueda.SelectedIndex == 8)
            {
                columnaFiltro = "nombreProducto";
            }
            if (cmbBusqueda.SelectedIndex == 9)
            {
                columnaFiltro = "marca";
            }
            if (cmbBusqueda.SelectedIndex == 10)
            {
                columnaFiltro = "precioVenta";
            }
            if (cmbBusqueda.SelectedIndex == 11)
            {
                columnaFiltro = "cantidad";
            }
            if (cmbBusqueda.SelectedIndex == 12)
            {
                columnaFiltro = "subTotal";
            }

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;

                }
            }
        }

        private void btnLimpiarBuscador_Click_1(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
