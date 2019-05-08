using Fulcrum.BAL;
using Fulcrum.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Fulcrum.BAL
{
    public class clsBAL_UserMaintenance
    {
        public static DataSet GetUserMasterlist()
        {
            return DAL.cls_DAL_UserMaintenance.GetUserMasterlist();
        }

        public static DataSet UserCompanyGetList()
        {
            return DAL.cls_DAL_UserMaintenance.UserCompanyGetList();
        }

        public static int UserRoleCreate(int RoleId, string AppType, string FirstName, string LastName, string Email)
        {
            return DAL.cls_DAL_UserMaintenance.UserRoleCreate(RoleId, AppType, FirstName, LastName, Email);
        }
        public static int UserRoleUpdate(int UserId, int RoleId, string AppType, string FirstName, string LastName, string Email)
        {
            return DAL.cls_DAL_UserMaintenance.UserRoleUpdate(UserId, RoleId, AppType, FirstName, LastName, Email);
        } 
        public static DataSet GetUserRoleDetails()
        {
            return DAL.cls_DAL_UserMaintenance.GetUserRoleDetails();
        }

        public static int UserRoleDelete(int UserRoleId)
        {
            return DAL.cls_DAL_UserMaintenance.UserRoleDelete(UserRoleId);
        }

        public static DataSet GetTranUserRoleDetails(string Email)
        {
            return DAL.cls_DAL_UserMaintenance.GetTranUserRoleDetails(Email);
        }

        public static DataSet GetUserApplications(string Email)
        {
            return DAL.cls_DAL_UserMaintenance.GetUserApplications(Email);
        } 
    }
}