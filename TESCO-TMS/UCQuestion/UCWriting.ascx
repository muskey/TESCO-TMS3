<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UCWriting.ascx.vb" Inherits="TESCO_TMS.UCWriting" %>
<div class="tab-content padding tab-content-inline tab-content-bottom">
    <div class="row">
        <div class="span2">
            <button class="btn-block btn btn-large btn-green">
                <asp:Label ID="lblQNumber" runat="server"></asp:Label>
            </button>
        </div>
        <div class="span8">
            <button class="btn-block btn btn-large" style="background: #fff; text-align: left">
                <asp:Label ID="lblQDetail" runat="server"></asp:Label>
            </button>
        </div>
    </div>
    <div class="row">
        <div class="span12"></div>
    </div>
    <asp:Panel ID="pnlQuestionWriting" runat="server" CssClass="tab-pane active" Visible="false">
        <div class="row">
            <div class="span2"></div>
            <div class="span8">
                <center>
                <div id="links" class="links" runat="server"></div>
                    </center>
                <!-- The Gallery as lightbox dialog, should be a child element of the document body -->
                <div id="blueimp-gallery" class="blueimp-gallery">
                    <div class="slides"></div>
                    <h3 class="title"></h3>
                    <a class="prev">‹</a>
                    <a class="next">›</a>
                    <a class="close">Close</a>
                    <a class="play-pause"></a>
                    <ol class="indicator"></ol>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="span12">&nbsp;</div>
        </div>
        <div class="row">
            <div class="span2"></div>
            <div class="span8">
                <asp:TextBox ID="txtAnsQuestion4" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
            <div class="span2"></div>
        </div>
    </asp:Panel>
    <div style="display: none">
        <asp:TextBox ID="txtTestID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtQuestionID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtQuestion_no" runat="server"></asp:TextBox>
        <asp:Label ID="lblImage2" runat="server"></asp:Label>
        <asp:TextBox ID="txtShowAnswer" runat="server"></asp:TextBox>
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