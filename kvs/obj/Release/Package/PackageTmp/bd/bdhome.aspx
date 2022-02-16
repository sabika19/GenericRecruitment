<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bdhome.aspx.cs" MasterPageFile="bd.Master" Inherits="kvs.bdhome" %>
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
            font-size: medium;
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
              <h3 class="box-title"><strong>ALL </strong><span class="auto-style1">CANDIDATES</span></h3>

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
               
                      </center>
                 
                      
                       <%--<table Class="table table-bordered"><tr><td><span class="auto-style3">Select Center:</span> </td><td><asp:DropDownList ID="ddlcen" runat="server" CssClass="form-control">
                           <asp:ListItem Value="0">--select--</asp:ListItem>
                           <asp:ListItem>Delhi</asp:ListItem>
                           <asp:ListItem>Bangalore</asp:ListItem>
                           <asp:ListItem>Kolkata</asp:ListItem>
                           <asp:ListItem>Chandigarh</asp:ListItem>
                           <asp:ListItem>jaipur</asp:ListItem>
                           <asp:ListItem>kolkata</asp:ListItem>
                           <asp:ListItem>lucknow</asp:ListItem>
                           <asp:ListItem>shillong</asp:ListItem>
                           </asp:DropDownList>

                           <asp:RequiredFieldValidator ID="reqcen" runat="server" ControlToValidate="ddlcen" ErrorMessage="*Required" Font-Bold="true" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                                        </td><td><asp:Button ID="btnFind" runat="server" Text="Find Candidates" class="btn btn-sm btn-primary" OnClick="btnFind_Click" /></td></tr>
                          </table>--%>
                       <table Class="table table-bordered"><tr><td><span class="auto-style3">Enter Customer ID/Registration Number :</span> </td><td>
                 <asp:RadioButtonList ID="rdblbank0" runat="server">
                     <asp:ListItem Value="HDFC" Text="HDFC" Selected></asp:ListItem>
                     <asp:ListItem Value="IB" Text="Indian Bank"></asp:ListItem>
                 </asp:RadioButtonList>
                           </td><td><asp:TextBox ID="txtgriid" runat="server" CssClass="form-control" Width="300px"></asp:TextBox></td><td><asp:Button ID="btnFind" runat="server" Text="Find Details" class="btn btn-sm btn-primary" OnClick="btnFind_Click" />
                           <br />
                           <br />
                           <asp:Button ID="expExcel" runat="server" CssClass="btn btn-sm btn-success" Text="Export To Excel" OnClick="expExcel_Click" /></td></tr></table>
                 
                 
              <table Class="table table-bordered" width="100%"> <tr><td><b>Filter By Date : </b></td><td>
                 <asp:RadioButtonList ID="rdblbank" runat="server">
                     <asp:ListItem Value="HDFC" Text="HDFC" Selected></asp:ListItem>
                     <asp:ListItem Value="IB" Text="Indian Bank"></asp:ListItem>
                 </asp:RadioButtonList>
                         </td><td><b>From Date :  <asp:TextBox ID="datepicker1"  class="form-control" placeholder="Date From (dd/mm/yyyy)" runat="server"></asp:TextBox></b></td><td><b>To Date :  <asp:TextBox ID="datepicker2"  class="form-control" placeholder="Date To (dd/mm/yyyy)" runat="server"></asp:TextBox></b></td><td width="25%" style="text-align:left"><asp:Button ID="btnFilDate" runat="server" CssClass="btn btn-sm btn-warning" Text="Find All Transactions" OnClick="btnFilDate_Click" Width="300px" /><br /><br /><asp:Button ID="expExceldate" runat="server" CssClass="btn btn-sm btn-success" Text="Export To Excel By Date" OnClick="expExceldate_Click" width="300px"/></td></tr>
                       </table>
                  <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" RowStyle-Wrap="true" AllowPaging="true"
            OnPageIndexChanging="OnPaging" PageSize="100" OnSelectedIndexChanged="grd1_SelectedIndexChanged" style="font-size: small">
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

                            <asp:TemplateField HeaderText="CANDIDATE ID">
                    <ItemTemplate>
                        <asp:Label ID="lblgriid" runat="server" Text='<%# Eval("cand_id")%>'></asp:Label>

                    </ItemTemplate>

                </asp:TemplateField>
                           <asp:TemplateField HeaderText="TRANSACTION ID">
                    <ItemTemplate>
                        <asp:Label ID="lblgritype" runat="server" Text='<%# Eval("txnid")%>'></asp:Label>

                    </ItemTemplate>

                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="TRANSACTION REFERENCE NUMBER">
                    <ItemTemplate>
                        <asp:Label ID="lblcname" runat="server" Text='<%# Eval("txnrefno")%>'></asp:Label>

                    </ItemTemplate>

                </asp:TemplateField>
                           <asp:TemplateField HeaderText="FEE PAID">
                    <ItemTemplate>
                        <asp:Label ID="lblcname" runat="server" Text='<%# Eval("inifee")%>'></asp:Label>

                    </ItemTemplate>

                </asp:TemplateField>
                           <asp:TemplateField HeaderText="TRANSACTION DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblcname" runat="server" Text='<%# Eval("txndate")%>'></asp:Label>

                    </ItemTemplate>

                </asp:TemplateField>
                          <asp:TemplateField  HeaderText="CONFIRMATION PAGE">
                              
                              <ItemTemplate>
                        <asp:HyperLink id="hypconf" runat="server" NavigateUrl='<%# "bdconf.aspx?candid="+(string)Eval("cand_id") %>' Text="View"></asp:HyperLink>
                    </ItemTemplate>
                    

                </asp:TemplateField>
                         
                      </Columns>
                  </asp:GridView>
                    <asp:Label ID="lblgrvcount" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
               </div>
                  </div>
            
          
                </div>
                     </div>
               </div>
             
               
                     <hr>

      
       
             </section>
                     
                    </div>
		
       
	</center>
    <script src="../dist/js/demo.js"></script>
        <!--datepicker-->
        <script src="../plugins/datepicker/bootstrap-datepicker.js"></script>
        <script>
            //Date picker
            $('#<%=datepicker1.ClientID%>').datepicker({
                autoclose: true
            });
        </script>
     <script src="../plugins/datepicker/bootstrap-datepicker.js"></script>
        <script>
            //Date picker
            $('#<%=datepicker2.ClientID%>').datepicker({
                autoclose: true
            });
        </script>
</asp:Content>