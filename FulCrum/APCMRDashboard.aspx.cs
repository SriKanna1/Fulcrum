using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fulcrum.Common;
using Fulcrum.BAL;
using System.Web.UI.HtmlControls;
using System.Data;

namespace Fulcrum
{
    public partial class APCMRDashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HideErrorTable(tr_ErrorRow, lblError, lblInfo);
                if (HttpContext.Current.Session.IsNewSession)
                {
                    Response.Redirect("https://login.microsoftonline.com/my-azure-ad-guid/oauth2/logout");
                }

                int RoleId = Convert.ToInt32(Session["ROLE_ID"]);

                if (RoleId == 1)
                {
                    HtmlAnchor link = (HtmlAnchor)(this.Master).FindControl("logoDashboard");
                    link.HRef = "Dashboard.aspx";
                }

                if (RoleId == 4)
                {
                    HtmlAnchor link1 = (HtmlAnchor)(this.Master).FindControl("logoDashboard");
                    link1.HRef = "CobbDashboard.aspx";
                }

               
                //DataSet dsMaster = BAL.clsBAL_Cobb_FieldDataEntry.MasterData_GetList();
                //ddlTrackingNo.DataSource = dsMaster.Tables[4];
                //ddlTrackingNo.DataTextField = "tracking_no";
                //ddlTrackingNo.DataValueField = "tracking_no";
                //ddlTrackingNo.DataBind();
                //ddlTrackingNo.Items.Insert(0, new ListItem("--Select--", "0"));
                //ddlTrackingNo.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
    }
}