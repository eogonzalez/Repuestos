using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Negocio.Catalogos;
using Capa_Objetos.Catalogos;
using System.Data;

namespace Repuestos.Catalogos
{
    public partial class Clientes : System.Web.UI.Page
    {
        CN_Clientes objClientes = new CN_Clientes();
        CO_Clientes objCO_Clientes = new CO_Clientes();

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
            //Capturar los valores
            objCO_Clientes.Nit = txtNit.Text;
            objCO_Clientes.Nombres = txtNombres.Text;
            objCO_Clientes.Direccion = txtDireccion.Text;
            objCO_Clientes.Telefono = txtTelefono.Text;
            objCO_Clientes.Correo = txtCorreo.Text;

            //Enviarlos a guardar
            Boolean guardo = objClientes.GuardarFormulario(objCO_Clientes);

            if (guardo)
            {
                Llenar_gvProveedores();
            }
            else
            {
                lkBtn_viewPanel_ModalPopupExtender.Show();
                ErrorMessage.Text = "Ha Ocurrido un Error al almacenar los datos";
            }

        }

        #endregion


        #region Funciones 

        //Funcion para llenar GridView
        protected void Llenar_gvProveedores()
        {
            var miTabla = new DataTable();

            miTabla = objClientes.SelectClientes();

            //Establecer valores al grid view
            gvClientes.DataSource = miTabla;
            gvClientes.DataBind();
        }



        #endregion


    }

}