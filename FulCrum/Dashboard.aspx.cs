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
using System.Net.Http;

namespace Fulcrum
{
    public partial class Dashboard : BasePage
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HideErrorTable(tr_ErrorRow, lblError, lblInfo);
                if (!IsPostBack)
                {
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

                    DataSet dsMaster = BAL.clsBAL_FieldDataEntry.MasterData_GetList();

                    ddlTrackingNo.DataSource = dsMaster.Tables[4];
                    ddlTrackingNo.DataTextField = "tracking_no";
                    ddlTrackingNo.DataValueField = "tracking_no";
                    ddlTrackingNo.DataBind();
                    ddlTrackingNo.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlTrackingNo.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        } 
        #endregion

        #region Buttons
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string TrackingNo = ddlTrackingNo.SelectedItem.Text;
                DataSet dsRecords = BAL.clsBAL_FieldDataEntry.GetTrackingRecords(TrackingNo);
                for (int i = 0; i < dsRecords.Tables[0].Rows.Count; i++)
                {
                    string RecordId = dsRecords.Tables[0].Rows[i]["_record_id"].ToString();
                    string UpdatePath = "https://api.fulcrumapp.com/api/v2/records/" + RecordId + ".json?token=9e7a63ab3ad0906cfca9077c652ffa28a64c123571cd32444434a24a01c55af3f44ad0983d223cfb";
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://api.fulcrumapp.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    HttpResponseMessage response = client.DeleteAsync(UpdatePath).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        DisplayError(tr_ErrorRow, lblInfo, "Data deleted successfully!");
                    }
                    else
                    {
                        DisplayError(tr_ErrorRow, lblError, "Data delete failure!");
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        } 
        #endregion
    }
}