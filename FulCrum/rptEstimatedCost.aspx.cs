using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fulcrum.Common;

namespace Fulcrum
{
    public partial class rptEstimatedCost : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exp)
            {
                DisplayError(tr_ErrorRow, lblError, exp.Message.ToString());
            }
        }
    }
}