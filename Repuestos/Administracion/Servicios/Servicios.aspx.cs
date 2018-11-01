using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Negocio.Administracion.Servicios;
using Capa_Objetos.General;

namespace Repuestos.Administracion.Servicios
{
    public partial class Servicios : System.Web.UI.Page
    {
        CN_Servicios obj_Negocio_Servicio = new CN_Servicios();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvServicios();
            }
        }

        protected void gvServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvServicios.Rows[index];

                int id_servicio = Convert.ToInt32(row.Cells[0].Text);
                var estado = row.Cells[6].Text.Substring(0, 1);

                switch (e.CommandName)
                {
                    case "modificar":
                        Response.Redirect("~/Administracion/Servicios/frmServicio.aspx?ids=" + id_servicio + "&st=" + estado);
                        break;
                    case "eliminar":
                        if (EliminarServicio(id_servicio))
                        {
                            Llenar_gvServicios();
                        }
                        else
                        {
                            ErrorPrincipal.Text = objRespuesta.MensajeRespuesta;
                        }


                        break;
                    default:
                        break;
                }
            }
        }

        protected void gvServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvServicios.PageIndex = e.NewPageIndex;
            Llenar_gvServicios();
        }
       
        protected void lkBtn_nuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracion/Servicios/frmServicio.aspx");
        }

        protected void gvServicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            if (e.Row.Cells[6].Text == "CERRADO")
            {
                e.Row.Cells[8].Controls.Clear();
            }
        }

        #endregion

        #region Funciones

        protected void Llenar_gvServicios()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_Servicio.SelectServicios();
            miTabla = objRespuesta.DataTableRespuesta;
            gvServicios.DataSource = miTabla;
            gvServicios.DataBind();

        }

        protected bool EliminarServicio(int id_servicio)
        {
            objRespuesta = obj_Negocio_Servicio.DeleteServicio(id_servicio);
            return objRespuesta.BoolRespuesta;
        }

        #endregion
    }
}