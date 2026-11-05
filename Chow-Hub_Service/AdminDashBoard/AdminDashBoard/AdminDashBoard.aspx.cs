using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chow_Hub_FrontEnd.ChowHub_Service;

namespace Chow_Hub_FrontEnd
{
    public partial class AdminDashBoard : System.Web.UI.Page
    {
        ChowHubServiceClient CH = new ChowHubServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                content.Text = CH.GetDashboardSummary();
            }
        }
    }
}