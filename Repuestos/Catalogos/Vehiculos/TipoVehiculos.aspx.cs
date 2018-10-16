using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using System.Data;

namespace Repuestos.Catalogos.Vehiculos
{
    public partial class TipoVehiculos : System.Web.UI.Page
    {

        CN_TipoVehiculos obj_Negocio_TipoVehiculos = new CN_TipoVehiculos();
        CO_TipoVehiculos obj_TipoVehiculos = new CO_TipoVehiculos();

        #region Funciones del formulario
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvTipoVehiculos();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_tipo_vehiculo = 0;

            if (Session["IDTipoVehiculo"] != null)
            {
                id_tipo_vehiculo = (Int32)Session["IDTipoVehiculo"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarTipoVehiculo(id_tipo_vehiculo))
                    {
                        Llenar_gvTipoVehiculos();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar tipo vehiculo";
                    }

                    break;
                case "Guardar":
                    if (GuardarTipoVehiculo())
                    {
                        LimpiarPanel();
                        Llenar_gvTipoVehiculos();
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

        protected void gvTipoVehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvTipoVehiculos.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_tipo_vehiculo = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDTipoVehiculo", id_tipo_vehiculo);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_tipo_vehiculo);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":
                        EliminarTipoVehiculo(id_tipo_vehiculo);
                        Llenar_gvTipoVehiculos();
                        break;

                    default:
                        break;
                }
            }
        }

        protected void gvTipoVehiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }



        #endregion

        #region Funciones

        protected void Llenar_gvTipoVehiculos()
        {
            var miTabla = new DataTable();
            miTabla = obj_Negocio_TipoVehiculos.SelectTipoVehiculos();
            gvTipoVehiculos.DataSource = miTabla;
            gvTipoVehiculos.DataBind();
        }

        protected void MostrarDatos(int id_tipo_vehiculo)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            tabla_datos = obj_Negocio_TipoVehiculos.SelectTipoVehiculos(id_tipo_vehiculo);
            var row = tabla_datos.Rows[0];

            txtTipo.Text = row["tipo"].ToString();

        }

        protected bool GuardarTipoVehiculo()
        {
            bool respuesta = false;
            obj_TipoVehiculos.Tipo = txtTipo.Text;

            respuesta = obj_Negocio_TipoVehiculos.InsertIpoVehiculo(obj_TipoVehiculos);

            return respuesta;
        }

        protected bool ActualizarTipoVehiculo(int id_tipo_vehiculo)
        {
            var respuesta = false;
            obj_TipoVehiculos.Id_Tipo_Vehiculo = id_tipo_vehiculo;
            obj_TipoVehiculos.Tipo = txtTipo.Text;

            respuesta = obj_Negocio_TipoVehiculos.UpdateTipoVehiculo(obj_TipoVehiculos);

            return respuesta;
        }

        protected void EliminarTipoVehiculo(int id_tipo_vehiculo)
        {
            obj_Negocio_TipoVehiculos.DeleteTipoVehiculo(id_tipo_vehiculo);
        }

        protected void LimpiarPanel()
        {
            txtTipo.Text = string.Empty;
        }

        #endregion
    }
}