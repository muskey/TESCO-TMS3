<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UCyesno.ascx.vb" Inherits="TESCO_TMS3.UCyesno" %>
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
   
    <asp:Panel ID="pnlQuestionYesNo" runat="server" CssClass="tab-pane active" Visible="true">
        <div class="row">
            <div class="span2"></div>            
            <div class="span2">
                <img src="Assets/PC/noimagefound.jpg" height="200" width="200" runat="server" id="imgQ" />
            </div>
            <div class="span8">
                <div class="check-line">
                    <asp:CheckBox ID="chkAnsYes" runat="server" CssClass="mycheckBig" />
                    <label class='inline' for="chkAnsYes" runat="server" id="lblAnsYes" style="width: 100%; font-size: large; color: white;">ใช่</label>
                </div>
                <div class="check-line">
                    <asp:CheckBox ID="chkAnsNo" runat="server" CssClass="mycheckBig" />
                    <label class='inline' for="chkAnsNo" runat="server" id="lblAnsNo" style="width: 100%; font-size: large; color: white;">ไม่ใช่</label>
                </div>
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
            <button class="btn-block btn btn-large" runat="server" id="btnSummary" visible="true">ผลสอบ</button>
            <button class="btn-block btn btn-large" runat="server" id="btnTest" visible="true">ผลสอบ test</button>
        </p>
    </div>
    <div class="span5"></div>
</div>