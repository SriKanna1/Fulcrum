using Fulcrum.BAL;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Fulcrum.Common;


namespace DAL
{
    public class cls_DAL_JobData
    {
        #region JobMasterData_GetList
        public static DataSet JobMasterData_GetList()
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_JOB_MASTER_DATA_GETLIST";
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd);
            return ds;
        }
        #endregion

        #region GetPermiteeList
        public static DataSet GetPermiteeList(string TrackingId)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_JOB_PERMITEE_GETLIST";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@TrackingId",SqlDbType.VarChar,50),
            };
            commandParameters[0].Value = TrackingId;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion

        #region GetJobDetails
        public static DataSet GetJobDetails(string TrackingId)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_JOB_GETDETAILS";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@tracking_no",SqlDbType.VarChar,50)
            };
            commandParameters[0].Value = TrackingId;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion

        #region JobData_Insert
        public static int JobData_Insert(clsJobData objJobData)
        {
            SqlConnection sqlConn;
            SqlTransaction sqlTran;

            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            sqlConn = new SqlConnection(dsn);
            int result = 0;
            int Transactionflag = 0;
            sqlConn.Open();
            sqlTran = sqlConn.BeginTransaction("Whole");

            try
            {
                string cmd = "SP_JOB_INSERT";
                SqlParameter[] commandParameters =
            {
                new SqlParameter("@JobName",SqlDbType.VarChar,100),
                new SqlParameter("@TrackingId",SqlDbType.VarChar,50),
                new SqlParameter("@Reference",SqlDbType.VarChar,50),
                new SqlParameter("@Workorder",SqlDbType.VarChar,50),
                new SqlParameter("@PikeJob",SqlDbType.VarChar,100),
                new SqlParameter("@Engineer",SqlDbType.VarChar,100),
                new SqlParameter("@City",SqlDbType.VarChar,100),
                new SqlParameter("@County",SqlDbType.VarChar,100),
                new SqlParameter("@Region",SqlDbType.VarChar,100),
                new SqlParameter("@Headquarters",SqlDbType.VarChar,100),
                new SqlParameter("@JobType",SqlDbType.VarChar,100),
                new SqlParameter("@Startdate",SqlDbType.DateTime),
                new SqlParameter("@QCEngineer",SqlDbType.VarChar,100),
                new SqlParameter("@QCDate",SqlDbType.DateTime),
                new SqlParameter("@NJUNSCode",SqlDbType.VarChar,500),
                new SqlParameter("@NJUNSProjNum",SqlDbType.VarChar,100),
                new SqlParameter("@FieldEngineer",SqlDbType.VarChar,100),
                new SqlParameter("@FieldEngineerDate",SqlDbType.DateTime),
            };

                commandParameters[0].Value = objJobData.JobName;
                commandParameters[1].Value = objJobData.TrackingId;
                commandParameters[2].Value = objJobData.Reference;
                commandParameters[3].Value = objJobData.Workorder;
                commandParameters[4].Value = objJobData.PikeJob;
                commandParameters[5].Value = objJobData.Engineer;
                commandParameters[6].Value = objJobData.City;
                commandParameters[7].Value = objJobData.County;
                commandParameters[8].Value = objJobData.Region;
                commandParameters[9].Value = objJobData.Headquarters;
                commandParameters[10].Value = objJobData.JobType;
                commandParameters[11].Value = objJobData.StartDate;
                commandParameters[12].Value = objJobData.QCEngineer;
                commandParameters[13].Value = objJobData.QCDate;
                commandParameters[14].Value = objJobData.NJUNSCode;
                commandParameters[15].Value = objJobData.NJUNSProjNum;
                commandParameters[16].Value = objJobData.FieldEngineer;
                commandParameters[17].Value = objJobData.FieldEngDate;

                result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
                if (result == 0)
                {
                    Transactionflag = 1;
                }
                #region Datatable
                //else
                //{
                //    TrackingNo = commandParameters[1].Value.ToString();
                //    string cmd1 = "SP_JOB_EDIT_PERMITEE_INSERT";
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        SqlParameter[] commandParameters1 =
                //         {
                //            new SqlParameter("@TrackingId",SqlDbType.VarChar,50),
                //            new SqlParameter("@CompanyName",SqlDbType.VarChar,100),
                //            new SqlParameter("@Permitee",SqlDbType.VarChar,50),                    
                //         };
                //        commandParameters1[0].Value = TrackingNo;
                //        commandParameters1[1].Value = dt.Rows[i]["CompanyName"].ToString();
                //        commandParameters1[2].Value = dt.Rows[i]["Permitee"].ToString();
                //        intResult2 = SqlHelper.ExecuteNonQuery(sqlConn, sqlTran, CommandType.StoredProcedure, cmd1, commandParameters1);
                //    }
                //} 
                #endregion

                if (Transactionflag == 1)
                    sqlTran.Rollback("Whole");
                else
                    sqlTran.Commit();
            }
            catch (Exception exp)
            {
                sqlTran.Rollback("Whole");
                Transactionflag = 1;
                throw exp;
            }

            if (Transactionflag == 1)
                sqlTran.Rollback("Whole");//RollBAcking the Transaction (removing all the Inserted Values)

            sqlConn.Close();
            sqlConn.Dispose();
            return result;
        }

        #endregion

        #region EditPermitee_Insert
        public static int EditPermitee_Insert(string TrackingId, string CompanyName, bool Permitee)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_JOB_COMPANY__INSERT";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@TrackingId",SqlDbType.VarChar,50),
                new SqlParameter("@CompanyName",SqlDbType.VarChar,100),
                new SqlParameter("@Permitee",SqlDbType.VarChar,50),
            };
            commandParameters[0].Value = TrackingId;
            commandParameters[1].Value = CompanyName;
            commandParameters[2].Value = Permitee;

            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }

        #endregion
    }
}