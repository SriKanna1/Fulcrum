using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fulcrum.Common;
using Fulcrum.BAL;
using System.Data;
using Telerik.Web.UI;
using FulCrum.BAL;
using Spire.Xls;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Drawing;
using System.Configuration;
using System.IO.Compression;

namespace FulCrum
{
    public partial class VeirzonFormUpdate : BasePage
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            HideErrorTable(tr_ErrorRow, lblError, lblInfo);
            if (HttpContext.Current.Session.IsNewSession)
            {
                Response.Redirect("https://login.microsoftonline.com/my-azure-ad-guid/oauth2/logout");
            }
            if (!IsPostBack)
            {
                try
                {
                    EwoList();
                    RadComboewo_SelectedIndexChanged(sender, null);
                    lnkDownloadAllImage.Visible = false;
                }
                catch (Exception exp)
                {
                    DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
                }
            }
        }
        #endregion

        #region EwoList
        public void EwoList()
        {
            try
            {
                DataSet ds = cls_BAL_Verizon_Maintenance.GetEwoList();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RadComboewo.DataSource = ds;
                    RadComboewo.DataTextField = "Ewo";
                    RadComboewo.DataValueField = "Ewo";
                    RadComboewo.DataBind();
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region LnkExport_Click
        protected void LnkExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (RadComboewo.SelectedValue != "")
                {
                    string ewo = RadComboewo.SelectedValue;
                    string[] ewo3 = ewo.Split('(');
                    ewo = ewo3[0].ToString().Trim();
                    DataSet ds = cls_BAL_Verizon_Maintenance.GetPoleDetails(ewo);

                    string Standardpath = string.Concat(Server.MapPath("~/VerizonExport/form3FsResults_FINAL.XLS"));

                    string InsertPath = string.Concat(Server.MapPath("~/VerizonExport/"));
                    string fileExtension = System.IO.Path.GetExtension(Standardpath);

                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(@Standardpath);
                    Worksheet Polesheet = workbook.Worksheets[2];
                    Worksheet Attachmentsheet = workbook.Worksheets[3];
                    Worksheet ExistingAttachersheet = workbook.Worksheets[5];
                    Worksheet FormFSRHeadersheet = workbook.Worksheets[0];
                    Worksheet RenamePicturesheet = workbook.Worksheets[6];
                    Worksheet PoleSurveysheet = workbook.Worksheets[7];

                    //Pole
                    var UniqueRows = ds.Tables[0].AsEnumerable().Distinct(DataRowComparer.Default);
                    DataTable dtPole = UniqueRows.CopyToDataTable();

                    foreach (DataRow row in dtPole.Rows)
                    {
                        int k = 2 + dtPole.Rows.IndexOf(row);
                        Polesheet.Copy(Polesheet.Range["A" + k + ":AS" + k], Polesheet.Range["A" + (k + 1) + ":AS" + (k + 1)], true);
                        Polesheet.Copy(Polesheet.Range[string.Format("A" + k + ":AS" + k)], Polesheet.Range[string.Format("A" + (k + 1) + ":AS" + (k + 1))], true);

                        object[] array = row.ItemArray;
                        for (int i = 0; i < array.Length; i++)
                        {
                            Polesheet.Range[k, i + 1].Text = array[i].ToString().Trim();
                        }
                    }

                    //Attachment
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        int k = 2 + ds.Tables[1].Rows.IndexOf(row);
                        Attachmentsheet.Copy(Attachmentsheet.Range["A" + k + ":AS" + k], Attachmentsheet.Range["A" + (k + 1) + ":AS" + (k + 1)], true);
                        Attachmentsheet.Copy(Attachmentsheet.Range[string.Format("A" + k + ":AS" + k)], Attachmentsheet.Range[string.Format("A" + (k + 1) + ":AS" + (k + 1))], true);

                        object[] array = row.ItemArray;
                        for (int i = 0; i < array.Length; i++)
                        {
                            Attachmentsheet.Range[k, i + 1].Text = array[i].ToString().Trim();
                        }
                    }

                    //ExistingAttacherMrInfo
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        int k = 2 + ds.Tables[2].Rows.IndexOf(row);
                        ExistingAttachersheet.Copy(ExistingAttachersheet.Range["A" + k + ":AS" + k], ExistingAttachersheet.Range["A" + (k + 1) + ":AS" + (k + 1)], true);
                        ExistingAttachersheet.Copy(ExistingAttachersheet.Range[string.Format("A" + k + ":AS" + k)], ExistingAttachersheet.Range[string.Format("A" + (k + 1) + ":AS" + (k + 1))], true);

                        object[] array = row.ItemArray;
                        for (int i = 0; i < array.Length; i++)
                        {
                            ExistingAttachersheet.Range[k, i + 1].Text = array[i].ToString().Trim();
                        }
                    }
                    //Form 3 FSR Header
                    foreach (DataRow row in ds.Tables[3].Rows)
                    {
                        int k = 29;

                        FormFSRHeadersheet.Copy(FormFSRHeadersheet.Range["A" + k + ":AS" + k], FormFSRHeadersheet.Range["A" + (k + 1) + ":AS" + (k + 1)], true);
                        FormFSRHeadersheet.Copy(FormFSRHeadersheet.Range[string.Format("A" + k + ":AS" + k)], FormFSRHeadersheet.Range[string.Format("A" + (k + 1) + ":AS" + (k + 1))], true);

                        object[] array = row.ItemArray;
                        for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                        {
                            FormFSRHeadersheet.Range[k, 2].Text = ds.Tables[3].Rows[i]["ewo"].ToString();
                            FormFSRHeadersheet.Range[k + 1, 2].Text = ds.Tables[3].Rows[i]["MakeReadyRequired"].ToString();
                            FormFSRHeadersheet.Range[k + 2, 2].Text = ds.Tables[3].Rows[i]["Pole_Replacement"].ToString();
                            FormFSRHeadersheet.Range[k + 3, 2].Text = ds.Tables[3].Rows[i]["Total_poles_needing_make_ready"].ToString();
                            FormFSRHeadersheet.Range[k + 4, 2].Text = ds.Tables[3].Rows[i]["Total_poles_actually_surveyed"].ToString();
                            //FormFSRHeadersheet.Range[k + 4, 2].Text = ds.Tables[3].Rows.Count.ToString();
                        }
                    }
                    //Rename Pictures
                    foreach (DataRow row in ds.Tables[4].Rows)
                    {
                        int k = 2 + ds.Tables[4].Rows.IndexOf(row);
                        RenamePicturesheet.Copy(RenamePicturesheet.Range["A" + k + ":AS" + k], RenamePicturesheet.Range["A" + (k + 1) + ":AS" + (k + 1)], true);
                        RenamePicturesheet.Copy(RenamePicturesheet.Range[string.Format("A" + k + ":AS" + k)], RenamePicturesheet.Range[string.Format("A" + (k + 1) + ":AS" + (k + 1))], true);

                        object[] array = row.ItemArray;
                        for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                        {
                            RenamePicturesheet.Range[i + 2, 1].Text = InsertPath + ds.Tables[4].Rows[i]["FulcrumId"].ToString() + ".jpg";
                            RenamePicturesheet.Range[i + 2, 2].Text = InsertPath + ds.Tables[4].Rows[i]["seqNo"].ToString() + "_" + ds.Tables[4].Rows[i]["ewo"].ToString() + "_P" + ds.Tables[4].Rows[i]["PoleTag"].ToString() + "_" + ds.Tables[4].Rows[i]["PictureType"].ToString() + ".jpg";
                        }
                    }
                    //PoleSurvey
                    foreach (DataRow row in ds.Tables[5].Rows)
                    {
                        PoleSurveysheet.Range[3, 1].Text = ds.Tables[5].Rows[0]["ewo"].ToString();
                        int k = 5 + ds.Tables[5].Rows.IndexOf(row);
                        PoleSurveysheet.Copy(PoleSurveysheet.Range["A" + k + ":AS" + k], PoleSurveysheet.Range["A" + (k + 1) + ":AS" + (k + 1)], true);
                        PoleSurveysheet.Copy(PoleSurveysheet.Range[string.Format("A" + k + ":AS" + k)], PoleSurveysheet.Range[string.Format("A" + (k + 1) + ":AS" + (k + 1))], true);
                        object[] array = row.ItemArray;
                        for (int i = 0; i < array.Length - 1; i++)
                        {
                            string DataValue = " " + array[i].ToString().TrimStart(',', ' ');
                            List<string> uniqueValues = DataValue.Split(',').Distinct().ToList();
                            string str = string.Empty;
                            foreach (var item in uniqueValues)
                                str = str + item + ",";
                            str = str.Remove(str.Length - 1);
                            str = str.TrimStart(' ');
                            str = str.Replace("  ,", ",");
                            str = str.Replace(" ,", ",");
                            str = str.Replace(", ", ",");
                            str = str.Replace(",,", ",");
                            str = str.TrimEnd(',');
                            PoleSurveysheet.Range[k, i + 1].Text = str.Replace(",", "" + Environment.NewLine); 
                        }
                    }

                    string date = DateTime.Now.ToString("MMddyyyyhhssmm");  
                    string FileName = InsertPath + "FormDetails" + date + ".xlsx";
                    Session["DownloadFile"] = "FormDetails" + date + ".xlsx";
                    workbook.SaveToFile(FileName, ExcelVersion.Version2016);

                    //RadComboewo.ClearSelection();
                    DisplayError(tr_ErrorRow, lblInfo, "Successfully exported the file..!");
                    lnkDownload.Enabled = true;

                    //this.Response.ContentType = "application/vnd.ms-excel";
                    //this.Response.AddHeader("Content-Disposition", "attachment; filename=" + "FormDetails.xlsx" + "");

                    //this.Response.TransmitFile(FileName);
                    //this.Response.End(); 
                }
                else
                {
                    DisplayError(tr_ErrorRow, lblError, "Please select ewo!");
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region lnkDownload_Click
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = Session["DownloadFile"].ToString();
                string FilePath = string.Concat(Server.MapPath("~/VerizonExport/" + FileName));
                Response.Buffer = false;
                Response.ClearHeaders();
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                Response.ClearContent();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                Response.TransmitFile(FilePath);
                Response.Close();
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        } 
        #endregion

        #region RadComboewo_SelectedIndexChanged
        protected void RadComboewo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (RadComboewo.SelectedValue != "")
                {
                    lnkDownloadAllImage.Visible = true;
                    string ewo = RadComboewo.SelectedValue;
                    string[] ewo3 = ewo.Split('(');
                    ewo = ewo3[0].ToString().Trim();
                    DataSet ds = cls_BAL_Verizon_Maintenance.GetPoleMapDetails(ewo);
                    DataTable dt = ds.Tables[0];
                    rptMarkers.DataSource = dt;
                    rptMarkers.DataBind();

                    DataSet ds1 = cls_BAL_Verizon_Maintenance.GetewoPictures(ewo);
                    rgImage.DataSource = ds1;
                    ViewState["PictureURL"] = ds1;
                    rgImage.DataBind();
                }
                else
                {
                    DataSet ds = cls_BAL_Verizon_Maintenance.GetPoleMapDetails("");
                    DataTable dt = ds.Tables[0];
                    rptMarkers.DataSource = dt;
                    rptMarkers.DataBind();
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }

        } 
        #endregion

        #region GridEvents
        protected void rgImage_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "cmdLink")
                {
                    string MainURL = (ConfigurationManager.AppSettings["MainURL"]);
                    string Token = (ConfigurationManager.AppSettings["VerizonToken"]);
                    GridDataItem ditem = (GridDataItem)e.Item;
                    if (ditem["pictureName"].Text.ToString() == "" || ditem["pictureName"].Text.ToString() == null || ditem["pictureName"].Text.ToString() == "&nbsp;")
                    {
                        DisplayError(tr_ErrorRow, lblError, "No Image!!");
                    }
                    else
                    {
                        string ImageId = ditem["pictureName"].Text.ToString().TrimStart('[', ' ').TrimEnd(']', ' ').TrimStart('\"', ' ').TrimEnd('\"', ' ').Trim();
                        string ImageURL = MainURL + ImageId + "?token=" + Token + "";
                        WebClient client = new WebClient();
                        Stream stream = client.OpenRead(ImageURL);
                        Bitmap bitmap; bitmap = new Bitmap(stream);

                        string FileName = ditem["Rename"].Text;

                        if (bitmap != null)
                            bitmap.Save(Server.MapPath("~/VerizonPoleImages/" + FileName), ImageFormat.Png);
                        bitmap.Dispose();

                        string FilePath = string.Concat(Server.MapPath("~/VerizonPoleImages/" + FileName));
                        Response.Buffer = false;
                        Response.ClearHeaders();
                        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                        Response.ClearContent();
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                        Response.TransmitFile(FilePath);
                        Response.Close();

                        stream.Flush();
                        stream.Close();
                        client.Dispose();
                    }

                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        protected void rgImage_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["PictureURL"];
                rgImage.DataSource = ds;
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        protected void rgImage_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                string MainURL = (ConfigurationManager.AppSettings["MainURL"]);
                string Token = (ConfigurationManager.AppSettings["VerizonToken"]);
                DataSet ds = (DataSet)ViewState["PictureURL"];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GridDataItem item = e.Item as GridDataItem;
                    if (e.Item is GridDataItem)
                    {
                        string ImageId = item["pictureName"].Text.ToString().TrimStart('[', ' ').TrimEnd(']', ' ').TrimStart('\"', ' ').TrimEnd('\"', ' ').Trim();
                        if (ds.Tables[0].Rows[i]["pictureName"].ToString().TrimStart('[', ' ').TrimEnd(']', ' ').TrimStart('\"', ' ').TrimEnd('\"', ' ').Trim() == ImageId)
                        {
                            System.Web.UI.WebControls.HyperLink link2 = (System.Web.UI.WebControls.HyperLink)item["PhotoAPI"].Controls[0];
                            link2.ImageUrl = MainURL + ImageId + "?token=" + Token + "";
                            link2.NavigateUrl = link2.ImageUrl;
                            link2.ImageWidth = 80;
                            link2.ImageHeight = 80;
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

        #region lnkDownloadAllImage_Click
        protected void lnkDownloadAllImage_Click(object sender, EventArgs e)
        {
            try
            {
                Array.ForEach(Directory.GetFiles(Server.MapPath("~/VerizonAllPoleImages/")), File.Delete);
                Array.ForEach(Directory.GetFiles(Server.MapPath("~/VerizonZipAllPoleImages/")), File.Delete);

                DataSet ds = (DataSet)ViewState["PictureURL"];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string MainURL = (ConfigurationManager.AppSettings["MainURL"]);
                    string Token = (ConfigurationManager.AppSettings["VerizonToken"]);
                    if (ds.Tables[0].Rows[i]["pictureName"].ToString() == "" || ds.Tables[0].Rows[i]["pictureName"].ToString() == null || ds.Tables[0].Rows[i]["pictureName"].ToString() == "&nbsp;")
                    {
                    }
                    else
                    {
                        string ImageId = ds.Tables[0].Rows[i]["pictureName"].ToString().TrimStart('[', ' ').TrimEnd(']', ' ').TrimStart('\"', ' ').TrimEnd('\"', ' ').Trim();
                        string ImageURL = MainURL + ImageId + "?token=" + Token + "";
                        WebClient client = new WebClient();
                        Stream stream = client.OpenRead(ImageURL);
                        Bitmap bitmap; bitmap = new Bitmap(stream);

                        string FileName = ds.Tables[0].Rows[i]["Rename"].ToString();

                        if (bitmap != null)
                            bitmap.Save(Server.MapPath("~/VerizonAllPoleImages/" + FileName), ImageFormat.Png);
                        bitmap.Dispose();

                        stream.Flush();
                        stream.Close();
                        client.Dispose();
                    }
                }
                string startPath = Server.MapPath("~/VerizonAllPoleImages/");//folder to add
                string zipPath = Server.MapPath("~/VerizonZipAllPoleImages/AllImages.zip");//URL for your ZIP file
                ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);

                string FilePath = zipPath;
                string ZipFileName = "AllImages.zip";
                Response.Buffer = false;
                Response.ClearHeaders();
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                Response.ClearContent();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + ZipFileName + "\"");
                Response.TransmitFile(FilePath);
                Response.Close();
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        } 
        #endregion
    }
}