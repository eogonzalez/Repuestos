using System;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos;
using Capa_Objetos.Catalogos;
using System.Data;
using Capa_Objetos.General;

namespace Repuestos.Catalogos
{
    public partial class Clientes : System.Web.UI.Page
    {
        CN_Clientes objClientes = new CN_Clientes();
        CO_Clientes objCO_Clientes = new CO_Clientes();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario

        //Funcion en todas los formularios
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Llamado a la funcion para llenar el gridview
                Llenar_gvClientes();
            }
        }

        //funcion para la accion del click de guardar del formulario
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            int id_cliente = 0;

            if (Session["IDCliente"] != null)
            {
                id_cliente = (Int32)Session["IDCliente"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarCliente(id_cliente))
                    {
                        Llenar_gvClientes();
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar cliente - "+objRespuesta.MensajeRespuesta;
                    }

                    break;
                case "Guardar":
                    if (GuardarCliente())
                    {
                        LimpiarPanel();
                        Llenar_gvClientes();
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

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvClientes.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_cliente = Convert.ToInt32(row.Cells[0].Text);

                Session.Add("IDCliente", id_cliente);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_cliente);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":
                        if (EliminarCliente(id_cliente))
                        {
                            Llenar_gvClientes();
                        }
                        else
                        {
                            ErrorPrincipal.Text = objRespuesta.MensajeRespuesta;
                        }
                        
                        
                        break;
                    case "vehiculos":
                        Response.Redirect("~/Catalogos/VehiculosClientes.aspx?idc=" + id_cliente);
                        break;
                    default:
                        break;
                }
            }
        }

        protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClientes.PageIndex = e.NewPageIndex;
            Llenar_gvClientes();
        }

        #endregion

        #region Funciones 

        //Funcion para llenar GridView
        protected void Llenar_gvClientes()
        {
            var miTabla = new DataTable();
            objRespuesta = objClientes.SelectClientes();
            miTabla = objRespuesta.DataTableRespuesta;

            //Establecer valores al grid view
            gvClientes.DataSource = miTabla;
            gvClientes.DataBind();
        }

        protected void MostrarDatos(int id_cliente)
        { 
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            objRespuesta = objClientes.SelectClientes(id_cliente);
            tabla_datos = objRespuesta.DataTableRespuesta;

            var row = tabla_datos.Rows[0];

            txtNit.Text = row["nit"].ToString();
            txtNombres.Text = row["nombres"].ToString();
            txtDireccion.Text = row["direccion"].ToString();
            txtTelefono.Text = row["telefono"].ToString();
            txtCorreo.Text = row["correo"].ToString();
            
        }

        protected bool GuardarCliente()
        {
            bool respuesta = false;
            objCO_Clientes.Nit = txtNit.Text;
            objCO_Clientes.Nombres = txtNombres.Text;
            objCO_Clientes.Direccion = txtDireccion.Text;
            objCO_Clientes.Telefono = txtTelefono.Text;
            objCO_Clientes.Correo = txtCorreo.Text;

            objRespuesta = objClientes.GuardarFormulario(objCO_Clientes);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool ActualizarCliente(int id_cliente)
        {
            var respuesta = false;
            objCO_Clientes.ID_Cliente = id_cliente;
            objCO_Clientes.Nit = txtNit.Text;
            objCO_Clientes.Nombres = txtNombres.Text;
            objCO_Clientes.Direccion = txtDireccion.Text;
            objCO_Clientes.Telefono = txtTelefono.Text;
            objCO_Clientes.Correo = txtCorreo.Text;

            objRespuesta = objClientes.UpdateCliente(objCO_Clientes);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool EliminarCliente(int id_cliente)
        {
            objRespuesta = objClientes.DeleteCliente(id_cliente);
            return objRespuesta.BoolRespuesta;
        }

        protected void LimpiarPanel()
        {
            ErrorMessage.Text = string.Empty;
            ErrorPrincipal.Text = string.Empty;
            txtNit.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCorreo.Text = string.Empty;
        }

        #endregion


    }

}