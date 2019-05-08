using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FulCrum.DAL
{
    public class cls_DAL_Verizon_Maintenance
    {
        #region GetEwoList
        public static DataSet GetEwoList()
        {
            string dsn = ConfigurationSettings.AppSettings["VerizonConnectionString"].ToString();
            string cmd = "SP_VERIZON_GET_EWO_LIST";
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd);
            return ds;
        }
        #endregion

        #region GetPoleDetails
        public static DataSet GetPoleDetails(string Ewo)
        {
            string dsn = ConfigurationSettings.AppSettings["VerizonConnectionString"].ToString();
            string cmd = "SP_VERIZON_GET_POLE_DETAILS";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@EWO",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = Ewo;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion

        #region GetPoleMapDetails
        public static DataSet GetPoleMapDetails(string Ewo)
        {
            string dsn = ConfigurationSettings.AppSettings["VerizonConnectionString"].ToString();
            string cmd = "SP_VERIZON_GET_MAP_POLE_DETAILS";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@EWO",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = Ewo;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion

        #region GetewoPictures
        public static DataSet GetewoPictures(string Ewo)
        {
            string dsn = ConfigurationSettings.AppSettings["VerizonConnectionString"].ToString();
            string cmd = "SP_VERIZON_GET_EWO_PICTURES";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@EWO",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = Ewo;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion
    }
}