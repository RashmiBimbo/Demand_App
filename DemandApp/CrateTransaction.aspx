<%--<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NORMAL/BBISite.Master" CodeBehind="DepotDemand.aspx.cs" Inherits="DemandApp.DepotDemand" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NORMAL/BBISiteNew.Master" CodeBehind="CrateTransaction.aspx.cs" Inherits="DemandApp.CrateTransaction" %>


<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="photo">
        <div class="col-12 d-flex no-block align-items-center">
            <h4 class="page-title">Crate Transaction</h4>
            <div class="ms-auto text-end">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="home.aspx">Home</a></li>
                        <%-- <li class="breadcrumb-item active" aria-current="page">Library
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

        function enterToTab() {
            if (event.keyCode == 13)
                event.keyCode = 9;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Menu ID="Menu1" class="wizard clearfix" Width="100%" runat="server" BackColor="#F7F6F3" DynamicBottomSeparatorImageUrl="~/Images/background1.GIF"
        DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="Small" ForeColor="#075098"
        OnMenuItemClick="Menu1_MenuItemClick"
        Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
        StaticSubMenuIndent="10px">
        <Items>
            <asp:MenuItem Text=" VIEW CRATE TRANSACTION |" Value="0"></asp:MenuItem>
            <%-- <asp:MenuItem Text=" Verify/Submit Demand Sheet |" Value="1" Enabled="false"></asp:MenuItem>
            <asp:MenuItem Text=" Report |" Value="2" Enabled="false"></asp:MenuItem>--%>
        </Items>
        <StaticSelectedStyle BackColor="#2962FF" ForeColor="White" />
        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <DynamicHoverStyle BackColor="#e0e3f0" ForeColor="075098" />
        <DynamicMenuStyle BackColor="#2962FF" />
        <DynamicSelectedStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" Font-Names="Arial" Font-Size="Small" />
        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <StaticHoverStyle BackColor="#e0e3f0" ForeColor="075098" />
    </asp:Menu>

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="tabNew" runat="server">

            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <label for="fname" runat="server" id="lblSD" class="col-sm-3 text-end control-label col-form-label">Start Date<span style="color: red">&nbsp*</span></label>

                            <div class="col-sm-3">
                                <span></span>
                                <asp:TextBox ID="txt_startdate" class="mydatepicker" runat="server" Width="160px" AutoPostBack="true">
                                </asp:TextBox>
                                <%--<input type="text" class="form-control" id="fname" placeholder="First Name Here" />--%>
                            </div>
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">End Date<span style="color: red">&nbsp*</span></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_enddate" class="mydatepicker" runat="server" Width="160px" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txt_startdate"
                                ErrorMessage="Required Field" ForeColor="Red" Display="Static" Visible="true"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group row" id="Div_Filters" runat="server" visible="true">
                            <label id="lblSV" for="fname" runat="server" class="col-sm-3 text-end control-label col-form-label">Supervisor</label>
                            <%--<div class="form-group row" >--%>

                            <div class="col-sm-3" runat="server" id="DIV_SV" visible="true">
                                <asp:DropDownList ID="DdlSV" class="form-control   dropdown-toggle " data-bs-toggle="dropdown" runat="server" Width="144px" OnSelectedIndexChanged="DdlSV_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <%--</div>--%>
                            <%--<div class="form-group row">--%>
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">Sales Person</label>

                            <div class="col-sm-3" runat="server" id="DIV_SP" visible="true">
                                <asp:DropDownList ID="DDLSalesPerson" class="form-control   dropdown-toggle " data-bs-toggle="dropdown" runat="server" Width="144px" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--<label for="fname" class="col-sm-6 text-end control-label col-form-label" runat="server" id="lblmessgae"></label>--%>
                            <%--</div>--%>
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
                        <div class="form-group row">
                            <div class="col-sm-3">
                                <asp:Label ID="Label2" Style="color: red" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:ImageButton ID="ImageButton1" Height="25px" OnClick="Collection_VIEW_Click" runat="server"
                                    ImageUrl="~/Images/view.png" />

                                <asp:ImageButton ID="ImageButton3" OnClick="Excel_Click" runat="server" Height="20px"
                                    ImageAlign="AbsBottom" ImageUrl="~/Images/export-excel.png" Width="32px" />
                            </div>

                            <div class="col-sm-4">
                                <asp:Label ID="lblmsg" Style="color: red" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="width: auto; height: fit-content; overflow: auto" runat="server" id="Grid1div">
                <asp:GridView ID="GridView1" Style="height: 200px" runat="server" CellPadding="1"
                    CellSpacing="1" Font-Bold="False" CssClass="grid-view" Width="760px" Font-Size="Small" ForeColor="#333333"
                    GridLines="None" RowStyle-HorizontalAlign="Center" RowStyle-Wrap="false" HeaderStyle-Wrap="false"
                    AutoGenerateColumns="true" AllowPaging="True">
                    <RowStyle BackColor="white" HorizontalAlign="LEFT" Wrap="False" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="075098" Font-Bold="True" ForeColor="white" Wrap="true" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="#7ad0ed" />
                </asp:GridView>
            </div>

            <%-- <asp:ImageButton ID="CMDSave" OnClick="CMDSave_Click" runat="server" ImageUrl="~/Images/save_t.PNG" />--%>
        </asp:View>
        <asp:View ID="TABVERIFY" runat="server">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="form-group row">
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label">Demand Date</label>

                            <div class="col-sm-3">
                                <asp:TextBox ID="Vtxt_DemadDate" class="mydatepicker" runat="server" Width="160px" AutoPostBack="true"></asp:TextBox>
                                <%--<input type="text" class="form-control" id="fname" placeholder="First Name Here" />--%>
                            </div>
                            <label for="fname" class="col-sm-3 text-end control-label col-form-label" style="display: none">Item Group Name</label>

                            <div class="col-sm-3">
                                <asp:ImageButton ID="TABVERIFY_CMD_VIEW" runat="server" ImageUrl="~/Images/view.png" />

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
                                <asp:DropDownList ID="VDDlCust" class="form-control   dropdown-toggle " data-bs-toggle="dropdown" runat="server" Width="144px" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                            <label for="fname" class="col-sm-6 text-end control-label col-form-label" runat="server" id="Label1"></label>
                        </div>

                    </div>
                </div>

            </div>

            <div id="Div2" style="overflow: auto; width: 893px; height: 300px">
                <asp:GridView ID="GridView3" runat="server" CellPadding="1" CellSpacing="1"
                    Font-Bold="False" Font-Size="X-Small"
                    ForeColor="#333333" GridLines="None"
                    RowStyle-HorizontalAlign="Center" RowStyle-Wrap="false" HeaderStyle-Wrap="false">
                    <RowStyle BackColor="white" HorizontalAlign="Center" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#075098" Font-Bold="True" ForeColor="white" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="#7ad0ed" />
                </asp:GridView>
            </div>



            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="Closing Type" Width="121px"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="254px">
                            <asp:ListItem Value="1">All</asp:ListItem>
                            <asp:ListItem Value="2">Route</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <div class="album-frame">
                            <%--<asp:combobox ID="TABVERIFY_CMBROUTE" runat="server" AutoPostBack="True" Visible="False" Width="168px">
                                </asp:combobox>--%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:imagebutton id="cmdsubmit" runat="server" imagealign="AbsBottom" imageurl="~/Images/Submit.png" onclientclick="return confirm('Do you want to Submit the Demand  For this Depot?');" xmlns:asp="#unknown" />
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

            <table style="width: 773px; height: 16px" bgcolor="e0e3f0">
                <tr>
                    <td colspan="2">
                        <asp:RadioButtonList ID="tabreport_rdbtype" runat="server" Visible="false" RepeatDirection="Horizontal"
                            Width="755px">
                            <asp:ListItem Value="3">Depot Demand</asp:ListItem>
                            <asp:ListItem Value="0">Route wise Demand</asp:ListItem>
                            <asp:ListItem Value="1">Route Customer wise Demand</asp:ListItem>
                        </asp:RadioButtonList>
                        <table style="width: 497px">
                            <tr>
                                <td style="width: 112px; height: 22px" valign="top">
                                    <asp:Label ID="Label12" runat="server" Text="Demand Date" Width="134px"></asp:Label></td>
                                <td style="width: 200px; height: 22px" valign="top">
                                    <asp:TextBox ID="tabreport_txtdemanddate" runat="server" Width="123px"></asp:TextBox>
                                    <%-- <asp:CalendarExtender ID="tabreport_txtdemanddate_CalendarExtender" 
                                    runat="server" Format="dd-MMM-yyyy" DefaultView="Days" Enabled="True" PopupPosition="BottomLeft" 
                                    TargetControlID="tabreport_txtdemanddate">
                                </asp:CalendarExtender>--%>
                                </td>
                                <td style="width: 162px; height: 22px" valign="top">
                                    <asp:Label ID="Label14" runat="server" Text="Route  Name" Width="96px"></asp:Label></td>
                                <td style="height: 22px; width: 172px;">

                                    <div class="album-frame">
                                        <%--<asp:combobox ID="tabreport_cmbroutename" runat="server" AutoPostBack="True" 
                                        Width="168px"  AutoCompleteMode="Suggest" >
                                    </asp:combobox>--%>
                                    </div>

                                </td>
                                <td style="height: 22px">
                                    <asp:ImageButton ID="ImageButton7" runat="server"
                                        ImageUrl="~/Images/view.png" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 112px; height: 22px" valign="top">
                                    <asp:Label ID="Label15" runat="server" Text="Customer Name" Width="135px"></asp:Label>
                                </td>
                                <td style="width: 200px; height: 22px">

                                    <div class="album-frame">
                                        <%-- <asp:combobox ID="tabreport_cmbcustomer" runat="server" AutoPostBack="True" 
                                        Width="168px"  AutoCompleteMode="Suggest" >
                                    </asp:combobox>--%>
                                    </div>

                                </td>
                                <td style="width: 162px; height: 22px" valign="top">&nbsp;</td>
                                <td style="height: 22px; width: 172px;"></td>
                                <td style="height: 22px">
                                    <asp:ImageButton ID="ImageButton5" runat="server"
                                        ImageUrl="~/Images/export-excel.png" Height="24px" ImageAlign="AbsBottom"
                                        Width="32px" />
                                    <asp:ImageButton ID="ImageButton6" runat="server" Height="24px"
                                        ImageAlign="AbsBottom" ImageUrl="~/Images/printer.png"
                                        OnClientClick="javascript:CallPrint('divprint')" Width="24px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div style="overflow: auto; width: 840px; height: 363px" id="divprint">
                <asp:GridView ID="GridView2" runat="server" CellPadding="1" CellSpacing="1" Font-Bold="False"
                    Font-Size="X-Small" ForeColor="#333333" GridLines="None" DataSourceID="SqlDataSource2" RowStyle-HorizontalAlign="Center">
                    <RowStyle BackColor="white" HorizontalAlign="Center" Wrap="false" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#075098" Font-Bold="True" ForeColor="white" Wrap="false" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="#7ad0ed" />
                </asp:GridView>
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


