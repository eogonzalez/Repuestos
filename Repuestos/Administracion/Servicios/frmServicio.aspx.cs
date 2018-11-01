using Capa_Objetos.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repuestos.Administracion.Servicios
{
    public partial class frmServicio : System.Web.UI.Page
    {
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divAlertCorrecto.Visible = false;
                divAlertError.Visible = false;
                txtFechaServicio.Text = DateTime.Today.ToShortDateString();
                ddlVehiculo.Enabled = false;

                Llenar_ddlCliente();
                Llenar_ddlTipoServicio();

            }
        }

        protected void lkbRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracion/Servicios/Servicios.aspx");
        }

        protected void lkbtnCerrarCompra_Click(object sender, EventArgs e)
        {

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {

        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardarServicioExterno_Click(object sender, EventArgs e)
        {

        }

        protected void btnSalirServicioExterno_Click(object sender, EventArgs e)
        {

        }

        protected void gvServiciosExternos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvServiciosExternos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvServiciosExternos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lkBtn_AgregarProducto_Click(object sender, EventArgs e)
        {
            lkBtn_viewPanel_ModalPopupExtender.Show();
        }

        protected void lkBtn_AgregarServicioExterno_Click(object sender, EventArgs e)
        {
            lkBtn_viewPanelServicioExterno_ModalPopupExtender.Show();
        }

        protected void lkbtn_GuardarEncabezado_Click(object sender, EventArgs e)
        {

        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlVehiculo.Enabled = true;
            var id_cliente = 0;
            id_cliente = Convert.ToInt32(ddlCliente.SelectedValue.ToString());
            Llenar_ddlVehiculo(id_cliente);
        }

        protected void ddlTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id_tipo_servicio = 0;
            id_tipo_servicio = Convert.ToInt32(ddlTipoServicio.SelectedValue.ToString());
            if (id_tipo_servicio > 0)
            {
                Capa_Negocio.Administracion.Servicios.CN_Tipo_Servicio objTipoServicio = new Capa_Negocio.Administracion.Servicios.CN_Tipo_Servicio();
                var dt = new DataTable();
                objRespuesta = objTipoServicio.SelectTipoServicio(id_tipo_servicio);
                dt = objRespuesta.DataTableRespuesta;
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var costo = 0.00m;
                    var porcentage = 0.00m;
                    var costo_final = 0.0m;
                    costo = Convert.ToDecimal(row["costo"].ToString());
                    porcentage = Convert.ToDecimal(row["porcentaje_ganancia"].ToString());
                    costo_final = (costo * porcentage) + costo;
                    txtCostoServicio.Text = costo_final.ToString("#.##");
                }
            }
            else
            {
                txtCostoServicio.Text = "0";
            }

            
        }

        #endregion

        #region Funciones

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
            }
            ddlCliente.Items.Insert(0, new ListItem("Seleccione Cliente", "NA"));
        }

        protected void Llenar_ddlVehiculo(int id_cliente)
        {
            var dt = new DataTable();
            Capa_Negocio.Catalogos.CN_VehiculosClientes objVehiculo = new Capa_Negocio.Catalogos.CN_VehiculosClientes();
            objRespuesta = objVehiculo.SelectVehiculosClientes(id_cliente);
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddlVehiculo.DataTextField = dt.Columns["vehiculo"].ToString();
                ddlVehiculo.DataValueField = dt.Columns["id_vehiculo_cliente"].ToString();
                ddlVehiculo.DataSource = dt;
                ddlVehiculo.DataBind();
                ddlVehiculo.Items.Insert(0, new ListItem("Seleccione Vehiculo", "NA"));
            }
            else 
            {
                ddlVehiculo.Items.Insert(0, new ListItem("No Tiene Vehiculo", "NA"));
            }

        }

        protected void Llenar_ddlTipoServicio()
        {
            var dt = new DataTable();
            Capa_Negocio.Administracion.Servicios.CN_Tipo_Servicio objTipoServicio = new Capa_Negocio.Administracion.Servicios.CN_Tipo_Servicio();
            objRespuesta = objTipoServicio.SelectTipoServicio();
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddlTipoServicio.DataTextField = dt.Columns["tipo_servicio"].ToString();
                ddlTipoServicio.DataValueField = dt.Columns["id_tipo_servicio"].ToString();
                ddlTipoServicio.DataSource = dt;
                ddlTipoServicio.DataBind();
                ddlTipoServicio.Items.Insert(0, new ListItem("Seleccione Tipo Servicio", "0"));
            }
                        
        }


        #endregion

        
    }
}