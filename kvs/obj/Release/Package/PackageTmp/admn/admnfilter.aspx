<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admnfilter.aspx.cs" MasterPageFile="admn.Master" Inherits="kvs.admnfilter" %>
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
                 <b>All Applications (Latest First)</b>
                      </center>
                 
                      
                       <table Class="table table-bordered">
                           <tr><td><span class="auto-style3">Enter Registration Number :</span> </td><td><asp:TextBox ID="txtgriid" runat="server" CssClass="form-control" Width="300px"></asp:TextBox></td><td><asp:Button ID="btnFind" runat="server" Text="Find" class="btn btn-sm btn-primary" OnClick="btnFind_Click" /></td></tr>
 <tr><td><span class="auto-style3">Filter By Status:</span> </td><td>
     <asp:DropdownList ID="ddlfilter" runat="server" CssClass="form-control" Width="300px">
         <asp:ListItem value="0" Text="--Select--"></asp:ListItem>
         <asp:ListItem value="1" Text="Step 1"></asp:ListItem>
         <asp:ListItem value="2" Text="Step 2"></asp:ListItem>
          <asp:ListItem value="3" Text="All Steps Complete"></asp:ListItem>
     </asp:DropdownList>

                                                           </td><td><asp:Button ID="btnFilter" runat="server" Text="Find All" class="btn btn-sm btn-primary" OnClick="btnFilter_Click" /></td></tr>

                       </table>
                 
                 <asp:Label ID="lblgrvcount" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br />
                  <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" RowStyle-Wrap="true" AllowPaging="true"
            OnPageIndexChanging="OnPaging" PageSize="100" OnSelectedIndexChanged="grd1_SelectedIndexChanged" OnRowDataBound="grd1_RowDataBound">
                       <PagerSettings Mode="NumericFirstLast"
                FirstPageText="First"
                LastPageText="Last"
                NextPageText="Next"
                PreviousPageText="Prev" Position="TopAndBottom" />
            <PagerStyle Font-Size="Large" />
                      <Columns>
                         <%--  <asp:TemplateField  HeaderText="SLNO" itemstyle-wrap="true">
                   <ItemTemplate >
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>

                </asp:TemplateField>--%>
                        
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
                  
                          <asp:TemplateField  HeaderText="STATUS">
                              
                              <ItemTemplate>
                                  <b><asp:Label ID="lblstatus" runat="server"></asp:Label></b><br />
                                   
                                    <asp:Image ID="imgphoto" runat="server" Width="100px" Height="100px" />
                                    <asp:Image ID="imgsign" runat="server" Width="100px" Height="100px" /><br />
                       <b> <asp:HyperLink id="hypconf" runat="server" NavigateUrl='<%# "admnconf.aspx?candid="+(string)Eval("cand_id") %>' Text="View Confirmation Page" Font-Size="Small"></asp:HyperLink>
                    </b>
                           </ItemTemplate>
                    

                </asp:TemplateField>
                         
                         
                      </Columns>
                  </asp:GridView>
                    
               
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