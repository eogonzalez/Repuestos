using System;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using System.Data;
using Capa_Objetos.General;

namespace Repuestos.Catalogos.Vehiculos
{
    public partial class Vehiculos : System.Web.UI.Page
    {
        CN_Vehiculos obj_Negocio_Vehiculos = new CN_Vehiculos();
        CO_Vehiculos obj_Vehiculos = new CO_Vehiculos();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvVehiculos();
                Llenar_ddlMarca();
                Llenar_ddlModelo();
                Llenar_ddlLinea();
                Llenar_ddlTipoVehiculo();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_vehiculo = 0;

            if (Session["IDVehiculo"] != null)
            {
                id_vehiculo = (Int32)Session["IDVehiculo"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarVehiculo(id_vehiculo))
                    {
                        Llenar_gvVehiculos();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar vehiculo - "+objRespuesta.MensajeRespuesta;
                    }

                    break;
                case "Guardar":
                    if (GuardarVehiculo())
                    {
                        LimpiarPanel();
                        Llenar_gvVehiculos();
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

        protected void gvVehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvVehiculos.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_vehiculo = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDVehiculo", id_vehiculo);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_vehiculo);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":
                                                
                        if (EliminarVehiculo(id_vehiculo))
                        {
                            Llenar_gvVehiculos();
                        }else
                        {
                            ErrorPrincipal.Text = objRespuesta.MensajeRespuesta;
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        protected void gvVehiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehiculos.PageIndex = e.NewPageIndex;
            Llenar_gvVehiculos();
        }

        protected void ddl_marca_SelectedIndexChanged(object sender, EventArgs e)
        {            
            var id_marca = Convert.ToInt32(ddl_marca.SelectedValue.ToString());

            ddl_linea.Enabled = true;
            ddl_modelo.Enabled = false;
            ddl_tipo_vehiculo.Enabled = false;

            Llenar_ddlLinea(id_marca);
            Llenar_ddlModelo();
            Llenar_ddlTipoVehiculo();            
            lkBtn_viewPanel_ModalPopupExtender.Show();
        }

        protected void ddl_linea_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var id_marca = Convert.ToInt32(ddl_marca.SelectedValue.ToString());
            var id_linea = Convert.ToInt32(ddl_linea.SelectedValue.ToString());
            ddl_modelo.Enabled = true;
            ddl_tipo_vehiculo.Enabled = true;

            Llenar_ddlModelo(id_linea);
            Llenar_ddlTipoVehiculo();
            lkBtn_viewPanel_ModalPopupExtender.Show();
        }

        #endregion

        #region Funciones

        protected void Llenar_gvVehiculos()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_Vehiculos.SelectVehiculos();
            miTabla = objRespuesta.DataTableRespuesta;
            gvVehiculos.DataSource = miTabla;
            gvVehiculos.DataBind();
        }

        protected void MostrarDatos(int id_vehiculo)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            objRespuesta = obj_Negocio_Vehiculos.SelectVehiculos(id_vehiculo);
            tabla_datos = objRespuesta.DataTableRespuesta;
            var row = tabla_datos.Rows[0];

            ddl_marca.SelectedValue = row["id_marca"].ToString();

            Llenar_ddlLinea(Convert.ToInt32(ddl_marca.SelectedValue.ToString()));
            ddl_linea.SelectedValue = row["id_linea"].ToString();
            ddl_linea.Enabled = true;

            Llenar_ddlModelo(Convert.ToInt32(ddl_linea.SelectedValue.ToString()));
            ddl_modelo.SelectedValue = row["id_modelo"].ToString();
            ddl_modelo.Enabled = true;
            
            ddl_tipo_vehiculo.SelectedValue = row["id_tipo_vehiculo"].ToString();
            ddl_tipo_vehiculo.Enabled = true;
            txtDescripcion.Text = row["descripcion"].ToString();
        }

