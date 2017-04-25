<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmLogin.aspx.vb" Inherits="TESCO_TMS3.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel='stylesheet prefetch' href='http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css' />
    <link rel="stylesheet" href="Assets/css/login_style.css" />


</head>
<body style="background:#29363f">
    <form id="form1" runat="server">
    <div>
        <div class="span6">
            <div class="login-form1" style="background:#29363f">
                <img src="assets/img/logo/Login_Logo.png" width="100%" />
            </div>
        </div>
        <div class="span6">
            <div class="login-form2" style="background:#29363f">
                <div class="form-group ">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" required></asp:TextBox>
                    <i class="fa fa-user"></i>
                </div>
                <div class="form-group log-status">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" required></asp:TextBox>
                    <i class="fa fa-lock"></i>
                </div>
                <span class="alert">Invalid Credentials</span>
                <asp:Button ID="btnLogin" runat="server" CssClass="log-btn" Text="Login" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
