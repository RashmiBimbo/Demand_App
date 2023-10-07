<%--<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NORMAL/BBISite.Master" CodeBehind="DepotDemand.aspx.cs" Inherits="DemandApp.DepotDemand" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NORMAL/BBISiteNew.Master" CodeBehind="DepotDemand.aspx.cs" Inherits="DemandApp.DepotDemand" %>


<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="photo">
        <div class="col-12 d-flex no-block align-items-center">
            <h4 class="page-title">DEMAND </h4>
            <div class="ms-auto text-end">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="home.aspx">Home</a></li>
                        <%--   <li class="breadcrumb-item active" aria-current="page">Library
                    </li>--%>
                    </ol>
                </nav>
            </div>
        </div>
        <div class="name">
            <asp:Label ID="lblwelcome" runat="server"></asp:Label>
        </div>

    </div>


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=400,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }

        //$(document).keypress(function (e) {
        //    var keyCode = (window.event) ? e.which : e.keyCode;
        //    if (keyCode && keyCode == 13) {
        //        e.preventDefault();
        //        return false;
        //    }
        //});

        function enterToTab() {
            if (event.keyCode == 13)
                event.keyCode = 9;
        }



               //$(document).ready(function () {
               //    $('input').keyup(function (e) {
               //        if (e.which == 39)
               //            $(this).closest('td').next().find('input').focus();
               //        else if (e.which == 37)
               //            $(this).closest('td').prev().find('input').focus();
               //        else if (e.which == 40)
               //            $(this).closest('tr').next().find('td:eq(' + $(this).closest('td').index() + ')').find('input').focus();
               //        else if (e.which == 38)
               //            $(this).closest('tr').prev().find('td:eq(' + $(this).closest('td').index() + ')').find('input').focus();
               //    });
               //});


               //function tt() {
               //    $(document).ready(function () {
               //        $('input').keyup(function (e) {
               //            if (e.which == 39)
               //                $(this).closest('td').next().find('input').focus();
               //            else if (e.which == 37)
               //                $(this).closest('td').prev().find('input').focus();
               //            else if (e.which == 40)
               //                $(this).closest('tr').next().find('td:eq(' + $(this).closest('td').index() + ')').find('input').focus();
               //            else if (e.which == 38)
               //                $(this).closest('tr').prev().find('td:eq(' + $(this).closest('td').index() + ')').find('input').focus();
               //        });
               //    });
               //}



               //$(document).keypress(function (e) {
               //    var keyCode = (window.event) ? e.which : e.keyCode;
               //    if (keyCode && keyCode == 13) {
               //        e.preventDefault();
               //        return false;
               //    }
               //});
// ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Menu ID="Menu1" class="wizard clearfix" Width="100%" runat="server" BackColor="#F7F6F3" DynamicBottomSeparatorImageUrl="~/Images/background1.GIF"
        DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="Small" ForeColor="#075098"
        OnMenuItemClick="Menu1_MenuItemClick"
        Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
        StaticSubMenuIndent="10px">
        <Items>
            <asp:MenuItem Text=" New/Edit Demand |" Value="0"></asp:MenuItem>
            <asp:MenuItem Text=" Verify/Submit Demand Sheet |" Value="1"></asp:MenuItem>
            <asp:MenuItem Text=" Report |" Value="2"></asp:MenuItem>
        </Items>
        <StaticSelectedStyle BackColor="#2962FF" ForeColor="White" />
        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <DynamicHoverStyle BackColor="#e0e3f0" ForeColor="075098" />
        <DynamicMenuStyle BackColor="#2962FF" />
        <DynamicSelectedStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" Font-Names="Arial" Font-Size="Small" />
        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <StaticHoverStyle BackColor="#e0e3f0" ForeColor="075098" />
    </asp:Menu>
     <div>
            <asp:Label ID="lblmgs"  visble="false" style="color:red;font-weight:bold" runat="server" />
        </div>
    <asp:MultiView ID="MultiView1" runat="server">
       
        <asp:View ID="tabNew" runat="server">

            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">Demand Date</label>

                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_demnaddate" class="mydatepicker" runat="server" Width="160px" OnTextChanged="txt_demnaddate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <%--<input type="text" class="form-control" id="fname" placeholder="First Name Here" />--%>
                            </div>
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">Item Group Name</label>

                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlgroupname" class="form-control " OnSelectedIndexChanged="ddlgroupnameIndexChanged" runat="server" Width="144px" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                        </div>
                        <div class="form-group row">
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">Customer Name</label>

                            <div class="col-sm-6">
                                <asp:DropDownList ID="DDLCustomer" class="form-control   dropdown-toggle " style="width:175px" data-bs-toggle="dropdown" runat="server" Width="144px" OnSelectedIndexChanged="OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <label for="fname" class="col-sm-6 text-end control-label col-form-label" runat="server" id="lblmessgae"></label>
                        </div>
                        <div class="form-group row" style="display: none">
                            <label for="fname" class="col-sm-4 text-end control-label col-form-label">Demand/Target Qty</label>

                            <div class="col-sm-3">
                                <asp:Label ID="lblqty" Visible="false" runat="server"></asp:Label>

                            </div>

                            <asp:Button ID="Button1" Visible="false" class="col-sm-3 text-end control-label col-form-label" runat="server" Text="Total Qty" Height="25px" />
                            <%--     <label for="fname" class="col-sm-3 text-end control-label col-form-label">Customer Name</label>--%>

                            <div class="col-sm-2">
                                <asp:Label ID="lbledit" runat="server" Visible="False"></asp:Label>
                                <asp:ImageButton ID="ImageButton8" runat="server" Height="25px" ImageUrl="~/Images/delete.png" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>


            <div style="width: 100%; height: 380px; overflow: scroll" runat="server" id="Grid1div">
                <asp:GridView ID="GridView1" Style="height: 400px; overflow: auto" runat="server" CellPadding="1" CellSpacing="1" Font-Bold="False"
                    CssClass="grid-view" Width="100%" Font-Size="Small" ForeColor="#333333" GridLines="None" RowStyle-HorizontalAlign="LEFT" RowStyle-Wrap="false" HeaderStyle-Wrap="false" AutoGenerateColumns="False">
                    <RowStyle BackColor="white" HorizontalAlign="LEFT" Wrap="False" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="075098" Font-Bold="True" ForeColor="white" Wrap="False" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="#7ad0ed" />
                    <Columns>
                        <asp:BoundField DataField="OrderNo" HeaderText="Order No" ReadOnly="True" SortExpression="Item_ID" />
                        <asp:BoundField DataField="ItemGroup" HeaderText="Item Group" ReadOnly="True" SortExpression="Item_ID" />
                        <asp:BoundField DataField="Item_ID" HeaderText="Item ID" ReadOnly="True" SortExpression="Item_ID" />
                        <%--<asp:BoundField DataField="Item_Description" HeaderText="Description" SortExpression="Item_Description" ItemStyle-HorizontalAlign="Justify" ItemStyle-Font-Size="SMALL" HeaderStyle-HorizontalAlign="Justify"  />--%>
                        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" SortExpression="Item_Name" ItemStyle-HorizontalAlign="Justify" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="SMALL" HeaderStyle-HorizontalAlign="Justify" />
                        <asp:BoundField DataField="Cust_Qty" HeaderText="Cust_Qty(pc)" SortExpression="Cust_Qty" ItemStyle-HorizontalAlign="Justify" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="SMALL" HeaderStyle-HorizontalAlign="Justify" />

                        <asp:TemplateField HeaderText="Qty(pc)" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                &nbsp;
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Txtqty"   runat="server"  Width="48px" Text='<%# Eval("Cust_Qty") %>' onkeydown="tt()"   AutoPostBack="false"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <%--  <asp:BoundField DataField="Target_Qty" HeaderText="Target" SortExpression="Target" ItemStyle-HorizontalAlign="Justify" ItemStyle-Font-Bold ="true" ItemStyle-Font-Size="SMALL" HeaderStyle-HorizontalAlign="Justify"  />--%>
                    </Columns>


                </asp:GridView>


            </div>
            <asp:ImageButton ID="CMDSave" OnClick="CMDSave_Click" runat="server" ImageUrl="~/Images/save_t.PNG" />


        </asp:View>
        <asp:View ID="TABVERIFY" runat="server">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">Demand Date</label>

                            <div class="col-sm-3">
                                <asp:TextBox ID="Vtxt_DemadDate" class="mydatepicker" runat="server" Width="160px" OnTextChanged="txt_demnaddate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <%--<input type="text" class="form-control" id="fname" placeholder="First Name Here" />--%>
                            </div>
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label" style="display: none">Item Group Name</label>

                            <div class="col-sm-3">
                                <asp:ImageButton ID="TABVERIFY_CMD_VIEW" OnClick="TABVERIFY_CMD_VIEW_Click" runat="server"
                                    ImageUrl="~/Images/view.png" />

                                <asp:ImageButton ID="TABVERIFY_CMD_Print" runat="server" Height="20px"
                                    ImageAlign="AbsBottom" ImageUrl="~/Images/printer.png"
                                    OnClientClick="javascript:CallPrint('Div2')" Style="text-align: right"
                                    Width="24px" />
                                <asp:ImageButton ID="TABVERIFY_CMD_exportExcel" runat="server" Height="20px"
                                    ImageAlign="AbsBottom" ImageUrl="~/Images/export-excel.png" Width="32px" />

                                <asp:DropDownList ID="VDDlItemgroup" Visible="false" class="form-control " runat="server" Width="144px" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                        </div>
                        <div class="form-group row" style="display: none">
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">Customer Name</label>

                            <div class="col-sm-3">
                                <asp:DropDownList ID="VDDlCust" class="form-control   dropdown-toggle " data-bs-toggle="dropdown" runat="server" Width="144px" OnSelectedIndexChanged="OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <label for="fname" class="col-sm-6 text-end control-label col-form-label" runat="server" id="Label1"></label>
                        </div>


                    </div>
                </div>

            </div>

            <div id="Div2" style="overflow: auto; width: 100%; height: 300px">
                <asp:GridView ID="GridView3" runat="server" Width="100%" CellPadding="1" CellSpacing="1"
                    Font-Bold="False" Font-Size="X-Small"
                    ForeColor="#333333" GridLines="None"
                    RowStyle-HorizontalAlign="Center" RowStyle-Wrap="false" HeaderStyle-Wrap="false">
                    <RowStyle BackColor="white" HorizontalAlign="Center" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True"  ForeColor="#333333" />
                    <HeaderStyle BackColor="#075098" Font-Bold="True" HorizontalAlign="Center" ForeColor="white" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="#7ad0ed" />
                </asp:GridView>
            </div>



            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="Submit Type" Width="121px"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RB_CUST" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RB_CUST_SelectedIndexChanged" RepeatDirection="Horizontal" Width="254px">
                            <asp:ListItem Value="1">All</asp:ListItem>
                            <asp:ListItem Value="2">Customer</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <div class="album-frame">

                            <div class="col-sm-3">
                                <asp:DropDownList ID="DDlCust" class="form-control   dropdown-toggle " data-bs-toggle="dropdown" runat="server" Width="144px" OnSelectedIndexChanged="DDlCust_BindDataTable" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <%--<asp:combobox ID="TABVERIFY_CMBROUTE" runat="server" AutoPostBack="True" Visible="False" Width="168px">
                                </asp:combobox>--%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:imagebutton id="cmdsubmit" runat="server" onclick="SendToServer_Click" imagealign="AbsBottom" imageurl="~/Images/Submit.png" onclientclick="return confirm('Do you want to Submit the Demand  For this Depot?');" xmlns:asp="#unknown" />
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="#990000" Style="font-size: smaller" Text="* Note:Demand submit once depot at a time(Depot wise) , If once you click on submit button ,you can't change the demand" Visible="False" Width="592px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>


        </asp:View>

        <asp:View ID="tabView" runat="server">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">Start Date</label>

                            <div class="col-sm-3">
                                <asp:TextBox ID="rpt_strdate" class="mydatepicker" runat="server" Width="160px" AutoPostBack="true"></asp:TextBox>

                            </div>
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">End Date</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="rpt_Enddate" class="mydatepicker" runat="server" Width="160px" AutoPostBack="true"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-group row" style="text-align:center">
                            <%--<label for="fname" class="col-sm-3 text-end control-label col-form-label">Item Group Name</label>
                            <div class="col-sm-3">

                                <asp:DropDownList ID="Rpt_ddlgname"  class="form-control " runat="server" Width="144px" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>--%>

                            <div class="col-sm-6">
                                <asp:ImageButton ID="btn_rptView" OnClick="RPT_CMD_VIEW_Click" runat="server"
                                    ImageUrl="~/Images/view.png" />
                            
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="20px"
                                    ImageAlign="AbsBottom" ImageUrl="~/Images/printer.png"
                                    OnClientClick="javascript:CallPrint('divprint')" Style="text-align: right"
                                    Width="24px" />
                                <asp:ImageButton ID="ImageButton3" runat="server" Height="20px"
                                    ImageAlign="AbsBottom" ImageUrl="~/Images/export-excel.png" Width="32px" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div style="overflow: auto; width: 100%; height: 363px" id="divprint">
                    <asp:GridView ID="GridView2" runat="server" CellPadding="1" Width="100%" CellSpacing="1" Font-Bold="False"
                        Font-Size="X-Small" ForeColor="#333333" GridLines="None" RowStyle-HorizontalAlign="Center">
                        <RowStyle BackColor="white" HorizontalAlign="Center" Wrap="false" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#075098" Font-Bold="True" HorizontalAlign="Center" ForeColor="white" Wrap="false" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="#7ad0ed" />
                    </asp:GridView>
                </div>
        </asp:View>
    </asp:MultiView>
    <%--      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>--%>
    <%--<script type="text/javascript" src="js/gridviewScroll.min.js"></script>--%>
    <script type="text/javascript">
                //$(document).ready(function () {
                //    gridviewScroll();
                //});

              <%--  function gridviewScroll() {
                    $('#<%=GridView1.ClientID%>').gridviewScroll({
                        width: "880px",
                        height: 340,
                        freezesize: 0
                    });

              } --%> 
    </script>

    <asp:Label ID="lblleaf_id" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblrecid" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblpaycode" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblip" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblsubcompanyid" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblcompanyid" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblusertype" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="T_LOCATION" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lbldepot" runat="server" Visible="False"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectBBIDemand %>"
        ProviderName="<%$ ConnectionStrings:ConnectBBIDemand.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectBBIDemand %>"
        ProviderName="<%$ ConnectionStrings:ConnectBBIDemand.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectBBIDemand %>"
        ProviderName="<%$ ConnectionStrings:ConnectBBIDemand.ProviderName %>"></asp:SqlDataSource>
</asp:Content>


