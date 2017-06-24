<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmFormTest.aspx.vb" Inherits="TESCO_TMS3.frmFormTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <title></title>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
    <div class="breadcrumbs" style="background: #29363f">
        <asp:Label runat="server" ID="lblTestTitle" Font-Size="Larger"></asp:Label>
    </div>
    <asp:Panel ID="pnlTestQuestion" runat="server" CssClass="container-fluid" Visible="true">
        <div class="box-content">
            <div class="row-fluid">
                <div class="span12">
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
                                 

                                 <ul class="gallery">
                                     <li>
										<a href="#">
                                            <img id="img1" runat="server" src="https://tescolotuslc.com/learningcenterdev/storage/question/2017/05/05/09/134e/20170505-091419.meat q24.png" alt=""  />
										</a>
										<div class="extras">
											<div class="extras-inner">
												<a id="likImage1" runat="server" href="https://tescolotuslc.com/learningcenterdev/storage/question/2017/05/05/09/134e/20170505-091419.meat q24.png" class='colorbox-image' rel="group-1">
                                                    <i class="icon-search"></i>
												</a>
											</div>
										</div>
									</li>
                                 </ul>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
    </asp:Panel>
</asp:Content>
