using System;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using System.Data;
using Capa_Objetos.General;

namespace Repuestos.Catalogos
{
    public partial class Marcas : System.Web.UI.Page
    {
        CN_Marcas obj_Negocio_Marcas = new CN_Marcas();
        CO_Marcas obj_Marcas = new CO_Marcas();
        CO_Respuesta objRespueta = new CO_Respuesta();

        #region Funciones del formulario
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvMarcas();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_marca = 0;

            if (Session["IDMarca"] != null)
            {
                id_marca = (Int32)Session["IDMarca"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarMarca(id_marca))
                    {
                        Llenar_gvMarcas();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar marca";
                    }

                    break;
                case "Guardar":
                    if (GuardarMarca())
                    {
                        LimpiarPanel();
                        Llenar_gvMarcas();
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al almacenar los datos - "+objRespueta.MensajeRespuesta;
                    }
                    break;

                default:
                    break;
            }
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvMarcas.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_marca = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDMarca", id_marca);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_marca);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":
                        EliminarMarca(id_marca);
                        Llenar_gvMarcas();
                        break;

                    default:
                        break;
                }
            }
        }

        protected void gvMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMarcas.PageIndex = e.NewPageIndex;
            Llenar_gvMarcas();
        }

        #endregion

        #region Funciones

        protected void Llenar_gvMarcas()
        {
            var miTabla = new DataTable();
            miTabla = obj_Negocio_Marcas.SelectMarcas();
            gvMarcas.DataSource = miTabla;
            gvMarcas.DataBind();
        }

        protected void MostrarDatos(int id_marca)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            tabla_datos = obj_Negocio_Marcas.SelectMarcas(id_marca);
            var row = tabla_datos.Rows[0];

            txtMarca.Text = row["marca"].ToString();
            txtDescripcion.Text = row["descripcion"].ToString();

        }

        protected bool GuardarMarca()
        {
            bool respuesta = false;
            obj_Marcas.Marca = txtMarca.Text;
            obj_Marcas.Descripcion = txtDescripcion.Text;

            objRespueta = obj_Negocio_Marcas.InsertMarca(obj_Marcas);
            respuesta = objRespueta.BoolRespuesta;

            return respuesta;
        }

        protected bool ActualizarMarca(int id_marca)
        {
            var respuesta = false;
            obj_Marcas.Id_Marca = id_marca;
            obj_Marcas.Marca = txtMarca.Text;
            obj_Marcas.Descripcion = txtDescripcion.Text;

            respuesta = obj_Negocio_Marcas.UpdateMarca(obj_Marcas);

            return respuesta;
        }

        protected void EliminarMarca(int id_marca)
        {
            obj_Negocio_Marcas.DeleteMarca(id_marca);
        }

        protected void LimpiarPanel()
        {
            ErrorMessage.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        #endregion

    }
}