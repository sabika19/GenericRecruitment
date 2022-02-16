<%@ Page Title="" Language="C#" MasterPageFile="~/kvs.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="kvs.info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  

    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
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
  <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <script type="text/javascript">
        function buttonEnable(chkvalid) {
            var divbtn = document.getElementById("divbtn");
          

            if (chkvalid.checked == true) {
              
                divbtn.style.display = "block";
                
            }
            else {
               
                divbtn.style.display = "none";
            }
           
        }

</script>
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server"> 
    
  
    <div class="container">
               
                   <section class="content">
     <div class="row">
        <div class="col-md-12">

          <div class="box box-warning">
            <div class="box-header with-border">
              <h4 class="box-title"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Recruitment of Principal And Other Teaching Posts In Kendriya Vidyalaya Sangathan</strong></h4>

              <div class="box-tools pull-right">
              
              </div>
            </div>
            
                 <div id="divform" runat="server">
                      <div class="box-body" >
            
            <div id="secinfo" runat="server">
              <div class="col-md-12">
              
            <div class="text-justify" >
    <center>
         <h4>(Advertisement Number: )</h4>
       
        <table class="table table-bordered table-hover" >
           <tr>
                <td>
                   <b><a href="" target="_blank" style="font-size:small">Detailed Advertisement</a></b>
                </td>
            </tr>
             <tr>
               
                <td>
                   <b style="font-size:small">Start Date For Online Application : 15th September, 2018 11:30 am</b> 
                </td>
            </tr>
             <tr>
                
                <td>
                   <b style="font-size:small"> Last Date For Online Application :  30th September, 2018 05:30 pm</b>
                </td>
            </tr>
             <tr>
                
                <td>
                     <b><a href="imgsign.aspx" style="font-size:small">Upload Images (If STEP-1 of Registration has already been completed and Application Number received)</a></b>
                </td>
            </tr>
             <tr>
              
                <td>
                    <b><a href="payment.aspx" style="font-size:small">Make Payment & Complete Application (If STEP-1 and STEP-2 of Registration has already been completed and Application Number received)</a></b> 
                </td>
            </tr>
        </table>
         </center> 
        <hr />
                <div class="text-justify" style="font-size:small">
         <strong>Please read the Eligibility criteria carefully before proceeding for filling up the online application form:</strong><br /><br />

                <span class="auto-style1"><strong>Note:</strong></span> On-line application validation rules are designed based on the eligibility criteria and other requirements/information indicated in the eligibility criteria. <b>Applicants are required to read the eligibility criteria carefully and refer to the section "<a href="howtoapply.aspx">How to Apply</a>" from the top Menu.</b> Application submitted through online mode does not imply that candidate has fulfilled all the eligibility criterion/conditions. <strong>CBSE/KVS</strong> will not check/verify the educational/experience certificates etc. of the candidates at the time of appearing in the written examination and it will be the sole responsibility of the candidate to satisfy whether he/she is meeting the prescribed criterion. However, the documents will be verified at any point of time during process of recruitment. Applications are subject to further scrutiny at the time of interview and any point of time during the period of service. The application will be rejected if it is found that the applicant does not fulfil the eligibility criteria or has misrepresented/suppressed the information. 
                <em><strong>The applicants are requested to ensure their eligibility for the post being applied for before filling up their application.<br />
                    </strong></em><br />

                    <strong>Following are required to be kept handy before applying online</strong>
                 
<ul><li>Credit card/ Debit card / Bank details if Fee is applicable</li></ul>
                   
                    <ul><li>Scanned Photograph along with Signature (<strong>JPEG/JPG format, size less than 50 KB</strong>) Click here for sample Image</li></ul>
                   
                   <%-- <ul><li>Scanned copy in single PDF file(Size less than 2 MB) of essential educational qualification, experience, Date of birth, Caste Certificate (SC/ST/OBC), if applicable and Category Certificate (PWD or any other), if applicable</li></ul>
                  --%>  
                    <span class="auto-style1"><strong>Important Instructions:</strong></span> Candidates are advised to go through this advertisement in detail for determining their eligibility as per specified criteria for each post, instructions, selection procedure, mode of examination, etc. before applying. 
                    <br />
                    <br />
                    <span class="auto-style1"><strong>Steps for Online Registration:</strong></span>
                  <ul><li>  <strong>STEP 1:</strong> Submission of Applicant’s Details</li></ul>
                  <ul><li>  <strong>STEP 2:</strong> Uploading of scanned Photograph along with Signature</li></ul>
                    <ul><li>  <strong>STEP 3:</strong> Payment of application fee online, if applicable</li></ul>
                  
                    The application shall be treated complete only if all the three mandatory Steps (Step1, Step2 and Step3 (if applicable) ) are completed successfully. In case candidate is not able to submit fee (if applicable) by closing date and time, or the application is otherwise incomplete, his/her candidature will summarily be rejected. If fee is not applicable, candidate must click on '<strong>Submit Application</strong>' button at <b>STEP 2</b> to complete the application otherwise it will be treated as incomplete/rejected. 

                    <br />
                    <br />
                    Applicant can view the Application details from the <strong>View/Print Application</strong> menu option available on the home page by providing Application Number and Date of Birth. Applicant is required to make sure that "<strong>Application Status</strong>" on application form is “<strong>Submitted Successfully</strong>” otherwise application will be treated as incomplete and summarily rejected.

                    <br /><br />
                   <asp:CheckBox ID="chckok" CssClass="checkbox-inline" runat="server" Text="I have read all the above instructions and the eligibility criteria carefully." onclick="buttonEnable(this)" /><br />
                    <div id="divbtn" style="display:none;text-align:center">
                        <asp:Button ID="btnagree" runat="server" CssClass="btn btn-lg btn-danger" Text="Continue To Application" OnClick="btnagree_Click" />
                    </div>
                </div>
        
        </div>
                     </div>
              </div></div>
         </div>
                       </div>
            </div>
         </div>
                      
   </section>
        </div>

</asp:Content>