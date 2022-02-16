
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
//using ExifLib;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Globalization;

namespace kvs
{
    public partial class appforgot : System.Web.UI.Page
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;


        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
         
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += Page_PreLoad;


          
          


        }
      
        protected void btnlogin_Click(object sender, EventArgs e)
        {
          
        }

        protected void btnagree_Click(object sender, EventArgs e)
        {


            try
            {
                // DateTime dob1 = DateTime.Parse(datepicker.Text);
                DateTime dob1 = DateTime.ParseExact(datepicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            }
            catch
            {
                lblMsg.Text = "Enter Valid Date in DD/MM/YYYY format only";
                return;
            }
            DateTime dob = DateTime.ParseExact(datepicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
           // DateTime dob = DateTime.Parse(datepicker.Text);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());

            con.Open();



            SqlCommand cmd2 = new SqlCommand(@"select * from kv_cand where cname_f=@cname_f and cname_l=@cname_l and mobile1=@mob and dob=@dob and email=@email", con);
            cmd2.Parameters.AddWithValue("@cname_f", SqlDbType.VarChar).Value = txtcnamef.Text.ToUpper();
            cmd2.Parameters.AddWithValue("@cname_l", SqlDbType.VarChar).Value = txtcnamel.Text.ToUpper();
            cmd2.Parameters.AddWithValue("@mob", SqlDbType.VarChar).Value = txtmob.Text;
            cmd2.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = dob;
            cmd2.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = txtemail.Text.ToUpper();
            SqlDataReader sdr = cmd2.ExecuteReader();
            if (@sdr.Read())
            {

                lblMsg.Text = "Your Registration Number Is : " + sdr["cand_id"].ToString();


            }
            else
            {
                lblMsg.Text = "No Data Found";
                return;
            }
            con.Close();
            con.Dispose();
        }
    }
}


              
    