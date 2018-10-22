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

                }
                else
                {
                    if (Session["IDCompra"] != null)
                    {
                        id_compra = (Int32)Session["IDCompra"];
                        LlenargvProductos(id_compra);
                    }
                    else
                    {
                        LlenargvProductos();
                    }
                }
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            //Valido si encabezado de compra no esta creado
            if (Session["IDCompra"] != null)
            {//Si compra esta creada
                var id_compra = (Int32)Session["IDCompra"];
                ActualizoEncabezadoCompra(id_compra);                
                AgregoDetalleCompra(id_compra);
                LlenoEncabezado(id_compra);
            }
            else
            {//Si compra no esta creada
                var id_compra = CreoEncabezadoCompra();
                Session.Add("IDCompra", id_compra);
                AgregoDetalleCompra(id_compra);
            }
            
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administracion/Inventario/Compras.aspx");
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

        protected void AgregoDetalleCompra(int id_compra)
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
            if (respuesta)
            {
                LlenargvProductos(objCompras.Id_Compra);
            }

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

        #endregion
    }
}