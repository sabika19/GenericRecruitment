using System;
using System.Xml;
using System.Drawing;
using System.Web.UI.WebControls;

namespace kvs.Components
{
    /// <summary>
    /// Summary description for Common.
    /// </summary>
    public class Common
    {
        public static bool isActiveClass(string cls)
        {
            bool retVal = true;
            if (cls == "09")
                if (System.Web.HttpContext.Current.Session["def09"].ToString() == "1")
                    retVal = false;

            if (cls == "11")
                if (System.Web.HttpContext.Current.Session["def11"].ToString() == "1")
                    retVal = false;
            
            return retVal;
        }

        public static string getPvtAppType(string apType)
        {
            if (apType == "A")
                apType = "Additional Subject";
            else if (apType == "C")
                apType = "Compartment";
            else if (apType == "I")
                apType = "Improvement";
            else if (apType == "F")
                apType = "Failure";
            else if (apType == "W")
                apType = "Female Fresh/PwD of Delhi";
            else if (apType == "H")
                apType = "PwD of Delhi";

            return apType;
        }

        public static string getCheckBoxValues(CheckBoxList chkBoxList)
        {
            string retVal = "";

            foreach (ListItem item in chkBoxList.Items)
            {
                if (item.Selected)
                {
                    retVal += "," + item.Value;
                }
            }

            if (retVal.Length > 0)
                retVal = retVal.Substring(1);

            return retVal;
        }

        public static string DMY(object dd)
        {
            int D, M;
            string DS, MS;
            if (IsDate(dd.ToString()))
            {
                DateTime dt = new DateTime();
                dt = DateTime.Parse(dd.ToString());

                D = dt.Day;
                M = dt.Month;
                if (D < 10)
                    DS = "0" + D.ToString();
                else
                    DS = D.ToString();

                if (M < 10)
                    MS = "0" + M.ToString();
                else
                    MS = M.ToString();

                return DS + "/" + MS + "/" + dt.Year.ToString();
            }
            else
                return dd.ToString();
        }

        public static object MDY(String dd)
        {
            String[] NewDt = new String[2];
            string s;

            if (dd != "")
            {
                NewDt = dd.Split('/');
                s = String.Format(NewDt[1], "00") + "/" + String.Format(NewDt[0], "00") + "/" + NewDt[2];
                if (IsDate(s))
                    return s;
                else
                    return ""; //System.DBNull;
            }
            else
                return System.DBNull.Value;
        }

