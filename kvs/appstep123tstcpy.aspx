<%@ Page Title="" Language="C#" MasterPageFile="~/kvs.Master" AutoEventWireup="true" CodeBehind="appstep123tstcpy.aspx.cs" Inherits="kvs.appstep123tstcpy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="rsv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
    <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
    <style type="text/css">
        .auto-style2 {
            font-size: small;
        }
        .auto-style3 {
            text-align: left;
        }
        .auto-style4 {
            width: 26%;
        }
        .auto-style5 {
            width: 337px;
        }
        .auto-style7 {
            width: 100px;
        }
        .auto-style8 {
            font-size: medium;
        }
        .auto-style9 {
            font-weight: bold;
            text-align: center;
        }
        </style>
    <style type="text/css">
        .center {
    margin: auto;
    width: 80%;
    border: 3px solid #EAEDED;
    padding: 10px;
    background-color:White;
  
}
           .modalBackground
    {
        background-color: Black;
       filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 80%;
        height: 100%;
        overflow-y: scroll;
    }
        .auto-style10 {
            width: 337px;
            font-weight: bold;
        }
        .auto-style11 {
            font-size: x-small;
        }
        .auto-style12 {
            color: #FF0000;
        }
        p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	        margin-left: 0in;
            margin-right: 0in;
            margin-top: 0in;
        }
p.MsoNoSpacing
	{margin-bottom:.0001pt;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	        margin-left: 0in;
            margin-right: 0in;
            margin-top: 0in;
        }
        </style>

    <script type="text/javascript">
        function buttonEnable(chckagree) {
            var divbutton = document.getElementById("divbtn");
            //var radiono = document.getElementById("rdbpwdn");

            if (chckagree.checked == true) {
              
                divbutton.style.display = "block";
                
            }
            else {
               
                divbutton.style.display = "none";
            }
           
        }

</script>
   
    <script type="text/javascript">
        function viewpwdcat(radioy) {
            var divpwdcat = document.getElementById("pwdcat");
            //var radiono = document.getElementById("rdbpwdn");

            if (radioy.checked == true) {
              
                divpwdcat.style.display = "block";
                
            }
            else {
               
                divpwdcat.style.display = "none";
            }
           
        }

</script>
    <script type="text/javascript">
        function checkdob(dob) {
          
            var date = dob.value;
            var bdate = new Date(date);
            var divdate = document.getElementById("dvdatecheck");
         
            mydate = new Date('09/30/1999');
           
            if (bdate > mydate) {
               
                divdate.style.display = "block";
                return false;
            }
            else {
               
                divdate.style.display = "none";
                return true;
            }

        }

</script>
   <script type="text/javascript">
        function stoppwdcat(radion) {
            var divpwdcat = document.getElementById("pwdcat");
            //var radiono = document.getElementById("rdbpwdn");

            if (radion.checked == true) {
              
                divpwdcat.style.display = "none";
                
            }
            else {
               
                divpwdcat.style.display = "block";
            }
           
        }

</script>

     <script type="text/javascript">
        function viewcg(radioy) {
            var divcgemp = document.getElementById("cgemp");
            //var radiono = document.getElementById("rdbpwdn");

            if (radioy.checked == true) {
              
                divcgemp.style.display = "block";
                
            }
            else {
               
                divcgemp.style.display = "none";
            }
           
        }

</script>
   
   <script type="text/javascript">
        function stopcg(radion) {
            var divcgemp = document.getElementById("cgemp");
            //var radiono = document.getElementById("rdbpwdn");

            if (radion.checked == true) {
              
                divcgemp.style.display = "none";
                
            }
            else {
               
                divcgemp.style.display = "block";
            }
           
        }

</script>
   
    
     <script type="text/javascript">
         function checkpgt() {
             var dvpgt = document.getElementById("dvpgt");
             var pgtchck = document.getElementById("<%=pgt.ClientID %>");
             var reqpgt = document.getElementById("reqpgt");
             if (pgtchck.checked==true) {
                
                 dvpgt.style.display = "block";
                 reqpgt.disbaled = false;
             }
             else {
                 
                 dvpgt.style.display = "none";
                 reqpgt.disabled = true;
             }
         }
    </script>

      <script type="text/javascript">
          function checktgt() {
              var ddltgt = document.getElementById("ddltgt");
              var tgtchck = document.getElementById("<%=tgt.ClientID %>");
              var reqtgt = document.getElementById("reqtgt");
             if (tgtchck.checked==true) {
              
                 dvtgt.style.display = "block";
                 reqtgt.disbaled = false;
             }
             else {
               
                 dvtgt.style.display = "none";
                 reqtgt.disabled = true;
             }
         }
    </script>

   
   
  <script type="javascript" runat="server">
   void ServerValidation (object source, ServerValidateEventArgs arguments)
      {
         
           string i = arguments.Value;
          
          try
                {
                    double j = double.Parse(i);
                    if (j < 3.0)
                    {

                        arguments.IsValid = false;

                    }
                    else
                    {
                        arguments.IsValid = true;
                    }
                }
                catch
                {
                    arguments.IsValid = false; 
                }

            

      }
   </script>
    <script type="javascript" runat="server">
   void ServerValidation2 (object source, ServerValidateEventArgs arguments)
      {
         
           string i = arguments.Value;
          
          try
                {
                    double j = double.Parse(i);
                    if (j < 0.0 || j>100.0)
                    {

                        arguments.IsValid = false;

                    }
                    else
                    {
                        arguments.IsValid = true;
                    }
                }
                catch
                {
                    arguments.IsValid = false; 
                }

            

      }
   </script>
     <script type="javascript" runat="server">
   void ServerValidationCtet (object source, ServerValidateEventArgs arguments)
      {
         
           string i = arguments.Value;
          
          try
                {
                    double j = double.Parse(i);
                    if (j < 0.0 || j>150.0)
                    {

                        arguments.IsValid = false;

                    }
                    else
                    {
                        arguments.IsValid = true;
                    }
                }
                catch
                {
                    arguments.IsValid = false; 
                }
             
      }
   </script>
      <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right
        specialKeys.push(32); //Space
        specialKeys.push(44); //Comma
        function IsAlphaNumeric(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode == 44 || keyCode == 32)||(keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
            document.getElementById("erroradd1").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>
     <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right
        specialKeys.push(32); //Space
        specialKeys.push(44); //Comma
        function IsAlphaNumeric2(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode == 44 || keyCode == 32) || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
            document.getElementById("erroradd2").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>
    <script src="js/aes.js" type="text/javascript"></script>
    <%--<script type="text/javascript">  
  
        function SubmitsEncry() {  
         
            debugger;  
            var govid = document.getElementById("<%=txtgovid.ClientID %>").value.trim();  
               var key = CryptoJS.enc.Utf8.parse('8080808080808080');  
                var iv = CryptoJS.enc.Utf8.parse('8080808080808080');  
                var encryptgovid = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(govid), key,  
                {  
                    keySize: 128 / 8,  
                    iv: iv,  
                    mode: CryptoJS.mode.CBC, 
                    padding: CryptoJS.pad.Pkcs7  
                });  
  
                document.getElementById("<%=txtgovid.ClientID %>").value = encryptgovid;        
        }  
    </script>--%>  
    <script type="text/javascript">
          window.history.forward();
          function noBack() { window.history.forward(); }
</script>
       <script type = "text/javascript" >
      function burstCache() {
        if (!navigator.onLine) {
            
            window.location = 'Oops.html';
        }
    }
