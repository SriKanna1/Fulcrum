using FulCrum.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fulcrum.Common;
using Fulcrum.BAL;
using System.Net;
using System.IO;
using System.Net.Http;

namespace FulCrum
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //string Location = "Location";
                    //string Station = "Station";
                    //string PowerMap = "PowerMap";
                    //string Commmap = "CommMap";
                    //string NJUNS = "NJUNSNo";

                    //string UpdatePath = "https://api.fulcrumapp.com/api/v2/records/38e80a1e-2591-4f9d-8781-26840e3bc900.json";
                    //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(UpdatePath);
                    ////byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(username + ":" + pwd);
                    ////string credentials = System.Convert.ToBase64String(toEncodeAsBytes);
                    //httpWebRequest.Method = "POST";
                    //httpWebRequest.UserAgent = "WSDAPI";
                    //httpWebRequest.ContentType = "application/json; charset=utf-8";
                    //// httpWebRequest.Headers.Add("Authorization", credentials);
                    //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    //{
                    //    string json = "\"record\": {"
                    //          + "\"Location\" : \"" + Location + "\","
                    //          + "\"Station\" : \"" + Station + "\","
                    //          + "\"PowerMap\" : \"" + PowerMap + "\","
                    //          + "\"CommMap\" : \"" + Commmap + "\","
                    //          + "\"NJUNSNo\" : \"" + NJUNS + "\"}";
                    //    streamWriter.Write(json);
                    //    streamWriter.Flush();
                    //    streamWriter.Close();
                    //}                   
                    //HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    //{
                    //    string responseStr = streamReader.ReadToEnd();
                    //} 
                }
                catch (Exception exp)
                {
                    //DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
                }
            }
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = "Test Results.xlsx";
                string FilePath = string.Concat(Server.MapPath("~/VerizonExport/" + FileName));
                Response.Buffer = false;
                Response.ClearHeaders();
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.xlsx");
                // Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                //Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", FileName));
                //Response.AddHeader("Content-Length", FileName.Length.ToString());
                Response.TransmitFile(FilePath);
                Response.Close();
            }
            catch (Exception exp)
            {
                // DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string UpdatePath = "https://api.fulcrumapp.com/api/v2/records/1c374e8a-b5d3-426e-9f90-1955a24c6921.json?token=9e7a63ab3ad0906cfca9077c652ffa28a64c123571cd32444434a24a01c55af3f44ad0983d223cfb";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.fulcrumapp.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = client.DeleteAsync(UpdatePath).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Write("Success");
            }
            else
                Console.Write("Error");

        }
    }
}