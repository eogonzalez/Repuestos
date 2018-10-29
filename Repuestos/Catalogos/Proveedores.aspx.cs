using System;
using System.Data;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos;
using Capa_Objetos.Catalogos;
using Capa_Objetos.General;

namespace Repuestos.Catalogos
{
    public partial class Proveedores : System.Web.UI.Page
    {

        CN_Proveedores objProveedores = new CN_Proveedores();
        CO_Proveedores objCO_Proveedores = new CO_Proveedores();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario

        //Funcion en todas los formularios
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Llamado a la funcion para llenar el gridview
                Llenar_gvProveedores();
            }
        }

        //funcion para la accion del click de guardar del formulario
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_proveedor = 0;

            if (Session["IDProveedor"] != null)
            {
                id_proveedor = (Int32)Session["IDProveedor"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarProveedor(id_proveedor))
                    {//Es Verdadero
                        Llenar_gvProveedores();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {//Es Falso
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar proveedor."+objRespuesta.MensajeRespuesta;
                    }

                    break;

                case "Guardar":

                    if (GuardarProveedor())
                    {
                        LimpiarPanel();
                        Llenar_gvProveedores();
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha Ocurrido un Error al almacenar los datos - "+objRespuesta.MensajeRespuesta;
                    }

                    break;

                default:
                    break;
            }           
        }

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvProveedores.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_proveedor = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDProveedor", id_proveedor);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_proveedor);
                        lkBtn_viewPanel_ModalPopupExtender.Show();                        
                        break;

                    case "eliminar":
                        if (EliminarProveedor(id_proveedor))
                        {
                            Llenar_gvProveedores();
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

        protected void gvProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProveedores.PageIndex = e.NewPageIndex;
            Llenar_gvProveedores();
        }

        #endregion

        #region Funciones 

        protected void Llenar_gvProveedores()
        {
            var miTabla = new DataTable();
            objRespuesta = objProveedores.SelectProveedores();
            miTabla = objRespuesta.DataTableRespuesta;

            //Establecer valores al grid view
            gvProveedores.DataSource = miTabla;
            gvProveedores.DataBind();
        }

        protected void MostrarDatos(int id_provedor)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            objRespuesta = objProveedores.SelectProveedores(id_provedor);
            tabla_datos = objRespuesta.DataTableRespuesta;
            var row = tabla_datos.Rows[0];

            //Asigno valores de tabla de datos a mis elementos del formulario
            txtNit.Text = row["nit"].ToString();
            txtNombre.Text = row["nombre_proveedor"].ToString();
            txtDireccion.Text = row["direccion"].ToString();
            txtTelefono.Text = row["telefono"].ToString();
            txtCorreo.Text = row["correo"].ToString();

        }

        protected bool GuardarProveedor()
        {

            Boolean respuesta = false;

            //Capturar los valores
            objCO_Proveedores.Nit = txtNit.Text;
            objCO_Proveedores.Nombre_Proveedor = txtNombre.Text;
            objCO_Proveedores.Direccion = txtDireccion.Text;
            objCO_Proveedores.Telefono = txtTelefono.Text;
            objCO_Proveedores.Correo = txtCorreo.Text;

            //Enviarlos a guardar
            objRespuesta =  objProveedores.GuardarFormulario(objCO_Proveedores);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool ActualizarProveedor(int id_proveedor)
        {
            Boolean respuesta = false;

            //Capturar los valores
            objCO_Proveedores.ID_Proveedor = id_proveedor;
            objCO_Proveedores.Nit = txtNit.Text;
            objCO_Proveedores.Nombre_Proveedor = txtNombre.Text;
            objCO_Proveedores.Direccion = txtDireccion.Text;
            objCO_Proveedores.Telefono = txtTelefono.Text;
            objCO_Proveedores.Correo = txtCorreo.Text;

            objRespuesta = objProveedores.ActualizarProveedor(objCO_Proveedores);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool EliminarProveedor(int id_proveedor)
        {
            objRespuesta = objProveedores.DeleteProveedor(id_proveedor);
            return objRespuesta.BoolRespuesta;
        }

        protected void LimpiarPanel()
        {
            ErrorPrincipal.Text = string.Empty;
            ErrorMessage.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtNit.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }

        #endregion

    }
}