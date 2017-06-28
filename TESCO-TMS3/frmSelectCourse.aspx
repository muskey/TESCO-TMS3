<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectCourse.aspx.vb" Inherits="TESCO_TMS3.frmSelectCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <script>

        function fselect(id, name) {
            CreateTransLog('<%=UserData.LoginHistoryID %>', 'เลือก Course ' + name);
            var url = "frmSelectCourseDetail.aspx?id=" + id + '&title=' + name;
            window.location = url;
            return false;
        }

        function ShowPopup(id, name, desc, UserSessionID) {
            CreateTransLog('<%=UserData.LoginHistoryID %>', 'เลือก Course ' + name);

            onEdit(id);

            $(function () {
                // $("#dialog").html(message);
                var wWidth = $(window).width();
                var dWidth = wWidth * 0.8;
                var wHeight = $(window).height();
                var dHeight = wHeight * 0.8;

                $("#dialog").dialog({
                    title: name,
                    width: dWidth,
                    height: dHeight,
                    buttons: [
                          {
                              text: "ลงทะเบียน",
                              "class": 'saveButtonClass',
                              click: function () {
                                  $("#dialog").dialog("close");

                                  CreateTransLog('<%=UserData.LoginHistoryID %>', 'เริ่มเรียน Course ' + name);
                                  //onDocumentData(id, name, UserSessionID);

                                  var url = "frmDisplayCenter.aspx?id=" + id + '&title=' + name;
                                  window.location = url;
                                  //return false;
                              }
                          }
                    ],
                    modal: true
                });
            });
        };



        function onEdit(id) {

            var param = "{'id':" + JSON.stringify(id) + "}";
            $.ajax({
                type: "POST",
                url: "WebService/WebService.asmx/LoadCoureseDescByID",
                data: param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    var strvalue = data.d;
                    if (strvalue.length > 0) {
                        $("#dialog").html(strvalue);
                    } else {
                        $("#dialog").html('');
                    }
                },
                error: function (data) {
                }
            });

        }

        //function onDocumentData(id, name, UserSessionID) {

        //    var param = "{'id':" + JSON.stringify(id) + ",'UserSessionID':" + JSON.stringify(UserSessionID) +  "}";
        //    $.ajax({
        //        type: "POST",
        //        url: "WebService/WebService.asmx/SetDocumentData",
        //        data: param,
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {

        //            var strvalue = data.d;
        //            if (strvalue == 'True') {
        //                var url = "frmDisplayCenter.aspx?id=" + id + '&title=' + name;
        //                window.location = url;
        //                return false;
        //            }
        //        },
        //        error: function (data) {
        //        }
        //    });

        //}
        function goBack() {
            window.location.href = document.referrer;
            return false;
        }
    </script>
    <style>
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


        .fixedbutton {
            position: fixed;
            bottom: 30%;
            left: 0;
            background-repeat: no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid nav-hidden" id="content">
        
        <div id="main" style="background: #29363f">
            <div  style="background: #29363f">
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
            </div>
            <div class="container-fluid">
 
                <div class="box-content">
                    <div class="row-fluid">
                        <div class="span1">
                       <asp:ImageButton ID="btnBack" runat="server" CssClass="fixedbutton" Visible="true" Height="120px" Width="25px" ImageUrl="~/Assets/PC/btnButtonBack.png" />

                        </div>
                        <div class="span10">

                            <asp:Label runat="server" ID="lblMain"></asp:Label>



                        </div>
                        <div class="span1"></div>
                    </div>

                </div>
            </div>
        </div>

    </div>
    <div id="dialog" style="display: none;">

        <div class="row-fluid">
            <div class="span5"></div>
            <div class="span2 text-center">

                <asp:Button ID="btnRegister" runat="server" CssClass="btn-block btn btn-large btn-green" Text="ลงทะเบียน" />
                <asp:TextBox ID="txtCoursetID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtCourseTitle" runat="server" Visible="false">></asp:TextBox>
            </div>
            <div class="span5"></div>
        </div>
    </div>
</asp:Content>
