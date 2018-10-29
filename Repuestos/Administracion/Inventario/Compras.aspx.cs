using System;
using System.Data;
using System.Web.UI.WebControls;
using Capa_Negocio.Administracion.Inventario;
using Capa_Objetos.General;

namespace Repuestos.Inventario
{
    public partial class Compras : System.Web.UI.Page
    {
        CN_Compras obj_Negocio_Compras = new CN_Compras();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvCompras();
            }
        }

        protected void gvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvCompras.Rows[index];

                int id_compra = Convert.ToInt32(row.Cells[0].Text);
                var estado =  row.Cells[5].Text.Substring(0,1);
                
                switch (e.CommandName)
                {
                    case "modificar":
                        Response.Redirect("~/Administracion/Inventario/frmCompras.aspx?idc=" + id_compra+"&st="+estado);
                        break;
                    case "eliminar":
                        if (EliminarCompra(id_compra))
                        {
                            Llenar_gvCompras();
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

        protected void gvCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompras.PageIndex = e.NewPageIndex;
            Llenar_gvCompras();
        }

        protected void lkBtn_nuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracion/Inventario/frmCompras.aspx");
        }

        protected void gvCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            if (e.Row.Cells[5].Text == "CERRADO")
            {                
                e.Row.Cells[7].Controls.Clear();
            }
        }

        #endregion

        #region Funciones

        protected void Llenar_gvCompras()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_Compras.SelectCompra();
            miTabla = objRespuesta.DataTableRespuesta;
            gvCompras.DataSource = miTabla;
            gvCompras.DataBind();

        }

        protected bool EliminarCompra(int id_compra)
        {
            objRespuesta =  obj_Negocio_Compras.DeleteCompra(id_compra);
            return objRespuesta.BoolRespuesta;
        }

        #endregion

    }
}