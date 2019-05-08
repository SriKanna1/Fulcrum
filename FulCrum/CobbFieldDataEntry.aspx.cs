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
    public partial class CobbFieldDataEntry : BasePage
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
                    DivViolation.Visible = false;
                    lblPoaInfo.Visible = false;
                    lblMidspanInfo.Visible = false;
                    lblNotesInfo.Visible = false;
                    lblInsertInfo.Visible = false;
                    DataSet dsMaster = BAL.clsBAL_Cobb_FieldDataEntry.MasterData_GetList();
                    ViewState["MasterData"] = dsMaster;

                    ddlTrackingNo.DataSource = dsMaster.Tables[4];
                    ddlTrackingNo.DataTextField = "tracking_no";
                    ddlTrackingNo.DataValueField = "tracking_no";
                    ddlTrackingNo.DataBind();
                    ddlTrackingNo.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlTrackingNo.SelectedIndex = 0;

                    ddl_Owner.DataSource = dsMaster.Tables[1];
                    ddl_Owner.DataTextField = "Pole_Owner";
                    ddl_Owner.DataValueField = "Pole_Owner";
                    ddl_Owner.DataBind();
                    ddl_Owner.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddl_Owner.SelectedIndex = 0;

                    GetGoToPoleDetails("", "");
                }

            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region GetGoToPoleDetails
        protected void GetGoToPoleDetails(string PoleId, string TrackingNo)
        {
            try
            {
                DataSet ds = BAL.clsBAL_Cobb_FieldDataEntry.GetPoleDetails(PoleId, TrackingNo);
                ViewState["PoleDetails"] = ds;
                ViewState["Pole_Id"] = PoleId;
                ViewState["Tracking_No"] = TrackingNo;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txt_Location.Text = ds.Tables[0].Rows[0]["Location"].ToString();
                    txt_Station.Text = ds.Tables[0].Rows[0]["Station"].ToString();

                    txtPowerMap.Text = ds.Tables[0].Rows[0]["PowerMap"].ToString(); //CobbPole
                    txtCommMap.Text = ds.Tables[0].Rows[0]["CommMap"].ToString();

                    txtNJUNS.Text = ds.Tables[0].Rows[0]["NJUNSNo"].ToString();
                    txt_HtClass_ExistingFT.Text = ds.Tables[0].Rows[0]["Ht_Class_ExistingFt"].ToString();
                    txt_HtClass_ExistingInch.Text = ds.Tables[0].Rows[0]["Ht_Class_ExistingIn"].ToString();
                    //txt_HtClass_NewFT.Text = ds.Tables[0].Rows[0]["Ht_Class_NewFt"].ToString();
                    //txt_HtClass_NewInch.Text = ds.Tables[0].Rows[0]["Ht_Class_NewIn"].ToString();

                    //txt_Component_Id.Text = ds.Tables[0].Rows[0]["Component_Id"].ToString();
                    txtLatitude.Text = ds.Tables[0].Rows[0]["latitude"].ToString();
                    txtLongitude.Text = ds.Tables[0].Rows[0]["longitude"].ToString();

                    txt_Temp.Text = ds.Tables[0].Rows[0]["Temparature"].ToString();
                    if(ds.Tables[0].Rows[0]["Date"].ToString()=="")
                    {
                        RadDate.SelectedDate = null;
                    }
                    else
                    {
                        RadDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"].ToString());
                    } 
                    txtConstructionCost.Text = ds.Tables[0].Rows[0]["GPCCost"].ToString();

                    txt_FieldNotes.Text = ds.Tables[0].Rows[0]["FieldNotes"].ToString();
                    txtEngNotes.Text = ds.Tables[0].Rows[0]["EngineerNotes"].ToString();

                    ddl_Owner.ClearSelection();
                    for (int i = 0; i < ddl_Owner.Items.Count; i++)
                    {
                        if (ddl_Owner.Items[i].Text.ToString() == ds.Tables[0].Rows[0]["Owner"].ToString())
                        {
                            ddl_Owner.Items[i].Selected = true;
                            break;
                        }
                    }
                }
                else
                {
                    FieldDataReset();
                }
                GetPOAList(PoleId, TrackingNo);
                GetMidspanList(PoleId, TrackingNo);
                GetNotesList(PoleId, TrackingNo);
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region Button
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                clsFieldDataEntry FieldData = new clsFieldDataEntry();
                FieldData.TrackingNo = ddlTrackingNo.SelectedItem.Value;
                FieldData.Pole_Id = ddl_GoToPole.SelectedItem.Value; 
                FieldData.Station = txt_Station.Text;
                FieldData.CommMap = txtCommMap.Text;
                FieldData.Owner = ddl_Owner.SelectedItem.Text;
                FieldData.Location = txt_Location.Text;
                FieldData.PowerMap = txtPowerMap.Text;  //CobbPole
                FieldData.NJUNSNo = txtNJUNS.Text;
                FieldData.Ht_Class_ExistingFt = txt_HtClass_ExistingFT.Text; ;
                FieldData.Ht_Class_ExistingIn = txt_HtClass_ExistingInch.Text;
                FieldData.Ht_Class_NewFt = "";
                FieldData.Ht_Class_NewIn = ""; 
                FieldData.latitude = txtLatitude.Text;
                FieldData.longitude = txtLongitude.Text;
                FieldData.Component_Id = "";
                FieldData.Date = Convert.ToDateTime(RadDate.SelectedDate);
                FieldData.Temparature = txt_Temp.Text;
                FieldData.GPCCost = txtConstructionCost.Text; 
                FieldData.FieldNotes = txt_FieldNotes.Text;
                FieldData.EngineerNotes = txtEngNotes.Text;
                FieldData.PoleComplete = chkPoleComplete.Checked ? 1 : 0;
                if (FieldData.PoleComplete == 1)
                    FieldData.PoleCompleteDate = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
                else
                    FieldData.PoleCompleteDate = "";

                int result = BAL.clsBAL_Cobb_FieldDataEntry.FieldData_Insert(FieldData);
                if (result > 0)
                {
                    lblMidspanInfo.Visible = false;
                    lblNotesInfo.Visible = false;
                    lblInsertInfo.Visible = true;
                    lblPoaInfo.Visible = false;
                    lblInsertInfo.ForeColor = System.Drawing.Color.Green;
                    lblInsertInfo.Text = "Field Data Entry Inserted successfully!";
                }
                else
                {
                    lblMidspanInfo.Visible = false;
                    lblNotesInfo.Visible = false;
                    lblInsertInfo.Visible = true;
                    lblPoaInfo.Visible = false;
                    lblInsertInfo.ForeColor = System.Drawing.Color.Red;
                    lblInsertInfo.Text = "Field Data Entry failure!";
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region ddl_SelectedIndexChanged
        protected void ddl_GoToPole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMidspanInfo.Visible = false;
                lblNotesInfo.Visible = false;
                lblInsertInfo.Visible = false;
                lblPoaInfo.Visible = false;
                DivViolation.Visible = false;
                GetGoToPoleDetails(ddl_GoToPole.SelectedItem.Value, ddlTrackingNo.SelectedItem.Value);
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void ddlTrackingNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMidspanInfo.Visible = false;
                lblNotesInfo.Visible = false;
                lblInsertInfo.Visible = false;
                lblPoaInfo.Visible = false;
                DivViolation.Visible = false;
                FieldDataReset();
                DataSet dsMaster = BAL.clsBAL_Cobb_FieldDataEntry.GetTrackingPoles(ddlTrackingNo.SelectedItem.Value);
                ViewState["PoleAndCompany"] = dsMaster;
                ddl_GoToPole.DataSource = dsMaster.Tables[0];
                ddl_GoToPole.DataTextField = "Pole_No";
                ddl_GoToPole.DataValueField = "Pole_No";
                ddl_GoToPole.DataBind();
                ddl_GoToPole.Items.Insert(0, new ListItem("--Select--", "0"));
                ddl_GoToPole.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region Common
        public void GetPOAList(string Pole_Id, string TrackingNo)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["PoleDetails"];
                ViewState["POA_List"] = ds;
                rg_POA.DataSource = ds.Tables[1];
                rg_POA.DataBind();
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        public void GetMidspanList(string Pole_Id, string TrackingNo)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["PoleDetails"];
                ViewState["Midspan_List"] = ds;
                rg_Midspans.DataSource = ds.Tables[2];
                rg_Midspans.DataBind();
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        public void GetNotesList(string Pole_Id, string TrackingNo)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["PoleDetails"];
                ViewState["Notes_List"] = ds;
                rg_Notes.DataSource = ds.Tables[3];
                rg_Notes.DataBind();
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region POAGrid
        protected void rg_POA_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["POA_List"];
                rg_POA.DataSource = ds.Tables[1];
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_POA_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["POA_List"];
                DataSet dsMaster = (DataSet)ViewState["MasterData"];
                DataSet dsCompany = (DataSet)ViewState["PoleAndCompany"];
                if (e.Item is GridDataItem)
                {
                    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                    {
                        GridEditableItem item = (GridEditableItem)e.Item;
                        TextBox txtChildRecordId = (TextBox)item.FindControl("txtChildRecordId");
                        ViewState["ChildRecordId"] = txtChildRecordId.Text.ToString();
                        TextBox txtRecordId = (TextBox)item.FindControl("txtRecordId");
                        ViewState["RecordId"] = txtRecordId.Text.ToString();

                        TextBox txtPomFt = (TextBox)item["PomFt"].Controls[0];
                        TextBox txtPomIn = (TextBox)item["PomIn"].Controls[0];
                        TextBox txtNewFt = (TextBox)item["NewFt"].Controls[0];
                        TextBox txtNewIn = (TextBox)item["NewIn"].Controls[0];

                        txtPomFt.Width = 40;
                        txtPomFt.MaxLength = 2;
                         
                        txtPomFt.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                        txtPomFt.Attributes.Add("onpaste", "return false;");
                        txtPomIn.Width = 40;
                        txtPomIn.MaxLength = 2;
                        txtPomIn.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                        txtPomIn.Attributes.Add("onpaste", "return false;");
                        txtNewFt.Width = 40;
                        txtNewFt.MaxLength = 2;
                        txtNewFt.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                        txtNewFt.Attributes.Add("onpaste", "return false;");
                        txtNewIn.Width = 40;
                        txtNewIn.MaxLength = 2;
                        txtNewIn.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                        txtNewIn.Attributes.Add("onpaste", "return false;");


                        DropDownList Companylist = item.FindControl("ddlCompanyy") as DropDownList;
                        Companylist.DataSource = dsCompany.Tables[1];
                        Companylist.DataTextField = "Company";
                        Companylist.DataValueField = "Company";
                        Companylist.DataBind();

                        DropDownList Typelist = item.FindControl("ddlType") as DropDownList;
                        Typelist.DataSource = dsMaster.Tables[5];
                        Typelist.DataTextField = "Type";
                        Typelist.DataValueField = "Type";
                        Typelist.DataBind();


                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string Company = ds.Tables[1].Rows[i]["Company"].ToString().Trim();
                            string Type = ds.Tables[1].Rows[i]["Type"].ToString().Trim();
                            string childrecordid = ds.Tables[1].Rows[i]["_child_record_id"].ToString().Trim();
                            //int PomFt =Convert.ToInt32(ds.Tables[1].Rows[i]["PomFt"].ToString().Trim());
                            //int PomIn = Convert.ToInt32(ds.Tables[1].Rows[i]["PomIn"].ToString().Trim());
                            //if(PomFt==0)
                            //{
                            //    TextBox txtPomFt2 = (TextBox)item["PomFt"].Controls[0];
                            //    txtPomFt2.Text = "";
                            //}
                            //if (PomIn == 0)
                            //{
                            //    TextBox txtPomIn2 = (TextBox)item["PomIn"].Controls[0];
                            //    txtPomIn2.Text = "";
                            //}


                            for (int j = 0; j < Companylist.Items.Count; j++)
                            {
                                if (childrecordid.Trim() == txtChildRecordId.Text.Trim())
                                {
                                    if (Companylist.Items[j].Text.ToString().Trim() == Company)
                                    {
                                        Companylist.Items[j].Selected = true;
                                        break;
                                    }
                                }
                            }
                            for (int k = 0; k < Typelist.Items.Count; k++)
                            {
                                if (childrecordid == txtChildRecordId.Text.Trim())
                                {
                                    if (Typelist.Items[k].Text.ToString().Trim() == Type)
                                    {
                                        Typelist.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        GridDataItem dataItem = e.Item as GridDataItem;
                        string strChildRecordId = dataItem["child_record_id"].Text;

                        DropDownList Companylist = dataItem.FindControl("ddlCompanyy") as DropDownList;
                        Companylist.DataSource = dsCompany.Tables[1];
                        Companylist.DataTextField = "Company";
                        Companylist.DataValueField = "Company";
                        Companylist.DataBind();

                        DropDownList Typelist = dataItem.FindControl("ddlType") as DropDownList;
                        Typelist.DataSource = dsMaster.Tables[5];
                        Typelist.DataTextField = "Type";
                        Typelist.DataValueField = "Type";
                        Typelist.DataBind();


                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            string Company = ds.Tables[1].Rows[i]["Company"].ToString().Trim();
                            string Type = ds.Tables[1].Rows[i]["Type"].ToString().Trim();
                            string childrecordid = ds.Tables[1].Rows[i]["_child_record_id"].ToString().Trim();

                            for (int j = 0; j < Companylist.Items.Count; j++)
                            {
                                if (childrecordid.Trim() == strChildRecordId.Trim())
                                {
                                    if (Companylist.Items[j].Text.ToString().Trim() == Company)
                                    {
                                        Companylist.Items[j].Selected = true;
                                        break;
                                    }
                                }
                            }
                            Companylist.Enabled = false;
                            for (int k = 0; k < Typelist.Items.Count; k++)
                            {
                                if (childrecordid.Trim() == strChildRecordId.Trim())
                                {
                                    if (Typelist.Items[k].Text.ToString().Trim() == Type)
                                    {
                                        Typelist.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                            Typelist.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }

        }
        protected void rg_POA_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.IsInEditMode)
                {
                    GridEditableItem editedItem = (GridEditableItem)e.Item;
                    clsPOA objPOA = new clsPOA();
                    objPOA.TrackingId = ViewState["Tracking_No"].ToString();
                    objPOA.PoleId = ViewState["Pole_Id"].ToString();
                    objPOA.ChildRecordId = "";
                    objPOA.Company = Convert.ToString(((DropDownList)editedItem.FindControl("ddlCompanyy")).SelectedItem.Text);
                    objPOA.Type = Convert.ToString(((DropDownList)editedItem.FindControl("ddlType")).SelectedItem.Text);
                    //objPOA.POA_FT = ((TextBox)(editedItem["PomFt"].Controls[0])).Text;
                    //objPOA.POA_IN = ((TextBox)(editedItem["PomIn"].Controls[0])).Text;
                    objPOA.POA_FT = "";
                    objPOA.POA_IN = "";
                    objPOA.New_Ft = ((TextBox)(editedItem["NewFt"].Controls[0])).Text;
                    //if(Convert.ToInt32(objPOA.New_Ft)<12)
                    //{
                    //    objPOA.New_Ft = ((TextBox)(editedItem["NewFt"].Controls[0])).Text;
                    //}
                    //else
                    //{
                    //    lblPoaInfo.Visible = true;
                    //    lblPoaInfo.ForeColor = System.Drawing.Color.Red;
                    //    lblPoaInfo.Text = "Inches values should be less than 12!";
                    //}
                    objPOA.New_In = ((TextBox)(editedItem["NewIn"].Controls[0])).Text;

                    int result = BAL.clsBAL_Cobb_FieldDataEntry.POA_Create(objPOA);
                    if (result > 0)
                    {
                        GetGoToPoleDetails(objPOA.PoleId, objPOA.TrackingId);
                        lblMidspanInfo.Visible = false;
                        lblNotesInfo.Visible = false;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = true;
                        lblPoaInfo.ForeColor = System.Drawing.Color.Green;
                        lblPoaInfo.Text = "Successfully created poa!";
                    }
                    else
                    {
                        lblMidspanInfo.Visible = false;
                        lblNotesInfo.Visible = false;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = true;
                        lblPoaInfo.ForeColor = System.Drawing.Color.Red;
                        lblPoaInfo.Text = "Poa create failure!";
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_POA_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.IsInEditMode)
                {
                    GridEditableItem editedItem = (GridEditableItem)e.Item;
                    clsPOA objPOA = new clsPOA();
                    objPOA.TrackingId = ViewState["Tracking_No"].ToString();
                    objPOA.PoleId = ViewState["Pole_Id"].ToString();
                    objPOA.RecordId = ViewState["RecordId"].ToString();
                    objPOA.ChildRecordId = ViewState["ChildRecordId"].ToString();
                    objPOA.Company = Convert.ToString(((DropDownList)editedItem.FindControl("ddlCompanyy")).SelectedItem.Text);
                    objPOA.Type = Convert.ToString(((DropDownList)editedItem.FindControl("ddlType")).SelectedItem.Text);
                    objPOA.POA_FT = ((TextBox)(editedItem["PomFt"].Controls[0])).Text;
                    objPOA.POA_IN = ((TextBox)(editedItem["PomIn"].Controls[0])).Text;
                    objPOA.New_Ft = ((TextBox)(editedItem["NewFt"].Controls[0])).Text;
                    objPOA.New_In = ((TextBox)(editedItem["NewIn"].Controls[0])).Text;

                    int result = BAL.clsBAL_Cobb_FieldDataEntry.POA_Create(objPOA);
                    if (result > 0)
                    {
                        GetGoToPoleDetails(objPOA.PoleId, objPOA.TrackingId);
                        lblMidspanInfo.Visible = false;
                        lblNotesInfo.Visible = false;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = true;
                        lblPoaInfo.ForeColor = System.Drawing.Color.Green;
                        lblPoaInfo.Text = "Successfully updated poa!";
                    }
                    else
                    {
                        lblMidspanInfo.Visible = false;
                        lblNotesInfo.Visible = false;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = true;
                        lblPoaInfo.ForeColor = System.Drawing.Color.Red;
                        lblPoaInfo.Text = "Poa update failure!";
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_POA_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Session["CurrentPageIndex"] = rg_POA.CurrentPageIndex;
                if (e.CommandName == "cmdDelete")
                {
                    if (e.Item.ItemIndex >= 0)
                    {
                        string TrackingId = ViewState["Tracking_No"].ToString();
                        string PoleId = ViewState["Pole_Id"].ToString();
                        GridDataItem item = (GridDataItem)e.Item;
                        TextBox txtRecordId = (TextBox)item.FindControl("txtRecordId");
                        string RecordId = txtRecordId.Text.ToString();
                        TextBox txtChildRecordId = (TextBox)item.FindControl("txtChildRecordId");
                        string ChildRecordId = txtChildRecordId.Text.ToString();
                        int result = BAL.clsBAL_Cobb_FieldDataEntry.POA_Delete(RecordId, ChildRecordId);
                        if (result > 0)
                        {
                            GetGoToPoleDetails(PoleId, TrackingId);
                            lblMidspanInfo.Visible = false;
                            lblNotesInfo.Visible = false;
                            lblInsertInfo.Visible = false;
                            lblPoaInfo.Visible = true;
                            lblPoaInfo.ForeColor = System.Drawing.Color.Green;
                            lblPoaInfo.Text = "Pom deleted successfully!";
                        }
                        else
                        {
                            lblMidspanInfo.Visible = false;
                            lblNotesInfo.Visible = false;
                            lblInsertInfo.Visible = false;
                            lblPoaInfo.Visible = true;
                            lblPoaInfo.ForeColor = System.Drawing.Color.Red;
                            lblPoaInfo.Text = "Can not delete source records!";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        #endregion

        #region MidspanGrid
        protected void rg_Midspans_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["Midspan_List"];
                rg_Midspans.DataSource = ds.Tables[2];
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Midspans_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["Midspan_List"];
                DataSet dsMaster = (DataSet)ViewState["MasterData"];
                DataSet dsCompany = (DataSet)ViewState["PoleAndCompany"];
                if (e.Item is GridDataItem)
                {
                    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                    {
                        GridEditableItem item = (GridEditableItem)e.Item;
                        TextBox txtChildRecordId = (TextBox)item.FindControl("txtChildRecordId");
                        ViewState["Mid_ChildRecordId"] = txtChildRecordId.Text.ToString();
                        TextBox txtRecordId = (TextBox)item.FindControl("txtRecordId");
                        ViewState["Mid_RecordId"] = txtRecordId.Text.ToString();

                        TextBox txtTo = (TextBox)item["[To]"].Controls[0];
                        TextBox txtMFt = (TextBox)item["mid_ft"].Controls[0];
                        TextBox txtMIn = (TextBox)item["mid_in"].Controls[0];

                        txtTo.Width = 40;
                        txtMFt.Width = 40;
                        txtMFt.MaxLength = 2;
                        txtMFt.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                        txtMFt.Attributes.Add("onpaste", "return false;");
                        txtMIn.Width = 40;
                        txtMIn.MaxLength = 2;
                        txtMIn.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                        txtMIn.Attributes.Add("onpaste", "return false;");

                        DropDownList Companylist = item.FindControl("ddlCompanyy") as DropDownList;
                        Companylist.DataSource = dsCompany.Tables[1];
                        Companylist.DataTextField = "Company";
                        Companylist.DataValueField = "Company";
                        Companylist.DataBind();

                        DropDownList Typelist = item.FindControl("ddlType") as DropDownList;
                        Typelist.DataSource = dsMaster.Tables[5];
                        Typelist.DataTextField = "Type";
                        Typelist.DataValueField = "Type";
                        Typelist.DataBind();

                        DropDownList Overlist = item.FindControl("ddlOver") as DropDownList;
                        Overlist.DataSource = dsMaster.Tables[2];
                        Overlist.DataTextField = "Over";
                        Overlist.DataValueField = "Over";
                        Overlist.DataBind();

                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            string Company = ds.Tables[2].Rows[i]["Company"].ToString().Trim();
                            string Type = ds.Tables[2].Rows[i]["Type"].ToString().Trim();
                            string Over = ds.Tables[2].Rows[i]["Over"].ToString().Trim();
                            string childrecordid = ds.Tables[2].Rows[i]["_child_record_id"].ToString().Trim();

                            for (int j = 0; j < Companylist.Items.Count; j++)
                            {
                                if (childrecordid.Trim() == txtChildRecordId.Text.Trim())
                                {
                                    if (Companylist.Items[j].Text.ToString().Trim() == Company)
                                    {
                                        Companylist.Items[j].Selected = true;
                                        break;
                                    }
                                }
                            }
                            for (int k = 0; k < Typelist.Items.Count; k++)
                            {
                                if (childrecordid == txtChildRecordId.Text.Trim())
                                {
                                    if (Typelist.Items[k].Text.ToString().Trim() == Type)
                                    {
                                        Typelist.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }

                            for (int l = 0; l < Overlist.Items.Count; l++)
                            {
                                if (childrecordid == txtChildRecordId.Text.Trim())
                                {
                                    if (Overlist.Items[l].Text.ToString().Trim() == Over)
                                    {
                                        Overlist.Items[l].Selected = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        GridDataItem dataItem = e.Item as GridDataItem;
                        string strChildRecordId = dataItem["child_record_id"].Text;

                        DropDownList Companylist = dataItem.FindControl("ddlCompanyy") as DropDownList;
                        Companylist.DataSource = dsCompany.Tables[1];
                        Companylist.DataTextField = "Company";
                        Companylist.DataValueField = "Company";
                        Companylist.DataBind();


                        DropDownList Typelist = dataItem.FindControl("ddlType") as DropDownList;
                        Typelist.DataSource = dsMaster.Tables[5];
                        Typelist.DataTextField = "Type";
                        Typelist.DataValueField = "Type";
                        Typelist.DataBind();


                        DropDownList Overlist = dataItem.FindControl("ddlOver") as DropDownList;
                        Overlist.DataSource = dsMaster.Tables[2];
                        Overlist.DataTextField = "Over";
                        Overlist.DataValueField = "Over";
                        Overlist.DataBind();

                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            string Company = ds.Tables[2].Rows[i]["Company"].ToString().Trim();
                            string Type = ds.Tables[2].Rows[i]["Type"].ToString().Trim();
                            string Over = ds.Tables[2].Rows[i]["Over"].ToString().Trim();
                            string childrecordid = ds.Tables[2].Rows[i]["_child_record_id"].ToString().Trim();

                            for (int j = 0; j < Companylist.Items.Count; j++)
                            {
                                if (childrecordid.Trim() == strChildRecordId.Trim())
                                {
                                    if (Companylist.Items[j].Text.ToString().Trim() == Company)
                                    {
                                        Companylist.Items[j].Selected = true;
                                        break;
                                    }
                                }
                            }
                            Companylist.Enabled = false;
                            for (int k = 0; k < Typelist.Items.Count; k++)
                            {
                                if (childrecordid.Trim() == strChildRecordId.Trim())
                                {
                                    if (Typelist.Items[k].Text.ToString().Trim() == Type)
                                    {
                                        Typelist.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                            Typelist.Enabled = false;
                            for (int l = 0; l < Overlist.Items.Count; l++)
                            {
                                if (childrecordid.Trim() == strChildRecordId.Trim())
                                {
                                    if (Overlist.Items[l].Text.ToString().Trim() == Over)
                                    {
                                        Overlist.Items[l].Selected = true;
                                        break;
                                    }
                                }
                            }
                            Overlist.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Midspans_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.IsInEditMode)
                {
                    GridEditableItem editedItem = (GridEditableItem)e.Item;
                    clsMidspan objmid = new clsMidspan();
                    objmid.TrackingId = ViewState["Tracking_No"].ToString();
                    objmid.PoleId = ViewState["Pole_Id"].ToString();
                    objmid.RecordId = ViewState["Mid_RecordId"].ToString();
                    objmid.ChildRecordId = ViewState["Mid_ChildRecordId"].ToString();
                    objmid.To = ((TextBox)(editedItem["[To]"].Controls[0])).Text;
                    objmid.Company = Convert.ToString(((DropDownList)editedItem.FindControl("ddlCompanyy")).SelectedItem.Text);
                    objmid.Type = Convert.ToString(((DropDownList)editedItem.FindControl("ddlType")).SelectedItem.Text);
                    objmid.MidFt = ((TextBox)(editedItem["mid_ft"].Controls[0])).Text;
                    objmid.MidIn = ((TextBox)(editedItem["mid_in"].Controls[0])).Text;
                    objmid.Over = Convert.ToString(((DropDownList)editedItem.FindControl("ddlOver")).SelectedItem.Text);

                    int result = BAL.clsBAL_Cobb_FieldDataEntry.Midspan_Create(objmid);
                    if (result > 0)
                    {
                        GetGoToPoleDetails(objmid.PoleId, objmid.TrackingId);
                        lblMidspanInfo.Visible = true;
                        lblNotesInfo.Visible = false;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = false;
                        lblMidspanInfo.ForeColor = System.Drawing.Color.Green;
                        lblMidspanInfo.Text = "Successfully updated midspan!";
                    }
                    else
                    {
                        lblMidspanInfo.Visible = true;
                        lblNotesInfo.Visible = false;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = false;
                        lblMidspanInfo.ForeColor = System.Drawing.Color.Red;
                        lblMidspanInfo.Text = "Updated midspan failure!";
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Midspans_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.IsInEditMode)
                {
                    GridEditableItem editedItem = (GridEditableItem)e.Item;
                    clsMidspan objmid = new clsMidspan();
                    objmid.TrackingId = ViewState["Tracking_No"].ToString();
                    objmid.PoleId = ViewState["Pole_Id"].ToString();
                    objmid.ChildRecordId = "";
                    objmid.To = ((TextBox)(editedItem["[To]"].Controls[0])).Text;
                    objmid.Company = Convert.ToString(((DropDownList)editedItem.FindControl("ddlCompanyy")).SelectedItem.Text);
                    objmid.Type = Convert.ToString(((DropDownList)editedItem.FindControl("ddlType")).SelectedItem.Text);
                    objmid.MidFt = ((TextBox)(editedItem["mid_ft"].Controls[0])).Text;
                    objmid.MidIn = ((TextBox)(editedItem["mid_in"].Controls[0])).Text;
                    objmid.Over = Convert.ToString(((DropDownList)editedItem.FindControl("ddlOver")).SelectedItem.Text);

                    int result = BAL.clsBAL_Cobb_FieldDataEntry.Midspan_Create(objmid);
                    if (result > 0)
                    {
                        GetGoToPoleDetails(objmid.PoleId, objmid.TrackingId);
                        lblMidspanInfo.Visible = true;
                        lblNotesInfo.Visible = false;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = false;
                        lblMidspanInfo.ForeColor = System.Drawing.Color.Green;
                        lblMidspanInfo.Text = "Successfully created midspan!";
                    }
                    else
                    {
                        lblMidspanInfo.Visible = true;
                        lblNotesInfo.Visible = false;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = false;
                        lblMidspanInfo.ForeColor = System.Drawing.Color.Red;
                        lblMidspanInfo.Text = "Midspan create failure!";
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Midspans_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "cmdDelete")
                {
                    if (e.Item.ItemIndex >= 0)
                    {
                        string TrackingId = ViewState["Tracking_No"].ToString();
                        string PoleId = ViewState["Pole_Id"].ToString();
                        GridDataItem item = (GridDataItem)e.Item;
                        TextBox txtRecordId = (TextBox)item.FindControl("txtRecordId");
                        string RecordId = txtRecordId.Text.ToString();
                        TextBox txtChildRecordId = (TextBox)item.FindControl("txtChildRecordId");
                        string ChildRecordId = txtChildRecordId.Text.ToString();
                        int result = BAL.clsBAL_Cobb_FieldDataEntry.Midspan_Delete(RecordId, ChildRecordId);
                        if (result > 0)
                        {
                            GetGoToPoleDetails(PoleId, TrackingId);
                            lblMidspanInfo.Visible = true;
                            lblNotesInfo.Visible = false;
                            lblInsertInfo.Visible = false;
                            lblPoaInfo.Visible = false;
                            lblMidspanInfo.ForeColor = System.Drawing.Color.Green;
                            lblMidspanInfo.Text = "Midspan deleted successfully!";
                        }
                        else
                        {
                            lblMidspanInfo.Visible = true;
                            lblNotesInfo.Visible = false;
                            lblInsertInfo.Visible = false;
                            lblPoaInfo.Visible = false;
                            lblMidspanInfo.ForeColor = System.Drawing.Color.Red;
                            lblMidspanInfo.Text = "Can not delete source records!";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        #endregion

        #region Notes
        protected void rg_Notes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["Notes_List"];
                rg_Notes.DataSource = ds.Tables[3];
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Notes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["Notes_List"];
                DataSet dsMaster = BAL.clsBAL_Cobb_FieldDataEntry.MasterData_GetList();
                DataSet dsCompany = (DataSet)ViewState["PoleAndCompany"];
                if (e.Item is GridDataItem)
                {
                    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                    {
                        GridEditableItem item = (GridEditableItem)e.Item;
                        TextBox txtId = (TextBox)item.FindControl("txtId");
                        ViewState["NotesId"] = txtId.Text.ToString();

                        TextBox txtStep = (TextBox)item["Step#"].Controls[0];
                        CheckBox txtCheck = (CheckBox)item.FindControl("chkShare");
                        txtCheck.Enabled = true;
                        txtStep.Width = 40;
                        txtStep.MaxLength = 2;
                        txtStep.Attributes.Add("onkeypress", "return allowOnlyNumber(event);");
                        txtStep.Attributes.Add("onpaste", "return false;");

                        DropDownList Companylist = item.FindControl("ddlCompanyy") as DropDownList;
                        Companylist.DataSource = dsCompany.Tables[1];
                        Companylist.DataTextField = "Company";
                        Companylist.DataValueField = "Company";
                        Companylist.DataBind();


                        RadComboBox Noteslist = item.FindControl("ddlNotes") as RadComboBox;
                        Noteslist.DataSource = dsMaster.Tables[6];
                        Noteslist.DataTextField = "NotesDesc";
                        Noteslist.DataValueField = "NotesDesc";
                        Noteslist.DataBind();

                        for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                        {
                            string Company = ds.Tables[3].Rows[i]["Company"].ToString().Trim();
                            string Notes = ds.Tables[3].Rows[i]["Notes"].ToString().Trim();
                            string ID = ds.Tables[3].Rows[i]["Id"].ToString().Trim();

                            for (int j = 0; j < Companylist.Items.Count; j++)
                            {
                                if (ID.Trim() == txtId.Text.Trim())
                                {
                                    if (Companylist.Items[j].Text.ToString().Trim() == Company)
                                    {
                                        Companylist.Items[j].Selected = true;
                                        break;
                                    }
                                }
                            }
                            for (int k = 0; k < Noteslist.Items.Count; k++)
                            {
                                if (ID.Trim() == txtId.Text.Trim())
                                {
                                    if (Noteslist.Items[k].Text.ToString().Trim() == Notes)
                                    {
                                        Noteslist.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        GridDataItem dataItem = e.Item as GridDataItem;
                        TextBox txtId = (TextBox)dataItem.FindControl("txtId");
                        DropDownList Companylist = dataItem.FindControl("ddlCompanyy") as DropDownList;
                        Companylist.DataSource = dsCompany.Tables[1];
                        Companylist.DataTextField = "Company";
                        Companylist.DataValueField = "Company";
                        Companylist.DataBind();


                        RadComboBox Noteslist = dataItem.FindControl("ddlNotes") as RadComboBox;
                        Noteslist.DataSource = dsMaster.Tables[6];
                        Noteslist.DataTextField = "NotesDesc";
                        Noteslist.DataValueField = "NotesDesc";
                        Noteslist.DataBind();

                        for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                        {
                            string Company = ds.Tables[3].Rows[i]["Company"].ToString().Trim();
                            string Notes = ds.Tables[3].Rows[i]["Notes"].ToString().Trim();
                            string ID = ds.Tables[3].Rows[i]["Id"].ToString().Trim();
                            for (int j = 0; j < Companylist.Items.Count; j++)
                            {
                                if (ID.Trim() == txtId.Text.Trim())
                                {
                                    if (Companylist.Items[j].Text.ToString().Trim() == Company)
                                    {
                                        Companylist.Items[j].Selected = true;
                                        break;
                                    }
                                }
                            }
                            Companylist.Enabled = false;
                            for (int k = 0; k < Noteslist.Items.Count; k++)
                            {
                                if (ID.Trim() == txtId.Text.Trim())
                                {
                                    if (Noteslist.Items[k].Text.ToString().Trim() == Notes)
                                    {
                                        Noteslist.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                            Noteslist.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Notes_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.IsInEditMode)
                {
                    GridEditableItem editedItem = (GridEditableItem)e.Item;
                    clsNotes objNotes = new clsNotes();
                    objNotes.TrackingId = ViewState["Tracking_No"].ToString();
                    objNotes.PoleId = ViewState["Pole_Id"].ToString();
                    objNotes.Id = Convert.ToInt32(ViewState["NotesId"].ToString());
                    objNotes.Company = Convert.ToString(((DropDownList)editedItem.FindControl("ddlCompanyy")).SelectedItem.Text);
                    objNotes.Notes = Convert.ToString(((RadComboBox)editedItem.FindControl("ddlNotes")).Text);
                    if (((TextBox)(editedItem["Step#"].Controls[0])).Text != "")
                    {
                        objNotes.Step = Convert.ToInt32(((TextBox)(editedItem["Step#"].Controls[0])).Text);
                    }
                    else
                    {
                        objNotes.Step = 0;
                    }
                    objNotes.Share = Convert.ToInt32(((CheckBox)(editedItem["Share"].Controls[1])).Checked);

                    int result = BAL.clsBAL_Cobb_FieldDataEntry.Notes_Create(objNotes);
                    if (result > 0)
                    {
                        GetGoToPoleDetails(objNotes.PoleId, objNotes.TrackingId);
                        lblMidspanInfo.Visible = false;
                        lblNotesInfo.Visible = true;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = false;
                        lblNotesInfo.ForeColor = System.Drawing.Color.Green;
                        lblNotesInfo.Text = "Successfully updated notes!";
                    }
                    else
                    {
                        lblMidspanInfo.Visible = false;
                        lblNotesInfo.Visible = true;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = false;
                        lblNotesInfo.ForeColor = System.Drawing.Color.Red;
                        lblNotesInfo.Text = "Notes update failure!";
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Notes_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.Item.IsInEditMode)
                {
                    GridEditableItem editedItem = (GridEditableItem)e.Item;
                    clsNotes objNotes = new clsNotes();
                    objNotes.TrackingId = ViewState["Tracking_No"].ToString();
                    objNotes.PoleId = ViewState["Pole_Id"].ToString();
                    objNotes.Id = 0;
                    objNotes.Company = Convert.ToString(((DropDownList)editedItem.FindControl("ddlCompanyy")).SelectedItem.Text);
                    objNotes.Notes = Convert.ToString(((RadComboBox)editedItem.FindControl("ddlNotes")).Text);
                    if (((TextBox)(editedItem["Step#"].Controls[0])).Text != "")
                    {
                        objNotes.Step = Convert.ToInt32(((TextBox)(editedItem["Step#"].Controls[0])).Text);
                    }
                    else
                    {
                        objNotes.Step = 0;
                    } 
                    objNotes.Share = Convert.ToInt32(((CheckBox)(editedItem["Share"].Controls[1])).Checked);

                    int result = BAL.clsBAL_Cobb_FieldDataEntry.Notes_Create(objNotes);
                    if (result > 0)
                    {
                        GetGoToPoleDetails(objNotes.PoleId, objNotes.TrackingId);
                        lblMidspanInfo.Visible = false;
                        lblNotesInfo.Visible = true;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = false;
                        lblNotesInfo.ForeColor = System.Drawing.Color.Green;
                        lblNotesInfo.Text = "Successfully created notes!";
                    }
                    else
                    {
                        lblMidspanInfo.Visible = false;
                        lblNotesInfo.Visible = true;
                        lblInsertInfo.Visible = false;
                        lblPoaInfo.Visible = false;
                        lblNotesInfo.ForeColor = System.Drawing.Color.Red;
                        lblNotesInfo.Text = "Notes creation failure!";
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        protected void rg_Notes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "cmdDelete")
                {
                    if (e.Item.ItemIndex >= 0)
                    {
                        string TrackingId = ViewState["Tracking_No"].ToString();
                        string PoleId = ViewState["Pole_Id"].ToString();
                        GridDataItem item = (GridDataItem)e.Item;
                        int Id = Convert.ToInt32(item["Id"].Text);
                        int result = BAL.clsBAL_Cobb_FieldDataEntry.Notes_Delete(Id, TrackingId, PoleId);
                        if (result > 0)
                        {
                            GetGoToPoleDetails(PoleId, TrackingId);
                            lblMidspanInfo.Visible = false;
                            lblNotesInfo.Visible = true;
                            lblInsertInfo.Visible = false;
                            lblPoaInfo.Visible = false;
                            lblNotesInfo.ForeColor = System.Drawing.Color.Green;
                            lblNotesInfo.Text = "Notes deleted successfully!";
                        }
                        else
                        {
                            lblMidspanInfo.Visible = false;
                            lblNotesInfo.Visible = true;
                            lblInsertInfo.Visible = false;
                            lblPoaInfo.Visible = false;
                            lblNotesInfo.ForeColor = System.Drawing.Color.Red;
                            lblNotesInfo.Text = "Notes delete failure!";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region Field Data Reset
        public void FieldDataReset()
        {
            //txt_Component_Id.Text = "";
            ddl_Owner.ClearSelection();
            txt_Location.Text = "";
            txt_Station.Text = "";
            txtPowerMap.Text = "";
            txtCommMap.Text = "";
            txtNJUNS.Text = "";
            txt_HtClass_ExistingFT.Text = "";
            txt_HtClass_ExistingInch.Text = "";
            //txt_HtClass_NewFT.Text = "";
            //txt_HtClass_NewInch.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
            txt_FieldNotes.Text = "";
            txtEngNotes.Text = "";
            RadDate.Clear();
            txt_Temp.Text = "";
            txtConstructionCost.Text = "";
            chkPoleComplete.Checked = false;

            rg_POA.DataSource = new string[] { };
            rg_POA.DataBind();

            rg_Midspans.DataSource = new string[] { };
            rg_Midspans.DataBind();

            rg_Notes.DataSource = new string[] { };
            rg_Notes.DataBind();

            lblPoaInfo.Visible = false;
            lblMidspanInfo.Visible = false;
            lblNotesInfo.Visible = false;
            lblInsertInfo.Visible = false;

        }
        #endregion

        #region CheckViolations
        protected void btn_CheckViolations_Click(object sender, EventArgs e)
        {
            try
            {
                DivViolation.Visible = true;
                string TrackingId = ddlTrackingNo.SelectedItem.Value;
                string PoleId = ddl_GoToPole.SelectedItem.Value;
                txt_Pole.Text = ddl_GoToPole.SelectedItem.Value;

                DataSet ds = BAL.clsBAL_Cobb_FieldDataEntry.CheckViolation(TrackingId, PoleId);
                DataTable dt1 = ds.Tables[0];
                Rad40InchVio.DataSource = dt1;
                Rad40InchVio.DataBind();

                DataTable dt2 = ds.Tables[1];
                Rad30InchVio.DataSource = dt2;
                Rad30InchVio.DataBind();

                DataTable dt3 = ds.Tables[2];
                Rad12InchVio.DataSource = dt3;
                Rad12InchVio.DataBind();

                DataTable dt4 = ds.Tables[3];
                Rad04InchVio.DataSource = dt4;
                Rad04InchVio.DataBind();

                DataSet dsMid = BAL.clsBAL_Cobb_FieldDataEntry.CheckViolationMidspan(TrackingId, PoleId);
                DataTable dt5 = dsMid.Tables[0];
                Rad27GC.DataSource = dt5;
                Rad27GC.DataBind();

                DataTable dt6 = dsMid.Tables[1];
                Rad18GCHigh.DataSource = dt6;
                Rad18GCHigh.DataBind();

                DataTable dt7 = dsMid.Tables[2];
                Rad156GCTrav.DataSource = dt7;
                Rad156GCTrav.DataBind();

                DataTable dt8 = dsMid.Tables[3];
                Rad13GCRural.DataSource = dt8;
                Rad13GCRural.DataBind();

                DataTable dt9 = dsMid.Tables[4];
                Rad96GCPedOnly.DataSource = dt9;
                Rad96GCPedOnly.DataBind();

                DataTable dt10 = dsMid.Tables[5];
                RadMSSeparation30.DataSource = dt10;
                RadMSSeparation30.DataBind();

                DataTable dt11 = dsMid.Tables[6];
                RadMSSeparation12.DataSource = dt11;
                RadMSSeparation12.DataBind();
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        #endregion

    }
}