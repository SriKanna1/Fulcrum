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
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace Fulcrum
{
    public partial class User_Manintenance : BasePage
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HideErrorTable(tr_ErrorRow, lblError, lblInfo);
                if (HttpContext.Current.Session.IsNewSession)
                {
                    Response.Redirect("https://login.microsoftonline.com/my-azure-ad-guid/oauth2/logout");
                }
                if (!IsPostBack)
                {
                    GetUserMasterList();
                    GetUserRoleDetails();

                    DataSet ds = BAL.clsBAL_UserMaintenance.UserCompanyGetList();
                    ddlRole.DataSource = ds.Tables[0];
                    ddlRole.DataTextField = "ROLE_NAME";
                    ddlRole.DataValueField = "ROLE_ID";
                    ddlRole.DataBind();
                    ddlRole.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlRole.SelectedIndex = 0;
                    ViewState["User_Maintenance"] = "Insert";
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

            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region Button Events
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                int RoleId = Convert.ToInt32(ddlRole.SelectedItem.Value);
                string AppType = "";
                foreach (RadComboBoxItem item in ddlAppType.Items)
                {
                    if (item.Checked)
                        AppType += item.Value + ",";
                }
                AppType = AppType.TrimEnd(',');
                string FirstName = txtFirstName.Text;
                string LastName = txtLastName.Text;
                string Email = txtEmail.Text;

                if (ViewState["User_Maintenance"].ToString() == "Insert")
                {
                    int result = BAL.clsBAL_UserMaintenance.UserRoleCreate(RoleId, AppType, FirstName, LastName, Email);
                    if (result > 0)
                    {
                        DisplayError(tr_ErrorRow, lblInfo, "User Role Created Successfully");
                        Reset();
                        GetUserRoleDetails();
                        GetUserMasterList();
                    }
                    else
                    {
                        DisplayError(tr_ErrorRow, lblError, "Insert Error!!!");
                    }
                }

                else if (ViewState["User_Maintenance"].ToString() == "Modify")
                {
                    int UserId = Convert.ToInt32(ViewState["UserId"].ToString());
                    int result = BAL.clsBAL_UserMaintenance.UserRoleUpdate(UserId, RoleId, AppType, FirstName, LastName, Email);
                    if (result > 0)
                    {
                        DisplayError(tr_ErrorRow, lblInfo, "User Role  updated successfully!");
                        Reset();
                        GetUserRoleDetails();
                        GetUserMasterList();
                        this.btn_Submit.Text = "insert";
                    }
                    else
                    {
                        DisplayError(tr_ErrorRow, lblError, "User Role  update failure!");
                    }
                }
            }
            catch (SqlException exp)
            {
                if (exp.Number == 2601 || exp.Number == 2627)
                    DisplayError(tr_ErrorRow, lblError, "User already exists!!");
                   
                else
                    DisplayError(tr_ErrorRow, lblError, "Sql Exception");
                Reset();
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region Common Functions
        public void Reset()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            ddlAppType.ClearSelection();
            ddlRole.ClearSelection();
            ViewState["User_Maintenance"] = "Insert";
            GetUserMasterList();
            GetUserRoleDetails();
        }


        public void GetUserRoleDetails()
        {
            DataSet ds = BAL.clsBAL_UserMaintenance.GetUserRoleDetails();
            rgUsers.DataSource = ds;
            ViewState["UserRole"] = ds;
            rgUsers.DataBind();
        }

        public void GetUserMasterList()
        {
            DataSet ds = BAL.clsBAL_UserMaintenance.GetUserMasterlist();

            ddlAppType.DataSource = ds.Tables[1];
            ddlAppType.DataTextField = "FULCRUM_COMPANY_NAME";
            ddlAppType.DataValueField = "FULCRUM_COMPANY_ID";
            ddlAppType.DataBind();
        }
        #endregion

        #region Grid Events
        protected void rgUsers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["UserRole"];
                rgUsers.DataSource = ds;
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        protected void rgUsers_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "cmdDelete")
                {
                    if (e.Item.ItemIndex >= 0)
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        int UserRoleId = Convert.ToInt32(item["USER_ID"].Text);
                        int result = BAL.clsBAL_UserMaintenance.UserRoleDelete(UserRoleId);
                        if (result > 0)
                        {
                            DisplayError(tr_ErrorRow, lblInfo, "User deleted successfully");
                            GetUserRoleDetails();
                            GetUserMasterList();
                        }
                        else
                        {
                            GetUserRoleDetails();
                            GetUserMasterList();
                        }
                    }
                }
                if (e.CommandName == "cmdEdit")
                {
                    if (e.Item.ItemIndex >= 0)
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        int UserId = Convert.ToInt32(item["USER_ID"].Text);
                        string UserName = item["USER_NAME"].Text;
                        string[] Name = UserName.Split(' ');
                        txtFirstName.Text = Name[0];
                        txtLastName.Text = Name[1];
                        txtEmail.Text = item["USER_EMAIL"].Text;
                        ddlRole.SelectedValue = item["User_ROLE"].Text;
                        string Applications = item["APPLICATION_NAME"].Text;
                        string[] strArray = Applications.Split(',');
                        ddlAppType.ClearCheckedItems();
                        for (int i = 0; i < ddlAppType.Items.Count; i++)
                        {
                            foreach (string entry in strArray)
                            {
                                string ApplicationType = entry;
                                if (ddlAppType.Items[i].Text.ToString() == ApplicationType)
                                {
                                    ddlAppType.Items[i].Checked = true;
                                    break;
                                }
                            }
                        }
                        ViewState["User_Maintenance"] = "Modify";
                        this.btn_Submit.Text = "Modify";
                        ViewState["UserId"] = UserId;
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        protected void rgUsers_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    ImageButton button = item["cmdDelete"].Controls[0] as ImageButton;
                    button.Attributes["onclick"] = "return confirm('Are you sure you want to delete ?')";
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Reset();
            this.btn_Submit.Text = "insert";
        }
    }
}