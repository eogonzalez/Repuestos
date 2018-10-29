using System;
using System.Data;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos;
using Capa_Objetos.Catalogos;
using Capa_Negocio.Catalogos.Vehiculos;
using Capa_Objetos.General;

namespace Repuestos.Catalogos
{
    public partial class VehiculosClientes : System.Web.UI.Page
    {
        CN_VehiculosClientes obj_Negocio_VehiculoClientes = new CN_VehiculosClientes();
        CO_VehiculosClientes objVehiculosClientes = new CO_VehiculosClientes();
        CO_Respuesta objRespuesta = new CO_Respuesta();

        #region Funciones del formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idc"] != null)
                {
                    var id_cliente = Convert.ToInt32(Request.QueryString["idc"]);
                    Session.Add("IDCliente", id_cliente);

                    Llenar_gvVehiculoCliente(id_cliente);
                    Llenar_ddlCliente();

                    ddl_cliente.SelectedValue = id_cliente.ToString();

                    Llenar_ddlVehiculo();
                }
            }
        }

        protected void gvVehiculosClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {//Cuando no haga click en cambiar de pagina
                //Convierto en  entero  lo que venga  en e.commandargument
                int index = Convert.ToInt32(e.CommandArgument);

                //Defino una variable de tipo  fila del  grid  
                //y le  asigno el  numero de   fila que obtengo  de la variable index
                GridViewRow row = gvVehiculosClientes.Rows[index];

                //Obtengo  en un numero  entero el  id del  registro que deseo modificar
                int id_vehiculo_cliente = Convert.ToInt32(row.Cells[0].Text);
                int id_cliente = Convert.ToInt32(Session["IDCliente"].ToString());

                Session.Add("IDVehiculoCliente", id_vehiculo_cliente);

                switch (e.CommandName)
                {
                    case "modificar":
                        MostrarDatos(id_vehiculo_cliente);
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        break;

                    case "eliminar":
                        if (EliminarVehiculoCliente(id_vehiculo_cliente))
                        {
                            Llenar_gvVehiculoCliente(id_cliente);
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

        protected void gvVehiculosClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVehiculosClientes.PageIndex = e.NewPageIndex;
            int id_cliente = 0;

            if (Session["IDCliente"] != null)
            {
                id_cliente = (Int32)Session["IDCliente"];
            }

            Llenar_gvVehiculoCliente(id_cliente);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_vehiculo_cliente = 0;
            int id_cliente = 0;

            if (Session["IDVehiculoCliente"] != null)
            {
                id_vehiculo_cliente = (Int32)Session["IDVehiculoCliente"];
            }

            if (Session["IDCliente"] != null)
            {
                id_cliente = (Int32)Session["IDCliente"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":

                    if (ActualizarVehiculoCliente(id_vehiculo_cliente))
                    {
                        Llenar_gvVehiculoCliente(id_cliente);
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar vehiculo del cliente - "+objRespuesta.MensajeRespuesta;
                    }

                    break;
                case "Guardar":
                    if (GuardarVehiculoCliente())
                    {
                        LimpiarPanel();
                        Llenar_gvVehiculoCliente(id_cliente);
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

        protected void lkBtn_Regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Catalogos/Clientes.aspx");
        }

        #endregion

        #region Funciones

        protected void Llenar_gvVehiculoCliente(int id_cliente)
        {
            var miTabla = new DataTable();
            objRespuesta = obj_Negocio_VehiculoClientes.SelectVehiculosClientes(id_cliente);
            miTabla = objRespuesta.DataTableRespuesta;
            gvVehiculosClientes.DataSource = miTabla;
            gvVehiculosClientes.DataBind();
        }

        protected void MostrarDatos(int id_vehiculoCliente)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tabla_datos = new DataTable();
            objRespuesta = obj_Negocio_VehiculoClientes.SelectVehiculoClienteDetalle(id_vehiculoCliente);
            tabla_datos = objRespuesta.DataTableRespuesta;
            var row = tabla_datos.Rows[0];

            ddl_cliente.SelectedValue = row["id_cliente"].ToString();           
            ddl_vehiculo.SelectedValue = row["id_vehiculo"].ToString();
            
            txtPlaca.Text = row["placa"].ToString();
            txtColor.Text = row["color"].ToString();
            txtKilometraje.Text = row["kilometraje"].ToString();
        }

        protected bool GuardarVehiculoCliente()
        {
            bool respuesta = false;
            objVehiculosClientes.Id_Cliente = Convert.ToInt32(ddl_cliente.SelectedValue.ToString());
            objVehiculosClientes.Id_Vehiculo = Convert.ToInt32(ddl_vehiculo.SelectedValue.ToString());
            objVehiculosClientes.Placa = txtPlaca.Text;
            objVehiculosClientes.Color= txtColor.Text;
            objVehiculosClientes.Kilometraje= Convert.ToInt32(txtKilometraje.Text);

            objRespuesta = obj_Negocio_VehiculoClientes.InsertVehiculoCliente(objVehiculosClientes);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool ActualizarVehiculoCliente(int id_vehiculoCliente)
        {
            var respuesta = false;
            objVehiculosClientes.Id_Vehiculo_Cliente = id_vehiculoCliente;
            objVehiculosClientes.Id_Vehiculo = Convert.ToInt32(ddl_vehiculo.SelectedValue.ToString());
            objVehiculosClientes.Placa = txtPlaca.Text;
            objVehiculosClientes.Color = txtColor.Text;
            objVehiculosClientes.Kilometraje = Convert.ToInt32(txtKilometraje.Text);            

            objRespuesta = obj_Negocio_VehiculoClientes.UpdateVehiculoCliente(objVehiculosClientes);
            respuesta = objRespuesta.BoolRespuesta;

            return respuesta;
        }

        protected bool EliminarVehiculoCliente(int id_vehiculo_cliente)
        {
            objRespuesta = obj_Negocio_VehiculoClientes.DeleteVehiculoCliente(id_vehiculo_cliente);
            return objRespuesta.BoolRespuesta;
        }

        protected void LimpiarPanel()
        {
            ErrorPrincipal.Text = string.Empty;
            ErrorMessage.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtKilometraje.Text = string.Empty;
        }

        protected void Llenar_ddlCliente()
        {
            var dt = new DataTable();
            CN_Clientes objCliente = new CN_Clientes();
            objRespuesta = objCliente.SelectClientes();
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_cliente.DataTextField = dt.Columns["nombres"].ToString();
                ddl_cliente.DataValueField = dt.Columns["id_cliente"].ToString();
                ddl_cliente.DataSource = dt;
                ddl_cliente.DataBind();
            }
            ddl_cliente.Items.Insert(0, new ListItem("Seleccione Cliente", "NA"));
        }

        protected void Llenar_ddlVehiculo()
        {
            var dt = new DataTable();
            CN_Vehiculos objVehiculo = new CN_Vehiculos();
            objRespuesta = objVehiculo.SelectVehiculos(true);
            dt = objRespuesta.DataTableRespuesta;

            if (dt.Rows.Count > 0)
            {
                ddl_vehiculo.DataTextField = dt.Columns["vehiculo"].ToString();
                ddl_vehiculo.DataValueField = dt.Columns["id_vehiculo"].ToString();
                ddl_vehiculo.DataSource = dt;
                ddl_vehiculo.DataBind();
            }
            ddl_vehiculo.Items.Insert(0, new ListItem("Seleccione Vehiculo", "NA"));
        }

        #endregion

    }
}