using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Fulcrum.Common;
using Fulcrum.BAL;

namespace Fulcrum
{
    public partial class JobData : BasePage
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HideErrorTable(tr_ErrorRow, lblError, lblInfo);
                //if (HttpContext.Current.Session.IsNewSession)
                //{
                //    Response.Redirect("./Login.aspx?session=out");
                //}
                if (!IsPostBack)
                {
                    DataSet dsMaster = BAL.clsBAL_JobData.JobMasterData_GetList();
                    ddljobtype.DataSource = dsMaster.Tables[0];
                    ddljobtype.DataTextField = "JobType";
                    ddljobtype.DataValueField = "JobType";
                    ddljobtype.DataBind();
                    ddljobtype.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddljobtype.SelectedIndex = 0;

                    ddlNJUNSCode.DataSource = dsMaster.Tables[1];
                    ddlNJUNSCode.DataTextField = "NJUNSCodeDesc";
                    ddlNJUNSCode.DataValueField = "NJUNSCodeDesc";
                    ddlNJUNSCode.DataBind();
                    ddlNJUNSCode.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlNJUNSCode.SelectedIndex = 0;

                    ddlTrackingNo.DataSource = dsMaster.Tables[2];
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

        #region Binding
        protected void BindJobData()
        {
            try
            {
                string TrackingId = ddlTrackingNo.SelectedItem.Value;
                ViewState["TrackingId"] = TrackingId;
                DataSet ds = BAL.clsBAL_JobData.GetJobDetails(TrackingId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txt_JobName.Text = ds.Tables[0].Rows[0]["JobName"].ToString();
                    txtReference.Text = ds.Tables[0].Rows[0]["Reference"].ToString();
                    txtworkorder.Text = ds.Tables[0].Rows[0]["Workorder"].ToString();
                    txtPikeJob.Text = ds.Tables[0].Rows[0]["PikeJob"].ToString();
                    txtEngineer.Text = ds.Tables[0].Rows[0]["Engineer"].ToString();
                    txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                    txtCounty.Text = ds.Tables[0].Rows[0]["County"].ToString();
                    txtRegion.Text = ds.Tables[0].Rows[0]["Region"].ToString();
                    txtHeadquaters.Text = ds.Tables[0].Rows[0]["Headquarters"].ToString();
                    ddljobtype.ClearSelection();
                    for (int i = 0; i < ddljobtype.Items.Count; i++)
                    {
                        if (ddljobtype.Items[i].Text.ToString() == ds.Tables[0].Rows[0]["JobType"].ToString())
                        {
                            ddljobtype.Items[i].Selected = true;
                            break;
                        }
                    }
                    if (ds.Tables[0].Rows[0]["Startdate"].ToString() != "")
                    {
                        RadstartDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Startdate"].ToString());
                    }
                    else
                    {
                        RadstartDate.SelectedDate = null;
                    }
                    ddlNJUNSCode.ClearSelection();
                    for (int i = 0; i < ddlNJUNSCode.Items.Count; i++)
                    {
                        if (ddlNJUNSCode.Items[i].Text.ToString() == ds.Tables[0].Rows[0]["NJUNSCode"].ToString())
                        {
                            ddlNJUNSCode.Items[i].Selected = true;
                            break;
                        }
                    }
                    txtNJUNSProjNum.Text = ds.Tables[0].Rows[0]["NJUNSProjNum"].ToString();
                    txtFieldEng.Text = ds.Tables[0].Rows[0]["FieldEngineer"].ToString();
                    RadDateEngStartDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FieldEngineerDate"].ToString());
                    txtQCEngineer.Text = ds.Tables[0].Rows[0]["QCEngineer"].ToString();
                    if (ds.Tables[0].Rows[0]["QCDate"].ToString() != "")
                    {
                        RadQCDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["QCDate"].ToString());
                    }
                    else
                    {
                        RadQCDate.SelectedDate = null;
                    }
                }
                else
                {
                    JobDataReset();
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        public void PermiteeList()
        {
            try
            {
                string TrackingId = ddlTrackingNo.SelectedItem.Value;
                DataSet ds = BAL.clsBAL_JobData.GetPermiteeList(TrackingId);
                ViewState["Permitee_List"] = ds;
                rg_Permitee.DataSource = ds;
                rg_Permitee.DataBind();
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region ddlTrackingNo_SelectedIndexChanged
        protected void ddlTrackingNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTrackingNo.SelectedItem.Value == null || ddlTrackingNo.SelectedItem.Value == "0")
                {
                    BindJobData();
                }
                else
                {
                    BindJobData();
                    PermiteeList();
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region GridEvents
        protected void rg_Permitee_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["Permitee_List"];
                rg_Permitee.DataSource = ds;
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Permitee_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }
        protected void rg_Permitee_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.IsInEditMode)
                {
                    GridEditableItem editedItem = (GridEditableItem)e.Item;
                    string CompanyName = ((TextBox)(editedItem["CompanyName"].Controls[0])).Text;
                    if (CompanyName != "")
                    {
                        //bool Permitee = ((CheckBox)(editedItem["Permitee"].Controls[1])).Checked;
                        bool Permitee = false;
                        string TrackingId = ViewState["TrackingId"].ToString();
                        int result = BAL.clsBAL_JobData.EditPermitee_Insert(TrackingId, CompanyName, Permitee);
                        if (result > 0)
                        {
                            lblPermiteeInfo.Visible = true;
                            lblPermiteeInfo.ForeColor = System.Drawing.Color.Green;
                            lblPermiteeInfo.Text = "Successfully inserted company!";
                            PermiteeList();
                        }
                    }
                    else
                    {
                        lblPermiteeInfo.Visible = true;
                        lblPermiteeInfo.ForeColor = System.Drawing.Color.Green;
                        lblPermiteeInfo.Text = "Please enter new company name!";
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        protected void rg_Permitee_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.IsInEditMode)
                {

                    GridEditableItem editedItem = (GridEditableItem)e.Item;
                    lblPermiteeInfo.Visible = true;
                    lblPermiteeInfo.ForeColor = System.Drawing.Color.Red;
                    lblPermiteeInfo.Text = "You are not allowed to edit the company name!";
                    //string CompanyName = ((TextBox)(editedItem["CompanyName"].Controls[0])).Text;
                    //if (CompanyName != "")
                    //{ 
                    //    bool Permitee = ((CheckBox)(editedItem["Permitee"].Controls[1])).Checked;
                    //    //int Id = Convert.ToInt32(((TextBox)(editedItem["Id"].Controls[0])).Text);
                    //    string TrackingId = ViewState["TrackingId"].ToString();
                    //    int result = BAL.clsBAL_JobData.EditPermitee_Insert(TrackingId, CompanyName, Permitee);
                    //    if (result > 0)
                    //    {
                    //        PermiteeList();
                    //    }
                    //}
                    //else
                    //{
                    //    DisplayError(tr_ErrorRow, lblError, "Please enter new company name");
                    //}
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        protected void rg_Permitee_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                rg_Permitee.MasterTableView.GetColumn("EditCommandColumn").Visible = false;
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    rg_Permitee.MasterTableView.GetColumn("EditCommandColumn").Visible = true;
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        #endregion

        #region Button
        protected void btn_JobInsert_Click(object sender, EventArgs e)
        {
            try
            {
                clsJobData objJobData = new clsJobData();
                objJobData.JobName = txt_JobName.Text;
                objJobData.TrackingId = ddlTrackingNo.SelectedItem.Value;
                objJobData.Reference = txtReference.Text;
                objJobData.Workorder = txtworkorder.Text;
                objJobData.PikeJob = txtPikeJob.Text;
                objJobData.Engineer = txtEngineer.Text;
                objJobData.City = txtCity.Text;
                objJobData.County = txtCounty.Text;
                objJobData.Region = txtRegion.Text;
                objJobData.Headquarters = txtHeadquaters.Text;
                objJobData.JobType = ddljobtype.SelectedItem.Text;
                objJobData.StartDate = Convert.ToDateTime(RadstartDate.SelectedDate);
                objJobData.QCEngineer = txtQCEngineer.Text;
                objJobData.QCDate = Convert.ToDateTime(RadQCDate.SelectedDate);
                objJobData.NJUNSCode = ddlNJUNSCode.SelectedItem.Text;
                objJobData.NJUNSProjNum = txtNJUNSProjNum.Text;
                objJobData.FieldEngineer = txtFieldEng.Text;
                objJobData.FieldEngDate = Convert.ToDateTime(RadDateEngStartDate.SelectedDate);

                int result = BAL.clsBAL_JobData.JobData_Insert(objJobData);
                if (result > 0)
                {
                    DisplayError(tr_ErrorRow, lblInfo, "Job data entry updated successfully!");
                }
                else
                {
                    DisplayError(tr_ErrorRow, lblError, "Job data update failure!");
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region Reset
        public void JobDataReset()
        {
            txt_JobName.Text = "";
            ddlTrackingNo.ClearSelection();
            txtReference.Text = "";
            txtworkorder.Text = "";
            txtPikeJob.Text = "";
            txtEngineer.Text = "";
            txtCity.Text = "";
            txtCounty.Text = "";
            txtRegion.Text = "";
            txtHeadquaters.Text = "";
            ddljobtype.ClearSelection();
            RadstartDate.Clear();
            txtQCEngineer.Text = "";
            RadQCDate.Clear();
            ddlNJUNSCode.ClearSelection();
            txtNJUNSProjNum.Text = "";
            txtFieldEng.Text = "";
            RadDateEngStartDate.Clear();
            rg_Permitee.DataSource = new string[] { };
            rg_Permitee.DataBind();
        }
        #endregion
    }
}