        public static bool IsDate(string dt)
        {
            try
            {
                DateTime Dt = DateTime.Parse(dt);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool IsNumeric(string s)
        {
            try
            {
                Int32.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal static bool isNumeric(string numberString)
        {
            foreach (char c in numberString)
            {
                if (!char.IsNumber(c))
                    return false;
            }
            return true;
        }

        public static string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        public static string IntegerToWords(string inputNum)
        {
            int i;
            string retval = "";
            string[] ones ={
							  "",
							  "ONE",
							  "TWO",
							  "THREE",
							  "FOUR",
							  "FIVE",
							  "SIX",
							  "SEVEN",
							  "EIGHT",
							  "NINE",
							  "TEN",
							  "ELEVEN",
							  "TWELVE",
							  "THIRTEEN",
							  "FOURTEEN",
							  "FIFTEEN",
							  "SIXTEEN",
							  "SEVENTEEN",
							  "EIGHTEEN",
							  "NINETEEN"
						  };
            string[] tens ={
							  "",
							  "TEN",
							  "TWENTY",
							  "THIRTY",
							  "FORTY",
							  "FIFTY",
							  "SIXTY",
							  "SEVENTY",
							  "EIGHTY",
							  "NINETY"
						  };
            string[] thou ={
							  "",
							  "thousand",
							  "million",
							  "billion",
							  "trillion",
							  "quadrillion",
							  "quintillion"
						  };

            if (Convert.ToInt32(inputNum) == 0)
                return ("zero");

            string s = inputNum;

            string s1, s2, s3, s4;

            s1 = s.Substring(0, 2);
            s2 = s.Substring(2, 2);
            s3 = s.Substring(4, 1);
            s4 = s.Substring(5, 2);

            if (Convert.ToInt16(s1) > 0)
            {
                retval = tens[Convert.ToInt16(s1.Substring(0, 1))];
                retval = retval + " " + ones[Convert.ToInt16(s1.Substring(1, 1))] + " LAC ";
            }

            if (Convert.ToInt16(s2) > 0)
            {
                retval = retval + " " + tens[Convert.ToInt16(s2.Substring(0, 1))];
                retval = retval + " " + ones[Convert.ToInt16(s2.Substring(1, 1))] + " THOUSAND ";
            }

            if (Convert.ToInt16(s3) > 0)
            {
                retval = retval + " " + ones[Convert.ToInt16(s3.Substring(0, 1))] + " HUNDRED ";
            }

            i = Convert.ToInt16(s4);

            if (i > 0)
            {
                if (i > 19)
                {
                    retval = retval + " " + tens[Convert.ToInt16(s4.Substring(0, 1))];
                    retval = retval + " " + ones[Convert.ToInt16(s4.Substring(1, 1))];
                }
                else
                {
                    retval = retval + " " + ones[Convert.ToInt16(s4)];
                }
            }

            if (retval.Length > 0)
                retval += " ONLY";

            return (retval);
        }

        public static string IntegerToWords(long inputNum)
        {
            int i;
            string retval = "";
            string[] ones ={
							  "",
							  "ONE",
							  "TWO",
							  "THREE",
							  "FOUR",
							  "FIVE",
							  "SIX",
							  "SEVEN",
							  "EIGHT",
							  "NINE",
							  "TEN",
							  "ELEVEN",
							  "TWELVE",
							  "THIRTEEN",
							  "FOURTEEN",
							  "FIFTEEN",
							  "SIXTEEN",
							  "SEVENTEEN",
							  "EIGHTEEN",
							  "NINETEEN"
						  };
            string[] tens ={
							  "",
							  "TEN",
							  "TWENTY",
							  "THIRTY",
							  "FORTY",
							  "FIFTY",
							  "SIXTY",
							  "SEVENTY",
							  "EIGHTY",
							  "NINETY"
						  };
            string[] thou ={
							  "",
							  "thousand",
							  "million",
							  "billion",
							  "trillion",
							  "quadrillion",
							  "quintillion"
						  };

            if (inputNum == 0)
                return ("zero");

            string s = inputNum.ToString();

            string s1, s2, s3, s4;

            s1 = s.Substring(0, 2);
            s2 = s.Substring(2, 2);
            s3 = s.Substring(4, 1);
            s4 = s.Substring(5, 2);

            if (Convert.ToInt16(s1) > 0)
            {
                retval = tens[Convert.ToInt16(s1.Substring(0, 1))];
                retval = retval + " " + ones[Convert.ToInt16(s1.Substring(1, 1))] + " LAC ";
            }

            if (Convert.ToInt16(s2) > 0)
            {
                retval = retval + " " + tens[Convert.ToInt16(s2.Substring(0, 1))];
                retval = retval + " " + ones[Convert.ToInt16(s2.Substring(1, 1))] + " THOUSAND ";
            }

            if (Convert.ToInt16(s3) > 0)
            {
                retval = retval + " " + ones[Convert.ToInt16(s3.Substring(0, 1))] + " HUNDRED ";
            }

            i = Convert.ToInt16(s4);

            if (i > 0)
            {
                if (i > 19)
                {
                    retval = retval + " " + tens[Convert.ToInt16(s4.Substring(0, 1))];
                    retval = retval + " " + ones[Convert.ToInt16(s4.Substring(1, 1))];
                }
                else
                {
                    retval = retval + " " + ones[Convert.ToInt16(s4)];
                }
            }

            if (retval.Length > 0)
                retval += " ONLY";

            return (retval);
        }

        public static byte[] CreateImage(string checkCode)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(Convert.ToInt32(Math.Ceiling((decimal)(checkCode.Length * 14))), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                Random random = new Random();
                g.Clear(Color.AliceBlue);

                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                System.Drawing.Font font = new System.Drawing.Font("Comic Sans MS", 12, System.Drawing.FontStyle.Bold);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, new SolidBrush(Color.Red), 2, 2);

                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
                //Response.ClearContent();
                //Response.ContentType = "image/Gif";
                //Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        public static int FeeCalc(DateTime Edate, string iSchNo, string iReg, string cls)
        {
            int sch = 0;
            int feeamt = 150;
            sch = Convert.ToInt32(iSchNo);

            if (iReg == "D")
            {
                if (sch >= 72000 && sch < 75000)
                {
                    feeamt = 250;
                    
                    if (cls == "11")
                        feeamt = 300;
                }
            }
            return feeamt;
        }

        public static int FeeCalc(string subs, string schno, string reg, string cls, string schtype)
        {
            string subx = "";
            int feeamt = 0, prsub = 0, sch, j = 0;
            sch = Convert.ToInt32(schno);
            string[] subcount;
            subcount = subs.Split(',');

            for (int i = 0; i < subcount.Length; i++)
            {
                if (subcount[i] != "" && subcount[i] != "-")
                    j++;
            }

            if (cls == "12")
            {
                XmlTextReader reader = new XmlTextReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/prsub12.xml"));
                reader.WhitespaceHandling = WhitespaceHandling.None;
                XmlDocument xmlDoc = new XmlDocument();
                //Load the file into the XmlDocument
                xmlDoc.Load(reader);
                reader.Close();
                for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
                {
                    subx = xmlDoc.DocumentElement.ChildNodes[i]["sub"].InnerText;

                    if (subs.IndexOf(subx) > 0)
                    {
                        prsub++;
                    }
                }
            }

            if (reg == "D")
            {
                if (sch < 65000)
                {
                    feeamt = 250 + (prsub * 25);
                    if (j >= 6)
                        feeamt += 120;
                }
                if (sch >= 65001 && sch <= 65999)
                {
                    feeamt = 500 + (prsub * 40);
                    if (j >= 6)
                        feeamt += 200;
                }
                if (sch >= 75000 && sch <= 90000)
                {
                    feeamt = 500 + (prsub * 40);
                    if (j >= 6)
                        feeamt += 200;
                }
                if (sch >= 72001 && sch <= 72999)
                {
                    feeamt = 3000 + (prsub * 125);
                    if (j >= 6)
                        feeamt += 1000;
                }
            }
            else
            {
                feeamt = 500 + (prsub * 40);
                if (j >= 6)
                    feeamt += 200;
            }

            return feeamt;
        }

        public static int LateFee(DateTime Edate, string iSchNo, string iReg)
        {
            int sch = 0;
            sch = Convert.ToInt32(iSchNo);
            
            string ixml = "lastdate.xml";

            /*
            string jkSch = "'04401','04402','04403','04404','04405','04406','04407','04408','04409','04410','04411','04412','04413','04414','04415','04416','04417','04418','04419','04420','04421','04422','04423','04424','04425','04426','04427','04428','04430','04431','04432','04433','04434','04435','04436','04437','04438','04439','04440','04441','04442','04443','04444','04445','04446','04447','04448','04449','04450','04451','04452','04453','04454','04455','04456','04458','04459','04460','04461','04462','04463','04464','04465','04466','04467','04468','04469','24001','24002','24003','24004','24005','24006','24007','24008','24009','24010','24011','24012','24013','24014','24015','24016','24017','24018','24019','24020','24021','24022','24023','24024','24026','24027','24028','24029','24030','24031','24032','24033','24034','24035','24036','24037','24038','24039','24040','24041','24042','24043','24044','24045','24046','24047','24048','24049'";
            string hudHud = "'06056','06057','06058','06059','06060','06061','06062','06063','06064','06065','06066','06067','06073','06075','06082','06095','06096','06099','06108','06115','06120','06130','06134','06136','06142','06151','06153','06170','06176','06187','06192','06197','06210','06213','06219','06222','06224','06232','06233','06238','06247','06250','06257','06261','06265','06268','06270','06280','06284','06288','06289','06290','06292','06293','06295','06298','06300','08314','08315','08316','08317','08318','08319','08320','08334','08337','08339','08341','08354','08357','08366','08369','08371','08372','08383','08388','08390','08392','09708','09714','09718','40006','40014','40021','40034','40038','40044','40047','40051','40060','40066','40072','40084','40088','40093','40126','53007','53008','53010','53013','53016','53017','53019','53020','53021','53026','53039','53043','53052','53053','53065','53074','53079','53082','53085','53089','53090','53091','53098','53102','53103','53107','53108','53112','53113','53125','53143','53144','53147','53162','53164','53165','53166'";

            if (jkSch.Contains(System.Web.HttpContext.Current.Session["sch"].ToString()) && System.Web.HttpContext.Current.Session["reg"].ToString() == "C")
            {
                if (System.DateTime.Now < Convert.ToDateTime("31-Oct-2014 23:59:59"))
                {
                    return 0;
                }
            }
            if (hudHud.Contains(System.Web.HttpContext.Current.Session["sch"].ToString()))
            {
                if (System.DateTime.Now < Convert.ToDateTime("15-Nov-2014 23:59:59"))
                {
                    return 0;
                }
            }
            */

            if (iReg == "D")
            {
                //last date for foregin schools of UAE has been extended to 15/10/2013///
                if (sch >= 72000 && sch < 75000)
                {
                    ixml = "lastdatef.xml";
                }
            }

            XmlTextReader reader = new XmlTextReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/" + ixml));
            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument xmlDoc = new XmlDocument();
            //Load the file into the XmlDocument
            xmlDoc.Load(reader);
            reader.Close();
            string ldate, lfee, grace, lfeeamt="0";
            for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
            {
                ldate = xmlDoc.DocumentElement.ChildNodes[i]["ldate"].InnerText;
                lfee = xmlDoc.DocumentElement.ChildNodes[i]["lfee"].InnerText;
                grace = xmlDoc.DocumentElement.ChildNodes[i]["grace"].InnerText;

                if (Edate > Convert.ToDateTime(ldate))
                {
                    lfeeamt = lfee;
                }
            }
            return Convert.ToInt16(lfeeamt);
        }

        public static int LateFee(DateTime Edate, string cls, string iSchNo, string iReg, string SchType)
        {
            int sch = 0;
            sch = Convert.ToInt32(iSchNo);
            /*
            if (iReg == "D")
            {
                //last date for foregin schools of UAE has been extended to 15/10/2013///
                if ("72601,72602,72603,72604,72605,72606,72607,72608,72609,72610,72621,72622,72623,72624,72625,72626,72628,72629,72673,72761,72762,72763,72768,72769,72651,72652,72653,72654,72655,72656,72657,72658,72659,72660,72661,72662,72663,72664,72665,72666,72667,72668,72669,72670,72671,72672,72674,72675,72676,72677,72678,72679,72680,72681,72682,72683,72684,72685,72686,72687,72688,72689,72690,72693,72694,72695,72696,72697,72698,72702,72703,72704,72705,72706,72707,72708,72709,72710,72714,72738,72740,72748,72511,72512,72513,72514,72515,72516,72551,72562,72563,72564,72565,72566,72567,72568,72569,72570,72571,72572,72573,72574,72576,72577,72578,72579,72580,72581,72582,72583,72584,72585,72586,72587,72588,72589,72590,72591,72592,72593,72594,72595,72596,72597".Contains(iSchNo) && DateTime.Now <= Convert.ToDateTime("15-Oct-2013 23:59"))
                    return 0;
            }
            */
            /*
            if (iReg == "D" && cls == "12")
            {
                if (sch >= 72000 && sch < 75000)
                {
                    if (Edate < Convert.ToDateTime("10/15/2011 23:59:59"))
                        return 0;
                }
            }

            if (SchType == "J" && cls == "10")
            {
                if (Edate < Convert.ToDateTime("10/31/2011 23:59:59"))
                    return 0;
            }
            */

            string filename;
            if (cls == "10")
                filename = "~/App_Data/lastdate10.xml";
            else
                filename = "~/App_Data/lastdate12.xml";

            XmlTextReader reader = new XmlTextReader(System.Web.HttpContext.Current.Server.MapPath(filename));

            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument xmlDoc = new XmlDocument();
            //Load the file into the XmlDocument
            xmlDoc.Load(reader);
            reader.Close();
            string ldate, lfee, grace, lfeeamt = "0";
            for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
            {
                ldate = xmlDoc.DocumentElement.ChildNodes[i]["ldate"].InnerText;
                lfee = xmlDoc.DocumentElement.ChildNodes[i]["lfee"].InnerText;
                grace = xmlDoc.DocumentElement.ChildNodes[i]["grace"].InnerText;

                if (Edate > Convert.ToDateTime(ldate))
                {
                    lfeeamt = lfee;
                }
            }
            return Convert.ToInt16(lfeeamt);
        }


        public static bool chkVocSub(string sub1, string sub2, string sub3, string sub4, string sub5, string sub6)
        {
            bool retVal = true;
            string subCom = "";
            int sCtr = 0, iPkgSub = 0;

            int vcSub = 0;
            if (sub1.Substring(0, 1) == "6" || sub1.Substring(0, 1) == "7" || sub1.Substring(0, 1) == "8")
                vcSub++;
            if (sub2.Substring(0, 1) == "6" || sub2.Substring(0, 1) == "7" || sub2.Substring(0, 1) == "8")
                vcSub++;
            if (sub3.Substring(0, 1) == "6" || sub3.Substring(0, 1) == "7" || sub3.Substring(0, 1) == "8")
                vcSub++;
            if (sub4.Substring(0, 1) == "6" || sub4.Substring(0, 1) == "7" || sub4.Substring(0, 1) == "8")
                vcSub++;
            if (sub5.Substring(0, 1) == "6" || sub5.Substring(0, 1) == "7" || sub5.Substring(0, 1) == "8")
                vcSub++;

            if(sub6.Length > 0)
                if (sub6.Substring(0, 1) == "6" || sub6.Substring(0, 1) == "7" || sub6.Substring(0, 1) == "8")
                    vcSub++;

            if (vcSub == 0)
                return true;

            //if (sub1.Substring(0, 2) != "30" && sub2.Substring(0, 2) != "30")
            //    return retVal;

            XmlTextReader reader = new XmlTextReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/invvoc.xml"));
            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument xmlDoc = new XmlDocument();
            //Load the file into the XmlDocument
            xmlDoc.Load(reader);
            reader.Close();
            for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
            {
                subCom = xmlDoc.DocumentElement.ChildNodes[i]["subcom"].InnerText;

                if (subCom.IndexOf(sub1) >= 0)
                    sCtr++;
                if (subCom.IndexOf(sub2) >= 0)
                    sCtr++;
                if (subCom.IndexOf(sub3) >= 0)
                    sCtr++;
                if (subCom.IndexOf(sub4) >= 0)
                    sCtr++;
                if (subCom.IndexOf(sub5) >= 0)
                    sCtr++;

                if (sCtr >= 5)
                {
                    retVal = false;
                    break;
                }
                else
                    sCtr = 0;
            }

            retVal = false;
            reader = new XmlTextReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/valvoc.xml"));
            reader.WhitespaceHandling = WhitespaceHandling.None;
            //Load the file into the XmlDocument
            xmlDoc.Load(reader);
            reader.Close();
            for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
            {
                subCom = xmlDoc.DocumentElement.ChildNodes[i]["subcom"].InnerText;
                iPkgSub = Convert.ToInt16(xmlDoc.DocumentElement.ChildNodes[i]["pkgsub"].InnerText);
                sCtr = 0;

                if (subCom.IndexOf(sub1) >= 0 && sub1.Trim().Length > 0)
                    sCtr++;
                if (subCom.IndexOf(sub2) >= 0 && sub2.Trim().Length > 0)
                    sCtr++;
                if (subCom.IndexOf(sub3) >= 0 && sub3.Trim().Length > 0)
                    sCtr++;
                if (subCom.IndexOf(sub4) >= 0 && sub4.Trim().Length > 0)
                    sCtr++;
                if (subCom.IndexOf(sub5) >= 0 && sub5.Trim().Length > 0)
                    sCtr++;
                if (subCom.IndexOf(sub6) >= 0 && sub6.Trim().Length > 0)
                    sCtr++;

                if (sCtr >= iPkgSub && vcSub >= iPkgSub)
                {
                    retVal = true;
                    break;
                }
                else
                    sCtr = 0;
            }
            return retVal;
        }

        public static int GetSequence(int irec2)
        {
            int isn1, isn2, isn3, isn4, isn5, isn6, isum, idigit;
            isn1 = isn2 = isn3 = isn4 = isn5 = isn6 = isum = idigit = 0;

            isn1 = (irec2 / 100000);
            isn2 = ((irec2 - 100000 * isn1) / 10000);
            isn3 = ((irec2 - 100000 * isn1 - 10000 * isn2) / 1000);
            isn4 = ((irec2 - 100000 * isn1 - 10000 * isn2 - 1000 * isn3) / 100);
            isn5 = ((irec2 - 100000 * isn1 - 10000 * isn2 - 1000 * isn3 - 100 * isn4) / 10);
            isn6 = (irec2 - 100000 * isn1 - 10000 * isn2 - 1000 * isn3 - 100 * isn4 - 10 * isn5);
            isum = 7 * isn1 + 6 * isn2 + 5 * isn3 + 4 * isn4 + 3 * isn5 + 2 * isn6;
            idigit = (isum / 10);
            idigit = isum - 10 * idigit;
            
            if (idigit != 0)
                idigit = 10 - idigit;

            return idigit;
        }

        public static string MD5(string password)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(password);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                //hash = cryptHandler.ComputeHash(System.Text.Encoding.Default.GetBytes(hash.ToString().ToUpper() + "salt"));
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        public static string GetSalt(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,+,#,$,=,%";
            string[] allCharArray = allChar.Split(',');
            string saltCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(41);
                if (temp != -1 && temp == t)
                {
                    return GetSalt(codeCount);
                }
                temp = t;
                saltCode += allCharArray[t];
            }
            return saltCode;
        }

        public static string RemSplChrDate(string cstr)
        {
            cstr = cstr.Replace("'", "");
            cstr = cstr.Replace("-", "");
            cstr = cstr.Replace("\\", "");
            cstr = cstr.Replace("+", "");
            cstr = cstr.Replace("*", "");
            cstr = cstr.Replace("#", "");
            cstr = cstr.Replace("&", "");
            cstr = cstr.Replace(".", "");
            cstr = cstr.Replace("%", "");
            cstr = cstr.Replace("<", "");
            cstr = cstr.Replace(">", "");
            cstr = cstr.Replace(";", "");
            cstr = cstr.Replace(")", "");
            cstr = cstr.Replace("(", "");
            cstr = cstr.Replace("=", "");
            return cstr;
        }

        public static string RemSplChr(string cstr)
        {
            cstr = cstr.Replace(". ", " ");
            cstr = cstr.Replace(".", " ");
            cstr = cstr.Replace("'", "");
            cstr = cstr.Replace("-", "");
            cstr = cstr.Replace("/", "");
            cstr = cstr.Replace("\\", "");
            cstr = cstr.Replace("+", "");
            cstr = cstr.Replace("*", "");
            cstr = cstr.Replace("#", "");
            cstr = cstr.Replace("&", "");
            cstr = cstr.Replace("%", "");
            cstr = cstr.Replace("<", "");
            cstr = cstr.Replace(">", "");
            cstr = cstr.Replace(";", "");
            cstr = cstr.Replace(")", "");
            cstr = cstr.Replace("(", "");
            cstr = cstr.Replace("=", "");
            //cstr = cstr.Replace(" ", "");
            return cstr;
        }

        public static string RemSplChrNum(string cstr)
        {
            cstr = cstr.Replace("'", "");
            cstr = cstr.Replace("-", "");
            cstr = cstr.Replace("/", "");
            cstr = cstr.Replace("\\", "");
            cstr = cstr.Replace("+", "");
            cstr = cstr.Replace("*", "");
            cstr = cstr.Replace("#", "");
            cstr = cstr.Replace("&", "");
            cstr = cstr.Replace("%", "");
            cstr = cstr.Replace("<", "");
            cstr = cstr.Replace(">", "");
            cstr = cstr.Replace(";", "");
            cstr = cstr.Replace(")", "");
            cstr = cstr.Replace("(", "");
            cstr = cstr.Replace("=", "");
            //cstr = cstr.Replace(" ", "");
            return cstr;
        }

        public static string RemSplChrGuid(string cstr)
        {
            cstr = cstr.Replace("'", "");
            cstr = cstr.Replace("/", "");
            cstr = cstr.Replace("\\", "");
            cstr = cstr.Replace("+", "");
            cstr = cstr.Replace("*", "");
            cstr = cstr.Replace("#", "");
            cstr = cstr.Replace("&", "");
            cstr = cstr.Replace(".", "");
            cstr = cstr.Replace("%", "");
            cstr = cstr.Replace("<", "");
            cstr = cstr.Replace(">", "");
            cstr = cstr.Replace(";", "");
            cstr = cstr.Replace(")", "");
            cstr = cstr.Replace("(", "");
            cstr = cstr.Replace("=", "");
            //cstr = cstr.Replace(" ", "");
            return cstr;
        }

        public static string IsMedium(string isub, string imed)
        {
            if (isub == "" || isub.IndexOf("-") > 0 || isub == "-  " || isub == "-" || isub == " ")
                return " ";

            int ilansub = 0;

            try
            {
                ilansub = Convert.ToInt16(isub);
            }
            catch (Exception ex)
            {
                return " ";
            }

            if (ilansub <= 26 || ilansub == 322 || (ilansub >= 92 && ilansub <= 126) || (ilansub >= 195 && ilansub <= 199) || (ilansub >= 301 && ilansub <= 303))
                return " ";
            else
                return imed;

        }
        public static string IsMediumText(string isub, string imed)
        {
            if (isub == "" || isub == "-")
                return " ";

            int ilansub = Convert.ToInt16(isub);

            if (ilansub <= 26 || ilansub == 322 || (ilansub >= 92 && ilansub <= 126) || (ilansub >= 195 && ilansub <= 199) || (ilansub >= 301 && ilansub <= 303))
                return " ";
            else
            {
                if (imed == "1")
                    return "ENG";
                else if (imed == "2")
                    return "HIN";
                else if (imed == "3")
                    return "URD";
                else if (imed == "4")
                    return "PUN";
                else
                    return imed;
            }
        }

        public static string Decode(string icode, string iitem)
        {
            string iret = icode;

            if (iitem == "handicap")
                iret = (icode == "H" ? "LI" : icode == "B" ? "VI" : icode == "D" ? "Deaf" : icode == "S" ? "Spastic" : icode == "C" ? "Dyslexic" : icode == "A" ? "Autistic" : "");

            if (iitem == "cast" || iitem == "scst")
                iret = (icode == "C" ? "SC" : icode == "T" ? "ST" : icode == "O" ? "OBC" : "Gen");

            if (iitem == "sex")
                iret = (icode == "M" ? "Male" : icode == "F" ? "Female" : icode);

            return iret;
        }
        public static string FormatRegdNo(string mregdno)
        {
            if (mregdno.Length == 13)
                mregdno = mregdno.Substring(0, 1) + "/" + mregdno.Substring(1, 1) + "/" + mregdno.Substring(2, 2) + "/" + mregdno.Substring(4, 5) + "/" + mregdno.Substring(9);
            if (mregdno.Length == 14)
                mregdno = mregdno.Substring(0, 1) + "/" + mregdno.Substring(1, 2) + "/" + mregdno.Substring(3, 5) + "/" + mregdno.Substring(8);
            if (mregdno.Length == 15)
                mregdno = mregdno.Substring(0, 1) + "/" + mregdno.Substring(1, 2) + "/" + mregdno.Substring(3, 2) + "/" + mregdno.Substring(5, 5) + "/" + mregdno.Substring(10);

            return mregdno;
        }

    }
}
