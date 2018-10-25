using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Negocio.Administracion.Inventario;
using Capa_Objetos.Administracion.Inventario;

namespace Repuestos.Administracion.Inventario
{
    public partial class frmCompras : System.Web.UI.Page
    {
        CN_Compras obj_Negocio_Compras = new CN_Compras();
        CO_Compras objCompras = new CO_Compras();

        #region Funciones del formulario

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                divAlertCorrecto.Visible = false;
                divAlertError.Visible = false;
                txtTotal.Enabled = false;

                txtFechaCompra.Text = DateTime.Today.ToShortDateString();
                var id_compra = 0;
                Llenar_ddlProveedor();
                Llenar_ddlRepuestos();

                if (Request.QueryString["idc"] !=null)
                {//Si se envia del query string
                    id_compra = Convert.ToInt32(Request.QueryString["idc"]);
                    Session.Add("IDCompra", id_compra);
                    LlenoEncabezado(id_compra);
                    LlenargvProductos(id_compra);

                    if (Request.QueryString["st"] == "C")
                    {
                        BloquearControles();
                    }

                }
                else
                {
                    Session.Remove("IDCompra");
                    LlenargvProductos();                    
                }
            }
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
                int id_compra = Convert.ToInt32(Request.QueryString["idc"]);

                Session.Add("IDCorrelativo", id_correlativo);

