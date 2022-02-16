<%@ Page Title="" Language="C#" MasterPageFile="~/kvs.Master" AutoEventWireup="true" CodeBehind="editstep3.aspx.cs" Inherits="kvs.editstep3" %>
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
              <h4 class="box-title" style="text-align:center; width: 100%;"><strong>Direct Recruitment Drive 2018</strong></h4>

              <div class="box-tools pull-right">
            <a href="images/Terms and Conditions.pdf" target="_blank" style="font-size:medium;font-weight:bold;color:red">Terms & Conditions</a>
              </div>
            </div>
            
                 <div id="divform" runat="server">
                      <div class="box-body" >
            
            <div id="secinfo" runat="server">
              <div class="col-md-12">
              
            <div class="text-justify" >
    <center>
         <h4 class="auto-style7"><strong>STEP 3</strong></h4>
         <p class="auto-style7"><asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></p>
        
        <table class="table table-bordered table-hover" id="tblpay" runat="server" >
            <tr><td colspan="4" class="auto-style5"><h4 class="auto-style4"><strong>CANDIDATE DETAILS</strong></h4></td></tr>
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
                    <span class="ui-priority-primary"><strong>Mother&#39;s Name</strong></span></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblmname" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong>
               </td>
                    <td class="auto-style2" >
                    <strong>Father's/Guardian's Name</strong></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblfname" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong>
               </td>
            </tr>
             <tr>
                <td class="auto-style2" >
                    <strong>Photograph</strong></td>
               <td  class="auto-style3"><strong><asp:Image id="imgphoto" runat="server" Width="100px" Height="100px" /><br class="auto-style2" />
                   </strong></td>
               <td class="auto-style2" >
                    <strong>Signature</strong></td>
               <td  class="auto-style3"><strong><asp:Image id="imgsign" runat="server" Width="200px" Height="100px"/><br class="auto-style2" />
                   </strong></td>
             
            </tr>
             <tr style="display:none;">
                <td class="auto-style2" >
                    <strong>Photograph</strong></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblmobile" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong></td>
               <td class="auto-style2" >
                    <strong>Signature</strong></td>
               <td  class="auto-style3"><strong><asp:Label ID="lblemail" runat="server" CssClass="auto-style2"></asp:Label><br class="auto-style2" />
                   </strong></td>
             
            </tr>
             <tr><td colspan="4" class="auto-style5"><h4 class="auto-style4">PAYMENT DETAILS</h4></td></tr>
           <tr>
                <td class="auto-style2" colspan="4" >
                  <center><b>Fee Applicable : &#8377 <asp:Label ID="lblfee" runat="server"></asp:Label>  &nbsp;Only</b></center></td>
              
             
            </tr>
             
        </table>
         <div class="form-group">
        <label>
            Choose Payment Gateway
        </label>
        
        <asp:RadioButtonList ID="rdblgtwy" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="H" Text="HDFC" Selected></asp:ListItem>
            <asp:ListItem Value="I" Text="Indian Bank"></asp:ListItem>

        </asp:RadioButtonList>
         </div>
         </center>
                <div class="text-justify" style="font-size:small">
                    



                        <br />
                    <div id="divbtn" runat="server" style="text-align:center">
                        <asp:Button ID="btnagree" runat="server" CssClass="btn btn-lg btn-info" Text="Continue To Pay" OnClick="btnagree_Click" />
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
