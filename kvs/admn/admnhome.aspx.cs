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

    public partial class admnhome : System.Web.UI.Page
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
                   
                //}

            }

        }
        private void BindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand cmdchck = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,c.photo,c.sign,a.flag from kv_cand a inner join kv_status b on a.cand_id=b.cand_id inner join kv_photo c on a.cand_id=c.cand_id where b.step1='Y' and b.step2='Y' and b.step3='Y' order by a.dateup desc", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmdchck);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grd1.DataSource = dt;
            grd1.DataBind();
            con.Close();
            con.Dispose();
            if(dt.Rows.Count==0)
            {
                lblgrvcount.Text = "No Data Found";
            }
            else
            {
                for (int i=0;i< grd1.Rows.Count;i++)
                {
                    CheckBox chckflag = (CheckBox)grd1.Rows[i].FindControl("chckflag");
                    if(dt.Rows[i][7].ToString()=="Y")
                    {
                        chckflag.Checked = true;
                    }
                    else
                    {
                        chckflag.Checked = false;
                    }
                    Label lbldob=(Label)grd1.Rows[i].FindControl("lbldob");
                    String dob = DateTime.Parse(lbldob.Text).ToString("dd/MM/yyyy");
                    lbldob.Text = dob;

                    Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                    byte[] bytes = (byte[])dt.Rows[i][5];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                    Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                    byte[] bytes2 = (byte[])dt.Rows[i][6];
                    string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                    imgsign.ImageUrl = "data:image/png;base64," + base64String2;


                }
                lblgrvcount.Text = "";
            }
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            grd1.PageIndex = e.NewPageIndex;
            //grd1.DataBind();
        }

        protected void grd1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index = grd1.SelectedRow.RowIndex;
            //Label lblcandid = (Label)grd1.Rows[index].FindControl("lblcandid");

            //string candid = lblcandid.Text;
        
            //Response.Redirect("admnconf.aspx?candid=" + candid+"", false);
        }

      

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (txtgriid.Text != "")
            {
                lblgrvcount.Text = "";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                con.Open();
                SqlCommand cmdchck = new SqlCommand(@"select a.cand_id,a.cname_f+' '+a.cname_l as cname,a.mname,a.fname,a.dob,c.photo,c.sign,a.flag from kv_cand a inner join kv_status b on a.cand_id=b.cand_id inner join kv_photo c on a.cand_id=c.cand_id where b.step1='Y' and b.step2='Y' and b.step3='Y' and a.cand_id=@candid order by a.dateup", con);
                cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtgriid.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmdchck);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                grd1.DataSource = dt;
                grd1.DataBind();
                con.Close();
                con.Dispose();
                if (dt.Rows.Count == 0)
                {
                    lblgrvcount.Text = "No Data Found";
                }
                else
                {
                    for (int i = 0; i < grd1.Rows.Count; i++)
                    {
                        CheckBox chckflag = (CheckBox)grd1.Rows[i].FindControl("chckflag");
                        if (dt.Rows[i][7].ToString() == "Y")
                        {
                            chckflag.Checked = true;
                        }
                        else
                        {
                            chckflag.Checked = false;
                        }
                        Label lbldob = (Label)grd1.Rows[i].FindControl("lbldob");
                        String dob = DateTime.Parse(lbldob.Text).ToString("dd/MM/yyyy");
                        lbldob.Text = dob;

                        Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                        byte[] bytes = (byte[])dt.Rows[i][5];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                        Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                        byte[] bytes2 = (byte[])dt.Rows[i][6];
                        string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                        imgsign.ImageUrl = "data:image/png;base64," + base64String2;
                    }
                    lblgrvcount.Text = "";
                }
            }
            else
            {
                BindData();
            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            lblgrvcount.Text = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            for (int i = 0; i < grd1.Rows.Count; i++)
            {
                CheckBox chckflag = (CheckBox)grd1.Rows[i].FindControl("chckflag");
                Label lblcandid = (Label)grd1.Rows[i].FindControl("lblcandid");
                if (chckflag.Checked==true)
                {
                    SqlCommand cmdchck = new SqlCommand(@"update kv_cand set flag='Y' where cand_id=@candid", con);
                    cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = lblcandid.Text;
                    cmdchck.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdchck = new SqlCommand(@"update kv_cand set flag=null where cand_id=@candid", con);
                    cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = lblcandid.Text;
                    cmdchck.ExecuteNonQuery();
                }
              
            }
            con.Close();
            con.Dispose();
            lblgrvcount.Text = "All Flags Updated.";
           
        }

        protected void grd1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grd1, "Select$" + e.Row.RowIndex);
            //    e.Row.Attributes["style"] = "cursor:pointer";
            //}
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnSummary_Click(object sender, EventArgs e)
        {

            GetCustomersPageWise(1);
        }
        private void GetCustomersPageWise(int pageIndex)
        {
            string constring = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetCustomersPageWise", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", "10");
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    //IDataReader idr = cmd.ExecuteReader();
                    grd1.DataSource = dt;
                    grd1.DataBind();             
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    this.PopulatePager(recordCount, pageIndex);
                    for (int i = 0; i < grd1.Rows.Count; i++)
                    {
                        CheckBox chckflag = (CheckBox)grd1.Rows[i].FindControl("chckflag");
                        if (dt.Rows[i][8].ToString() == "Y")
                        {
                            chckflag.Checked = true;
                        }
                        else
                        {
                            chckflag.Checked = false;
                        }
                        Label lbldob = (Label)grd1.Rows[i].FindControl("lbldob");
                        String dob = DateTime.Parse(lbldob.Text).ToString("dd/MM/yyyy");
                        lbldob.Text = dob;

                        Image imgphoto = (Image)grd1.Rows[i].FindControl("imgphoto");
                        byte[] bytes = (byte[])dt.Rows[i][6];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        imgphoto.ImageUrl = "data:image/png;base64," + base64String;

                        Image imgsign = (Image)grd1.Rows[i].FindControl("imgsign");
                        byte[] bytes2 = (byte[])dt.Rows[i][7];
                        string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                        imgsign.ImageUrl = "data:image/png;base64," + base64String2;
                    }
                    dt.Dispose();
                    con.Close();
                }
            }
        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / decimal.Parse("10"));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("First", "1", currentPage > 1));
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.GetCustomersPageWise(pageIndex);
        }
    }
}
    