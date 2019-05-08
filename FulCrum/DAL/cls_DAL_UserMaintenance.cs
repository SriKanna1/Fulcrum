using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class cls_DAL_UserMaintenance
    {
        #region GetUserMasterlist
        public static DataSet GetUserMasterlist()
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_M_USER_MASTER_GETLIST";
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd);
            return ds;
        }
        #endregion

        #region UserCompanyGetList
        public static DataSet UserCompanyGetList()
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_M_USER_COMPANY_GETLIST_NEW";
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd);
            return ds;
        }
        #endregion

        #region UserRoleCreate
        public static int UserRoleCreate(int RoleId, string AppType, string FirstName, string LastName, string Email)
        {
            SqlConnection sqlConn;
            SqlTransaction sqlTran;

            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            sqlConn = new SqlConnection(dsn);
            int intResult2 = 0;
            int UserId = 0;
            int Transactionflag = 0;
            sqlConn.Open();
            sqlTran = sqlConn.BeginTransaction("Whole");

            try
            {
                string cmd = "SP_TRAN_USER_ROLE_CREATE_NEW";
                SqlParameter[] commandParameters =
                {
                new SqlParameter("@ROLE_ID",SqlDbType.Int),
                new SqlParameter("@FIRST_NAME",SqlDbType.VarChar,150),
                new SqlParameter("@LAST_NAME",SqlDbType.VarChar,150),
                new SqlParameter("@USER_EMAIL",SqlDbType.VarChar,150),
                new SqlParameter("@RETVAL",SqlDbType.Int)
            };

                commandParameters[0].Value = RoleId;
                commandParameters[1].Value = FirstName;
                commandParameters[2].Value = LastName;
                commandParameters[3].Value = Email;
                commandParameters[4].Value = 0;
                commandParameters[4].Direction = ParameterDirection.Output;

                int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
                if (result == 0)
                {
                    Transactionflag = 1;
                }
                else
                {
                    UserId = Convert.ToInt32(commandParameters[4].Value);
                    string[] ApplicationType = AppType.Split(',');
                    string cmd1 = "SP_TRAN_USER_APPLICATION_CREATE";
                    for (int i = 0; i < ApplicationType.Count(); i++)
                    {
                        SqlParameter[] commandParameters1 =
                     {
                        new SqlParameter("@UserId",SqlDbType.Int),
                        new SqlParameter("@ApplicationId",SqlDbType.Int),

                     };
                        commandParameters1[0].Value = UserId;
                        commandParameters1[1].Value = ApplicationType[i].ToString();

                        intResult2 = SqlHelper.ExecuteNonQuery(sqlConn, sqlTran, CommandType.StoredProcedure, cmd1, commandParameters1);
                    }
                }

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
            return intResult2;
        }
        #endregion

        #region UserRoleUpdate
        public static int UserRoleUpdate(int UserId, int RoleId, string AppType, string FirstName, string LastName, string Email)
        {
            SqlConnection sqlConn;
            SqlTransaction sqlTran;

            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            sqlConn = new SqlConnection(dsn);
            int intResult2 = 0;
            int Transactionflag = 0;
            sqlConn.Open();
            sqlTran = sqlConn.BeginTransaction("Whole");

            try
            {
                string cmd = "SP_TRAN_USER_ROLE_UPDATE";
                SqlParameter[] commandParameters =
                {
                new SqlParameter("@ROLE_ID",SqlDbType.Int),
                new SqlParameter("@FIRST_NAME",SqlDbType.VarChar,150),
                new SqlParameter("@LAST_NAME",SqlDbType.VarChar,150),
                new SqlParameter("@USER_EMAIL",SqlDbType.VarChar,150),
                new SqlParameter("@USER_ID",SqlDbType.Int)
            };

                commandParameters[0].Value = RoleId;
                commandParameters[1].Value = FirstName;
                commandParameters[2].Value = LastName;
                commandParameters[3].Value = Email;
                commandParameters[4].Value = UserId;

                int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
                if (result == 0)
                {
                    Transactionflag = 1;
                }
                else
                {
                    UserId = Convert.ToInt32(commandParameters[4].Value);
                    string cmdd = "SP_TRAN_USER_APPLICATION_DELETE";
                    SqlParameter[] commandParameters2 =
                     {
                        new SqlParameter("@UserId",SqlDbType.Int),
                     };
                    commandParameters2[0].Value = UserId;
                    int Result2 = SqlHelper.ExecuteNonQuery(sqlConn, sqlTran, CommandType.StoredProcedure, cmdd, commandParameters2);
                     
                    string[] ApplicationType = AppType.Split(',');
                    string cmd1 = "SP_TRAN_USER_APPLICATION_CREATE";
                    for (int i = 0; i < ApplicationType.Count(); i++)
                    {
                        SqlParameter[] commandParameters1 =
                         {
                        new SqlParameter("@UserId",SqlDbType.Int),
                        new SqlParameter("@ApplicationId",SqlDbType.Int),

                          };
                        commandParameters1[0].Value = UserId;
                        commandParameters1[1].Value = ApplicationType[i].ToString();

                        intResult2 = SqlHelper.ExecuteNonQuery(sqlConn, sqlTran, CommandType.StoredProcedure, cmd1, commandParameters1);
                    }
                }

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
            return intResult2;
        }
        #endregion

        #region GetUserRoleDetails
        public static DataSet GetUserRoleDetails()
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_M_USER_ROLE_DETAILS_GETLIST_NEW";
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd);
            return ds;
        }
        #endregion

        #region UserRoleDelete
        public static int UserRoleDelete(int UserRoleId)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_TRAN_USER_ROLE_DELETE";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@USER_ID",SqlDbType.Int)
            };

            commandParameters[0].Value = UserRoleId;


            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }
        #endregion

        #region GetTranUserRoleDetails
        public static DataSet GetTranUserRoleDetails(string Email)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_TRAN_USER_ROLE_GETDETAILS_NEW";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@EMAIL",SqlDbType.VarChar,100)
            };

            commandParameters[0].Value = Email;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion

        #region GetUserApplications
        public static DataSet GetUserApplications(string Email)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_TRAN_USER_APPLICATION_GETLIST";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@EMAIL",SqlDbType.VarChar,100)
            };

            commandParameters[0].Value = Email;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion
    }
}