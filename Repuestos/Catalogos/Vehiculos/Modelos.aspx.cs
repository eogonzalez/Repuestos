using System;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos.Vehiculos;
using Capa_Objetos.Catalogos.Vehiculos;
using System.Data;

namespace Repuestos.Catalogos
{
    public partial class Modelos : System.Web.UI.Page
    {
        CN_Modelos obj_Negocio_Modelos = new CN_Modelos();
        CO_Modelos obj_Modelos = new CO_Modelos();

        #region Funciones del formulario
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvModelos();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_modelo = 0;

            if (Session["IDModelo"] != null)
            {
                id_modelo = (Int32)Session["IDModelo"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarModelo(id_modelo))
                    {
                        Llenar_gvModelos();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar modelo";
                    }

                    break;
                case "Guardar":
                    if (GuardarModelo())
                    {
                        LimpiarPanel();
                        Llenar_gvModelos();
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

        protected void gvModelos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvModelos.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_modelo = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDModelo", id_modelo);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_modelo);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":
                        EliminarModelo(id_modelo);
                        Llenar_gvModelos();
                        break;

                    default:
                        break;
                }
            }
        }

        protected void gvModelos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvModelos.PageIndex = e.NewPageIndex;
            Llenar_gvModelos();
        }

        #endregion

        #region Funciones

        protected void Llenar_gvModelos()
        {
            var miTabla = new DataTable();
            miTabla = obj_Negocio_Modelos.SelectModelos();
            gvModelos.DataSource = miTabla;
            gvModelos.DataBind();
        }

        protected void MostrarDatos(int id_modelo)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            tabla_datos = obj_Negocio_Modelos.SelectModelos(id_modelo);
            var row = tabla_datos.Rows[0];

            txtModelo.Text = row["modelo"].ToString();
            
        }

        protected bool GuardarModelo()
        {
            bool respuesta = false;
            obj_Modelos.Modelo = txtModelo.Text;
            
            respuesta = obj_Negocio_Modelos.InsertModelo(obj_Modelos);

            return respuesta;
        }

        protected bool ActualizarModelo(int id_modelo)
        {
            var respuesta = false;
            obj_Modelos.Id_Modelo = id_modelo;
            obj_Modelos.Modelo = txtModelo.Text;
            
            respuesta = obj_Negocio_Modelos.UpdateModelo(obj_Modelos);

            return respuesta;
        }

        protected void EliminarModelo(int id_modelo)
        {
            obj_Negocio_Modelos.DeleteModelo(id_modelo);
        }

        protected void LimpiarPanel()
        {
            txtModelo.Text = string.Empty;            
        }

        #endregion
    }
}