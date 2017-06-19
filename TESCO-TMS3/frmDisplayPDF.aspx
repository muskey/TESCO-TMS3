<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmDisplayPDF.aspx.vb" Inherits="TESCO_TMS3.frmDisplayPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <style media="screen" type="text/css">
        /**
 * Demo Styles
 */

        html {
            height: 100%;
            box-sizing: border-box;
        }

        *,
        *:before,
        *:after {
            box-sizing: inherit;
        }

        body {
            position: relative;
            margin: 0;
            padding-bottom: 6rem;
            min-height: 100%;
            font-family: "Helvetica Neue", Arial, sans-serif;
        }

        .demo {
            margin: 0 auto;
            padding-top: 64px;
            max-width: 640px;
            width: 94%;
        }

            .demo h1 {
                margin-top: 0;
            }

        /**
 * Footer Styles
 */

        .footer {
            position: absolute;
            right: 0;
            bottom: 0;
            left: 0;
            background-color: #2B4354;
            text-align: center;
        }

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
    <script>

        function fselect(id, file_url, rowindex) {

            var url = '';
            if (file_url.indexOf(".png") != -1 || file_url.indexOf(".jpg") != -1) {
                url = 'frmDisplayImage.aspx?id=' + id;
            } else if (file_url.indexOf(".pdf") != -1) {
                url = 'frmDisplayPDF.aspx?id=' + id;
            } else if (file_url.indexOf(".mp4") != -1) {
                url = 'frmDisplayVDO.aspx?id=' + id;
            } else if (file_url.indexOf(".html") != -1) {
                url = 'frmDisplayHTML.aspx?id=' + id;
            }

            CreateTransLog('<%=UserData.LoginHistoryID %>', 'เลือกเอกสารจากสารบัญ URL=' + file_url);

            window.location = url;
        }

        function loadIframe(url) {

            var $iframe = $('#myIframe');
            if ($iframe.length) {
                $iframe.attr('src', url);
                return false;
            }
            return true;
        }

        function showcontent() {
            $("#myBody").hide();
            $("#myContent").show();


            document.getElementById('<%=btnCloseContent.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=btnBack.ClientID %>').style.visibility = 'hidden';
            document.getElementById('<%=btnNext.ClientID %>').style.visibility = 'hidden';
            document.getElementById('<%=btnHome.ClientID %>').style.visibility = 'hidden';
            document.getElementById('<%=btnContent.ClientID %>').style.visibility = 'hidden';
        }

        function hidecontent() {
            $("#myBody").show();
            $("#myContent").hide();

            document.getElementById('<%=btnCloseContent.ClientID %>').style.visibility = 'hidden';
            document.getElementById('<%=btnBack.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=btnNext.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=btnHome.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=btnContent.ClientID %>').style.visibility = 'visible';
        }

        function ShowPopup() {
            CreateTransLog('<%=UserData.LoginHistoryID %>', 'คลิกปุ่มสารบัญ');

            $(function () {
                // $("#dialog").html(message);
                var wWidth = $(window).width();
                var dWidth = wWidth * 0.8;
                var wHeight = $(window).height();
                var dHeight = wHeight * 0.8;

                $("#myContent").dialog({
                    title: name,
                    width: dWidth,
                    height: dHeight,
                    modal: true
                });
            });
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div id="myContent" style="display: none;">

        <div style="width: 100%">
            <asp:Label ID="lblContent" runat="server" Text="sssssssssssssssssssss"></asp:Label>
        </div>



    </div>
    <div id="myBody">
        <iframe name="myIframe" id="myIframe" runat="server" style="height: 87vh; width: 100%" align="center" frameborder="0" allowfullscreen="true"></iframe>

    </div>



    <%--    <div class="footer">This footer will always be positioned at the bottom of the page, but <strong>not fixed</strong>.</div>--%>
    <div class="footer">
        <div class="row">
            <div class="span4"></div>
            <div class="span4 text-center" >
                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Assets/PC/btnPreviousDoc.png" Height="40px" Width="100px" />
                <asp:ImageButton ID="btnPDFBack" runat="server" ImageUrl="~/Assets/PC/btnPreviousDidable.png" Height="40px" Width="40px" />
                <asp:DropDownList CssClass="form-control" ID="ddlPage" runat="server" Width="50px" Height="23px" AutoPostBack="true"  Font-Size="Smaller" ></asp:DropDownList><asp:Label ID="lblPDFPage" runat="server" Text="1/99"></asp:Label>

                <asp:ImageButton ID="btnPDFNext" runat="server" ImageUrl="~/Assets/PC/btnNextDisable.png" Height="40px" Width="40px" />
                <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Assets/PC/btnNextDoc.png" Height="40px" Width="100px" />

                <asp:TextBox ID="txtPre" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtCurrent" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtNext" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtMax" runat="server" Visible="false"></asp:TextBox>
            </div>
            <div class="span4 text-right">
                <asp:ImageButton ID="btnHome" runat="server" ImageUrl="~/Assets/PC/btnCloseDoc.png" Height="40px" Width="100px" />
                <asp:ImageButton ID="btnContent" runat="server" ImageUrl="~/Assets/PC/index_icon.png" Height="40px" Width="100px" />
                <asp:Button ID="btnCloseContent" runat="server" CssClass="btn btn-Normal btn-green" Text="ปิด" Width="" Visible="false" />
            </div>

        </div>


    </div>
</asp:Content>
