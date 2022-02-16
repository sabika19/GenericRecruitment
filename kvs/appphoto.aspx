<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/kvs.Master" CodeBehind="appphoto.aspx.cs" Inherits="kvs.appphoto" %>
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
  <title>Recruitment</title>
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
              <h4 class="box-title" style="text-align:center; width: 100%;"><strong>Direct Recruitment Drive 2018</strong></h4>

              <div class="box-tools pull-right">
               
              </div>
            </div>
            <div class="box-body">
            
                  <div class="text-justify" >
    <center>
         <h4 class="auto-style7"><strong>STEP 2</strong></h4>
         <p class="auto-style7"><asp:Label ID="lblMsgOver" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label></p>
         <p class="auto-style7"><asp:Label ID="lblflag" runat="server" style="font-size: xx-small" /></p>
        
        <table class="table table-bordered table-hover" id="tblphoto" runat="server" style="font-size: small" >
            <tr><td colspan="4" class="auto-style5"><h4 class="auto-style4"><strong>CANDIDATE DETAILS</strong></h4></td></tr>
           <tr>
                <td class="auto-style2" >
                    <strong>Name</strong></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblcname" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong>
               </td>  <td class="auto-style2" >
                    <strong>Registration Number</strong></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblregn" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong>
               </td>
            </tr>
              <tr>
                <td class="auto-style2" >
                    <strong>Mother&#39;s Name</strong></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblmname" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong>
               </td>
                    <td class="auto-style2" >
                    <strong>Father's/Guardian's Name</strong></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblfname" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong>
               </td>
            </tr>
             <tr><td colspan="4" class="auto-style5"><h4 class="auto-style4"><strong>UPLOAD PHOTO &amp; SIGNATURE</strong></h4>
                 <p class="auto-style4"><asp:Label ID="lblMsg" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label></p>
                 </td></tr>
            
           <tr id="trphoto" runat="server">
                <td class="auto-style2" >
                    <strong>Photograph      <br />   <font style="color:red">Valid File Types : JPG<br />
                    Maximum File Size : 50 KB</font></strong></td>
               <td  class="auto-style3"><strong><asp:FileUpload ID="upphoto" runat="server" /><br class="auto-style2" />
                   <asp:RequiredFieldValidator ID="reqphoto" runat="server" ControlToValidate="upphoto" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true" CssClass="auto-style2"></asp:RequiredFieldValidator>
                   </strong>
               </td>  <td class="auto-style2" colspan="2">
                   <div id="image-holder1" style="width:100px; height:100px;"></div></td>
             
            </tr>
              <tr id="trsign" runat="server">
                <td class="auto-style2" >
                    <span class="ui-priority-primary"><strong>Signature<br />
                    </strong>
                    <font style="color:red"><strong>Valid File Types : JPG<br />
                    Maximum File Size : 20 KB</strong></font> </span></td>
               <td  class="auto-style3"><strong><asp:FileUpload ID="upsign" runat="server" /><br class="auto-style2" />
                   <asp:RequiredFieldValidator ID="reqsign" runat="server" ControlToValidate="upsign" ErrorMessage="*Required" ForeColor="Red" Font-Bold="true" CssClass="auto-style2"></asp:RequiredFieldValidator></strong>
               </td>
                   
              <td class="auto-style2" colspan="2">
                  <div id="image-holder2" style="width:100px; height:100px;"></div></td>
            </tr>
              
        </table>
         </center>
                <div class="text-justify" style="font-size:small" id="dvbutton" runat="server">
                    
                    <br /><br />
                        <br />
                    <div id="divbtn" runat="server" style="text-align:center">
                        <asp:Button ID="btnagree" runat="server" CssClass="btn btn-lg btn-danger" Text="Update Photo" OnClick="btnagree_Click" />
                    <strong>
                    <asp:Button ID="btnConf" runat="server" CssClass="btn btn-md btn-info" Text="Continue To Confirmation Page" OnClick="btnConf_Click" style="font-weight: bold" /></strong><br />
                   
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
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=upphoto.ClientID%>').on('change', function () {
            //Get count of selected files
            var countFiles = $(this)[0].files.length;
            var imgPath = $(this)[0].value;
            var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();
            var image_holder = $("#image-holder1");
            image_holder.empty();
            if (extn == "jpg") {
                if (typeof (FileReader) != "undefined") {
                    //loop for each file selected for uploaded.
                    var validFileSize = 50 * 1024;

                    var fileSize = this.files[0].size;
                    if (fileSize < validFileSize) {
                        for (var i = 0; i < countFiles; i++) {
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                $("<img />", {
                                    "src": e.target.result,
                                    "class": "thumb-image"
                                }).appendTo(image_holder);
                            }
                            image_holder.show();
                            reader.readAsDataURL($(this)[0].files[i]);
                        }
                    }
                    else {
                        alert("Photograph Should Be Less Than 50 KB.");

                        $(this).val('');
                        return false;
                    }
                } else {
                    alert("This browser does not support FileReader.");

                    $(this).val('');
                    return false;
                }
            } else {
                alert("Please select JPG  images only");
                $(this).val('');
                return false;
            }
        });
    });
</script>
<script>
    $(document).ready(function () {
        $('#<%=upsign.ClientID%>').on('change', function () {
            //Get count of selected files
            var countFiles = $(this)[0].files.length;
            var imgPath = $(this)[0].value;
            var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();
            var image_holder = $("#image-holder2");
            image_holder.empty();
            if (extn == "jpg") {
                if (typeof (FileReader) != "undefined") {
                    //loop for each file selected for uploaded.
                    var validFileSize = 20 * 1024;

                    var fileSize = this.files[0].size;
                    if (fileSize < validFileSize) {
                        for (var i = 0; i < countFiles; i++) {
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                $("<img />", {
                                    "src": e.target.result,
                                    "class": "thumb-image"
                                }).appendTo(image_holder);
                            }
                            image_holder.show();
                            reader.readAsDataURL($(this)[0].files[i]);
                        }
                    }
                    else {
                        alert("Signature Should Be Less Than 20 KB.");

                        $(this).val('');
                        return false;
                    }
                } else {
                    alert("This browser does not support FileReader.");

                    $(this).val('');
                    return false;
                }
            } else {
                alert("Please select JPG  images only");
                $(this).val('');
                return false;
            }
        });
    });
</script>
  
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
</asp:Content>