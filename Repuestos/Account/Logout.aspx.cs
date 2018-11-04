using System;
using System.Web.Security;

namespace Repuestos.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["UsuarioID"] = 0;
            Session.Clear();
            ViewState.Clear();
            FormsAuthentication.SignOut();
            //Server.Transfer("~/Default.aspx", false);
            //Server.Transfer("~/", false);
            Response.Redirect("~/Default.aspx");
            //FormsAuthentication.RedirectToLoginPage();     
        }
    }
}