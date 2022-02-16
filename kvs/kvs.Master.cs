using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kvs
{
    public partial class kvs : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kvid"] == null)
            {

                lilogout.Visible = false;
            }
            else
            {

                lilogout.Visible = true;
            }
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("applogin.aspx", false);
        }
    }
}