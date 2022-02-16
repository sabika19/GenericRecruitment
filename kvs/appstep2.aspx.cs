
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
using System.Runtime.InteropServices;

namespace kvs
{
    public partial class appstep2 : System.Web.UI.Page
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

        protected int checkmime()
        {
            int retVal = 0;
            HttpPostedFile file_photo = upphoto.PostedFile;
            byte[] document_photo = new byte[file_photo.ContentLength];
            file_photo.InputStream.Read(document_photo, 0, file_photo.ContentLength);
            System.IntPtr mimetype_photo;
            FindMimeFromData(IntPtr.Zero, null, document_photo, 256, null, 0, out mimetype_photo, 0);
           // System.IntPtr mimeTypePtr_photo = new IntPtr(mimetype_photo);
            string mime_photo = Marshal.PtrToStringUni(mimetype_photo);
            Marshal.FreeCoTaskMem(mimetype_photo);

            HttpPostedFile file_sign = upsign.PostedFile;
            byte[] document_sign = new byte[file_sign.ContentLength];
            file_sign.InputStream.Read(document_sign, 0, file_sign.ContentLength);
            System.IntPtr mimetype_sign;
            FindMimeFromData(IntPtr.Zero, null, document_sign, 256, null, 0, out mimetype_sign, 0);
           // System.IntPtr mimeTypePtr_sign = new IntPtr(mimetype_sign);
            string mime_sign = Marshal.PtrToStringUni(mimetype_sign);
            Marshal.FreeCoTaskMem(mimetype_sign);

            if (mime_photo == "image/pjpeg" && mime_sign == "image/pjpeg")
            {
                retVal = 0;

            }
            else
            {
                retVal = 1;
            }
            return retVal;
        }

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
                Response.Redirect("applogin.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                if (DateTime.Now > Convert.ToDateTime(ConfigurationManager.AppSettings["lastphotodate"]))
                {
                    lblMsgOver.Text = "LAST DATE FOR PHOTO/SIGNATURE UPLOAD IS OVER!!";
                    tblphoto.Visible = false;
                    dvbutton.Visible = false;
                    return;
                }
                else
                {
                    tblphoto.Visible = true;
                    dvbutton.Visible = true;
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

                        btnConf.Visible = false;
                        btnStep3.Visible = false;
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

                            SqlCommand cmd2 = new SqlCommand(@"select * from kv_cand where cand_id=@cand_id and cand_id not in(select cand_id from kv_photo)", con);
                            cmd2.Parameters.AddWithValue("@cand_id", SqlDbType.VarChar).Value = lblregn.Text;

                            SqlDataReader sdr = cmd2.ExecuteReader();
                            if (@sdr.Read())
                            {
                                trphoto.Visible = true;
                                trsign.Visible = true;
                                btnagree.Visible = true;


                                lblcname.Text = sdr["cname_f"].ToString() + " " + sdr["cname_l"].ToString();
                                lblmname.Text = sdr["mname"].ToString();
                                lblfname.Text = sdr["fname"].ToString();
                                Session["fee_exemp"] = sdr["fee_ex"].ToString();

                                sdr.Close();

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
                        con.Close();
                        con.Dispose();

                    }
                }
            }

        }

        protected bool getMimeType()
        {
            bool isValid = false;
            try
            {
                string ext_photo = System.IO.Path.GetExtension(upphoto.PostedFile.FileName);
                string ext_sign = System.IO.Path.GetExtension(upsign.PostedFile.FileName);
                if (upphoto.PostedFile.ContentLength > (50 * 1024) || upphoto.PostedFile.ContentLength == 0 || ext_photo.ToUpper() != ".JPG" || upsign.PostedFile.ContentLength > (20 * 1024) || upsign.PostedFile.ContentLength == 0 || ext_sign.ToUpper() != ".JPG")
                {
                    isValid = false;
                }
                else
                {
                    //double extension
                    int count_photo = upphoto.FileName.Split('.').Length - 1;
                    int count_sign = upsign.FileName.Split('.').Length - 1;

                    if (count_photo > 1 || count_sign > 1)
                    {
                        isValid = false;
                    }
                    else
                    {
                        char[] headerphoto = new char[10];
                        StreamReader srphoto = new StreamReader(upphoto.PostedFile.InputStream);
                        srphoto.Read(headerphoto, 0, 10);
                        srphoto.Close();

                        char[] headersign = new char[10];
                        StreamReader srsign = new StreamReader(upsign.PostedFile.InputStream);
                        srsign.Read(headersign, 0, 10);
                        srsign.Close();

                        //char headerphotocheck = headerphoto[0];
                        //char headerphotocheck1 = headerphoto[1];
                        //// check if JPG
                        if ((headerphoto[0] == 0xFF && headerphoto[1] == 0xD8) || (headerphoto[6] == 'J' && headerphoto[7] == 'F' && headerphoto[8] == 'I' && headerphoto[9] == 'F') && (headersign[0] == 0xFF && headersign[1] == 0xD8) || (headersign[6] == 'J' && headersign[7] == 'F' && headersign[8] == 'I' && headersign[9] == 'F'))
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                }
            }
            catch
            {
                isValid = false;
            }
            //myStream.Read(Input, 0, fileLen);
            return isValid;
        }
        protected void btnagree_Click(object sender, EventArgs e)
        {
            if (upphoto.HasFile && upsign.HasFile)
            {

                //check MIME type of the files
                int checkmimeVal = 0;
                checkmimeVal = checkmime();

               // bool checkmimeVal = getMimeType();
                if(checkmimeVal==0)
                {
                  //  check file size and file format
                    string ext_photo = System.IO.Path.GetExtension(upphoto.PostedFile.FileName);
                    string ext_sign = System.IO.Path.GetExtension(upsign.PostedFile.FileName);
                    // check double extension
                    int count_photo = upphoto.FileName.Split('.').Length - 1;
                    int count_sign = upsign.FileName.Split('.').Length - 1;

                    if (count_photo > 1 || count_sign > 1)
                    {
                        lblMsg.Text = "INVALID FILE TYPE";
                        trphoto.Visible = true;
                        trsign.Visible = true;
                        dvbutton.Visible = true;
                        return;
                    }
                    if (upphoto.PostedFile.ContentLength > (50 * 1024) || upphoto.PostedFile.ContentLength == 0 || ext_photo.ToUpper() != ".JPG")
                    {
                        lblMsg.Text = "PLEASE UPLOAD PHOTOGRAPH WITH SIZE LESS THAN 50 KB IN JPG FORMAT ONLY";
                        trphoto.Visible = true;
                        trsign.Visible = true;
                        dvbutton.Visible = true;
                        return;
                    }
                    if (upsign.PostedFile.ContentLength > (20 * 1024) || upsign.PostedFile.ContentLength == 0 || ext_sign.ToUpper() != ".JPG")
                    {
                        lblMsg.Text = "PLEASE UPLOAD SIGNATURE WITH SIZE LESS THAN 20 KB IN JPG FORMAT ONLY";
                        trphoto.Visible = true;
                        trsign.Visible = true;
                        dvbutton.Visible = true;
                        return;
                    }

                    // reset input stream
                    upphoto.PostedFile.InputStream.Position = 0;
                    upsign.PostedFile.InputStream.Position = 0;
                    int length_photo = upphoto.PostedFile.ContentLength;
                byte[] imgbyte_photo = new byte[length_photo];
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
                        trphoto.Visible = true;
                        trsign.Visible = true;
                        dvbutton.Visible = true;
                        return;
                    }
                    string filename_photo = lblregn.Text + "_photo";

                int length_sign = upsign.PostedFile.ContentLength;
                byte[] imgbyte_sign = new byte[length_sign];
                HttpPostedFile img_sign = upsign.PostedFile;
                //set the binary data
                img_sign.InputStream.Read(imgbyte_sign, 0, length_sign);
                    // check first four bytes for JPG format

                    byte firstsi = imgbyte_photo[0];
                    byte secsi = imgbyte_photo[1];
                    byte thsi = imgbyte_photo[2];
                    byte fosi = imgbyte_photo[3];
                    if (firstsi != 255 || secsi != 216)
                    {
                        lblMsg.Text = "Invalid File Type For Signature.";
                        trphoto.Visible = true;
                        trsign.Visible = true;
                        dvbutton.Visible = true;
                        return;
                    }
                    string filename_sign = lblregn.Text + "_sign";

              
                    // upload the Files because files are valid
                   

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        //duplicacy check
                        SqlCommand Cmdcheck = new SqlCommand(@"select count(*) from kv_photo where cand_id=@candid", con, trans);
                        Cmdcheck.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = lblregn.Text;
                        int countrowsphoto = (int)Cmdcheck.ExecuteScalar();
                        if (countrowsphoto != 0)//if duplicate
                        {
                            lblMsg.Text = "Photo & signature already uploaded. Can not upload again.";
                            return;
                        }
                        else//all good
                        {
                            SqlCommand Cmd = new SqlCommand(@"insert into kv_photo(cand_id,photo,sign,ip,dateup)values(@candid,@photo,@sign,@ip,@dateup)", con, trans);
                            Cmd.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = lblregn.Text;
                            Cmd.Parameters.AddWithValue("@photo", SqlDbType.VarChar).Value = imgbyte_photo;
                            Cmd.Parameters.AddWithValue("@sign", SqlDbType.VarChar).Value = imgbyte_sign;
                            Cmd.Parameters.AddWithValue("@ip", SqlDbType.VarChar).Value = Request.ServerVariables["REMOTE_ADDR"];
                            Cmd.Parameters.AddWithValue("@dateup", SqlDbType.VarChar).Value = DateTime.Now;
                            Cmd.ExecuteNonQuery();



                            if (Session["fee_exemp"].ToString() == "Y")
                            {
                                SqlCommand Cmdstatusfee = new SqlCommand(@"update kv_status set step2=@step2,step3=@step3 where cand_id=@candid", con, trans);
                                Cmdstatusfee.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = lblregn.Text;
                                Cmdstatusfee.Parameters.AddWithValue("@step2", SqlDbType.VarChar).Value = "Y";
                                Cmdstatusfee.Parameters.AddWithValue("@step3", SqlDbType.VarChar).Value = "Y";
                                Cmdstatusfee.ExecuteNonQuery();
                                lblMsg.Text = "File Uploaded.Step 2 Completed Successfully.Kindly Print Confirmation Page As Your Fee is Exempted.";
                                trphoto.Visible = false;
                                trsign.Visible = false;
                                btnagree.Visible = false;
                                btnConf.Visible = true;
                                btnStep3.Visible = false;
                                trans.Commit();
                            }
                            else

                            {
                                SqlCommand Cmdstatus = new SqlCommand(@"update kv_status set step2=@step2 where cand_id=@candid", con, trans);
                                Cmdstatus.Parameters.AddWithValue("@candid", SqlDbType.VarChar).Value = lblregn.Text;
                                Cmdstatus.Parameters.AddWithValue("@step2", SqlDbType.VarChar).Value = "Y";
                                Cmdstatus.ExecuteNonQuery();
                                lblMsg.Text = "File Uploaded.Step 2 Completed Successfully.Please Go To Step 3 To Pay Fees.";
                                trphoto.Visible = false;
                                trsign.Visible = false;
                                btnagree.Visible = false;
                                btnStep3.Visible = true;
                                btnConf.Visible = false;
                                trans.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.ToString();
                        trans.Rollback();
                        trphoto.Visible = true;
                        trsign.Visible = true;
                        dvbutton.Visible = true;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
                else
                {
                    lblMsg.Text = "Malicious File Type.";
                    trphoto.Visible = true;
                    trsign.Visible = true;
                    dvbutton.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "Upload Both Photograph and Signature";
                trphoto.Visible = true;
                trsign.Visible = true;
                dvbutton.Visible = true;
            }

        }

        protected void btnStep3_Click(object sender, EventArgs e)
        {
                Response.Redirect("appstep3.aspx?pid=" + Request.QueryString["pid"] + "", false);
                return;
          
        }

        protected void btnConf_Click(object sender, EventArgs e)
        {
            Response.Redirect("appconf.aspx?pid=" + Request.QueryString["pid"] + "", false);
            return;
        }
    }
}


              
    