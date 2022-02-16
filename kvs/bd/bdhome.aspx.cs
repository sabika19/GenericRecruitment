using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kvs
{

    public partial class bdhome : System.Web.UI.Page
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
            if (Session["bd"] == null)
            {
                Response.Redirect("bdlogin.aspx", false);
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
            if (Session["bd"] == null)
            {
                Response.Redirect("bdlogin.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
               
                    grd1.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
                   // BindData();
              

            }
            

        }
        private void BindData()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand cmdchck = new SqlCommand(@"select * from kv_feelot where [status]='SUCCESS' order by txndate desc", con);
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
                lblgrvcount.Text = "";
            }

        }
        private void BindDatadate()
        {

            lblgrvcount.Text = "";
            DateTime datefrom = DateTime.ParseExact(datepicker1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateto = DateTime.ParseExact(datepicker2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand cmdchck = new SqlCommand(@"select cand_id,''''+txnid+'' as txnid,txnrefno,inifee,txndate from kv_feelot where [status]='SUCCESS' and gtwyname=@gtwyname and inidate>='" + datefrom + "' and inidate<='" + dateto + "' ", con);
            cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtgriid.Text;
            cmdchck.Parameters.AddWithValue("@gtwyname", SqlDbType.VarChar).Value = rdblbank.SelectedValue;
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
            BindData();
            lblgrvcount.Text = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand cmdchck = new SqlCommand(@"select cand_id,''''+txnid+'' as txnid,txnrefno,inifee,txndate from kv_feelot where cand_id=@candid and [status]='SUCCESS' and gtwyname=@gtwyname", con);
            cmdchck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = txtgriid.Text;
            cmdchck.Parameters.AddWithValue("@gtwyname", SqlDbType.VarChar).Value = rdblbank.SelectedValue;
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
                lblgrvcount.Text = "";
            }

        }

        protected void expExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=CBSEKVSReport_" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grd1.AllowPaging = false;
                this.BindData();
              
                grd1.HeaderRow.BackColor = Color.White;

                foreach (TableCell cell in grd1.HeaderRow.Cells)
                {
                    cell.BackColor = grd1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grd1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grd1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grd1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grd1.RenderControl(hw);

                //style to format numbers to string
                 string style = @"<style> .textmode { } </style>";
               // string style = @"<style> td { mso-number-format:'0'; } </style>";

                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnFilDate_Click(object sender, EventArgs e)
        {
            BindDatadate();
        }

        protected void expExceldate_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=CBSEKVSReport_" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grd1.AllowPaging = false;
                this.BindDatadate();

                grd1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grd1.HeaderRow.Cells)
                {
                    cell.BackColor = grd1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grd1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grd1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grd1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grd1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
               // string style = @"<style> td { mso-number-format:'0'; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
      
    }
}
    