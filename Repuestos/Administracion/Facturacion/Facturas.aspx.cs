using Capa_Negocio.Administracion.Facturacion;
using Capa_Objetos.General;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repuestos.Administracion.Facturacion
{
    public partial class Facturas : System.Web.UI.Page
    {
        CO_Respuesta objRespuesta = new CO_Respuesta();
        CN_Facturacion obj_Negocio_Facturacion = new CN_Facturacion();

        #region Funciones del formulario


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvCompras();
            }
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvFacturas.Rows[index];

                int id_factura = Convert.ToInt32(row.Cells[0].Text);
                var estado = row.Cells[4].Text.Substring(0, 1);

                switch (e.CommandName)
                {
                    case "mostrar":
                        Response.Redirect("~/Administracion/Facturacion/frmFactura.aspx?idf=" + id_factura + "&st=" + estado);
                        break;
                    case "imprimir":
                        /*Opcion que genera pdf y lo muestra*/
                        ImprimirReporte(id_factura);
                        break;
                    default:
                        break;
                }
            }
        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFacturas.PageIndex = e.NewPageIndex;
            Llenar_gvCompras();
        }

        #endregion

        #region Funciones

        protected void Llenar_gvCompras()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_Facturacion.SelectFactura();
            miTabla = objRespuesta.DataTableRespuesta;
            gvFacturas.DataSource = miTabla;
            gvFacturas.DataBind();

        }

        protected void ImprimirReporte(int id_factura)
        {
            string pathdb = "~/Administracion/Facturacion/Factura_rep.rpt";

            string path = Server.MapPath(pathdb);
            ReportDocument reporte = new ReportDocument();
            var dt_detalle = new DataTable();

            reporte.Load(path);

            objRespuesta = obj_Negocio_Facturacion.SelectDetalleFactura(id_factura);
            dt_detalle = objRespuesta.DataTableRespuesta;
          
            reporte.SetDataSource(dt_detalle);
            reporte.Refresh();

            string saveFilePath = Server.MapPath("~/doctos");

            string nombreArchivo = "formulario.pdf";
            string nombreDocto = saveFilePath + "\\" + nombreArchivo;

            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, nombreDocto);
            Response.Redirect("~/doctos/" + nombreArchivo);
                            
        }

        #endregion
    }
}