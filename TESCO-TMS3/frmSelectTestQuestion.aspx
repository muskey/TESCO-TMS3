<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectTestQuestion.aspx.vb" Inherits="TESCO_TMS3.frmSelectTestQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <title>FLAT - Tiles</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <script>

        function onConfirmTest(value, valuetrue, choice, lastchoice) {

            var title = 'เฉลย ข้อ ' + choice;
            var dialogid = $('#<%=txtQuestion_Dialog.ClientID %>').val();
            var msg = '';
            var bc = '';
            var bt = ''
            if (value == 'True') {
                //title = '';
                msg += '<font color="#019b79"><h4>คุณตอบถูก<h4></font>'
                // msg += '<font color="#019b79"><h4>' + valuetrue + '<h4></font>';
                bc = 'forestgreen';
                bt = 'NextButtonClassTrue';
            } else {

                msg = '<font color="#019b79"><h4>คุณตอบผิด</font>'
                msg += '<font color="#019b79"><h4>คำตอบที่ถูกคือ<h4></font>'
                msg += '<font color="#019b79"><h4>' + valuetrue + '<h4></font>';
                bc = 'red';
                bt = 'NextButtonClassFalse';
            }

            var div = $("<div>" + msg + "</div>");
            div.dialog({
                title: title,
                modal: true,
                buttons: [
                            {
                                text: "ต่อไป",
                                "class": bt,
                                click: function () {

                                    $('#<%=lblA.ClientID %>').text($('#<%=lblA2.ClientID %>').text());
                                    $('#<%=lblB.ClientID %>').text($('#<%=lblB2.ClientID %>').text());
                                    $('#<%=lblC.ClientID %>').text($('#<%=lblC2.ClientID %>').text());
                                    $('#<%=lblD.ClientID %>').text($('#<%=lblD2.ClientID %>').text());
                                    $('#<%=lblQDetail.ClientID %>').text($('#<%=lblQDetail2.ClientID %>').text());
                                    $('#<%=lblQNumber.ClientID %>').text($('#<%=lblQNumber2.ClientID %>').text());
                                    $('#<%=imgQ.ClientID %>').attr("src", $('#<%=lblImage2.ClientID %>').text());

                                    //$('input:checkbox[name=checkme]').attr('checked', true);
                                    $('#<%=ckbA.ClientID %>').attr('checked', false);
                                    $('#<%=ckbB.ClientID %>').attr('checked', false);
                                    $('#<%=ckbC.ClientID %>').attr('checked', false);
                                    $('#<%=ckbD.ClientID %>').attr('checked', false);

                                    if (lastchoice == '0') {
                                        div.dialog("close");
                                    } else {
                                        div.dialog("close");
                                        onSummary(dialogid);
                                    }
                                }
                            }
                        ]
                    }).prev(".ui-dialog-titlebar").css("background", bc);
                }

        function onCheckSelect() {


            var div = $("<div>กรุณาเลือกคำตอบ</div>");
            div.dialog({
                title: 'เตือน',
                modal: true,
                buttons: [
                            {
                                text: "ปิด",
                                "class": 'NextButtonClassTrue',
                                click: function () {
                                    div.dialog("close");

                                }
                            }
                ]
            }).prev(".ui-dialog-titlebar").css("background", "forestgreen");
        }

        function onSummary(dialogid) {

            var wWidth = $(window).width();
            var dWidth = wWidth * 0.65;
            var wHeight = $(window).height();
            var dHeight = wHeight * 0.65;

            var bt = '';
            var bt = ''
            if (dialogid == 'dialogtrue') {
                bc = 'forestgreen';
                bt = 'NextButtonClassTrue';
            } else {
                bc = 'red';
                bt = 'NextButtonClassFalse';
            }

            var div = $("#" + dialogid);
            div.dialog({
                title: 'สรุปผลการทดสอบ',
                width: dWidth,
                height: dHeight,
                modal: true,
                buttons: [
                            {
                                text: "กลับหน้าแรก",
                                "class": bt,
                                click: function () {
                                    div.dialog("close");
                                    var url = "frmSelectTestCourse.aspx";
                                    //alert(url);
                                    window.location = url;
                                }
                            }
                ]
            }).prev(".ui-dialog-titlebar").css("background", bc);
        }


        function onConfirmCheck(choice) {
            //alert(choice);
            $('#<%=txtQuestion_Choice.ClientID %>').val(choice);

            if (choice == 0) {
                $('#<%=ckbA.ClientID %>').attr('checked', true);
                $('#<%=ckbB.ClientID %>').attr('checked', false);
                $('#<%=ckbC.ClientID %>').attr('checked', false);
                $('#<%=ckbD.ClientID %>').attr('checked', false);
            } else if (choice == 1) {
                $('#<%=ckbA.ClientID %>').attr('checked', false);
                $('#<%=ckbB.ClientID %>').attr('checked', true);
                $('#<%=ckbC.ClientID %>').attr('checked', false);
                $('#<%=ckbD.ClientID %>').attr('checked', false);
            } else if (choice == 2) {
                $('#<%=ckbA.ClientID %>').attr('checked', false);
                $('#<%=ckbB.ClientID %>').attr('checked', false);
                $('#<%=ckbC.ClientID %>').attr('checked', true);
                $('#<%=ckbD.ClientID %>').attr('checked', false);

            } else if (choice == 3) {
                $('#<%=ckbA.ClientID %>').attr('checked', false);
                $('#<%=ckbB.ClientID %>').attr('checked', false);
                $('#<%=ckbC.ClientID %>').attr('checked', false);
                $('#<%=ckbD.ClientID %>').attr('checked', true);
            }

        }

        function onConfirmCheckYesNo(choice) {
            $('#<%=txtQuestion_Choice.ClientID %>').val(choice);

            if (choice == 0) {
                $('#<%=chkAnsYes.ClientID %>').attr('checked', true);
                $('#<%=chkAnsNo.ClientID %>').attr('checked', false);
            } else if (choice == 1) {
                $('#<%=chkAnsYes.ClientID %>').attr('checked', false);
                $('#<%=chkAnsNo.ClientID %>').attr('checked', true);
            }
        }
    </script>

    <style media="screen" type="text/css">
        .ui-widget-header {
            background: forestgreen;
        }

        .ui-button.NextButtonClassTrue {
            color: white;
            background: forestgreen;
        }

        .ui-button.NextButtonClassFalse {
            color: white;
            background: red;
        }

        .ui-dialog .ui-dialog-buttonpane {
            text-align: center;
        }

            .ui-dialog .ui-dialog-buttonpane .ui-dialog-buttonset {
                float: none;
            }

        .mycheckBig input {
            width: 18px;
            height: 18px;
        }

        .mycheckSmall input {
            width: 10px;
            height: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
            <div class="breadcrumbs" style="background: #29363f">
                <asp:Label runat="server" ID="lblTestTitle" Font-Size="Larger"></asp:Label>
            </div>

            <div class="container-fluid" >
                <div class="box-content" >
                    <div class="row-fluid">
                        <%--<div class="span1"></div>--%>
                        <div class="span12"  >
                            <div class="box box-bordered box-color">

                                    <div class="tab-content padding tab-content-inline tab-content-bottom" >
                                        <div class="row"  >
                                            <div class="span2" >
                                                <button class="btn-block btn btn-large btn-green">
                                                    <asp:Label ID="lblQNumber" runat="server"></asp:Label>
                                                </button>
                                            </div>
                                            <div class="span8">
                                                <button class="btn-block btn btn-large" style="background: #fff;text-align:left">
                                                    <asp:Label ID="lblQDetail" runat="server"></asp:Label>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="row"><div class="span12"></div> </div>

                                        <asp:Panel ID="pnlQuestionABCD" runat="server" CssClass="tab-pane active" Visible="false" >
                                            <div class="row">
                                                <div class="span2"></div>
                                                <div class="span3">
                                                    <img src="Assets/PC/noimagefound.jpg" height="200" width="200" runat="server" id="imgQ" />
                                                </div>
                                                <div class="span7">
                                                    <div class="check-line">
                                                        <asp:CheckBox ID="ckbA" runat="server" CssClass="mycheckBig"   />
                                                        <label class='inline' for="ckbA" runat="server" id="lblA" style="width: 100%; font-size: large;color:white;"></label>
                                                    </div>
                                                    <div class="check-line">
                                                        <asp:CheckBox ID="ckbB" runat="server" CssClass="mycheckBig"   />
                                                        <label class='inline' for="ckbB" runat="server" id="lblB" style="width: 100%; font-size: large;color:white;"></label>
                                                    </div>
                                                    <div class="check-line">
                                                        <asp:CheckBox ID="ckbC" runat="server" CssClass="mycheckBig"   />
                                                        <label class='inline' for="ckbC" runat="server" id="lblC" style="width: 100%; font-size: large;color:white;"></label>
                                                    </div>
                                                    <div class="check-line">
                                                        <asp:CheckBox ID="ckbD" runat="server" CssClass="mycheckBig"   />
                                                        <label class='inline' for="ckbD" runat="server" id="lblD" style="width: 100%; font-size: large;color:white;"></label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </asp:Panel>

                                        <asp:Panel ID="pnlQuestionMatching" runat="server" CssClass="tab-pane active" Visible="false">
                                            <div class="row">
                                                <div class="span2"></div>
                                                <div class="span10">
                                                    <img src="Assets/PC/noimagefound.jpg" style="width:100%;height:200px;margin-bottom:20px" runat="server" id="img1" />
                                                </div>
                                            </div>
                                            
                                            <asp:Repeater ID="rptQuestionMatching" runat="server">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="span2"></div>
                                                        <div class="span1">
                                                            <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control"  ></asp:TextBox>
                                                        </div>
                                                        <div class="span4">
                                                            <asp:Label ID="lblQuestion" runat="server" style="font-size: large;color:white;" ></asp:Label>
                                                        </div>
                                                        <div class="span4">
                                                            <asp:Label ID="lblAnswer" runat="server" style="font-size: large;color:white;" ></asp:Label>
                                                        </div>
                                                        <div class="span1"></div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlQuestionYesNo" runat="server" CssClass="tab-pane active" Visible="false">
                                            <div class="row">
                                                <div class="span2"></div>
                                                <div class="span8">
                                                    <div class="check-line">
                                                        <asp:CheckBox ID="chkAnsYes" runat="server" CssClass="mycheckBig"   />
                                                        <label class='inline' for="chkAnsYes" runat="server" id="lblAnsYes" style="width: 100%; font-size: large;color:white;">ใช่</label>
                                                    </div>
                                                    <div class="check-line">
                                                        <asp:CheckBox ID="chkAnsNo" runat="server" CssClass="mycheckBig"  />
                                                        <label class='inline' for="chkAnsNo" runat="server" id="lblAnsNo" style="width: 100%; font-size: large;color:white;">ไม่ใช่</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlQuestionWriting" runat="server" CssClass="tab-pane active" Visible="false">
                                            <div class="row">
                                                <div class="span2"></div>
                                                <div class="span8">
                                                    <asp:TextBox ID="txtAnsQuestion4" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                </div>
                                                <div class="span2"></div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlQuestionPicture" runat="server" CssClass="tab-pane active" Visible="false">
                                            <div class="row">
                                                <div class="span2"></div>
                                                <div class="span3">
                                                    <img src="Assets/PC/noimagefound.jpg" style="width:100%;height:200px;margin-bottom:20px" runat="server" id="img2" />
                                                </div>
                                                <div class="span7">
                                                    <table >
                                                        <asp:Repeater ID="rptQuestionPicture" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="width:20%">
                                                                        <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                    </td>
                                                                    <td style="width:5px"></td>
                                                                    <td style="width:70%">
                                                                        <asp:Label ID="lblQuestion" runat="server" style="font-size: large;color:white;" ></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <div style="display: none">
                                            <asp:TextBox ID="txtQuestion_no" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtQuestion_Count" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtCourse_id" runat="server"></asp:TextBox>
                                             <asp:TextBox ID="txtQuestion_Dialog" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtQuestion_Choice" runat="server"></asp:TextBox>
                                            <label class='inline' for="lblA2" runat="server" id="lblA2"></label>
                                            <label class='inline' for="lblB2" runat="server" id="lblB2"></label>
                                            <label class='inline' for="lblC2" runat="server" id="lblC2"></label>
                                            <label class='inline' for="lblD2" runat="server" id="lblD2"></label>
                                            <asp:Label ID="lblImage2" runat="server"></asp:Label>
                                            <asp:Label ID="lblQNumber2" runat="server"></asp:Label>
                                            <asp:Label ID="lblQDetail2" runat="server"></asp:Label>

                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row-fluid">
                                        <div class="span5"></div>
                                        <div class="span2 text-center">
                                            <p>
                                                <button class="btn-block btn btn-large btn-green" runat="server" id="btnAns">ตอบ</button>
                                                <button class="btn-block btn btn-large" runat="server" id="btnSummary" visible="false">ผลสอบ</button>
                                                <button class="btn-block btn btn-large" runat="server" id="btnTest" visible="false">ผลสอบ test</button>
                                            </p>
                                        </div>
                                        <div class="span5"></div>
                                    </div>

                            </div>

                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                        <%--    <div class="span1"></div>--%>
                    </div>
                    <div id="modal-1" class="modal hide" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-header" style="background: #019b79">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h2 id="myModalLabel" class="text-center"><font color="#fff">ยินดีด้วย</font></h2>
                        </div>
                        <div class="modal-body text-center">
                            <h2>คุณตอบถูก</h2>
                            <button class="btn btn-primary" data-dismiss="modal">ต่อไป</button>
                        </div>
                    </div>
                </div>
            </div>





            <div class="container-fluid" id="dialogfalse" style="display: none">

                <div class="box-content">
                    <div class="row-fluid">
                        <div class="span1"></div>
                        <div class="span10">



                            <div class="box-content nopadding">
                                <div class="tab-content padding tab-content-inline tab-content-bottom">
                                    <div class="tab-pane active text-center" id="first11">
                                        <h2><font color="red">เสียใจด้วยคุณยังสอบไม่ผ่านวิชานี้</font></h2>
                                        <br />
                                        <h3>คุณตอบถูก<font color="red"> <asp:Label runat="server" ID="lblQ1T"></asp:Label></font> ข้อ จากทั้งหมด<font color="red"> <asp:Label runat="server" ID="lblQ1A"></asp:Label></font> ข้อ</h3>
                                        <h3>ไม่ผ่านเกณฑ์คะแนน
                                    <asp:Label runat="server" ID="lblQ1P"></asp:Label></h3>

                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="span1"></div>
                    </div>

                </div>

            </div>

            <div class="container-fluid" id="dialogtrue" style="display: none">


                <div class="box-content">
                    <div class="row-fluid">
                        <div class="span1"></div>
                        <div class="span10">



                            <div class="box-content nopadding">
                                <div class="tab-content padding tab-content-inline tab-content-bottom">
                                    <div class="tab-pane active text-center" id="first11">
                                        <h1><font color="green">ยินดีด้วยคุณสอบผ่านวิชานี้แล้ว</font></h1>
                                        <br />
                                        <h2>คุณตอบถูก<font color="green"> <asp:Label runat="server" ID="lblQ2T"></asp:Label></font> ข้อ จากทั้งหมด<font color="green"> <asp:Label runat="server" ID="lblQ2A"></asp:Label></font> ข้อ</h2>
                                        <h2>ผ่านเกณฑ์คะแนน 
                                    <asp:Label runat="server" ID="lblQ2P"></asp:Label></h2>
                                        <div class="clearfix"></div>


                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="span1"></div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

