<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="admn.Master" CodeBehind="admnlogin.aspx.cs" Inherits="kvs.admnlogin" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="rsv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    

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
  <title>Recruitment</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.6 -->
  <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
  <!-- jvectormap -->
  <link rel="stylesheet" href="../plugins/jvectormap/jquery-jvectormap-1.2.2.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="../dist/css/AdminLTE.min.css">
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href="../dist/css/skins/_all-skins.min.css">
    
<!--Progress Bar-->
   <link rel="stylesheet" href="../progress/css/progress-wizard.min.css">
 

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
    
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server" onload="noBack();">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<div class="wrapper">

  
  <!-- Left side column. contains the logo and sidebar -->
    <div class="container">
<div class="login-box">
  <div class="login-logo">
 
    </div>
    <div align="center">
    <a href="http://cbse.nic.in"><h2><b>KVS</b>Recruitment</h2><br />Admin Login</a>
    
    <!-- Main content -->
       
    
  <div class="login-box-body">
  
   <div id="chckdate" runat="server">
    <p class="login-box-msg"><b>Log In<br /><br /><font style="color:Red; font-size:small"></font> </b></p>

   
      <div class="form-group has-feedback">
        <asp:TextBox ID="txtUser1"  class="form-control" placeholder="User Name" runat="server"  required></asp:TextBox>
        <span class="glyphicon glyphicon-user form-control-feedback"></span>
     </div>
      <div class="form-group has-feedback">
        <asp:TextBox ID="txtpwd"  class="form-control" placeholder="Password" runat="server" type="password" autocomplete="off" required ></asp:TextBox>
       <span class="glyphicon glyphicon-lock form-control-feedback"></span>
   
      </div>
      <div class="row">
        <div class="col-xs-12">
          <div align="center">
            <%--<rsv:captchacontrol ID="captcha1" runat="server" CaptchaLength="5" CaptchaHeight="60"
                    CaptchaWidth="200" CaptchaLineNoise="None" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                    ForeColor="Red" BackColor="White" CaptchaChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789"
                    FontColor="Red" /> --%>
                <img src="../randimg.aspx" height="40" width="120" border="0" /><br />
               <asp:TextBox ID="TextBox1"  class="form-control" placeholder="Enter Captcha" runat="server"  autocomplete="off" ></asp:TextBox> 
          </div>
        </div>
      
        <!-- /.col -->
        <div class="col-xs-12">
        <br />    <asp:Button class="btn btn-primary btn-block btn-flat" Text="Log In" runat="server" ID="btnlogin" onclick="btnlogin_Click" />
        </div>
        <!-- /.col -->
      </div>
      <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    <div align="center">
 
                <div class="modal" align="center">
        <div class="center">
            <img alt="" src="../images/loader.gif" />
        </div>
    </div>
           
</div>

   
     </div>
  
   

  </div>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->
 <hr>

        <!-- Footer -->
        <footer>
            <div class="row">
                <div class="col-lg-12">
                    <p>Copyright &copy; </p>
                </div>
            </div>
        </footer>
        </div>
<!-- jQuery 2.2.0 -->
<script src="plugins/jQuery/jQuery-2.2.0.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="bootstrap/js/bootstrap.min.js"></script>
<!-- iCheck -->

<script>
    $(function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    });
</script>
<!-- ./wrapper -->

 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".modal");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    </script>
<!-- jQuery 2.2.0 -->
<script src="../plugins/jQuery/jQuery-2.2.0.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="../bootstrap/js/bootstrap.min.js"></script>
<!-- FastClick -->
<script src="../plugins/fastclick/fastclick.js"></script>
<!-- AdminLTE App -->
<script src="../dist/js/app.min.js"></script>
<!-- Sparkline -->
<script src="../plugins/sparkline/jquery.sparkline.min.js"></script>
<!-- jvectormap -->
<script src="../plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
<script src="../plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>
<!-- SlimScroll 1.3.0 -->
<script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
<!-- ChartJS 1.0.1 -->

<!-- AdminLTE for demo purposes -->
<script src="../dist/js/demo.js"></script>
        <!--datepicker-->
        <script src="plugins/datepicker/bootstrap-datepicker.js"></script>
     <%--   <script>
            //Date picker
            $('#<%=datepicker.ClientID%>').datepicker({
                autoclose: true
            });
        </script>--%>

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
</asp:Content>