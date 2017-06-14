<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectQuestionTest.aspx.vb" Inherits="TESCO_TMS3.frmSelectQuestionTest" %>

<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="UCabcd" %>
<%@ Register Src="~/UCQuestion/UCMaching.ascx" TagPrefix="uc2" TagName="UCMaching" %>
<%@ Register Src="~/UCQuestion/UCPicture.ascx" TagPrefix="uc3" TagName="UCPicture" %>
<%@ Register Src="~/UCQuestion/UCyesno.ascx" TagPrefix="uc4" TagName="UCyesno" %>
<%@ Register Src="~/UCQuestion/UCWriting.ascx" TagPrefix="uc5" TagName="UCWriting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />

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

    <div class="breadcrumbs" style="background: #29363f">
        <asp:Label runat="server" ID="lblTestTitle" Font-Size="Larger"></asp:Label>
    </div>
    <div class="container-fluid">
        <div class="box-content">
            <div class="row-fluid">
                <%--<div class="span1"></div>--%>
                <div class="span12">
                    <div class="box box-bordered box-color">

                        <uc1:UCabcd runat="server" ID="UCabcd1" Visible="false" />
                        <uc2:UCMaching runat="server" ID="UCMaching" Visible="false" />
                        <uc3:UCPicture runat="server" ID="UCPicture" Visible="false"/>
                        <uc4:UCyesno runat="server" ID="UCyesno" Visible="false"/>
                        <uc5:UCWriting runat="server" ID="UCWriting" Visible="false" />
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
    <asp:TextBox ID="txtQuestion_no" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