</script> 
   
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server" onload="noBack();"> 
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
   
    <div class="container">
               
                   <section class="content">
     <div class="row">
        <div class="col-md-12">

          <div class="box box-warning">
            <div class="box-header with-border">
             <h4 class="box-title" style="text-align:center; width: 100%;"><strong>Direct Recruitment Drive 2018</strong></h4>

                <%--<asp:CheckBox ID="prtmusic" runat="server" Text="Primary Teacher Music (PRT Music)" Font-Size="Small" />--%>
              
            </div>
            
                 <div id="divform" runat="server">
                      <div class="box-body" >
            
            <div id="secinfo" runat="server">
              <div class="col-md-12">
              
            <div class="text-justify" >
    <center>
        
         <h4 class="auto-style8"><strong>STEP 1</strong></h4>
         <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red" CssClass="auto-style8"></asp:Label>
        <div id="all_form" runat="server">
          <div id="dvcand" style="display:block" runat="server">
        <table class="table table-bordered table-hover" >
         
               <tr><td colspan="2"><h4 class="auto-style9"><strong>PART- A</strong> : CANDIDATE DETAILS</h4></td></tr>
           <tr>
                <td class="auto-style4">
                    <b style="font-size:small">Name <font style="color:red;">*</font>
                     </b>
                </td>
                <td> 
                    <table><tr><td>  <div class="auto-style3">
                        <b style="font-size:small">First Name<font style="color:red;">*</font></b>
                    </div>
                    <asp:TextBox ID="txtcfname" runat="server" CssClass="form-control" required ValidationGroup="cadndet" width="300px" autocomplete="off" MaxLength="50"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regcnamef" runat="server" ControlToValidate="txtcfname"
    ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Invalid Name" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" />
                    <div class="auto-style3"></div></td><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td> <b style="font-size:small">Last Name</b> 
                  
                    <asp:TextBox ID="txtclname" runat="server" CssClass="form-control" ValidationGroup="cadndet" width="300px" autocomplete="off" MaxLength="50"></asp:TextBox>  
                        <asp:RegularExpressionValidator ID="regcnamel" runat="server" ControlToValidate="txtclname" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Invalid Name" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" /></td></tr></table>
                  
                    <br />
                   
                </td>
              
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Mother's Name <font style="color:red;">*</font>

                </td>
                 <td>
                     <asp:TextBox ID="txtmname" runat="server" MaxLength="80" class="form-control" ValidationGroup="cadndet" required width="300px" autocomplete="off"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtmname" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Invalid Name" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" />
                  
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Father's/Guardian's Name <font style="color:red;">*</font>

                </td>
                 <td >
                     <asp:TextBox ID="txtfname" runat="server" MaxLength="80" class="form-control" ValidationGroup="cadndet" required width="300px" autocomplete="off"></asp:TextBox>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtfname" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Invalid Name" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" />
                
                   </td>
            </tr>
             <tr>
               
                <td class="auto-style2"  >
                    <strong>Enter Government ID</strong></td>
                 <td >
                       <table><tr><td> <asp:DropDownList ID="ddlid" class="form-control select2" runat="server" width="300px" >
                          <asp:ListItem Value="0" Text="--Select ID--"></asp:ListItem>
                      <asp:ListItem Value="AA" Text="Aadhaar"></asp:ListItem>
                           <asp:ListItem Value="PS" Text="Passport"></asp:ListItem>
                           <asp:ListItem Value="DL" Text="Driving Licence"></asp:ListItem>
                            <asp:ListItem Value="VI" Text="Voter ID"></asp:ListItem>
                       <asp:ListItem Value="PN" Text="PAN Card"></asp:ListItem>
                                                   </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="reqgovtid" runat="server" ControlToValidate="ddlid" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true" CssClass="auto-style2"></asp:RequiredFieldValidator>
                                  </td><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td>  <asp:TextBox ID="txtgovid" runat="server" MaxLength="30" class="form-control" ValidationGroup="cadndet" width="300px" autocomplete="off" placeholder="Enter ID Number" required ></asp:TextBox> <br />    
              </td></tr></table>
                
                            
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Gender <font style="color:red;">*</font>

                </td>
                 <td >
                     <asp:DropDownList ID="ddlgen" class="form-control select2" runat="server" width="300px" ValidationGroup="cadndet" >
                          <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                           <asp:ListItem Value="F" Text="Female"></asp:ListItem>
                           <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                         
                                                   </asp:DropDownList>
                 <asp:RequiredFieldValidator class="auto-style2" ID="RequiredFieldValidator2" runat="server" ValidationGroup="cadndet" ControlToValidate="ddlgen" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Marital status <font style="color:red;">*</font>

                </td>
                 <td >
                      <asp:DropDownList ID="ddlmar" class="form-control select2" runat="server" width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlmar_SelectedIndexChanged" >
                          <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                           <asp:ListItem Value="S" Text="Single"></asp:ListItem>
                           <asp:ListItem Value="M" Text="Married"></asp:ListItem>
                          
                                                   </asp:DropDownList>
                 <asp:RequiredFieldValidator class="auto-style2" ID="RequiredFieldValidator1" runat="server" ValidationGroup="cadndet" ControlToValidate="ddlmar" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                      <br />
                   
                      <div id="dvnewname" runat="server">
                           <b style="font-size:small">Name After Marriage <font style="color:red;">(If Changed)</font>
                   <asp:TextBox ID="txtnewname" runat="server" CssClass="form-control" Width="300px" placeholde="Name After Marriage" MaxLength="100"></asp:TextBox>
                   </div>
                          </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Religion <font style="color:red;">*</font>

                </td>
                 <td >
                     <asp:DropDownList ID="ddlrel" runat="server" class="form-control select2" Width="300px">
                         <asp:ListItem value="0" Text="--Select--"></asp:ListItem>
                         <asp:ListItem value="H" Text="Hinduism"></asp:ListItem>
                          <asp:ListItem value="I" Text="Islam"></asp:ListItem>
                         <asp:ListItem value="C" Text="Christianity"></asp:ListItem>
                         <asp:ListItem value="S" Text="Sikhism"></asp:ListItem>
                         <asp:ListItem value="J" Text="Jainism"></asp:ListItem>
                         <asp:ListItem value="B" Text="Buddhism"></asp:ListItem>
                         <asp:ListItem value="O" Text="Others"></asp:ListItem>
                     </asp:DropDownList>
                           
                <asp:RequiredFieldValidator ID="reqrel" runat="server" ControlToValidate="ddlrel" InitialValue="0" ErrorMessage="*Required" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2"></asp:RequiredFieldValidator>
                   
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Category <font style="color:red;">*</font>

                    <br />
                    <span class="auto-style12">No change of category will be permitted at any stage after registration of the online application and the result will be processed considering the category which has been indicated in the online application</span></td>
                 <td >
                     <asp:DropDownList ID="ddlcat" class="form-control select2" runat="server" width="300px" ValidationGroup="cadndet" >
                          <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                           <asp:ListItem Value="G" Text="General"></asp:ListItem>
                           <asp:ListItem Value="C" Text="SC"></asp:ListItem>
                           <asp:ListItem Value="T" Text="ST"></asp:ListItem>
                         <asp:ListItem Value="O" Text="OBC (Central Govt. List)"></asp:ListItem>
                                                   </asp:DropDownList>
                 <asp:RequiredFieldValidator class="auto-style2" ID="RequiredFieldValidator3" runat="server" ValidationGroup="cadndet" ControlToValidate="ddlcat" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Are you differently abled with 40% or above disability? <font style="color:red;">*</font>

                    <br />
                    <p class="MsoNormal" style="text-align: justify; font-size: x-small;">
                        <span class="auto-style12"><b><span>* As per OM dated 15.01.2018 of DOPT the following categories to be given 1% reservation <o:p></o:p></span></b>
                    </p>
                    <p class="MsoNoSpacing" style="font-weight: bold; font-size: x-small">
                        (d) autism, intellectual disabilities, specific learning disabilities and mental illness<o:p></o:p></p>
                    <p class="MsoNoSpacing" style="margin-left: .25in; text-indent: -.25in; font-size: x-small;">
                        </span><span>(e) multiple disabilities from amongst persons under clause (a) to (d)&nbsp;&nbsp;&nbsp; including deaf –blindness.</span><o:p></o:p></span></p>

                </td>
                 <td >
                     <table style="width:100%"><tr><td style="width:40%" class="auto-style3"> 
                         <asp:RadioButtonList ID="rdbpwd" runat="server" RepeatDirection="Horizontal" CssClass="auto-style2" OnSelectedIndexChanged="rdbpwd_SelectedIndexChanged" AutoPostBack="true">
                             <asp:ListItem value="Y" Text="Yes"></asp:ListItem>
                              <asp:ListItem value="N" Text="No" Selected></asp:ListItem>
                         </asp:RadioButtonList>
                         <%--<asp:DropDownList ID="ddldip" runat="server" CssClass="form-control" Width="100px">
                                 <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                  <asp:ListItem Value="8" Text="Five Years' Diploma In Drawing & Painting/Sculpture/Graphic Art"></asp:ListItem>
                                   <asp:ListItem Value="9" Text="Three years Diploma in Electrical or Electronics Engineering"></asp:ListItem>
                                   <asp:ListItem Value="10" Text="Library Science"></asp:ListItem>
                                   <asp:ListItem Value="11" Text="Post Graduate Diploma In Computers"></asp:ListItem>
                                                       </asp:DropDownList>--%>

                                                   </td>
                         </tr>
                         </table>
                      <div id="dvpwdcat" runat="server" style="text-align:left">
                          <%--<td class="auto-style5"><span><strong>&#39;B’ Level from DOEACC<br />
                           (Only for PGT-Computer Science)</strong></span></td><td class="auto-style7">
                                &nbsp;</td>
                            <td class="auto-style7"><asp:TextBox ID="txtyrblvl" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator7" Operator="LessThanEqual" type="String" ControltoValidate="txtyrblvl" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" /><asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrblvl" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator></td><td>&nbsp;</td><td><asp:TextBox ID="txtpercblvl" runat="server" class="form-control" width="60px" MaxLength="5"></asp:TextBox><asp:RegularExpressionValidator ID="regrail6" runat="server" ControlToValidate="txtpercblvl" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ></asp:RegularExpressionValidator><asp:CustomValidator ID="CustomValidator9" runat="server" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" Font-Bold="true" ForeColor="Red" ControlToValidate="txtpercblvl" ValidationGroup="cadndet" CssClass="auto-style2"></asp:CustomValidator>
                            </td>--%>
                             <table>
                                 <tr>
                         <td style="width:60%" class="auto-style3">
                             
                         <b style="font-size:small">Select a PWD Category</b>
                         <br />
                            
                                 <div class="auto-style3">
                        
                                     <asp:DropDownList ID="ddlpwdcat" runat="server" CssClass="form-control">
                                         <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                         <asp:ListItem Value="V" Text="Visually Impaired"></asp:ListItem>
                          <asp:ListItem Value="O" Text="Orthopedic Impaired"></asp:ListItem>
                          <asp:ListItem Value="H" Text="Hearing Impaired"></asp:ListItem> 
                                          <asp:ListItem Value="T" Text="Others"></asp:ListItem>  
                                     </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="reqpwdcat" runat="server" ValidationGroup="cadndet" ErrorMessage="*Required" InitialValue="0" ControlToValidate="ddlpwdcat" Font-Bold="true" ForeColor="Red" CssClass="auto-style2"></asp:RequiredFieldValidator>
                              
                                </div>
                                 <br />
                                   <b style="font-size:small">Do You Need A Scribe?</b>
                         <br />
                                   <div class="auto-style3">
                                  <asp:RadioButtonList ID="rdbscribe" runat="server" RepeatDirection="Horizontal" CssClass="auto-style2">
                             <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                           <asp:ListItem Value="N" Text="No" selected></asp:ListItem>                       
                      </asp:RadioButtonList>
                                             
                                 </div>
                                 </td>
                          </tr>
                                 </table>
                        
                          </div>
                          
                      
                    
                  
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Are you a KVS employee? <font style="color:red;">*</font>

                </td>
                 <td class="auto-style3" >
                        <asp:RadioButtonList ID="rdbemp" runat="server" CssClass="auto-style2" RepeatDirection="Horizontal" ValidationGroup="cadndet">
                          <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                           <asp:ListItem Value="N" Text="No" Selected></asp:ListItem>
                      </asp:RadioButtonList>
                  
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Are you a Central Government employee? <font style="color:red;">*</font>
                    <br />
                   <b class="auto-style11"> <font style="color:red;">Age relaxation is not applicable for government service below 3 years</font></b><br />

                </td>
                 <td class="auto-style3" >
                     <table><tr><td>
                          <asp:RadioButtonList ID="rdbcgemp" runat="server" RepeatDirection="Horizontal" CssClass="auto-style2" AutoPostBack="true" OnSelectedIndexChanged="rdbcgemp_SelectedIndexChanged">
                             <asp:ListItem value="Y" Text="Yes"></asp:ListItem>
                              <asp:ListItem value="N" Text="No" Selected></asp:ListItem>
                         </asp:RadioButtonList>
                          <%--<td class="auto-style5"><span><strong>&#39;C’ Level from DOEACC<br />
                           (Only for PGT-Computer Science)</strong></span></td><td class="auto-style7">
                                &nbsp;</td>
                            <td class="auto-style7"><asp:TextBox ID="txtyrclvl" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator8" Operator="LessThanEqual" type="String" ControltoValidate="txtyrclvl" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" /><asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrclvl" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator></td><td>&nbsp;</td><td><asp:TextBox ID="txtpercclvl" runat="server" class="form-control" width="60px" MaxLength="5"></asp:TextBox><asp:RegularExpressionValidator ID="regrail7" runat="server" ControlToValidate="txtpercclvl" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ></asp:RegularExpressionValidator><asp:CustomValidator ID="CustomValidator10" runat="server" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" Font-Bold="true" ForeColor="Red" ControlToValidate="txtpercclvl" ValidationGroup="cadndet" CssClass="auto-style2"></asp:CustomValidator>
                            </td>--%>


                                </td></tr></table>
                     <div id="divcgemp" runat="server">
                         <%-- <asp:LinkButton ID="lnkOk" runat="server"></asp:LinkButton>--%>
                         <table><tr><td class="auto-style3">
                              <b style="font-size:small">Enter Years' of Regular Service</b>
                              <asp:TextBox ID="txtyrreg" runat="server" CssClass="form-control" width="300px" MaxLength="2" autocomplete="off"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="reqyrreg" runat="server" ControlToValidate="txtyrreg" ErrorMessage="*Required" Font-Bold="true" ForeColor="Red" CssClass="auto-style2" ValidationGroup="cadndet"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regyrreg" runat="server" ControlToValidate="txtyrreg" ErrorMessage="*Numbers Only" Font-Bold="true" ForeColor="Red" ValidationExpression="\d+" CssClass="auto-style2" ValidationGroup="cadndet"></asp:RegularExpressionValidator>
                                    </td></tr></table></div>
                
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Are you ordinarily domiciled in the state of Jammu & Kashmir <span>during 01.01.1980 to 31.12.1989</span>? <font style="color:red;">*</font>

                </td>
                 <td class="auto-style3" >
                      <asp:RadioButtonList ID="rdbjk" runat="server" CssClass="auto-style2" RepeatDirection="Horizontal" ValidationGroup="cadndet">
                          <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                           <asp:ListItem Value="N" Text="No" Selected></asp:ListItem>
                      </asp:RadioButtonList>
                  
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Are you an Ex-Serviceman? <font style="color:red;">*</font>

                </td>
                 <td class="auto-style3" >
                       <asp:RadioButtonList ID="rdbexserv" runat="server" CssClass="auto-style2" RepeatDirection="Horizontal" ValidationGroup="cadndet" AutoPostBack="True" OnSelectedIndexChanged="rdbexserv_SelectedIndexChanged">
                          <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                           <asp:ListItem Value="N" Text="No" Selected></asp:ListItem>
                      </asp:RadioButtonList>
                       <%-- <asp:LinkButton ID="lnkOk" runat="server"></asp:LinkButton>--%>
               <br />
                     <div id="divexserv" runat="server">
                         <b style="font-size:small">Length of Service (In Years)</b>
                         <br />
                           <asp:TextBox ID="txtservlen" runat="server" MaxLength="2" class="form-control" width="300px" ValidationGroup="cadndet" autocomplete="off"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqservlen" runat="server" ErrorMessage="*Required" Font-Bold="true" ForeColor="Red" ControlToValidate="txtservlen" ValidationGroup="cadndet" CssClass="auto-style2"></asp:RequiredFieldValidator>
                                                   <asp:RegularExpressionValidator ID="regservlen" runat="server" ControlToValidate="txtservlen" ErrorMessage="*Numbers Only" Font-Bold="true" ForeColor="Red" ValidationExpression="\d+" CssClass="auto-style2" ValidationGroup="cadndet"></asp:RegularExpressionValidator>
                         <br />
                          <b style="font-size:small">Are you employed under Central Government/State Government in Group ‘C’ posts on regular basis after availing of the benefits of reservation given to ex-servicemen for their re-employment?</b>
                         <br />
                            <asp:RadioButtonList ID="rdbexwork" runat="server" CssClass="auto-style2" RepeatDirection="Horizontal" ValidationGroup="cadndet" >
                          <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                           <asp:ListItem Value="N" Text="No" Selected></asp:ListItem>
                      </asp:RadioButtonList>
                     </div>
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Visible Identification Mark <font style="color:red;">*</font>

                </td>
                 <td class="auto-style3" >
                      
                  <asp:TextBox ID="txtiden" runat="server" MaxLength="100" class="form-control" required width="300px" ValidationGroup="cadndet" autocomplete="off"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regcnamef0" runat="server" ControlToValidate="txtiden"
    ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" />
               <br />
                     
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Date of Birth <font style="color:red;">*</font>

                </td>
                 <td class="auto-style3" >
                     <asp:TextBox ID="datepicker" runat="server"  class="form-control" required width="300px" ValidationGroup="cadndet" placeholder="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                          
           
              
                           
                  
               <br />
                     
                   </td>
            </tr>

              <tr>
               
                <td >
                   <b style="font-size:small">Address <font style="color:red;">*</font>

                </td>
                 <td class="auto-style3" >
                     <b style="font-size:small"> Address Line 1:</b><br />
                  <asp:TextBox ID="txtadd1" runat="server" MaxLength="100" class="form-control" required width="600px" ValidationGroup="cadndet" autocomplete="off" onkeypress="return IsAlphaNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                     <span id="erroradd1" style="color: Red; display: none; font-size:small;font-weight:bold">* Special Characters not allowed</span>  <br />
                     <b style="font-size:small"> Address Line 2:</b><br />
                  <asp:TextBox ID="txtadd2" runat="server" MaxLength="100" class="form-control" width="600px" autocomplete="off" onkeypress="return IsAlphaNumeric2(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                     <span id="erroradd2" style="color: Red; display: none; font-size:small;font-weight:bold">* Special Characters not allowed</span> 
                     <br />
                        <b style="font-size:small"> State:</b><br />
                        
                 <asp:DropDownList ID="ddlstate"  class="form-control select2" runat="server" width="300px" ValidationGroup="cadndet" AutoPostBack="True" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" AppendDataBoundItems="True">
                     <asp:ListItem value="0" Text="--Select State--"></asp:ListItem>
                 </asp:DropDownList>  <asp:RequiredFieldValidator class="auto-style2" ID="RequiredFieldValidator6" runat="server" ValidationGroup="cadndet" ControlToValidate="ddlstate" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
          
               <br />
                        <b style="font-size:small"> City / District:</b><br />
                       
                 <asp:DropDownList ID="ddlcity"  class="form-control select2" runat="server" width="300px" ValidationGroup="cadndet">
                     <asp:ListItem value="0" Text="--Select City--"></asp:ListItem>
                 </asp:DropDownList>
                       <asp:RequiredFieldValidator class="auto-style2" ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcity" ValidationGroup="cadndet" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                       <br />
                        <b style="font-size:small"> PIN Code:</b><br />
                  <asp:TextBox ID="txtpin" runat="server" MaxLength="6" class="form-control" required width="300px" ValidationGroup="cadndet" autocomplete="off"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtpin" ErrorMessage="*Numbers Only" Font-Bold="true" ForeColor="Red" ValidationExpression="\d+" CssClass="auto-style2" ValidationGroup="cadndet"></asp:RegularExpressionValidator>
               <br />
                     
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Email ID <font style="color:red;">*</font>

                </td>
                 <td class="auto-style3" >
                      
                  <asp:TextBox ID="txtemail" runat="server" MaxLength="100" TextMode="email" class="form-control" required width="300px" ValidationGroup="cadndet" autocomplete="off"></asp:TextBox>
               <asp:RegularExpressionValidator ID="regemail" runat="server" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" ControlToValidate="txtemail" ErrorMessage="*Invalid Email (Use Lower Case Characters Only)" ForeColor="Red" Font-Bold="true" CssClass="auto-style2"></asp:RegularExpressionValidator>
                     <br />
                     
                   </td>
            </tr>
             <tr>
               
                <td >
                   <b style="font-size:small">Mobile Number <font style="color:red;">*</font></b>

                </td>
                 <td class="auto-style3" >
                       <b style="font-size:small"> Primary:<font style="color:red;">*</font></b><br />
                  <asp:TextBox ID="txtmob" runat="server" MaxLength="10" class="form-control" required width="300px" ValidationGroup="cadndet" autocomplete="off"></asp:TextBox>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtmob" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationExpression="^[789]\d{9}$" CssClass="auto-style2" ValidationGroup="cadndet"></asp:RegularExpressionValidator>
           
               <br />
                       <b style="font-size:small"> Secondary (Optional):</b><br />
                  <asp:TextBox ID="txtmob2" runat="server" MaxLength="10" class="form-control" width="300px" autocomplete="off"></asp:TextBox>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtmob2" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationExpression="^[789]\d{9}$" CssClass="auto-style2" ValidationGroup="cadndet"></asp:RegularExpressionValidator>
           
               <br />
                   </td>
            </tr>
            </table>
             
