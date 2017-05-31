<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmLogin.aspx.vb" Inherits="TESCO_TMS3.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
	<!-- Apple devices fullscreen -->
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<!-- Apple devices fullscreen -->
	<meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <!-- Bootstrap -->
    <link rel="stylesheet" href="Assets/css/login_style.css">
	<!-- Bootstrap responsive -->
	<link rel="stylesheet" href="Assets/css/bootstrap-responsive.min.css">
	<!-- jQuery UI -->
	<link rel="stylesheet" href="Assets/css/plugins/jquery-ui/smoothness/jquery-ui.css">
	<link rel="stylesheet" href="Assets/css/plugins/jquery-ui/smoothness/jquery.ui.theme.css">
	<!-- Notify -->
	<link rel="stylesheet" href="Assets/css/plugins/gritter/jquery.gritter.css">
	<!-- Theme CSS -->
	<link rel="stylesheet" href="Assets/css/style.css">
	<!-- Color CSS -->
	<link rel="stylesheet" href="Assets/css/themes.css">
	<!-- select2 -->
	<link rel="stylesheet" href="Assets/css/plugins/select2/select2.css">
	<!-- icheck -->
	<link rel="stylesheet" href="Assets/css/plugins/icheck/all.css">
	<!-- chosen -->
	<link rel="stylesheet" href="Assets/css/plugins/chosen/chosen.css">
	<!-- multi select -->
	<link rel="stylesheet" href="Assets/css/plugins/multiselect/multi-select.css">

    <link rel='stylesheet prefetch' href='http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css' />

</head>
<body  style="background:#29363f;">
    <form id="form1" runat="server">
        <div>
            <div class="container-fluid wrapper">
                <div class="box-content">

                        <div class="row-fluid">
                            <div class="span3"></div>
                            <div class="span3" style="background:#29363f;text-align:center">
                                <img src="assets/img/logo/Login_Logo.png" style="width:100%"   />
                            </div>
                            <div class="span3" style="background:#29363f">
                                <asp:Panel ID="pnlLogin" runat="server" CssClass="row-fluid">
                                    <div class="form-group ">
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off" required AutoPostBack="true" ></asp:TextBox>
                                        <i class="fa fa-user fa-2x"></i>
                                    </div>
                                    <div class="form-group log-status">
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="รหัสผ่าน" AutoComplete="off" required></asp:TextBox>
                                        <i class="fa fa-lock fa-2x"></i>
                                    </div>
                                    <div class="form-group ">
                                        <span class="alert">Invalid Credentials</span>
                                        <asp:Button ID="btnLogin" runat="server" CssClass="log-btn" Text="Login" />
                                    </div>
                                    <div class="form-group" style="text-align: center;">
                                        <asp:LinkButton ID="btnForgetPassword" runat="server" style="text-decoration:none;" >
                                            <span class="text-center" style="color:white;font-weight:bold;font-size: 16px;">ลืมรหัสผ่าน</span>
                                        </asp:LinkButton>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlRequestOTP" runat="server" CssClass="row-fluid" Visible="false">
                                    <div class="form-group ">
                                        <asp:TextBox ID="txtReqestOTPSendUsername" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off" required></asp:TextBox>
                                        <i class="fa fa-user fa-2x"></i>
                                    </div>
                                    <div class="form-group ">
                                        <asp:Button ID="btnSendOTP" runat="server" CssClass="log-btn" Text="SEND OTP" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlLoginOTP" runat="server" CssClass="row-fluid" Visible="false">
                                    <div class="form-group ">
                                        <asp:TextBox ID="txtOTPUserLogin" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off" required></asp:TextBox>
                                        <i class="fa fa-user fa-2x"></i>
                                    </div>
                                    <div class="form-group ">
                                        <asp:TextBox ID="txtOTPCode" runat="server" CssClass="form-control" placeholder="OTP Code" AutoComplete="off" required></asp:TextBox>
                                        <i class="fa fa-mobile fa-2x"></i>
                                    </div>
                                    <div class="form-group ">
                                        <asp:TextBox ID="txtOTPPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="รหัสผ่านใหม่" AutoComplete="off" required></asp:TextBox>
                                        <i class="fa fa-lock fa-2x"></i>
                                    </div>
                                    <div class="form-group ">
                                        <asp:TextBox ID="txtOTPConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="ยืนยันรหัสผ่าน" AutoComplete="off" required></asp:TextBox>
                                        <i class="fa fa-lock fa-2x"></i>
                                    </div>
                                    <div class="form-group ">
                                        <asp:Button ID="btnOTPLogin" runat="server" CssClass="log-btn" Text="Login" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="span3"></div>
                        </div>
                </div>
            </div>
        </div>
    </form>
</body>

