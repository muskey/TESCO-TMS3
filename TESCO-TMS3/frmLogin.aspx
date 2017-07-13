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
    <%--<link rel='stylesheet prefetch' href='http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css' />--%>

    <!--[if lte IE 9]>
		<script src="Assets/js/plugins/placeholder/jquery.placeholder.min.js"></script>
		<script>
			$(document).ready(function() {
				$('input, textarea').placeholder();
			});
		</script>
	<![endif]-->

    <script src="Scripts/ScriptJS.js"></script>
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

<body class='login' style="background: #29363f;">
    <div class="wrapper">
        <h1><a href="#">
            <img src="assets/img/logo/Login_Logo.png" alt="" class='retina-ready' style="width: 250px" /></a></h1>
        <div class="login-body">
            <form runat="server" class='form-validate' style="margin-bottom: 0px">
                <asp:Panel ID="pnlLogin" runat="server">
                    <div class="control-group">
                        <div class="form-group ">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off" data-rule-required="true"
                                Style="padding: 4px 0px; font-size: 20px; height: 26px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group" style="margin-bottom:0px;">
                        <div class="form-group ">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="รหัสผ่าน" AutoComplete="off" data-rule-required="true"
                                Style="padding: 4px 0px;margin-bottom:0px; font-size: 20px; height: 26px"  ></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="form-group ">
                            <asp:CheckBox ID="chkShowPassword" runat="server" AutoPostBack="false" />
                            <span style="color:white;font-weight:bold;font-size: 12px;">
                                แสดงรหัสผ่าน
                            </span>
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="form-group text-center">
                            <asp:Button ID="btnLogin" runat="server" CssClass="log-btn" Style="width: 100%" Text="Login" />
                            <asp:TextBox ID="txtTempMobileNo" runat="server" style="display:none;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" style="text-align: center;">
                        <asp:LinkButton ID="btnForgetPassword" runat="server" Style="text-decoration: none;">
                            <span class="text-center" style="color:white;font-weight:bold;font-size: 16px;">ลืมรหัสผ่าน</span>
                        </asp:LinkButton>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlRequestOTP" runat="server" CssClass="row-fluid" Visible="false">
                    <div class="form-group ">
                        <asp:TextBox ID="txtReqestOTPSendUsername" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off" data-rule-required="true"
                            Style="padding: 4px 0px; font-size: 20px; height: 26px"></asp:TextBox>
                    </div>
                    <div class="from-group">
                        <asp:TextBox ID="txtRequestOTPShowMobileNo" runat="server" CssClass="form-control" Enabled="false" AutoComplete="off" data-rule-required="true" 
                            Style="padding: 4px 0px;font-size:20px;height:26px"></asp:TextBox>
                    </div>
                    <div class="from-group">
                        <IMG  alt="" src="RenderCaptcha.aspx" style="width:100%;height:60px">
                    </div>
                    <div class="from-group">
                        <asp:TextBox ID="txtCaptchaText" runat="server" CssClass="form-control" placeholder="กรุณากรอกรหัสด้านบน" AutoComplete="off" data-rule-required="true" 
                            Style="padding: 4px 0px;font-size:20px;height:26px;"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:Button ID="btnSendOTP" runat="server" CssClass="log-btn" Style="width: 100%" Text="SEND OTP" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlLoginOTP" runat="server" CssClass="row-fluid" Visible="false">
                    <div class="form-group ">
                        <asp:TextBox ID="txtOTPUserLogin" runat="server" CssClass="form-control" placeholder="รหัสพนักงาน" AutoComplete="off" data-rule-required="true"
                            Style="padding: 4px 0px; font-size: 20px; height: 26px"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:TextBox ID="txtOTPCode" runat="server" CssClass="form-control" placeholder="OTP Code" AutoComplete="off" data-rule-required="true"
                            Style="padding: 4px 0px; font-size: 20px; height: 26px"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:TextBox ID="txtOTPPassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="18" placeholder="รหัสผ่านใหม่" AutoComplete="off" data-rule-required="true"
                            Style="padding: 4px 0px; font-size: 20px; height: 26px"></asp:TextBox>
                    </div>
                    <div class="form-group ">
                        <asp:TextBox ID="txtOTPConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="18" placeholder="ยืนยันรหัสผ่าน" AutoComplete="off" data-rule-required="true"
                            Style="padding: 4px 0px;margin-bottom:0px; font-size: 20px; height: 26px"></asp:TextBox>
                    </div>
                    <div class="form-group" style="margin-bottom:10px;">
                        <asp:CheckBox ID="chkShowOTPPassword" runat="server" AutoPostBack="false" />
                        <span style="color:white;font-weight:bold;font-size: 12px;">
                            แสดงรหัสผ่าน
                        </span>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnOTPLogin" runat="server" CssClass="log-btn" Style="width: 100%" Text="Login" />
                    </div>
                </asp:Panel>
            </form>
        </div>

        <div class="login-body" style="background-color:rgba(211, 211, 211, 0.12);" id="rowPssPolicy" runat="server" visible="false">
            <h4 style="color:white;font-weight:bold;">
                &nbsp;&nbsp;นโยบายการตั้งรหัสผ่าน
            </h4>
            <span style="color:white;font-weight:bold;font-size: 12px;">
                <ul>
                    <li>รหัสผ่านต้องประกอบไปด้วย ตัวอักษรพิมพ์ใหญ่, ตัวอักษรตัวพิมพ์เล็ก, ตัวอักษรตัวเลข, ตัวอักษรพิเศษ (A-Z, a-z, 0-9, !"#$%)</li>
                    <li>รหัสผ่านต้องมีความยาวต่ำสุด 8 ตัวอักษร และสูงสุด 18 ตัวอักษร</li>
                    <li>รหัสผ่านต้องไม่ซ้ำกับรหัสผ่านล่าสุด 3 ครั้งสุดท้าย</li>
                </ul>
            </span>
        </div>
        <br />
        <br />
        <br />
    </div>

    <script>

        $(function () {
            $("#chkShowPassword").bind("click", function () {
                var txtPassword = $("[id*=txtPassword]");
                if ($(this).is(":checked")) {
                    txtPassword.after('<input id = "txt_' + txtPassword.attr("id") + '" type = "text" value = "' + txtPassword.val() + '" onBlur="TextPasswordChange(txtPassword, txt_' + txtPassword.attr("id") + ');" style="padding: 4px 0px;margin-bottom:0px; font-size: 20px; height: 26px" />');
                    txtPassword.hide();
                } else {
                    txtPassword.val(txtPassword.next().val());
                    txtPassword.next().remove();
                    txtPassword.show();
                }
            });

            $("#chkShowOTPPassword").bind("click", function () {
                var txtOTPPassword = $("[id*=txtOTPPassword]");
                var txtOTPConfirmPassword = $("[id*=txtOTPConfirmPassword]");
                if ($(this).is(":checked")) {
                    txtOTPPassword.after('<input   id = "txt_' + txtOTPPassword.attr("id") + '" type = "text" value = "' + txtOTPPassword.val() + '" onBlur="TextPasswordChange(txtOTPPassword, txt_' + txtOTPPassword.attr("id") + ');" style="padding: 4px 0px; font-size: 20px; height: 26px" />');
                    txtOTPPassword.hide();

                    txtOTPConfirmPassword.after('<input id = "txt_' + txtOTPConfirmPassword.attr("id") + '" type = "text" value = "' + txtOTPConfirmPassword.val() + '" onBlur="TextPasswordChange(txtOTPConfirmPassword, txt_' + txtOTPConfirmPassword.attr("id") + ');" style="padding: 4px 0px; margin-bottom:0px; font-size: 20px; height: 26px" />');
                    txtOTPConfirmPassword.hide();
                } else {
                    txtOTPPassword.val(txtOTPPassword.next().val());
                    txtOTPPassword.next().remove();
                    txtOTPPassword.show();

                    txtOTPConfirmPassword.val(txtOTPConfirmPassword.next().val());
                    txtOTPConfirmPassword.next().remove();
                    txtOTPConfirmPassword.show();
                }
            });
        });


        function setTextPassword() {
            var txtPassword = $("[id*=txtPassword]");
            if ($("#chkShowPassword").is(":checked")) {
                txtPassword.val(txtPassword.next().val());
            } 

            return true;
        }

        function setTextOTPPassword() {
            var txtOTPPassword = $("[id*=txtOTPPassword]");
            var txtOTPConfirmPassword = $("[id*=txtOTPConfirmPassword]");

            if ($("#chkShowOTPPassword").is(":checked")) {
                txtOTPPassword.val(txtOTPPassword.next().val());
                txtOTPConfirmPassword.val(txtOTPConfirmPassword.next().val());
            }

            return true;
        }



        function TextPasswordChange(txtPassword, txtShowPassword) {
            setTimeout(function () {  //the horror...the horror...  
                $("#" + txtPassword).val($("#" + txtShowPassword).next().val());
                $("#" + txtPassword).next().remove();
                $("#" + txtPassword).show();
            }, 1);
        }


        function zeroFill(number, width) {
            width -= number.toString().length;
            if (width > 0) {
                return new Array(width + (/\./.test(number) ? 2 : 1)).join('0') + number;
            }
            return number + ""; // always return a string
        }

        function GetLoginStatus(event, txtUserName) {
            var UserName = document.getElementById(txtUserName).value;

            var startChr = UserName.substring(0, 1);
            var chrInt = startChr.charCodeAt(0);
            
            if (chrInt >= 48 && chrInt <= 57) {
                if (UserName.length < 8) {
                    UserName = "764" + zeroFill(UserName, 8);
                }
            }

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
                            var MobileNo = ret[2];

                            if (IsTelephoneExist == "true") {
                                alert("ผู้ใช้ไม่มีหมายเลขโทรศัพท์ กรุณาติดต่อผู้ดูแลระบบ");
                                setTimeout(function () {  //the horror...the horror...  
                                       document.getElementById(event.target.id).select();  
                                }, 1);    
                                return false;
                            } else if (IsFirstItmeLogin == "true") {

                                document.getElementById("<%=txtTempMobileNo.ClientID%>").value = MobileNo;
                                document.getElementById("<%=btnForgetPassword.ClientID%>").click();
                            }
                    }
                }
                },
                error: function (data) {
                }
            });

        }

        function GetMobileNo(event, txtReqestOTPSendUsername, txtRequestOTPShowMobileNo) {
            var UserName = document.getElementById(txtReqestOTPSendUsername).value;
            if (UserName.length < 8) {
                UserName = "764" + zeroFill(UserName, 8);
            }

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
                            var MobileNo = ret[2];

                            if (IsTelephoneExist == "true") {
                                alert("ผู้ใช้ไม่มีหมายเลขโทรศัพท์ กรุณาติดต่อผู้ดูแลระบบ");
                                setTimeout(function () {  //the horror...the horror...  
                                       document.getElementById(event.target.id).select();  
                                }, 1);    
                                return false;
                            } else {
                                document.getElementById("txtRequestOTPShowMobileNo").value = MobileNo;
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
