using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
//using System.IdentityModel.Claims;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System.Data;
using System.Web.UI.HtmlControls;

namespace Fulcrum
{
    public partial class FulcrumMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                DateTime CurrentDate = DateTime.UtcNow;
                lblDatetime.Text = CurrentDate.ToString("ddd, dd MMMM yyyy");
               //lblName.Text = "Amarnath Vangoor";
                
                ClaimsPrincipal principal = new ClaimsPrincipal();

                //var name = Context.User.Identity as System.Security.Claims.ClaimsIdentity;
                //string fn = name.FindFirst(ClaimTypes.GivenName).Value;
                //string ln = name.FindFirst(ClaimTypes.Surname).Value;

                ////var name = principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                //var fn = Context.User.Identity.principal.FindFirst(ClaimTypes.GivenName).Value;
                //var ln = principal.FindFirst(ClaimTypes.Surname).Value;


                //lblName.Text = fn + " " + ln;

                string Email = Session["Email"].ToString();
                lblName.Text = Session["UserName"].ToString();
                int ApplicationType = Convert.ToInt32(Session["ApplicationType"]);
                int RoleId = Convert.ToInt32(Session["ROLE_ID"]);
                 
                DataSet ds = BAL.clsBAL_UserMaintenance.GetUserApplications(Email);
                DdlApplication.DataSource = ds.Tables[0];
                DdlApplication.DataTextField = "APPLICATION_NAME";
                DdlApplication.DataValueField = "APPLICATION_ID";
                DdlApplication.DataBind();


                //MakeReadyDashBoard.Visible = false;
                //MakeredyForms.Visible = false;
                //MakeredyReports.Visible = false;
                //cobbDashBoard.Visible = false;
                //CobbForms.Visible = false;
                //CobbReports.Visible = false;
                //UserMaintenance.Visible = false;

                if (ApplicationType == 1)
                {
                    MakeReadyDashBoard.Visible = true;
                    MakeredyForms.Visible = true;
                    MakeredyReports.Visible = true;
                    cobbDashBoard.Visible = false;
                    CobbForms.Visible = false;
                    CobbReports.Visible = false;
                    UserMaintenance.Visible = false;
                    APCMRApcoDashboard.Visible = false;
                    APCMRApcoForms.Visible = false;
                    APCMRApcoReports.Visible = false;
                }

                if (ApplicationType == 2)
                {
                    MakeReadyDashBoard.Visible = false;
                    MakeredyForms.Visible = false;
                    MakeredyReports.Visible = false;
                    cobbDashBoard.Visible = true;
                    CobbForms.Visible = true;
                    CobbReports.Visible = true;
                    UserMaintenance.Visible = false;
                    APCMRApcoDashboard.Visible = false;
                    APCMRApcoForms.Visible = false;
                    APCMRApcoReports.Visible = false;
                }

                if (ApplicationType == 3)
                {
                    MakeReadyDashBoard.Visible = false;
                    MakeredyForms.Visible = false;
                    MakeredyReports.Visible = false;
                    cobbDashBoard.Visible = false;
                    CobbForms.Visible = false;
                    CobbReports.Visible = false;
                    UserMaintenance.Visible = false;
                    APCMRApcoDashboard.Visible = false;
                    APCMRApcoForms.Visible = false;
                    APCMRApcoReports.Visible = false;
                }

                if (ApplicationType == 4)
                {
                    MakeReadyDashBoard.Visible = false;
                    MakeredyForms.Visible = false;
                    MakeredyReports.Visible = false;
                    cobbDashBoard.Visible = false;
                    CobbForms.Visible = false;
                    CobbReports.Visible = false;
                    UserMaintenance.Visible = false;
                    APCMRApcoDashboard.Visible = true;
                    APCMRApcoForms.Visible = true;
                    APCMRApcoReports.Visible = true;
                }
                DdlApplication.ClearSelection();
                for (int i = 0; i < DdlApplication.Items.Count; i++)
                {
                    if (Convert.ToInt32(DdlApplication.Items[i].Value.ToString()) == ApplicationType)
                    {
                        DdlApplication.Items[i].Selected = true;
                        break;
                    }
                }
                if (RoleId == 1)
                {
                    UserMaintenance.Visible = true;
                }
                else
                {
                    UserMaintenance.Visible = false;
                }
            }
        }

        protected void DdlApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ApplicationId =Convert.ToInt32(DdlApplication.SelectedItem.Value);
            Response.Redirect("NavigateTemplate.aspx?ApplicationType=" + ApplicationId);
        }
    }
}