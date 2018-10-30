using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Negocio.Administracion.Servicios;
using Capa_Objetos.Administracion.Servicios;
using Capa_Objetos.General;
using System.Data;

namespace Repuestos.Administracion.Servicios
{
    public partial class TipoServicio : System.Web.UI.Page
    {
        CN_Tipo_Servicio obj_Negocio_TipoServicio = new CN_Tipo_Servicio();
        CO_Tipo_Servicio objTipoServicio = new CO_Tipo_Servicio();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvTipoServicio();
            }
        }

        protected void gvTipoServicio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvTipoServicio.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_tipo_servicio = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDTipoServicio", id_tipo_servicio);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_tipo_servicio);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":

                        if (EliminarTipoServicio(id_tipo_servicio))
                        {
                            Llenar_gvTipoServicio();
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

        protected void gvTipoServicio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTipoServicio.PageIndex = e.NewPageIndex;
            Llenar_gvTipoServicio();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_tipo_servicio = 0;

            if (Session["IDTipoServicio"] != null)
            {
                id_tipo_servicio = (Int32)Session["IDTipoServicio"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarTipoServicio(id_tipo_servicio))
                    {
                        Llenar_gvTipoServicio();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar marca - " + objRespuesta.MensajeRespuesta;
                    }

                    break;
                case "Guardar":
                    if (GuardarTipoServicio())
                    {
                        LimpiarPanel();
                        Llenar_gvTipoServicio();
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al almacenar los datos - " + objRespuesta.MensajeRespuesta;
                    }
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Funciones

        protected void Llenar_gvTipoServicio()
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_TipoServicio.SelectTipoServicio();
            miTabla = objRespuesta.DataTableRespuesta;
            gvTipoServicio.DataSource = miTabla;
            gvTipoServicio.DataBind();
        }

        protected void MostrarDatos(int id_tipo_servicio)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            objRespuesta = obj_Negocio_TipoServicio.SelectTipoServicio(id_tipo_servicio);
            tabla_datos = objRespuesta.DataTableRespuesta;
            var row = tabla_datos.Rows[0];

            txtTipoServicio.Text = row["tipo_servicio"].ToString();
            txtDescripcion.Text = row["descripcion"].ToString();
            txtCosto.Text = row["costo"].ToString();
            txtPorcentaje.Text = row["porcentaje_ganancia"].ToString();

        }

        protected bool GuardarTipoServicio()
        {
            bool respuesta = false;
            objTipoServicio.TipoServicio = txtTipoServicio.Text;
            objTipoServicio.Descripcion = txtDescripcion.Text;
            objTipoServicio.Costo = Convert.ToDecimal(txtCosto.Text);
            objTipoServicio.Porcentaje_Ganancia = Convert.ToDecimal(txtPorcentaje.Text);

            objRespuesta = obj_Negocio_TipoServicio.InsertTipoServicio(objTipoServicio);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool ActualizarTipoServicio(int id_tipo_servicio)
        {
            var respuesta = false;
            objTipoServicio.Id_Tipo_Servicio = id_tipo_servicio;
            objTipoServicio.TipoServicio = txtTipoServicio.Text;
            objTipoServicio.Descripcion = txtDescripcion.Text;
            objTipoServicio.Costo = Convert.ToDecimal(txtCosto.Text);
            objTipoServicio.Porcentaje_Ganancia = Convert.ToDecimal(txtPorcentaje.Text);

            objRespuesta = obj_Negocio_TipoServicio.UpdateTipoServicio(objTipoServicio);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool EliminarTipoServicio(int id_tipo_servicio)
        {
            objRespuesta = obj_Negocio_TipoServicio.DeleteTipoServicio(id_tipo_servicio);
            return objRespuesta.BoolRespuesta;
        }

        protected void LimpiarPanel()
        {
            ErrorPrincipal.Text = string.Empty;
            ErrorMessage.Text = string.Empty;
            txtTipoServicio.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtCosto.Text = string.Empty;
            txtPorcentaje.Text = string.Empty;
        }

        #endregion
    }
}