<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UCTestABCD.ascx.vb" Inherits="TESCO_TMS3.UCTestABCD" %>

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
    <asp:Panel ID="pnlQuestionABCD" runat="server" CssClass="tab-pane active" Visible="true">
        <div class="row">
            <div class="span2"></div>
            <div class="span3">
                <img src="Assets/PC/noimagefound.jpg" height="200" width="200" runat="server" id="imgQ" />
            </div>
            <div class="span7">
                <div class="check-line">
                    <asp:CheckBox ID="ckbA" runat="server" CssClass="mycheckBig" />
                    <label class='inline' for="ckbA" runat="server" id="lblA" style="width: 100%; font-size: large; color: white;"></label>
                </div>
                <div class="check-line">
                    <asp:CheckBox ID="ckbB" runat="server" CssClass="mycheckBig" />
                    <label class='inline' for="ckbB" runat="server" id="lblB" style="width: 100%; font-size: large; color: white;"></label>
                </div>
                <div class="check-line">
                    <asp:CheckBox ID="ckbC" runat="server" CssClass="mycheckBig" />
                    <label class='inline' for="ckbC" runat="server" id="lblC" style="width: 100%; font-size: large; color: white;"></label>
                </div>
                <div class="check-line">
                    <asp:CheckBox ID="ckbD" runat="server" CssClass="mycheckBig" />
                    <label class='inline' for="ckbD" runat="server" id="lblD" style="width: 100%; font-size: large; color: white;"></label>
                </div>
            </div>
        </div>

    </asp:Panel>
    <div style="display:none">
        <asp:TextBox ID="txtTestID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtQuestionID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtQuestion_no" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtCorrectChoice" runat="server"  ></asp:TextBox>
        <asp:TextBox ID="txtCorrectAnswer" runat="server"  ></asp:TextBox>
        <asp:TextBox ID="txtShowAnswer" runat="server"  ></asp:TextBox>
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

<asp:Panel ID="pnlAnsResult" runat="server" CssClass="modal hide in" tabindex="-1" aria-hidden="false" style="display:block;" Visible="false" >
    <div class="modal-dialog modal-center">
        <div class="modal-content">
	        <div class="modal-header" style="background: #019b79" id="divHeader" runat="server">
                <asp:LinkButton ID="btnCloseDialog" runat="server" CssClass="close" data-dismiss="modal" aria-hidden="true">
                    x
                </asp:LinkButton>
		        <h3 class="text-center"><asp:Label ID="lblDialogHead" runat="server" ForeColor="White"></asp:Label></h3>
	        </div>
	        <div class="modal-body text-center">
                <asp:Literal ID="litAnsDetail" runat="server"></asp:Literal>
                <asp:LinkButton ID="btnNext" runat="server" CssClass="btn btn-green" data-dismiss="modal">
                    ต่อไป
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Panel>

<script>
    function onConfirmCheck(choice) {
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

    
</script>