                switch (e.CommandName)
                {
                    case "modificarProducto":
                        MostrarDatos(id_correlativo);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminarProducto":
                        EliminarDetalleCompra(id_correlativo,id_compra);
                        LlenargvProductos();
                        break;

                    default:
                        break;
                }
            }
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            //Valido si encabezado de compra no esta creado
            if (Session["IDCompra"] != null)
            {//Si compra esta creada
                var id_compra = (Int32)Session["IDCompra"];
                
                ActualizoEncabezadoCompra(id_compra);

                switch (btnGuardarProducto.CommandName)
                {
                    case "Editar":
                        var id_correlativo = (Int32)Session["IDCorrelativo"];

                        if (ActualizarDetalleCompra(id_correlativo,id_compra))
                        {
                            LlenoEncabezado(id_compra);
                            LlenargvProductos(id_compra);
                            btnGuardarProducto.Text = "Agregar";
                            btnGuardarProducto.CommandName = "GuardarProducto";
                        }
                        else
                        {
                            lkBtn_AgregarProducto_ModalPopupExtender.Show();
                            ErrorMessage.Text = "Ha ocurrido un error al actualizar producto.";
                        }
                        break;
                    case "GuardarProducto":
                        if (AgregoDetalleCompra(id_compra))
                        {
                            LlenoEncabezado(id_compra);
                            LlenargvProductos(id_compra);

                        }
                        else
                        {
                            lkBtn_AgregarProducto_ModalPopupExtender.Show();
                            ErrorMessage.Text = "Ha ocurrido un error al almacenar datos.";
                        }
                        break;
                    default:
                        break;
                }                                            
            }
            else
            {//Si compra no esta creada
                var id_compra = CreoEncabezadoCompra();
                Session.Add("IDCompra", id_compra);
                if (AgregoDetalleCompra(id_compra))
                {
                    LlenoEncabezado(id_compra);
                    LlenargvProductos(id_compra);
                }
                
            }
            
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracion/Inventario/Compras.aspx");
        }

        protected void lkbtnCerrarCompra_Click(object sender, EventArgs e)
        {
            if (Session["IDCompra"] != null)
            {
                int id_compra = Convert.ToInt32(Session["IDCompra"].ToString());
                CerrarCompra(id_compra);
                BloquearControles();
                divAlertCorrecto.Visible = true;
                MensajeCorrectoPrincipal.Text = "Compra cerrada correctamente, se agregaron los productos al inventario.";
            }
            else
            {
                divAlertError.Visible = true;
                ErrorMessagePrincipal.Text = "No es posible cerrar compra.";
            }


        }

        protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            if (Request.QueryString["st"] != null)
            {
                if (Request.QueryString["st"] == "C")
                {
                    e.Row.Cells[5].Controls.Clear();
                    e.Row.Cells[6].Controls.Clear();
                }
            }
        }

        #endregion

        #region Funciones

        protected void LlenargvProductos(int id_compra = 0)
        {
            var miTabla = new DataTable();
            miTabla = obj_Negocio_Compras.SelectDetalleCompras(id_compra);
            gvProductos.DataSource = miTabla;
            gvProductos.DataBind();
        }

        protected void Llenar_ddlProveedor()
        {
            var dt = new DataTable();
            Capa_Negocio.Catalogos.CN_Proveedores objProvedor = new Capa_Negocio.Catalogos.CN_Proveedores();
            dt = objProvedor.SelectProveedores();

            if (dt.Rows.Count > 0)
            {
                ddlProveedor.DataTextField = dt.Columns["nombre_proveedor"].ToString();
                ddlProveedor.DataValueField = dt.Columns["id_proveedor"].ToString();
                ddlProveedor.DataSource = dt;
                ddlProveedor.DataBind();
            }
            ddlProveedor.Items.Insert(0, new ListItem("Seleccione Proveedor", "NA"));
        }

        protected void Llenar_ddlRepuestos()
        {
            var dt = new DataTable();
            Capa_Negocio.Catalogos.Repuestos.CN_Productos objProductos = new Capa_Negocio.Catalogos.Repuestos.CN_Productos();
            dt = objProductos.SelectProductos();

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
            }

            ddlProducto.Items.Insert(0, new ListItem("Seleccione Producto", "NA"));
        }

        protected int CreoEncabezadoCompra()
        {
            //Obtengo valores de encabezado
            objCompras.Fecha_Compra = Convert.ToDateTime(txtFechaCompra.Text);
            objCompras.Id_Proveedor = Convert.ToInt32(ddlProveedor.SelectedValue.ToString());
            objCompras.NumeroCompra = Convert.ToInt32(txtNumeroCompra.Text);
            objCompras.Serie = txtSerieCompra.Text;

            var respuesta = obj_Negocio_Compras.InsertEncabezadoCompra(objCompras);

            return respuesta;
        }

        protected bool AgregoDetalleCompra(int id_compra)
        {
            //Obtengo valores de detalle
            objCompras.Id_Compra = id_compra;
            objCompras.NumeroCompra = Convert.ToInt32(txtNumeroCompra.Text);
            objCompras.Serie = txtSerieCompra.Text;
            objCompras.Id_Producto = Convert.ToInt32(ddlProducto.SelectedValue.ToString());
            objCompras.Cantidad = Convert.ToInt32(txtCantidad.Text);
            objCompras.Precio = Convert.ToDouble(txtPrecio.Text);
            objCompras.SubTotal = objCompras.Cantidad * objCompras.Precio;

            var respuesta = obj_Negocio_Compras.InsertDetalleCompra(objCompras);
            return respuesta;
        }

        protected void LlenoEncabezado(int id_compra)
        {
            var dt = obj_Negocio_Compras.SelectCompra(id_compra);
            var row = dt.Rows[0];
            txtFechaCompra.Text = row["fecha_compra"].ToString();
            ddlProveedor.SelectedValue = row["id_proveedor"].ToString();
            txtNumeroCompra.Text = row["numero_compra"].ToString();
            txtSerieCompra.Text = row["serie"].ToString();

            if (row["total"] != null)
            {
                txtTotal.Text = row["total"].ToString();
            }
            else
            {
                txtTotal.Text = "0.00";
            }

            
        }

        protected void ActualizoEncabezadoCompra(int id_compra)
        {
            //Obtengo valores de encabezado
            objCompras.Id_Compra = id_compra;
            objCompras.Fecha_Compra = Convert.ToDateTime(txtFechaCompra.Text);
            objCompras.Id_Proveedor = Convert.ToInt32(ddlProveedor.SelectedValue.ToString());
            objCompras.NumeroCompra = Convert.ToInt32(txtNumeroCompra.Text);
            objCompras.Serie = txtSerieCompra.Text;

            obj_Negocio_Compras.UpdateEncabezadoCompra(objCompras);

        }

        protected void BloquearControles()
        {
            txtFechaCompra.Enabled = false;
            ddlProveedor.Enabled = false;
            txtNumeroCompra.Enabled = false;
            txtSerieCompra.Enabled = false;
            lkBtn_AgregarProducto.Attributes.Add("disabled","true");

            lkBtn_AgregarProducto_ModalPopupExtender.Enabled = false;
            lkbtnCerrarCompra.Attributes.Add("disabled","true");
        }

        protected bool CerrarCompra(int id_compra)
        {
            return obj_Negocio_Compras.CerrarCompra(id_compra);
        }

        protected void MostrarDatos(int id_correlativo)
        {
            btnGuardarProducto.Text = "Editar";
            btnGuardarProducto.CommandName = "Editar";

            var tabla_datos = new DataTable();
            tabla_datos = obj_Negocio_Compras.SelectDetalleCompraProducto(id_correlativo);
            var row = tabla_datos.Rows[0];

            ddlProducto.SelectedValue = row["id_producto"].ToString();
            txtCantidad.Text = row["cantidad"].ToString();
            txtPrecio.Text = row["precio"].ToString();
        }

        protected bool ActualizarDetalleCompra(int id_correlativo, int id_compra)
        {
            var respuesta = false;
            objCompras.Id_Correlativo = id_correlativo;
            objCompras.Id_Compra = id_compra;
            objCompras.NumeroCompra = Convert.ToInt32(txtNumeroCompra.Text);
            objCompras.Serie = txtSerieCompra.Text;
            objCompras.Id_Producto = Convert.ToInt32(ddlProducto.SelectedValue.ToString());

            objCompras.Cantidad = Convert.ToInt32(txtCantidad.Text.Replace(",",""));            

            objCompras.Precio = Convert.ToDouble(txtPrecio.Text);
            objCompras.SubTotal = objCompras.Cantidad * objCompras.Precio;

            respuesta = obj_Negocio_Compras.UpdateDetalleCompra(objCompras);
            return respuesta;
        }

        protected bool EliminarDetalleCompra(int id_correlativo, int id_compra)
        {
            return obj_Negocio_Compras.DeleteDetalleCompra(id_correlativo,id_compra);
        }

        #endregion


    }
}