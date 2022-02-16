using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;



namespace kvs
{
    public partial class editstep3 : System.Web.UI.Page
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
            if (Request.QueryString["pid"] == null && Session["candid"] == null)
            {
                Response.Redirect("editlogin.aspx", false);
                return;
            }
            else
            {

            }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["pid"] == null && Session["candid"] == null)
            {
                Response.Redirect("editlogin.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                if (DateTime.Now > Convert.ToDateTime(ConfigurationManager.AppSettings["lastpaydate"]))
                {
                    lblmsg.Text = "LAST DATE FOR PAYMENT IS OVER!!";
                    tblpay.Visible = false;
                    divbtn.Visible = false;
                    return;
                }
                else
                {
                    tblpay.Visible = true;
                    divbtn.Visible = true;
                    if (Session["candid"] == null && Request.QueryString["pid"] != null)
                    {
                        string pid = "", uid = "";
                        int proceed = 0;
                        pid = Request.QueryString["pid"];

                        if (pid.Length != 36)
                        {
                            Response.Redirect("editlogin.aspx", false);
                            return;
                        }
                      
                        System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand();
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                        con.Open();
                        System.Data.SqlClient.SqlDataReader Dr;
                        string Stmt;

                        Stmt = " select au_user from kv_auth where au_query = '" + pid + Context.Session.SessionID + "salted' and au_status = 'pass'";
                        // Stmt = "select au_user from kv_auth where au_query='" + pid + Context.Session.SessionID + "' and au_status='pass'";
                        Cmd = new System.Data.SqlClient.SqlCommand(Stmt, con);
                        Dr = Cmd.ExecuteReader();
                        if (Dr.Read())
                        {

                            lblregn.Text = Dr["au_user"].ToString();

                            Dr.Close();

                            SqlCommand cmd2 = new SqlCommand(@"select a.* from kv_cand_edit a inner join kv_photo_edit b on a.cand_id=b.cand_id where a.cand_id=@cand_id", con);
                            cmd2.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblregn.Text;

                            SqlDataReader sdr = cmd2.ExecuteReader();
                            if (@sdr.Read())
                            {

                                btnagree.Visible = true;
                                lblcname.Text = sdr["cname_f"].ToString() + " " + sdr["cname_l"].ToString();
                                lblmname.Text = sdr["mname"].ToString();
                                lblfname.Text = sdr["fname"].ToString();
                                lblfee.Text = sdr["fee"].ToString() + ".00";
                                //  lblfee.Text = "2.00";
                                lblmobile.Text = sdr["mobile1"].ToString();
                                lblemail.Text = sdr["email"].ToString();
                                sdr.Close();
                                SqlCommand cmd3 = new SqlCommand(@"select * from kv_photo_edit where cand_id=@cand_id", con);
                                cmd3.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblregn.Text;
                                sdr = cmd3.ExecuteReader();
                                if (@sdr.Read())
                                {

                                    byte[] bytes = (byte[])sdr["photo"];
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                    imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                                    byte[] bytes2 = (byte[])sdr["sign"];
                                    string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                                    imgsign.ImageUrl = "data:image/png;base64," + base64String2;

                                }
                                sdr.Close();
                                SqlCommand cmd4 = new SqlCommand(@"select count(*) from kv_status with (nolock) where cand_id=@cand_id and step3='Y'", con);
                                cmd4.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblregn.Text;
                                int countstep3 = (int)cmd4.ExecuteScalar();
                                if (countstep3 != 0)
                                {
                                    Response.Redirect("appconf.aspx", false);
                                    return;
                                }
                            }
                            else
                            {
                                Response.Redirect("editlogin.aspx", false);
                                return;
                            }
                        }
                        else
                        {
                            Response.Redirect("editlogin.aspx", false);
                            return;
                        }
                        con.Close();
                        con.Dispose();

                    }
                }
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        protected void btnagree_Click(object sender, EventArgs e)
        {

            string pid = Request.QueryString["pid"];
            string cname = lblcname.Text;
            string hh = DateTime.Now.Hour.ToString();
            string mm = DateTime.Now.Minute.ToString();
            string sc = DateTime.Now.Second.ToString();
            string msc = DateTime.Now.Millisecond.ToString();
            string txnid = lblregn.Text + hh + mm + sc + msc;
            String data = "";
            String commonkey = "";
            if (rdblgtwy.SelectedValue == "I")
            {
                data = "CBSEKVS|" + txnid + "|NA|" + lblfee.Text + "|NA|NA|NA|INR|NA|R|cbsekvs|NA|NA|F|" + lblmobile.Text + "|" + lblemail.Text + "|" + lblregn.Text + "|" + pid + "|" + cname + "|IB|NA|http://59.179.16.89/edit/editpaysuccess.aspx";
                commonkey = "5JQs4zl4hcxL";
            }
            else
            {
                data = "CBSEKVSHDF|" + txnid + "|NA|" + lblfee.Text + "|NA|NA|NA|INR|NA|R|cbsekvshdf|NA|NA|F|" + lblmobile.Text + "|" + lblemail.Text + "|" + lblregn.Text + "|" + pid + "|" + cname + "|HDFC|NA|http://59.179.16.89/edit/editpaysuccess.aspx";
                commonkey = "OaxbAGna7VbU";
            }
            appstep3 dataprg = new appstep3();
            String hash = String.Empty;
            hash = dataprg.GetHMACSHA256(data, commonkey);
            // Console.Out.WriteLine("HMAC {0}", hash);
            //  Console.ReadKey();
            string msg = data + "|" + hash.ToUpper();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            //SqlTransaction trans = con.BeginTransaction();
            try
            {
                SqlCommand cmdpaylog = new SqlCommand(@"insert into kv_feelot([cand_id]
      ,[txnid]
      ,[PAYMODE]
      ,[inidate]
      ,[iniip]
      ,[inifee]
      ,[status])values(@candid,@txnid,@paymode,@inidate,@iniip,@inifee,@status)", con);
                cmdpaylog.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = lblregn.Text;
                cmdpaylog.Parameters.AddWithValue("@txnid", SqlDbType.VarChar).Value = txnid;
                cmdpaylog.Parameters.AddWithValue("@paymode", SqlDbType.VarChar).Value = "ONLINE";
                cmdpaylog.Parameters.AddWithValue("@inidate", SqlDbType.VarChar).Value = DateTime.Now;
                cmdpaylog.Parameters.AddWithValue("@iniip", SqlDbType.VarChar).Value = Request.ServerVariables["REMOTE_ADDR"];
                cmdpaylog.Parameters.AddWithValue("@inifee", SqlDbType.VarChar).Value = lblfee.Text;
                cmdpaylog.Parameters.AddWithValue("@status", SqlDbType.VarChar).Value = "PENDING";
                cmdpaylog.ExecuteNonQuery();
                Response.Redirect("https://pgi.billdesk.com/pgidsk/PGIMerchantPayment?msg=" + msg + "", false);

            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }
        public editstep3() { }


        public string GetHMACSHA256(string text, string key)
        {
            UTF8Encoding encoder = new UTF8Encoding();

            byte[] hashValue;
            byte[] keybyt = encoder.GetBytes(key);
            byte[] message = encoder.GetBytes(text);

            HMACSHA256 hashString = new HMACSHA256(keybyt);
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
    }
}