using Fulcrum.BAL;
using Fulcrum.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Fulcrum.BAL
{
    public class clsBAL_JobData
    {
        public static DataSet JobMasterData_GetList()
        {
            return DAL.cls_DAL_JobData.JobMasterData_GetList();
        }
        public static DataSet GetPermiteeList(string TrackingId)
        {
            return DAL.cls_DAL_JobData.GetPermiteeList(TrackingId);
        }
        public static DataSet GetJobDetails(string TrackingId)
        {
            return DAL.cls_DAL_JobData.GetJobDetails(TrackingId);
        }
        public static int JobData_Insert(clsJobData objJobData)
        {
            return DAL.cls_DAL_JobData.JobData_Insert(objJobData);
        }
        public static int EditPermitee_Insert(string TrackingId, string CompanyName, bool Permitee)
        {
            return DAL.cls_DAL_JobData.EditPermitee_Insert(TrackingId, CompanyName, Permitee);
        }
    }
}