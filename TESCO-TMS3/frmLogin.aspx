<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmLogin.aspx.vb" Inherits="TESCO_TMS3.frmLogin" %>

<!doctype html>
<html>
<head>
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
	<title>Login</title>

	<!-- Bootstrap -->
	<link rel="stylesheet" href="Assets/css/bootstrap.min.css">
	<!-- Bootstrap responsive -->
	<link rel="stylesheet" href="Assets/css/bootstrap-responsive.min.css">
	<!-- icheck -->
	<link rel="stylesheet" href="Assets/css/plugins/icheck/all.css">
	<!-- Theme CSS -->
	<link rel="stylesheet" href="Assets/css/style.css">
	<!-- Color CSS -->
	<link rel="stylesheet" href="Assets/css/themes.css">


	<!-- jQuery -->
	<script src="Assets/js/jquery.min.js"></script>
	
	<!-- Nice Scroll -->
	<script src="Assets/js/plugins/nicescroll/jquery.nicescroll.min.js"></script>
	<!-- Validation -->
	<script src="Assets/js/plugins/validation/jquery.validate.min.js"></script>
	<script src="Assets/js/plugins/validation/additional-methods.min.js"></script>
	<!-- icheck -->
	<script src="Assets/js/plugins/icheck/jquery.icheck.min.js"></script>
	<!-- Bootstrap -->
	<script src="Assets/js/bootstrap.min.js"></script>
	<script src="Assets/js/eakroko.js"></script>
    <link rel='stylesheet prefetch' href='http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css' />

	<!--[if lte IE 9]>
		<script src="Assets/js/plugins/placeholder/jquery.placeholder.min.js"></script>
		<script>
			$(document).ready(function() {
				$('input, textarea').placeholder();
			});
		</script>
	<![endif]-->

    <style>
        .log-btn {
          background: #0AC986;
          dispaly: inline-block;
          width: 100%;
          font-size: 16px;
          height: 50px;
          color: #fff;
          text-decoration: none;
          border: none;
          -moz-border-radius: 4px;
          -webkit-border-radius: 4px;
          border-radius: 4px;
        }
    </style>
</head>

<body class='login' style="background:#29363f;">
	<div class="wrapper">
        <h1><a href="#"><img src="assets/img/logo/Login_Logo.png" alt="" class='retina-ready' style="width:250px" /></a></h1>
        <div class="login-body">
			<form runat="server" class='form-validate' style="margin-bottom:0px" >
                <asp:Panel ID="pnlLogin" runat="server" >
                    <div class="control-group">
					    <div class="form-group ">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off"  data-rule-required="true" 
                                style="padding:4px 0px;font-size:20px;height:26px" ></asp:TextBox>
                        </div>
				    </div>
                    <div class="control-group">
                        <div class="form-group ">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="รหัสผ่าน" AutoComplete="off" data-rule-required="true" 
                                Style="padding: 4px 0px;font-size:20px;height:26px"></asp:TextBox>
                        </div>
                    </div>
				    <div class="control-group">
                        <div class="form-group text-center">
                            <asp:Button ID="btnLogin" runat="server" CssClass="log-btn" style="width:100%" Text="Login" />
                        </div>
				    </div>
                    <div class="form-group" style="text-align: center;">
                        <asp:LinkButton ID="btnForgetPassword" runat="server" style="text-decoration:none;" >
                            <span class="text-center" style="color:white;font-weight:bold;font-size: 16px;">ลืมรหัสผ่าน</span>
                        </asp:LinkButton>
                    </div>
				</asp:Panel>
                <asp:Panel ID="pnlRequestOTP" runat="server" CssClass="row-fluid" Visible="false">
                    <div class="form-group ">
                        <asp:TextBox ID="txtReqestOTPSendUsername" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off" data-rule-required="true" 
                            Style="padding: 4px 0px;font-size:20px;height:26px"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:Button ID="btnSendOTP" runat="server" CssClass="log-btn" style="width:100%" Text="SEND OTP" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlLoginOTP" runat="server" CssClass="row-fluid" Visible="false">
                    <div class="form-group ">
                        <asp:TextBox ID="txtOTPUserLogin" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off" data-rule-required="true" 
                            Style="padding: 4px 0px;font-size:20px;height:26px"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:TextBox ID="txtOTPCode" runat="server" CssClass="form-control" placeholder="OTP Code" AutoComplete="off" data-rule-required="true" 
                            Style="padding: 4px 0px;font-size:20px;height:26px"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:TextBox ID="txtOTPPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="รหัสผ่านใหม่" AutoComplete="off" data-rule-required="true" 
                            Style="padding: 4px 0px;font-size:20px;height:26px"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:TextBox ID="txtOTPConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="ยืนยันรหัสผ่าน" AutoComplete="off" data-rule-required="true" 
                            Style="padding: 4px 0px;font-size:20px;height:26px"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:Button ID="btnOTPLogin" runat="server" CssClass="log-btn" style="width:100%" Text="Login" />
                    </div>
                </asp:Panel>
			</form>
		</div>
	</div>

    <script>
        function GetLoginStatus(txtUserName) {
            var UserName = txtUsername.value;
            //alert(UserName);

            //;


            var param = "{'UserName':" + JSON.stringify(UserName) + "}";
            $.ajax({
                type: "POST",
                url: "WebService/WebService.asmx/GetLoginStatus",
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    var strvalue = data.d;
                    if (strvalue != '') {
                        var ret = strvalue.split("#")
                        if (ret.length == 3) {
                            var IsFirstItmeLogin = ret[0];
                            var IsTelephoneExist = ret[1];

                            if (IsTelephoneExist == "true") {
                                alert("ผู้ใช้ไม่มีหมายเลขโทรศัพท์ กรุณาติดต่อผู้ดูแลระบบ");
                            } else if (IsFirstItmeLogin == "true") {
                                document.getElementById("<%=btnForgetPassword.ClientID%>").click();
                            }
                        }
                    }
                },
                error: function (data) {
                }
            });
        }
    </script>
</body>

</html>
