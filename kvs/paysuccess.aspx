<%@ Page Title="" Language="C#" MasterPageFile="~/kvs.Master" AutoEventWireup="true" CodeBehind="paysuccess.aspx.cs" Inherits="kvs.paysuccess" %>
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
            font-weight: bold;
            color: #003300;
        }
        .auto-style8 {
            font-weight: bold;
        }
        .auto-style9 {
            color: #CC0000;
        }
        .auto-style10 {
            font-size: xx-small;
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
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server"> 
    <script type="text/javascript">
$(function () {
$("#txtdatepicker").datepicker();
});
</script>


   

    <div class="container">
               
                   <section class="content">
     <div class="row">
        <div class="col-md-12">

          <div class="box box-warning">
            <div class="box-header with-border">
              <h4 class="box-title" style="text-align:center; width: 100%;"><strong>Direct Recruitment Drive of Teaching Posts in Kendriya Vidyalaya Sangathan</strong></h4>

              <div class="box-tools pull-right">
            
              </div>
            </div>
            
                 <div id="divform" runat="server">
                      <div class="box-body" >
            
            <div id="secinfo" runat="server">
              <div class="col-md-12">
              
            <div class="text-justify" >
    <center>
         <h4 class="auto-style7">PAYMENT STATUS : <asp:Label ID="lblstatus" runat="server"></asp:Label> </h4>
         <p class="auto-style7"><asp:Label ID="lblmsg1" runat="server" CssClass="auto-style9"></asp:Label></p>
         <p class="auto-style7"><asp:Label ID="lblpid" Visible="false" runat="server" CssClass="auto-style10"></asp:Label></p>
        <table class="table table-bordered table-hover" >
            <tr><td colspan="4" class="auto-style5"><h4 class="auto-style4"><strong>PAYMENT DETAILS</strong></h4></td></tr>
           <tr>
                <td class="auto-style2" >
                    <span class="ui-priority-primary"><strong>Name</strong></span></td>
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
                    <span class="auto-style8">Fee</span></td>
               <td  class="auto-style3"><strong> <span class="auto-style2">&#8377</span> <asp:Label ID="lblamount" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong></td>
               <td class="auto-style2" >
                    <span class="auto-style8">Transaction ID</span></td>
               <td  class="auto-style3"><strong><asp:Label ID="lbltrid" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong>
             </td>
            </tr>
            <tr>
                <td class="auto-style2" >
                    <span class="auto-style8">Payment Mode</span></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblpaymode" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong></td>
               <td class="auto-style2" >
                    <span class="auto-style8">Date of Payment</span></td>
               <td  class="auto-style3"><strong><asp:Label ID="lbldate" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong>
             
            </tr>
             
                          
        </table>
         </center>
                <div class="text-justify" style="font-size:small">
                    



                        <br />
                    <div id="divbtn" style="text-align:center">
                        <asp:Button ID="btnagree" runat="server" CssClass="btn btn-lg btn-info" Text="Generate Confirmation Page" OnClick="btnagree_Click" />
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
      
  
  
     
    </span>
      
  
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
     
</asp:Content>
