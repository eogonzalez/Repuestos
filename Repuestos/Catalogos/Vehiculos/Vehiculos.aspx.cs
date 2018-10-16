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
    public partial class Vehiculos : System.Web.UI.Page
    {
        CN_Vehiculos obj_Negocio_Vehiculos = new CN_Vehiculos();
        CO_Vehiculos obj_Vehiculos = new CO_Vehiculos();

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

        protected void gvVehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvVehiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Funciones
        protected void Llenar_gvVehiculos()
        {
            var miTabla = new DataTable();
            miTabla = obj_Negocio_Vehiculos.SelectVehiculos();
            gvVehiculos.DataSource = miTabla;
            gvVehiculos.DataBind();
        }

        protected void MostrarDatos(int id_vehiculo)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            tabla_datos = obj_Negocio_Vehiculos.SelectVehiculos(id_vehiculo);
            var row = tabla_datos.Rows[0];

            ddl_marca.SelectedValue = row["id_marca"].ToString();
            ddl_modelo.SelectedValue = row["id_modelo"].ToString();
            ddl_linea.SelectedValue = row["id_linea"].ToString();
            ddl_tipo_vehiculo.SelectedValue = row["id_tipo_vehiculo"].ToString();
            txtDescripcion.Text = row["descripcion"].ToString();
        }

        protected bool GuardarLinea()
        {
            bool respuesta = false;
            obj_Vehiculos.Id_Marca = Convert.ToInt32(ddl_marca.SelectedValue.ToString());
            obj_Vehiculos.Id_Modelo = Convert.ToInt32(ddl_modelo.SelectedValue.ToString());
            obj_Vehiculos.Id_Linea = Convert.ToInt32(ddl_linea.SelectedValue.ToString());
            obj_Vehiculos.Id_Tipo_Vehiculo = Convert.ToInt32(ddl_tipo_vehiculo.ToString());
            obj_Vehiculos.Descripcion = txtDescripcion.Text;

            respuesta = obj_Negocio_Vehiculos.InsertVehiculo(obj_Vehiculos);

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

            respuesta = obj_Negocio_Vehiculos.UpdateVehiculo(obj_Vehiculos);

            return respuesta;
        }

        protected void EliminarVehiculo(int id_vehiculo)
        {
            obj_Negocio_Vehiculos.DeleteVehiculo(id_vehiculo);
        }

        protected void LimpiarPanel()
        {
            txtDescripcion.Text = string.Empty;
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

        protected void Llenar_ddlLinea()
        {
            var dt = new DataTable();
            CN_Lineas objLinea = new CN_Lineas();
            dt = objLinea.SelectLineasCBO();
            if (dt.Rows.Count > 0)
            {
                ddl_linea.DataTextField = dt.Columns["linea"].ToString();
                ddl_linea.DataValueField = dt.Columns["id_linea"].ToString();
                ddl_linea.DataSource = dt;
                ddl_linea.DataBind();
            }
        }

        protected void Llenar_ddlTipoVehiculo()
        {
            var dt = new DataTable();
            CN_TipoVehiculos objTipoVehiculos = new CN_TipoVehiculos();
            dt = objTipoVehiculos.SelectTipoVehiculos();
            if (dt.Rows.Count > 0)
            {
                ddl_tipo_vehiculo.DataTextField = dt.Columns["tipo"].ToString();
                ddl_tipo_vehiculo.DataValueField = dt.Columns["id_tipo_vehiculo"].ToString();
                ddl_tipo_vehiculo.DataSource = dt;
                ddl_tipo_vehiculo.DataBind();
            }
        }

        #endregion
    }
}