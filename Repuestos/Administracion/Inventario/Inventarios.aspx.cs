using System;
using System.Data;
using System.Web.UI.WebControls;
using Capa_Negocio.Administracion.Inventario;
using Capa_Objetos.General;

namespace Repuestos.Inventario
{
    public partial class Inventarios : System.Web.UI.Page
    {
        CN_Inventarios obj_Negocio_Inventarios = new CN_Inventarios();
        CO_Respuesta objRespuesta = new CO_Respuesta();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvInventarios();
            }
        }

        protected void gvInventarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvInventarios.Rows[index];

                int id_movimiento = Convert.ToInt32(row.Cells[0].Text);
                var estado = row.Cells[5].Text.Substring(0, 1);

            }
        }

        protected void gvInventarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInventarios.PageIndex = e.NewPageIndex;
            Llenar_gvInventarios();
        }


        private void Llenar_gvInventarios()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_Inventarios.SelectInventarios();
            miTabla = objRespuesta.DataTableRespuesta;
            gvInventarios.DataSource = miTabla;
            gvInventarios.DataBind();
        }

        protected void gvInventarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}