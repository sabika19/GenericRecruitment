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

    public partial class admnReport : System.Web.UI.Page
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
               

            }


        }

        protected void btnpost_Click(object sender, EventArgs e)
        {
            if (ddlpost.SelectedValue != "0")
            {
                string column_name = ddlpost.SelectedValue;
                try
                {
                    lblgrvcount.Text = "";
                    lbldate.Text = DateTime.Now.ToShortDateString();
                    String datenow = DateTime.Parse(lbldate.Text).ToString("dd/MM/yyyy");
                    lbldate.Text = datenow + " " + DateTime.Now.ToShortTimeString();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                    con.Open();
                    SqlCommand cmdreg = new SqlCommand(@"select count(cand_id) from kv_cand where " + column_name + " is not null", con);
                    int countreg = (int)cmdreg.ExecuteScalar();
                    lblregcand.Text = countreg.ToString();

                    SqlCommand cmdphoto = new SqlCommand(@"select count(a.cand_id) from kv_photo a  inner join kv_cand b on a.cand_id=b.cand_id where b." + column_name + " is not null", con);
                    int countphoto = (int)cmdphoto.ExecuteScalar();
                    lblphoto.Text = countphoto.ToString();

                    SqlCommand cmdpay = new SqlCommand(@"select count(a.cand_id) from kv_feelot a  inner join kv_cand b on a.cand_id=b.cand_id where [status]='SUCCESS' and b." + column_name + " is not null", con);
                    int countpay = (int)cmdpay.ExecuteScalar();
                    lblpay.Text = countpay.ToString();

                    SqlCommand cmdall = new SqlCommand(@"select count(a.cand_id) from kv_status a  inner join kv_cand b on a.cand_id=b.cand_id where step3='Y' and b." + column_name + " is not null", con);
                    int countall = (int)cmdall.ExecuteScalar();
                    lblall.Text = countall.ToString();

                    //general male
                    SqlCommand cmdreggnnmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G' and gender='M' and " + column_name + " is not null", con);
                    int countreggnml = (int)cmdreggnnmale.ExecuteScalar();
                    lblmlreggen.Text = countreggnml.ToString();

                    SqlCommand cmdgenmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G' and b.gender='M' and b." + column_name + " is not null", con);
                    int countphotogenml = (int)cmdgenmlpg.ExecuteScalar();
                    lblmlphgen.Text = countphotogenml.ToString();

                    SqlCommand cmdpaymlgen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G' and b.gender='M' and b." + column_name + " is not null", con);
                    int countpaymlgen = (int)cmdpaymlgen.ExecuteScalar();
                    lblmlfeegen.Text = countpaymlgen.ToString();

                    SqlCommand cmdallmlgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G' and b.gender='M' and b." + column_name + " is not null", con);
                    int countallmlgen = (int)cmdallmlgen.ExecuteScalar();
                    lblmlcompgen.Text = countallmlgen.ToString();

                    //obc male
                    SqlCommand cmdregobcmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O' and gender='M' and " + column_name + " is not null", con);
                    int countregobcml = (int)cmdregobcmale.ExecuteScalar();
                    lblmlrego.Text = countregobcml.ToString();

                    SqlCommand cmdobcmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O' and b.gender='M' and b." + column_name + " is not null", con);
                    int countphotoobcml = (int)cmdobcmlpg.ExecuteScalar();
                    lblmlpho.Text = countphotoobcml.ToString();

                    SqlCommand cmdpaymlobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O' and b.gender='M' and b." + column_name + " is not null", con);
                    int countpaymlobc = (int)cmdpaymlobc.ExecuteScalar();
                    lblmlfeeo.Text = countpaymlobc.ToString();

                    SqlCommand cmdallmlobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O' and b.gender='M' and b." + column_name + " is not null", con);
                    int countallmlobc = (int)cmdallmlobc.ExecuteScalar();
                    lblmlcompo.Text = countallmlobc.ToString();

                    //sc male
                    SqlCommand cmdregcmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C' and gender='M' and " + column_name + " is not null", con);
                    int countregcml = (int)cmdregcmale.ExecuteScalar();
                    lblmlregc.Text = countregcml.ToString();

                    SqlCommand cmdcmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C' and b.gender='M' and b." + column_name + " is not null", con);
                    int countphotocml = (int)cmdcmlpg.ExecuteScalar();
                    lblmlphc.Text = countphotocml.ToString();

                    SqlCommand cmdpaymlc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C' and b.gender='M' and b." + column_name + " is not null", con);
                    int countpaymlc = (int)cmdpaymlc.ExecuteScalar();
                    lblmlfeec.Text = countpaymlc.ToString();

                    SqlCommand cmdallmlc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C' and b.gender='M' and b." + column_name + " is not null", con);
                    int countallmlc = (int)cmdallmlc.ExecuteScalar();
                    lblmlcompc.Text = countallmlc.ToString();

                    //st male
                    SqlCommand cmdregtmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T' and gender='M' and " + column_name + " is not null", con);
                    int countregtml = (int)cmdregtmale.ExecuteScalar();
                    lblmlregt.Text = countregtml.ToString();

                    SqlCommand cmdtmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' and b.gender='M' and b." + column_name + " is not null", con);
                    int countphototml = (int)cmdtmlpg.ExecuteScalar();
                    lblmlpht.Text = countphototml.ToString();

                    SqlCommand cmdpaymlt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' and b.gender='M' and b." + column_name + " is not null", con);
                    int countpaymlt = (int)cmdpaymlt.ExecuteScalar();
                    lblmlfeet.Text = countpaymlt.ToString();

                    SqlCommand cmdallmlt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' and b.gender='M' and b." + column_name + " is not null", con);
                    int countallmlt = (int)cmdallmlt.ExecuteScalar();
                    lblmlcompt.Text = countallmlt.ToString();

                    //pwd male
                    SqlCommand cmdregpmale = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null and gender='M' and " + column_name + " is not null", con);
                    int countregpml = (int)cmdregpmale.ExecuteScalar();
                    lblmlregp.Text = countregpml.ToString();

                    SqlCommand cmdpmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null and b.gender='M' and b." + column_name + " is not null", con);
                    int countphotopml = (int)cmdpmlpg.ExecuteScalar();
                    lblmlphp.Text = countphotopml.ToString();

                    SqlCommand cmdpaymlp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null and b.gender='M' and b." + column_name + " is not null", con);
                    int countpaymlp = (int)cmdpaymlp.ExecuteScalar();
                    lblmlfeep.Text = countpaymlp.ToString();

                    SqlCommand cmdallmlp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null and b.gender='M' and b." + column_name + " is not null", con);
                    int countallmlp = (int)cmdallmlp.ExecuteScalar();
                    lblmlcompp.Text = countallmlp.ToString();

                    //all male
                    SqlCommand cmdregmale = new SqlCommand(@"select count(cand_id) from kv_cand where gender='M' and " + column_name + " is not null", con);
                    int countregml = (int)cmdregmale.ExecuteScalar();
                    lblmlreg.Text = countregml.ToString();

                    SqlCommand cmdmlph = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.gender='M' and b." + column_name + " is not null", con);
                    int countphotoml = (int)cmdmlph.ExecuteScalar();
                    lblmlph.Text = countphotoml.ToString();

                    SqlCommand cmdpayml = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.gender='M' and b." + column_name + " is not null", con);
                    int countpayml = (int)cmdpayml.ExecuteScalar();
                    lblmlfee.Text = countpayml.ToString();

                    SqlCommand cmdallml = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.gender='M' and b." + column_name + " is not null", con);
                    int countallml = (int)cmdallml.ExecuteScalar();
                    lblmlcomp.Text = countallml.ToString();




                    //general female
                    SqlCommand cmdreggnnfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G' and gender='F' and " + column_name + " is not null", con);
                    int countreggnfl = (int)cmdreggnnfemale.ExecuteScalar();
                    lblflreggen.Text = countreggnfl.ToString();

                    SqlCommand cmdgenflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G' and b.gender='F' and b." + column_name + " is not null", con);
                    int countphotogenfl = (int)cmdgenflpg.ExecuteScalar();
                    lblflphgen.Text = countphotogenfl.ToString();

                    SqlCommand cmdpayflgen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G' and b.gender='F' and b." + column_name + " is not null", con);
                    int countpayflgen = (int)cmdpayflgen.ExecuteScalar();
                    lblflfeegen.Text = countpayflgen.ToString();

                    SqlCommand cmdallflgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G' and b.gender='F' and b." + column_name + " is not null", con);
                    int countallflgen = (int)cmdallflgen.ExecuteScalar();
                    lblflcompgen.Text = countallflgen.ToString();

                    //obc female
                    SqlCommand cmdregobcfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O' and gender='F' and " + column_name + " is not null", con);
                    int countregobcfl = (int)cmdregobcfemale.ExecuteScalar();
                    lblflrego.Text = countregobcfl.ToString();

                    SqlCommand cmdobcflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O' and b.gender='F' and b." + column_name + " is not null", con);
                    int countphotoobcfl = (int)cmdobcflpg.ExecuteScalar();
                    lblflpho.Text = countphotoobcfl.ToString();

                    SqlCommand cmdpayflobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O' and b.gender='F' and b." + column_name + " is not null", con);
                    int countpayflobc = (int)cmdpayflobc.ExecuteScalar();
                    lblflfeeo.Text = countpayflobc.ToString();

                    SqlCommand cmdallflobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O' and b.gender='F' and b." + column_name + " is not null", con);
                    int countallflobc = (int)cmdallflobc.ExecuteScalar();
                    lblflcompo.Text = countallflobc.ToString();

                    //sc female
                    SqlCommand cmdregcfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C' and gender='F' and " + column_name + " is not null", con);
                    int countregcfl = (int)cmdregcfemale.ExecuteScalar();
                    lblflregc.Text = countregcfl.ToString();

                    SqlCommand cmdcflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C' and b.gender='F' and b." + column_name + " is not null", con);
                    int countphotocfl = (int)cmdcflpg.ExecuteScalar();
                    lblflphc.Text = countphotocfl.ToString();

                    SqlCommand cmdpayflc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C' and b.gender='F' and b." + column_name + " is not null", con);
                    int countpayflc = (int)cmdpayflc.ExecuteScalar();
                    lblflfeec.Text = countpayflc.ToString();

                    SqlCommand cmdallflc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C' and b.gender='F' and b." + column_name + " is not null", con);
                    int countallflc = (int)cmdallflc.ExecuteScalar();
                    lblflcompc.Text = countallflc.ToString();

                    //st female
                    SqlCommand cmdregtfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T' and gender='F' and " + column_name + " is not null", con);
                    int countregtfl = (int)cmdregtfemale.ExecuteScalar();
                    lblflregt.Text = countregtfl.ToString();

                    SqlCommand cmdtflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' and b.gender='F' and b." + column_name + " is not null", con);
                    int countphototfl = (int)cmdtflpg.ExecuteScalar();
                    lblflpht.Text = countphototfl.ToString();

                    SqlCommand cmdpayflt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' and b.gender='F' and b." + column_name + " is not null", con);
                    int countpayflt = (int)cmdpayflt.ExecuteScalar();
                    lblflfeet.Text = countpayflt.ToString();

                    SqlCommand cmdallflt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' and b.gender='F' and b." + column_name + " is not null", con);
                    int countallflt = (int)cmdallflt.ExecuteScalar();
                    lblflcompt.Text = countallflt.ToString();

                    //pwd female
                    SqlCommand cmdregpfemale = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null and gender='F' and " + column_name + " is not null", con);
                    int countregpfl = (int)cmdregpfemale.ExecuteScalar();
                    lblflregp.Text = countregpfl.ToString();

                    SqlCommand cmdpflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null and b.gender='F' and b." + column_name + " is not null", con);
                    int countphotopfl = (int)cmdpflpg.ExecuteScalar();
                    lblflphp.Text = countphotopfl.ToString();

                    SqlCommand cmdpayflp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null and b.gender='F' and b." + column_name + " is not null", con);
                    int countpayflp = (int)cmdpayflp.ExecuteScalar();
                    lblflfeep.Text = countpayflp.ToString();

                    SqlCommand cmdallflp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null and b.gender='F' and b." + column_name + " is not null", con);
                    int countallflp = (int)cmdallflp.ExecuteScalar();
                    lblflcompp.Text = countallflp.ToString();

                    //all female
                    SqlCommand cmdregfemale = new SqlCommand(@"select count(cand_id) from kv_cand where gender='F' and " + column_name + " is not null", con);
                    int countregfl = (int)cmdregfemale.ExecuteScalar();
                    lblflreg.Text = countregfl.ToString();

                    SqlCommand cmdflph = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.gender='F' and b." + column_name + " is not null", con);
                    int countphotofl = (int)cmdflph.ExecuteScalar();
                    lblflph.Text = countphotofl.ToString();

                    SqlCommand cmdpayfl = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.gender='F' and b." + column_name + " is not null", con);
                    int countpayfl = (int)cmdpayfl.ExecuteScalar();
                    lblflfee.Text = countpayfl.ToString();

                    SqlCommand cmdallfl = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.gender='F' and b." + column_name + " is not null", con);
                    int countallfl = (int)cmdallfl.ExecuteScalar();
                    lblflcomp.Text = countallfl.ToString();

                    //general
                    SqlCommand cmdreggn = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G' and " + column_name + " is not null", con);
                    int countreggn = (int)cmdreggn.ExecuteScalar();
                    lblreggen.Text = countreggn.ToString();

                    SqlCommand cmdgenpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G' and b." + column_name + " is not null", con);
                    int countphotogen = (int)cmdgenpg.ExecuteScalar();
                    lblphgen.Text = countphotogen.ToString();

                    SqlCommand cmdpaygen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G' and b." + column_name + " is not null", con);
                    int countpaygen = (int)cmdpaygen.ExecuteScalar();
                    lblfeegen.Text = countpaygen.ToString();

                    SqlCommand cmdallgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G' and b." + column_name + " is not null", con);
                    int countallgen = (int)cmdallgen.ExecuteScalar();
                    lblcompgen.Text = countallgen.ToString();

                    //obc 
                    SqlCommand cmdregobc = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O' and " + column_name + " is not null", con);
                    int countregobc = (int)cmdregobc.ExecuteScalar();
                    lblrego.Text = countregobc.ToString();

                    SqlCommand cmdobcpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O' and b." + column_name + " is not null", con);
                    int countphotoobc = (int)cmdobcpg.ExecuteScalar();
                    lblpho.Text = countphotoobc.ToString();

                    SqlCommand cmdpayobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O' and b." + column_name + " is not null", con);
                    int countpayobc = (int)cmdpayobc.ExecuteScalar();
                    lblfeeo.Text = countpayobc.ToString();

                    SqlCommand cmdallobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O' and b." + column_name + " is not null", con);
                    int countallobc = (int)cmdallobc.ExecuteScalar();
                    lblcompo.Text = countallobc.ToString();

                    //sc 
                    SqlCommand cmdregc = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C' and " + column_name + " is not null", con);
                    int countregc = (int)cmdregc.ExecuteScalar();
                    lblregc.Text = countregc.ToString();

                    SqlCommand cmdcpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C' and b." + column_name + " is not null", con);
                    int countphotoc = (int)cmdcpg.ExecuteScalar();
                    lblphc.Text = countphotoc.ToString();

                    SqlCommand cmdpayc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C' and b." + column_name + " is not null", con);
                    int countpayc = (int)cmdpayc.ExecuteScalar();
                    lblfeec.Text = countpayc.ToString();

                    SqlCommand cmdallc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C' and b." + column_name + " is not null", con);
                    int countallc = (int)cmdallc.ExecuteScalar();
                    lblcompc.Text = countallc.ToString();

                    //st 
                    SqlCommand cmdregt = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T' and " + column_name + " is not null", con);
                    int countregt = (int)cmdregt.ExecuteScalar();
                    lblregt.Text = countregt.ToString();

                    SqlCommand cmdtpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' and b." + column_name + " is not null ", con);
                    int countphotot = (int)cmdtpg.ExecuteScalar();
                    lblpht.Text = countphotot.ToString();

                    SqlCommand cmdpayt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' and b." + column_name + " is not null", con);
                    int countpayt = (int)cmdpayt.ExecuteScalar();
                    lblfeet.Text = countpayt.ToString();

                    SqlCommand cmdallt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' and b." + column_name + " is not null", con);
                    int countallt = (int)cmdallt.ExecuteScalar();
                    lblcompt.Text = countallt.ToString();

                    //pwd 
                    SqlCommand cmdregp = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null and " + column_name + " is not null", con);
                    int countregp = (int)cmdregp.ExecuteScalar();
                    lblregp.Text = countregp.ToString();

                    SqlCommand cmdppg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null and b." + column_name + " is not null", con);
                    int countphotop = (int)cmdppg.ExecuteScalar();
                    lblphp.Text = countphotop.ToString();

                    SqlCommand cmdpayp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null and b." + column_name + " is not null", con);
                    int countpayp = (int)cmdpayp.ExecuteScalar();
                    lblfeep.Text = countpayp.ToString();

                    SqlCommand cmdallp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null and b." + column_name + " is not null", con);
                    int countallp = (int)cmdallp.ExecuteScalar();
                    lblcompp.Text = countallp.ToString();



                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    lblgrvcount.Text = ex.ToString();
                    return;
                }
            }
            else
            {
                try
                {
                    lblgrvcount.Text = "";
                    lbldate.Text = DateTime.Now.ToShortDateString();
                    String datenow = DateTime.Parse(lbldate.Text).ToString("dd/MM/yyyy");
                    lbldate.Text = datenow + " " + DateTime.Now.ToShortTimeString();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                    con.Open();
                    SqlCommand cmdreg = new SqlCommand(@"select count(cand_id) from kv_cand", con);
                    int countreg = (int)cmdreg.ExecuteScalar();
                    lblregcand.Text = countreg.ToString();

                    SqlCommand cmdphoto = new SqlCommand(@"select count(cand_id) from kv_photo", con);
                    int countphoto = (int)cmdphoto.ExecuteScalar();
                    lblphoto.Text = countphoto.ToString();

                    SqlCommand cmdpay = new SqlCommand(@"select count(cand_id) from kv_feelot where [status]='SUCCESS'", con);
                    int countpay = (int)cmdpay.ExecuteScalar();
                    lblpay.Text = countpay.ToString();

                    SqlCommand cmdall = new SqlCommand(@"select count(cand_id) from kv_status where step3='Y'", con);
                    int countall = (int)cmdall.ExecuteScalar();
                    lblall.Text = countall.ToString();

                    //general male
                    SqlCommand cmdreggnnmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G' and gender='M'", con);
                    int countreggnml = (int)cmdreggnnmale.ExecuteScalar();
                    lblmlreggen.Text = countreggnml.ToString();

                    SqlCommand cmdgenmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G' and b.gender='M'", con);
                    int countphotogenml = (int)cmdgenmlpg.ExecuteScalar();
                    lblmlphgen.Text = countphotogenml.ToString();

                    SqlCommand cmdpaymlgen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G' and b.gender='M'", con);
                    int countpaymlgen = (int)cmdpaymlgen.ExecuteScalar();
                    lblmlfeegen.Text = countpaymlgen.ToString();

                    SqlCommand cmdallmlgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G' and b.gender='M'", con);
                    int countallmlgen = (int)cmdallmlgen.ExecuteScalar();
                    lblmlcompgen.Text = countallmlgen.ToString();

                    //obc male
                    SqlCommand cmdregobcmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O' and gender='M'", con);
                    int countregobcml = (int)cmdregobcmale.ExecuteScalar();
                    lblmlrego.Text = countregobcml.ToString();

                    SqlCommand cmdobcmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O' and b.gender='M'", con);
                    int countphotoobcml = (int)cmdobcmlpg.ExecuteScalar();
                    lblmlpho.Text = countphotoobcml.ToString();

                    SqlCommand cmdpaymlobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O' and b.gender='M'", con);
                    int countpaymlobc = (int)cmdpaymlobc.ExecuteScalar();
                    lblmlfeeo.Text = countpaymlobc.ToString();

                    SqlCommand cmdallmlobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O' and b.gender='M'", con);
                    int countallmlobc = (int)cmdallmlobc.ExecuteScalar();
                    lblmlcompo.Text = countallmlobc.ToString();

                    //sc male
                    SqlCommand cmdregcmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C' and gender='M'", con);
                    int countregcml = (int)cmdregcmale.ExecuteScalar();
                    lblmlregc.Text = countregcml.ToString();

                    SqlCommand cmdcmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C' and b.gender='M'", con);
                    int countphotocml = (int)cmdcmlpg.ExecuteScalar();
                    lblmlphc.Text = countphotocml.ToString();

                    SqlCommand cmdpaymlc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C' and b.gender='M'", con);
                    int countpaymlc = (int)cmdpaymlc.ExecuteScalar();
                    lblmlfeec.Text = countpaymlc.ToString();

                    SqlCommand cmdallmlc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C' and b.gender='M'", con);
                    int countallmlc = (int)cmdallmlc.ExecuteScalar();
                    lblmlcompc.Text = countallmlc.ToString();

                    //st male
                    SqlCommand cmdregtmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T' and gender='M'", con);
                    int countregtml = (int)cmdregtmale.ExecuteScalar();
                    lblmlregt.Text = countregtml.ToString();

                    SqlCommand cmdtmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' and b.gender='M'", con);
                    int countphototml = (int)cmdtmlpg.ExecuteScalar();
                    lblmlpht.Text = countphototml.ToString();

                    SqlCommand cmdpaymlt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' and b.gender='M'", con);
                    int countpaymlt = (int)cmdpaymlt.ExecuteScalar();
                    lblmlfeet.Text = countpaymlt.ToString();

                    SqlCommand cmdallmlt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' and b.gender='M'", con);
                    int countallmlt = (int)cmdallmlt.ExecuteScalar();
                    lblmlcompt.Text = countallmlt.ToString();

                    //pwd male
                    SqlCommand cmdregpmale = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null and gender='M'", con);
                    int countregpml = (int)cmdregpmale.ExecuteScalar();
                    lblmlregp.Text = countregpml.ToString();

                    SqlCommand cmdpmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null and b.gender='M'", con);
                    int countphotopml = (int)cmdpmlpg.ExecuteScalar();
                    lblmlphp.Text = countphotopml.ToString();

                    SqlCommand cmdpaymlp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null and b.gender='M'", con);
                    int countpaymlp = (int)cmdpaymlp.ExecuteScalar();
                    lblmlfeep.Text = countpaymlp.ToString();

                    SqlCommand cmdallmlp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null and b.gender='M'", con);
                    int countallmlp = (int)cmdallmlp.ExecuteScalar();
                    lblmlcompp.Text = countallmlp.ToString();

                    //all male
                    SqlCommand cmdregmale = new SqlCommand(@"select count(cand_id) from kv_cand where gender='M'", con);
                    int countregml = (int)cmdregmale.ExecuteScalar();
                    lblmlreg.Text = countregml.ToString();

                    SqlCommand cmdmlph = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.gender='M'", con);
                    int countphotoml = (int)cmdmlph.ExecuteScalar();
                    lblmlph.Text = countphotoml.ToString();

                    SqlCommand cmdpayml = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.gender='M'", con);
                    int countpayml = (int)cmdpayml.ExecuteScalar();
                    lblmlfee.Text = countpayml.ToString();

                    SqlCommand cmdallml = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.gender='M'", con);
                    int countallml = (int)cmdallml.ExecuteScalar();
                    lblmlcomp.Text = countallml.ToString();




                    //general female
                    SqlCommand cmdreggnnfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G' and gender='F'", con);
                    int countreggnfl = (int)cmdreggnnfemale.ExecuteScalar();
                    lblflreggen.Text = countreggnfl.ToString();

                    SqlCommand cmdgenflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G' and b.gender='F'", con);
                    int countphotogenfl = (int)cmdgenflpg.ExecuteScalar();
                    lblflphgen.Text = countphotogenfl.ToString();

                    SqlCommand cmdpayflgen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G' and b.gender='F'", con);
                    int countpayflgen = (int)cmdpayflgen.ExecuteScalar();
                    lblflfeegen.Text = countpayflgen.ToString();

                    SqlCommand cmdallflgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G' and b.gender='F'", con);
                    int countallflgen = (int)cmdallflgen.ExecuteScalar();
                    lblflcompgen.Text = countallflgen.ToString();

                    //obc female
                    SqlCommand cmdregobcfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O' and gender='F'", con);
                    int countregobcfl = (int)cmdregobcfemale.ExecuteScalar();
                    lblflrego.Text = countregobcfl.ToString();

                    SqlCommand cmdobcflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O' and b.gender='F'", con);
                    int countphotoobcfl = (int)cmdobcflpg.ExecuteScalar();
                    lblflpho.Text = countphotoobcfl.ToString();

                    SqlCommand cmdpayflobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O' and b.gender='F'", con);
                    int countpayflobc = (int)cmdpayflobc.ExecuteScalar();
                    lblflfeeo.Text = countpayflobc.ToString();

                    SqlCommand cmdallflobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O' and b.gender='F'", con);
                    int countallflobc = (int)cmdallflobc.ExecuteScalar();
                    lblflcompo.Text = countallflobc.ToString();

                    //sc female
                    SqlCommand cmdregcfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C' and gender='F'", con);
                    int countregcfl = (int)cmdregcfemale.ExecuteScalar();
                    lblflregc.Text = countregcfl.ToString();

                    SqlCommand cmdcflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C' and b.gender='F'", con);
                    int countphotocfl = (int)cmdcflpg.ExecuteScalar();
                    lblflphc.Text = countphotocfl.ToString();

                    SqlCommand cmdpayflc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C' and b.gender='F'", con);
                    int countpayflc = (int)cmdpayflc.ExecuteScalar();
                    lblflfeec.Text = countpayflc.ToString();

                    SqlCommand cmdallflc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C' and b.gender='F'", con);
                    int countallflc = (int)cmdallflc.ExecuteScalar();
                    lblflcompc.Text = countallflc.ToString();

                    //st female
                    SqlCommand cmdregtfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T' and gender='F'", con);
                    int countregtfl = (int)cmdregtfemale.ExecuteScalar();
                    lblflregt.Text = countregtfl.ToString();

                    SqlCommand cmdtflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' and b.gender='F'", con);
                    int countphototfl = (int)cmdtflpg.ExecuteScalar();
                    lblflpht.Text = countphototfl.ToString();

                    SqlCommand cmdpayflt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' and b.gender='F'", con);
                    int countpayflt = (int)cmdpayflt.ExecuteScalar();
                    lblflfeet.Text = countpayflt.ToString();

                    SqlCommand cmdallflt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' and b.gender='F'", con);
                    int countallflt = (int)cmdallflt.ExecuteScalar();
                    lblflcompt.Text = countallflt.ToString();

                    //pwd female
                    SqlCommand cmdregpfemale = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null and gender='F'", con);
                    int countregpfl = (int)cmdregpfemale.ExecuteScalar();
                    lblflregp.Text = countregpfl.ToString();

                    SqlCommand cmdpflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null and b.gender='F'", con);
                    int countphotopfl = (int)cmdpflpg.ExecuteScalar();
                    lblflphp.Text = countphotopfl.ToString();

                    SqlCommand cmdpayflp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null and b.gender='F'", con);
                    int countpayflp = (int)cmdpayflp.ExecuteScalar();
                    lblflfeep.Text = countpayflp.ToString();

                    SqlCommand cmdallflp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null and b.gender='F'", con);
                    int countallflp = (int)cmdallflp.ExecuteScalar();
                    lblflcompp.Text = countallflp.ToString();

                    //all female
                    SqlCommand cmdregfemale = new SqlCommand(@"select count(cand_id) from kv_cand where gender='F'", con);
                    int countregfl = (int)cmdregfemale.ExecuteScalar();
                    lblflreg.Text = countregfl.ToString();

                    SqlCommand cmdflph = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.gender='F'", con);
                    int countphotofl = (int)cmdflph.ExecuteScalar();
                    lblflph.Text = countphotofl.ToString();

                    SqlCommand cmdpayfl = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.gender='F'", con);
                    int countpayfl = (int)cmdpayfl.ExecuteScalar();
                    lblflfee.Text = countpayfl.ToString();

                    SqlCommand cmdallfl = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.gender='F'", con);
                    int countallfl = (int)cmdallfl.ExecuteScalar();
                    lblflcomp.Text = countallfl.ToString();

                    //general
                    SqlCommand cmdreggn = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G'", con);
                    int countreggn = (int)cmdreggn.ExecuteScalar();
                    lblreggen.Text = countreggn.ToString();

                    SqlCommand cmdgenpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G'", con);
                    int countphotogen = (int)cmdgenpg.ExecuteScalar();
                    lblphgen.Text = countphotogen.ToString();

                    SqlCommand cmdpaygen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G'", con);
                    int countpaygen = (int)cmdpaygen.ExecuteScalar();
                    lblfeegen.Text = countpaygen.ToString();

                    SqlCommand cmdallgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G'", con);
                    int countallgen = (int)cmdallgen.ExecuteScalar();
                    lblcompgen.Text = countallgen.ToString();

                    //obc 
                    SqlCommand cmdregobc = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O'", con);
                    int countregobc = (int)cmdregobc.ExecuteScalar();
                    lblrego.Text = countregobc.ToString();

                    SqlCommand cmdobcpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O'", con);
                    int countphotoobc = (int)cmdobcpg.ExecuteScalar();
                    lblpho.Text = countphotoobc.ToString();

                    SqlCommand cmdpayobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O'", con);
                    int countpayobc = (int)cmdpayobc.ExecuteScalar();
                    lblfeeo.Text = countpayobc.ToString();

                    SqlCommand cmdallobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O'", con);
                    int countallobc = (int)cmdallobc.ExecuteScalar();
                    lblcompo.Text = countallobc.ToString();

                    //sc 
                    SqlCommand cmdregc = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C'", con);
                    int countregc = (int)cmdregc.ExecuteScalar();
                    lblregc.Text = countregc.ToString();

                    SqlCommand cmdcpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C'", con);
                    int countphotoc = (int)cmdcpg.ExecuteScalar();
                    lblphc.Text = countphotoc.ToString();

                    SqlCommand cmdpayc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C'", con);
                    int countpayc = (int)cmdpayc.ExecuteScalar();
                    lblfeec.Text = countpayc.ToString();

                    SqlCommand cmdallc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C'", con);
                    int countallc = (int)cmdallc.ExecuteScalar();
                    lblcompc.Text = countallc.ToString();

                    //st 
                    SqlCommand cmdregt = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T'", con);
                    int countregt = (int)cmdregt.ExecuteScalar();
                    lblregt.Text = countregt.ToString();

                    SqlCommand cmdtpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' ", con);
                    int countphotot = (int)cmdtpg.ExecuteScalar();
                    lblpht.Text = countphotot.ToString();

                    SqlCommand cmdpayt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' ", con);
                    int countpayt = (int)cmdpayt.ExecuteScalar();
                    lblfeet.Text = countpayt.ToString();

                    SqlCommand cmdallt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' ", con);
                    int countallt = (int)cmdallt.ExecuteScalar();
                    lblcompt.Text = countallt.ToString();

                    //pwd 
                    SqlCommand cmdregp = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null ", con);
                    int countregp = (int)cmdregp.ExecuteScalar();
                    lblregp.Text = countregp.ToString();

                    SqlCommand cmdppg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null ", con);
                    int countphotop = (int)cmdppg.ExecuteScalar();
                    lblphp.Text = countphotop.ToString();

                    SqlCommand cmdpayp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null ", con);
                    int countpayp = (int)cmdpayp.ExecuteScalar();
                    lblfeep.Text = countpayp.ToString();

                    SqlCommand cmdallp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null ", con);
                    int countallp = (int)cmdallp.ExecuteScalar();
                    lblcompp.Text = countallp.ToString();



                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    lblgrvcount.Text = ex.ToString();
                    return;
                }

            }

        }

        protected void btnSummary_Click(object sender, EventArgs e)
        {
            try
            {
                lblgrvcount.Text = "";
                lbldate.Text = DateTime.Now.ToShortDateString();
                String datenow = DateTime.Parse(lbldate.Text).ToString("dd/MM/yyyy");
                lbldate.Text = datenow + " " + DateTime.Now.ToShortTimeString();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                con.Open();
                SqlCommand cmdreg = new SqlCommand(@"select count(cand_id) from kv_cand", con);
                int countreg = (int)cmdreg.ExecuteScalar();
                lblregcand.Text = countreg.ToString();

                SqlCommand cmdphoto = new SqlCommand(@"select count(cand_id) from kv_photo", con);
                int countphoto = (int)cmdphoto.ExecuteScalar();
                lblphoto.Text = countphoto.ToString();

                SqlCommand cmdpay = new SqlCommand(@"select count(cand_id) from kv_feelot where [status]='SUCCESS'", con);
                int countpay = (int)cmdpay.ExecuteScalar();
                lblpay.Text = countpay.ToString();

                SqlCommand cmdall = new SqlCommand(@"select count(cand_id) from kv_status where step3='Y'", con);
                int countall = (int)cmdall.ExecuteScalar();
                lblall.Text = countall.ToString();

                //general male
                SqlCommand cmdreggnnmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G' and gender='M'", con);
                int countreggnml = (int)cmdreggnnmale.ExecuteScalar();
                lblmlreggen.Text = countreggnml.ToString();

                SqlCommand cmdgenmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G' and b.gender='M'", con);
                int countphotogenml = (int)cmdgenmlpg.ExecuteScalar();
                lblmlphgen.Text = countphotogenml.ToString();

                SqlCommand cmdpaymlgen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G' and b.gender='M'", con);
                int countpaymlgen = (int)cmdpaymlgen.ExecuteScalar();
                lblmlfeegen.Text = countpaymlgen.ToString();

                SqlCommand cmdallmlgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G' and b.gender='M'", con);
                int countallmlgen = (int)cmdallmlgen.ExecuteScalar();
                lblmlcompgen.Text = countallmlgen.ToString();

                //obc male
                SqlCommand cmdregobcmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O' and gender='M'", con);
                int countregobcml = (int)cmdregobcmale.ExecuteScalar();
                lblmlrego.Text = countregobcml.ToString();

                SqlCommand cmdobcmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O' and b.gender='M'", con);
                int countphotoobcml = (int)cmdobcmlpg.ExecuteScalar();
                lblmlpho.Text = countphotoobcml.ToString();

                SqlCommand cmdpaymlobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O' and b.gender='M'", con);
                int countpaymlobc = (int)cmdpaymlobc.ExecuteScalar();
                lblmlfeeo.Text = countpaymlobc.ToString();

                SqlCommand cmdallmlobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O' and b.gender='M'", con);
                int countallmlobc = (int)cmdallmlobc.ExecuteScalar();
                lblmlcompo.Text = countallmlobc.ToString();

                //sc male
                SqlCommand cmdregcmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C' and gender='M'", con);
                int countregcml = (int)cmdregcmale.ExecuteScalar();
                lblmlregc.Text = countregcml.ToString();

                SqlCommand cmdcmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C' and b.gender='M'", con);
                int countphotocml = (int)cmdcmlpg.ExecuteScalar();
                lblmlphc.Text = countphotocml.ToString();

                SqlCommand cmdpaymlc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C' and b.gender='M'", con);
                int countpaymlc = (int)cmdpaymlc.ExecuteScalar();
                lblmlfeec.Text = countpaymlc.ToString();

                SqlCommand cmdallmlc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C' and b.gender='M'", con);
                int countallmlc = (int)cmdallmlc.ExecuteScalar();
                lblmlcompc.Text = countallmlc.ToString();

                //st male
                SqlCommand cmdregtmale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T' and gender='M'", con);
                int countregtml = (int)cmdregtmale.ExecuteScalar();
                lblmlregt.Text = countregtml.ToString();

                SqlCommand cmdtmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' and b.gender='M'", con);
                int countphototml = (int)cmdtmlpg.ExecuteScalar();
                lblmlpht.Text = countphototml.ToString();

                SqlCommand cmdpaymlt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' and b.gender='M'", con);
                int countpaymlt = (int)cmdpaymlt.ExecuteScalar();
                lblmlfeet.Text = countpaymlt.ToString();

                SqlCommand cmdallmlt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' and b.gender='M'", con);
                int countallmlt = (int)cmdallmlt.ExecuteScalar();
                lblmlcompt.Text = countallmlt.ToString();

                //pwd male
                SqlCommand cmdregpmale = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null and gender='M'", con);
                int countregpml = (int)cmdregpmale.ExecuteScalar();
                lblmlregp.Text = countregpml.ToString();

                SqlCommand cmdpmlpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null and b.gender='M'", con);
                int countphotopml = (int)cmdpmlpg.ExecuteScalar();
                lblmlphp.Text = countphotopml.ToString();

                SqlCommand cmdpaymlp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null and b.gender='M'", con);
                int countpaymlp = (int)cmdpaymlp.ExecuteScalar();
                lblmlfeep.Text = countpaymlp.ToString();

                SqlCommand cmdallmlp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null and b.gender='M'", con);
                int countallmlp = (int)cmdallmlp.ExecuteScalar();
                lblmlcompp.Text = countallmlp.ToString();

                //all male
                SqlCommand cmdregmale = new SqlCommand(@"select count(cand_id) from kv_cand where gender='M'", con);
                int countregml = (int)cmdregmale.ExecuteScalar();
                lblmlreg.Text = countregml.ToString();

                SqlCommand cmdmlph = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.gender='M'", con);
                int countphotoml = (int)cmdmlph.ExecuteScalar();
                lblmlph.Text = countphotoml.ToString();

                SqlCommand cmdpayml = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.gender='M'", con);
                int countpayml = (int)cmdpayml.ExecuteScalar();
                lblmlfee.Text = countpayml.ToString();

                SqlCommand cmdallml = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.gender='M'", con);
                int countallml = (int)cmdallml.ExecuteScalar();
                lblmlcomp.Text = countallml.ToString();




                //general female
                SqlCommand cmdreggnnfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G' and gender='F'", con);
                int countreggnfl = (int)cmdreggnnfemale.ExecuteScalar();
                lblflreggen.Text = countreggnfl.ToString();

                SqlCommand cmdgenflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G' and b.gender='F'", con);
                int countphotogenfl = (int)cmdgenflpg.ExecuteScalar();
                lblflphgen.Text = countphotogenfl.ToString();

                SqlCommand cmdpayflgen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G' and b.gender='F'", con);
                int countpayflgen = (int)cmdpayflgen.ExecuteScalar();
                lblflfeegen.Text = countpayflgen.ToString();

                SqlCommand cmdallflgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G' and b.gender='F'", con);
                int countallflgen = (int)cmdallflgen.ExecuteScalar();
                lblflcompgen.Text = countallflgen.ToString();

                //obc female
                SqlCommand cmdregobcfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O' and gender='F'", con);
                int countregobcfl = (int)cmdregobcfemale.ExecuteScalar();
                lblflrego.Text = countregobcfl.ToString();

                SqlCommand cmdobcflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O' and b.gender='F'", con);
                int countphotoobcfl = (int)cmdobcflpg.ExecuteScalar();
                lblflpho.Text = countphotoobcfl.ToString();

                SqlCommand cmdpayflobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O' and b.gender='F'", con);
                int countpayflobc = (int)cmdpayflobc.ExecuteScalar();
                lblflfeeo.Text = countpayflobc.ToString();

                SqlCommand cmdallflobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O' and b.gender='F'", con);
                int countallflobc = (int)cmdallflobc.ExecuteScalar();
                lblflcompo.Text = countallflobc.ToString();

                //sc female
                SqlCommand cmdregcfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C' and gender='F'", con);
                int countregcfl = (int)cmdregcfemale.ExecuteScalar();
                lblflregc.Text = countregcfl.ToString();

                SqlCommand cmdcflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C' and b.gender='F'", con);
                int countphotocfl = (int)cmdcflpg.ExecuteScalar();
                lblflphc.Text = countphotocfl.ToString();

                SqlCommand cmdpayflc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C' and b.gender='F'", con);
                int countpayflc = (int)cmdpayflc.ExecuteScalar();
                lblflfeec.Text = countpayflc.ToString();

                SqlCommand cmdallflc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C' and b.gender='F'", con);
                int countallflc = (int)cmdallflc.ExecuteScalar();
                lblflcompc.Text = countallflc.ToString();

                //st female
                SqlCommand cmdregtfemale = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T' and gender='F'", con);
                int countregtfl = (int)cmdregtfemale.ExecuteScalar();
                lblflregt.Text = countregtfl.ToString();

                SqlCommand cmdtflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' and b.gender='F'", con);
                int countphototfl = (int)cmdtflpg.ExecuteScalar();
                lblflpht.Text = countphototfl.ToString();

                SqlCommand cmdpayflt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' and b.gender='F'", con);
                int countpayflt = (int)cmdpayflt.ExecuteScalar();
                lblflfeet.Text = countpayflt.ToString();

                SqlCommand cmdallflt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' and b.gender='F'", con);
                int countallflt = (int)cmdallflt.ExecuteScalar();
                lblflcompt.Text = countallflt.ToString();

                //pwd female
                SqlCommand cmdregpfemale = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null and gender='F'", con);
                int countregpfl = (int)cmdregpfemale.ExecuteScalar();
                lblflregp.Text = countregpfl.ToString();

                SqlCommand cmdpflpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null and b.gender='F'", con);
                int countphotopfl = (int)cmdpflpg.ExecuteScalar();
                lblflphp.Text = countphotopfl.ToString();

                SqlCommand cmdpayflp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null and b.gender='F'", con);
                int countpayflp = (int)cmdpayflp.ExecuteScalar();
                lblflfeep.Text = countpayflp.ToString();

                SqlCommand cmdallflp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null and b.gender='F'", con);
                int countallflp = (int)cmdallflp.ExecuteScalar();
                lblflcompp.Text = countallflp.ToString();

                //all female
                SqlCommand cmdregfemale = new SqlCommand(@"select count(cand_id) from kv_cand where gender='F'", con);
                int countregfl = (int)cmdregfemale.ExecuteScalar();
                lblflreg.Text = countregfl.ToString();

                SqlCommand cmdflph = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.gender='F'", con);
                int countphotofl = (int)cmdflph.ExecuteScalar();
                lblflph.Text = countphotofl.ToString();

                SqlCommand cmdpayfl = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.gender='F'", con);
                int countpayfl = (int)cmdpayfl.ExecuteScalar();
                lblflfee.Text = countpayfl.ToString();

                SqlCommand cmdallfl = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.gender='F'", con);
                int countallfl = (int)cmdallfl.ExecuteScalar();
                lblflcomp.Text = countallfl.ToString();

                //general
                SqlCommand cmdreggn = new SqlCommand(@"select count(cand_id) from kv_cand where cat='G'", con);
                int countreggn = (int)cmdreggn.ExecuteScalar();
                lblreggen.Text = countreggn.ToString();

                SqlCommand cmdgenpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='G'", con);
                int countphotogen = (int)cmdgenpg.ExecuteScalar();
                lblphgen.Text = countphotogen.ToString();

                SqlCommand cmdpaygen = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='G'", con);
                int countpaygen = (int)cmdpaygen.ExecuteScalar();
                lblfeegen.Text = countpaygen.ToString();

                SqlCommand cmdallgen = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='G'", con);
                int countallgen = (int)cmdallgen.ExecuteScalar();
                lblcompgen.Text = countallgen.ToString();

                //obc 
                SqlCommand cmdregobc = new SqlCommand(@"select count(cand_id) from kv_cand where cat='O'", con);
                int countregobc = (int)cmdregobc.ExecuteScalar();
                lblrego.Text = countregobc.ToString();

                SqlCommand cmdobcpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='O'", con);
                int countphotoobc = (int)cmdobcpg.ExecuteScalar();
                lblpho.Text = countphotoobc.ToString();

                SqlCommand cmdpayobc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='O'", con);
                int countpayobc = (int)cmdpayobc.ExecuteScalar();
                lblfeeo.Text = countpayobc.ToString();

                SqlCommand cmdallobc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='O'", con);
                int countallobc = (int)cmdallobc.ExecuteScalar();
                lblcompo.Text = countallobc.ToString();

                //sc 
                SqlCommand cmdregc = new SqlCommand(@"select count(cand_id) from kv_cand where cat='C'", con);
                int countregc = (int)cmdregc.ExecuteScalar();
                lblregc.Text = countregc.ToString();

                SqlCommand cmdcpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='C'", con);
                int countphotoc = (int)cmdcpg.ExecuteScalar();
                lblphc.Text = countphotoc.ToString();

                SqlCommand cmdpayc = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='C'", con);
                int countpayc = (int)cmdpayc.ExecuteScalar();
                lblfeec.Text = countpayc.ToString();

                SqlCommand cmdallc = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='C'", con);
                int countallc = (int)cmdallc.ExecuteScalar();
                lblcompc.Text = countallc.ToString();

                //st 
                SqlCommand cmdregt = new SqlCommand(@"select count(cand_id) from kv_cand where cat='T'", con);
                int countregt = (int)cmdregt.ExecuteScalar();
                lblregt.Text = countregt.ToString();

                SqlCommand cmdtpg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.cat='T' ", con);
                int countphotot = (int)cmdtpg.ExecuteScalar();
                lblpht.Text = countphotot.ToString();

                SqlCommand cmdpayt = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.cat='T' ", con);
                int countpayt = (int)cmdpayt.ExecuteScalar();
                lblfeet.Text = countpayt.ToString();

                SqlCommand cmdallt = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.cat='T' ", con);
                int countallt = (int)cmdallt.ExecuteScalar();
                lblcompt.Text = countallt.ToString();

                //pwd 
                SqlCommand cmdregp = new SqlCommand(@"select count(cand_id) from kv_cand where pwd is not null ", con);
                int countregp = (int)cmdregp.ExecuteScalar();
                lblregp.Text = countregp.ToString();

                SqlCommand cmdppg = new SqlCommand(@"select count(a.cand_id) from kv_photo a inner join kv_cand b on a.cand_id=b.cand_id where b.pwd is not null ", con);
                int countphotop = (int)cmdppg.ExecuteScalar();
                lblphp.Text = countphotop.ToString();

                SqlCommand cmdpayp = new SqlCommand(@"select count(a.cand_id) from kv_feelot a inner join kv_cand b on a.cand_id=b.cand_id where a.[status]='SUCCESS' and b.pwd is not null ", con);
                int countpayp = (int)cmdpayp.ExecuteScalar();
                lblfeep.Text = countpayp.ToString();

                SqlCommand cmdallp = new SqlCommand(@"select count(a.cand_id) from kv_status a inner join kv_cand b on a.cand_id=b.cand_id where a.step3='Y' and b.pwd is not null ", con);
                int countallp = (int)cmdallp.ExecuteScalar();
                lblcompp.Text = countallp.ToString();



                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                lblgrvcount.Text = ex.ToString();
                return;
            }

        }
    }
}
    