using System;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using System.Data;
using Capa_Objetos.General;

namespace Repuestos.Catalogos.Vehiculos
{
    public partial class Lineas : System.Web.UI.Page
    {
        CN_Lineas obj_Negocio_Lineas = new CN_Lineas();
        CO_Lineas obj_Objeto_Lineas = new CO_Lineas();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvLineas();
                Llenar_ddlMarca();
                Llenar_ddlModelo();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_linea = 0;

            if (Session["IDLinea"] != null)
            {
                id_linea = (Int32)Session["IDLinea"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarLinea(id_linea))
                    {
                        Llenar_gvLineas();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar linea";
                    }

                    break;
                case "Guardar":
                    if (GuardarLinea())
                    {
                        LimpiarPanel();
                        Llenar_gvLineas();
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al almacenar los datos";
                    }
                    break;

                default:
                    break;
            }
        }

        protected void gvLineas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvLineas.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_linea = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDLinea", id_linea);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_linea);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":
                        EliminarLinea(id_linea);
                        Llenar_gvLineas();
                        break;

                    default:
                        break;
                }
            }
        }

        protected void gvLineas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLineas.PageIndex = e.NewPageIndex;
            Llenar_gvLineas();
        }

        #endregion

        #region Funciones

        protected void Llenar_gvLineas()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_Lineas.SelectLineas();
            miTabla = objRespuesta.DataTableRespuesta;
            gvLineas.DataSource = miTabla;
            gvLineas.DataBind();
        }

        protected void MostrarDatos(int id_linea)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            tabla_datos = obj_Negocio_Lineas.SelectLineas(id_linea, true);
            var row = tabla_datos.Rows[0];
            
            ddl_marca.SelectedValue = row["id_marca"].ToString();
            ddl_modelo.SelectedValue = row["id_modelo"].ToString();
            txtLinea.Text = row["linea"].ToString();
        }

        protected bool GuardarLinea()
        {
            bool respuesta = false;
            obj_Objeto_Lineas.Id_Marca = Convert.ToInt32(ddl_marca.SelectedValue.ToString());
            obj_Objeto_Lineas.Id_Modelo = Convert.ToInt32(ddl_modelo.SelectedValue.ToString());
            obj_Objeto_Lineas.Linea = txtLinea.Text;

            respuesta = obj_Negocio_Lineas.InsertLinea(obj_Objeto_Lineas);

            return respuesta;
        }

        protected bool ActualizarLinea(int id_linea)
        {
            var respuesta = false;
            obj_Objeto_Lineas.Id_Linea = id_linea;
            obj_Objeto_Lineas.Id_Marca = Convert.ToInt32(ddl_marca.SelectedValue.ToString());
            obj_Objeto_Lineas.Id_Modelo = Convert.ToInt32(ddl_modelo.SelectedValue.ToString());
            obj_Objeto_Lineas.Linea = txtLinea.Text;

            respuesta = obj_Negocio_Lineas.UpdateLinea(obj_Objeto_Lineas);

            return respuesta;
        }

        protected void EliminarLinea(int id_linea)
        {
            obj_Negocio_Lineas.DeleteLinea(id_linea);
        }

        protected void LimpiarPanel()
        {
            txtLinea.Text = string.Empty;
        }

        protected void Llenar_ddlMarca()
        {
            var dt = new DataTable();
            CN_Marcas objMarca = new CN_Marcas();
            dt = objMarca.SelectMarcas();
            if (dt.Rows.Count > 0)
            {
                ddl_marca.DataTextField = dt.Columns["marca"].ToString();
                ddl_marca.DataValueField = dt.Columns["id_marca"].ToString();
                ddl_marca.DataSource = dt;
                ddl_marca.DataBind();                                
            }
        }

        protected void Llenar_ddlModelo()
        {
            var dt = new DataTable();
            CN_Modelos objModelo = new CN_Modelos();
            dt = objModelo.SelectModelos();
            if (dt.Rows.Count > 0)
            {
                ddl_modelo.DataTextField = dt.Columns["modelo"].ToString();
                ddl_modelo.DataValueField = dt.Columns["id_modelo"].ToString();
                ddl_modelo.DataSource = dt;
                ddl_modelo.DataBind();
            }
        }

        #endregion
    }
}