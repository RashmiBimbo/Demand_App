<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="DemandApp.Registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Welcome in Harvest Gold</title>
    <link href="../App_Themes/Black/Default.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Black/Default.css" rel="stylesheet" type="text/css" />
    <!--css start -->
</head>

<body>
    <form id="form1" runat="server">
        <!--here start outerwrapper-->
        <div id="outerwrapper">
            <!--here start innerwrapper-->
            <div id="wrapper">
                <!--here start main contant-->
                <div id="main" style="background-color: #ffec95">
                    <div id="top">
                        <div class="flag"></div>
                    </div>
                    <div id="login">
                        <div class="signin">
                            <div class="sign_up">
                                Sign up<br />
                                <div>
                                    <asp:RadioButton ID="RBCust" runat="server"    Text="Customer/Dealer" GroupName="loginid" OnCheckedChanged="loginid_CheckedChanged" AutoPostBack="true" Checked="True" />
                                    <asp:RadioButton ID="RBSales" runat="server"  Text="Salesperson"  GroupName="loginid"   OnCheckedChanged="loginid_CheckedChanged" AutoPostBack="true"/>
                                </div>
                                 <div>
                                    <asp:Label ID="lblcompany" runat="server" Text="Company ID"></asp:Label>
                                </div>
                                <div class="txtbox" style="width: 140px">
                                    
                                   <asp:DropDownList ID="DDlcompany" runat="server" Width="139px"></asp:DropDownList>
                                </div>
                                <div>
                                    <asp:Label ID="Lbluserid" runat="server" Text="User ID"></asp:Label>
                                </div>
                                <div class="txtbox" style="width: 140px">
                                    <asp:DropDownList ID="DdlUser" runat="server" Visible="false" Width="139px"></asp:DropDownList>
                                    <asp:TextBox ID="TxtUserID" runat="server" Width="133px" Height="16px" OnTextChanged="TxtUserID_TextChanged" AutoPostBack="true" CausesValidation="True"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Label ID="LblId" runat="server" Text="Name"></asp:Label>
                                </div>
                                <div class="txtbox" style="width: 140px">
                                    <asp:TextBox ID="Txtname" runat="server" Width="133px" Height="16px"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Label ID="LblMailId" runat="server" Text="Email"></asp:Label>
                                </div>
                                <div class="txtbox" style="width: 140px">
                                    <asp:TextBox ID="TxtMail" runat="server" TextMode="Email" Width="133px" Height="16px" CausesValidation="True"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Label ID="LblPhn" runat="server" Text="Phone No."></asp:Label>
                                </div>
                                <div class="txtbox" style="width: 140px">
                                    <asp:TextBox ID="TxtPhn" runat="server" TextMode="Phone" Width="133px" Height="16px" CausesValidation="True"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Label ID="lbladd" runat="server" Text="Address"></asp:Label>
                                </div>
                                <div class="txtbox" style="width: 140px">
                                    <asp:TextBox ID="txtadd" runat="server" TextMode="MultiLine" Width="133px" Height="16px" CausesValidation="True"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Label ID="lblGstNo" runat="server" Text="GST No"></asp:Label>
                                </div>
                                <div class="txtbox" style="width: 140px">
                                    <asp:TextBox ID="txtGstNo" runat="server" TextMode="Number" Width="133px" Height="16px" CausesValidation="True"></asp:TextBox>
                                </div>
                                <asp:ImageButton ID="ImgBtnSbmt" runat="server" ImageUrl="~/Images/login_button.png" OnClick="ImgBtnSbmt_Click" /><br />
                                <asp:Label ID="lblmsg" runat="server" Width="175px" Text="" Font-Bold="False" Font-Names="Verdana" Font-Size="7pt" ForeColor="#000000">
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                    <!--here end main contant-->

                    <!--start buttom contant-->
                    <div id="buttom">
                    </div>
                    <!--End buttom contant-->
                </div>
            </div>
            <asp:Label ID="lblip" runat="server" Width="175px" Font-Bold="False" Font-Names="Verdana" Font-Size="7pt"
                ForeColor="Black" Visible="False">
            </asp:Label>
        </div>
    </form>
    <!--here end outerwrapper-->

</body>
</html>
