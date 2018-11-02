using Capa_Objetos.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Objetos.Administracion.Servicios;
using Capa_Negocio.Administracion.Servicios;

namespace Repuestos.Administracion.Servicios
{
    public partial class frmServicio : System.Web.UI.Page
    {
        CO_Respuesta objRespuesta = new CO_Respuesta();
        CO_Servicios objServicios = new CO_Servicios();
        CN_Servicios obj_Negocio_Servicios = new CN_Servicios();

        #region Funciones del formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divAlertCorrecto.Visible = false;
                divAlertError.Visible = false;
                txtFechaServicio.Text = DateTime.Today.ToShortDateString();
                ddlVehiculo.Enabled = false;
                txtPrecio.Enabled = false;
                txtCantidadDisponible.Enabled = false;

                Llenar_ddlCliente();
                Llenar_ddlTipoServicio();
                Llenar_ddlRepuestos();

                if (Request.QueryString["ids"] != null)
                {//Si se envia del query string
                    var id_servicio = 0;
                    id_servicio = Convert.ToInt32(Request.QueryString["ids"]);
                    Session.Add("IDServicio", id_servicio);
                    LlenarEncabezado(id_servicio);

                    lkbtn_GuardarEncabezado.CommandName = "Actualizar";
                    lkbtn_GuardarEncabezado.Text = "Actualizar Encabezado";
                    ddlVehiculo.Enabled = true;
                    Llenar_gvProductos(id_servicio);
                    Llenar_gvServiciosExternos(id_servicio);
                }
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
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvProductos.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_correlativo = Convert.ToInt32(row.Cells[0].Text);
                int id_servicio = Convert.ToInt32(Request.QueryString["ids"]);

                Session.Add("IDCorrRespuesto", id_correlativo);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatosRepuesto(id_correlativo);
                        lkBtn_viewPanel_ModalPopupExtender.Show();                        
                        break;

                    case "eliminar":
                        if (EliminarDetalleRepuesto(id_correlativo))
                        {
                            Llenar_gvProductos(id_servicio);

                            objRespuesta = obj_Negocio_Servicios.SelectSubTotalRepuesto(id_servicio);
                            var costo_repuesto = objRespuesta.DecimalRespuesta;
                            ActualizaCostoRepuestos(costo_repuesto);
                            ActualizoMuestroCostoTotal();
                            ActualizarEncabezadoServicio(id_servicio);
                            LimpiarPaneles();
                        }
                        else
                        {
                            divAlertError.Visible = true;
                            ErrorMessagePrincipal.Text = objRespuesta.MensajeRespuesta;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductos.PageIndex = e.NewPageIndex;
            Llenar_gvProductos((int)Session["IDServicio"]);
        }

        protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            var id_servicio = 0;

