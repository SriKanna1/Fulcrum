using System;
using System.Web.Configuration;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for clsConfiguration
/// </summary>
public class clsConfiguration
{


    public const string ConnectionString_Key = "ConnectionString";
    
    

    private static volatile clsConfiguration _instance = null;
    private string strConnectString;    
   
    #region clsConfiguration
    private clsConfiguration()
    {
        strConnectString = WebConfigurationManager.AppSettings[ConnectionString_Key];
        if (strConnectString == null)
            throw new ApplicationException(string.Format("No exist {0} in {1}.", ConnectionString_Key, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));

      


    }
    #endregion

    #region CurrentConfig
    public static clsConfiguration CurrentConfig
    {
        get
        {
            if (_instance == null)
            {
                lock (typeof(clsConfiguration))
                {
                    if (_instance == null)
                    {
                        _instance = new clsConfiguration();
                    }
                }
            }
            return _instance;
        }
    }
    #endregion


    #region ConnectionString
    public string ConnectionString
    {
        get
        {
            return strConnectString;
        }
    }
    #endregion

   
}