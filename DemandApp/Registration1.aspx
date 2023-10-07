<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Registration1.aspx.cs" Inherits="DemandApp.Registration1" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Welcome to Harvest Gold</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta
        name="keywords"
        content="wrappixel, admin dashboard, html css dashboard, web dashboard, bootstrap 5 admin, bootstrap 5, css3 dashboard, bootstrap 5 dashboard, Matrix lite admin bootstrap 5 dashboard, frontend, responsive bootstrap 5 admin template, Matrix admin lite design, Matrix admin lite dashboard bootstrap 5 dashboard template" />
    <meta
        name="description"
        content="Matrix Admin Lite Free Version is powerful and clean admin dashboard template, inpired from Bootstrap Framework" />
    <meta name="robots" content="noindex,nofollow" />
    <%-- <title>Matrix Admin Lite Free Versions Template by WrapPixel</title>--%>
    <!-- Favicon icon -->
    <link
        rel="icon"
        type="image/png"
        sizes="16x16"
        href="assets/images/favicon.png" />
    <!-- Custom CSS -->
    <%--<link href="../dist/css/style.min.css" rel="stylesheet" />--%>
    <link href="../dist/css/style.min.css" rel="stylesheet" />

    <%--    <link href="../App_Themes/Black/Default.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Black/Default.css" rel="stylesheet" type="text/css" />--%>
    <!--css start -->
</head>

<body>

    <div class="preloader">
        <div class="lds-ripple">
            <div class="lds-pos"></div>
            <div class="lds-pos"></div>
        </div>
    </div>
    <!-- ============================================================== -->
    <!-- Preloader - style you can find in spinners.css -->
    <!-- ============================================================== -->
    <!-- ============================================================== -->
    <!-- Login box.scss -->
    <!-- ============================================================== -->
    <div class="auth-wrapper d-flex no-block justify-content-center align-items-center bg-dark">
        <div class="auth-box bg-dark border-top border-secondary">
            <div id="loginform">
                <div class="text-center pt-3 pb-3" >
                  <a class="dropdown-item" href="../home.aspx">   <span class="db" style="color: white">
                         <img src="../assets/images/logo.png" alt="logo" />
                        <br />
                             Registration Details

                    </span>

                      </a>
                </div>
                <!-- Form -->
                <%--<form id="form2" runat="server">--%>
                <form runat="server" class="form-horizontal mt-3">
                    <div class="row pb-4">
                        <div class="col-12">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text bg-success text-white h-100" id="basic-addon1"><i class="fas fa-id-card"></i></span>
                                </div>
                                <asp:RadioButton ID="RBCust"  GroupName="group1" runat="server" Text="Customer/Dealer" Class="form-check-label mb-0" style="color:white"  OnCheckedChanged="loginid_CheckedChanged" AutoPostBack="true"  />
                                <asp:RadioButton ID="RBSales"  GroupName="group1" runat="server" Text="Salesperson"  Class="form-check-label mb-0" style="color:white" OnCheckedChanged="loginid_CheckedChanged" AutoPostBack="true" />

                              


                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                     <span class="input-group-text bg-success text-white h-100" id="basic-addon1"><i class="fas fa-id-card"></i></span>
                                </div>
                               
                                  <asp:DropDownList ID="DDlcompany" class="form-control   btn-primary dropdown-toggle " data-bs-toggle="dropdown" runat="server" Width="144px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DdlUser" Visible="false" runat="server" Width="144px">
                                </asp:DropDownList>

                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text bg-success text-white h-100" id="basic-addon1"><i class="mdi mdi-account fs-4"></i></span><%--<i class="mdi mdi-lock fs-4"></i>--%>
                                </div>
                                <asp:TextBox ID="TxtUserID" OnTextChanged="TxtUserID_TextChanged" AutoPostBack="true" class="form-control form-control-lg" placeholder="Sustomer Id or Sales person Id" aria-label="user id" aria-describedby="basic-addon1" runat="server"></asp:TextBox>


                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text bg-warning text-white h-100" id="basic-addon2"><i class="mdi mdi-clipboard-text fs-4"></i></span>
                                </div>
                                <asp:TextBox ID="Txtname" class="form-control form-control-lg" placeholder="User Name" aria-label="Name" aria-describedby="basic-addon1" runat="server" ></asp:TextBox>

                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text bg-warning text-white h-100" id="basic-addon2"><i class="mdi mdi-email"></i></span>
                                </div>
                                <asp:TextBox ID="TxtMail" class="form-control form-control-lg" placeholder="Mail Id" aria-label="mail" aria-describedby="basic-addon1" runat="server" ></asp:TextBox>

                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text bg-warning text-white h-100" id="basic-addon2"><i class="mdi mdi-phone"></i></span>
                                </div>
                                <asp:TextBox ID="TxtPhn" class="form-control form-control-lg" placeholder="contact no" aria-label="contact" aria-describedby="basic-addon1" runat="server" ></asp:TextBox>

                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text bg-warning text-white h-100" id="basic-addon2"><i class="mdi mdi-apps"></i></span>
                                </div>
                                <asp:TextBox ID="txtadd" class="form-control form-control-lg" placeholder="Addres" aria-label="Address" aria-describedby="basic-addon1" runat="server" ></asp:TextBox>

                            </div>
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text bg-warning text-white h-100" id="basic-addon2"><i class="mdi mdi-book"></i></span>
                                </div>
                                <asp:TextBox ID="txtGstNo" class="form-control form-control-lg" placeholder="GST No" aria-label="GST No" aria-describedby="basic-addon1" runat="server" ></asp:TextBox>

                            </div>
                           
                        </div>
                    </div>
                    <div class="row border-top border-secondary">
                        <div class="col-12">
                            <div class="form-group">
                                <div class="pt-3">

                                    
                                    <asp:Button runat="server" ID="BtnSubmit" class=" btn btn-success float-end text-white" Text="Sign Up" OnClick="BtnSubmit_Click" />
                                    <asp:Label ID="lblmsg" runat="server" Width="175px" Text="" Font-Bold="False" Font-Names="Verdana" Font-Size="7pt" ForeColor="#000000"></asp:Label>
                                </div>
                               
                            </div>
                        </div>
                    </div>
                </form>
            </div>


        </div>
        <!-- ============================================================== -->
        <!-- Login box.scss -->
        <!-- ============================================================== -->
        <!-- ============================================================== -->
        <!-- Page wrapper scss in scafholding.scss -->
        <!-- ============================================================== -->
        <!-- ============================================================== -->
        <!-- Page wrapper scss in scafholding.scss -->
        <!-- ============================================================== -->
        <!-- ============================================================== -->
        <!-- Right Sidebar -->
        <!-- ============================================================== -->
        <!-- ============================================================== -->
        <!-- Right Sidebar -->
        <!-- ============================================================== -->
        <%-- </form>--%>
    </div>
    
    <!--here end outerwrapper-->
    <!-- ============================================================== -->
    <!-- All Required js -->
    <!-- ============================================================== -->
    <script src="../assets/libs/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap tether Core JavaScript -->
    <script src="../assets/libs/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <!-- ============================================================== -->
    <!-- This page plugin js -->
    <!-- ============================================================== -->
    <script>
        $(".preloader").fadeOut();
    </script>
</body>
</html>
