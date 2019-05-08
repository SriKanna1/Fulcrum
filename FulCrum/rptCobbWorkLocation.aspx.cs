using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fulcrum.Common;

namespace FulCrum
{
    public partial class rptCobbWorkLocation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session.IsNewSession)
                {
                    Response.Redirect("https://login.microsoftonline.com/my-azure-ad-guid/oauth2/logout");
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
    }
}