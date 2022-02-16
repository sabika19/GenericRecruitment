<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/kvs.Master" CodeBehind="appforgot.aspx.cs" Inherits="kvs.appforgot" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="rsv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
      

      <link rel="stylesheet" href="http://travbeast.com/css/jquery.datepick.css" />
    <script src="http://travbeast.com/js/jquery.plugin.js"></script>
    <script src="http://travbeast.com/js/jquery.datepick.js"></script>
 <style type="text/css">
      .thumb-image{float:left;width:300px;position:relative;padding:5px;}
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
        width: 500px;
        height: 500px;
    }
     .auto-style2 {
        font-size: medium;
    }
     </style>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
  <title>KVS | Recruitment</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.6 -->
  <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
  <!-- jvectormap -->
  <link rel="stylesheet" href="plugins/jvectormap/jquery-jvectormap-1.2.2.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css">
    
<!--Progress Bar-->
   <link rel="stylesheet" href="progress/css/progress-wizard.min.css">
 

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
</script>
     <script type = "text/javascript" >
      function burstCache() {
        if (!navigator.onLine) {
           // document.body.innerHTML = 'Loading...';
            window.location = 'Oops.html';
        }
    }
</script>
      <!-- bootstrap datepicker -->
  <link rel="stylesheet" href="plugins/datepicker/datepicker3.css" />
    <script src="plugins/datepicker/bootstrap-datepicker.js"></script>
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<div class="wrapper">

  
  <!-- Left side column. contains the logo and sidebar -->
   <div class="container">
      <section class="content">
    
    <!-- Main content -->
        <div class="row">
        <div class="col-md-12">

          <div class="box box-success">
            <div class="box-header with-border">
              <h4 class="box-title" style="text-align:center; width: 100%;"><strong>Direct Recruitment Drive of Teaching Posts in Kendriya Vidyalaya Sangathan</strong></h4>

            
            </div>
            <div class="box-body">
            
                  <div class="text-justify" >
    <center>
         <h4 class="auto-style7"><strong>FORGOT REGISTRATION NUMBER</strong></h4>
        
        <table class="table table-bordered table-hover" style="font-size: small" >
            <tr><td colspan="4" class="auto-style5"><h4 class="auto-style4"><strong>ENTER DETAILS</strong></h4></td></tr>
           <tr>
                <td style="width:50%" class="auto-style2" >
                    <strong>First Name</strong></td>
               <td  class="auto-style3"><strong><asp:TextBox ID="txtcnamef" runat="server" CssClass="form-control" Width="300px" autocomplete="off" required></asp:TextBox>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtcnamef" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Invalid Name" Font-Bold="true" ForeColor="Red" CssClass="auto-style2" />
              
                   </strong>
               </td> 
            </tr>
             <tr>
                <td style="width:50%" class="auto-style2" >
                    <strong>Last Name</strong></td>
               <td  class="auto-style3"><strong><asp:TextBox ID="txtcnamel" runat="server" CssClass="form-control" Width="300px" autocomplete="off" ></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtcnamel" ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Invalid Name" Font-Bold="true" ForeColor="Red" CssClass="auto-style2" />
              
                   </strong>
               </td> 
            </tr>
              <tr>
                <td class="auto-style2" >
                    <strong>Mobile Number</strong></td>
               <td  class="auto-style3"><strong><asp:TextBox ID="txtmob" runat="server" MaxLength="10" CssClass="form-control" Width="300px" autocomplete="off" required></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtmob" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationExpression="^[789]\d{9}$" CssClass="auto-style2"></asp:RegularExpressionValidator>
                   </strong>
               </td>
                   
            </tr>
             <tr>
                <td class="auto-style2" >
                    <strong>Email ID</strong></td>
               <td  class="auto-style3"><strong><asp:TextBox ID="txtemail" TextMode="email" runat="server" MaxLength="100" CssClass="form-control" Width="300px" autocomplete="off" required></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtemail" ErrorMessage="*Invalid" Font-Bold="true" ForeColor="Red" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" CssClass="auto-style2"></asp:RegularExpressionValidator>
                   </strong>
               </td>
                   
            </tr>
             <tr>
                <td class="auto-style2" >
                    <strong>Date of Birth</strong></td>
               <td  class="auto-style3"><strong><asp:TextBox ID="datepicker" runat="server" Placeholder="dd/mm/yyyy" CssClass="form-control" Width="300px" autocomplete="off" required></asp:TextBox>
                   </strong>
               </td>
                   
            </tr>
             <tr><td colspan="2" style="text-align:center"><strong> <asp:Label ID="lblMsg" ForeColor="Red" Font-Bold="true" runat="server" CssClass="auto-style2"></asp:Label><br /> <asp:Button ID="btnagree" runat="server" CssClass="btn btn-lg btn-danger" Text="Retrieve Details" OnClick="btnagree_Click" />
        </strong>
                </td></tr>
          
              
        </table>
         </center>
               
        
        </div>
                     </div>
              </div></div>
         </div>
    </section>
    </div>
    </div>
      <!--datepicker-->
        <script src="plugins/datepicker/bootstrap-datepicker.js"></script>
        <script>
            //Date picker
            $('#<%=datepicker.ClientID%>').datepicker({
                autoclose: true
            });
        </script>

</asp:Content>