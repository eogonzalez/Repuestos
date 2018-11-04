using Capa_Negocio.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repuestos.Account
{
    public partial class SolicitudRegistro : System.Web.UI.Page
    {
        CNLogin objCapaNegocio = new CNLogin();

        #region Eventos del Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Llenar_gvSuscripcionUsuarios();
                Llenar_ddlTipoPermiso();
            }
        }

        protected void gvSolicitudRegistro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {

                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvSolicitudRegistro.Rows[index];

                //Agrego usuario seleccionado como variable de session
                Session.Add("IDUsuarioPermiso", row.Cells[0].Text);
                Session.Add("NombreUsuarioPermiso", row.Cells[1].Text);
                Session.Add("ApellidoUsuarioPermiso", row.Cells[2].Text);
                Session.Add("CorreoUsuarioPermiso", row.Cells[4].Text);

                switch (e.CommandName)
                {
                    case "autorizar":
                        UsuarioMesage.Text = Session["NombreUsuarioPermiso"].ToString() + " " + Session["ApellidoUsuarioPermiso"].ToString();
                        lkBtn_Autorizar_ModalPopupExtender.Show();
                        break;

                    case "aclarar":
                        btnEnviar.CommandName = "aclarar";
                        lkBtn_Motivo_ModalPopupExtender.Show();
                        break;

                    case "rechazar":
                        btnEnviar.CommandName = "rechazar";
                        lkBtn_Motivo_ModalPopupExtender.Show();
                        break;

                }
            }
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            //Llama al metodo que inserta la autorizacion
            int id_tipousuario = Convert.ToInt32(ddlTipoPermiso.SelectedValue.ToString());
            int ID_UsuarioPermiso = Convert.ToInt32(Session["IDUsuarioPermiso"].ToString());
            int id_usuarioAutoriza = Convert.ToInt32(Session["UsuarioID"].ToString());

            if (objCapaNegocio.InsertAutorizacionPermisoUsuario(ID_UsuarioPermiso, id_tipousuario, id_usuarioAutoriza))
            {
                string NombreUsuarioPermiso = Session["NombreUsuarioPermiso"].ToString();
                string ApellidoUsuarioPermiso = Session["ApellidoUsuarioPermiso"].ToString();
                string CorreoUsuarioPermiso = Session["CorreoUsuarioPermiso"].ToString();

                var message = new MailMessage();
                message.Subject = "Registro Aprobado -Unidad Origen DACE-";
                message.Body = "Apreciable usuario " + NombreUsuarioPermiso + " " + ApellidoUsuarioPermiso + " Bienvenido al Sistema de Gestion de Procesos de la Unidad de Origen de la -DACE- \n\n " +
                    "Hemos recibido su solicitud de registro al sistema.\n\n" +
                    "Se ha autorizado el acceso, con las credenciales que utilizo en su registro. \n\n" +
                    "Favor de no responder este correo.";


                //Envia correo
                if (EnvioMensajeRegistro(NombreUsuarioPermiso, ApellidoUsuarioPermiso, CorreoUsuarioPermiso, message))
                {
                    //Actualizo Grid
                    Llenar_gvSuscripcionUsuarios();
                }
                else
                {
                    ErrorMessage.Text = "Ha ocurrido un error al notificar al usuario sobre su registro.";
                }

            }
            else
            {
                ErrorMessage.Text = "Ha ocurrido un error al dar permiso al usuario seleccionado.";
            }

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string NombreUsuarioPermiso = Session["NombreUsuarioPermiso"].ToString();
            string ApellidoUsuarioPermiso = Session["ApellidoUsuarioPermiso"].ToString();
            string CorreoUsuarioPermiso = Session["CorreoUsuarioPermiso"].ToString();
            string Motivo = txtMotivo.Text;
            MailMessage message = new MailMessage();

            switch (btnEnviar.CommandName)
            {

                case "rechazar":
                    int ID_UsuarioPermiso = Convert.ToInt32(Session["IDUsuarioPermiso"].ToString());
                    int id_usuarioAutoriza = Convert.ToInt32(Session["UsuarioID"].ToString());

                    if (objCapaNegocio.UpdateRechazoPermisoUsuario(id_usuarioAutoriza, ID_UsuarioPermiso))
                    {
                        message.Subject = "Rechazo Registro- ";
                        message.Body = "Apreciable usuario " + NombreUsuarioPermiso + " " + ApellidoUsuarioPermiso + " Hemos recibido su solicitud de registro al sistema. \n\n " +
                            "La cual hemos rechazado por el siguiente motivo: \n\n " +
                            Motivo + "\n\n" +
                            " Favor de no responder este correo.";

                        if (EnvioMensajeRegistro(NombreUsuarioPermiso, ApellidoUsuarioPermiso, CorreoUsuarioPermiso, message))
                        {
                            Llenar_gvSuscripcionUsuarios();
                        }
                        else
                        {
                            ErrorMessage.Text = "Ha ocurrido un error al notificar al usuario sobre su registro.";
                        }
                    }
                    else
                    {
                        ErrorMessage.Text = "Ha ocurrido un error al actualizar el registro del usuario.";
                    }

                    break;
            }
        }

        protected void gvSolicitudRegistro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSolicitudRegistro.PageIndex = e.NewPageIndex;
            Llenar_gvSuscripcionUsuarios();
        }

        #endregion

        #region Funciones

        protected void Llenar_gvSuscripcionUsuarios()
        {
            DataTable tbl = new DataTable();

            tbl = objCapaNegocio.SelectSolicitudRegistroUsuarios().Tables[0];

            gvSolicitudRegistro.DataSource = tbl;
            gvSolicitudRegistro.DataBind();

        }

        protected void Llenar_ddlTipoPermiso()
        {
            var dt = new DataTable();

            dt = objCapaNegocio.SelectComboPerfiles();

            if (dt.Rows.Count > 0)
            {
                ddlTipoPermiso.DataTextField = dt.Columns["nombre"].ToString();
                ddlTipoPermiso.DataValueField = dt.Columns["id_tipousuario"].ToString();
                ddlTipoPermiso.DataSource = dt;
                ddlTipoPermiso.DataBind();
            }
        }

        protected Boolean EnvioMensajeRegistro(string nombre, string apellido, string correo, MailMessage message)
        {
            Boolean Enviado = false;
            try
            {

                Correo Cr = new Correo();
                MailMessage mnsj = new MailMessage();

                mnsj.Subject = message.Subject;

                mnsj.To.Add(new MailAddress(correo));

                mnsj.From = new MailAddress("alertas.verpi@gmail.com", "Alertas Taller Mecanico");

                /* Si deseamos Adjuntar algún archivo*/
                //mnsj.Attachments.Add(new Attachment("C:\\archivo.pdf"));

                mnsj.Body = message.Body;

                /* Enviar */
                Cr.EnviarCorreo(mnsj);
                Enviado = true;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Enviado = false;
            }

            return Enviado;
        }

        #endregion
    }
}