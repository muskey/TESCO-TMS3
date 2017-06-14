<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UCPicture.ascx.vb" Inherits="TESCO_TMS3.UCPicture" %>
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
 <asp:Panel ID="pnlQuestionPicture" runat="server" CssClass="tab-pane active" Visible="true">
     <div class="row">
         <div class="span2"></div>
         <div class="span3">
             <img src="Assets/PC/noimagefound.jpg" style="width: 100%; height: 200px; margin-bottom: 20px" runat="server" id="img2" />
         </div>
         <div class="span7">
             <table>
                 <asp:Repeater ID="rptQuestionPicture" runat="server">
                     <ItemTemplate>
                         <tr>
                             <td style="width: 10%; text-align: center;">
                                 <asp:Label ID="No" runat="server" Style="font-size: large; color: white;"></asp:Label>
                             </td>
                             <td style="width: 20%; text-align: left;">
                                 <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                             </td>
                             <td style="width: 5px"></td>
                             <td style="width: 70%">
                                 <asp:Label ID="abc" runat="server" Style="font-size: large; color: white;"></asp:Label>
                                 <asp:Label ID="lblQuestion" runat="server" Style="font-size: large; color: white;"></asp:Label>
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