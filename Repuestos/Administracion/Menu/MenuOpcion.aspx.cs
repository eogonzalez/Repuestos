﻿using Capa_Negocio.Administracion;
using Capa_Objetos.Administracion;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Repuestos.Administracion.Menu
{
    public partial class MenuOpcion : System.Web.UI.Page
    {
        CNMenu objCNMenu = new CNMenu();
        CEMenu objCEMenu = new CEMenu();

        #region Eventos del formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id_menu = 0;
                id_menu = Convert.ToInt32(Request.QueryString["id_om"].ToString());
                Session.Add("id_menu", id_menu);

                Llenar_gvMenu(id_menu);

                //Valores por defecto si es nuevo
                txtOrden.Text = "0";
                cb_obligatorio.Checked = false;
                cb_obligatorio.Visible = true;
                btnGuardar.Attributes.Add("onclick", "this.value='Procesando Espere...';this.disabled=true;" + ClientScript.GetPostBackEventReference(btnGuardar, ""));
            }
        }

        protected void Llenar_gvMenu(int id_menu)
        {
            var tbl = new DataTable();

            tbl = objCNMenu.SelectMenu(id_menu);

            gvMenu.DataSource = tbl;
            gvMenu.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int id_menuOpcion = 0;

            if (Session["IDMenuOpcion"] != null)
            {
                id_menuOpcion = (Int32)Session["IDMenuOpcion"];
            }

            switch (btnGuardar.CommandName)
            {
                case "Editar":
                    if (ActualizarMenuOpcion(id_menuOpcion))
                    {
                        Llenar_gvMenu(Convert.ToInt32(Session["id_menu"].ToString()));
                        LimpiarPanel();
                        btnGuardar.Text = "Guardar";
                        btnGuardar.CommandName = "Guardar";
                    }
                    else
                    {
                        this.lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha Ocurrido un Error al actualizar Opcion";
                    }
                    break;
                case "Guardar":

                    if (GuardarMenu())
                    {
                        Llenar_gvMenu(Convert.ToInt32(Session["id_menu"].ToString()));
                        LimpiarPanel();
                    }
                    else
                    {
                        this.lkBtn_viewPanel_ModalPopupExtender.Show();
                        ErrorMessage.Text = "Ha Ocurrido un Error al guardar Opcion";
                    }

                    break;
                default:
                    break;
            }


        }

        protected void gvMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvMenu.Rows[index];
            int id_menuOpcion = Convert.ToInt32(row.Cells[0].Text);

            /*
             * Agrego a la variable de sesion el id de menu seleccionado
             * para las acciones de editar o eliminar
             */

            Session.Add("IDMenuOpcion", id_menuOpcion);

            int id_menu = 0;
            id_menu = Convert.ToInt32(Request.QueryString["id_om"].ToString());
            Session.Add("id_menu", id_menu);

            switch (e.CommandName)
            {
                //case "submenu":
                //    Response.Redirect("~/Administracion/MenuOpcion.aspx?id_om=" + id_menu.ToString());
                //    break;

                case "modificar":
                    MostrarDatos(id_menuOpcion);
                    this.lkBtn_viewPanel_ModalPopupExtender.Show();
                    break;

                case "eliminar":
                    EliminaMenuOpcion(id_menuOpcion);
                    Llenar_gvMenu(id_menu);
                    break;

                default:
                    break;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
            btnGuardar.Text = "Guardar";
            btnGuardar.CommandName = "Guardar";
        }

        #endregion

        #region Funciones

        protected Boolean GuardarMenu()
        {
            objCEMenu.Nombre = txtNombreOpcion.Text;
            objCEMenu.Descripcion = getDescripcion();
            objCEMenu.URL = getURL();
            objCEMenu.Comando = getComando();
            objCEMenu.Orden = getOrden();
            objCEMenu.Obligatorio = getObligatorio();
            objCEMenu.Visible = getVisible();
            objCEMenu.Login = getConLogin();
            objCEMenu.Id_Padre = Convert.ToInt32(Session["id_menu"].ToString());
            objCEMenu.ID_UsuarioAutoriza = Convert.ToInt32(Session["UsuarioID"].ToString());

            return objCNMenu.SaveMenu(objCEMenu);
        }

        protected Boolean ActualizarMenuOpcion(int id_opcion)
        {
            var respuesta = false;

            objCEMenu.ID_MenuOpcion = id_opcion;
            objCEMenu.Nombre = txtNombreOpcion.Text;
            objCEMenu.Descripcion = getDescripcion();
            objCEMenu.URL = getURL();
            objCEMenu.Comando = getComando();
            objCEMenu.Orden = getOrden();
            objCEMenu.Obligatorio = getObligatorio();
            objCEMenu.Visible = getVisible();
            objCEMenu.Login = getConLogin();
            objCEMenu.Id_Padre = Convert.ToInt32(Session["id_menu"].ToString()); ;
            objCEMenu.ID_UsuarioAutoriza = Convert.ToInt32(Session["UsuarioID"].ToString());

            respuesta = objCNMenu.UpdateMenuOpcion(objCEMenu);

            return respuesta;
        }

        protected void MostrarDatos(int id_menuOpcion)
        {
            btnGuardar.Text = "Editar";
            btnGuardar.CommandName = "Editar";

            var tbl = new DataTable();
            tbl = objCNMenu.SelectOpcionMenu(id_menuOpcion);
            var row = tbl.Rows[0];

            txtNombreOpcion.Text = row["nombre"].ToString();
            txtDescripcionOpcion.Text = row["descripcion"].ToString();
            txtURL.Text = row["url"].ToString();
            txtComando.Text = row["comando"].ToString();
            txtOrden.Text = row["orden"].ToString();
            cb_visible.Checked = (Boolean)row["visible"];
            cb_obligatorio.Checked = (Boolean)row["obligatorio"];
            cb_login.Checked = (Boolean)row["login"];
        }

        protected Boolean EliminaMenuOpcion(int id_opcion)
        {
            var respuesta = false;

            objCEMenu = new CEMenu();
            objCEMenu.ID_MenuOpcion = id_opcion;
            objCEMenu.ID_UsuarioAutoriza = Convert.ToInt32(Session["UsuarioID"].ToString());

            respuesta = objCNMenu.DeleteMenuOpcion(objCEMenu);
            return respuesta;
        }

        protected void LimpiarPanel()
        {
            txtNombreOpcion.Text = "";
            txtDescripcionOpcion.Text = "";
            txtURL.Text = "";
            txtComando.Text = "";
            txtOrden.Text = "";
            cb_visible.Checked = false;
            cb_obligatorio.Checked = false;
            cb_login.Checked = false;
        }

        #endregion

        #region Funciones para obtener valores del formulario

        protected string getNombre()
        {
            return txtNombreOpcion.Text;
        }

        protected string getDescripcion()
        {
            return txtDescripcionOpcion.Text;
        }

        protected string getURL()
        {
            return txtURL.Text;
        }

        protected string getComando()
        {
            return txtComando.Text;
        }

        protected int getOrden()
        {
            return Convert.ToInt32(txtOrden.Text);
        }

        protected Boolean getObligatorio()
        {
            return cb_obligatorio.Checked;
        }

        protected Boolean getVisible()
        {
            return cb_visible.Checked;
        }

        protected Boolean getConLogin()
        {
            return cb_login.Checked;
        }

        #endregion
    }
}