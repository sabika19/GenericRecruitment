<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admnhome.aspx.cs" MasterPageFile="admn.Master" Inherits="kvs.admnhome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 

    <style type="text/css">
        .table-hover {
            font-size: x-small;
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
            font-size: small;
            font-weight: bold;
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
              <h3 class="box-title"><strong>ALL </strong><span class="auto-style1">APPLICATIONS</span></h3>

              <div class="box-tools pull-right">
              
              </div>
            </div>
                 <div id="divform" runat="server">
            <div class="box-body" >
            
            <div id="secinfo" runat="server">
              <div class="auto-style2">
                  <hr />
                  <center>
                  <b>Welcome</b><br />
                 <b>All Complete Applications (Latest First)</b>
                      <br />
                      <br />
                      <b><span class="auto-style3">
                     <asp:Button ID="btnSummary" runat="server" CssClass="btn btn-danger btn-block-md" OnClick="btnSummary_Click" Text="Click To Generate Complete List" Font-Bold="true"/>
                      <br />
                      <br />
                      </span></b>
                      </center>
                 
                      
                       <table Class="table table-bordered">
                           <tr><td><span class="auto-style3">Enter Registration Number :</span> </td><td><asp:TextBox ID="txtgriid" runat="server" CssClass="form-control" Width="300px"></asp:TextBox></td><td><asp:Button ID="btnFind" runat="server" Text="Find" class="btn btn-sm btn-primary" OnClick="btnFind_Click" /></td></tr>
 
                       </table>
                 
                 <asp:Label ID="lblgrvcount" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br />
                  <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" RowStyle-Wrap="true"  OnSelectedIndexChanged="grd1_SelectedIndexChanged" OnRowDataBound="grd1_RowDataBound">
                       <PagerSettings Mode="NumericFirstLast"
                FirstPageText="First"
                LastPageText="Last"
                NextPageText="Next"
                PreviousPageText="Prev" Position="TopAndBottom" />
            <PagerStyle Font-Size="Large" />
                      <Columns>
                           <asp:TemplateField  HeaderText="SLNO" itemstyle-wrap="true">
                   <ItemTemplate >
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>

                </asp:TemplateField>
                           <asp:TemplateField HeaderText="PHOTOGRAPH">
                    <ItemTemplate>
                      <asp:Image ID="imgphoto" runat="server" Width="100px" Height="100px" />
                      
                    </ItemTemplate>

                </asp:TemplateField>
                             <asp:TemplateField HeaderText="SIGNATURE">
                    <ItemTemplate>
                     
                        <asp:Image ID="imgsign" runat="server" Width="100px" Height="100px" />

                    </ItemTemplate>

                </asp:TemplateField>
                            <asp:TemplateField HeaderText="REGISTRATION NUMBER">
                    <ItemTemplate>
                        <asp:Label ID="lblcandid" runat="server" Text='<%# Eval("cand_id")%>'></asp:Label>
                      
                    </ItemTemplate>

                </asp:TemplateField>
                            <asp:TemplateField HeaderText="CANDIDATE DETAILS">
                    <ItemTemplate>
                        <b>NAME : </b><asp:Label ID="lblcname" runat="server" Text='<%# Eval("cname")%>'></asp:Label>
                          <br />
                         <b>MOTHER'S NAME : </b> <asp:Label ID="lblmname" runat="server" Text='<%# Eval("mname")%>' style="word-wrap: break-word; width: 50px;"></asp:Label>
                      <br/>
                           <b>FATHER'S NAME : </b><asp:Label ID="lblfname" runat="server" Text='<%# Eval("fname")%>'></asp:Label></b>
                    </ItemTemplate>

                </asp:TemplateField>
                      
                            <asp:TemplateField  HeaderText="DATE OF BIRTH" itemstyle-wrap="true">
                    <ItemTemplate>
                       
                          <asp:Label ID="lbldob" runat="server" Text='<%# Eval("dob")%>'></asp:Label></b>
                    </ItemTemplate>

                </asp:TemplateField>
                  
                          <asp:TemplateField  HeaderText="CONFIRMATION PAGE">
                              
                              <ItemTemplate>
                        <asp:HyperLink id="hypconf" runat="server" NavigateUrl='<%# "admnconf.aspx?candid="+(string)Eval("cand_id") %>' Text="View" Font-Size="Small"></asp:HyperLink>
                    </ItemTemplate>
                    

                </asp:TemplateField>
                            <asp:TemplateField  HeaderText="FLAG THIS">
                              
                              <ItemTemplate>
                       <asp:CheckBox ID="chckflag" runat="server" CssClass="form-control" />
                    </ItemTemplate>
                    

                </asp:TemplateField>
                         
                      </Columns>
                  </asp:GridView>
                    <asp:Repeater ID="rptPager" runat="server">
<ItemTemplate>
    <asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' Enabled = '<%# Eval("Enabled") %>' OnClick = "Page_Changed" Font-Size="Medium"></asp:LinkButton>
</ItemTemplate>
</asp:Repeater>
                  <asp:Button class="btn btn-primary btn-block btn-flat" Text="Update Flags" runat="server" ID="btnupdate" onclick="btnupdate_Click" />
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