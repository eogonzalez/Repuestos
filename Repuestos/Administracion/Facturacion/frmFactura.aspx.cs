using Capa_Negocio.Administracion.Facturacion;
using Capa_Objetos.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repuestos.Administracion.Facturacion
{
    public partial class frmFactura : System.Web.UI.Page
    {
        CO_Respuesta objRespuesta = new CO_Respuesta();
        CN_Facturacion obj_Negocio_Facturacion = new CN_Facturacion();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idf"] != null)
                {
                    var id_factura = Convert.ToInt32(Request.QueryString["idf"]);
                    Session.Add("IDFactura", id_factura);
                    Llenar_ddlCliente();
                    LlenoEncabezado(id_factura);
                    Llenar_gvDetalleFactura(id_factura);

                    if (Request.QueryString["st"] == "C")
                    {
                        BloquearControles();
                    }
                }
            }
        }

        protected void gvDetalleFactura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalleFactura.PageIndex = e.NewPageIndex;
            Llenar_gvDetalleFactura((int)Session["IDFactura"]);
            
        }

        protected void lkbRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracion/Facturacion/Facturas.aspx");
        }

        #region Funciones

        protected void Llenar_gvDetalleFactura(int id_factura)
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_Facturacion.SelectDetalleFactura(id_factura);
            miTabla = objRespuesta.DataTableRespuesta;
            gvDetalleFactura.DataSource = miTabla;
            gvDetalleFactura.DataBind();
        }

        protected void LlenoEncabezado(int id_factura)
        {
            var dt = new DataTable();
            objRespuesta = obj_Negocio_Facturacion.SelectFactura(id_factura);
            dt = objRespuesta.DataTableRespuesta;

            var row = dt.Rows[0];
            txtFechaFactura.Text = row["fecha_factura"].ToString();
            ddlCliente.SelectedValue = row["id_cliente"].ToString();
            txtNumeroFactura.Text = row["numero_factura"].ToString();
            txtSerieFactura.Text = row["serie"].ToString();

            if (row["total"] != null)
            {
                txtTotal.Text = Convert.ToDecimal(row["total"]).ToString("#.##");
            }
            else
            {
                txtTotal.Text = "0.00";
            }
        }

        protected void Llenar_ddlCliente()
        {
            var dt = new DataTable();
            Capa_Negocio.Catalogos.CN_Clientes objClientes = new Capa_Negocio.Catalogos.CN_Clientes();
            objRespuesta = objClientes.SelectClientes();
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddlCliente.DataTextField = dt.Columns["nombres"].ToString();
                ddlCliente.DataValueField = dt.Columns["id_cliente"].ToString();
                ddlCliente.DataSource = dt;
                ddlCliente.DataBind();
                ddlCliente.Items.Insert(0, new ListItem("Seleccione Cliente", "0"));
            }

        }

        protected void BloquearControles()
        {
            txtFechaFactura.Enabled = false;
            ddlCliente.Enabled = false;
            txtNumeroFactura.Enabled = false;
            txtSerieFactura.Enabled = false;
            txtTotal.Enabled = false;                  
        }

        #endregion


    }
}