        protected bool GuardarVehiculo()
        {
            bool respuesta = false;
            obj_Vehiculos.Id_Marca = Convert.ToInt32(ddl_marca.SelectedValue.ToString());
            obj_Vehiculos.Id_Modelo = Convert.ToInt32(ddl_modelo.SelectedValue.ToString());
            obj_Vehiculos.Id_Linea = Convert.ToInt32(ddl_linea.SelectedValue.ToString());
            obj_Vehiculos.Id_Tipo_Vehiculo = Convert.ToInt32(ddl_tipo_vehiculo.SelectedValue.ToString());
            obj_Vehiculos.Descripcion = txtDescripcion.Text;

            objRespuesta = obj_Negocio_Vehiculos.InsertVehiculo(obj_Vehiculos);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool ActualizarVehiculo(int id_vehiculo)
        {
            var respuesta = false;
            obj_Vehiculos.Id_Vehiculo = id_vehiculo;            
            obj_Vehiculos.Id_Marca = Convert.ToInt32(ddl_marca.SelectedValue.ToString());
            obj_Vehiculos.Id_Modelo = Convert.ToInt32(ddl_modelo.SelectedValue.ToString());
            obj_Vehiculos.Id_Linea = Convert.ToInt32(ddl_linea.SelectedValue.ToString()) ;
            obj_Vehiculos.Id_Tipo_Vehiculo = Convert.ToInt32(ddl_tipo_vehiculo.SelectedValue.ToString());
            obj_Vehiculos.Descripcion = txtDescripcion.Text;

            objRespuesta = obj_Negocio_Vehiculos.UpdateVehiculo(obj_Vehiculos);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool EliminarVehiculo(int id_vehiculo)
        {
            objRespuesta = obj_Negocio_Vehiculos.DeleteVehiculo(id_vehiculo);
            return objRespuesta.BoolRespuesta;
        }

        protected void LimpiarPanel()
        {
            ErrorPrincipal.Text = string.Empty;
            ErrorMessage.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        protected void Llenar_ddlMarca()
        {
            var dt = new DataTable();
            CN_Marcas objMarca = new CN_Marcas();
            objRespuesta = objMarca.SelectMarcas();
            dt = objRespuesta.DataTableRespuesta;
                       
            if (dt.Rows.Count > 0)
            {
                ddl_marca.DataTextField = dt.Columns["marca"].ToString();
                ddl_marca.DataValueField = dt.Columns["id_marca"].ToString();
                ddl_marca.DataSource = dt;
                ddl_marca.DataBind();
            }
            ddl_marca.Items.Insert(0, new ListItem("Seleccione Marca", "NA"));
        }

        protected void Llenar_ddlModelo()
        {
            var dt = new DataTable();
            CN_Modelos objModelo = new CN_Modelos();
            objRespuesta = objModelo.SelectModelos();
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_modelo.DataTextField = dt.Columns["modelo"].ToString();
                ddl_modelo.DataValueField = dt.Columns["id_modelo"].ToString();
                ddl_modelo.DataSource = dt;
                ddl_modelo.DataBind();
            }
            ddl_modelo.Items.Insert(0, new ListItem("Seleccione Modelo", "NA"));
        }

        protected void Llenar_ddlModelo(int id_linea)
        {
            var dt = new DataTable();
            CN_Modelos objModelo = new CN_Modelos();
            objRespuesta = objModelo.SelectModelos(id_linea);
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_modelo.DataTextField = dt.Columns["modelo"].ToString();
                ddl_modelo.DataValueField = dt.Columns["id_modelo"].ToString();
                ddl_modelo.DataSource = dt;
                ddl_modelo.DataBind();
            }
            ddl_modelo.Items.Insert(0, new ListItem("Seleccione Modelo", "NA"));
        }

        protected void Llenar_ddlLinea()
        {
            var dt = new DataTable();
            CN_Lineas objLinea = new CN_Lineas();
            objRespuesta = objLinea.SelectLineas(0);
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_linea.DataTextField = dt.Columns["linea"].ToString();
                ddl_linea.DataValueField = dt.Columns["id_linea"].ToString();
                ddl_linea.DataSource = dt;
                ddl_linea.DataBind();
            }
            ddl_linea.Items.Insert(0, new ListItem("Seleccione Linea", "NA"));
        }

        protected void Llenar_ddlLinea(int id_marca)
        {
            var dt = new DataTable();
            CN_Lineas objLinea = new CN_Lineas();
            objRespuesta = objLinea.SelectLineas(id_marca);
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_linea.DataTextField = dt.Columns["linea"].ToString();
                ddl_linea.DataValueField = dt.Columns["id_linea"].ToString();
                ddl_linea.DataSource = dt;
                ddl_linea.DataBind();
            }
            ddl_linea.Items.Insert(0, new ListItem("Seleccione Linea", "NA"));
        }

        protected void Llenar_ddlTipoVehiculo()
        {
            var dt = new DataTable();
            CN_TipoVehiculos objTipoVehiculos = new CN_TipoVehiculos();
            objRespuesta = objTipoVehiculos.SelectTipoVehiculos();
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_tipo_vehiculo.DataTextField = dt.Columns["tipo"].ToString();
                ddl_tipo_vehiculo.DataValueField = dt.Columns["id_tipo_vehiculo"].ToString();
                ddl_tipo_vehiculo.DataSource = dt;
                ddl_tipo_vehiculo.DataBind();
            }
            ddl_tipo_vehiculo.Items.Insert(0, new ListItem("Seleccione Tipo", "NA"));
        }

        #endregion

    }
}