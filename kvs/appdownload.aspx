<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/kvs.Master" CodeBehind="appdownload.aspx.cs" Inherits="kvs.appdownload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <style type="text/css">
        .auto-style2 {
            font-size: small;
        }
        .auto-style3 {
            text-align: left;
        }
        .auto-style4 {
            font-size: large;
            font-weight: bold;
            text-align: center;
        }
        .auto-style5 {
            font-size: large;
            font-weight: bold;
        }
        .auto-style7 {
            font-size: medium;
        }
        </style>


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

              <div class="box-tools pull-right">
               
              </div>
            </div>
            <div class="box-body">
            
                  <div class="text-justify" >
    <center>
         <h4 class="auto-style7"><strong>STEP 2</strong></h4>
         <p class="auto-style7"><asp:Label ID="lblMsgOver" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label></p>
         <p class="auto-style7"><asp:Label ID="lblflag" runat="server" style="font-size: xx-small" /></p>
        
        
         </center>
                <div class="text-justify" style="font-size:small" id="dvbutton" runat="server">
                    
                    <br /><br />
                        <br />
                    <div id="divbtn" runat="server" style="text-align:center">
                        <asp:Button ID="btnagree" runat="server" CssClass="btn btn-md btn-danger" Text="Download Photo" OnClick="btnagree1_Click" />
                   <strong>
                   <%-- <asp:Button ID="btnConf" runat="server" CssClass="btn btn-md btn-info" Text="Download Sign" OnClick="btnConf_Click" style="font-weight: bold" /></strong><br />
                   --%>
                    </div>
                    
                    
                       <div class="modal" align="center">
        <div class="center">
            <img alt="" src="images/loader.gif" />
        </div>
    </div>
                </div>
        
        </div>
                     </div>
              </div></div>
         </div>
    </section>
    </div>
    </div>
           
       
           
        <div align="center">
 
                <div class="modal" align="center">
        <div class="center">
            <img alt="" src="img/loader.gif" />
        </div>
    </div>
           
</div>
    
  
<!-- ./wrapper -->

<!-- jQuery 2.2.0 -->
<script src="plugins/jQuery/jQuery-2.2.0.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="bootstrap/js/bootstrap.min.js"></script>
<!-- FastClick -->
<script src="plugins/fastclick/fastclick.js"></script>
<!-- AdminLTE App -->
<script src="dist/js/app.min.js"></script>
<!-- Sparkline -->
<script src="plugins/sparkline/jquery.sparkline.min.js"></script>
<!-- jvectormap -->
<script src="plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
<script src="plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>
<!-- SlimScroll 1.3.0 -->
<script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
<!-- ChartJS 1.0.1 -->

<!-- AdminLTE for demo purposes -->
<script src="dist/js/demo.js"></script>


<script type="text/javascript">
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
    </script>
    </strong>
</asp:Content>