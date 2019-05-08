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
    public class cls_DAL_FieldDataEntry
    {
        #region MasterData_GetList
        public static DataSet MasterData_GetList()
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_MASTER_DATA_GETLIST";
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd);
            return ds;
        }
        #endregion

        #region GetPoleDetails
        public static DataSet GetPoleDetails(string PoleId, string tracking_no)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_GET_POLE_DETAILS";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@PoleId",SqlDbType.VarChar,10),
                new SqlParameter("@tracking_no",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = PoleId;
            commandParameters[1].Value = tracking_no;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion 

        #region FieldData_Insert
        public static int FieldData_Insert(clsFieldDataEntry FieldData)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_INSERT";
            SqlParameter[] commandParameters =
               {
                new SqlParameter("@TrackingNo",SqlDbType.VarChar,50),
                new SqlParameter("@Pole_Id",SqlDbType.VarChar,50),
                new SqlParameter("@Owner",SqlDbType.VarChar,100),
                new SqlParameter("@Location",SqlDbType.VarChar,500),
                new SqlParameter("@Station",SqlDbType.VarChar,50),
                new SqlParameter("@PowerMap",SqlDbType.VarChar,50),
                new SqlParameter("@CommMap",SqlDbType.VarChar,50),
                new SqlParameter("@NJUNSNo",SqlDbType.VarChar,50),
                new SqlParameter("@Ht_Class_ExistingFt",SqlDbType.VarChar,50),
                new SqlParameter("@Ht_Class_ExistingIn",SqlDbType.VarChar,50),
                new SqlParameter("@Ht_Class_NewFt",SqlDbType.VarChar,50),
                new SqlParameter("@Ht_Class_NewIn",SqlDbType.VarChar,50),
                new SqlParameter("@Component_Id",SqlDbType.VarChar,50),
                new SqlParameter("@latitude",SqlDbType.VarChar,50),
                new SqlParameter("@longitude",SqlDbType.VarChar,50),
                new SqlParameter("@Date",SqlDbType.DateTime),
                new SqlParameter("@Temparature",SqlDbType.VarChar,50),
                new SqlParameter("@FieldNotes",SqlDbType.VarChar,4000),
                new SqlParameter("@EngineerNotes",SqlDbType.VarChar,4000),
                new SqlParameter("@PoleComplete",SqlDbType.VarChar,50),
                new SqlParameter("@GPCCost",SqlDbType.VarChar,50),
                new SqlParameter("@PoleCompleteDate",SqlDbType.VarChar,50)
            };
            commandParameters[0].Value = FieldData.TrackingNo;
            commandParameters[1].Value = FieldData.Pole_Id;
            commandParameters[2].Value = FieldData.Owner;
            commandParameters[3].Value = FieldData.Location;
            commandParameters[4].Value = FieldData.Station;
            commandParameters[5].Value = FieldData.PowerMap;
            commandParameters[6].Value = FieldData.CommMap;
            commandParameters[7].Value = FieldData.NJUNSNo;
            commandParameters[8].Value = FieldData.Ht_Class_ExistingFt;
            commandParameters[9].Value = FieldData.Ht_Class_ExistingIn;
            commandParameters[10].Value = FieldData.Ht_Class_NewFt;
            commandParameters[11].Value = FieldData.Ht_Class_NewIn;
            commandParameters[12].Value = FieldData.Component_Id;
            commandParameters[13].Value = FieldData.latitude;
            commandParameters[14].Value = FieldData.longitude;
            commandParameters[15].Value = FieldData.Date;
            commandParameters[16].Value = FieldData.Temparature;
            commandParameters[17].Value = FieldData.FieldNotes;
            commandParameters[18].Value = FieldData.EngineerNotes;
            commandParameters[19].Value = FieldData.PoleComplete;
            commandParameters[20].Value = FieldData.GPCCost;
            commandParameters[21].Value = FieldData.PoleCompleteDate;
            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }

        #endregion  

        #region POA_Create
        public static int POA_Create(clsPOA objPoa)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_POA_CREATE";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@TrackingId",SqlDbType.VarChar,50),
                new SqlParameter("@POLE_ID",SqlDbType.VarChar,50),
                new SqlParameter("@ChildRecordId",SqlDbType.VarChar,100),
                new SqlParameter("@COMPANY",SqlDbType.VarChar,100),
                new SqlParameter("@TYPE",SqlDbType.VarChar,100),
                new SqlParameter("@POA_FT",SqlDbType.VarChar,50),
                new SqlParameter("@POA_IN",SqlDbType.VarChar,50),
                new SqlParameter("@NEW_FT",SqlDbType.VarChar,50),
                new SqlParameter("@NEW_IN",SqlDbType.VarChar,50),
            };
            commandParameters[0].Value = objPoa.TrackingId;
            commandParameters[1].Value = objPoa.PoleId;
            commandParameters[2].Value = objPoa.ChildRecordId;
            commandParameters[3].Value = objPoa.Company;
            commandParameters[4].Value = objPoa.Type;
            commandParameters[5].Value = objPoa.POA_FT;
            commandParameters[6].Value = objPoa.POA_IN;
            commandParameters[7].Value = objPoa.New_Ft;
            commandParameters[8].Value = objPoa.New_In;

            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }

        #endregion 

        #region POA_Delete
        public static int POA_Delete(string RecordId, string ChildRecordId)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_POA_DELETE";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@RecordId",SqlDbType.VarChar,100),
                new SqlParameter("@ChildRecordId",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = RecordId;
            commandParameters[1].Value = ChildRecordId;
            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }
        #endregion 

        #region Midspan_Create
        public static int Midspan_Create(clsMidspan objmid)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_MIDSPAN_CREATE";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@TrackingId",SqlDbType.VarChar,50),
                new SqlParameter("@POLE_ID",SqlDbType.VarChar,50),
                new SqlParameter("@TO",SqlDbType.VarChar,50),
                new SqlParameter("@ChildRecordId",SqlDbType.VarChar,100),
                new SqlParameter("@COMPANY",SqlDbType.VarChar,100),
                new SqlParameter("@TYPE",SqlDbType.VarChar,100),
                new SqlParameter("@MID_FT",SqlDbType.VarChar,50),
                new SqlParameter("@MID_IN",SqlDbType.VarChar,50),
                new SqlParameter("@OVER",SqlDbType.VarChar,100),
            };
            commandParameters[0].Value = objmid.TrackingId;
            commandParameters[1].Value = objmid.PoleId;
            commandParameters[2].Value = objmid.To;
            commandParameters[3].Value = objmid.ChildRecordId;
            commandParameters[4].Value = objmid.Company;
            commandParameters[5].Value = objmid.Type;
            commandParameters[6].Value = objmid.MidFt;
            commandParameters[7].Value = objmid.MidIn;
            commandParameters[8].Value = objmid.Over;

            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }

        #endregion

        #region Midspan_Delete
        public static int Midspan_Delete(string RecordId, string ChildRecordId)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_MIDSPAN_DELETE";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@RecordId",SqlDbType.VarChar,100),
                new SqlParameter("@ChildRecordId",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = RecordId;
            commandParameters[1].Value = ChildRecordId;
            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }
        #endregion  

        #region Notes_Create
        public static int Notes_Create(clsNotes objNotes)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_NOTES_CREATE";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@Id",SqlDbType.Int),
                new SqlParameter("@TrackingId",SqlDbType.VarChar,50),
                new SqlParameter("@POLE_ID",SqlDbType.VarChar,50),
                new SqlParameter("@COMPANY",SqlDbType.VarChar,100),
                new SqlParameter("@NOTES",SqlDbType.VarChar,1000),
                new SqlParameter("@STEP",SqlDbType.Int),
                new SqlParameter("@SHARE",SqlDbType.Int)
            };
            commandParameters[0].Value = objNotes.Id;
            commandParameters[1].Value = objNotes.TrackingId;
            commandParameters[2].Value = objNotes.PoleId;
            commandParameters[3].Value = objNotes.Company;
            commandParameters[4].Value = objNotes.Notes;
            commandParameters[5].Value = objNotes.Step;
            commandParameters[6].Value = objNotes.Share;

            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }

        #endregion

        #region Notes_Delete
        public static int Notes_Delete(int Id, string TrackingId, string PoleId)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_NOTES_DELETE";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@Id",SqlDbType.Int),
                new SqlParameter("@TrackingId",SqlDbType.VarChar,100),
                new SqlParameter("@PoleId",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = Id;
            commandParameters[1].Value = TrackingId;
            commandParameters[2].Value = PoleId;
            int result = SqlHelper.ExecuteNonQuery(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return result;
        }
        #endregion 

        #region GetTrackingPoles
        public static DataSet GetTrackingPoles(string tracking_no)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_GET_TRACKING_POLE_LIST";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@tracking_no",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = tracking_no;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion  

        #region CheckViolation
        public static DataSet CheckViolation(string TrackingNo, string PoleId)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_CHECK_VIOLATION";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@TrackingId",SqlDbType.VarChar,100),
                new SqlParameter("@PoleId",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = TrackingNo;
            commandParameters[1].Value = PoleId;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion 


        #region CheckViolationMidspan
        public static DataSet CheckViolationMidspan(string TrackingNo, string PoleId)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_CHECK_VIOLATION_MIDSPAN";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@TrackingId",SqlDbType.VarChar,100),
                new SqlParameter("@PoleId",SqlDbType.VarChar,100)
            };
            commandParameters[0].Value = TrackingNo;
            commandParameters[1].Value = PoleId;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion

        #region GetTrackingRecords
        public static DataSet GetTrackingRecords(string TrackingNo)
        {
            string dsn = clsConfiguration.CurrentConfig.ConnectionString;
            string cmd = "SP_FIELD_DATA_GET_TRACKING_RECORDS";
            SqlParameter[] commandParameters =
            {
                new SqlParameter("@TrackingId",SqlDbType.VarChar,100),
            };
            commandParameters[0].Value = TrackingNo;
            DataSet ds = SqlHelper.ExecuteDataset(dsn, CommandType.StoredProcedure, cmd, commandParameters);
            return ds;
        }
        #endregion 
    }
}
