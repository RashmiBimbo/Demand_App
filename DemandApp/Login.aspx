﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DemandApp.LoginPage" %>

<!DOCTYPE html>

<html dir="ltr">

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
    <link href="dist/css/style.min.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>

<body>
    <div class="main-wrapper">


        <!-- ============================================================== -->
        <!-- Preloader - style you can find in spinners.css -->
        <!-- ============================================================== -->
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
        <div class="auth-wrapper d-flex no-block justify-content-center align-items-center bg-darkl" style="min-height:550px;background:#325272">
            <div class="auth-box bg-dark border-top border-secondary">
                <div id="loginform">
                    <div class="text-center pt-3 pb-3" style="color: white">
                        <span class="db">
                             <img src="../assets/images/logo.png" alt="logo" />
                           <%-- BIMBO DEMAND LOGIN--%>

                        </span>
                    </div>
                    <!-- Form -->
                    <%--<form id="form2" runat="server">--%>
                    <form runat="server" class="form-horizontal mt-3" >
                        <div class="row pb-4">
                            <div class="col-12">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-success text-white h-100" id="basic-addon1"><i class="fas fa-id-card"></i></span>
                                    </div>

                                    <asp:DropDownList ID="LstCmpny" class="form-control   btn-primary dropdown-toggle " data-bs-toggle="dropdown" runat="server" Width="144px">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="LstLocn" Visible="false" runat="server" Width="144px">
                                    </asp:DropDownList>


                                </div>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-success text-white h-100" id="basic-addon1"><i class="mdi mdi-account fs-4"></i></span><%--<i class="mdi mdi-lock fs-4"></i>--%>
                                    </div>
                                    <asp:TextBox ID="TxtUserId" class="form-control form-control-lg" placeholder="User Id" aria-label="user id" aria-describedby="basic-addon1" runat="server"></asp:TextBox>


                                </div>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text bg-warning text-white h-100" id="basic-addon2"><i class="mdi mdi-lock fs-4"></i></span>
                                    </div>
                                    <asp:TextBox ID="TxtPswrd" class="form-control form-control-lg" placeholder="Password" aria-label="Password" aria-describedby="basic-addon1" runat="server" TextMode="Password"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="row border-top border-secondary">
                            <div class="col-12">
                                <div class="form-group">
                                    <div class="pt-3">

                                        <asp:Button runat="server" ID="btnLogin" class=" btn btn-success float-end text-white" Text="Log In" OnClick="btnLogin_Click" />

                                    </div>
                                    <div class="pt-3">

                                        <asp:Label ID="Label1"  runat="server" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>

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
        // ==============================================================
        // Login and Recover Password
        // ==============================================================
        $("#to-recover").on("click", function () {
            $("#loginform").slideUp();
            $("#recoverform").fadeIn();
        });
        $("#to-login").click(function () {
            $("#recoverform").hide();
            $("#loginform").fadeIn();
        });
    </script>
</body>
</html>
