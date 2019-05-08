using Fulcrum.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Fulcrum.Common;
using Fulcrum.BAL;


namespace Fulcrum.BAL
{
    public class clsBAL_FieldDataEntry
    {
        public static DataSet MasterData_GetList()
        {
            return DAL.cls_DAL_FieldDataEntry.MasterData_GetList();
        }

        public static DataSet GetPoleDetails(string PoleId, string tracking_no)
        {
            return DAL.cls_DAL_FieldDataEntry.GetPoleDetails(PoleId, tracking_no);
        }

        public static int FieldData_Insert(clsFieldDataEntry FieldData)
        {
            return DAL.cls_DAL_FieldDataEntry.FieldData_Insert(FieldData);
        }

        public static int POA_Create(clsPOA objPoa)
        {
            return DAL.cls_DAL_FieldDataEntry.POA_Create(objPoa);
        }

        public static int POA_Delete(string RecordId, string ChildRecordId)
        {
            return DAL.cls_DAL_FieldDataEntry.POA_Delete(RecordId, ChildRecordId);
        }

        public static int Midspan_Create(clsMidspan objmid)
        {
            return DAL.cls_DAL_FieldDataEntry.Midspan_Create(objmid);
        }
        public static int Midspan_Delete(string RecordId, string ChildRecordId)
        {
            return DAL.cls_DAL_FieldDataEntry.Midspan_Delete(RecordId, ChildRecordId);
        }
        public static int Notes_Create(clsNotes objNotes)
        {
            return DAL.cls_DAL_FieldDataEntry.Notes_Create(objNotes);
        }

        public static int Notes_Delete(int Id, string TrackingId, string PoleId)
        {
            return DAL.cls_DAL_FieldDataEntry.Notes_Delete(Id, TrackingId, PoleId);
        }

        public static DataSet GetTrackingPoles(string TrackingNo)
        {
            return DAL.cls_DAL_FieldDataEntry.GetTrackingPoles(TrackingNo);
        }

        public static DataSet CheckViolation(string TrackingNo, string PoleId)
        {
            return DAL.cls_DAL_FieldDataEntry.CheckViolation(TrackingNo, PoleId);
        }
        public static DataSet CheckViolationMidspan(string TrackingNo, string PoleId)
        {
            return DAL.cls_DAL_FieldDataEntry.CheckViolationMidspan(TrackingNo, PoleId);
        }

        public static DataSet GetTrackingRecords(string TrackingNo)
        {
            return DAL.cls_DAL_FieldDataEntry.GetTrackingRecords(TrackingNo);
        }
    }
}