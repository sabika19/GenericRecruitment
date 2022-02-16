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
using System.Globalization;

namespace kvs
{
    public partial class editpaysuccess : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.Params["msg"]==null)
                {
                    Response.Redirect("Oops.html", false);
                    return;
                }

                btnagree.Visible = false;
                
                string msg2 = Request.Params["msg"];

                string[] response = msg2.Split('|');

                string merch_id = response[0].ToString();
                string txn_id = response[1].ToString();
                string txn_refno = response[2].ToString();
                string bnkrefno = response[3].ToString();
                string txnamt = response[4].ToString();
                string bnkid = response[5].ToString();
                string bnkmerch_id = response[6].ToString();
                string txntype = response[7].ToString();
                string curr = response[8].ToString();
                string txndate = response[13].ToString();
               // DateTime dttxndate = DateTime.Parse(txndate);
                string auth = response[14].ToString();
                string status = "PENDING";
                lblstatus.Text = "PENDING";
                if (auth == "0300")
                {
                    status = "SUCCESS";                  
                    lblstatus.ForeColor = System.Drawing.Color.Green;
                    
                }
                else if (auth == "0399")
                {
                    status = "FAILURE";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                    lblstatus.Text = "FAILURE";
                }
                else if (auth == "NA")
                {
                    status = "ERROR";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                    lblstatus.Text = "FAILURE";
                }
                else if (auth == "0002")
                {
                    status = "ABANDONED";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                    lblstatus.Text = "FAILURE";
                }
                else if (auth == "0001")
                {
                    status = "Error At Bill Desk";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                    lblstatus.Text = "FAILURE";
                }
               
                string mob = response[16].ToString();
                string email = response[17].ToString();
                string candid = response[18].ToString();
                string pid = response[19].ToString();
                lblpid.Text = pid;
                string cname = response[20].ToString();
                string gateway = response[21].ToString();
                string failmsg = response[24].ToString();


               
                lblcname.Text = cname.ToUpper();
                lblamount.Text = txnamt;
                lblregn.Text = candid;
                lbltrid.Text = txn_id;
                lblpaymode.Text = "ONLINE";
                lbldate.Text = txndate.ToString();


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    SqlCommand cmdchck = new SqlCommand(@"select count(*) from kv_feelot with (nolock) where cand_id=@candid and [status]='SUCCESS'", con, trans);
                    cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = candid;
                    int countchck = (int)cmdchck.ExecuteScalar();
                    if (countchck == 0)
                    {
                        //  DateTime dttxndate = DateTime.Parse(txndate);
                        // DateTime dt = DateTime.ParseExact(txndate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        // SqlCommand cmdpaylog = new SqlCommand(@"update set t.[status]=@status,t.curr=@curr,t.txnrefno=@txnrefno,t.bnkrefno=@bnkrefno,t.txnamt=@txnamt,t.bnkid=@bnkid,t.bnkmerchid=@bnkmerchid,t.txntype=@txntype,t.authstatus=@authstatus,t.txndate=@txndate from kv_feelot t with(NOLOCK) where t.cand_id=@candid and t.txnid=@txnid", con, trans);
                        SqlCommand cmdpaylog = new SqlCommand(@"update kv_feelot_edit set [status]=@status,curr=@curr,txnrefno=@txnrefno,bnkrefno=@bnkrefno,txnamt=@txnamt,bnkid=@bnkid,bnkmerchid=@bnkmerchid,txntype=@txntype,authstatus=@authstatus,txndate=@txndate,gtwyname=@gtwyname where cand_id=@candid and txnid=@txnid", con, trans);
                        cmdpaylog.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = candid;
                        cmdpaylog.Parameters.AddWithValue("@txnid", SqlDbType.VarChar).Value = txn_id;
                      //  cmdpaylog.Parameters.AddWithValue("@feeamt", SqlDbType.VarChar).Value = txnamt;
                        cmdpaylog.Parameters.AddWithValue("@curr", SqlDbType.VarChar).Value = curr;
                        cmdpaylog.Parameters.AddWithValue("@status", SqlDbType.VarChar).Value = status;
                        cmdpaylog.Parameters.AddWithValue("@txnrefno", SqlDbType.VarChar).Value = txn_refno;
                        cmdpaylog.Parameters.AddWithValue("@bnkrefno", SqlDbType.VarChar).Value = bnkrefno;
                        cmdpaylog.Parameters.AddWithValue("@txnamt", SqlDbType.VarChar).Value = txnamt;
                        cmdpaylog.Parameters.AddWithValue("@bnkid", SqlDbType.VarChar).Value = bnkid;
                        cmdpaylog.Parameters.AddWithValue("@bnkmerchid", SqlDbType.VarChar).Value = bnkmerch_id;
                        cmdpaylog.Parameters.AddWithValue("@txntype", SqlDbType.VarChar).Value = txntype;
                        cmdpaylog.Parameters.AddWithValue("@txndate", SqlDbType.VarChar).Value = lbldate.Text;
                        cmdpaylog.Parameters.AddWithValue("@authstatus", SqlDbType.VarChar).Value = auth;
                        cmdpaylog.Parameters.AddWithValue("@gtwyname", SqlDbType.VarChar).Value = gateway;
                        cmdpaylog.ExecuteNonQuery();

                        SqlCommand cmdchcknow = new SqlCommand(@"select count(*) from kv_feelot_edit with (nolock) where cand_id=@candid and [status]='SUCCESS'", con, trans);
                        cmdchcknow.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = candid;
                        int countchecknow = (int)cmdchcknow.ExecuteScalar();
                        if (auth == "0300" && countchecknow != 0)
                        {
                            SqlCommand Cmdstatusfee = new SqlCommand(@"update kv_status set editstep3=@step3 where cand_id=@candid", con, trans);
                            Cmdstatusfee.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = candid;
                            Cmdstatusfee.Parameters.AddWithValue("@step3", SqlDbType.VarChar).Value = "Y";
                            Cmdstatusfee.ExecuteNonQuery();
                            btnagree.Visible = true;
                            lblstatus.Text = "SUCCESS";
                        }
                        trans.Commit();
                      
                    }

                }
                catch (Exception ex)
                {
                    lblmsg1.Text = ex.ToString();
                    trans.Rollback();
                    btnagree.Visible = false;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

            }
        }

        protected void btnagree_Click(object sender, EventArgs e)
        {
            Response.Redirect("appconf.aspx?pid="+lblpid.Text+"",false);
        }
       


     
    }
}