<br />

           </div>
          <div id="dvpostexam" runat="server">
                                <table class="table table-bordered table-hover">
             <tr><td colspan="2"><h4 class="auto-style9"><b>PART- B : </b>POST DETAILS</h4></td></tr>
           <tr>
                <td style="width:30%">
                   <b style="font-size:small">Post applied for <font style="color:red;">*</font>
                     </b>
                </td>
               <td  class="auto-style3">
                   <asp:CheckBox ID="princi" runat="server" ForeColor="Red" Text="Principal" Font-Size="Small" AutoPostBack="true" OnCheckedChanged="princi_CheckedChanged" /><br />
                   <asp:CheckBox ID="vcp" runat="server" ForeColor="Red" Text="Vice Principal" Font-Size="Small" AutoPostBack="true" OnCheckedChanged="vcp_CheckedChanged" /><br />
                   <asp:CheckBox ID="pgt" runat="server" ForeColor="Red" Text="Post Graduate Teacher (PGT)" Font-Size="Small" AutoPostBack="true" OnCheckedChanged="pgt_CheckedChanged1" /><br />
                   <div id="dvpgt" runat="server">
                       <asp:DropDownList ID="ddlpgt" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlpgt_SelectedIndexChanged" AppendDataBoundItems="True">
                           <asp:ListItem Value="0" Text="--Select Subject--"></asp:ListItem>
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="reqpgt" runat="server" ErrorMessage="*Required" ControlToValidate="ddlpgt" InitialValue="0" Font-Bold="true" ForeColor="Red" CssClass="auto-style2" ValidationGroup="cadndet"></asp:RequiredFieldValidator>
                  <br /><span class="auto-style2" id="spnesspgt" runat="server"><strong>Select The Essential Qualification You Fulfill</strong></span>
                       <div class="auto-style3">
                       <asp:RadioButtonList ID="rdblesspgt" runat="server" style="text-align:left;" CssClass="auto-style2">
                         
                       </asp:RadioButtonList>
                        
                       </div>
                        
                   </div>
                   <asp:CheckBox ID="tgt" runat="server" ForeColor="Red" Text="Trained Graduate Teacher (TGT)" Font-Size="Small" AutoPostBack="true" OnCheckedChanged="tgt_CheckedChanged"/><br />
                    <div id="dvtgt" runat="server">
                       <asp:DropDownList ID="ddltgt" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltgt_SelectedIndexChanged" AppendDataBoundItems="True">
                           <asp:ListItem Value="0" Text="--Select Subject--"></asp:ListItem>
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="reqtgt" runat="server" ErrorMessage="*Required" ControlToValidate="ddltgt" InitialValue="0" Font-Bold="true" ForeColor="Red" CssClass="auto-style2" ValidationGroup="cadndet"></asp:RequiredFieldValidator>
                    <br /><span class="auto-style2" id="spnesstgt" runat="server"><strong>Select The Essential Qualification You Fulfill</strong></span>
                
                          <asp:RadioButtonList ID="rdblesstgt" runat="server" style="text-align:left;" CssClass="auto-style2">
                         
                       </asp:RadioButtonList>
                    </div>
                   <asp:CheckBox ID="lib" runat="server" Text="Librarian" ForeColor="Red" Font-Size="Small" AutoPostBack="true" OnCheckedChanged="lib_CheckedChanged" />
                    <div id="dvlib" runat="server">
                    <span class="auto-style2" id="Span1" runat="server"><strong>Select The Essential Qualification You Fulfill</strong></span>
                
                           <asp:RadioButtonList ID="rdbllib" runat="server" style="text-align:left;" CssClass="auto-style2" >
                         
                       </asp:RadioButtonList>
                    </div>
                    <br />
                   <asp:CheckBox ID="prt" runat="server" Text="Primary Teacher (PRT)" ForeColor="Red" Font-Size="Small" AutoPostBack="true" OnCheckedChanged="prt_CheckedChanged" /><br />
                  <div id="dvprt" runat="server">
                    <span class="auto-style2" id="Span2" runat="server"><strong>Select The Essential Qualification You Fulfill</strong></span>
                
                           <asp:RadioButtonList ID="rdblprt" runat="server" style="text-align:left;" CssClass="auto-style2">
                         
                       </asp:RadioButtonList>
                      <span><strong>*Who has acquired the qualification of Bachelor of Education from any NCTE recognized institution shall be considered for appointment as a teacher in class I-V provided the person so appointed as a teacher shall mandatorily undergo a six month Bridge Course in Elementary Education recognized by the NCTE within two years of such appointment as Primary Teacher.</strong></span>
                    </div>
                    <asp:CheckBox ID="prtmusic" runat="server" Text="Primary Teacher Music (PRT-M)" ForeColor="Red" Font-Size="Small" AutoPostBack="true" OnCheckedChanged="prtm_CheckedChanged" />
                  <div id="dvprtm" runat="server">
                    <span class="auto-style2" id="Span3" runat="server"><strong>Select The Essential Qualification You Fulfill</strong></span>
                
                           <asp:RadioButtonList ID="rdblprtm" runat="server" style="text-align:left;" CssClass="auto-style2">
                         
                       </asp:RadioButtonList>
                                            <span><strong><span class="auto-style12"><span class="auto-style2">*For the post of Primary Teacher (Music), the candidates who have obtained Diploma in Music are not eligible to apply.</span></span></strong></span><span class="auto-style12"><span class="auto-style2"> </span></span>

                    </div><br />
               </td>
            </tr>
             <tr>
               
                <td >
                   <b><span class="auto-style2">Essential Qualifications:</span></b>
                    <br />
                    <strong>
                    <asp:HyperLink ID="hypessqual" runat="server" NavigateUrl="kvs_static/Criteria.html" Text="Check Essential Qualification" CssClass="auto-style2" Target="_blank"></asp:HyperLink>
                    </strong>
                    <br />
                   <b> <span class="auto-style2">Desirable Qualifications:</span></b>
                    <br />
                    <strong>
                    <asp:HyperLink ID="hypdes" runat="server" NavigateUrl="kvs_static/Criteria.html" Text="Check Desirable Qualification" CssClass="auto-style2" Target="_blank"></asp:HyperLink>
                    </strong>
                    <br />

                </td>
                 <td >
                      <div class="auto-style3">
                      <b><span class="auto-style2">Do You Fulfill Essential Qualifications?</span></b> 
                    <br />
                     <span class="auto-style2">
                      </div>
                      <asp:DropDownList ID="ddless" class="form-control select2" runat="server" width="300px" ValidationGroup="exmdet" >
                          <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                           <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                           <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                   </asp:DropDownList>
                     <asp:RequiredFieldValidator class="auto-style2" ID="reqess" ValidationGroup="cadndet" runat="server" ControlToValidate="ddless" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                      <div class="auto-style3">
                         
                     <br />
                      <b><span class="auto-style2">Do You Fulfill Desirable Qualifications?</span></b> 
                    <br />
                     <span class="auto-style2">
                      </div>
                      <asp:DropDownList ID="ddldesire" class="form-control select2" runat="server" width="300px" ValidationGroup="exmdet">
                          <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                           <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                           <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                   </asp:DropDownList>
                       <asp:RequiredFieldValidator class="auto-style2" ID="reqdesire" ValidationGroup="cadndet" runat="server" ControlToValidate="ddldesire" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                 </td>
            </tr>
                        </table>
               <table class="table table-bordered table-hover">
            <tr><td colspan="2"><b><h4>EXAMINATION DETAILS</h4></b></td></tr>
             <tr>
               
                <td style="width:30%">
                   <b style="font-size:small">Select Preferences For Examination City <font style="color:red;">*</font>
                    <br />
                    <b><span><font style="color:red;">The centre for the written examination for the post of Principal and Vice Principal will be at Delhi Only</font></span></b></td>
                 <td class="auto-style3" >
                      
                 <asp:DropDownList ID="ddlcity1"  class="form-control select2" runat="server" width="300px" ValidationGroup="exmdet" AppendDataBoundItems="True">
                     <asp:ListItem value="0" Text="--First Option--"></asp:ListItem>
                 </asp:DropDownList>
                       <asp:RequiredFieldValidator class="auto-style2" ID="reqcity1" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlcity1" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                      <asp:DropDownList ID="ddlcity2"  class="form-control select2" runat="server" width="300px" AppendDataBoundItems="true">
                     <asp:ListItem value="0" Text="--Second Option--"></asp:ListItem>
                 </asp:DropDownList>   <asp:RequiredFieldValidator class="auto-style2" ID="reqcity2" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlcity2" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                      <asp:DropDownList ID="ddlcity3"  class="form-control select2" runat="server" width="300px" AppendDataBoundItems="True">
                     <asp:ListItem value="0" Text="--Third Option--"></asp:ListItem>
                 </asp:DropDownList>   <asp:RequiredFieldValidator class="auto-style2" ID="reqcity3" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlcity3" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                      <asp:DropDownList ID="ddlcity4"  class="form-control select2" runat="server" width="300px" AppendDataBoundItems="true">
                     <asp:ListItem value="0" Text="--Fourth Option--"></asp:ListItem>
                 </asp:DropDownList>
                        <asp:RequiredFieldValidator class="auto-style2" ID="reqcity4" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlcity4" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
             
                    
                   </td>
            </tr>
                    <tr>
               
                <td style="width:30%">
                   <b style="font-size:small">Select Preferences For Posting Zone <font style="color:red;">*</font>
                    <br />
                    <b style="font-size: small"><span><font style="color:red;">Not Applicable for the post of Principal and Vice Principal</font></span></b></td>
                 <td class="auto-style3" >
                      
             <asp:DropDownList ID="ddlzone1"  class="form-control select2" runat="server" width="300px" ValidationGroup="exmdet" >
                     <asp:ListItem value="0" Text="--First Option--"></asp:ListItem>
                  <asp:ListItem value="C" Text="Central Zone"></asp:ListItem>
                  <asp:ListItem value="N" Text="North Zone"></asp:ListItem>
                  <asp:ListItem value="S" Text="South Zone"></asp:ListItem>
                  <asp:ListItem value="E" Text="East Zone"></asp:ListItem>
                  <asp:ListItem value="W" Text="West Zone"></asp:ListItem>
                  <asp:ListItem value="NE" Text="North Eastern Zone"></asp:ListItem>
                 </asp:DropDownList>
                       <asp:RequiredFieldValidator class="auto-style2" ID="reqzone1" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlzone1" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                      <asp:DropDownList ID="ddlzone2"  class="form-control select2" runat="server" width="300px" >
                     <asp:ListItem value="0" Text="--Second Option--"></asp:ListItem>
                           <asp:ListItem value="C" Text="Central Zone"></asp:ListItem>
                  <asp:ListItem value="N" Text="North Zone"></asp:ListItem>
                  <asp:ListItem value="S" Text="South Zone"></asp:ListItem>
                  <asp:ListItem value="E" Text="East Zone"></asp:ListItem>
                  <asp:ListItem value="W" Text="West Zone"></asp:ListItem>
                                       <asp:ListItem value="NE" Text="North Eastern Zone"></asp:ListItem>
                
                 </asp:DropDownList>   <asp:RequiredFieldValidator class="auto-style2" ID="reqzone2" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlzone2" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                      <asp:DropDownList ID="ddlzone3"  class="form-control select2" runat="server" width="300px" >
                     <asp:ListItem value="0" Text="--Third Option--"></asp:ListItem>
                           <asp:ListItem value="C" Text="Central Zone"></asp:ListItem>
                  <asp:ListItem value="N" Text="North Zone"></asp:ListItem>
                  <asp:ListItem value="S" Text="South Zone"></asp:ListItem>
                  <asp:ListItem value="E" Text="East Zone"></asp:ListItem>
                  <asp:ListItem value="W" Text="West Zone"></asp:ListItem>
             <asp:ListItem value="NE" Text="North Eastern Zone"></asp:ListItem>
                      </asp:DropDownList>   <asp:RequiredFieldValidator class="auto-style2" ID="reqzone3" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlzone3" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
                      <asp:DropDownList ID="ddlzone4"  class="form-control select2" runat="server" width="300px" >
                     <asp:ListItem value="0" Text="--Fourth Option--"></asp:ListItem>
                           <asp:ListItem value="C" Text="Central Zone"></asp:ListItem>
                  <asp:ListItem value="N" Text="North Zone"></asp:ListItem>
                  <asp:ListItem value="S" Text="South Zone"></asp:ListItem>
                  <asp:ListItem value="E" Text="East Zone"></asp:ListItem>
                  <asp:ListItem value="W" Text="West Zone"></asp:ListItem>
            <asp:ListItem value="NE" Text="North Eastern Zone"></asp:ListItem>
                       </asp:DropDownList>
                        <asp:RequiredFieldValidator class="auto-style2" ID="reqzone4" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlzone4" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                     <asp:DropDownList ID="ddlzone5"  class="form-control select2" runat="server" width="300px" >
                     <asp:ListItem value="0" Text="--Fifth Option--"></asp:ListItem>
                          <asp:ListItem value="C" Text="Central Zone"></asp:ListItem>
                  <asp:ListItem value="N" Text="North Zone"></asp:ListItem>
                  <asp:ListItem value="S" Text="South Zone"></asp:ListItem>
                  <asp:ListItem value="E" Text="East Zone"></asp:ListItem>
                  <asp:ListItem value="W" Text="West Zone"></asp:ListItem>
            <asp:ListItem value="NE" Text="North Eastern Zone"></asp:ListItem>
                
                 </asp:DropDownList>
                        <asp:RequiredFieldValidator class="auto-style2" ID="reqzone5" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlzone5" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                     <asp:DropDownList ID="ddlzone6"  class="form-control select2" runat="server" width="300px" >
                     <asp:ListItem value="0" Text="--Sixth Option--"></asp:ListItem>
                          <asp:ListItem value="C" Text="Central Zone"></asp:ListItem>
                  <asp:ListItem value="N" Text="North Zone"></asp:ListItem>
                  <asp:ListItem value="S" Text="South Zone"></asp:ListItem>
                  <asp:ListItem value="E" Text="East Zone"></asp:ListItem>
                  <asp:ListItem value="W" Text="West Zone"></asp:ListItem>
            <asp:ListItem value="NE" Text="North Eastern Zone"></asp:ListItem>
                
                 </asp:DropDownList>
                        <asp:RequiredFieldValidator class="auto-style2" ID="reqzone6" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlzone6" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                   
             
             
                    
                   </td>
            </tr>
              <tr>
               
                <td >
                   <b style="font-size:small">Select Medium of Question Paper <font style="color:red;">*</font>

                </td>
                 <td class="auto-style3" >
                      
                 <asp:DropDownList ID="ddlmed"  class="form-control select2" runat="server" width="300px" ValidationGroup="exmdet">
                     <asp:ListItem value="0" Text="--Select Medium--"></asp:ListItem>
                     <asp:ListItem value="1" Text="English"></asp:ListItem>
                     <asp:ListItem value="2" Text="Hindi"></asp:ListItem>
                      <asp:ListItem value="3" Text="Bilingual (English & Hindi)"></asp:ListItem>
                 </asp:DropDownList>
                      <asp:RequiredFieldValidator class="auto-style2" ID="reqmed" ValidationGroup="cadndet" runat="server" ControlToValidate="ddlmed" InitialValue="0" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true" ></asp:RequiredFieldValidator>
                     
                   </td>
            </tr>
                   </table>
                               
       
              </div>
            <div id="dvedu" runat="server">
                 <table class="table table-bordered table-hover">
             <tr><td colspan="2"><h4 class="auto-style9"><b>PART- C : </b>EDUCATIONAL / PROFESSIONAL QUALIFICATION</h4></td></tr>
              <tr>
               
                <td colspan="2">
                  <table class="table table-bordered" style="font-size: small">
                      <tr>
                          <th class="auto-style5"></th><th class="auto-style7">Name of Degree</th><th class="auto-style7">Year of Passing</th><th>University/Institution/Board</th><th>Percentage Obtained<br />(Put Only Numbers)</th>
                      </tr>
                      <tr><td class="auto-style5"><b>10/Equivalent</b><b style="font-size:small"> <font style="color:red;">*</font></b><b> </b>

                </td><td class="auto-style7">&nbsp;</td><td class="auto-style7"><asp:TextBox ID="txtyr10" runat="server" class="form-control" autocomplete="off" width="70px" MaxLength="4" required ValidationGroup="edudet" ></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator" Operator="LessThanEqual" type="String" ControltoValidate="txtyr10" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /><asp:RegularExpressionValidator ID="regyr10" runat="server" ErrorMessage="*Numbers
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyr10" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator></td><td><asp:TextBox ID="txtuni10" runat="server" class="form-control" width="300px" MaxLength="100" required ValidationGroup="cadndet" autocomplete="off"></asp:TextBox>   <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtuni10" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Only Alphabets Allowed" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" />
                  </td><td><asp:TextBox ID="txtperc10" runat="server" class="form-control" width="60px" MaxLength="5" required ValidationGroup="cadndet" autocomplete="off"></asp:TextBox> <asp:RegularExpressionValidator ID="regrail" runat="server" ControlToValidate="txtperc10" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" ></asp:RegularExpressionValidator>  <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" Font-Bold="true" ForeColor="Red" ControlToValidate="txtperc10" ValidationGroup="cadndet" CssClass="auto-style2"></asp:CustomValidator>
                            
              </td></tr>
                        <tr><td class="auto-style5"><b>10+2/Equivalent</b><b style="font-size:small"> <font style="color:red;">*</font></b>

                <b>

                <br />
                           
                            </b>
                            </td><td class="auto-style7">&nbsp;</td><td class="auto-style7"><asp:TextBox ID="txtyr12" runat="server" class="form-control" width="70px" MaxLength="4" required ValidationGroup="edudet" onchange="return checkyear(this);" autocomplete="off"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator0" Operator="LessThanEqual" type="String" ControltoValidate="txtyr12" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyr12" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator></td><td><asp:TextBox ID="txtuni12" runat="server" class="form-control" width="300px" MaxLength="100" required ValidationGroup="cadndet" autocomplete="off"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtuni12" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Only Alphabets Allowed" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" /></td><td><asp:TextBox ID="txtperc12" runat="server" class="form-control" width="60px" MaxLength="5" required ValidationGroup="cadndet" autocomplete="off"></asp:TextBox> <asp:RegularExpressionValidator ID="regrail0" runat="server" ControlToValidate="txtperc12" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" ></asp:RegularExpressionValidator><asp:CustomValidator ID="CustomValidator2" runat="server" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" Font-Bold="true" ForeColor="Red" ControlToValidate="txtperc12" ValidationGroup="cadndet" CssClass="auto-style2"></asp:CustomValidator>
                            </td></tr>
                        <tr><td class="auto-style10">Diploma</td><td class="auto-style7">
                              <%-- <script type="text/javascript">
      
                  function disbale(element)
                  {
                      var button = document.getElementById(element);
                      button.value = "Please Wait...";
                      button.disable = true;
                  }
    </script>--%>

                               </td>
                            <td class="auto-style7"><asp:TextBox ID="txtyrdip" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);" autocomplete="off"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator1" Operator="LessThanEqual" type="String" ControltoValidate="txtyrdip" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /><asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrdip" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator></td><td><asp:TextBox ID="txtunidip" runat="server" class="form-control" width="300px" MaxLength="100" autocomplete="off"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtunidip" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Only Alphabets Allowed" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" /></td><td><asp:TextBox ID="txtpercdip" runat="server" class="form-control" width="60px" MaxLength="5" autocomplete="off"></asp:TextBox><asp:RegularExpressionValidator ID="regrail5" runat="server" ControlToValidate="txtpercdip" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ></asp:RegularExpressionValidator><asp:CustomValidator ID="CustomValidator3" runat="server" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" Font-Bold="true" ForeColor="Red" ControlToValidate="txtpercdip" ValidationGroup="cadndet" CssClass="auto-style2"></asp:CustomValidator>
                            </td></tr>
                        <tr><%-- <asp:LinkButton ID="lnkOk" runat="server"></asp:LinkButton>--%></tr>
                        <tr><%--   <div align="center">
 
                <div class="modal" align="center">
        <div class="center">
            <img alt="" src="img/loader.gif" />
        </div>
    </div>--%></tr>
                        <tr><td class="auto-style5"><b>Graduation</b><b><br />
                          </b>
                          </td><td class="auto-style7">
                              <asp:DropDownList ID="ddlgrad" runat="server" CssClass="form-control" Width="100px" AppendDataBoundItems="True">
                                 <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                       </asp:DropDownList>
                              <asp:RequiredFieldValidator ID="reqgrad" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="ddlgrad" InitialValue="0" ValidationGroup="cadndet"></asp:RequiredFieldValidator>

                               </td>
                            <td class="auto-style7"><asp:TextBox ID="txtyrgrad" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);" autocomplete="off"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator2" Operator="LessThanEqual" type="String" ControltoValidate="txtyrgrad" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrgrad" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="reqgradyr" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtyrgrad" ValidationGroup="cadndet"></asp:RequiredFieldValidator></td><td><asp:TextBox ID="txtunigrad" runat="server" class="form-control" width="300px" MaxLength="100" autocomplete="off"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtunigrad" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Only Alphabets Allowed" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" /><asp:RequiredFieldValidator ID="reqgraduni" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtunigrad" ValidationGroup="cadndet"></asp:RequiredFieldValidator></td><td><asp:TextBox ID="txtpercgrad" runat="server" class="form-control" width="60px" MaxLength="5" autocomplete="off"></asp:TextBox><asp:RequiredFieldValidator ID="reqgradperc" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtpercgrad" ValidationGroup="cadndet"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regrail1" runat="server" ControlToValidate="txtpercgrad" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet"></asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="CustomValidator4" runat="server" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" Font-Bold="true" ForeColor="Red" ControlToValidate="txtpercgrad" ValidationGroup="cadndet" CssClass="auto-style2"></asp:CustomValidator></td></tr>
                    <tr><td class="auto-style10">Post Graduation
                          </td><td class="auto-style7"><asp:DropDownList ID="ddlpostgrad" runat="server" CssClass="form-control" Width="100px" AppendDataBoundItems="True">
                                 <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                       </asp:DropDownList>
                              <asp:RequiredFieldValidator ID="reqpg" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="ddlpostgrad" InitialValue="0" ValidationGroup="cadndet"></asp:RequiredFieldValidator>

                               </td><td class="auto-style7"><asp:TextBox ID="txtyrpg" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);" autocomplete="off"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator3" Operator="LessThanEqual" type="String" ControltoValidate="txtyrpg" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrpg" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="reqyrpg" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtyrpg" ValidationGroup="cadndet"></asp:RequiredFieldValidator></td><td><asp:TextBox ID="txtunipg" runat="server" class="form-control" width="300px" MaxLength="100" autocomplete="off"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="txtunipg" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="*Only Alphabets Allowed" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" CssClass="auto-style2" /><asp:RequiredFieldValidator ID="requnipg" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtunipg" ValidationGroup="cadndet"></asp:RequiredFieldValidator></td><td><asp:TextBox ID="txtpercpg" runat="server" class="form-control" width="60px" MaxLength="5" autocomplete="off"></asp:TextBox> <asp:RequiredFieldValidator ID="reqpercpg" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtpercpg" ValidationGroup="cadndet"></asp:RequiredFieldValidator> 
                        <asp:RegularExpressionValidator ID="regrail2" runat="server" ControlToValidate="txtpercpg" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" ></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="CustomValidator5" runat="server" ControlToValidate="txtpercpg" CssClass="auto-style2" Font-Bold="true" ForeColor="Red" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" ValidationGroup="cadndet"></asp:CustomValidator>
                        </td></tr>
                   
                      <tr><td class="auto-style10">D.Ed/B.El.Ed
                          </td><td class="auto-style7">&nbsp;</td><td class="auto-style7"><asp:TextBox ID="txtyrded" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);" autocomplete="off"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator4" Operator="LessThanEqual" type="String" ControltoValidate="txtyrded" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /><asp:RequiredFieldValidator ID="reqyrded" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtyrded" ValidationGroup="cadndet"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrded" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator></td><td><asp:TextBox ID="txtunided" runat="server" class="form-control" width="300px" MaxLength="100" autocomplete="off"></asp:TextBox>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ClientIDMode="AutoID" ControlToValidate="txtunided" CssClass="auto-style2" ErrorMessage="*Only Alphabets Allowed" Font-Bold="true" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$" ValidationGroup="cadndet" />
                              <asp:RequiredFieldValidator ID="requnided" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtunided" ValidationGroup="cadndet"></asp:RequiredFieldValidator></td><td><asp:TextBox ID="txtpercded" runat="server" class="form-control" width="60px" MaxLength="5" autocomplete="off"></asp:TextBox> <asp:RequiredFieldValidator ID="reqpercded" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtpercded" ValidationGroup="cadndet"></asp:RequiredFieldValidator> 
                          <asp:RegularExpressionValidator ID="regrail3" runat="server" ControlToValidate="txtpercded" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" ></asp:RegularExpressionValidator>
                          <asp:CustomValidator ID="CustomValidator6" runat="server" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" Font-Bold="true" ForeColor="Red" ControlToValidate="txtpercded" ValidationGroup="cadndet" CssClass="auto-style2"></asp:CustomValidator></td></tr>
                       <tr><td class="auto-style10">B.Ed/Equivalent
                          </td><td class="auto-style7">&nbsp;</td><td class="auto-style7"><asp:TextBox ID="txtyrbed" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);" autocomplete="off"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator5" Operator="LessThanEqual" type="String" ControltoValidate="txtyrbed" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /><asp:RequiredFieldValidator ID="reqyrbed" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtyrbed" ValidationGroup="cadndet"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrbed" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator></td><td><asp:TextBox ID="txtunibed" runat="server" class="form-control" width="300px" MaxLength="100" autocomplete="off"></asp:TextBox>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ClientIDMode="AutoID" ControlToValidate="txtunibed" CssClass="auto-style2" ErrorMessage="*Only Alphabets Allowed" Font-Bold="true" ForeColor="Red" ValidationExpression="[a-zA-Z ]*$" ValidationGroup="cadndet" />
                               <asp:RequiredFieldValidator ID="requnibed" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtunibed" ValidationGroup="cadndet"></asp:RequiredFieldValidator></td><td><asp:TextBox ID="txtpercbed" runat="server" class="form-control" width="60px" MaxLength="5" autocomplete="off"></asp:TextBox> <asp:RequiredFieldValidator ID="reqpercbed" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtpercbed" ValidationGroup="cadndet"></asp:RequiredFieldValidator> 
                           <asp:RegularExpressionValidator ID="regrail4" runat="server" ControlToValidate="txtpercbed" ValidationExpression="^[0-9]\d*(\.\d+)?$" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationGroup="cadndet" ></asp:RegularExpressionValidator>
                           <asp:CustomValidator ID="CustomValidator7" runat="server" OnServerValidate="ServerValidation2" Text="*Should be between 0 and 100" Font-Bold="true" ForeColor="Red" ControlToValidate="txtpercbed" ValidationGroup="cadndet" CssClass="auto-style2"></asp:CustomValidator></td></tr>
                       <tr><td class="auto-style10">CTET Paper-I<br />
                           (Enter Marks Obtained For Applying for PRT Posts)<br />
                           <span class="auto-style12">CTET certificate is valid for 7 years from the date of declaration of result</span></td><td class="auto-style7">&nbsp;</td><td class="auto-style7"><asp:TextBox ID="txtyrctet" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);" autocomplete="off"></asp:TextBox><asp:CompareValidator ID="CompareEndTodayValidator6" Operator="LessThanEqual" type="String" ControltoValidate="txtyrctet" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" /><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrctet" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="reqyrctet" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtyrctet" ValidationGroup="cadndet"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareEndTodayValidator7" Operator="GreaterThanEqual" Type="String" ControltoValidate="txtyrctet" ErrorMessage="*Invalid due to CTET Validity is 7 years" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /></td><td>&nbsp;</td><td><asp:TextBox ID="txtpercctet" runat="server" class="form-control" width="60px" MaxLength="5" autocomplete="off"></asp:TextBox><asp:RegularExpressionValidator ID="regpercctet" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpercctet" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="reqpercctet" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="*Required" ControlToValidate="txtpercctet" ValidationGroup="cadndet"></asp:RequiredFieldValidator>
                           <asp:CustomValidator ID="CustomValidator8" runat="server" ControlToValidate="txtpercctet" CssClass="auto-style2" Font-Bold="true" ForeColor="Red" OnServerValidate="ServerValidationCtet" Text="*Should be between 0 and 150"  ValidationGroup="cadndet"></asp:CustomValidator>
                           </td></tr>
                      <tr><td class="auto-style10">CTET Paper-II<br />
                           (Enter Marks Obtained For Applying for TGT Posts)<br />
                          <span class="auto-style12">CTET certificate is valid for 7 years from the date of declaration of result</span></td><td class="auto-style7">&nbsp;</td><td class="auto-style7"><asp:TextBox ID="txtyrctet2" runat="server" class="form-control" width="70px" MaxLength="4" onchange="return checkyear(this);" autocomplete="off"></asp:TextBox><asp:CompareValidator ID="compctet2" Operator="LessThanEqual" type="String" ControltoValidate="txtyrctet2" ErrorMessage="*Invalid" runat="server" ForeColor="Red" Font-Bold="true" /><asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtyrctet2" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator><asp:CompareValidator ID="compctet3" Operator="GreaterThanEqual" Type="String" ControltoValidate="txtyrctet2" ErrorMessage="*Invalid due to CTET Validity is 7 years" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="cadndet" /></td><td>&nbsp;</td><td><asp:TextBox ID="txtpercctet2" runat="server" class="form-control" width="60px" MaxLength="5" autocomplete="off"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="*Numbers 
                          Only" ForeColor="Red" Font-Bold="true" ControlToValidate="txtpercctet2" ValidationExpression="\d+" ValidationGroup="cadndet"></asp:RegularExpressionValidator>
                           <asp:CustomValidator ID="CustomValidator9" runat="server" ControlToValidate="txtpercctet2" CssClass="auto-style2" Font-Bold="true" ForeColor="Red" OnServerValidate="ServerValidationCtet" Text="*Should be between 0 and 150"  ValidationGroup="cadndet"></asp:CustomValidator>
                           </td></tr>
                  </table>
                    
           
                   
                   </td>
            </tr>
                </table>
                 <div class="text-justify" style="font-size:small; display:block">
         <strong>I hereby declare that:</strong><br /><br />

             <li> I have gone through the "<strong>General Instructions</strong>" and shall abide by the same.</li>  
                    <li>The information filled in the application are true, complete and correct to the best of my knowledge and belief.</li>  
                 
                    <li> I fulfill all the conditions of eligibility prescribed for the post applied for.</li>  
                    <li>I have never been debarred by any organisation for any illegal activity during my education/service.</li>  
                    <li> I understand that merely submission of online application form does not imply the fulfilling of eligibility criteria for the applied post.
