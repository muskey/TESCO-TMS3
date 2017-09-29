<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectTestCourse.aspx.vb" Inherits="TESCO_TMS.frmSelectTestCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <title></title>

    <script type="text/javascript" src="Scripts/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="Scripts/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="Scripts/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script>
        function fselect(id, title, percent, qty) {
            $.each($("button"), function (key, value) {
                //alert(key + ":" + value)
                $(value).css("background-color", "");
            });

            var control = $("#" + id);
            $(control).css("background-color", "aquamarine");

            $('#<%=txtFormatID.ClientID %>').val(id);
            $('#<%=txtFormatTitle.ClientID %>').val(title);

            CreateTransLog('<%=UserData.LoginHistoryID %>', 'เลือกแบบทดสอบ' + title);
            onConfirmTest(id, title, percent, qty);
        }

        function onConfirmTest(id, title, desc, percent, qty) {
            CreateTransLog('<%=UserData.LoginHistoryID %>', 'เลือกแบบทดสอบ' + title);

            var wWidth = $(window).width();
            var dWidth = wWidth * 0.8;
            var wHeight = $(window).height();
            var dHeight = wHeight * 0.8;

            var msg = '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
            msg += desc + '<br /><br /><br />';
            msg += 'จำนวนคำถาม ' + qty + ' ข้อ เกณฑ์คะแนน ' + percent + '%';
            var div = $("<div><h4>" + msg + "<h4></div>");
            div.dialog({
                title: title,
                width: dWidth,
                height: dHeight,
                modal: true,
                buttons: [
                            {
                                text: "เริ่มสอบ",
                                "class": 'saveButtonClass',
                                click: function () {
                                    div.dialog("close");

                                    var url = "frmSelectQuestionTest.aspx?id=" + id + "&q_id=1";
                                    CreateTransLog('<%=UserData.LoginHistoryID %>', 'เริ่มทำแบบทดสอบ' + title);
                                    window.location = url;
                                    // onDelete(id);

                                }
                            },
                            {
                                text: "ปิด",
                                "class": 'saveButtonClass',
                                click: function () {
                                    CreateTransLog('<%=UserData.LoginHistoryID %>', 'คลิกปุ่มปิด');
                                    div.dialog("close");
                                }
                            }
                ]
            });
        }
    </script>

    <style media="screen" type="text/css">
 
        .ui-widget-header {
            background: forestgreen;
        }

        .ui-button.saveButtonClass {
            color: white;
            background: forestgreen;
        }

        .ui-dialog .ui-dialog-buttonpane {
            text-align: center;
        }

            .ui-dialog .ui-dialog-buttonpane .ui-dialog-buttonset {
                float: none;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid nav-hidden" id="content">
       <%-- <font color="#019b79"><h1 class="text-center">ยินดีต้อนรับเข้าสู่</h1></font>
        <font color="#fff"><h3 class="text-center">แบบทดสอบ</h3></font>--%>
            <div class="text-center">
        <img src="Assets/PC/bgSelectTestCourseHeader.png" width="250" height="250" />
    </div>
        <div id="main" style="background: #29363f">
            <div class="container-fluid">
                <div class="box-content">
                    <div class="row-fluid">
                        <div class="span2"></div>
                        <div class="span3">
                            <div class="box-content">
                                <span style="color:#fff"><h4 class="text-center">กรุณาเลือกบททดสอบ</h4></span>
                                <a href="#modal-1" data-toggle="modal"></a>
                                <asp:Label runat="server" ID="lblBotton"></asp:Label>
                            </div>
                        </div>

                        <div class="span1"></div>
                        <div class="span4">
                            <div class="box " style="text-align:center">
                                <span class="text-center" style="color:#019b79;font-size:32px;" >สถิติเบื้องต้นของคุณ</span>
                                <div style="width: 100%; height: 340px;">
                                    <div class="box-content" style="background: #29363f;text-align:left;">
                                        <span style="color:#fff">
                                            <asp:Label runat="server" ID="lblNEWS"></asp:Label>
								        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="span2"></div>
                    </div>
                    <div class="clearfix"></div>
                    <br />
                    <div class="row-fluid" style="display:none">
                        <div class="span5"></div>
                        <div class="span2 text-center">

                            <asp:Button ID="btnFormat" runat="server" CssClass="btn-block btn btn-large btn-green" Text="หลักสูตร" />
                            <asp:TextBox ID="txtFormatID" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtFormatTitle" runat="server"></asp:TextBox>
                        </div>
                        <div class="span5"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

