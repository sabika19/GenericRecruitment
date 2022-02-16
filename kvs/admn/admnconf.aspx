<%@ Page Title="" Language="C#" MasterPageFile="admn.Master" AutoEventWireup="true" CodeBehind="admnconf.aspx.cs" Inherits="kvs.admnconf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
  
    <style type="text/css">
        .auto-style8 {
            font-size: medium;
            font-weight: bold;
            text-decoration: underline;
        }
        .auto-style11 {
            text-align: right;
        }
        .auto-style12 {
            width: 25%;
            text-align: left;
        }
        .auto-style13 {
            text-align: left;
        }
        .auto-style14 {
            width: 25%;
            text-align: left;
            height: 143px;
        }
        .auto-style15 {
            text-align: center;
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

   
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server" onload="noBack();"> 
  


   

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
        <table class="table table-bordered" align="center" >
          
        <tr>
           <td style="font-size:small;font-weight:bold" colspan="2" class="auto-style14">Registration Number : <asp:Label ID="lblcandid" runat="server" ></asp:Label>
               <br />
               Name : <asp:Label ID="lblname" runat="server" ></asp:Label>
               <br />
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
            <td style="font-size:small;text-align:right" colspan="2"><asp:Image ID="candimg" runat="server" Width="110px" Height="110px" /></td>
        </tr>
             <tr>
            <td style="font-size:medium;font-weight:bold;" colspan="4" class="auto-style15" >Personal Details</td>
        </tr>
             <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12"> Date of Birth</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lbldob" runat="server"></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style12">Gender</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblgen" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Religion</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblrel" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style12">Category</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblcat" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Marital Status</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lblmar" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style12">Visible Identification Mark</td><td style="font-size:small" class="auto-style12"><asp:Label ID="lbliden" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Are you differently abled with 40% or above disability?</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblpwd" runat="server"></asp:Label>
             <br />
             <asp:Label ID="lblpwdcat" runat="server"></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">Do you need a scribe?</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblscribe" runat="server"></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Are you a KVS employee?</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblkvemp" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">Are you a Central Government employee with at least 3 years of regular service?</td><td style="font-size:small" class="auto-style11">
              <div class="auto-style13">
                  <asp:Label ID="lblcgemp" runat="server"></asp:Label>
              </div>
              <div id="dvcglenservlbl" runat="server" class="auto-style13"><asp:Label ID="lblcgservlen" runat="server"></asp:Label></div></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Are you ordinarily domiciled in the state of Jammu & Kashmir during 01.01.1980 to 31.12.1989?</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lbljk" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">Are you an Ex-Serviceman?</td><td style="font-size:small" class="auto-style11">
             <div class="auto-style13">
                 <asp:Label ID="lblexserv" runat="server"></asp:Label>
             </div>
             <div id="dvexservlenlbl" runat="server" class="auto-style13"><asp:Label ID="lblexservlen" runat="server"></asp:Label></div></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Address</td><td style="font-size:small" colspan="3" class="auto-style13"><asp:Label ID="lbladd" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Email ID</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblemail" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">Contact No.</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblcon" runat="server"></asp:Label></td>
        </tr>
              <tr>
            <td style="font-size:medium;font-weight:bold;" colspan="4" class="auto-style15" >Examination Details</td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Do you Fulfill Essential Qualifications?</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblqual" runat="server"></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">Do you Fulfill Desirable Qualifications?</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lbldes" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Examination Cities Opted</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblcity1" runat="server" ></asp:Label><br /><asp:Label ID="lblcity2" runat="server" ></asp:Label><br /><asp:Label ID="lblcity3" runat="server" ></asp:Label><br /><asp:Label ID="lblcity4" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">Medium of Question Paper</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblmed" runat="server"></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:medium;font-weight:bold;" colspan="4" class="auto-style15" >Educational Qualification Details</td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Class 10/Equivalent</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblyr10" runat="server" ></asp:Label><br /><asp:Label ID="lbluni10" runat="server" ></asp:Label><br /><asp:Label ID="lblperc10" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">Class 12/Equivalent</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblyr12" runat="server" ></asp:Label><br /><asp:Label ID="lbluni12" runat="server" ></asp:Label><br /><asp:Label ID="lblperc12" runat="server" ></asp:Label></td>
        </tr>
         <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Diploma</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblyrdip" runat="server" ></asp:Label><br /><asp:Label ID="lblunidip" runat="server" ></asp:Label><br /><asp:Label ID="lblpercdip" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">B.El.Ed/D.Ed</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblyrded" runat="server" ></asp:Label><br /><asp:Label ID="lblunided" runat="server" ></asp:Label><br /><asp:Label ID="lblpercded" runat="server" ></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">Graduation</td><td style="font-size:small" class="auto-style13">
              <asp:Label ID="lblgradsub" runat="server"></asp:Label><br />
              <asp:Label ID="lblyrgrad" runat="server" ></asp:Label><br /><asp:Label ID="lblunigrad" runat="server" ></asp:Label><br /><asp:Label ID="lblpercgrad" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">Post-Graduation</td><td style="font-size:small" class="auto-style13"> <asp:Label ID="lblsubpg" runat="server"></asp:Label><br /><asp:Label ID="lblyrpg" runat="server" ></asp:Label><br /><asp:Label ID="lblunipg" runat="server" ></asp:Label><br /><asp:Label ID="lblpercpg" runat="server" ></asp:Label></td>
        </tr>
          <tr>
            <td style="font-size:small;font-weight:bold" class="auto-style12">B.Ed</td><td style="font-size:small" class="auto-style13"><asp:Label ID="lblyrbed" runat="server" ></asp:Label><br /><asp:Label ID="lblunibed" runat="server" ></asp:Label><br /><asp:Label ID="lblpercbed" runat="server" ></asp:Label></td><td style="font-size:small;font-weight:bold" class="auto-style13">CTET</td><td style="font-size:small" class="auto-style11"><div id="ctet1" runat="server" class="auto-style13"><b>Paper I<br /></b><asp:Label ID="lblyrctet" runat="server" ></asp:Label><br /><asp:Label ID="lblpercctet" runat="server" ></asp:Label></div><div id="ctet2" runat="server" class="auto-style13"><b>Paper II<br /></b><asp:Label ID="lblyrctet2" runat="server" ></asp:Label><br /><asp:Label ID="lblpercctet2" runat="server" ></asp:Label></div></td>
        </tr>
              <tr>
                <td style="font-size:small;font-weight:bold;" class="auto-style12" colspan="4">Declaration:<br />
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