            if (Session["IDServicio"] != null)
            {
                id_servicio = (int)Session["IDServicio"];

                switch (btnGuardarProducto.CommandName)
                {
                    case "Editar":
                        var correlativo = (int)Session["IDCorrRespuesto"];
                        if (ActualizarDetalleRepuestos(correlativo, id_servicio))
                        {
                            Llenar_gvProductos(id_servicio);
                            btnGuardarProducto.Text = "Agregar";
                            btnGuardarProducto.CommandName = "Guardar";

                            objRespuesta = obj_Negocio_Servicios.SelectSubTotalRepuesto(id_servicio);
                            var costo_repuesto = objRespuesta.DecimalRespuesta;
                            ActualizaCostoRepuestos(costo_repuesto);
                            ActualizoMuestroCostoTotal();
                            ActualizarEncabezadoServicio(id_servicio);
                            LimpiarPaneles();
                        }
                        else
                        {
                            lkBtn_viewPanel_ModalPopupExtender.Show();
                            ErrorMessageRepuesto.Text = "Ha ocurrido un error al actualizar. -" + objRespuesta.MensajeRespuesta;
                        } 

                        break;
                    case "Guardar":
                        if (AgregoDetalleRepuestos(id_servicio))
                        {
                            Llenar_gvProductos(id_servicio);

                            objRespuesta = obj_Negocio_Servicios.SelectSubTotalRepuesto(id_servicio);
                            var costo_repuesto = objRespuesta.DecimalRespuesta;
                            ActualizaCostoRepuestos(costo_repuesto);
                            ActualizoMuestroCostoTotal();
                            ActualizarEncabezadoServicio(id_servicio);
                            LimpiarPaneles();
                        }
                        else
                        {
                            lkBtn_viewPanel_ModalPopupExtender.Show();
                            ErrorMessageRepuesto.Text = "Ha ocurrido un error al almacenar producto. - "+objRespuesta.MensajeRespuesta;
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        protected void btnGuardarServicioExterno_Click(object sender, EventArgs e)
        {
            var id_servicio = 0;

            if (Session["IDServicio"] != null)
            {
                id_servicio = (int)Session["IDServicio"];

                switch (btnGuardarServicioExterno.CommandName)
                {
                    case "Editar":
                        var correlativo = (int)Session["IDCorrServicioExterno"];
                        if (ActualizarDetalleServicios(correlativo, id_servicio))
                        {
                            Llenar_gvServiciosExternos(id_servicio);
                            btnGuardarServicioExterno.Text = "Agregar";
                            btnGuardarServicioExterno.CommandName = "Guardar";


                            objRespuesta = obj_Negocio_Servicios.SelectSubTotalServicioExterno(id_servicio);
                            var costo_servicio = objRespuesta.DecimalRespuesta;
                            ActualizaCostoServiciosExernos(costo_servicio);
                            ActualizoMuestroCostoTotal();
                            ActualizarEncabezadoServicio(id_servicio);
                            LimpiarPaneles();
                        }
                        else
                        {
                            lkBtn_viewPanelServicioExterno_ModalPopupExtender.Show();
                            ErrorMessageServicioExterno.Text = "Ha ocurrido un error al actualizar. -" + objRespuesta.MensajeRespuesta;
                        }

                        break;
                    case "Guardar":
                        if (AgregoDetalleServicioExterno(id_servicio))
                        {
                            Llenar_gvServiciosExternos(id_servicio);

                            objRespuesta = obj_Negocio_Servicios.SelectSubTotalServicioExterno(id_servicio);
                            var costo_servicio = objRespuesta.DecimalRespuesta;
                            ActualizaCostoServiciosExernos(costo_servicio);
                            ActualizoMuestroCostoTotal();
                            ActualizarEncabezadoServicio(id_servicio);
                            LimpiarPaneles();
                        }
                        else
                        {
                            lkBtn_viewPanelServicioExterno_ModalPopupExtender.Show();
                            ErrorMessageServicioExterno.Text = "Ha ocurrido un error al almacenar. - " + objRespuesta.MensajeRespuesta;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        protected void btnSalirServicioExterno_Click(object sender, EventArgs e)
        {

        }

        protected void gvServiciosExternos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvServiciosExternos.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_correlativo = Convert.ToInt32(row.Cells[0].Text);
                int id_servicio = Convert.ToInt32(Request.QueryString["ids"]);

                Session.Add("IDCorrServicioExterno", id_correlativo);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatosServicio(id_correlativo);
                        lkBtn_viewPanelServicioExterno_ModalPopupExtender.Show();                        
                        break;

                    case "eliminar":
                        if (EliminarDetalleServicio(id_correlativo))
                        {
                            Llenar_gvServiciosExternos(id_servicio);

                            objRespuesta = obj_Negocio_Servicios.SelectSubTotalServicioExterno(id_servicio);
                            var costo_servicio = objRespuesta.DecimalRespuesta;
                            ActualizaCostoServiciosExernos(costo_servicio);
                            ActualizoMuestroCostoTotal();
                            ActualizarEncabezadoServicio(id_servicio);
                            LimpiarPaneles();
                        }
                        else
                        {
                            divAlertError.Visible = true;
                            ErrorMessagePrincipal.Text = objRespuesta.MensajeRespuesta;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        protected void gvServiciosExternos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvServiciosExternos.PageIndex = e.NewPageIndex;
            Llenar_gvServiciosExternos((int)Session["IDServicio"]);
        }

        protected void gvServiciosExternos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lkBtn_AgregarProducto_Click(object sender, EventArgs e)
        {
            if (Session["IDServicio"] != null)
            {
                lkBtn_viewPanel_ModalPopupExtender.Show();
            }
            else
            {
                divAlertError.Visible = true;
                ErrorMessagePrincipal.Text = "No se permite agregar detalle, Guarde los detalles del encabezado primero.";
            }            
        }

        protected void lkBtn_AgregarServicioExterno_Click(object sender, EventArgs e)
        {
            if (Session["IDServicio"] != null)
            {
                lkBtn_viewPanelServicioExterno_ModalPopupExtender.Show();
            }
            else
            {
                divAlertError.Visible = true;
                ErrorMessagePrincipal.Text = "No se permite agregar detalle, Guarde los detalles del encabezado primero.";
            }
        }

        protected void lkbtn_GuardarEncabezado_Click(object sender, EventArgs e)
        {
            switch (lkbtn_GuardarEncabezado.CommandName)
            {
                case "Guardar":
                    var id_servicio = GuardarEncabezadoServicio();
                    if (id_servicio > 0)
                    {
                        Session.Add("IDServicio", id_servicio);
                        lkbtn_GuardarEncabezado.CommandName = "Actualizar";
                        lkbtn_GuardarEncabezado.Text = "Actualizar Encabezado";
                        divAlertCorrecto.Visible = true;
                        MensajeCorrectoPrincipal.Text = "Encabezado Guardado Correctamente.";
                    }
                    else
                    {
                        divAlertError.Visible = true;
                        ErrorMessagePrincipal.Text = "Ha ocurrido un error al guardar encabezado.";
                    }
                    

                    break;
                case "Actualizar":
                    if (ActualizarEncabezadoServicio((int)Session["IDServicio"]))
                    {
                        divAlertCorrecto.Visible = true;
                        MensajeCorrectoPrincipal.Text = "Encabezado Actualizado Correctamente.";
                    }
                    else
                    {
                        divAlertError.Visible = true;
                        ErrorMessagePrincipal.Text = "Ha ocurrido un error al actualizar encabezado.";
                    }
                    break;
               
                default:
                    break;
            }


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
                    ActualizaCostoServicio(costo_final);
                    txtCostoServicio.Text = costo_final.ToString("#.##");
                    ActualizoMuestroCostoTotal();
                }
            }
            else
            {
                var costo_final = 0.0m;
                ActualizaCostoServicio(costo_final);
                txtCostoServicio.Text = costo_final.ToString("#.##");
                ActualizoMuestroCostoTotal();
            }

            
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lkBtn_viewPanel_ModalPopupExtender.Show();
            var id_producto = 0;
            id_producto = Convert.ToInt32(ddlProducto.SelectedValue.ToString());
            ActualizarProductoInventario(id_producto);
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
                ddlCliente.Items.Insert(0, new ListItem("Seleccione Cliente", "0"));
            }
            
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
                ddlVehiculo.Items.Insert(0, new ListItem("Seleccione Vehiculo", "0"));
            }
            else 
            {
                ddlVehiculo.Items.Insert(0, new ListItem("No Tiene Vehiculo", "0"));
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
        
        protected void ActualizaCostoServicio(decimal costo_servicio)
        {
            if (Session["CostoServicio"] != null)
            {//Si costo Existe
                Session["CostoServicio"] = costo_servicio;
            }
            else
            {//Si Costo no Existe
                Session.Add("CostoServicio", costo_servicio);
            }
        }

        protected void ActualizaCostoRepuestos(decimal costo_repuestos)
        {
            if (Session["CostoRepuestos"] != null)
            {//Si costo Existe
                Session["CostoRepuestos"] = costo_repuestos;
            }
            else
            {//Si Costo no Existe
                Session.Add("CostoRepuestos", costo_repuestos);
            }
        }

        protected void ActualizaCostoServiciosExernos(decimal costo_servicio_externo)
        {
            if (Session["CostoServiciosExernos"] != null)
            {//Si costo Existe
                Session["CostoServiciosExernos"] = costo_servicio_externo;
            }
            else
            {//Si Costo no Existe
                Session.Add("CostoServiciosExernos", costo_servicio_externo);
            }
        }

        protected void ActualizoMuestroCostoTotal()
        {
            decimal costo_servicio = 0.00m;
            decimal costo_repuesto = 0.00m;
            decimal costo_servicio_externo = 0.00m;
            decimal CostoTotal = 0.00m;

            if (Session["CostoServicio"] != null)
            {//Si existe existe
                costo_servicio = (decimal)Session["CostoServicio"];
            }
            else
            {
                Session.Add("CostoServiciosExernos", costo_servicio);
            }

            if (Session["CostoRepuestos"] != null)
            {
                costo_repuesto = (decimal)Session["CostoRepuestos"];
            }
            else
            {
                Session.Add("CostoRepuestos", costo_repuesto);
            }

            if (Session["CostoServiciosExernos"] != null)
            {
                costo_servicio_externo = (decimal)Session["CostoServiciosExernos"];
            }
            else
            {
                Session.Add("CostoServiciosExernos", costo_servicio_externo);
            }

            CostoTotal = CalculoCostoTotal(costo_servicio, costo_repuesto, costo_servicio_externo);
            txtTotal.Text = CostoTotal.ToString("#.##");

        }

        protected decimal CalculoCostoTotal(decimal costo_servicio, decimal costo_repuestos, decimal costo_servicio_externo)
        {
            decimal CostoTotal = 0.00m;
            CostoTotal = costo_servicio + costo_repuestos + costo_servicio_externo;

            if (Session["CostoTotal"] !=null)
            {//Si costo total existe
                Session["CostoTotal"] = CostoTotal;
            }
            else
            {//Si costo total no existe
                Session.Add("CostoTotal",CostoTotal);
            }

            return CostoTotal;
        }

        protected int GuardarEncabezadoServicio()
        {
            objServicios.Id_Cliente = Convert.ToInt32(ddlCliente.SelectedValue.ToString());
            objServicios.Id_Vehiculo_Cliente = Convert.ToInt32(ddlVehiculo.SelectedValue.ToString());
            objServicios.Id_TipoServicio = Convert.ToInt32(ddlTipoServicio.SelectedValue.ToString());
            objServicios.Fecha_Ingreso_Servicio = Convert.ToDateTime(txtFechaServicio.Text);
            objServicios.Kilometraje_Ingreso_Servicio = Convert.ToDecimal(txtKilometraje.Text);
            objServicios.CostoServicio = Convert.ToDecimal(txtCostoServicio.Text);
            objServicios.CostoTotal = Convert.ToDecimal(txtTotal.Text);

            var respuesta = 0;
            objRespuesta = obj_Negocio_Servicios.InsertEncabezadoServicio(objServicios);
            respuesta = objRespuesta.IntRespuesta;

            return respuesta;
        }

        protected void LlenarEncabezado(int id_servicio)
        {
            var dt = new DataTable();
            objRespuesta = obj_Negocio_Servicios.SelectServicios(id_servicio);
            dt = objRespuesta.DataTableRespuesta;

            var row = dt.Rows[0];
            ddlCliente.SelectedValue = row["id_cliente"].ToString();
            Llenar_ddlVehiculo((int)row["id_cliente"]);
            ddlVehiculo.SelectedValue = row["id_vehiculo_cliente"].ToString();
            ddlTipoServicio.SelectedValue = row["id_tipo_servicio"].ToString();
            txtKilometraje.Text = row["kilometraje_servicio"].ToString();

            var costo_servicio = (decimal)row["costo_servicio"];
            ActualizaCostoServicio(costo_servicio);
            txtCostoServicio.Text = costo_servicio.ToString("#.##");

            var total = (decimal)row["costo_total"];
            txtTotal.Text = total.ToString("#.##");
        }

        protected bool ActualizarEncabezadoServicio(int id_servicio)
        {
            objServicios.Id_Servicio = id_servicio;
            objServicios.Id_Cliente = Convert.ToInt32(ddlCliente.SelectedValue.ToString());
            objServicios.Id_Vehiculo_Cliente = Convert.ToInt32(ddlVehiculo.SelectedValue.ToString());
            objServicios.Id_TipoServicio = Convert.ToInt32(ddlTipoServicio.SelectedValue.ToString());
            objServicios.Fecha_Ingreso_Servicio = Convert.ToDateTime(txtFechaServicio.Text);
            objServicios.Kilometraje_Ingreso_Servicio = Convert.ToInt32(txtKilometraje.Text);
            objServicios.CostoServicio = Convert.ToDecimal(txtCostoServicio.Text);
            objServicios.CostoTotal = Convert.ToDecimal(txtTotal.Text);

            objRespuesta = obj_Negocio_Servicios.UpdateEncabezadoServicios(objServicios);
            return objRespuesta.BoolRespuesta;
        }

        protected bool AgregoDetalleRepuestos(int id_servicio)
        {
            objServicios.Id_Servicio = id_servicio;
            objServicios.Id_Producto = Convert.ToInt32(ddlProducto.SelectedValue.ToString());
            objServicios.Cantidad = Convert.ToInt32(txtCantidad.Text);
            objServicios.PrecioVenta = Convert.ToDecimal(txtPrecio.Text);
            objServicios.SubTotal = objServicios.Cantidad * objServicios.PrecioVenta;

            objRespuesta = obj_Negocio_Servicios.InsertDetalleRepuesto(objServicios);
            return objRespuesta.BoolRespuesta;
        }

        protected bool AgregoDetalleServicioExterno(int id_servicio)
        {
            objServicios.Id_Servicio = id_servicio;
            objServicios.Descripcion = txtDescripcion.Text;
            objServicios.PrecioServicio = Convert.ToDecimal(txtPrecioServicio.Text);

            objRespuesta = obj_Negocio_Servicios.InsertDetalleServicio(objServicios);
            return objRespuesta.BoolRespuesta;
        }

        protected void Llenar_gvProductos(int id_servicio)
        {
            var dt = new DataTable();
            objRespuesta = obj_Negocio_Servicios.SelectDetalleRepuestos(id_servicio);
            dt = objRespuesta.DataTableRespuesta;
            gvProductos.DataSource = dt;
            gvProductos.DataBind();
        }

        protected void Llenar_gvServiciosExternos(int id_servicio)
        {
            var dt = new DataTable();
            objRespuesta = obj_Negocio_Servicios.SelectDetalleServicios(id_servicio);
            dt = objRespuesta.DataTableRespuesta;
            gvServiciosExternos.DataSource = dt;
            gvServiciosExternos.DataBind();
        }

        protected bool ActualizarDetalleRepuestos(int correlativo_repuestos, int id_servicio)
        {
            objServicios.Corr_ServicioRepuesto = correlativo_repuestos;
            objServicios.Id_Servicio = id_servicio;
            objServicios.Id_Producto = Convert.ToInt32(ddlProducto.SelectedValue.ToString());
            objServicios.Cantidad = Convert.ToInt32(txtCantidad.Text);
            objServicios.PrecioVenta = Convert.ToDecimal(txtPrecio.Text);
            objServicios.SubTotal = objServicios.Cantidad * objServicios.PrecioVenta;

            objRespuesta = obj_Negocio_Servicios.UpdateDetalleRepuestos(objServicios);
            return objRespuesta.BoolRespuesta;
        }

        protected bool ActualizarDetalleServicios(int correlativo_servicio, int id_servicio)
        {
            objServicios.Corr_ServicioExterno = correlativo_servicio;
            objServicios.Id_Servicio = id_servicio;
            objServicios.Descripcion = txtDescripcion.Text;
            objServicios.PrecioServicio = Convert.ToDecimal(txtPrecioServicio.Text);

            objRespuesta = obj_Negocio_Servicios.UpdateDetalleServicioExterno(objServicios);
            return objRespuesta.BoolRespuesta;
        }

        protected void Llenar_ddlRepuestos()
        {
            var dt = new DataTable();
            Capa_Negocio.Catalogos.Repuestos.CN_Productos objProductos = new Capa_Negocio.Catalogos.Repuestos.CN_Productos();
            objRespuesta = objProductos.SelectProductos();
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                var repuesto = dt.Columns["repuesto"].ToString();
                var marca = dt.Columns["marca"].ToString();
                var vehiculo = dt.Columns["vehiculo"].ToString();
                var valor_combo = repuesto + " - " + marca + " - " + vehiculo;

                ddlProducto.DataTextField = dt.Columns["valor_combo"].ToString();
                ddlProducto.DataValueField = dt.Columns["id_producto"].ToString();
                ddlProducto.DataSource = dt;
                ddlProducto.DataBind();
                ddlProducto.Items.Insert(0, new ListItem("Seleccione Producto", "0"));
            }            
        }

        protected void ActualizarProductoInventario(int id_producto)
        {
            var dt = new DataTable();
            Capa_Negocio.Administracion.Inventario.CN_Inventarios objInventarios = new Capa_Negocio.Administracion.Inventario.CN_Inventarios();
            objRespuesta = objInventarios.SelectProductoInventario(id_producto);
            dt = objRespuesta.DataTableRespuesta;
            var row = dt.Rows[0];

            if (row["precio"] != null)
            {
                txtCantidadDisponible.Text = row["disponible"].ToString();
                txtPrecio.Text = Convert.ToDecimal(row["precio"]).ToString("#.##");
            }
            else
            {
                txtCantidadDisponible.Text = "0";
                txtPrecio.Text = "0.00";
            }            
        }

        protected void LimpiarPaneles()
        {
            divAlertCorrecto.Visible = false;
            divAlertError.Visible = false;
            ErrorMessagePrincipal.Text = string.Empty;
            ErrorMessageRepuesto.Text = string.Empty;
            ErrorMessageServicioExterno.Text = string.Empty;

            txtCantidad.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecioServicio.Text = string.Empty;

            ddlProducto.SelectedValue = "0";
            
        }

        protected void MostrarDatosServicio(int correlativo_servicio)
        {
            btnGuardarServicioExterno.Text = "Editar";
            btnGuardarServicioExterno.CommandName = "Editar";

            var dt = new DataTable();
            objRespuesta = obj_Negocio_Servicios.SelectDetalleServicios(correlativo_servicio, true);
            dt = objRespuesta.DataTableRespuesta;
            var row = dt.Rows[0];

            txtDescripcion.Text = row["descripcion"].ToString();
            txtPrecioServicio.Text = row["precio"].ToString();

        }

        protected bool EliminarDetalleServicio(int correlativo_servicio)
        {
            objRespuesta = obj_Negocio_Servicios.DeleteDetalleServicioExterno(correlativo_servicio);
            return objRespuesta.BoolRespuesta;
        }

        protected void MostrarDatosRepuesto(int correlativo_repuesto)
        {
            btnGuardarProducto.Text = "Editar";
            btnGuardarProducto.CommandName = "Editar";

            var dt = new DataTable();
            objRespuesta = obj_Negocio_Servicios.SelectDetalleRepuestos(correlativo_repuesto, true);
            dt = objRespuesta.DataTableRespuesta;
            var row = dt.Rows[0];

            ddlProducto.SelectedValue = row["id_producto"].ToString();
            ActualizarProductoInventario((int)row["id_producto"]);
            txtCantidad.Text = row["cantidad"].ToString();
        }

        protected bool EliminarDetalleRepuesto(int correlativo_repuesto)
        {
            objRespuesta = obj_Negocio_Servicios.DeleteDetalleRepuesto(correlativo_repuesto);
            return objRespuesta.BoolRespuesta;
        }
        #endregion

    }
}