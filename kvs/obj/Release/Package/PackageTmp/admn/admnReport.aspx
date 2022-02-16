<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admnReport.aspx.cs" MasterPageFile="admn.Master" Inherits="kvs.admnReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 

    <style type="text/css">
        .table-hover {
            font-size: small;
            font-weight: 700;
        }
        .auto-style1 {
            font-weight: bold;
        }
        .auto-style2 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 100%;
            text-align: center;
            font-size: small;
            padding-left: 15px;
            padding-right: 15px;
        }
        .auto-style3 {
            font-size: medium;
        }
        </style>
     <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
</script>
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server" onload="noBack();">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
     <center>
			<br />
    
     
    <div class="container">
                <input id="mode" type="hidden" value="save" name="mode">
				<input id="view_type" type="hidden" value="Q" name="mode">
                   <section class="content">
    
    <!-- Main content -->
        <div class="row">
        <div class="col-md-12">

          <div class="box box-success">
            <div class="box-header with-border">
              <h3 class="box-title"><strong>SUMMARY</strong></h3>

              <div class="box-tools pull-right">
              
              </div>
            </div>
                 <div id="divform" runat="server">
            <div class="box-body" >
            
            <div id="secinfo" runat="server">
              <div class="auto-style2">
                  <hr />
                  <center>
                  <b><span class="auto-style3">Welcome</span></b><span class="auto-style3"><br />
                      </span>
                      <b><span class="auto-style3">
                      <br />
                     <asp:Button ID="btnSummary" runat="server" CssClass="btn btn-warning btn-block-md" OnClick="btnSummary_Click" Text="Click To Generate Summary" />
                      <br /><br />
                     <label>Generate Post Wise Summary</label>
                     <asp:DropDownList ID="ddlpost" runat="server" CssClass="form-control" Width="300px">
                         <asp:ListItem Value="0" Text="--All Posts--"></asp:ListItem>
                         <asp:ListItem Value="post_princi" Text="Principal"></asp:ListItem>
                         <asp:ListItem Value="post_vcp" Text="Vice Principal"></asp:ListItem>
                         <asp:ListItem Value="post_pgt" Text="PGT"></asp:ListItem>
                         <asp:ListItem Value="post_tgt" Text="TGT"></asp:ListItem>
                         <asp:ListItem Value="post_lib" Text="Librarian"></asp:ListItem>
                         <asp:ListItem Value="post_prt" Text="PRT"></asp:ListItem>
                         <asp:ListItem Value="post_prtm" Text="PRT-MUSIC"></asp:ListItem>
                     </asp:DropDownList>
                     &nbsp;<asp:Button ID="btnpost" runat="server" CssClass="btn btn-block btn-success" Text="Generate Summary" Width="300px" OnClick="btnpost_Click" />
                      <br />
                       KVS Summary Report as On <asp:Label ID="lbldate" runat="server"></asp:Label>
                      </span></b></center>
                 
                      
                 <asp:Label ID="lblgrvcount" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                  <br />
                  <br />
                  <table class="table table-bordered table-hover" align="left">
                      <tr>
                          <td>CATEGORY</td>
                          <td colspan="4">MALE</td>
                          <td colspan="4">FEMALE</td>
                           <td colspan="4">TOTAL</td>
                      </tr>
                      <tr>
                          <td>&nbsp;</td>
                          <td>REGISTERED</td>
                          <td>PHOTO UPLOAD</td>
                          <td>FEE PAYMENT</td>
                          <td>COMPLETE FORMS</td>
                          <td>REGISTERED</td>
                          <td>PHOTO UPLOAD</td>
                          <td>FEE PAYMENT</td>
                          <td>COMPLETE FORMS</td>
                             <td>REGISTERED</td>
                          <td>PHOTO UPLOAD</td>
                          <td>FEE PAYMENT</td>
                          <td>COMPLETE FORMS</td>
                      </tr>
                      <tr>
                          <td>GENERAL</td>
                          <td><asp:Label ID="lblmlreggen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlphgen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlfeegen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlcompgen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflreggen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflphgen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflfeegen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflcompgen" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblreggen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblphgen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblfeegen" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblcompgen" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td>OBC</td>
                          <td><asp:Label ID="lblmlrego" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlpho" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlfeeo" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlcompo" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflrego" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflpho" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflfeeo" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflcompo" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblrego" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblpho" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblfeeo" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblcompo" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td>SC</td>
                          <td><asp:Label ID="lblmlregc" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlphc" runat="server"></asp:Label></td>
                         <td><asp:Label ID="lblmlfeec" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlcompc" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflregc" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflphc" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflfeec" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflcompc" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblregc" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblphc" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblfeec" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblcompc" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td>ST</td>
                          <td><asp:Label ID="lblmlregt" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlpht" runat="server"></asp:Label></td>
                           <td><asp:Label ID="lblmlfeet" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlcompt" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflregt" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflpht" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflfeet" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflcompt" runat="server"></asp:Label></td>
                           <td><asp:Label ID="lblregt" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblpht" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblfeet" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblcompt" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td>PWD</td>
                          <td><asp:Label ID="lblmlregp" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlphp" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblmlfeep" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlcompp" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflregp" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflphp" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflfeep" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflcompp" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblregp" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblphp" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblfeep" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblcompp" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td>TOTAL</td>
                          <td><asp:Label ID="lblmlreg" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlph" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblmlfee" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblmlcomp" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflreg" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflph" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflfee" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblflcomp" runat="server"></asp:Label></td>
                            <td><asp:Label ID="lblregcand" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblphoto" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblpay" runat="server"></asp:Label></td>
                          <td><asp:Label ID="lblall" runat="server"></asp:Label></td>
                      </tr>
                  </table>
                  <br />
                  <br />
                  <b>
              <%--    <table class="table table-bordered table-hover" style="font-size: small">
                      <tr>
                          <th>CATEGORY</th>
                          <th>NUMBER OF UPLOADS</th>
                      </tr>
                      <tr>
                          <td>TOTAL REGISTERED CANDIDATES</td>
                          <td><asp:Label ID="lblregcand" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td>TOTAL PHOTO/SIGNATURE UPLOADS</td>
                          <td><asp:Label ID="lblphoto" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td>TOTAL SUCCESSFUL PAYMENTS</td>
                          <td><asp:Label ID="lblpay" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td>TOTAL COMPLETED FORMS</td>
                          <td><asp:Label ID="lblall" runat="server"></asp:Label></td>
                      </tr>
                  </table>--%>
                    </b>
               
               </div>
                  </div>
            
          
                </div>
                     </div>
               </div>
             
               
                     <hr>

      
       
             </section>
                     
                    </div>
		
       
	</center>
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