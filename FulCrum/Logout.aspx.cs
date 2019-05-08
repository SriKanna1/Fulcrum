using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fulcrum.Common;

namespace Fulcrum
{
    public partial class Logout : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HideErrorTable(tr_ErrorRow, lblError, lblInfo);
                if (!IsPostBack)
                {
                    Session.Abandon();
                    //FormsAuthentication.SignOut();
                    foreach (string cookie in HttpContext.Current.Request.Cookies.AllKeys)
                    {
                        HttpContext.Current.Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                    }

                    Response.Redirect("https://login.microsoftonline.com/my-azure-ad-guid/oauth2/logout");
                    //Response.Redirect("https://login.microsoftonline.com/{pikeenterprises.onmicrosoft.com}/oauth2/logout?post_logout_redirect_uri={https://oraclewebservicesstg.pike.com/Fulcrum/}");
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
    }
}