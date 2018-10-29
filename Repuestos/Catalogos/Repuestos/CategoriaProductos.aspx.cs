using System;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos.Repuestos;
using Capa_Objetos.Catalogos.Repuestos;
using System.Data;
using Capa_Objetos.General;

namespace Repuestos.Inventario
{
    public partial class CategoriaProducto : System.Web.UI.Page
    {
        CN_CategoriaProductos obj_Negocio_Categoria = new CN_CategoriaProductos();
        CO_CategoriaProductos objCategoria = new CO_CategoriaProductos();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvCategorias();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_categoria = 0;

            if (Session["IDCategoria"] != null)
            {
                id_categoria = (Int32)Session["IDCategoria"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarCategoria(id_categoria))
                    {
                        Llenar_gvCategorias();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar categoria - "+objRespuesta.MensajeRespuesta;
                    }

                    break;
                case "Guardar":
                    if (GuardarCategoria())
                    {
                        LimpiarPanel();
                        Llenar_gvCategorias();
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

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvCategorias.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_categoria = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDCategoria", id_categoria);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_categoria);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":                                                
                        if (EliminarCategoria(id_categoria))
                        {
                            Llenar_gvCategorias();
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

        protected void gvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategorias.PageIndex = e.NewPageIndex;
            Llenar_gvCategorias();
        }

        #endregion

        #region Funciones

        protected void Llenar_gvCategorias()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_Categoria.SelectCategorias();
            miTabla = objRespuesta.DataTableRespuesta;
            gvCategorias.DataSource = miTabla;
            gvCategorias.DataBind();
        }

        protected void MostrarDatos(int id_categoria)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            objRespuesta = obj_Negocio_Categoria.SelectCategorias(id_categoria);
            tabla_datos = objRespuesta.DataTableRespuesta;
            var row = tabla_datos.Rows[0];

            txtCategoria.Text = row["nombre"].ToString();
            txtDescripcion.Text = row["descripcion"].ToString();

        }

        protected bool GuardarCategoria()
        {
            bool respuesta = false;
            objCategoria.Categoria = txtCategoria.Text;
            objCategoria.Descripcion = txtDescripcion.Text;

            objRespuesta = obj_Negocio_Categoria.InsertCategoria(objCategoria);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool ActualizarCategoria(int id_categoria)
        {
            var respuesta = false;
            objCategoria.Id_Categoria = id_categoria;
            objCategoria.Categoria = txtCategoria.Text;
            objCategoria.Descripcion = txtDescripcion.Text;

            objRespuesta = obj_Negocio_Categoria.UpdateCategoria(objCategoria);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool EliminarCategoria(int id_categoria)
        {
            objRespuesta = obj_Negocio_Categoria.DeleteCategoria(id_categoria);
            return objRespuesta.BoolRespuesta;
        }

        protected void LimpiarPanel()
        {
            ErrorPrincipal.Text = string.Empty;
            ErrorMessage.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        #endregion
    }
}