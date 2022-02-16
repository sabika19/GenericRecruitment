﻿<%@ Page Title="" Language="C#" MasterPageFile="~/kvs.Master" AutoEventWireup="true" CodeBehind="appconf.aspx.cs" Inherits="kvs.appconf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
  
    <style type="text/css">
        .auto-style8 {
            font-size: medium;
            font-weight: bold;
            text-decoration: underline;
        }
        .auto-style9 {
            width: 25%;
        }
        .auto-style10 {
            width: 25%;
            text-align: right;
        }
        .auto-style11 {
            font-size: x-small;
            color: #FF0000;
        }
        .auto-style12 {
            width: 20%;
        }
        .auto-style13 {
            color: #FF0000;
        }
        .auto-style14 {
            font-size: x-small;
        }
        .auto-style15 {
            width: 20%;
            font-size: x-small;
            color: #FF0000;
        }
        </style>
    
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
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server" onload="noBack();burstCache();"> 
  


   

    <div class="container">
               
                   <section class="content">
     <div class="row">
        <div class="col-md-12">

          <div class="box box-warning">
            <div class="box-header with-border">
              <h4 class="box-title" style="text-align:center; width: 100%;"><strong>Direct Recruitment Drive 2018</strong></h4>

            </div>
            
                 <div id="divform" runat="server">
                      <div class="box-body" >
            
            <div id="secinfo" runat="server">
              <div class="col-md-12">
              
            <div class="text-justify" >
    <center>
         <h4 class="auto-style8">CONFIRMATION PAGE</h4>
        <table class="table table-bordered" >
          
        <tr>
           <td style="font-size:small;font-weight:bold" colspan="2" class="auto-style9">Registration Number : <asp:Label ID="lblcandid" runat="server" ></asp:Label>
               <br />
               Name : <asp:Label ID="lblname" runat="server" ></asp:Label>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Label ID="lblmarname" runat="server"></asp:Label><br />
               Mother's Name : <asp:Label ID="lblmname" runat="server" ></asp:Label>
               <br />
               Father's Name : <asp:Label ID="lblfname" runat="server"></asp:Label>
               <br />
               Post(s) Applied : <asp:Label ID="lblpost" runat="server" ></asp:Label>
               <br />
               Fee Applicable : &#8377 <asp:Label ID="lblfee" runat="server"></asp:Label>
               <br />
               <asp:Label ID="lbltxnid" runat="server"></asp:Label>
            </td>
            <td style="font-size:small;width:25%;text-align:right" colspan="2"><asp:Image ID="candimg" runat="server" Width="110px" Height="110px" /></td>
        </tr>
             <tr>
            <td style="font-size:medium;font-weight:bold;text-align:center" colspan="4" >Personal Details</td>
        </tr>
             <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9"> Date of Birth</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lbldob" runat="server"></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style9">Gender</td><td style="font-size:small" class="auto-style9"><asp:Label ID="lblgen" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Government ID</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblgovid" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style9">ID Number</td><td style="font-size:small" class="auto-style9"><asp:Label ID="lblgovidnum" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Religion</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblrel" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style9">Category   <span class="auto-style11">
             <br />
             No change of category will be permitted at any stage after registration of the online application and the result will be processed considering the category which has been indicated in the online application</span></td><td style="font-size:small" class="auto-style9"><asp:Label ID="lblcat" runat="server" ></asp:Label>
             <br />
                </td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Marital Status</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblmar" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style9">Visible Identification Mark</td><td style="font-size:small" class="auto-style9"><asp:Label ID="lbliden" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Are you differently abled with 40% or above disability?<br />
                   <b style="font-size:small">
                    <p class="MsoNormal" style="text-align: justify; font-size: x-small;">
                        &nbsp;</p>
                        <span class="auto-style12">
                <p class="MsoNormal" style="text-align: justify; font-size: x-small;">
                        <span class="auto-style13">
                        <b><span>* As per OM dated 15.01.2018 of DOPT the following categories to be given 1% reservation <o:p></o:p></span></b>
                    </p>
                    <p class="MsoNoSpacing" style="font-weight: bold; font-size: x-small">
                        (d) autism, intellectual disabilities, specific learning disabilities and mental illness<o:p></o:p></p>
                        </span>
                    <p class="MsoNoSpacing" style="margin-left: .25in; text-indent: -.25in; font-size: x-small;">
                        <span>(e) multiple disabilities from amongst persons under clause (a) to (d)&nbsp;&nbsp;&nbsp; including deaf –blindness.</span><o:p></o:p></span></span></p>

                </td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblpwd" runat="server"></asp:Label>
             <br />
             <asp:Label ID="lblpwdcat" runat="server"></asp:Label></td><td style="font-size:small;font-weight:bold">Do you need a scribe?</td><td style="font-size:small"><asp:Label ID="lblscribe" runat="server"></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Are you a current employee?</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblkvemp" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Are you a Central Government employee ?<br />
                   <b style="font-size:small">
                   <b class="auto-style11"> <font style="color:red;">Age relaxation is not applicable for government service below 3 years</font></b></td><td style="font-size:small"><asp:Label ID="lblcgemp" runat="server"></asp:Label><div id="dvcglenservlbl" runat="server"><asp:Label ID="lblcgservlen" runat="server"></asp:Label></div></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Are you ordinarily domiciled in the state of Jammu & Kashmir during 01.01.1980 to 31.12.1989?</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lbljk" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Are you an Ex-Serviceman?</td><td style="font-size:small"><asp:Label ID="lblexserv" runat="server"></asp:Label>
             <div id="dvexservlenlbl" runat="server"><asp:Label ID="lblexservlen" runat="server"></asp:Label></div></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Address</td><td style="font-size:small" colspan="3"><asp:Label ID="lbladd" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Email ID</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblemail" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Contact No.</td><td style="font-size:small"><asp:Label ID="lblcon" runat="server"></asp:Label></td>
        </tr>
              <tr>
            <td style="font-size:medium;font-weight:bold;text-align:center" colspan="4" >Examination Details</td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Do you Fulfill Essential Qualifications?</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblqual" runat="server"></asp:Label></td><td style="font-size:small;font-weight:bold">Do you Fulfill Desirable Qualifications?</td><td style="font-size:small"><asp:Label ID="lbldes" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Essential Qualifications Fulfilled</td><td style="font-size:small" colspan="3"><asp:Label ID="lblessqual" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Examination Cities Opted<br />
                    <b class="auto-style14"><span><font style="color:red;">The centre for the written examination for the post of Principal and Vice Principal will be at Delhi Only</font></span></b></td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblcity1" runat="server" ></asp:Label><br /><asp:Label ID="lblcity2" runat="server" ></asp:Label><br /><asp:Label ID="lblcity3" runat="server" ></asp:Label><br /><asp:Label ID="lblcity4" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Zonal Preference<br />
                   <b class="auto-style14"><span><font style="color:red;">Not Applicable for the post of Principal and Vice Principal</font></span></td><td style="font-size:small"><asp:Label ID="lblzone1" runat="server" ></asp:Label><br /><asp:Label ID="lblzone2" runat="server" ></asp:Label><br /><asp:Label ID="lblzone3" runat="server" ></asp:Label>
             <br />
             <asp:Label ID="lblzone4" runat="server" ></asp:Label>
             <br />
             <asp:Label ID="lblzone5" runat="server" ></asp:Label>
             <br />
             <asp:Label ID="lblzone6" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Medium of Question Paper</td><td style="font-size:small" colspan="3"><asp:Label ID="lblmed" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:medium;font-weight:bold;text-align:center" colspan="4" >Educational Qualification Details</td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Class 10/Equivalent</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblyr10" runat="server" ></asp:Label><br /><asp:Label ID="lbluni10" runat="server" ></asp:Label><br /><asp:Label ID="lblperc10" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Class 12/Equivalent</td><td style="font-size:small"><asp:Label ID="lblyr12" runat="server" ></asp:Label><br /><asp:Label ID="lbluni12" runat="server" ></asp:Label><br /><asp:Label ID="lblperc12" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Diploma</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblyrdip" runat="server" ></asp:Label><br /><asp:Label ID="lblunidip" runat="server" ></asp:Label><br /><asp:Label ID="lblpercdip" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">B.El.Ed/D.Ed</td><td style="font-size:small"><asp:Label ID="lblyrded" runat="server" ></asp:Label><br /><asp:Label ID="lblunided" runat="server" ></asp:Label><br /><asp:Label ID="lblpercded" runat="server" ></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">Graduation</td><td style="font-size:small" class="auto-style12">
              <asp:Label ID="lblgradsub" runat="server"></asp:Label><br />
              <asp:Label ID="lblyrgrad" runat="server" ></asp:Label><br /><asp:Label ID="lblunigrad" runat="server" ></asp:Label><br /><asp:Label ID="lblpercgrad" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">Post-Graduation</td><td style="font-size:small"> <asp:Label ID="lblsubpg" runat="server"></asp:Label><br /><asp:Label ID="lblyrpg" runat="server" ></asp:Label><br /><asp:Label ID="lblunipg" runat="server" ></asp:Label><br /><asp:Label ID="lblpercpg" runat="server" ></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style9">B.Ed</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblyrbed" runat="server" ></asp:Label><br /><asp:Label ID="lblunibed" runat="server" ></asp:Label><br /><asp:Label ID="lblpercbed" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold">CTET<br />
                          <span class="auto-style15">CTET certificate is valid for 7 years from the date of declaration of result</span></td><td style="font-size:small"><asp:Label ID="lblnoctet" runat="server"></asp:Label><br /><div id="ctet1" runat="server"><b>Paper I<br /></b><asp:Label ID="lblyrctet" runat="server" ></asp:Label><br /><asp:Label ID="lblpercctet" runat="server" ></asp:Label></div><div id="ctet2" runat="server"><b>Paper II<br /></b><asp:Label ID="lblyrctet2" runat="server" ></asp:Label><br /><asp:Label ID="lblpercctet2" runat="server" ></asp:Label></div></td>
        </tr>
              <tr>
                <td style="font-size:small;font-weight:bold;text-align:left" class="auto-style10" colspan="4">Declaration:<br />
                    I hereby declare that all the statements made in this application are True, Complete and Correct to the best of my knowledge and belief. I understand that in the event of any information being found untrue or incorrect at any stage or I am not satisfying any of the eligibility criteria stipulated, and also in case of creating influence/undue pressure regarding recruitment shall tantamount to cancellation of my candidature.<br />
                   <table style="width:100%;border-width:0px">
                       <tr style="border-width:0px">
                           <td style="text-align:left;border-width:0px">
                               <b> <asp:Label ID="lbldate" runat="server" Visible="false"></asp:Label></b>
                           </td>
                           <td style="text-align:right;border-width:0px">
                              
                                 <asp:Image ID="candsign" runat="server" Width="200px" Height="80px" />
                           </td>
                       </tr>
                   </table>
                   

                </td>
            </tr>
        </table>
         </center> 
             
        
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
