
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
using Ionic.Zip;
//using Ionic.Zlib;

namespace kvs
{
    public partial class appdownload : System.Web.UI.Page
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

           // Response.Redirect("applogin.aspx", false);
        }
        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
        private void download(DataTable dt)
        {

            Byte[] bytes = (Byte[])dt.Rows[0]["Data"];
            string file_name = dt.Rows[0]["Name"].ToString();
            // File.WriteAllBytes("C:\\photos\\" + file_name + "", bytes);
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = dt.Rows[0]["ContentType"].ToString();
            Response.AddHeader("content-disposition", "attachment;filename="
            + dt.Rows[0]["Name"].ToString());
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();


        }

        private void downloadsign(DataTable dt)
        {

            Byte[] bytes = (Byte[])dt.Rows[0]["Data"];
            string file_name = dt.Rows[0]["Name"].ToString();
            // File.WriteAllBytes("C:\\photos\\" + file_name + "", bytes);
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = dt.Rows[0]["ContentType"].ToString();
            Response.AddHeader("content-disposition", "attachment;filename="
            + dt.Rows[0]["Name"].ToString());
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        protected void btnagree_Click(object sender, EventArgs e)
        {

            string folderName = Server.MapPath("~/") + "\\sign_lde\\";

            // Create a temp directory for the logged in user

            DirectoryInfo di = CreateUserDirectory(folderName);

            // Get images from database into a dataset on webserver

            DataSet ds = GetPhotosByAlbumID();

            // Store the images from dataset to a folder

            StoreImagesInFolder(ds, di.FullName);

            // Zip contents of the folder

            string fileName = ZipFolder(di.FullName);

            Download(fileName);

            di.Delete(true);



            //  //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            //  //con.Open();

            //  string strQuery = "select cand_id+'_photo.jpg' as Name,'image/jpeg' as ContentType,photo as Data from kv_photo";
            //  SqlCommand cmd = new SqlCommand(strQuery);
            ////  cmd.Parameters.Add("@cand_id", SqlDbType.Int).Value = 17000008;
            //  DataTable dt = GetData(cmd);
            //  if (dt != null)
            //  {

            //          download(dt);

            //  }


        }

        /* Create temporary User Directory to store images and zipped file. */

        public DirectoryInfo CreateUserDirectory(string directoryPath)
        {
            // Initialize userName by GUID

            string userName = System.Guid.NewGuid().ToString("N");

            // If user is registered, use the new userName else use GUID

            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                userName = User.Identity.Name;
            }

            string userDirectoryName = directoryPath + "\\" + userName;
            DirectoryInfo di;

            // Create a directory for the user

            if (!Directory.Exists(userDirectoryName))
            {
                di = System.IO.Directory.CreateDirectory(userDirectoryName);
                return di;
            }
            else
            {
                di = new DirectoryInfo(userDirectoryName);
            }
            return di;
        }
        public DataSet GetPhotosByAlbumID()
        {
            //Initialize SQL Server connection.

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                string strQuery = "select  distinct empcode,declr from kvs_lde where id>5000 and id<=7000 order by empcode";

                using (SqlCommand cmd = new SqlCommand(strQuery,connection))
                {
                    connection.Open();
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Photos");
                    return dataSet;
                }
            }
        }
        /* Store images in the folder */

        public void StoreImagesInFolder(DataSet ds, string folderName)
        {
            foreach (DataRow row in ds.Tables["Photos"].Rows)
            {
                // Get the image caption

                string FileName = row["empcode"].ToString();
                string filetype = row["declr"].ToString();
                FileStream outStream;
                //if (filetype.Contains("0xFF"))
                //{
                //    outStream = File.OpenWrite(folderName + "\\" + FileName + ".JPG");
                //}
                //else
                //{
                    outStream = File.OpenWrite(folderName + "\\" + FileName + ".JPG");
               // }

                //Get the original image data

                byte[] imageData = (byte[])row["declr"];

                //Read image data into a memory stream

                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.WriteTo(outStream);

                    outStream.Flush();
                    outStream.Close();

                }
            }
        }

        /* Zip the folder */

        private string ZipFolder(string folderName)
        {
            // This call is just to get the Album name, you can skip this and name it anything, like may be use GUID again

            //System.Collections.Generic.List<Album> list = PhotoManager.GetAlbumByAlbumID(albumID);

            // string albumName = list[0].Caption;
            string albumName = "kvs_lde_declr";

            //string fileName = folderName + "\\" + System.Guid.NewGuid().ToString("N".zip";
            string fileName = folderName + "\\" + albumName + ".zip";
            using (ZipFile zip = new ZipFile("lde_declr.zip"))
            {
                // add this file into the "images" directory in the zip archive

                zip.AddDirectory(folderName);
                zip.Save(fileName);
            }

            // Return zipped file name

            return fileName;
        }

        /* Download the zipped file */

        public void Download(string fileName)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(fileName);
            //-- if the file exists on the server
            //set appropriate headers
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.Close();
            }
            else
            {
                Response.Write("This file does not exist.");
            }
        }

        protected void btnagree1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ToString());
            con.Open();
            
            SqlCommand cmd = new SqlCommand(@"select rroll,ppath from  adm12all where rroll in select roll from hrm_list2",con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for(int i=0;i<dt.Rows.Count;i++)
            {
                string url = "http://59.179.16.89/cbse/2018/ex2011/" + dt.Rows[i][1].ToString();
                Byte[] bytes = (Byte[])dt.Rows[0]["Data"];
                string file_name = dt.Rows[0][0].ToString();
                // File.WriteAllBytes("C:\\photos\\" + file_name + "", bytes);
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = dt.Rows[0]["ContentType"].ToString();
                Response.AddHeader("content-disposition", "attachment;filename="
                + dt.Rows[0][0].ToString());
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
    }

    //protected void btnConf_Click(object sender, EventArgs e)
    //    {
    //        string strQuery = "select cand_id+'_sign.jpg' as Name,'image/jpeg' as ContentType,sign as Data from kv_photo";
    //        SqlCommand cmd = new SqlCommand(strQuery);
    //       // cmd.Parameters.Add("@cand_id", SqlDbType.Int).Value = 17000008;
    //        DataTable dt = GetData(cmd);
    //        if (dt != null)
    //        {
    //            downloadsign(dt);
    //        }
    //    }
}



              
    