using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fulcrum.Common;
using Fulcrum.BAL;

namespace Fulcrum
{
    public partial class CheckViolations : BasePage
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txt_Pole.Text = Request.QueryString["PoleId"].ToString();
                    string TrackingId = Request.QueryString["TrackingId"].ToString();
                    string PoleId = Request.QueryString["PoleId"].ToString();
                    GetCompleteList(TrackingId, PoleId);
                }
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }

        }
        #endregion

        #region txt_Pole_TextChanged
        protected void txt_Pole_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string TrackingId = Request.QueryString["TrackingId"].ToString();
                string PoleId = txt_Pole.Text;
                GetCompleteList(TrackingId, PoleId);
            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion

        #region Common
        public void GetCompleteList(string TrackingId, string PoleId)
        {
            try
            {
                DataSet ds = BAL.clsBAL_FieldDataEntry.CheckViolation(TrackingId, PoleId);
                DataTable dt1 = ds.Tables[0];
                Rad40InchVio.DataSource = dt1;
                Rad40InchVio.DataBind();

                DataTable dt2 = ds.Tables[1];
                Rad30InchVio.DataSource = dt2;
                Rad30InchVio.DataBind();

                DataTable dt3 = ds.Tables[2];
                Rad12InchVio.DataSource = dt3;
                Rad12InchVio.DataBind();

                DataTable dt4 = ds.Tables[3];
                Rad04InchVio.DataSource = dt4;
                Rad04InchVio.DataBind();

                DataSet dsMid = BAL.clsBAL_FieldDataEntry.CheckViolationMidspan(TrackingId, PoleId);
                DataTable dt5 = dsMid.Tables[0];
                Rad27GC.DataSource = dt5;
                Rad27GC.DataBind();

                DataTable dt6 = dsMid.Tables[1];
                Rad18GCHigh.DataSource = dt6;
                Rad18GCHigh.DataBind();

                DataTable dt7 = dsMid.Tables[2];
                Rad156GCTrav.DataSource = dt7;
                Rad156GCTrav.DataBind();

                DataTable dt8 = dsMid.Tables[3];
                Rad13GCRural.DataSource = dt8;
                Rad13GCRural.DataBind();

                DataTable dt9 = dsMid.Tables[4];
                Rad96GCPedOnly.DataSource = dt9;
                Rad96GCPedOnly.DataBind();

                DataTable dt10 = dsMid.Tables[5];
                RadMSSeparation30.DataSource = dt10;
                RadMSSeparation30.DataBind();

                DataTable dt11 = dsMid.Tables[6];
                RadMSSeparation12.DataSource = dt11;
                RadMSSeparation12.DataBind();

            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
        #endregion
    }
}