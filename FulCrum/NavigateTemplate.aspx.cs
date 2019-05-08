using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FulCrum
{
    public partial class NavigateTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int ApplicationType = 0;
            
            string Email = "Agarugula@pike.com";
            string Name = "Anil" + " " + "Garugula";

            

            DataSet ds = Fulcrum.BAL.clsBAL_UserMaintenance.GetTranUserRoleDetails(Email);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Request.QueryString["ApplicationType"] != null)
                {
                    ApplicationType = Convert.ToInt32(Request.QueryString["ApplicationType"].ToString());
                }
                else if (ds.Tables[0].Rows[0]["APPLICATION_TYPE"].ToString()!="")
                {
                    ApplicationType = Convert.ToInt32(ds.Tables[0].Rows[0]["APPLICATION_TYPE"].ToString());
                }
                else
                {
                    lblError.Text = "You are not assigned to any application!";
                }
                
                int Role_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ROLE_ID"].ToString());

                Session["ApplicationType"] = ApplicationType;
                Session["Role_Id"] = Role_Id;
                Session["Email"] = Email;
                Session["UserName"] = Name;

                //Response.Redirect("VeirzonFormUpdate.aspx");

                if (ApplicationType == 1)
                {                   
                    Response.Redirect("Dashboard.aspx");
                }

                if (ApplicationType == 2)
                {
                    Response.Redirect("CobbDashboard.aspx");
                }
                if (ApplicationType == 3)
                {
                    Response.Redirect("VeirzonFormUpdate.aspx");
                }
                if (ApplicationType == 4)
                {
                    Response.Redirect("APCMRDashboard.aspx");
                }
            }
            else
            {

            }
        }
    }
}