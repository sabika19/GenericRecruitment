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

namespace kvs
{

    public partial class admnfilter : System.Web.UI.Page
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
            if (Session["admin"] == null)
            {
                Response.Redirect("admnlogin.aspx", false);
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
            if (Session["admin"] == null)
            {
                Response.Redirect("admnlogin.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                //lblgrvcount.Text = "";
                //if (Session["candid"] == null && Request.QueryString["pid"] != null)
                //{
                //    string pid = "", uid = "";
                //    int proceed = 0;
                //    pid = Request.QueryString["pid"];

                //    if (pid.Length != 36)
                //    {
                //        Response.Redirect("bdlogin.aspx", false);
                //        return;
                //    }
                //    Session["bd"] = "Bill Desk";
                grd1.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
                BindData();
                //}

            }

        }
        private void BindData()
        {

            lblgrvcount.Text = "";
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con1.Open();
            SqlCommand cmdchck = new SqlCommand(@"select cand_id,step1,step2,step3 from kv_status", con1);

            //  SqlCommand cmdchck = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,b.step2,b.step3 from kv_cand a inner join kv_status b on a.cand_id=b.cand_id order by a.dateup desc", con);
            //  cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtgriid.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmdchck);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            con1.Close();
            con1.Dispose();
            if (dt.Rows.Count == 0)
            {
                lblgrvcount.Text = "No Data Found";
            }
            else
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][3].ToString() == "Y")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                        con.Open();
                        SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,c.photo,c.sign from kv_cand a inner join kv_photo c on a.cand_id=c.cand_id order by a.dateup", con);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        grd1.DataSource = dt1;
                        grd1.DataBind();
                        for (int i = 0; i < grd1.Rows.Count; i++)
                        {
                            Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                            HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                            Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                            byte[] bytes = (byte[])dt1.Rows[i][5];
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                            Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                            byte[] bytes2 = (byte[])dt1.Rows[i][6];
                            string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                            imgsign.ImageUrl = "data:image/png;base64," + base64String2;
                            hypconf.Visible = true;
                            lblstatus.Text = "ALL STEPS COMPLETE";
                        }
                        con.Close();
                        con.Dispose();

                    }
                    else if (dt.Rows[j][2].ToString() == "Y" && dt.Rows[j][3].ToString() == "")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                        con.Open();
                        SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,c.photo,c.sign from kv_cand a inner join kv_photo c on a.cand_id=c.cand_id order by a.dateup", con);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        grd1.DataSource = dt1;
                        grd1.DataBind();
                        for (int i = 0; i < grd1.Rows.Count; i++)
                        {
                            Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                            HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                            Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                            byte[] bytes = (byte[])dt1.Rows[i][5];
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                            Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                            byte[] bytes2 = (byte[])dt1.Rows[i][6];
                            string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                            imgsign.ImageUrl = "data:image/png;base64," + base64String2;


                            lblstatus.Text = "COMPLETE TILL STEP 2";

                            hypconf.Visible = false;
                        }
                        con.Close();
                        con.Dispose();
                    }
                    else if (dt.Rows[j][2].ToString() == "" && dt.Rows[j][3].ToString() == "")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                        con.Open();
                        SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob from kv_cand a order by a.dateup", con);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        grd1.DataSource = dt1;
                        grd1.DataBind();
                        for (int i = 0; i < grd1.Rows.Count; i++)
                        {
                            Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                            lblstatus.Text = "COMPLETE TILL STEP 1";
                            Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                            Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                            imgphoto.Visible = false;
                            imgsign.Visible = false;
                            HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                            hypconf.Visible = false;
                        }
                        con.Close();
                        con.Dispose();

                    }
                }
                lblgrvcount.Text = "";
            }
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            grd1.PageIndex = e.NewPageIndex;
            grd1.DataBind();
        }

        protected void grd1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }



        protected void btnFind_Click(object sender, EventArgs e)
        {

            lblgrvcount.Text = "";
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con1.Open();
            SqlCommand cmdchck = new SqlCommand(@"select cand_id,step1,step2,step3 from kv_status where cand_id=@candid", con1);
            cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtgriid.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmdchck);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            con1.Close();
            con1.Dispose();

            if (dt.Rows.Count == 0)
            {
                lblgrvcount.Text = "No Data Found";
            }
            else
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][3].ToString() == "Y")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                        con.Open();
                        SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,c.photo,c.sign from kv_cand a inner join kv_photo c on a.cand_id=c.cand_id where cand_id=@candid", con);
                        cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtgriid.Text;
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        grd1.DataSource = dt1;
                        grd1.DataBind();
                        for (int i = 0; i < grd1.Rows.Count; i++)
                        {
                            Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                            HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                            Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                            byte[] bytes = (byte[])dt1.Rows[i][5];
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                            Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                            byte[] bytes2 = (byte[])dt1.Rows[i][6];
                            string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                            imgsign.ImageUrl = "data:image/png;base64," + base64String2;
                            hypconf.Visible = true;
                            lblstatus.Text = "ALL STEPS COMPLETE";
                        }
                        con.Close();
                        con.Dispose();

                    }
                    else if (dt.Rows[j][2].ToString() == "Y" && dt.Rows[j][3].ToString() == "")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                        con.Open();
                        SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,c.photo,c.sign from kv_cand a inner join kv_photo c on a.cand_id=c.cand_id where cand_id=@candid", con);
                        cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtgriid.Text;
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        grd1.DataSource = dt1;
                        grd1.DataBind();
                        for (int i = 0; i < grd1.Rows.Count; i++)
                        {
                            Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                            HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                            Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                            byte[] bytes = (byte[])dt1.Rows[i][5];
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                            Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                            byte[] bytes2 = (byte[])dt1.Rows[i][6];
                            string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                            imgsign.ImageUrl = "data:image/png;base64," + base64String2;


                            lblstatus.Text = "COMPLETE TILL STEP 2";

                            hypconf.Visible = false;
                        }
                        con.Close();
                        con.Dispose();
                    }
                    else if (dt.Rows[j][2].ToString() == "" && dt.Rows[j][3].ToString() == "")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                        con.Open();
                        SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob from kv_cand a where cand_id=@candid", con);
                        cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtgriid.Text;
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        grd1.DataSource = dt1;
                        grd1.DataBind();
                        for (int i = 0; i < grd1.Rows.Count; i++)
                        {
                            Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                            lblstatus.Text = "COMPLETE TILL STEP 1";
                            Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                            Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                            imgphoto.Visible = false;
                            imgsign.Visible = false;
                            HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                            hypconf.Visible = false;
                        }
                        con.Close();
                        con.Dispose();

                    }
                }
                lblgrvcount.Text = "";
            }


        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
           

        }

        protected void grd1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            lblgrvcount.Text = "";
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con1.Open();
            if (ddlfilter.SelectedIndex == 1)
            {
                SqlCommand cmdchck = new SqlCommand(@"select cand_id,step1,step2,step3 from kv_status where step1='Y' and step2 is null and step3 is null", con1);
                SqlDataAdapter sda = new SqlDataAdapter(cmdchck);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con1.Close();
                con1.Dispose();
                if (dt.Rows.Count == 0)
                {
                    lblgrvcount.Text = "No Data Found";
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                    con.Open();
                    SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob from kv_cand a order by a.dateup", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    grd1.DataSource = dt1;
                    grd1.DataBind();
                    for (int i = 0; i < grd1.Rows.Count; i++)
                    {
                        Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                        lblstatus.Text = "COMPLETE TILL STEP 1";
                        Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                        Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                        imgphoto.Visible = false;
                        imgsign.Visible = false;
                        HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                        hypconf.Visible = false;
                    }
                    lblgrvcount.Text = "";
                    con.Close();
                    con.Dispose();
                }
            }
            else if (ddlfilter.SelectedIndex == 2)
            {
                SqlCommand cmdchck = new SqlCommand(@"select cand_id,step1,step2,step3 from kv_status where step1='Y' and step2='Y' and step3 is null", con1);
                SqlDataAdapter sda = new SqlDataAdapter(cmdchck);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con1.Close();
                con1.Dispose();
                if (dt.Rows.Count == 0)
                {
                    lblgrvcount.Text = "No Data Found";
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                    con.Open();
                    SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,c.photo,c.sign from kv_cand a inner join kv_photo c on a.cand_id=c.cand_id order by a.dateup", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    grd1.DataSource = dt1;
                    grd1.DataBind();
                    for (int i = 0; i < grd1.Rows.Count; i++)
                    {
                        Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                        HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                        Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                        byte[] bytes = (byte[])dt1.Rows[i][5];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                        Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                        byte[] bytes2 = (byte[])dt1.Rows[i][6];
                        string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                        imgsign.ImageUrl = "data:image/png;base64," + base64String2;


                        lblstatus.Text = "COMPLETE TILL STEP 2";

                        hypconf.Visible = false;
                    }
                    con.Close();
                    con.Dispose();
                    lblgrvcount.Text = "";
                }
            }
            else if (ddlfilter.SelectedIndex == 3)
            {
                SqlCommand cmdchck = new SqlCommand(@"select cand_id,step1,step2,step3 from kv_status where step1='Y' and step2='Y' and step3 ='Y'", con1);
                SqlDataAdapter sda = new SqlDataAdapter(cmdchck);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con1.Close();
                con1.Dispose();
                if (dt.Rows.Count == 0)
                {
                    lblgrvcount.Text = "No Data Found";
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                    con.Open();
                    SqlCommand cmdfilterall = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,c.photo,c.sign from kv_cand a inner join kv_photo c on a.cand_id=c.cand_id order by a.dateup", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmdfilterall);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    grd1.DataSource = dt1;
                    grd1.DataBind();
                    for (int i = 0; i < grd1.Rows.Count; i++)
                    {
                        Label lblstatus = (Label)grd1.Rows[i].FindControl("lblstatus");
                        HyperLink hypconf = (HyperLink)grd1.Rows[i].FindControl("hypconf");
                        Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                        byte[] bytes = (byte[])dt1.Rows[i][5];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                        Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                        byte[] bytes2 = (byte[])dt1.Rows[i][6];
                        string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                        imgsign.ImageUrl = "data:image/png;base64," + base64String2;


                        lblstatus.Text = "ALL STEPS COMPLETED";

                        hypconf.Visible = true;
                    }
                    con.Close();
                    con.Dispose();
                    lblgrvcount.Text = "";
                }
            }
        }
    }
}
    