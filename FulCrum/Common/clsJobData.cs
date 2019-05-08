using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fulcrum.Common
{
    public class clsJobData
    {
        public string JobName;
        public string TrackingId;
        public string Reference;
        public string Workorder;
        public string PikeJob;

        public string Engineer;
        public string City;
        public string County;
        public string Region;
        public string Headquarters;

        public string JobType;
        public DateTime StartDate;
        public string QCEngineer;
        public DateTime QCDate;
        public string NJUNSCode;
        public string NJUNSProjNum;

        public string FieldEngineer;
        public DateTime FieldEngDate;
    }
}