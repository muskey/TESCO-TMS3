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
             <div id="links" class="links" runat="server">
                    
            </div>
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
         <div class="span7">
             <table>
                 <asp:Repeater ID="rptQuestionPicture" runat="server">
                     <ItemTemplate>
                         <tr>
                             <td style="width: 15%; text-align: right;">
                                 <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control" Width="60%"></asp:TextBox>
                             </td>
                             <td style="width: 1px"></td>
                             <td style="width: 80%">
                                 <asp:Label ID="lblAbc" runat="server" Style="font-size: large; color: white;"></asp:Label>
                                 <asp:Label ID="lblQuestion" runat="server" Style="font-size: large; color: white;"></asp:Label>
                                 <asp:Label ID="lblCorrectAnswer" runat="server" Visible="false"></asp:Label>
                             </td>
                         </tr>
                     </ItemTemplate>
                 </asp:Repeater>
             </table>
         </div>
     </div>
 </asp:Panel>
    <div style="display: none">
        <asp:TextBox ID="txtTestID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtQuestionID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtQuestion_no" runat="server"></asp:TextBox>
        <asp:Label ID="lblImage2" runat="server"></asp:Label>
        <asp:TextBox ID="txtShowAnswer" runat="server"  ></asp:TextBox>
        <asp:TextBox ID="txtIconURL" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtWeight" runat="server" Text="0" ></asp:TextBox>
        <asp:TextBox ID="txtIsRandomAns" runat="server" Text="N" ></asp:TextBox>
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