</li>  
                    <li>I understand that in the event of any information found false/ incorrect/suppressed or any ineligibility being detected before or after the test/interview/ selection my candidature is liable to be cancelled and no correspondence will be entertained by KVS in this regard.
</li>  
                     <li>After the examination or at the time of interview, the request for change of particulars especially for change of category / sub-category will not be entertained by KVS.
</li>  



                    <br /><br />
                 
                   <div align="center">
                           <asp:Button ID="btncont1" runat="server" CssClass="btn btn-lg btn-danger" Text="I Agree, And Continue" ValidationGroup="cadndet" OnClick="btncont1_Click"   />
                <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
             </div>
                    </div>
                </div>
            <%-- <script type="text/javascript">
      
                  function disbale(element)
                  {
                      var button = document.getElementById(element);
                      button.value = "Please Wait...";
                      button.disable = true;
                  }
    </script>--%>  <cc2:ToolkitScriptManager ID="ToolkitScriptManager1"  AjaxFrameworkMode="Enabled" runat="server">
</cc2:ToolkitScriptManager>
      <!-- ModalPopupExtender -->
<cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2" TargetControlID="lnkDummy"
     BackgroundCssClass="modalBackground">
</cc2:ModalPopupExtender>
<asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" style = "display:none">
<asp:UpdatePanel ID="UpdatePanel2" runat="server" >
        <ContentTemplate>
         <br />
        <br />
   <h4><b> Data Once Submited Cannot be Changed.Please Review and Continue </b></h4><br /><br />
           
 <table class="table table-bordered table-hover" id="tblpreview" runat="server" >
        <tr>
            <td style="font-size:small;font-weight:bold">Name</td><td colspan="3" style="font-size:small"><asp:Label ID="lblname" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Mother's Name</td><td style="font-size:small"><asp:Label ID="lblmname" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Father's Name</td><td style="font-size:small"><asp:Label ID="lblfname" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Marital Status</td><td style="font-size:small"><asp:Label ID="lblmar" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Gender</td><td style="font-size:small"><asp:Label ID="lblgen" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Religion</td><td style="font-size:small"><asp:Label ID="lblrel" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Category</td><td style="font-size:small"><asp:Label ID="lblcat" runat="server" ></asp:Label></td>
        </tr>
        <tr><td style="font-size:small;font-weight:bold">Are you differently abled with 40% or above disability?</td><td style="font-size:small"><asp:Label ID="lblpwd" runat="server"></asp:Label></td><td style="font-size:small;font-weight:bold">PWD Category</td><td> <asp:Label ID="lblpwdcat" runat="server" style="font-size: small"></asp:Label><br /><asp:Label ID="lblscribe" runat="server" style="font-size: small"></asp:Label></td></tr>
          <tr>
            <td style="font-size:small;font-weight:bold">Are you a KVS employee?</td><td style="font-size:small"><asp:Label ID="lblkvemp" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Are you a Central Government employee with more than or equal to 3 years of regular service?</td><td style="font-size:small"><asp:Label ID="lblcgemp" runat="server"></asp:Label><div id="dvcglenservlbl" runat="server"><br /> <asp:Label ID="lblcgservlen" runat="server"></asp:Label></div></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Are you ordinarily domiciled in the state of Jammu & Kashmir during 01.01.1980 to 31.12.1989?</td><td style="font-size:small"><asp:Label ID="lbljk" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Are you an Ex-Serviceman?</td><td style="font-size:small"><asp:Label ID="lblexserv" runat="server"></asp:Label><div id="dvexservlenlbl" runat="server"><br /><asp:Label ID="lblexservlen" runat="server"></asp:Label></div></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Visible Identification Mark</td><td style="font-size:small"><asp:Label ID="lbliden" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Date of Birth</td><td style="font-size:small"><asp:Label ID="lbldob" runat="server"></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold">Address</td><td style="font-size:small" colspan="3"><asp:Label ID="lbladd" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Email ID</td><td style="font-size:small"><asp:Label ID="lblemail" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Contact No.</td><td style="font-size:small"><asp:Label ID="lblcon" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Post Applied</td><td style="font-size:small"><asp:Label ID="lblpost" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Do You Fulfill Essential Qualifications?</td><td style="font-size:small"><asp:Label ID="lblqual" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Exam Cities</td><td style="font-size:small"><asp:Label ID="lblcity1" runat="server" ></asp:Label><br /><asp:Label ID="lblcity2" runat="server" ></asp:Label><br /><asp:Label ID="lblcity3" runat="server" ></asp:Label><br /><asp:Label ID="lblcity4" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Medium of Question Paper</td><td style="font-size:small"><asp:Label ID="lblmed" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Class 10/Equivalent</td><td style="font-size:small"><asp:Label ID="lblyr10" runat="server" ></asp:Label><br /><asp:Label ID="lbluni10" runat="server" ></asp:Label><br /><asp:Label ID="lblperc10" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Class 12/Equivalent</td><td style="font-size:small"><asp:Label ID="lblyr12" runat="server" ></asp:Label><br /><asp:Label ID="lbluni12" runat="server" ></asp:Label><br /><asp:Label ID="lblperc12" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold">Diploma</td><td style="font-size:small"><asp:Label ID="lblyrdip" runat="server" ></asp:Label><br /><asp:Label ID="lblunidip" runat="server" ></asp:Label><br /><asp:Label ID="lblpercdip" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">B.El.Ed/D.Ed</td><td style="font-size:small"><asp:Label ID="lblyrded" runat="server" ></asp:Label><br /><asp:Label ID="lblunided" runat="server" ></asp:Label><br /><asp:Label ID="lblpercded" runat="server" ></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold">Graduation</td><td style="font-size:small"><asp:Label ID="lblyrgrad" runat="server" ></asp:Label><br /><asp:Label ID="lblunigrad" runat="server" ></asp:Label><br /><asp:Label ID="lblpercgrad" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Post-Graduation</td><td style="font-size:small"><asp:Label ID="lblyrpg" runat="server" ></asp:Label><br /><asp:Label ID="lblunipg" runat="server" ></asp:Label><br /><asp:Label ID="lblpercpg" runat="server" ></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold">B.Ed</td><td style="font-size:small"><asp:Label ID="lblyrbed" runat="server" ></asp:Label><br /><asp:Label ID="lblunibed" runat="server" ></asp:Label><br /><asp:Label ID="lblpercbed" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">CTET</td><td style="font-size:small"><b>Paper I:</b><br /><asp:Label ID="lblyrctet" runat="server" ></asp:Label><br /><asp:Label ID="lblpercctet" runat="server" ></asp:Label><br /><b>Paper II:</b><br /><asp:Label ID="lblyrctet2" runat="server" ></asp:Label><br /><asp:Label ID="lblpercctet2" runat="server" ></asp:Label></td>
        </tr>
       
        </table>
   
      
   <asp:Label ID="lblfee" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label><br />
             <asp:Label ID="lblfeeapp" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
   <br />
    <asp:Button ID="btnOk" runat="server" Text="SUBMIT STEP 1" OnClick="btnOK_Click" CssClass="btn btn-md btn-success" OnClientClick="if(!confirm('DATA ONCE SUBMITTED CANNOT BE CHANGED. CLICK CANCEL TO MAKE CHANGES OR OK TO SUBMIT')) return false;" />
   
            <%--  <asp:Button ID="btnOk" runat="server" Text="SUBMIT STEP 1" OnClick="btnOK_Click" CssClass="btn btn-md btn-success" OnClientClick="return myFunction();" />--%>
   
      &nbsp;
      <asp:Button ID="btnCancel" runat="server" Text="MAKE CHANGES" OnClick="btnCancel_Click" CssClass="btn btn-md btn-danger" />
            <br />
      <asp:Button ID="btnLog" runat="server" Text="Go To Log In" OnClick="btnLog_Click" CssClass="btn btn-md btn-info" />
