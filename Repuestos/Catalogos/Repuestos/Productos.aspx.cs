using System;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos.Repuestos;
using Capa_Objetos.Catalogos.Repuestos;
using System.Data;
using Capa_Negocio.Catalogos.Vehiculos;
using Capa_Objetos.General;

namespace Repuestos.Inventario
{
    public partial class Productos : System.Web.UI.Page
    {
        CN_Productos obj_Neg_Producto = new CN_Productos();
        CO_Productos obj_Producto = new CO_Productos();
        CO_Respuesta objRespuesta = new CO_Respuesta();


        #region Funciones del formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvProductos();
                Llenar_ddlCategoria();
                Llenar_ddlVehiculo();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_producto = 0;

            if (Session["IDProducto"] != null)
            {
                id_producto = (Int32)Session["IDProducto"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarProducto(id_producto))
                    {
                        Llenar_gvProductos();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar producto. - "+objRespuesta.MensajeRespuesta;
                    }

                    break;
                case "Guardar":
                    if (GuardarProducto())
                    {
                        LimpiarPanel();
                        Llenar_gvProductos();
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al almacenar los datos - "+objRespuesta.MensajeRespuesta;
                    }
                    break;

                default:
                    break;
            }
        }

        protected void gvRepuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvRepuestos.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_producto = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDProducto", id_producto);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_producto);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":
                        if (EliminarProducto(id_producto))
                        {
                            Llenar_gvProductos();
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

        protected void gvRepuestos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRepuestos.PageIndex = e.NewPageIndex;
            Llenar_gvProductos();
        }

        #endregion

        #region Funciones

        protected void Llenar_gvProductos()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Neg_Producto.SelectProductos();
            miTabla = objRespuesta.DataTableRespuesta;
            gvRepuestos.DataSource = miTabla;
            gvRepuestos.DataBind();
        }

        protected void MostrarDatos(int id_producto)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            objRespuesta = obj_Neg_Producto.SelectProductos(id_producto);
            tabla_datos = objRespuesta.DataTableRespuesta;
            var row = tabla_datos.Rows[0];

            ddl_categoria.SelectedValue = row["id_categoria"].ToString();

            //Llenar_ddlLinea(Convert.ToInt32(ddl_marca.SelectedValue.ToString()));
            ddl_vehiculo.SelectedValue = row["id_vehiculo"].ToString();
            ddl_vehiculo.Enabled = true;

            txtNombre.Text = row["nombre"].ToString();
            txtMarca.Text = row["marca"].ToString();
            txtDescripcion.Text = row["descripcion"].ToString();
        }

        protected bool GuardarProducto()
        {
            bool respuesta = false;
            obj_Producto.Id_Categoria = Convert.ToInt32(ddl_categoria.SelectedValue.ToString());
            obj_Producto.Id_Vehiculo = Convert.ToInt32(ddl_vehiculo.SelectedValue.ToString());
            obj_Producto.Nombre = txtNombre.Text;
            obj_Producto.Marca = txtMarca.Text;
            obj_Producto.Descripcion = txtDescripcion.Text;

            objRespuesta = obj_Neg_Producto.InsertProducto(obj_Producto);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool ActualizarProducto(int id_producto)
        {
            var respuesta = false;
            obj_Producto.Id_Producto = id_producto;
            obj_Producto.Id_Categoria = Convert.ToInt32(ddl_categoria.SelectedValue.ToString());
            obj_Producto.Id_Vehiculo = Convert.ToInt32(ddl_vehiculo.SelectedValue.ToString());
            obj_Producto.Nombre = txtNombre.Text;
            obj_Producto.Marca = txtMarca.Text;            
            obj_Producto.Descripcion = txtDescripcion.Text;

            objRespuesta = obj_Neg_Producto.UpdateProducto(obj_Producto);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool EliminarProducto(int id_producto)
        {
            objRespuesta = obj_Neg_Producto.DeleteProducto(id_producto);
            return objRespuesta.BoolRespuesta;
        }

        protected void LimpiarPanel()
        {
            ErrorMessage.Text = string.Empty;
            ErrorPrincipal.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        protected void Llenar_ddlCategoria()
        {
            var dt = new DataTable();
            CN_CategoriaProductos objCategoria = new CN_CategoriaProductos();
            objRespuesta = objCategoria.SelectCategorias();
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_categoria.DataTextField = dt.Columns["nombre"].ToString();
                ddl_categoria.DataValueField = dt.Columns["id_categoria"].ToString();
                ddl_categoria.DataSource = dt;
                ddl_categoria.DataBind();
            }
            ddl_categoria.Items.Insert(0, new ListItem("Seleccione Categoria", "NA"));
        }

        protected void Llenar_ddlVehiculo()
        {
            var dt = new DataTable();
            CN_Vehiculos objVehiculo = new CN_Vehiculos();
            objRespuesta = objVehiculo.SelectVehiculos(true);
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_vehiculo.DataTextField = dt.Columns["vehiculo"].ToString();
                ddl_vehiculo.DataValueField = dt.Columns["id_vehiculo"].ToString();
                ddl_vehiculo.DataSource = dt;
                ddl_vehiculo.DataBind();
            }
            ddl_vehiculo.Items.Insert(0, new ListItem("Seleccione Vehiculo", "NA"));
        }

        #endregion
    }
}