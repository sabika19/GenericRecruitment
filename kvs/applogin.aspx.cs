
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
using System.Web.SessionState;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using System.Collections;

namespace kvs
{
    public partial class applogin : System.Web.UI.Page
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;


        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
                Session.Clear();
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
        public void ClearCacheItems()
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();

            while (enumerator.MoveNext())
                keys.Add(enumerator.Key.ToString());

            for (int i = 0; i < keys.Count; i++)
                Cache.Remove(keys[i]);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearCacheItems();
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
                SessionIDManager Manager = new SessionIDManager();
                string NewID = Manager.CreateSessionID(Context);
                string OldID = Context.Session.SessionID;
                bool redirected = false;
                bool IsAdded = false;
                Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);
                //   string salt = CreateSalt(5);

                //Save the salt in session variable  
                //  salted = salt.ToString();

                //Add the JS function call to button with a parameter 
                //  btnlogin.Attributes.Add("onclick", "return HashPwdwithSalt('" + salt.ToString() + "');");
                txtUser1.Attributes.Add("autocomplete", "off");
                datepicker.Attributes.Add("autocomplete", "off");
            }
        }
        private string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        public string salted
        {
            get
            {
                return (string)ViewState["salted"] ?? " ";
            }
            set
            {
                ViewState["salted"] = value;
            }
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {

            try
            {
                //DateTime dob = DateTime.Parse(datepicker.Text);
                DateTime dob1 = DateTime.ParseExact(datepicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            }
            catch
            {
                lblMsg.Text = "Enter Valid Date in DD/MM/YYYY format only";
                return;
            }
            if (Session["pinsec"] == null)
                Response.Redirect("applogin.aspx");

            if (TextBox1.Text.ToUpper() != Session["pinsec"].ToString())
            {
                lblMsg.Text = "Invalid Captcha Entered..";
                return;
            }
            // captcha1.ValidateCaptcha(TextBox1.Text.Trim());
            //if (captcha1.UserValidated)
            //{
            DateTime dob = DateTime.ParseExact(datepicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            // DateTime dob = DateTime.Parse(datepicker.Text);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());

            con.Open();

            SqlCommand cmd2 = new SqlCommand(@"select * from kv_cand where cand_id=@cand_id and dob=@dob", con);
            cmd2.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = txtUser1.Text;
            cmd2.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = dob;
            SqlDataReader sdr = cmd2.ExecuteReader();
            if (@sdr.Read())
            {

                Session.Abandon();
                SessionIDManager Manager = new SessionIDManager();
                string NewID = Manager.CreateSessionID(Context);
                string OldID = Context.Session.SessionID;
                bool redirected = false;
                bool IsAdded = false;
                Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);
                string flag = sdr["flag"].ToString();
                sdr.Close();
                Session["kvid"] = Guid.NewGuid().ToString();

                //string strmsg = string.Empty;
                //byte[] encode = new byte[TextBox1.Text.Length];
                //encode = Encoding.UTF8.GetBytes(TextBox1.Text);
                //strmsg = Convert.ToBase64String(encode);
                //Session["candid"] = strmsg;

                //  string Stmt = "insert into kv_auth (au_agent,au_ip,au_url,au_user,au_query,au_ref,au_date,au_status) values ('" + Request.ServerVariables["HTTP_USER_AGENT"] + "','" + Request.ServerVariables["REMOTE_ADDR"] + "','kvs:" + Request.ServerVariables["URL"] + "', '" + txtUser1.Text + "', '" + Session["kvid"].ToString() + NewID + "salted','" + Request.ServerVariables["HTTP_REFERER"] + "', '" + DateTime.Now + "', 'pass')";

                string Stmt = "insert into kv_auth (au_agent,au_ip,au_url,au_user,au_query,au_ref,au_date,au_status) values (@user_agent,@ip,@url,@user,@query,@ref,@au_date,@au_status)";
                SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(Stmt, con);
                Cmd.Parameters.AddWithValue("@user_agent", SqlDbType.VarChar).Value = Request.ServerVariables["HTTP_USER_AGENT"];
                Cmd.Parameters.AddWithValue("@ip", SqlDbType.VarChar).Value = Request.ServerVariables["REMOTE_ADDR"];
                Cmd.Parameters.AddWithValue("@url", SqlDbType.VarChar).Value = Request.ServerVariables["URL"];
                Cmd.Parameters.AddWithValue("@user", SqlDbType.VarChar).Value = txtUser1.Text;
                Cmd.Parameters.AddWithValue("@query", SqlDbType.VarChar).Value = Session["kvid"].ToString() + NewID + "salted";
                Cmd.Parameters.AddWithValue("@ref", SqlDbType.VarChar).Value = Request.ServerVariables["HTTP_REFERER"];
                Cmd.Parameters.AddWithValue("@au_date", SqlDbType.DateTime).Value = DateTime.Now;
                Cmd.Parameters.AddWithValue("@au_status", SqlDbType.VarChar).Value = "pass";

                Cmd.ExecuteNonQuery();
                Cmd.Dispose();
                //if (flag == "Y")
                //{
                //    Response.Redirect("apppedit.aspx?pid=" + Session["kvid"].ToString(), false);
                //    return;
                //}
                SqlCommand Cmdstatus = new SqlCommand(@"select * from kv_status with (nolock) where cand_id=@candid", con);
                Cmdstatus.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtUser1.Text;
                sdr = Cmdstatus.ExecuteReader();
                if (@sdr.Read())
                {

                    string step2 = sdr["step2"].ToString();
                    string step3 = sdr["step3"].ToString();

                    if (step2 != "Y")
                        // Response.Redirect("appstep2.aspx?pid=" + Session["HashKey"].ToString()+"", false);
                        Response.Redirect("appstep2.aspx?pid=" + Session["kvid"].ToString());
                    else if (step2 == "Y" && step3 != "Y")
                        //  Response.Redirect("appstep3.aspx?pid=" + Session["HashKey"].ToString() + "", false);
                        Response.Redirect("appstep3.aspx?pid=" + Session["kvid"].ToString());
                    else if (step2 == "Y" && step3 == "Y")
                        //   Response.Redirect("appconf.aspx?pid=" + Session["HashKey"].ToString() + "", false);
                        Response.Redirect("appconf.aspx?pid=" + Session["kvid"].ToString());

                    sdr.Close();
                    sdr.Dispose();

                }

            }
            else
            {
                Session.Abandon();
                SessionIDManager Manager = new SessionIDManager();
                string NewID = Manager.CreateSessionID(Context);
                string OldID = Context.Session.SessionID;
                bool redirected = false;
                bool IsAdded = false;
                Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);
                sdr.Close();
                sdr.Dispose();
                Session["kvid"] = Guid.NewGuid().ToString();
                string Stmt = "insert into kv_auth (au_agent,au_ip,au_url,au_user,au_query,au_ref,au_date,au_status) values (@user_agent,@ip,@url,@user,@query,@ref,@au_date,@au_status)";
                SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(Stmt, con);
                Cmd.Parameters.AddWithValue("@user_agent", SqlDbType.VarChar).Value = Request.ServerVariables["HTTP_USER_AGENT"];
                Cmd.Parameters.AddWithValue("@ip", SqlDbType.VarChar).Value = Request.ServerVariables["REMOTE_ADDR"];
                Cmd.Parameters.AddWithValue("@url", SqlDbType.VarChar).Value = Request.ServerVariables["URL"];
                Cmd.Parameters.AddWithValue("@user", SqlDbType.VarChar).Value = txtUser1.Text;
                Cmd.Parameters.AddWithValue("@query", SqlDbType.VarChar).Value = Session["kvid"].ToString() + NewID + "salted";
                Cmd.Parameters.AddWithValue("@ref", SqlDbType.VarChar).Value = Request.ServerVariables["HTTP_REFERER"];
                Cmd.Parameters.AddWithValue("@au_date", SqlDbType.DateTime).Value = DateTime.Now;
                Cmd.Parameters.AddWithValue("@au_status", SqlDbType.VarChar).Value = "fail";

                Cmd.ExecuteNonQuery();

                Cmd.Dispose();
                lblMsg.Text = "Invalid Credentials";
                return;
            }
            con.Close();
            con.Dispose();
            //}
            //else
            //{
            //    lblMsg.Text = "Invalid Captcha Entered";
            //    txtUser1.Text = string.Empty;
            //    datepicker.Text = string.Empty;
            //    TextBox1.Text = string.Empty;
            //    return;
            //}
        }

    }
}


              
    