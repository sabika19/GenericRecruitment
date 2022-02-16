using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Web.Security;
using System.Web.SessionState;
using System.Runtime.InteropServices;
namespace kvs
{
    public partial class appedit : System.Web.UI.Page
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        static extern int FindMimeFromData(IntPtr pBC,
          [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
          [MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.I1, SizeParamIndex=3)]
    byte[] pBuffer,
          int cbSize,
          [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
          int dwMimeFlags,
          out IntPtr ppwzMimeOut,
          int dwReserved);

        protected int checkmime(string type)
        {
            int retVal = 0;
            if (type == "photo")
            {
                HttpPostedFile file_photo = upphoto.PostedFile;
                byte[] document_photo = new byte[file_photo.ContentLength];
                file_photo.InputStream.Read(document_photo, 0, file_photo.ContentLength);
                System.IntPtr mimetype_photo;
                FindMimeFromData(IntPtr.Zero, null, document_photo, 256, null, 0, out mimetype_photo, 0);
                // System.IntPtr mimeTypePtr_photo = new IntPtr(mimetype_photo);
                string mime_photo = Marshal.PtrToStringUni(mimetype_photo);
                Marshal.FreeCoTaskMem(mimetype_photo);
                if (mime_photo == "image/pjpeg")
                {
                    retVal = 0;

                }
                else
                {
                    retVal = 1;
                }
            }
            else if (type == "sign")
            {
                HttpPostedFile file_sign = upsign.PostedFile;
                byte[] document_sign = new byte[file_sign.ContentLength];
                file_sign.InputStream.Read(document_sign, 0, file_sign.ContentLength);
                System.IntPtr mimetype_sign;
                FindMimeFromData(IntPtr.Zero, null, document_sign, 256, null, 0, out mimetype_sign, 0);
                // System.IntPtr mimeTypePtr_sign = new IntPtr(mimetype_sign);
                string mime_sign = Marshal.PtrToStringUni(mimetype_sign);
                Marshal.FreeCoTaskMem(mimetype_sign);
                if (mime_sign == "image/pjpeg")
                {
                    retVal = 0;

                }
                else
                {
                    retVal = 1;
                }
            }
           
            return retVal;
        }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["pid"] == null && Session["candid"] == null)
            {
                Response.Redirect("editlogin.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
                SessionIDManager Manager = new SessionIDManager();
                string NewID = Manager.CreateSessionID(Context);
                string OldID = Context.Session.SessionID;
                bool redirected = false;
                bool IsAdded = false;
                Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);
                if (DateTime.Now < Convert.ToDateTime(ConfigurationManager.AppSettings["starteditdate"]))
                {
                    lblMsg.Text = "EDITING NOT OPEN YET.";
                    all_form.Visible = false;
                    return;
                }
                else if (DateTime.Now > Convert.ToDateTime(ConfigurationManager.AppSettings["lasteditdate"]))
                {
                    lblMsg.Text = "LAST DATE FOR EDITING IS OVER.";
                    all_form.Visible = false;
                    return;
                }

                else
                {
                  
                    all_form.Visible = true;
                    CompareEndTodayValidator.ValueToCompare = DateTime.Now.Year.ToString();
                    CompareEndTodayValidator0.ValueToCompare = DateTime.Now.Year.ToString();
                    CompareEndTodayValidator1.ValueToCompare = DateTime.Now.Year.ToString();
                    CompareEndTodayValidator2.ValueToCompare = DateTime.Now.Year.ToString();
                    CompareEndTodayValidator3.ValueToCompare = DateTime.Now.Year.ToString();
                    CompareEndTodayValidator4.ValueToCompare = DateTime.Now.Year.ToString();
                    CompareEndTodayValidator5.ValueToCompare = DateTime.Now.Year.ToString();
                    CompareEndTodayValidator6.ValueToCompare = DateTime.Now.Year.ToString();
                    //  DateTime yearctet = Convert.ToDateTime(ConfigurationManager.AppSettings["ctetyear"]);
                    //CompareEndTodayValidator7.ValueToCompare = yearctet.Year.ToString();
                    CompareEndTodayValidator7.ValueToCompare = "2012";
                    compctet2.ValueToCompare = DateTime.Now.Year.ToString();
                    // compctet3.ValueToCompare = yearctet.Year.ToString();
                    compctet3.ValueToCompare = "2012";
                    dvnewname.Visible = false;
                    btnLog.Visible = false;
                    dvsuccess.Visible = false;
                    Session["kvemp"] = "";
                    dvpwdcat.Visible = false;
                    reqpwdcat.Enabled = false;
                    reqyrreg.Enabled = false;
                    //dvcand.Visible = true;
                    //dvpostexam.Visible = false;
                    //dvedu.Visible = false;
                    divexserv.Visible = false;
                    reqservlen.Enabled = false;
                    divcgemp.Visible = false;
                    //  dvpgt.Visible = false;
                    reqpgt.Enabled = false;
                    reqtgt.Enabled = false;
                    getstates();
                    getexamcities();
                    getgrad();
                    getpostgrad();
                    getpgtsub();
                    gettgtsub();
                 


                    //disbale all required validators
                    reqgrad.Enabled = false;
                    reqgradyr.Enabled = false;
                    reqgraduni.Enabled = false;
                    reqgradperc.Enabled = false;


                    reqyrpg.Enabled = false;
                    requnipg.Enabled = false;
                    reqpercpg.Enabled = false;
                    reqpg.Enabled = false;


                    reqyrbed.Enabled = false;
                    requnibed.Enabled = false;
                    reqpercbed.Enabled = false;

                    reqyrded.Enabled = false;
                    requnided.Enabled = false;
                    reqpercded.Enabled = false;


                    reqyrctet.Enabled = false;
                    reqpercctet.Enabled = false;

                    reqpgt.Enabled = false;
                    reqtgt.Enabled = false;

                    dvtgt.Visible = false;
                    dvpgt.Visible = false;

                    spnesspgt.Visible = false;
                    spnesstgt.Visible = false;

                    dvlib.Visible = false;
                    dvprt.Visible = false;
                    dvprtm.Visible = false;


                    extractdata();



                }
               

            }
            else
            {
                string ControlID = string.Empty;
                if (!String.IsNullOrEmpty(Request.Form["__EVENTTARGET"]))
                {
                    ControlID = Request.Form["__EVENTTARGET"];
                    Control postbackControl = Page.FindControl(ControlID);
                    postbackControl.Focus();
                }
            }
           
           
        }
        protected void extractdata()
        {
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

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                con.Open();
                System.Data.SqlClient.SqlDataReader Dr;
                string Stmt;

                Stmt = " select au_user from kv_auth where au_query = '" + pid + Context.Session.SessionID + "salted' and au_status = 'pass'";
                // Stmt = "select au_user from kv_auth where au_query='" + pid + Context.Session.SessionID + "' and au_status='pass'";
                SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(Stmt, con);
                Dr = Cmd.ExecuteReader();
                if (Dr.Read())
                {

                    string cand_id = Dr["au_user"].ToString();
                    hdcandid.Value = cand_id;
                    Dr.Close();
                    SqlCommand cmd2 = new SqlCommand(@"select * from kv_cand where cand_id=@cand_id", con);
                    cmd2.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = cand_id;
                    SqlDataReader sdr = cmd2.ExecuteReader();
                    if (@sdr.Read())
                    {

                        ddlrel.SelectedValue = sdr["religion"].ToString();
                        txtcfname.Text = sdr["cname_f"].ToString();
                        txtclname.Text = sdr["cname_l"].ToString();
                        txtmname.Text = sdr["mname"].ToString();
                        txtfname.Text = sdr["fname"].ToString();
                        ddlmar.SelectedValue = sdr["marital"].ToString();
                        string essqual = sdr["essqual"].ToString();
                      
                        ddlgen.SelectedValue = sdr["gender"].ToString();
                        txtnewname.Text = sdr["newname"].ToString();
                        if (txtnewname.Text == "")
                            dvnewname.Visible = false;
                        else
                            dvnewname.Visible = true;

                        ddlid.SelectedValue = sdr["idtype"].ToString();

                        txtgovid.Text = sdr["idnum"].ToString();

                        txtgovid.Text = AESEncrytDecry.Decrypt(txtgovid.Text);

                        ddlcat.SelectedValue = sdr["cat"].ToString();


                        if (sdr["pwd"].ToString() != "")
                        {
                            ddlpwdcat.SelectedValue = sdr["pwd"].ToString();
                            rdbpwd.SelectedValue = "Y";
                            dvpwdcat.Visible = true;
                        }


                        rdbscribe.SelectedValue = sdr["scribe"].ToString();

                        rdbemp.SelectedValue = sdr["kvs"].ToString();
                        rdbcgemp.SelectedValue = sdr["cg"].ToString();
                        rdbexserv.SelectedValue = sdr["exserv"].ToString();
                        if (sdr["exserv"].ToString() != "")
                        {
                            txtservlen.Text = sdr["ex_len"].ToString();
                        }
                        hdfee.Value = sdr["fee"].ToString();
                        if (rdbcgemp.SelectedValue == "Y")
                        {
                            txtyrreg.Text = sdr["cg_len"].ToString();
                        }
                        rdbjk.SelectedValue = sdr["jk"].ToString();
                        txtiden.Text = sdr["iden_mark"].ToString();

                        string dob1 = sdr["dob"].ToString();
                        string dateshort = dob1;
                        String dob = DateTime.Parse(dateshort).ToString("dd/MM/yyyy");
                        datepicker.Text = dob.ToString();
                        datepicker.Text=datepicker.Text.Replace("-", "/");
                       
                        ddlstate.SelectedValue = sdr["state"].ToString();
                        getdistt();
                        ddlcity.SelectedValue = sdr["city"].ToString();
                        txtadd1.Text = sdr["add1"].ToString();
                        txtadd2.Text = sdr["add2"].ToString();
                        txtpin.Text = sdr["pin"].ToString();
                        txtemail.Text = sdr["email"].ToString();
                        txtmob.Text = sdr["mobile1"].ToString();
                        if (sdr["mobile2"].ToString() != "")
                            txtmob2.Text = sdr["mobile2"].ToString();
                        ddlmed.SelectedValue = sdr["med"].ToString();
                        txtyr10.Text = sdr["yr_10"].ToString();
                        txtuni10.Text = sdr["uni_10"].ToString();
                        txtperc10.Text = sdr["perc_10"].ToString();

                        txtyr12.Text = sdr["yr_12"].ToString();
                        txtuni12.Text = sdr["uni_12"].ToString();
                        txtperc12.Text = sdr["perc_12"].ToString();

                        if (sdr["yr_dip"].ToString() != "")
                        {
                            txtyrdip.Text = sdr["yr_dip"].ToString();
                            txtunidip.Text = sdr["uni_dip"].ToString();
                            txtpercdip.Text = sdr["perc_dip"].ToString();
                        }

                        if (sdr["yr_ded"].ToString() != "")
                        {
                            txtyrded.Text = sdr["yr_ded"].ToString();
                            txtunided.Text = sdr["uni_ded"].ToString();
                            txtpercded.Text = sdr["perc_ded"].ToString();
                        }

                        if (sdr["yr_bed"].ToString() != "")
                        {
                            txtyrbed.Text = sdr["yr_bed"].ToString();
                            txtunibed.Text = sdr["uni_bed"].ToString();
                            txtpercbed.Text = sdr["perc_bed"].ToString();
                        }


                        if (sdr["ctetregno"].ToString() != "")
                            txtctetreg.Text = sdr["ctetregno"].ToString();
                        if (sdr["ctet2regno"].ToString() != "")
                            txtctetreg2.Text = sdr["ctet2regno"].ToString();

                        if (sdr["yr_ctet"].ToString() != "")
                        {
                            txtyrctet.Text = sdr["yr_ctet"].ToString();
                            txtpercctet.Text = sdr["perc_ctet"].ToString();
                        }
                        if (sdr["yr_ctet2"].ToString() != "")
                        {
                            txtyrctet2.Text = sdr["yr_ctet2"].ToString();
                            txtpercctet2.Text = sdr["perc_ctet2"].ToString();
                        }

                        if (sdr["yr_grad"].ToString() != "")
                        {
                            ddlgrad.SelectedValue = sdr["sub_grad"].ToString();
                            txtyrgrad.Text = sdr["yr_grad"].ToString();
                            txtunigrad.Text = sdr["uni_grad"].ToString();
                            txtpercgrad.Text = sdr["perc_grad"].ToString();
                        }

                        if (sdr["yr_pg"].ToString() != "")
                        {
                            ddlpostgrad.SelectedValue = sdr["sub_pg"].ToString();
                            txtyrpg.Text = sdr["yr_pg"].ToString();
                            txtunipg.Text = sdr["uni_pg"].ToString();
                            txtpercpg.Text = sdr["perc_pg"].ToString();
                        }


                        if (sdr["post_pgt"].ToString() != "")
                        {
                            pgt.Checked = true;
                            ddlpgt.SelectedValue = sdr["post_pgt"].ToString();
                            dvpgt.Visible = true;
                            rdblesspgt.Visible = true;
                        }
                        if (sdr["post_tgt"].ToString() != "")
                        {
                            tgt.Checked = true;
                            ddltgt.SelectedValue = sdr["post_tgt"].ToString();
                            dvtgt.Visible = true;
                            rdblesstgt.Visible = true;
                        }
                        if (sdr["post_prt"].ToString() != "")
                        {
                            prt.Checked = true;
                            rdblprt.Visible = true;
                        }
                        if (sdr["post_prtm"].ToString() != "")
                        {
                            prtmusic.Checked = true;
                            rdblprtm.Visible = true;
                        }
                        if (sdr["post_lib"].ToString() != "")
                        {
                            lib.Checked = true;
                            rdbllib.Visible = true;
                        }
                        if (sdr["post_princi"].ToString() != "")
                        {
                            princi.Checked = true;
                        }
                        if (sdr["post_vcp"].ToString() != "")
                        {
                            vcp.Checked = true;
                        }



                        ddless.SelectedValue = sdr["ess_qual"].ToString();
                        ddldesire.SelectedValue = sdr["des_qual"].ToString();


                        ddlcity1.SelectedValue = sdr["exam_city1"].ToString();
                        ddlcity2.SelectedValue = sdr["exam_city2"].ToString();
                        ddlcity3.SelectedValue = sdr["exam_city3"].ToString();
                        ddlcity4.SelectedValue = sdr["exam_city4"].ToString();




                        ddlzone1.SelectedValue = sdr["zone1"].ToString();
                        ddlzone2.SelectedValue = sdr["zone2"].ToString();
                        ddlzone3.SelectedValue = sdr["zone3"].ToString();
                        ddlzone4.SelectedValue = sdr["zone4"].ToString();
                        ddlzone5.SelectedValue = sdr["zone5"].ToString();
                        ddlzone6.SelectedValue = sdr["zone6"].ToString();



                        sdr.Close();


                        SqlCommand cmd3 = new SqlCommand(@"select * from kv_photo where cand_id=@cand_id", con);
                        cmd3.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = cand_id;
                        sdr = cmd3.ExecuteReader();
                        if (@sdr.Read())
                        {

                            byte[] bytes = (byte[])sdr["photo"];
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            candimg.ImageUrl = "data:image/png;base64," + base64String;

                            byte[] bytes2 = (byte[])sdr["sign"];
                            string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                            candsign.ImageUrl = "data:image/png;base64," + base64String2;

                        }
                        sdr.Close();

                    }
                    else
                    {
                        Response.Redirect("editlogin.aspx", false);
                        return;
                    }
                }
                con.Close();
                con.Dispose();

            }
        }
        protected void getstates()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            //Stmt = "select * from states order by st_name";
            SqlCommand cmdstates = new SqlCommand(@"select * from states order by st_name", con);
            SqlDataReader Drstates = cmdstates.ExecuteReader();
            ddlstate.DataSource = Drstates;
            ddlstate.DataTextField = "st_name";
            ddlstate.DataValueField = "st_code";
            ddlstate.DataBind();
            Drstates.Close();

            con.Close();
            con.Dispose();
        }

        protected void getexamcities()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            //Stmt = "select * from states order by st_name";
            SqlCommand cmdcities = new SqlCommand(@"select * from kv_centres order by cen_no", con);
            SqlDataAdapter Dacities = new SqlDataAdapter(cmdcities);
            DataTable dtcities = new DataTable();
            Dacities.Fill(dtcities);

            ddlcity1.DataSource = dtcities;
            ddlcity1.DataTextField = "cen_name";
            ddlcity1.DataValueField = "cen_name";
            ddlcity1.DataBind();

            ddlcity2.DataSource = dtcities;
            ddlcity2.DataTextField = "cen_name";
            ddlcity2.DataValueField = "cen_name";
            ddlcity2.DataBind();

            ddlcity3.DataSource = dtcities;
            ddlcity3.DataTextField = "cen_name";
            ddlcity3.DataValueField = "cen_name";
            ddlcity3.DataBind();

            ddlcity4.DataSource = dtcities;
            ddlcity4.DataTextField = "cen_name";
            ddlcity4.DataValueField = "cen_name";
            ddlcity4.DataBind();

            
            con.Close();
            con.Dispose();
        }
        protected void btnagree_Click(object sender, EventArgs e)
        {

        }

        protected int getagerelax()
        {
            int agerelax = 0;
            if (rdbemp.SelectedValue == "Y")
            {
                agerelax = 100;
            }
            else
            {
                agerelax = 0;
                if (ddlgen.SelectedValue == "M" || ddlgen.SelectedValue == "O")
                {

                    if (ddlcat.SelectedValue == "G")
                    {

                        int agerelaxpwd = 0;
                        int agerelaxexsercv = 0;
                        int agerelaxcgemp = 0;
                        int agerelaxjk = 0;

                        //  agerelax = 0;
                        if (rdbpwd.SelectedValue == "Y")
                        {

                            agerelaxpwd = 10;

                        }
                        //else if (rdbpwd.SelectedValue == "N")
                        //{
                        if (txtyrreg.Text != String.Empty)
                        {
                            if (float.Parse(txtyrreg.Text) >= 3.0)
                            {
                                agerelaxcgemp = 5;
                            }
                        }
                        //else
                        //{
                        if (rdbjk.SelectedValue == "Y")
                        {
                            agerelaxjk = 5;
                        }
                        //else
                        //{
                        if (rdbexserv.SelectedValue == "Y")
                        {
                            agerelaxexsercv = int.Parse(txtservlen.Text) + 3;
                        }
                        //else
                        //{
                        //    agerelax = 0;
                        //}

                        int maxrelax1 = Math.Max(agerelaxpwd, agerelaxexsercv);
                        int maxrelax2 = Math.Max(maxrelax1, agerelaxcgemp);
                        agerelax = Math.Max(maxrelax2, agerelaxjk);

                    }
                    //    }

                    //}

                    //}
                    else if (ddlcat.SelectedValue == "O")
                    {
                        agerelax = 3;
                        int agerelaxpwd = 0;
                        int agerelaxexsercv = 0;
                        int agerelaxcgemp = 0;
                        int agerelaxjk = 0;
                        if (rdbpwd.SelectedValue == "Y")
                        {

                            agerelaxpwd = 13;

                        }
                        if (txtyrreg.Text != String.Empty)
                        {
                            if (float.Parse(txtyrreg.Text) >= 3.0)
                            {
                                agerelaxcgemp = 5;
                            }
                        }
                        if (rdbexserv.SelectedValue == "Y")
                        {

                            agerelaxexsercv = int.Parse(txtservlen.Text) + 6;

                        }
                        //else
                        //{
                        if (rdbjk.SelectedValue == "Y")
                        {
                            agerelaxjk = 5;
                        }
                        int maxrelax1 = Math.Max(agerelaxpwd, agerelaxexsercv);
                        int maxrelax2 = Math.Max(maxrelax1, agerelaxcgemp);
                        int maxrelax3 = Math.Max(maxrelax2, agerelaxjk);
                        int maxrelax4 = Math.Max(maxrelax3, agerelax);
                        agerelax = maxrelax4;
                    }
                    //        }

                    //    }
                    //}
                    else if (ddlcat.SelectedValue == "C" || ddlcat.SelectedValue == "T")
                    {
                        agerelax = 5;
                        int agerelaxpwd = 0;
                        int agerelaxexsercv = 0;
                        int agerelaxcgemp = 0;
                        int agerelaxjk = 0;
                        if (rdbpwd.SelectedValue == "Y")
                        {

                            agerelaxpwd = 15;

                        }
                        if (txtyrreg.Text != String.Empty)
                        {
                            if (float.Parse(txtyrreg.Text) >= 3.0)
                            {
                                agerelaxcgemp = 5;
                            }
                        }
                        if (rdbexserv.SelectedValue == "Y")
                        {

                            agerelaxexsercv = int.Parse(txtservlen.Text) + 8;

                        }
                        //else
                        //{
                        if (rdbjk.SelectedValue == "Y")
                        {
                            agerelaxjk = 5;
                        }
                        int maxrelax1 = Math.Max(agerelaxpwd, agerelaxexsercv);
                        int maxrelax2 = Math.Max(maxrelax1, agerelaxcgemp);
                        int maxrelax3 = Math.Max(maxrelax2, agerelaxjk);
                        int maxrelax4 = Math.Max(maxrelax3, agerelax);
                        agerelax = maxrelax4;
                    }
                    //        }

                    //    }
                    //}
                }
                else if (ddlgen.SelectedValue == "F")
                {
                    if ((princi.Checked == true && vcp.Checked == false && pgt.Checked == false && tgt.Checked == false && prt.Checked == false && prtmusic.Checked == false && lib.Checked == false) || (princi.Checked == false && vcp.Checked == true && pgt.Checked == false && tgt.Checked == false && prt.Checked == false && prtmusic.Checked == false && lib.Checked == false) || (princi.Checked == true && vcp.Checked == true && pgt.Checked == false && tgt.Checked == false && prt.Checked == false && prtmusic.Checked == false && lib.Checked == false))
                    {
                        agerelax = 0;
                       
                        if (ddlcat.SelectedValue == "G")
                        {

                            int agerelaxpwd = 0;
                            int agerelaxexsercv = 0;
                            int agerelaxcgemp = 0;
                            int agerelaxjk = 0;

                         
                            if (rdbpwd.SelectedValue == "Y")
                            {

                                agerelaxpwd = 10;

                            }
                          
                            if (txtyrreg.Text != String.Empty)
                            {
                                if (float.Parse(txtyrreg.Text) >= 3.0)
                                {
                                    agerelaxcgemp = 5;
                                }
                            }
                          
                            if (rdbjk.SelectedValue == "Y")
                            {
                                agerelaxjk = 5;
                            }
                          
                            if (rdbexserv.SelectedValue == "Y")
                            {
                                agerelaxexsercv = int.Parse(txtservlen.Text) + 3;
                            }
                         

                            int maxrelax1 = Math.Max(agerelaxpwd, agerelaxexsercv);
                            int maxrelax2 = Math.Max(maxrelax1, agerelaxcgemp);
                            agerelax = Math.Max(maxrelax2, agerelaxjk);

                        }
                       
                        else if (ddlcat.SelectedValue == "O")
                        {
                            agerelax = 3;
                            int agerelaxpwd = 0;
                            int agerelaxexsercv = 0;
                            int agerelaxcgemp = 0;
                            int agerelaxjk = 0;
                            if (rdbpwd.SelectedValue == "Y")
                            {

                                agerelaxpwd = 13;

                            }
                            if (txtyrreg.Text != String.Empty)
                            {
                                if (float.Parse(txtyrreg.Text) >= 3.0)
                                {
                                    agerelaxcgemp = 5;
                                }
                            }
                            if (rdbexserv.SelectedValue == "Y")
                            {

                                agerelaxexsercv = int.Parse(txtservlen.Text) + 6;

                            }
                           
                            if (rdbjk.SelectedValue == "Y")
                            {
                                agerelaxjk = 5;
                            }
                            int maxrelax1 = Math.Max(agerelaxpwd, agerelaxexsercv);
                            int maxrelax2 = Math.Max(maxrelax1, agerelaxcgemp);
                            int maxrelax3 = Math.Max(maxrelax2, agerelaxjk);
                            int maxrelax4 = Math.Max(maxrelax3, agerelax);
                            agerelax = maxrelax4;
                        }
                      
                        else if (ddlcat.SelectedValue == "C" || ddlcat.SelectedValue == "T")
                        {
                            agerelax = 5;
                            int agerelaxpwd = 0;
                            int agerelaxexsercv = 0;
                            int agerelaxcgemp = 0;
                            int agerelaxjk = 0;
                            if (rdbpwd.SelectedValue == "Y")
                            {

                                agerelaxpwd = 15;

                            }
                            if (txtyrreg.Text != String.Empty)
                            {
                                if (float.Parse(txtyrreg.Text) >= 3.0)
                                {
                                    agerelaxcgemp = 5;
                                }
                            }
                            if (rdbexserv.SelectedValue == "Y")
                            {

                                agerelaxexsercv = int.Parse(txtservlen.Text) + 8;

                            }
                        
                            if (rdbjk.SelectedValue == "Y")
                            {
                                agerelaxjk = 5;
                            }
                            int maxrelax1 = Math.Max(agerelaxpwd, agerelaxexsercv);
                            int maxrelax2 = Math.Max(maxrelax1, agerelaxcgemp);
                            int maxrelax3 = Math.Max(maxrelax2, agerelaxjk);
                            int maxrelax4 = Math.Max(maxrelax3, agerelax);
                            agerelax = maxrelax4;
                        }

                       
                    }
                    else
                    {
                        agerelax = 10;
                        int agerelaxpwd = 0;
                        int agerelaxexsercv = 0;
                        if (rdbpwd.SelectedValue == "Y")
                        {
                            agerelaxpwd = 10;
                            if (ddlcat.SelectedValue == "O")
                            {
                                agerelaxpwd = 13;
                            }
                            else if (ddlcat.SelectedValue == "C" || ddlcat.SelectedValue == "T")
                            {
                                agerelaxpwd = 15;
                            }
                        }
                        if (rdbexserv.SelectedValue == "Y")
                        {
                            agerelaxexsercv = int.Parse(txtservlen.Text) + 3;
                            if (ddlcat.SelectedValue == "O")
                            {
                                agerelaxexsercv = int.Parse(txtservlen.Text) + 6;
                            }
                            else if (ddlcat.SelectedValue == "C" || ddlcat.SelectedValue == "T")
                            {
                                agerelaxexsercv = int.Parse(txtservlen.Text) + 8;
                            }
                        }

                        int agerelax1 = Math.Max(agerelaxexsercv, agerelaxpwd);
                        agerelax = Math.Max(agerelax1, agerelax);
                    }

                }
            }

            return agerelax;
        }
        
      protected string  getfeeexempt()
        {
            string feeexempt = "N";
            if(rdbpwd.SelectedValue=="Y" || ddlcat.SelectedValue=="C" || ddlcat.SelectedValue=="T" || (rdbexserv.SelectedValue=="Y" && rdbexwork.SelectedValue=="N"))
            {
                feeexempt = "Y";
            }
            
            return feeexempt;
        }

        protected string getcutoffbday(int agerelax)
        {
            string retparam = "";//positive

            DateTime dob = DateTime.ParseExact(datepicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //PRINCIPAL
            if (princi.Checked == true)
            {
                DateTime cutoffdateprinci = DateTime.ParseExact("30/09/1968", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime relaxedbday = dob.AddYears(agerelax);
                if (relaxedbday < cutoffdateprinci)
                    retparam = retparam + ",princi";//negative            
                else
                {
                    DateTime cutoffdateprinci_low = DateTime.ParseExact("30/09/1983", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dob > cutoffdateprinci_low)
                        retparam = retparam + ",princi";//negative
                }
            }

            //VICE-PRINCIPAL
            if (vcp.Checked == true)
            {
                DateTime cutoffdatevcp = DateTime.ParseExact("30/09/1973", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime relaxedbday = dob.AddYears(agerelax);
                if (relaxedbday < cutoffdatevcp)
                    retparam = retparam + ",vcp";//negative            
                else
                {
                    DateTime cutoffdatevcp_low = DateTime.ParseExact("30/09/1983", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dob > cutoffdatevcp_low)
                        retparam = retparam + ",vcp";//negative
                }

            }

            //PGT
            if (pgt.Checked==true)
            {
                DateTime cutoffdatepgt = DateTime.ParseExact("30/09/1978", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime relaxedbday = dob.AddYears(agerelax);
                if (relaxedbday < cutoffdatepgt)
                    retparam = retparam + ",pgt";//negative            
                  
            }

            //TGT
            if (tgt.Checked == true)
            {
                DateTime cutoffdatetgt = DateTime.ParseExact("30/09/1983", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime relaxedbday = dob.AddYears(agerelax);
                if (relaxedbday < cutoffdatetgt)
                    retparam = retparam + ",tgt";//negative
            }

            //LIB
            if (lib.Checked == true)
            {
                DateTime cutoffdatetgt = DateTime.ParseExact("30/09/1983", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime relaxedbday = dob.AddYears(agerelax);
                if (relaxedbday < cutoffdatetgt)
                    retparam = retparam + ",lib";//negative
            }

            //PRT
            if (prt.Checked == true)
            {
                DateTime cutoffdateprt = DateTime.ParseExact("30/09/1988", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //  DateTime relaxedbday = DateTime.Parse(datepicker.Text).AddYears(agerelax);
                DateTime relaxedbday = dob.AddYears(agerelax);
                if (relaxedbday < cutoffdateprt)
                    retparam = retparam + ",prt";//negative
            }


           // PRT - M
            if (prtmusic.Checked == true)
            {
                DateTime cutoffdateprt = DateTime.ParseExact("30/09/1988", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //  DateTime relaxedbday = DateTime.Parse(datepicker.Text).AddYears(agerelax);
                DateTime relaxedbday = dob.AddYears(agerelax);
                if (relaxedbday < cutoffdateprt)
                    retparam = retparam + ",prtm";//negative
            }


            return retparam;
        }
        protected string checkedu()
        {
            string retparam = "";//positive
            //PRINCIPAL
            if(princi.Checked==true)
            {
                if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                {
                    lblMsg.Text = "Enter Graduation Details";
                    retparam = "Princi-Grad";
                    return retparam;
                }
                if (txtyrpg.Text == string.Empty || txtunipg.Text == string.Empty || txtpercpg.Text == string.Empty || ddlpostgrad.SelectedIndex == 0)
                {
                    lblMsg.Text = "Enter Post-Graduation Details";
                    retparam = "Princi-PostGrad";
                    return retparam;
                }
                if (txtyrbed.Text == string.Empty || txtunibed.Text == string.Empty || txtpercbed.Text == string.Empty)
                {
                    lblMsg.Text = "Enter B.Ed Details";
                    retparam = "Princi-Bed";
                    return retparam;
                }
                string stringPercpost = txtpercpg.Text;
                float percprinci = float.Parse(stringPercpost);
                if (percprinci < 45)
                {
                    lblMsg.Text = "Percentage Obtained in Post-Graduation Should Not Be Less Than 45";
                    retparam = "Princi-PercPG";
                    return retparam;
                }
                
            }

            //VICE-PRINCIPAL
            if (vcp.Checked == true)
            {
                if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                {
                    lblMsg.Text = "Enter Graduation Details";
                    retparam = "VP-Grad";
                    return retparam;
                }
                if (txtyrpg.Text == string.Empty || txtunipg.Text == string.Empty || txtpercpg.Text == string.Empty || ddlpostgrad.SelectedIndex == 0)
                {
                    lblMsg.Text = "Enter Post-Graduation Details";
                    retparam = "VP-PostGrad";
                    return retparam;
                }
                if (txtyrbed.Text == string.Empty || txtunibed.Text == string.Empty || txtpercbed.Text == string.Empty)
                {
                    lblMsg.Text = "Enter B.Ed Details";
                    retparam = "Princi-Bed";
                    return retparam;
                }
                string stringPercpost = txtpercpg.Text;
                float percprinci = float.Parse(stringPercpost);
                if (percprinci < 50)
                {
                    lblMsg.Text = "Percentage Obtained in Post-Graduation Should Not Be Less Than 50";
                    retparam = "VP-PercPG";
                    return retparam;
                }

            }

            //PGT
            if (pgt.Checked == true)
            {
                if (ddlpgt.SelectedValue == "11")//COMPUTER SCIENCE
                {
                    if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Enter Graduation Details";
                        retparam = "PGT-Grad-CS";
                        return retparam;
                    }
                    if (ddlgrad.SelectedValue == "03")//BE/BTech comp. sci
                    {
                        string stringPercpost = txtpercgrad.Text;
                        float percpgt = float.Parse(stringPercpost);
                        if (percpgt < 50)
                        {
                            lblMsg.Text = "Percentage Obtained in Graduation Should Not Be Less Than 50";
                            retparam = "PGT-PercBtech";
                            return retparam;
                        }
                    }
                   
                    else// Others
                    {
                        if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                        {
                            lblMsg.Text = "Enter Graduation Details";
                            retparam = "PGT-Grad-CS";
                            return retparam;
                        }

                        if (txtyrpg.Text == string.Empty || txtunipg.Text == string.Empty || txtpercpg.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                        {
                            lblMsg.Text = "Enter Post-Graduation Details";
                            retparam = "PGT-Grad";
                            return retparam;
                        }

                        if (ddlgrad.SelectedValue == "08" && ddlpostgrad.SelectedValue == "07")//B-Level+C-Level
                        {
                            lblMsg.Text = "B-Level at graduation and C-Level at Post-Graduation is Wrong Combination.";
                            retparam = "PGT-Grad-CS-B&C Level";
                            return retparam;
                        }


                        string stringPercpost = txtpercpg.Text;
                        float percpgt = float.Parse(stringPercpost);
                        if (percpgt < 50)
                        {
                            lblMsg.Text = "Percentage Obtained in Post-Graduation Should Not Be Less Than 50";
                            retparam = "PGT-PercPG";
                            return retparam;
                        }
                    }
                }

                else//OTHER PGTs
                {
                    if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Enter Graduation Details";
                        retparam = "PGT-Grad";
                        return retparam;
                    }

                    if (txtyrpg.Text == string.Empty || txtunipg.Text == string.Empty || txtpercpg.Text == string.Empty || ddlpostgrad.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Enter Post-Graduation Details";
                        retparam = "PGT-Post-Grad";
                        return retparam;
                    }
                    if (txtyrbed.Text == string.Empty || txtunibed.Text == string.Empty || txtpercbed.Text == string.Empty)
                    {
                        lblMsg.Text = "Enter B.Ed Details";
                        retparam = "PGT-Bed";
                        return retparam;
                    }
                    if (ddlpostgrad.SelectedValue == "08")//C-Level
                    {
                        lblMsg.Text = "Post-Graduation Degree Invalid for applying for PGT-" + ddlpgt.SelectedItem.ToString() + "";
                        retparam = "PGT-Grad-C Level";
                        return retparam;
                    }
                    if (ddlpostgrad.SelectedValue != "06")
                    {
                        string stringPercpost = txtpercpg.Text;
                        float percpgt = float.Parse(stringPercpost);
                        if (percpgt < 50)
                        {
                            lblMsg.Text = "Percentage Obtained in Post-Graduation Should Not Be Less Than 50";
                            retparam = "PGT-PercMaster";
                            return retparam;
                        }
                    }
                }

            }

            //TGT
            if (tgt.Checked == true)
            {
                if (ddltgt.SelectedValue == "08")//A.E
                {
                    if ((txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0) && (txtyrdip.Text == string.Empty || txtunidip.Text == string.Empty || txtpercdip.Text == string.Empty))
                    {
                        lblMsg.Text = "Enter Complete Graduation OR Diploma Details";
                        retparam = "TGT-Grad";
                        return retparam;
                    }

                    //if (txtyrdip.Text == string.Empty || txtunidip.Text == string.Empty || txtpercdip.Text == string.Empty)
                    //{
                    //    if (ddlgrad.SelectedValue != "01" || ddlgrad.SelectedValue != "05")//Not Bachelor of Arts
                    //    {
                    //        lblMsg.Text = "Graduation Details Invalid for Applying for TGT-Art Education (AE)";
                    //        retparam = "TGT-AE-Grad-Invalid";
                    //        return retparam;
                    //    }
                    //}


                }

               else if (ddltgt.SelectedValue == "09")//W.E
                {
                    if ((txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0) && (txtyrdip.Text == string.Empty || txtunidip.Text == string.Empty || txtpercdip.Text == string.Empty ))
                    {
                        lblMsg.Text = "Enter Complete Graduation OR Diploma Details";
                        retparam = "TGT-Grad";
                        return retparam;
                    }

                    if (txtyrdip.Text == string.Empty || txtunidip.Text == string.Empty || txtpercdip.Text == string.Empty)
                    {
                        if (ddlgrad.SelectedValue != "12")//Not B.E/B.Tech Electrical/Electronics
                        {
                            lblMsg.Text = "Graduation Details Invalid for Applying for TGT-Work Experience (WE)";
                            retparam = "TGT-WE-Grad-Invalid";
                            return retparam;
                        }
                    }


                }

             
               else if (ddltgt.SelectedValue == "07")//Phy Edu
                {
                    if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Enter Complete Graduation Details";
                        retparam = "TGT-Grad";
                        return retparam;
                    }
                    if (ddlgrad.SelectedValue != "11")//No Graduation in Phy Edu
                    {
                        lblMsg.Text = "Graduation Degree Invalid for applying for TGT-" + ddltgt.SelectedItem.ToString() + "";
                        retparam = "TGT-Grad-Degree";
                        return retparam;
                    }

                }
                else//OTHER TGTs
                {
                    if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Enter Graduation Details";
                        retparam = "TGT-Grad";
                        return retparam;
                    }
                    if (ddlgrad.SelectedValue == "03" || ddlgrad.SelectedValue == "10" || ddlgrad.SelectedValue == "12")//BE/BTech
                    {
                        lblMsg.Text = "Graduation Degree Invalid for applying for TGT-" + ddltgt.SelectedItem.ToString() + "";
                        retparam = "TGT-Grad-Degree";
                        return retparam;
                    }

                    if (txtyrbed.Text == string.Empty || txtunibed.Text == string.Empty || txtpercbed.Text == string.Empty)
                    {
                        lblMsg.Text = "Enter B.Ed Details";
                        retparam = "TGT-Bed";
                        return retparam;
                    }
                   
                    string stringPercpost = txtpercgrad.Text;
                    float perctgt = float.Parse(stringPercpost);
                    if (perctgt < 50)
                    {
                        lblMsg.Text = "Percentage Obtained in Graduation Should Not Be Less Than 50";
                        retparam = "TGT-PercGrad";
                        return retparam;
                    }

                    if (txtyrctet2.Text != string.Empty && txtyrctet2.Text != "2018")
                    {
                        if (txtpercctet2.Text != string.Empty)
                        {
                            string ctetmarks2 = txtpercctet2.Text;
                            int ctet2 = int.Parse(ctetmarks2);

                            if (ddlcat.SelectedIndex == 1 && rdbpwd.SelectedValue == "N")//general non pwd
                            {
                                if (ctet2 < 90)
                                {
                                    lblMsg.Text = "Marks Obtained in CTET Paper-II should not be less than 90";
                                    retparam = "TGT-Ctet1-Marks";
                                    return retparam;
                                }
                            }
                            else if (ddlcat.SelectedIndex != 1 || rdbpwd.SelectedValue == "Y")//scstobc or pwd
                            {
                                if (ctet2 < 82)
                                {
                                    lblMsg.Text = "Marks Obtained in CTET Paper-II should not be less than 82";
                                    retparam = "TGT-Ctet1-Marks-Cat";
                                    return retparam;
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Enter CTET II Marks Obtained";
                            retparam = "TGT-Ctet";
                            return retparam;
                        }
                    }
                    else if (txtyrctet2.Text == "2018")
                    {
                        if (txtctetreg2.Text == string.Empty)
                        {
                            lblMsg.Text = "Enter CTET II Registration Number";
                            retparam = "TGT-Ctet";
                            return retparam;
                        }
                    }
                }
            }
            //Librarian
            if (lib.Checked == true)
            {
                if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty || ddlgrad.SelectedIndex == 0)
                {
                    lblMsg.Text = "Enter Complete Graduation Details";
                    retparam = "TGT-Lib";
                    return retparam;
                }

                if (rdbllib.SelectedValue == "Graduate with one year diploma in Library Science from a recognized institution.")
                {
                    if (txtyrdip.Text == string.Empty || txtunidip.Text == string.Empty || txtpercdip.Text == string.Empty)
                    {
                        lblMsg.Text = "Enter Diploma Details";
                        retparam = "TGT-Lib";
                        return retparam;
                    }
                }

                if (txtyrdip.Text == string.Empty || txtunidip.Text == string.Empty || txtpercdip.Text == string.Empty)
                {
                    if (ddlgrad.SelectedValue != "13")//Not Library Sciences
                    {
                        lblMsg.Text = "Graduation Details Invalid for Applying for Librarian";
                        retparam = "TGT-Lib-Invalid";
                        return retparam;
                    }
                }

            }
            //PRT
            if (prt.Checked == true)
            {

                if ((txtyrbed.Text == string.Empty || txtunibed.Text == string.Empty || txtpercbed.Text == string.Empty) && (txtyrded.Text == string.Empty || txtunided.Text == string.Empty || txtpercded.Text == string.Empty) && (txtyrdip.Text == string.Empty || txtunidip.Text == string.Empty || txtpercdip.Text == string.Empty))
                {
                    lblMsg.Text = "Enter Complete B.Ed/B.El.Ed OR Diploma Details";
                    retparam = "TGT-Grad";
                    return retparam;
                }
                string stringPercpost = "";
                if (rdblprt.SelectedValue == "Graduation with atleast 50% marks and Bachelor of Education (B.Ed.)*")
                {
                    stringPercpost = txtpercgrad.Text;
                    float percprt = float.Parse(stringPercpost);
                    if (percprt < 50)
                    {
                        lblMsg.Text = "Percentage Obtained in Graduation Should Not Be Less Than 50";
                        retparam = "PRT-Perc12";
                        return retparam;
                    }
                }
                else
                {
                    stringPercpost = txtperc12.Text;
                    float percprt = float.Parse(stringPercpost);
                    if (percprt < 50)
                    {
                        lblMsg.Text = "Percentage Obtained in Senior Secondary/Equivalent Should Not Be Less Than 50";
                        retparam = "PRT-Perc12";
                        return retparam;
                    }
                }


                if (txtyrctet.Text == string.Empty && txtyrctet2.Text == string.Empty)
                {
                    lblMsg.Text = "Enter CTET Details";
                    retparam = "PRT-Ctet";
                    return retparam;
                }

                //CTET 1
                if (txtyrctet.Text != string.Empty && txtyrctet.Text != "2018")
                {
                    if (txtpercctet.Text != string.Empty)
                    {
                        string ctetmarks = txtpercctet.Text;
                        int ctet = int.Parse(ctetmarks);

                        if (ddlcat.SelectedIndex == 1 && rdbpwd.SelectedValue == "N")//general non pwd
                        {
                            if (ctet < 90)
                            {
                                lblMsg.Text = "Marks Obtained in CTET Paper-I should not be less than 90";
                                retparam = "PRT-Ctet1-Marks";
                                return retparam;
                            }
                        }
                        else if (ddlcat.SelectedIndex != 1 || rdbpwd.SelectedValue == "Y")//scstobc or pwd
                        {
                            if (ctet < 82)
                            {
                                lblMsg.Text = "Marks Obtained in CTET Paper-I should not be less than 82";
                                retparam = "PRT-Ctet1-Marks-Cat";
                                return retparam;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Enter CTET I Marks Obtained";
                        retparam = "PRT-Ctet";
                        return retparam;
                    }
                }
                else if (txtyrctet.Text == "2018")
                {
                    if (txtctetreg.Text == string.Empty)
                    {
                        lblMsg.Text = "Enter CTET I Registration Number";
                        retparam = "PRT-Ctet";
                        return retparam;
                    }
                }
                //CTET 2
                if (txtyrctet2.Text != string.Empty && txtyrctet2.Text != "2018")
                {
                    if (txtpercctet2.Text != string.Empty)
                    {
                        string ctetmarks2 = txtpercctet2.Text;
                        int ctet2 = int.Parse(ctetmarks2);

                        if (ddlcat.SelectedIndex == 1 && rdbpwd.SelectedValue == "N")//general non pwd
                        {
                            if (ctet2 < 90)
                            {
                                lblMsg.Text = "Marks Obtained in CTET Paper-II should not be less than 90";
                                retparam = "PRT-Ctet1-Marks";
                                return retparam;
                            }
                        }
                        else if (ddlcat.SelectedIndex != 1 || rdbpwd.SelectedValue == "Y")//scstobc or pwd
                        {
                            if (ctet2 < 82)
                            {
                                lblMsg.Text = "Marks Obtained in CTET Paper-II should not be less than 82";
                                retparam = "PRT-Ctet1-Marks-Cat";
                                return retparam;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Enter CTET II Marks Obtained";
                        retparam = "PRT-Ctet";
                        return retparam;
                    }
                }
                else if (txtyrctet2.Text == "2018")
                {
                    if (txtctetreg2.Text == string.Empty)
                    {
                        lblMsg.Text = "Enter CTET II Registration Number";
                        retparam = "PRT-Ctet";
                        return retparam;
                    }
                }
            }

            
            


           // PRT - M
            if (prtmusic.Checked == true)
            {
                string stringPercpost = txtperc12.Text;
                float percprt = float.Parse(stringPercpost);
                if (percprt < 50)
                {
                    lblMsg.Text = "Percentage Obtained in Senior Secondary/Equivalent Should Not Be Less Than 50";
                    retparam = "PRTM-Perc12";
                    return retparam;
                }
                if (txtyrgrad.Text == string.Empty || txtunigrad.Text == string.Empty || txtpercgrad.Text == string.Empty)
                {
                    lblMsg.Text = "Enter Graduation Details";
                    retparam = "PRTM-Grad";
                    return retparam;
                }
                if (ddlgrad.SelectedValue == "03" || ddlgrad.SelectedValue == "10" || ddlgrad.SelectedValue == "12")//BE/BTech
                {
                    lblMsg.Text = "Graduation Degree Invalid";
                    retparam = "PRTM-Grad-Degree";
                    return retparam;
                }
                //if (txtyrctet.Text == string.Empty || txtpercctet.Text == string.Empty)
                //{
                //    lblMsg.Text = "Enter CTET Paper-I Details";
                //    retparam = "TGT-Ctet";
                //    return retparam;
                //}
                //string ctetmarks = txtpercctet.Text;
                //int ctet = int.Parse(ctetmarks);

                //if (ddlcat.SelectedIndex == 1)
                //{
                //    if (ctet < 90)
                //    {
                //        lblMsg.Text = "Marks Obtained in CTET should not be less than 90";
                //        retparam = "PRTM-Ctet-Marks";
                //        return retparam;
                //    }
                //}
                //else
                //{
                //    if (ctet < 82)
                //    {
                //        lblMsg.Text = "Marks Obtained in CTET should not be less than 82";
                //        retparam = "PRTM-Ctet-Marks-Cat";
                //        return retparam;
                //    }
                //}

            }


            return retparam;
        }
        protected int calcFee()
        {
            int fee = 0;
            if (pgt.Checked == true)
                fee = fee + 1000;
            if (tgt.Checked == true)
                fee = fee + 1000;
            if (prt.Checked == true)
                fee = fee + 1000;
            if (prtmusic.Checked == true)
                fee = fee + 1000;
            if (lib.Checked == true)
                fee = fee + 1000;
            if (princi.Checked == true)
                fee = fee + 1500;
            if (vcp.Checked == true)
                fee = fee + 1500;
            return fee;
        }
        protected void btnLog_Click(object sender, EventArgs e)
        {
          
                Response.Redirect("editlogin.aspx", false);
            
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            //post check

            if (pgt.Checked == false && tgt.Checked == false && prt.Checked == false && princi.Checked == false && vcp.Checked == false && prtmusic.Checked == false && lib.Checked == false)
            {
                lblMsg.Text = "Select At Least One Post To Apply.";
                return;
            }
            DateTime dob = DateTime.ParseExact(datepicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(@"select count(*) from kv_cand_edit where cand_id=@cand_id", con);
            cmd.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = hdcandid.Value;

            int count = (int)cmd.ExecuteScalar();

            if (count != 0)
            {
                lblfee.Text = "Duplicate Data Found.";

                con.Close();
                con.Dispose();
                return;
            }
            else
            {
                //fee exemption
                string isFeeExempt = getfeeexempt();
                int fee = 0;
                int feediff = 0;
                if (isFeeExempt == "N")
                {
                    fee = calcFee();
                    int oldfee = int.Parse(hdfee.Value);
                    feediff = fee - oldfee;
                }
                //checkphoto
                byte[] imgbyte_sign=null;
                byte[] imgbyte_photo=null;
                if (upphoto.HasFile)
                {
                    //check MIME type of the files
                    int checkmimeVal = 0;
                    checkmimeVal = checkmime("photo");

                    // bool checkmimeVal = getMimeType();
                    if (checkmimeVal == 0)
                    {
                        //  check file size and file format
                        string ext_photo = System.IO.Path.GetExtension(upphoto.PostedFile.FileName);

                        // check double extension
                        int count_photo = upphoto.FileName.Split('.').Length - 1;


                        if (count_photo > 1)
                        {
                            lblMsg.Text = "INVALID FILE TYPE";
                            return;
                        }
                        if (upphoto.PostedFile.ContentLength > (50 * 1024) || upphoto.PostedFile.ContentLength == 0 || ext_photo.ToUpper() != ".JPG")
                        {
                            lblMsg.Text = "PLEASE UPLOAD PHOTOGRAPH WITH SIZE LESS THAN 50 KB IN JPG FORMAT ONLY";

                            return;
                        }


                        // reset input stream
                        upphoto.PostedFile.InputStream.Position = 0;

                        int length_photo = upphoto.PostedFile.ContentLength;
                        imgbyte_photo = new byte[length_photo];
                        HttpPostedFile img_photo = upphoto.PostedFile;
                        //set the binary data
                        img_photo.InputStream.Read(imgbyte_photo, 0, length_photo);
                        //   check first four bytes for JPG format
                        byte first = imgbyte_photo[0];
                        byte sec = imgbyte_photo[1];
                        byte th = imgbyte_photo[2];
                        byte fo = imgbyte_photo[3];
                        if (first != 255 || sec != 216)
                        {
                            lblMsg.Text = "Invalid File Type For Photo.";


                            return;
                        }
                        string filename_photo = hdcandid.Value + "_photo";
                    }
                }
                if (upsign.HasFile)
                {
                    int checkmimeVal = 0;
                    checkmimeVal = checkmime("sign");

                    // bool checkmimeVal = getMimeType();
                    if (checkmimeVal == 0)
                    {
                        //  check file size and file format
                        string ext_sign = System.IO.Path.GetExtension(upsign.PostedFile.FileName);

                        // check double extension
                        int count_sign = upsign.FileName.Split('.').Length - 1;


                        if (count_sign > 1)
                        {
                            lblMsg.Text = "INVALID FILE TYPE FOR SIGNATURE";
                            return;
                        }
                        if (upsign.PostedFile.ContentLength > (20 * 1024) || upsign.PostedFile.ContentLength == 0 || ext_sign.ToUpper() != ".JPG")
                        {
                            lblMsg.Text = "PLEASE UPLOAD SIGNATURE WITH SIZE LESS THAN 20 KB IN JPG FORMAT ONLY";

                            return;
                        }
                        upsign.PostedFile.InputStream.Position = 0;
                        int length_sign = upsign.PostedFile.ContentLength;
                        imgbyte_sign = new byte[length_sign];
                        HttpPostedFile img_sign = upsign.PostedFile;
                        //set the binary data
                        img_sign.InputStream.Read(imgbyte_sign, 0, length_sign);
                        // check first four bytes for JPG format

                        byte firstsi = imgbyte_sign[0];
                        byte secsi = imgbyte_sign[1];
                        byte thsi = imgbyte_sign[2];
                        byte fosi = imgbyte_sign[3];
                        if (firstsi != 255 || secsi != 216)
                        {
                            lblMsg.Text = "Invalid File Type For Signature.";

                            return;
                        }
                        string filename_sign = hdcandid.Value + "_sign";
                    }
                }
                //encryption

                string govidenc = AESEncrytDecry.Encrypt(txtgovid.Text.Trim());
                //store the data

                SqlTransaction trans = con.BeginTransaction();
                try
                {

                    string cand_id = hdcandid.Value;


                    SqlCommand Cmd = new SqlCommand(@"insert into kv_cand_edit(
       cand_id
      ,post_pgt
      ,post_tgt
      ,post_prt
    
      ,ess_qual
      ,des_qual
      ,cname_f
      ,cname_l
      ,mname
      ,fname
      ,marital
      ,gender
      ,cat
      ,pwd
      ,scribe
      ,kvs
      ,cg
      ,cg_len
      ,jk
      ,exserv
      ,ex_len
      ,iden_mark
      ,dob
      ,add1
      ,add2
      ,city
      ,[state]
      ,pin
      ,email
      ,mobile1
      ,mobile2
      ,exam_city1
      ,exam_city2
      ,exam_city3
      ,exam_city4
      ,med
      ,sub_grad
      ,sub_pg
     
      ,yr_10
      ,yr_12
      ,yr_ded
      ,yr_bed
      ,yr_grad
      ,yr_dip
      ,yr_pg
      ,yr_ctet
      ,yr_ctet2
      ,uni_10
      ,uni_12
      ,uni_ded
      ,uni_bed
      ,uni_grad
      ,uni_dip
      ,uni_pg
      ,perc_10
      ,perc_12
      ,perc_ded
      ,perc_bed
      ,perc_grad
      ,perc_dip
      ,perc_pg   
      ,perc_ctet,perc_ctet2,fee_ex,ip,dateup,fee,religion,post_prtm,post_lib,zone1,zone2,zone3,zone4,zone5,zone6,idtype,idnum,exserv_work,newname,essqual,post_princi,post_vcp,ctetregno,ctet2regno)values(
       @cand_id
      ,@post_pgt
      ,@post_tgt
      ,@post_prt
    
      ,@ess_qual
      ,@des_qual
      ,@cname_f
      ,@cname_l
      ,@mname
      ,@fname
      ,@marital
      ,@gender
      ,@cat
      ,@pwd
      ,@scribe
      ,@kvs
      ,@cg
      ,@cg_len
      ,@jk
      ,@exserv
      ,@ex_len
      ,@iden_mark
      ,@dob
      ,@add1
      ,@add2
      ,@city
      ,@state
      ,@pin
      ,@email
      ,@mobile1
      ,@mobile2
      ,@exam_city1
      ,@exam_city2
      ,@exam_city3
      ,@exam_city4
      ,@med
      ,@sub_grad
      ,@sub_pg
     
      ,@yr_10
      ,@yr_12
      ,@yr_ded
      ,@yr_bed
      ,@yr_grad
      ,@yr_dip
      ,@yr_pg
      ,@yr_ctet
      ,@yr_ctet2
      ,@uni_10
      ,@uni_12
      ,@uni_ded
      ,@uni_bed
      ,@uni_grad
      ,@uni_dip
      ,@uni_pg
      ,@perc_10
      ,@perc_12
      ,@perc_ded
      ,@perc_bed
      ,@perc_grad
      ,@perc_dip
      ,@perc_pg    
      ,@perc_ctet,@perc_ctet2,@fee_ex,@ip,@dateup,@fee,@religion,@post_prtm,@post_lib,@zone1,@zone2,@zone3,@zone4,@zone5,@zone6,@idtype,@idnum,@exserv_work,@newname,@essqual,@post_princi,@post_vcp,@ctetregno,@ctet2regno)", con, trans);
                    Cmd.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = cand_id;
                    Cmd.Parameters.AddWithValue("@post_pgt", SqlDbType.VarChar).Value = pgt.Checked == true ? ddlpgt.SelectedValue : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@post_tgt", SqlDbType.VarChar).Value = tgt.Checked == true ? ddltgt.SelectedValue : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@post_prt", SqlDbType.VarChar).Value = prt.Checked == true ? "PRT" : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@ess_qual", SqlDbType.VarChar).Value = ddless.SelectedValue;
                    Cmd.Parameters.AddWithValue("@des_qual", SqlDbType.VarChar).Value = ddldesire.SelectedValue;
                    Cmd.Parameters.AddWithValue("@cname_f", SqlDbType.VarChar).Value = txtcfname.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@cname_l", SqlDbType.VarChar).Value = txtclname.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@mname", SqlDbType.VarChar).Value = txtmname.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@fname", SqlDbType.VarChar).Value = txtfname.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@marital", SqlDbType.VarChar).Value = ddlmar.SelectedValue;
                    Cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar).Value = ddlgen.SelectedValue;
                    Cmd.Parameters.AddWithValue("@cat", SqlDbType.VarChar).Value = ddlcat.SelectedValue;
                    Cmd.Parameters.AddWithValue("@pwd", SqlDbType.VarChar).Value = rdbpwd.SelectedValue == "Y" ? ddlpwdcat.SelectedValue : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@scribe", SqlDbType.VarChar).Value = rdbpwd.SelectedValue == "Y" ? rdbscribe.SelectedValue : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@kvs", SqlDbType.VarChar).Value = rdbemp.SelectedValue;
                    Cmd.Parameters.AddWithValue("@cg", SqlDbType.VarChar).Value = rdbcgemp.SelectedValue;
                    Cmd.Parameters.AddWithValue("@cg_len", SqlDbType.VarChar).Value = rdbcgemp.SelectedValue == "Y" ? txtyrreg.Text : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@jk", SqlDbType.VarChar).Value = rdbjk.SelectedValue;
                    Cmd.Parameters.AddWithValue("@exserv", SqlDbType.VarChar).Value = rdbexserv.SelectedValue;
                    Cmd.Parameters.AddWithValue("@ex_len", SqlDbType.VarChar).Value = rdbexserv.SelectedValue == "Y" ? txtservlen.Text : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@iden_mark", SqlDbType.VarChar).Value = txtiden.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@dob", SqlDbType.Date).Value = dob;
                    Cmd.Parameters.AddWithValue("@add1", SqlDbType.VarChar).Value = txtadd1.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@add2", SqlDbType.VarChar).Value = txtadd2.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@city", SqlDbType.VarChar).Value = ddlcity.Text;
                    Cmd.Parameters.AddWithValue("@state", SqlDbType.VarChar).Value = ddlstate.Text;
                    Cmd.Parameters.AddWithValue("@pin", SqlDbType.VarChar).Value = txtpin.Text;
                    Cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = lblemail.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@mobile1", SqlDbType.VarChar).Value = txtmob.Text;
                    Cmd.Parameters.AddWithValue("@mobile2", SqlDbType.VarChar).Value = txtmob2.Text != string.Empty ? txtmob2.Text : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@exam_city1", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "Delhi" : ddlcity1.SelectedValue;
                    Cmd.Parameters.AddWithValue("@exam_city2", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "Delhi" : ddlcity2.SelectedValue;
                    Cmd.Parameters.AddWithValue("@exam_city3", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "Delhi" : ddlcity3.SelectedValue;
                    Cmd.Parameters.AddWithValue("@exam_city4", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "Delhi" : ddlcity4.SelectedValue;
                    Cmd.Parameters.AddWithValue("@med", SqlDbType.VarChar).Value = ddlmed.SelectedValue;
                    Cmd.Parameters.AddWithValue("@sub_grad", SqlDbType.VarChar).Value = ddlgrad.SelectedValue;
                    Cmd.Parameters.AddWithValue("@sub_pg", SqlDbType.VarChar).Value = ddlpostgrad.SelectedValue;

                    Cmd.Parameters.AddWithValue("@yr_10", SqlDbType.VarChar).Value = txtyr10.Text;
                    Cmd.Parameters.AddWithValue("@yr_12", SqlDbType.VarChar).Value = txtyr12.Text;
                    Cmd.Parameters.AddWithValue("@yr_grad", SqlDbType.VarChar).Value = txtyrgrad.Text;
                    Cmd.Parameters.AddWithValue("@yr_ded", SqlDbType.VarChar).Value = txtyrded.Text;
                    Cmd.Parameters.AddWithValue("@yr_bed", SqlDbType.VarChar).Value = txtyrbed.Text;
                    Cmd.Parameters.AddWithValue("@yr_pg", SqlDbType.VarChar).Value = txtyrpg.Text;
                    Cmd.Parameters.AddWithValue("@yr_dip", SqlDbType.VarChar).Value = txtyrdip.Text;
                    Cmd.Parameters.AddWithValue("@yr_ctet", SqlDbType.VarChar).Value = txtyrctet.Text;
                    Cmd.Parameters.AddWithValue("@yr_ctet2", SqlDbType.VarChar).Value = txtyrctet2.Text;

                    Cmd.Parameters.AddWithValue("@uni_10", SqlDbType.VarChar).Value = txtuni10.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@uni_12", SqlDbType.VarChar).Value = txtuni12.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@uni_grad", SqlDbType.VarChar).Value = txtunigrad.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@uni_ded", SqlDbType.VarChar).Value = txtunided.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@uni_bed", SqlDbType.VarChar).Value = txtunibed.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@uni_pg", SqlDbType.VarChar).Value = txtunipg.Text.ToUpper();
                    Cmd.Parameters.AddWithValue("@uni_dip", SqlDbType.VarChar).Value = txtunidip.Text.ToUpper();

                    Cmd.Parameters.AddWithValue("@perc_10", SqlDbType.VarChar).Value = txtperc10.Text;
                    Cmd.Parameters.AddWithValue("@perc_12", SqlDbType.VarChar).Value = txtperc12.Text;
                    Cmd.Parameters.AddWithValue("@perc_grad", SqlDbType.VarChar).Value = txtpercgrad.Text;
                    Cmd.Parameters.AddWithValue("@perc_ded", SqlDbType.VarChar).Value = txtpercded.Text;
                    Cmd.Parameters.AddWithValue("@perc_bed", SqlDbType.VarChar).Value = txtpercbed.Text;
                    Cmd.Parameters.AddWithValue("@perc_pg", SqlDbType.VarChar).Value = txtpercpg.Text;
                    Cmd.Parameters.AddWithValue("@perc_dip", SqlDbType.VarChar).Value = txtpercdip.Text;
                    Cmd.Parameters.AddWithValue("@perc_ctet", SqlDbType.VarChar).Value = txtpercctet.Text;
                    Cmd.Parameters.AddWithValue("@perc_ctet2", SqlDbType.VarChar).Value = txtpercctet2.Text;

                    Cmd.Parameters.AddWithValue("@fee_ex", SqlDbType.VarChar).Value = isFeeExempt;

                    Cmd.Parameters.AddWithValue("@ip", SqlDbType.VarChar).Value = Request.ServerVariables["REMOTE_ADDR"];
                    Cmd.Parameters.AddWithValue("@dateup", SqlDbType.Date).Value = DateTime.Now;
                    Cmd.Parameters.AddWithValue("@fee", SqlDbType.VarChar).Value = feediff;
                    Cmd.Parameters.AddWithValue("@religion", SqlDbType.VarChar).Value = ddlrel.SelectedValue.ToString();

                    Cmd.Parameters.AddWithValue("@post_prtm", SqlDbType.VarChar).Value = prtmusic.Checked == true ? "PRTM" : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@post_lib", SqlDbType.VarChar).Value = lib.Checked == true ? "LIB" : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@zone1", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "NA" : ddlzone1.SelectedValue;
                    Cmd.Parameters.AddWithValue("@zone2", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "NA" : ddlzone2.SelectedValue;
                    Cmd.Parameters.AddWithValue("@zone3", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "NA" : ddlzone3.SelectedValue;
                    Cmd.Parameters.AddWithValue("@zone4", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "NA" : ddlzone4.SelectedValue;
                    Cmd.Parameters.AddWithValue("@zone5", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "NA" : ddlzone5.SelectedValue;
                    Cmd.Parameters.AddWithValue("@zone6", SqlDbType.VarChar).Value = princi.Checked == true || vcp.Checked == true ? "NA" : ddlzone6.SelectedValue;
                    Cmd.Parameters.AddWithValue("@idtype", SqlDbType.VarChar).Value = ddlid.SelectedValue;
                    Cmd.Parameters.AddWithValue("@idnum", SqlDbType.VarChar).Value = govidenc;
                    Cmd.Parameters.AddWithValue("@exserv_work", SqlDbType.VarChar).Value = rdbexwork.SelectedValue;
                    Cmd.Parameters.AddWithValue("@newname", SqlDbType.VarChar).Value = txtnewname.Text;
                    Cmd.Parameters.AddWithValue("@essqual", SqlDbType.VarChar).Value = Session["essqual"].ToString();
                    Cmd.Parameters.AddWithValue("@post_princi", SqlDbType.VarChar).Value = princi.Checked == true ? "PRN" : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@post_vcp", SqlDbType.VarChar).Value = vcp.Checked == true ? "VCP" : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@ctetregno", SqlDbType.VarChar).Value = txtctetreg.Text != string.Empty ? txtctetreg.Text : (object)DBNull.Value;
                    Cmd.Parameters.AddWithValue("@ctet2regno", SqlDbType.VarChar).Value = txtctetreg2.Text != string.Empty ? txtctetreg2.Text : (object)DBNull.Value;
                    if (Cmd.ExecuteNonQuery().Equals(1))
                    {

                        SqlCommand CmdPhoto = new SqlCommand(@"insert into kv_photo_edit(cand_id,photo,sign,ip,dateup)values(@candid,@photo,@sign,@ip,@dateup)", con, trans);
                        CmdPhoto.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = cand_id;
                        CmdPhoto.Parameters.AddWithValue("@photo", SqlDbType.VarBinary).Value = upphoto.HasFile ? imgbyte_photo : System.Data.SqlTypes.SqlBinary.Null;
                        CmdPhoto.Parameters.AddWithValue("@sign", SqlDbType.VarBinary).Value = upsign.HasFile ? imgbyte_sign : System.Data.SqlTypes.SqlBinary.Null;
                        CmdPhoto.Parameters.AddWithValue("@ip", SqlDbType.VarChar).Value = Request.ServerVariables["REMOTE_ADDR"];
                        CmdPhoto.Parameters.AddWithValue("@dateup", SqlDbType.VarChar).Value = DateTime.Now;
                        CmdPhoto.ExecuteNonQuery();

                        if (isFeeExempt == "Y")
                        {
                            SqlCommand Cmdstatus = new SqlCommand(@"update kv_status set editstep1=@editstep1,editstep2=@editstep2,editstep3=@editstep3 where cand_id=@candid", con, trans);
                            Cmdstatus.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = cand_id;
                            Cmdstatus.Parameters.AddWithValue("@editstep1", SqlDbType.VarChar).Value = "Y";
                            Cmdstatus.Parameters.AddWithValue("@editstep2", SqlDbType.VarChar).Value = "Y";
                            Cmdstatus.Parameters.AddWithValue("@editstep3", SqlDbType.VarChar).Value = "Y";
                            Cmdstatus.ExecuteNonQuery();
                            lblfee.ForeColor = System.Drawing.Color.Green;
                            btnLog.Visible = true;
                            lblfee.Text = "Step 1 & 2 Edited Successfully For Registration Number : " + cand_id + ".\n\n Login With Your Registration Number And Date of Birth To Generate New Confirmation Page.";

                        }
                        else
                        {
                            if (feediff > 0)
                            {
                                SqlCommand Cmdstatus = new SqlCommand(@"update kv_status set editstep1=@editstep1,editstep2=@editstep2 where cand_id=@candid", con, trans);
                                Cmdstatus.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = cand_id;
                                Cmdstatus.Parameters.AddWithValue("@editstep1", SqlDbType.VarChar).Value = "Y";
                                Cmdstatus.Parameters.AddWithValue("@editstep2", SqlDbType.VarChar).Value = "Y";
                                Cmdstatus.ExecuteNonQuery();
                                lblfee.ForeColor = System.Drawing.Color.Green;
                                btnLog.Visible = true;

                                lblfee.Text = "Step 1 & 2 Edited Successfully For Registration Number : " + cand_id + ".\n\n Login With Your Registration Number And Date of Birth To Pay Fee Difference.";
                            }
                            else
                            {
                                SqlCommand Cmdstatus = new SqlCommand(@"update kv_status set editstep1=@editstep1,editstep2=@editstep2,editstep3=@editstep3 where cand_id=@candid", con, trans);
                                Cmdstatus.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = cand_id;
                                Cmdstatus.Parameters.AddWithValue("@editstep1", SqlDbType.VarChar).Value = "Y";
                                Cmdstatus.Parameters.AddWithValue("@editstep2", SqlDbType.VarChar).Value = "Y";
                                Cmdstatus.Parameters.AddWithValue("@editstep3", SqlDbType.VarChar).Value = "Y";
                                Cmdstatus.ExecuteNonQuery();
                                lblfee.ForeColor = System.Drawing.Color.Green;
                                btnLog.Visible = true;

                                lblfee.Text = "Step 1 & 2 Edited Successfully For Registration Number : " + cand_id + ".\n\n Login With Your Registration Number And Date of Birth To Generate New Confirmation Page.";

                            }
                        }
                        tblpreview.Visible = false;

                        btnOk.Visible = false;
                        btnCancel.Visible = false;
                        trans.Commit();
                    }

                }

                catch (Exception ex)
                {
                    tblpreview.Visible = true;

                    lblfee.Text = ex.ToString();
                    lblfee.ForeColor = System.Drawing.Color.Red;
                    btnLog.Visible = false;
                    all_form.Visible = true;

                    trans.Rollback();
                    return;
                }

                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
         

        protected void btnCancel_Click(object sender, EventArgs e)
        {


            ModalPopupExtender2.Hide();
        }
        protected void btncont1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //validate address and identification mark
                string add1 = txtadd1.Text;
                add1 = add1.Replace("'", "");
                add1 = add1.Replace("/", "");
                add1 = add1.Replace("\\", "");
                add1 = add1.Replace("+", "");
                add1 = add1.Replace("*", "");
                add1 = add1.Replace("#", "");
                add1 = add1.Replace("&", "");
                add1 = add1.Replace(".", "");
                add1 = add1.Replace("%", "");
                add1 = add1.Replace("<", "");
                add1 = add1.Replace(">", "");
                add1 = add1.Replace(";", "");
                add1 = add1.Replace(")", "");
                add1 = add1.Replace("(", "");
                add1 = add1.Replace("=", "");

                string add2 = txtadd2.Text;
                add2 = add2.Replace("'", "");
                add2 = add2.Replace("/", "");
                add2 = add2.Replace("\\", "");
                add2 = add2.Replace("+", "");
                add2 = add2.Replace("*", "");
                add2 = add2.Replace("#", "");
                add2 = add2.Replace("&", "");
                add2 = add2.Replace(".", "");
                add2 = add2.Replace("%", "");
                add2 = add2.Replace("<", "");
                add2 = add2.Replace(">", "");
                add2 = add2.Replace(";", "");
                add2 = add2.Replace(")", "");
                add2 = add2.Replace("(", "");
                add2 = add2.Replace("=", "");

                string iden = txtiden.Text;
                iden = iden.Replace("'", "");
                iden = iden.Replace("/", "");
                iden = iden.Replace("\\", "");
                iden = iden.Replace("+", "");
                iden = iden.Replace("*", "");
                iden = iden.Replace("#", "");
                iden = iden.Replace("&", "");
                iden = iden.Replace(".", "");
                iden = iden.Replace("%", "");
                iden = iden.Replace("<", "");
                iden = iden.Replace(">", "");
                iden = iden.Replace(";", "");
                iden = iden.Replace(")", "");
                iden = iden.Replace("(", "");
                iden = iden.Replace("=", "");


                //post check
                if (pgt.Checked == false && tgt.Checked == false && prt.Checked == false && princi.Checked == false && vcp.Checked == false && prtmusic.Checked == false && lib.Checked == false)
                {
                    lblMsg.Text = "Select At Least One Post To Apply.";
                    return;
                }
                if (pgt.Checked == true && ddlpgt.SelectedIndex==0)
                {
                    lblMsg.Text = "Select At Least One PGT Post To Apply.";
                    return;
                }
                if (tgt.Checked == true && ddltgt.SelectedIndex == 0)
                {
                    lblMsg.Text = "Select At Least One TGT Post To Apply.";
                    return;
                }

                //check dob
                DateTime dob;
                try
                {
                    dob = DateTime.ParseExact(datepicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dob > DateTime.ParseExact("30/09/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture))
                    {
                        lblMsg.Text = "Invalid Date of Birth.";
                        return;
                    }
                   
                    lbldob.Text = datepicker.Text;
                }
                catch(Exception ex)
                {
                    lblMsg.Text = "Invalid Date of Birth Format. Kindly Enter dd/MM/yyyy only.";
                    return;
                }
                //duplicacy check
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand(@"select count(*) from kv_cand_edit where cand_id=@cand_id ", con);
                cmd.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = hdcandid.Value;
              
                int count = (int)cmd.ExecuteScalar();

                if (count != 0)
                {
                    lblMsg.Text = "Duplicate Data Found.You Can Not Edit More Than Once";
                    return;
                }
                else
                {
                    //check essential qualification
                    if(ddless.SelectedValue=="N")
                    {
                        lblMsg.Text = "You Must Fulfill Essential Qualifications To Apply.";
                        return;
                    }
                    ////checkdate


                    //checkpreference
                    if (princi.Checked == false && vcp.Checked == false)
                    {
                        if (ddlcity1.SelectedIndex == ddlcity2.SelectedIndex)
                        {
                            lblMsg.Text = "Exam City Selection Should Be Unique";
                            return;
                        }
                        else if (ddlcity1.SelectedIndex == ddlcity3.SelectedIndex)
                        {
                            lblMsg.Text = "Exam City Selection Should Be Unique";
                            return;
                        }
                        else if (ddlcity1.SelectedIndex == ddlcity4.SelectedIndex)
                        {
                            lblMsg.Text = "Exam City Selection Should Be Unique";
                            return;
                        }
                        else if (ddlcity2.SelectedIndex == ddlcity3.SelectedIndex)
                        {
                            lblMsg.Text = "Exam City Selection Should Be Unique";
                            return;
                        }
                        else if (ddlcity2.SelectedIndex == ddlcity4.SelectedIndex)
                        {
                            lblMsg.Text = "Exam City Selection Should Be Unique";
                            return;
                        }
                        else if (ddlcity3.SelectedIndex == ddlcity4.SelectedIndex)
                        {
                            lblMsg.Text = "Exam City Selection Should Be Unique";
                            return;
                        }


                        //checkzone
                        if (ddlzone1.SelectedIndex == ddlzone2.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone1.SelectedIndex == ddlzone3.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone1.SelectedIndex == ddlzone4.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone1.SelectedIndex == ddlzone5.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone1.SelectedIndex == ddlzone6.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone2.SelectedIndex == ddlzone3.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone2.SelectedIndex == ddlzone4.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone2.SelectedIndex == ddlzone5.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone2.SelectedIndex == ddlzone6.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone3.SelectedIndex == ddlzone4.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone3.SelectedIndex == ddlzone5.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone3.SelectedIndex == ddlzone6.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone4.SelectedIndex == ddlzone5.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone4.SelectedIndex == ddlzone6.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }
                        else if (ddlzone5.SelectedIndex == ddlzone6.SelectedIndex)
                        {
                            lblMsg.Text = "Zone Selection Should Be Unique";
                            return;
                        }

                    }
                    
                    //check percentage obtained

                    if (float.Parse(txtperc10.Text) > 100.0 || float.Parse(txtperc12.Text) > 100.0)
                    {
                        lblMsg.Text = "Percentage Obtained Should Not Be More Than 100.";
                        return;
                    }

                    if (float.Parse(txtperc10.Text) < 0.0 || float.Parse(txtperc12.Text) < 0.0)
                    {
                        lblMsg.Text = "Percentage Obtained Should Not Be Less Than 0.";
                        return;
                    }
                    //age-relaxation
                    int agerelax = getagerelax();
                    //find cut-off birthday for post
                    string agestatus = getcutoffbday(agerelax);
                    if (agestatus != "") //problem with age
                    {
                        if (agestatus.Contains(",princi"))
                        {
                            lblMsg.Text = "Birthdate not valid for applying for Principal";
                            return;
                        }
                        if (agestatus.Contains(",vcp"))
                        {
                            lblMsg.Text = "Birthdate not valid for applying for Vice Principal";
                            return;
                        }
                        if (agestatus.Contains(",pgt"))
                        {
                            lblMsg.Text = "Birthdate not valid for applying for " + ddlpgt.SelectedItem.ToString() + "";
                            return;
                        }
                        if (agestatus.Contains(",tgt"))
                        {
                            lblMsg.Text = "Birthdate not valid for applying for " + ddltgt.SelectedItem.ToString() + "";
                            return;
                        }
                        if (agestatus.Contains(",prt"))
                        {
                            lblMsg.Text = "Birthdate not valid for applying for PRT";
                            return;
                        }
                        if (agestatus.Contains(",prtm"))
                        {
                            lblMsg.Text = "Birthdate not valid for applying for PRT-Music";
                            return;
                        }
                        if (agestatus.Contains(",lib"))
                        {
                            lblMsg.Text = "Birthdate not valid for applying for Librarian";
                            return;
                        }
                      
                    }

                    else //age is good to go
                    {
                        //check educational qualification
                        string isEduOk = checkedu();

                       
                        if (isEduOk == "")
                        {
                            lblMsg.Text = "";
                            //if everything correct

                            string isFeeExempt = getfeeexempt();
                            int fee = 0;
                            int oldfee = int.Parse(hdfee.Value);
                            if (isFeeExempt == "N")
                            {
                                fee = calcFee();
                                int feediff = oldfee - fee;
                                lblfeeapp.Text = "Fee Difference Applicable: Rupees " + feediff.ToString();
                            }
                            else
                            {
                                lblfeeapp.Text = "Fee Applicable: Rupees 0 (Exempted)";
                            }
                            lblfee.Text = "";
                            string essqual = "";
                            if (pgt.Checked == true)
                                essqual = rdblesspgt.SelectedValue;
                            if (tgt.Checked == true)
                                essqual = essqual + "," + rdblesstgt.SelectedValue;
                            if (prt.Checked == true)
                                essqual = essqual + "," + rdblprt.SelectedValue;
                            if (prtmusic.Checked == true)
                                essqual = essqual + "," + rdblprtm.SelectedValue;
                            if (lib.Checked == true)
                                essqual = essqual + "," + rdbllib.SelectedValue;
                            Session["essqual"] = essqual;
                            lblname.Text = txtcfname.Text.ToUpper() + " " + txtclname.Text.ToUpper();
                            lblmname.Text = txtmname.Text.ToUpper();
                            lblfname.Text = txtfname.Text.ToUpper();
                            lblmar.Text = ddlmar.SelectedItem.ToString().ToUpper();
                            lblgen.Text = ddlgen.SelectedItem.ToString().ToUpper();
                            lblcat.Text = ddlcat.SelectedItem.ToString().ToUpper();
                            lblpwd.Text = rdbpwd.SelectedItem.ToString().ToUpper();
                            lblpwdcat.Text = rdbpwd.SelectedIndex != 1 ? "PWD Category : " + ddlpwdcat.SelectedItem.ToString().ToUpper() : "";
                            lblscribe.Text = rdbpwd.SelectedIndex != 1 ? "Do you need a scribe? : " + rdbscribe.SelectedItem.ToString().ToUpper() : "";
                            lbljk.Text = rdbjk.SelectedItem.ToString().ToUpper();
                            lblcgemp.Text = rdbcgemp.SelectedItem.ToString().ToUpper();
                            lblcgservlen.Text = rdbcgemp.SelectedIndex != 1 ? "Length of service :" + txtyrreg.Text : "";
                            lblkvemp.Text = rdbemp.SelectedItem.ToString().ToUpper();
                            lblexserv.Text = rdbexserv.SelectedItem.ToString().ToUpper();
                            lblexservlen.Text = rdbexserv.SelectedIndex != 1 ? "Length of service :" + txtservlen.Text : "";
                            lbliden.Text = txtiden.Text.ToUpper();
                            lbladd.Text = txtadd1.Text.ToUpper() + " " + "," + " " + txtadd2.Text.ToUpper() + " " + "," + " " + ddlcity.SelectedItem.ToString().ToUpper() + " " + "," + " " + ddlstate.SelectedItem.ToString().ToUpper() + " " + "," + " " + "PIN - " + txtpin.Text;
                            lblemail.Text = txtemail.Text;
                            lblcon.Text = txtmob.Text + " " + "," + " " + txtmob2.Text;
                            lblrel.Text = ddlrel.SelectedItem.ToString().ToUpper();
                            lblpost.Text = "";
                            if (pgt.Checked == true && ddlpgt.SelectedIndex != 0)
                                lblpost.Text = "PGT-" + ddlpgt.SelectedItem.ToString();
                            if (tgt.Checked == true && ddltgt.SelectedIndex != 0)
                                lblpost.Text = lblpost.Text + " " + "TGT-" + ddltgt.SelectedItem.ToString();
                            if (prt.Checked == true)
                                lblpost.Text = lblpost.Text + " " + "PRT";
                            if (prtmusic.Checked == true)
                                lblpost.Text = lblpost.Text + " " + "PRT-MUSIC";
                            if (princi.Checked == true)
                                lblpost.Text = lblpost.Text + " " + "PRINCIPAL";
                            if (vcp.Checked == true)
                                lblpost.Text = lblpost.Text + " " + "VICE-PRINCIPAL";
                            if (lib.Checked == true)
                                lblpost.Text = lblpost.Text + " " + "LIBRARIAN";

                            if (ddless.SelectedValue == "Y")
                                lblqual.Text = "Yes";
                            else
                                lblqual.Text = "No";

                            lblcity1.Text = "Prefrence 1: " + ddlcity1.SelectedItem.ToString();
                            lblcity2.Text = "Prefrence 2: " + ddlcity2.SelectedItem.ToString();
                            lblcity3.Text = "Prefrence 3: " + ddlcity3.SelectedItem.ToString();
                            lblcity4.Text = "Prefrence 4: " + ddlcity4.SelectedItem.ToString();

                            lblmed.Text = ddlmed.SelectedItem.ToString();

                            lblyr10.Text = "Year: " + txtyr10.Text;
                            lbluni10.Text = "Board/University: " + txtuni10.Text.ToUpper();
                            lblperc10.Text = "Percentage Obtained: " + txtperc10.Text;

                            lblyr12.Text = "Year: " + txtyr12.Text;
                            lbluni12.Text = "Board/University: " + txtuni12.Text.ToUpper();
                            lblperc12.Text = "Percentage Obtained: " + txtperc12.Text;

                            lblyrdip.Text = "Year: " + txtyrdip.Text;
                            lblunidip.Text = "Board/University: " + txtunidip.Text.ToUpper();
                            lblpercdip.Text = "Percentage Obtained: " + txtpercdip.Text;

                            lblyrgrad.Text = "Year: " + txtyrgrad.Text;
                            lblunigrad.Text = "Board/University: " + txtunigrad.Text.ToUpper();
                            lblpercgrad.Text = "Percentage Obtained: " + txtpercgrad.Text;

                            lblyrpg.Text = "Year: " + txtyrpg.Text;
                            lblunipg.Text = "Board/University: " + txtunipg.Text.ToUpper();
                            lblpercpg.Text = "Percentage Obtained: " + txtpercpg.Text;

                            lblyrded.Text = "Year: " + txtyrded.Text;
                            lblunided.Text = "Board/University: " + txtunided.Text.ToUpper();
                            lblpercded.Text = "Percentage Obtained: " + txtpercded.Text;

                            lblyrbed.Text = "Year: " + txtyrbed.Text;
                            lblunibed.Text = "Board/University: " + txtunibed.Text.ToUpper();
                            lblpercbed.Text = "Percentage Obtained: " + txtpercbed.Text;

                            lblyrctet.Text = "Year: " + txtyrctet.Text;

                            lblpercctet.Text = "Marks Obtained: " + txtpercctet.Text;

                            lblyrctet2.Text = "Year: " + txtyrctet2.Text;

                            lblpercctet2.Text = "Marks Obtained: " + txtpercctet2.Text;

                 
                            ModalPopupExtender2.Show();
                        }
                        else//problem with data
                        {
                            return;
                        }

                    }


                }
            }
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlcity.Items.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand Cmd = new SqlCommand(@"select * from districts where st_code='" + ddlstate.SelectedValue + "' order by dt_name", con);
            SqlDataReader DrTmp = Cmd.ExecuteReader();
            ddlcity.DataSource = DrTmp;
            ddlcity.DataTextField = "dt_name";
            ddlcity.DataValueField = "dt_name";
            ddlcity.DataBind();
            DrTmp.Close();
            con.Close();
            con.Dispose();
        }
        protected void getdistt()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand Cmd = new SqlCommand(@"select * from districts where st_code='" + ddlstate.SelectedValue + "' order by dt_name", con);
            SqlDataReader DrTmp = Cmd.ExecuteReader();
            ddlcity.DataSource = DrTmp;
            ddlcity.DataTextField = "dt_name";
            ddlcity.DataValueField = "dt_name";
            ddlcity.DataBind();
            DrTmp.Close();
            con.Close();
            con.Dispose();
        }
        protected void rdbpwd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rdbpwd.SelectedValue=="N")
            {
                dvpwdcat.Visible = false;
                reqpwdcat.Enabled = false;
            }
            else
            {
                dvpwdcat.Visible = true;
                reqpwdcat.Enabled = true;
            }
        }

        protected void rdbcgemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbcgemp.SelectedValue == "N")
            {
                divcgemp.Visible = false;
                reqyrreg.Enabled = false;
            }
            else
            {
                divcgemp.Visible = true;
                reqyrreg.Enabled = true;
            }
        }

        protected void btncont2_Click(object sender, EventArgs e)
        {
            dvcand.Visible = false;
            dvpostexam.Visible = false;
            dvedu.Visible = true;
        }

        protected void btncont3_Click(object sender, EventArgs e)
        {

        }

        protected void rdbexserv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbexserv.SelectedValue == "N")
            {
                divexserv.Visible = false;
                reqservlen.Enabled = false;
            }
            else
            {
                divexserv.Visible = true;
                reqservlen.Enabled = true;
            }
        }

        protected void btnback1_Click(object sender, EventArgs e)
        {
            dvcand.Visible = true;
            dvpostexam.Visible = false;
            dvedu.Visible = false;
        }

        protected void btnback2_Click(object sender, EventArgs e)
        {
            dvcand.Visible = false;
            dvpostexam.Visible = true;
            dvedu.Visible = false;
        }

        protected void pgt_CheckedChanged(object sender, EventArgs e)
        {
          
        }
        protected void getpgtsub()
        {
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand Cmd = new SqlCommand(@"select * from kv_pgt_sub order by sub", con);
            SqlDataReader DrTmp = Cmd.ExecuteReader();
            ddlpgt.DataSource = DrTmp;
            ddlpgt.DataTextField = "subname";
            ddlpgt.DataValueField = "sub";
            ddlpgt.DataBind();
            DrTmp.Close();
            con.Close();
            con.Dispose();
        }
        protected void gettgtsub()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand Cmd = new SqlCommand(@"select * from kv_tgt_sub order by sub", con);
            SqlDataReader DrTmp = Cmd.ExecuteReader();
            ddltgt.DataSource = DrTmp;
            ddltgt.DataTextField = "subname";
            ddltgt.DataValueField = "sub";
            ddltgt.DataBind();
            DrTmp.Close();
            con.Close();
            con.Dispose();
        }
        protected void getgrad()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand Cmd = new SqlCommand(@"select * from kv_grad order by deg", con);
            SqlDataReader DrTmp = Cmd.ExecuteReader();
            ddlgrad.DataSource = DrTmp;
            ddlgrad.DataTextField = "degname";
            ddlgrad.DataValueField = "deg";
            ddlgrad.DataBind();
            DrTmp.Close();
            con.Close();
            con.Dispose();
        }
        protected void getpostgrad()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            SqlCommand Cmd = new SqlCommand(@"select * from kv_postgrad order by deg", con);
            SqlDataReader DrTmp = Cmd.ExecuteReader();
            ddlpostgrad.DataSource = DrTmp;
            ddlpostgrad.DataTextField = "degname";
            ddlpostgrad.DataValueField = "deg";
            ddlpostgrad.DataBind();
            DrTmp.Close();
            con.Close();
            con.Dispose();
        }

       
        protected void ddlpgt_SelectedIndexChanged(object sender, EventArgs e)
        {

            spnesspgt.Visible = true;
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            if (ddlpgt.SelectedValue != "11")
            {
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='PGT' and sub is null", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdblesspgt.DataSource = DrTmp;
                rdblesspgt.DataTextField = "qual";
                rdblesspgt.DataValueField = "qual";
                rdblesspgt.DataBind();
                DrTmp.Close();
            }
            else
            {
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='PGT' and sub='COMP'", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdblesspgt.DataSource = DrTmp;
                rdblesspgt.DataTextField = "qual";
                rdblesspgt.DataValueField = "qual";
                rdblesspgt.DataBind();
                DrTmp.Close();
            }
            rdblesspgt.SelectedIndex = 0;
            con.Close();
            con.Dispose();

        }

        protected void ddltgt_SelectedIndexChanged(object sender, EventArgs e)
        {
            spnesstgt.Visible = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            if (ddltgt.SelectedValue == "07")
            {
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='TGT' and sub='PE'", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdblesstgt.DataSource = DrTmp;
                rdblesstgt.DataTextField = "qual";
                rdblesstgt.DataValueField = "qual";
                rdblesstgt.DataBind();
                DrTmp.Close();
            }
            else if (ddltgt.SelectedValue == "08")
            {
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='TGT' and sub='AE'", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdblesstgt.DataSource = DrTmp;
                rdblesstgt.DataTextField = "qual";
                rdblesstgt.DataValueField = "qual";
                rdblesstgt.DataBind();
                DrTmp.Close();
            }
            else if (ddltgt.SelectedValue == "09")
            {
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='TGT' and sub='WE'", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdblesstgt.DataSource = DrTmp;
                rdblesstgt.DataTextField = "qual";
                rdblesstgt.DataValueField = "qual";
                rdblesstgt.DataBind();
                DrTmp.Close();
            }
            else
            {
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='TGT' and sub is null", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdblesstgt.DataSource = DrTmp;
                rdblesstgt.DataTextField = "qual";
                rdblesstgt.DataValueField = "qual";
                rdblesstgt.DataBind();
                DrTmp.Close();
            }
            rdblesstgt.SelectedIndex = 0;
            con.Close();
            con.Dispose();
        }

        protected void pgt_CheckedChanged1(object sender, EventArgs e)
        {

            if(pgt.Checked==true)
            {
                dvpgt.Visible = true;
                reqpgt.Enabled = true;

                ddlzone1.Enabled = true;
                ddlzone2.Enabled = true;
                ddlzone3.Enabled = true;
                ddlzone4.Enabled = true;
                ddlzone5.Enabled = true;
                ddlzone6.Enabled = true;

                ddlcity1.Enabled = true;
                ddlcity2.Enabled = true;
                ddlcity3.Enabled = true;
                ddlcity4.Enabled = true;

                reqcity1.Enabled = true;
                reqcity2.Enabled = true;
                reqcity3.Enabled = true;
                reqcity4.Enabled = true;

                reqzone1.Enabled = true;
                reqzone2.Enabled = true;
                reqzone3.Enabled = true;
                reqzone4.Enabled = true;
                reqzone5.Enabled = true;
                reqzone6.Enabled = true;
            }
            else
            {
                dvpgt.Visible = false;
                reqpgt.Enabled = false;
            }
            
        }

        protected void tgt_CheckedChanged(object sender, EventArgs e)
        {
            if (tgt.Checked == true)
            {
                dvtgt.Visible = true;
                reqtgt.Enabled = true;

                ddlzone1.Enabled = true;
                ddlzone2.Enabled = true;
                ddlzone3.Enabled = true;
                ddlzone4.Enabled = true;
                ddlzone5.Enabled = true;
                ddlzone6.Enabled = true;

                ddlcity1.Enabled = true;
                ddlcity2.Enabled = true;
                ddlcity3.Enabled = true;
                ddlcity4.Enabled = true;

                reqcity1.Enabled = true;
                reqcity2.Enabled = true;
                reqcity3.Enabled = true;
                reqcity4.Enabled = true;

                reqzone1.Enabled = true;
                reqzone2.Enabled = true;
                reqzone3.Enabled = true;
                reqzone4.Enabled = true;
                reqzone5.Enabled = true;
                reqzone6.Enabled = true;
            }
            else
            {
                dvtgt.Visible = false;
                reqtgt.Enabled = false;
            }
        }

        protected void ddlmar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlmar.SelectedValue == "M" && ddlgen.SelectedValue == "F")
                dvnewname.Visible = true;
            else
                dvnewname.Visible = false;         

        }

        protected void prtm_CheckedChanged(object sender, EventArgs e)
        {
            if (prtmusic.Checked == true)
            {
                dvprtm.Visible = true;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                con.Open();
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='PRTM'", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdblprtm.DataSource = DrTmp;
                rdblprtm.DataTextField = "qual";
                rdblprtm.DataValueField = "qual";
                rdblprtm.DataBind();
                rdblprtm.SelectedIndex = 0;
                DrTmp.Close();
                DrTmp.Dispose();
                con.Close();
                con.Dispose();

                ddlzone1.Enabled = true;
                ddlzone2.Enabled = true;
                ddlzone3.Enabled = true;
                ddlzone4.Enabled = true;
                ddlzone5.Enabled = true;
                ddlzone6.Enabled = true;

                ddlcity1.Enabled = true;
                ddlcity2.Enabled = true;
                ddlcity3.Enabled = true;
                ddlcity4.Enabled = true;

                reqcity1.Enabled = true;
                reqcity2.Enabled = true;
                reqcity3.Enabled = true;
                reqcity4.Enabled = true;

                reqzone1.Enabled = true;
                reqzone2.Enabled = true;
                reqzone3.Enabled = true;
                reqzone4.Enabled = true;
                reqzone5.Enabled = true;
                reqzone6.Enabled = true;
            }
            else
            {
                dvprtm.Visible = false;
            }
        }

        protected void prt_CheckedChanged(object sender, EventArgs e)
        {
            if (prt.Checked == true)
            {
                dvprt.Visible = true;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                con.Open();
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='PRT'", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdblprt.DataSource = DrTmp;
                rdblprt.DataTextField = "qual";
                rdblprt.DataValueField = "qual";
                rdblprt.DataBind();
                rdblprt.SelectedIndex = 0;
                DrTmp.Close();
                DrTmp.Dispose();
                con.Close();
                con.Dispose();

                ddlzone1.Enabled = true;
                ddlzone2.Enabled = true;
                ddlzone3.Enabled = true;
                ddlzone4.Enabled = true;
                ddlzone5.Enabled = true;
                ddlzone6.Enabled = true;

                ddlcity1.Enabled = true;
                ddlcity2.Enabled = true;
                ddlcity3.Enabled = true;
                ddlcity4.Enabled = true;

                reqcity1.Enabled = true;
                reqcity2.Enabled = true;
                reqcity3.Enabled = true;
                reqcity4.Enabled = true;

                reqzone1.Enabled = true;
                reqzone2.Enabled = true;
                reqzone3.Enabled = true;
                reqzone4.Enabled = true;
                reqzone5.Enabled = true;
                reqzone6.Enabled = true;

            }
            else
            {
                dvprt.Visible = false;
            }
        }

        protected void lib_CheckedChanged(object sender, EventArgs e)
        {
            if (lib.Checked == true)
            {
                dvlib.Visible = true;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                con.Open();
                SqlCommand Cmd = new SqlCommand(@"select * from kv_ess where post='LIB'", con);
                SqlDataReader DrTmp = Cmd.ExecuteReader();
                rdbllib.DataSource = DrTmp;
                rdbllib.DataTextField = "qual";
                rdbllib.DataValueField = "qual";
                rdbllib.DataBind();
                rdbllib.SelectedIndex = 0;
                DrTmp.Close();
                DrTmp.Dispose();
                con.Close();
                con.Dispose();

                ddlzone1.Enabled = true;
                ddlzone2.Enabled = true;
                ddlzone3.Enabled = true;
                ddlzone4.Enabled = true;
                ddlzone5.Enabled = true;
                ddlzone6.Enabled = true;

                ddlcity1.Enabled = true;
                ddlcity2.Enabled = true;
                ddlcity3.Enabled = true;
                ddlcity4.Enabled = true;

                reqcity1.Enabled = true;
                reqcity2.Enabled = true;
                reqcity3.Enabled = true;
                reqcity4.Enabled = true;

                reqzone1.Enabled = true;
                reqzone2.Enabled = true;
                reqzone3.Enabled = true;
                reqzone4.Enabled = true;
                reqzone5.Enabled = true;
                reqzone6.Enabled = true;
            }
            else
            {
                dvlib.Visible = false;
            }
        }

        protected void princi_CheckedChanged(object sender, EventArgs e)
        {
            if (princi.Checked == true)
            {
                if (pgt.Checked == false && tgt.Checked == false && prt.Checked == false && prtmusic.Checked == false && lib.Checked == false)
                {
                    ddlzone1.Enabled = false;
                    ddlzone2.Enabled = false;
                    ddlzone3.Enabled = false;
                    ddlzone4.Enabled = false;
                    ddlzone5.Enabled = false;
                    ddlzone6.Enabled = false;

                    ddlcity1.Enabled = false;
                    ddlcity2.Enabled = false;
                    ddlcity3.Enabled = false;
                    ddlcity4.Enabled = false;

                    reqcity1.Enabled = false;
                    reqcity2.Enabled = false;
                    reqcity3.Enabled = false;
                    reqcity4.Enabled = false;
                    reqzone1.Enabled = false;
                    reqzone2.Enabled = false;
                    reqzone3.Enabled = false;
                    reqzone4.Enabled = false;
                    reqzone5.Enabled = false;
                    reqzone6.Enabled = false;
                }

            }
            else
            {
                ddlzone1.Enabled = true;
                ddlzone2.Enabled = true;
                ddlzone3.Enabled = true;
                ddlzone4.Enabled = true;
                ddlzone5.Enabled = true;
                ddlzone6.Enabled = true;

                ddlcity1.Enabled = true;
                ddlcity2.Enabled = true;
                ddlcity3.Enabled = true;
                ddlcity4.Enabled = true;

                reqcity1.Enabled = true;
                reqcity2.Enabled = true;
                reqcity3.Enabled = true;
                reqcity4.Enabled = true;


                reqzone1.Enabled = true;
                reqzone2.Enabled = true;
                reqzone3.Enabled = true;
                reqzone4.Enabled = true;
                reqzone5.Enabled = true;
                reqzone6.Enabled = true;
            }
        }

        protected void vcp_CheckedChanged(object sender, EventArgs e)
        {
            if (vcp.Checked == true)
            {
                if (pgt.Checked == false && tgt.Checked == false && prt.Checked == false && prtmusic.Checked == false && lib.Checked == false)
                {
                    ddlzone1.Enabled = false;
                    ddlzone2.Enabled = false;
                    ddlzone3.Enabled = false;
                    ddlzone4.Enabled = false;
                    ddlzone5.Enabled = false;
                    ddlzone6.Enabled = false;

                    ddlcity1.Enabled = false;
                    ddlcity2.Enabled = false;
                    ddlcity3.Enabled = false;
                    ddlcity4.Enabled = false;

                    reqcity1.Enabled = false;
                    reqcity2.Enabled = false;
                    reqcity3.Enabled = false;
                    reqcity4.Enabled = false;
                    reqzone1.Enabled = false;
                    reqzone2.Enabled = false;
                    reqzone3.Enabled = false;
                    reqzone4.Enabled = false;
                    reqzone5.Enabled = false;
                    reqzone6.Enabled = false;
                }
            }
            else
            {
                ddlzone1.Enabled = true;
                ddlzone2.Enabled = true;
                ddlzone3.Enabled = true;
                ddlzone4.Enabled = true;
                ddlzone5.Enabled = true;
                ddlzone6.Enabled = true;

                ddlcity1.Enabled = true;
                ddlcity2.Enabled = true;
                ddlcity3.Enabled = true;
                ddlcity4.Enabled = true;

                reqcity1.Enabled = true;
                reqcity2.Enabled = true;
                reqcity3.Enabled = true;
                reqcity4.Enabled = true;

                reqzone1.Enabled = true;
                reqzone2.Enabled = true;
                reqzone3.Enabled = true;
                reqzone4.Enabled = true;
                reqzone5.Enabled = true;
                reqzone6.Enabled = true;
            }
        }
    }
}