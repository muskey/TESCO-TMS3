<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectQuestionTest.aspx.vb" Inherits="TESCO_TMS3.frmSelectQuestionTest" %>

<%@ Register Src="~/UCQuestion/UCTestABCD.ascx" TagPrefix="uc1" TagName="UCabcd" %>
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
    <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
    <div class="breadcrumbs" style="background: #29363f">
        <asp:Label runat="server" ID="lblTestTitle" Font-Size="Larger"></asp:Label>
    </div>
    <asp:Panel ID="pnlTestQuestion" runat="server" CssClass="container-fluid" Visible="true">
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
        </div>
    
        <asp:TextBox ID="txtQuestion_no" runat="server" Visible="false"></asp:TextBox>
        <asp:Label ID="lblIsShowAnswer" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblCourseID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTargetPercent" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblQuestionQty" runat="server" Visible="false"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlSummaryFalse" runat="server" CssClass="container-fluid" Visible="false">
        <br />
        <br />
        <div class="box-content">
            <div class="row-fluid">
                <div class="span1"></div>
                <div class="span10">
                    <div class="box-content nopadding" style="background-color:#e3dddd">
                        <div class="tab-content padding tab-content-inline tab-content-bottom">
                            <div class="tab-pane active text-center">
                                <h1 style="color:red">เสียใจด้วยคุณยังสอบไม่ผ่านวิชานี้</h1>
                                <br />
                                <h3>
                                    คุณทำได้ <asp:Label runat="server" ID="lblCorrectScoreFalse" ForeColor="Red" ></asp:Label> คะแนน 
                                    จากทั้งหมด <asp:Label runat="server" ID="lblTotalScoreFalse" ForeColor="Red" ></asp:Label> คะแนน
                                </h3>
                                <h3>จำนวนคำถามทั้งหมด <asp:Label runat="server" ID="lblTotalQuestionFalse" ForeColor="red" ></asp:Label> ข้อ</h3>
                                <h3>ไม่ผ่านเกณฑ์คะแนน <asp:Label runat="server" ID="lblResultPercentFalse" ></asp:Label></h3>
                            </div>
                            <br />
                            <br />
                            <br />
                            <div class="row">
                                <div class="span12 text-center">
                                    <asp:LinkButton ID="btnBackFalse" runat="server" CssClass="btn btn-green" >
                                        <span>กลับหน้าแรก</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span1"></div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlSummaryTrue" runat="server" CssClass="container-fluid" Visible="false">
        <br />
        <br />
        <div class="box-content">
            <div class="row-fluid">
                <div class="span1"></div>
                <div class="span10">
                    <div class="box-content nopadding" style="background-color:#e3dddd">
                        <div class="tab-content padding tab-content-inline tab-content-bottom">
                            <div class="tab-pane active text-center" id="first11">
                                <h1 style="color:green">ยินดีด้วยคุณสอบผ่านวิชานี้แล้ว</h1>
                                <br />
                                <h3>
                                    คุณทำได้ <asp:Label runat="server" ID="lblCorrectScoreTrue" ForeColor="Green"></asp:Label> คะแนน 
                                    จากทั้งหมด <asp:Label runat="server" ID="lblTotalScoreTrue" ForeColor="Green" Text="5"></asp:Label> คะแนน
                                </h3>
                                <h3>จำนวนคำถามทั้งหมด <asp:Label runat="server" ID="lblTotalQuestionTrue" ForeColor="Green" ></asp:Label> ข้อ</h3>
                                <h3>ผ่านเกณฑ์คะแนน <asp:Label runat="server" ID="lblResultPercentTrue" Text="100%"></asp:Label></h3>
                            </div>
                            <div class="row-fluid">
                                <div class="span12 text-center">
                                    <asp:LinkButton ID="btnBackTrue" runat="server" CssClass="btn btn-green" >
                                        <span>กลับหน้าแรก</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span1"></div>
            </div>
            
        </div>
    </asp:Panel>

</asp:Content>
