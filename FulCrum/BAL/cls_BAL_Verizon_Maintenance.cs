using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FulCrum.BAL
{
    public class cls_BAL_Verizon_Maintenance
    {
        public static DataSet GetEwoList()
        {
            return DAL.cls_DAL_Verizon_Maintenance.GetEwoList();
        }
        public static DataSet GetPoleDetails(string Ewo)
        {
            return DAL.cls_DAL_Verizon_Maintenance.GetPoleDetails(Ewo);
        }
        public static DataSet GetPoleMapDetails(string Ewo)
        {
            return DAL.cls_DAL_Verizon_Maintenance.GetPoleMapDetails(Ewo);
        }
        public static DataSet GetewoPictures(string Ewo)
        {
            return DAL.cls_DAL_Verizon_Maintenance.GetewoPictures(Ewo);
        }
 
    }
}