<br />
<br />   
      <br />
        <script>
function myFunction() {
    var txt;
    var r = confirm("DATA ONCE SUBMITTED CANNOT BE CHANGED. CLICK CANCEL TO MAKE CHANGES OR OK TO SUBMIT");
    if (r == true) {            
        var inputs = document.getElementsByTagName("INPUT");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type == "button" || inputs[i].type == "submit") {
                inputs[i].disabled = true;
                inputs[i].value = "Please Wait...";
            }
        }
       
    } else {
       
       
    }
  
}
</script>
           
</div>
    </ContentTemplate>
   
    </asp:UpdatePanel>  
  
</asp:Panel>
<!-- ModalPopupExtender --> 
             <div align="center" >
 
                <div class="modal" align="center">
        <div class="center">
            <img alt="" src="img/loader.gif" />
        </div>
    </div>
           
</div>

             <%-- <script type="text/javascript">
      
                  function disbale(element)
                  {
                      var button = document.getElementById(element);
                      button.value = "Please Wait...";
                      button.disable = true;
                  }
    </script>--%>

            <script type="text/javascript">
        window.onbeforeunload = function () {
            var inputs = document.getElementsByTagName("INPUT");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "button" || inputs[i].type == "submit") {
                    inputs[i].disabled = true;
                    inputs[i].value = "Please Wait...";
                }
            }
        };
    </script>
     
                </div>
           </div>
      <div id="dvsuccess" runat="server">
         
      </div>
         </center> 
        <hr />
               
        
        </div>
                     </div>
              </div></div>
         </div>
                       </div>
            </div>
        
                      
   </section>
        </div>
       <script src="plugins/datepicker/bootstrap-datepicker.js"></script>
        <script>
            //Date picker
            $('#<%=datepicker.ClientID%>').datepicker({
                autoclose: true,
               
            });
        </script>
    
 <%-- <script type="text/javascript">
    function WebForm_OnSubmit() {
        if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
            for (var i in Page_Validators) {
                try {
                } catch (e) { }
            }
            return false;
        }
        ShowProgress();
        return true;
    }
    </script>--%>
  
     
</asp:Content>
