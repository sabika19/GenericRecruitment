using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kvs
{
    public partial class info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnagree.Enabled = false;
        }

        protected void btnagree_Click(object sender, EventArgs e)
        {
            Response.Redirect("applicationpart1.aspx", false);
        }
    }
}