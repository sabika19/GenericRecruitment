using System;
using System.Collections;
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
    public partial class appconf : System.Web.UI.Page
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
                Response.Redirect("applogin.aspx", false);
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
            ClearCacheItems();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            lblnoctet.Text = "";
            if (Request.QueryString["pid"] == null && Session["candid"] == null)
            {
                Response.Redirect("applogin.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                if (Session["candid"] == null && Request.QueryString["pid"] != null)
                {
                    string pid = "", uid = "";
                    int proceed = 0;
                    pid = Request.QueryString["pid"];

                    if (pid.Length != 36)
                    {
                        Response.Redirect("applogin.aspx", false);
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

                        lblcandid.Text = Dr["au_user"].ToString();

                        Dr.Close();

                        string step3check = "";
                        SqlCommand cmdstep3 = new SqlCommand(@"select * from kv_status with (nolock) where cand_id=@cand_id", con);
                        cmdstep3.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblcandid.Text;
                        SqlDataReader sdrstep3 = cmdstep3.ExecuteReader();
                        if (@sdrstep3.Read())
                        {
                            step3check = sdrstep3["step3"].ToString();
                          
                        }
                        sdrstep3.Close();
                        sdrstep3.Dispose();
                        if (step3check == "Y")
                        {
                            string table_name = "kv_cand";
                           
                            SqlCommand cmdedit = new SqlCommand(@"select count(*) from kv_cand_edit where cand_id=@cand_id",con);
                            cmdedit.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblcandid.Text;
                            int countedit = (int)cmdedit.ExecuteScalar();
                            if (countedit != 0)
                                table_name = "kv_cand_edit";                            
                            SqlCommand cmd2 = new SqlCommand(@"select * from "+table_name+" where cand_id=@cand_id", con);
                            cmd2.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblcandid.Text;
                            SqlDataReader sdr = cmd2.ExecuteReader();
                            if (@sdr.Read())
                            {
                                lbldate.Text = DateTime.Now.ToShortDateString();
                                String datenow = DateTime.Parse(lbldate.Text).ToString("dd/MM/yyyy");
                                lbldate.Text = datenow;
                                lblrel.Text = sdr["religion"].ToString();
                                if (lblrel.Text == "H")
                                    lblrel.Text = "HINDU";
                                else if (lblrel.Text == "I")
                                    lblrel.Text = "MUSLIM";
                                else if (lblrel.Text == "C")
                                    lblrel.Text = "CHRISTIAN";
                                else if (lblrel.Text == "S")
                                    lblrel.Text = "SIKH";
                                else if (lblrel.Text == "J")
                                    lblrel.Text = "JAIN";
                                else if (lblrel.Text == "B")
                                    lblrel.Text = "BUDDH";
                                else if (lblrel.Text == "O")
                                    lblrel.Text = "OTHERS";


                                lblname.Text = sdr["cname_f"].ToString() + " " + sdr["cname_l"].ToString();
                                lblmname.Text = sdr["mname"].ToString();
                                lblfname.Text = sdr["fname"].ToString();
                                lblmar.Text = sdr["marital"].ToString() == "M" ? "MARRIED" : "SINGLE";
                                lblessqual.Text = sdr["essqual"].ToString();
                                lblgen.Text = sdr["gender"].ToString() == "F" ? "FEMALE" : sdr["gender"].ToString() == "M" ? "Male" : "OTHER";
                                lblmarname.Text = sdr["newname"].ToString() == "" ? "" : ",Married Name : " + sdr["newname"].ToString();
                                lblgovid.Text = sdr["idtype"].ToString();
                              
                                if (lblgovid.Text == "AA")
                                    lblgovid.Text = "AADHAAR";
                                else if (lblgovid.Text == "PS")
                                    lblgovid.Text = "PASSPORT";
                                else if (lblgovid.Text == "DL")
                                    lblgovid.Text = "DRIVING LICENCE";
                                else if (lblgovid.Text == "VI")
                                    lblgovid.Text = "VOTER ID";
                                else if (lblgovid.Text == "PN")
                                    lblgovid.Text = "PAN CARD";

                                lblgovidnum.Text = sdr["idnum"].ToString();
                               
                               lblgovidnum.Text = AESEncrytDecry.Decrypt(lblgovidnum.Text);
                                  //lblgovidnum.Text = AESEncrytDecry.DecryptStringAES(lblgovidnum.Text);
                                int length = lblgovidnum.Text.Length;
                                if (length > 5)
                                {
                                    lblgovidnum.Text = lblgovidnum.Text.Substring(length - 4, 4);
                                    for (int i = length - 5; i >= 0; i--)
                                        lblgovidnum.Text = "*" + lblgovidnum.Text;
                                }
                             
                                lblcat.Text = sdr["cat"].ToString();
                                if (lblcat.Text == "G")
                                    lblcat.Text = "GENERAL";
                                else if (lblcat.Text == "O")
                                    lblcat.Text = "OTHER BACKWARD CLASSES(OBC- Central Govt. List)";
                                else if (lblcat.Text == "C")
                                    lblcat.Text = "SCHEDULED CASTE(SC)";
                                else if (lblcat.Text == "T")
                                    lblcat.Text = "SCHEDULED TRIBE(ST)";

                                lblpwd.Text = sdr["pwd"].ToString();
                                if (lblpwd.Text == "")
                                {
                                    lblpwd.Text = "NO";
                                }
                                else
                                {
                                    if (lblpwd.Text == "V")
                                    {
                                        lblpwd.Text = "YES";
                                        lblpwdcat.Text = "PWD Category : Visually Impaired";
                                        lblpwdcat.Text = lblpwdcat.Text.ToUpper();
                                    }
                                    else if (lblpwd.Text == "O")
                                    {
                                        lblpwd.Text = "YES";
                                        lblpwdcat.Text = "PWD Category : Orthopedic Impaired";
                                        lblpwdcat.Text = lblpwdcat.Text.ToUpper();
                                    }
                                    else if (lblpwd.Text == "H")
                                    {
                                        lblpwd.Text = "YES";
                                        lblpwdcat.Text = "PWD Category : Hearing Impaired";
                                        lblpwdcat.Text = lblpwdcat.Text.ToUpper();
                                    }
                                    else
                                    {
                                        lblpwd.Text = "YES";
                                        lblpwdcat.Text = "PWD Category :Others";
                                        lblpwdcat.Text = lblpwdcat.Text.ToUpper();
                                    }

                                }

                                lblscribe.Text = sdr["scribe"].ToString();
                                if (lblscribe.Text == "" | lblscribe.Text == "N")
                                    lblscribe.Text = "NO";
                                else
                                    lblscribe.Text = "YES";


                                lblkvemp.Text = sdr["kvs"].ToString() == "N" ? "NO" : "YES";

                                lblcgemp.Text = sdr["cg"].ToString() == "N" ? "NO" : "YES";

                                //string transidToShow = "N";
                                //if(sdr["fee_ex"].ToString()=="N")
                                //{
                                //    lblfee.Text= "0/- (EXEMPTED)";
                                //}
                                //else
                                //{
                                //    transidToShow = "Y";
                                //    lblfee.Text = sdr["fee"].ToString() + "/- (PAID)";
                                //}
                                lblfee.Text = sdr["fee_ex"].ToString() == "N" ? sdr["fee"].ToString() + "/- (PAID)" : "0/- (EXEMPTED)";

                                if (lblcgemp.Text == "YES")
                                {
                                    lblcgservlen.Text = "Length of service :" + sdr["cg_len"].ToString() + " Years";
                                    lblcgservlen.Text = lblcgservlen.Text.ToUpper();
                                }

                                lbljk.Text = sdr["jk"].ToString() == "N" ? "NO" : "YES";
                                lblexserv.Text = sdr["exserv"].ToString() == "N" ? "NO" : "YES";
                                if (lblexserv.Text == "YES")
                                {
                                    lblexservlen.Text = "Length of service :" + sdr["ex_len"].ToString() + " Years";
                                    lblexservlen.Text = lblexservlen.Text.ToUpper();
                                }


                                lbliden.Text = sdr["iden_mark"].ToString();

                                lbldob.Text = sdr["dob"].ToString();
                                // DateTime dob = DateTime.Parse(lbldob.Text);
                                string dateshort = lbldob.Text;
                                String dob = DateTime.Parse(dateshort).ToString("dd/MM/yyyy");
                                lbldob.Text = dob.ToString();

                                string distt = sdr["city"].ToString();
                                string state = sdr["state"].ToString();
                                string add1 = sdr["add1"].ToString();
                                string add2 = sdr["add2"].ToString();
                                string pin = sdr["pin"].ToString();
                                lblemail.Text = sdr["email"].ToString();
                                lblcon.Text = sdr["mobile1"].ToString() + " " + "," + " " + sdr["mobile2"].ToString();
                                lbladd.Text = add1 + " " + "," + " " + add2 + " " + "," + " " + distt + " " + "," + " " + state + " " + "," + " " + "PIN - " + pin;



                                lblmed.Text = sdr["med"].ToString() == "1" ? "English" : sdr["med"].ToString() == "2" ? "Hindi" : "Bilingual";

                                lblyr10.Text = "Year: " + sdr["yr_10"].ToString();
                                lbluni10.Text = "Board/University: " + sdr["uni_10"].ToString();
                                lblperc10.Text = "Percentage Obtained: " + sdr["perc_10"].ToString();

                                lblyr12.Text = "Year: " + sdr["yr_12"].ToString();
                                lbluni12.Text = "Board/University: " + sdr["uni_12"].ToString();
                                lblperc12.Text = "Percentage Obtained: " + sdr["perc_12"].ToString();

                                if (sdr["yr_dip"].ToString() != "")
                                {
                                    lblyrdip.Text = "Year: " + sdr["yr_dip"].ToString();
                                    lblunidip.Text = "Board/University: " + sdr["uni_dip"].ToString();
                                    lblpercdip.Text = "Percentage Obtained: " + sdr["perc_dip"].ToString();
                                }
                                else
                                {
                                    lblyrdip.Text = "--";
                                }


                                if (sdr["yr_ded"].ToString() != "")
                                {
                                    lblyrded.Text = "Year: " + sdr["yr_ded"].ToString();
                                    lblunided.Text = "Board/University: " + sdr["uni_ded"].ToString();
                                    lblpercded.Text = "Percentage Obtained: " + sdr["perc_ded"].ToString();
                                }
                                else
                                {
                                    lblyrded.Text = "--";
                                }
                                if (sdr["yr_bed"].ToString() != "")
                                {
                                    lblyrbed.Text = "Year: " + sdr["yr_bed"].ToString();
                                    lblunibed.Text = "Board/University: " + sdr["uni_bed"].ToString();
                                    lblpercbed.Text = "Percentage Obtained: " + sdr["perc_bed"].ToString();
                                }
                                else
                                {
                                    lblyrbed.Text = "--";
                                }
                                if (sdr["yr_ctet"].ToString() != "" && sdr["yr_ctet2"].ToString() != "")
                                {
                                    lblnoctet.Text = "";
                                    ctet1.Attributes.Add("style", "display:block");
                                    lblyrctet.Text = "Year: " + sdr["yr_ctet"].ToString();

                                    lblpercctet.Text = "Marks Obtained: " + sdr["perc_ctet"].ToString();

                                    ctet2.Attributes.Add("style", "display:block");
                                    lblyrctet2.Text = "Year: " + sdr["yr_ctet2"].ToString();

                                    lblpercctet2.Text = "Marks Obtained: " + sdr["perc_ctet2"].ToString();
                                }
                                else if (sdr["yr_ctet"].ToString() != "" && sdr["yr_ctet2"].ToString() == "")
                                {
                                    lblnoctet.Text = "";
                                    ctet1.Attributes.Add("style", "display:block");
                                    lblyrctet.Text = "Year: " + sdr["yr_ctet"].ToString();

                                    lblpercctet.Text = "Marks Obtained: " + sdr["perc_ctet"].ToString();

                                    ctet2.Attributes.Add("style", "display:none");
                                }
                                else if (sdr["yr_ctet"].ToString() == "" && sdr["yr_ctet2"].ToString() != "")
                                {
                                    lblnoctet.Text = "";
                                    ctet1.Attributes.Add("style", "display:none");
                                    lblyrctet2.Text = "Year: " + sdr["yr_ctet2"].ToString();

                                    lblpercctet2.Text = "Marks Obtained: " + sdr["perc_ctet2"].ToString();

                                    ctet2.Attributes.Add("style", "display:block");
                                }

                                else if (sdr["yr_ctet"].ToString() == "" && sdr["yr_ctet2"].ToString() == "")
                                {
                                    lblnoctet.Text = "--";
                                    ctet1.Attributes.Add("style", "display:none");
                                   

                                    ctet2.Attributes.Add("style", "display:none");
                                }

                                if (sdr["yr_grad"].ToString() != "")
                                {
                                    lblgradsub.Text = sdr["sub_grad"].ToString();
                                    lblyrgrad.Text = "Year: " + sdr["yr_grad"].ToString();
                                    lblunigrad.Text = "Board/University: " + sdr["uni_grad"].ToString();
                                    lblpercgrad.Text = "Percentage Obtained: " + sdr["perc_grad"].ToString();
                                }
                                else
                                {
                                    lblyrgrad.Text = "--";
                                }
                                if (sdr["yr_pg"].ToString() != "")
                                {
                                    lblsubpg.Text = sdr["sub_pg"].ToString();
                                    lblyrpg.Text = "Year: " + sdr["yr_pg"].ToString();
                                    lblunipg.Text = "Board/University: " + sdr["uni_pg"].ToString();
                                    lblpercpg.Text = "Percentage Obtained: " + sdr["perc_pg"].ToString();
                                }
                                else
                                {
                                    lblyrpg.Text = "--";
                                }




                                string postpgt = "";
                                string posttgt = "";
                                string postprt = "";
                                string postprtm = "";
                                string postlib = "";
                                string postprinci = "";
                                string postvcp = "";


                                if (sdr["post_pgt"].ToString() != "")
                                {
                                    postpgt = sdr["post_pgt"].ToString();
                                }

                                if (sdr["post_tgt"].ToString() != "")
                                    posttgt = sdr["post_tgt"].ToString();
                                if (sdr["post_prt"].ToString() != "")
                                    postprt = "PRT";
                                if (sdr["post_prtm"].ToString() != "")
                                    postprtm = "PRT-MUSIC";
                                if (sdr["post_lib"].ToString() != "")
                                    postlib = "LIB";
                                if (sdr["post_princi"].ToString() != "")
                                    postprinci = "PRINCI";
                                if (sdr["post_vcp"].ToString() != "")
                                    postvcp = "VCP";



                                lblqual.Text = sdr["ess_qual"].ToString() == "N" ? "NO" : "YES";
                                lbldes.Text = sdr["des_qual"].ToString() == "N" ? "NO" : "YES";


                                lblcity1.Text = sdr["exam_city1"].ToString();
                                lblcity2.Text = sdr["exam_city2"].ToString();
                                lblcity3.Text = sdr["exam_city3"].ToString();
                                lblcity4.Text = sdr["exam_city4"].ToString();


                               
                                string zone1 = string.Empty;
                                string zone2 = string.Empty;
                                string zone3 = string.Empty;
                                string zone4 = string.Empty;
                                string zone5 = string.Empty;
                                string zone6 = string.Empty;
                                if (sdr["zone1"].ToString() == "C")
                                    zone1 = "Central Zone";
                                if (sdr["zone1"].ToString() == "E")
                                    zone1 = "East Zone";
                                if (sdr["zone1"].ToString() == "NE")
                                    zone1 = "North Eastern Zone";
                                if (sdr["zone1"].ToString() == "W")
                                    zone1 = "West Zone";
                                if (sdr["zone1"].ToString() == "S")
                                    zone1 = "South Zone";
                                if (sdr["zone1"].ToString() == "N")
                                    zone1 = "North Zone";


                                if (sdr["zone2"].ToString() == "C")
                                    zone2 = "Central Zone";
                                if (sdr["zone2"].ToString() == "E")
                                    zone2 = "East Zone";
                                if (sdr["zone2"].ToString() == "NE")
                                    zone2 = "North Eastern Zone";
                                if (sdr["zone2"].ToString() == "W")
                                    zone2 = "West Zone";
                                if (sdr["zone2"].ToString() == "S")
                                    zone2 = "South Zone";
                                if (sdr["zone2"].ToString() == "N")
                                    zone2 = "North Zone";

                                if (sdr["zone3"].ToString() == "C")
                                    zone3 = "Central Zone";
                                if (sdr["zone3"].ToString() == "E")
                                    zone3 = "East Zone";
                                if (sdr["zone3"].ToString() == "NE")
                                    zone3 = "North Eastern Zone";
                                if (sdr["zone3"].ToString() == "W")
                                    zone3 = "West Zone";
                                if (sdr["zone3"].ToString() == "S")
                                    zone3 = "South Zone";
                                if (sdr["zone3"].ToString() == "N")
                                    zone3 = "North Zone";

                                if (sdr["zone4"].ToString() == "C")
                                    zone4 = "Central Zone";
                                if (sdr["zone4"].ToString() == "E")
                                    zone4 = "East Zone";
                                if (sdr["zone4"].ToString() == "NE")
                                    zone4 = "North Eastern Zone";
                                if (sdr["zone4"].ToString() == "W")
                                    zone4 = "West Zone";
                                if (sdr["zone4"].ToString() == "S")
                                    zone4 = "South Zone";
                                if (sdr["zone4"].ToString() == "N")
                                    zone4 = "North Zone";

                                if (sdr["zone5"].ToString() == "C")
                                    zone5 = "Central Zone";
                                if (sdr["zone5"].ToString() == "E")
                                    zone5 = "East Zone";
                                if (sdr["zone5"].ToString() == "NE")
                                    zone5 = "North Eastern Zone";
                                if (sdr["zone5"].ToString() == "W")
                                    zone5 = "West Zone";
                                if (sdr["zone5"].ToString() == "S")
                                    zone5 = "South Zone";
                                if (sdr["zone5"].ToString() == "N")
                                    zone5 = "North Zone";


                                if (sdr["zone6"].ToString() == "C")
                                    zone6 = "Central Zone";
                                if (sdr["zone6"].ToString() == "E")
                                    zone6 = "East Zone";
                                if (sdr["zone6"].ToString() == "NE")
                                    zone6 = "North Eastern Zone";
                                if (sdr["zone6"].ToString() == "W")
                                    zone6 = "West Zone";
                                if (sdr["zone6"].ToString() == "S")
                                    zone6 = "South Zone";
                                if (sdr["zone6"].ToString() == "N")
                                    zone6 = "North Zone";





                                if (postprinci != "")
                                {
                                    lblzone1.Text = "NOT APPLICABLE";                                    
                                }
                                else if(postvcp!="")
                                {
                                    lblzone1.Text = "NOT APPLICABLE";
                                }
                                else
                                {
                                    lblzone1.Text = "Preference 1 : " + zone1;
                                    lblzone2.Text = "Preference 2 : " + zone2;
                                    lblzone3.Text = "Preference 3 : " + zone3;
                                    lblzone4.Text = "Preference 4 : " + zone4;
                                    lblzone5.Text = "Preference 5 : " + zone5;
                                    lblzone6.Text = "Preference 6 : " + zone6;
                                }
                                
                                sdr.Close();

                                SqlCommand cmd5 = new SqlCommand(@"select cen_name from kv_centres where cen_NO=@cen_NO", con);
                                cmd5.Parameters.AddWithValue("@cen_NO", SqlDbType.VarChar).Value = lblcity1.Text;
                                sdr = cmd5.ExecuteReader();
                                if (@sdr.Read())
                                {
                                    lblcity1.Text = "Preference 1: " + sdr["cen_name"].ToString();
                                }
                                sdr.Close();
                                SqlCommand cmd6 = new SqlCommand(@"select cen_name from kv_centres where cen_NO=@cen_NO", con);
                                cmd6.Parameters.AddWithValue("@cen_NO", SqlDbType.VarChar).Value = lblcity2.Text;
                                sdr = cmd6.ExecuteReader();
                                if (@sdr.Read())
                                {
                                    lblcity2.Text = "Preference 2: " + sdr["cen_name"].ToString();
                                }
                                sdr.Close();
                                SqlCommand cmd7 = new SqlCommand(@"select cen_name from kv_centres where cen_NO=@cen_NO", con);
                                cmd7.Parameters.AddWithValue("@cen_NO", SqlDbType.VarChar).Value = lblcity3.Text;
                                sdr = cmd7.ExecuteReader();
                                if (@sdr.Read())
                                {
                                    lblcity3.Text = "Preference 3: " + sdr["cen_name"].ToString();
                                }
                                sdr.Close();
                                SqlCommand cmd8 = new SqlCommand(@"select cen_name from kv_centres where cen_NO=@cen_NO", con);
                                cmd8.Parameters.AddWithValue("@cen_NO", SqlDbType.VarChar).Value = lblcity4.Text;
                                sdr = cmd8.ExecuteReader();
                                if (@sdr.Read())
                                {
                                    lblcity4.Text = "Preference 4: " + sdr["cen_name"].ToString();
                                }
                                sdr.Close();
                                if (lblgradsub.Text != "")
                                {
                                    SqlCommand cmd45 = new SqlCommand(@"select degname from kv_grad where deg=@deg", con);
                                    cmd45.Parameters.AddWithValue("@deg", SqlDbType.VarChar).Value = lblgradsub.Text;
                                    sdr = cmd45.ExecuteReader();
                                    if (@sdr.Read())
                                    {
                                        lblgradsub.Text = "Degree: " + sdr["degname"].ToString();
                                    }
                                }
                                sdr.Close();
                                if (lblsubpg.Text != "" && lblsubpg.Text != "0")
                                {
                                    SqlCommand cmd46 = new SqlCommand(@"select degname from kv_postgrad where deg=@deg", con);
                                    cmd46.Parameters.AddWithValue("@deg", SqlDbType.VarChar).Value = lblsubpg.Text;
                                    sdr = cmd46.ExecuteReader();
                                    if (@sdr.Read())
                                    {
                                        lblsubpg.Text = "Degree: " + sdr["degname"].ToString();
                                    }
                                }
                                sdr.Close();
                                if (postpgt != "")
                                {
                                    SqlCommand cmd47 = new SqlCommand(@"select subname from kv_pgt_sub where sub=@sub", con);
                                    cmd47.Parameters.AddWithValue("@sub", SqlDbType.VarChar).Value = postpgt;
                                    sdr = cmd47.ExecuteReader();
                                    if (@sdr.Read())
                                    {
                                        lblpost.Text = "PGT-" + sdr["subname"].ToString();
                                    }

                                }
                                sdr.Close();
                                if (posttgt != "")
                                {
                                    SqlCommand cmd48 = new SqlCommand(@"select subname from kv_tgt_sub where sub=@sub", con);
                                    cmd48.Parameters.AddWithValue("@sub", SqlDbType.VarChar).Value = posttgt;
                                    sdr = cmd48.ExecuteReader();
                                    if (@sdr.Read())
                                    {
                                        lblpost.Text = lblpost.Text + ", " + "TGT-" + sdr["subname"].ToString();
                                    }

                                }
                                if (postprt != "")
                                {

                                    lblpost.Text = lblpost.Text + ", " + "PRT";


                                }
                                if (postprtm != "")
                                {
                                    lblpost.Text = lblpost.Text + ", " + "PRT-MUSIC";

                                }
                                if (postlib != "")
                                {
                                    lblpost.Text = lblpost.Text + ", " + "LIBRARIAN";

                                }
                                if (postprinci != "")
                                    lblpost.Text = lblpost.Text + ", " + "PRINCIPAL";
                                if (postvcp != "")
                                    lblpost.Text = lblpost.Text + ", " + "VICE-PRINCIPAL";

                                sdr.Close();


                                SqlCommand cmd4 = new SqlCommand(@"select st_name from states where st_code=@st_code", con);
                                cmd4.Parameters.AddWithValue("@st_code", SqlDbType.VarChar).Value = state;
                                sdr = cmd4.ExecuteReader();
                                if (@sdr.Read())
                                {
                                    string st_name = sdr["st_name"].ToString();
                                    sdr.Close();
                                   
                                        lbladd.Text = add1 + " " + "," + " " + add2 + " " + "," + " " + distt + " " + "," + " " + st_name + " " + "," + " " + "PIN - " + pin;

                                   

                                }

                                sdr.Close();        
                                SqlCommand cmd3 = new SqlCommand(@"select * from kv_photo where cand_id=@cand_id", con);
                                cmd3.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblcandid.Text;
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
                                SqlCommand cmdphotoedit = new SqlCommand(@"select * from kv_photo_edit where cand_id=@cand_id", con);
                                cmdphotoedit.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblcandid.Text;
                                sdr = cmdphotoedit.ExecuteReader();
                                if (@sdr.Read())
                                {
                                    if (sdr["photo"].ToString() != "")
                                    {
                                        byte[] bytes = (byte[])sdr["photo"];
                                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                        candimg.ImageUrl = "data:image/png;base64," + base64String;
                                    }
                                    if (sdr["sign"].ToString() != "")
                                    {
                                        byte[] bytes2 = (byte[])sdr["sign"];
                                        string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                                        candsign.ImageUrl = "data:image/png;base64," + base64String2;
                                    }

                                }
                                sdr.Close();
                                SqlCommand cmdpayment = new SqlCommand(@"select * from kv_feelot where cand_id=@cand_id and [status]='SUCCESS'", con);
                                cmdpayment.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblcandid.Text;
                                sdr = cmdpayment.ExecuteReader();
                                if (@sdr.Read())
                                {

                                    lbltxnid.Text = "Transaction ID : "+sdr["txnid"].ToString();

                                }
                                sdr.Close();
                            }
                            else
                            {
                                Response.Redirect("applogin.aspx", false);
                                return;
                            }
                            con.Close();
                            con.Dispose();
                        }

                        else
                        {
                            Response.Redirect("applogin.aspx", false);
                            return;
                        }
                    }
                    else
                    {
                        Response.Redirect("applogin.aspx", false);
                        return;
                    }
                }
            }
        }
    }
}