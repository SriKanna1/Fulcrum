using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Telerik.Web.UI;
using System.Text;

namespace Fulcrum.Common
{
    public class BasePage : System.Web.UI.Page
    {
        #region Display Error
        public void DisplayError(System.Web.UI.HtmlControls.HtmlGenericControl tblError, Label lbl, string strMessage)
        {
            lbl.Text = strMessage;
            lbl.Visible = true;
            tblError.Visible = true;
        }
        #endregion Display Error

        #region Hide Error
        public void HideErrorTable(System.Web.UI.HtmlControls.HtmlGenericControl tblError, Label lblError, Label lblInfo)
        {
            lblError.Visible = false;
            tblError.Visible = false;
            lblInfo.Visible = false;
            lblInfo.Text = "";
            lblError.Text = "";
        }
        #endregion Hide Error

        public string Encryptdata(string password)
        {
            // Encrypt the given string data with UTF8 format.
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

    }

    public static class SingleTon
    {
        public static void ClearCache()
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();
        }

        public static void CleanTemporaryFolders()
        {
            //String tempFolder = Environment.ExpandEnvironmentVariables("%TEMP%");
            //String prefetch = Environment.ExpandEnvironmentVariables("%SYSTEMROOT%") + "\\Prefetch";
            // EmptyFolderContents(tempFolder);
            //EmptyFolderContents(prefetch);
        }

        public static void EmptyFolderContents(string folderName)
        {
            foreach (var folder in Directory.GetDirectories(folderName))
            {
                try
                {
                    Directory.Delete(folder, true);
                }
                catch (Exception excep)
                {
                    System.Diagnostics.Debug.WriteLine(excep);
                }
            }
            foreach (var file in Directory.GetFiles(folderName))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception excep)
                {
                    System.Diagnostics.Debug.WriteLine(excep);
                }
            }
        }
    }
}
