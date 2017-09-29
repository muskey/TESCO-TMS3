<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectCourseCheckStudent.aspx.vb" Inherits="TESCO_TMS.frmSelectCourseCheckStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid nav-hidden" id="content">
        <div id="main" style="background: #29363f">
            <div class="container-fluid">
                <div class="box-content">
                    <div class="row-fluid">
                        <div class="span3"></div>
                        <div class="span6">
                            <div  style="border-color:rgb(1, 155, 121);">
                                <div class="text-center" style="background-color:rgb(1, 155, 121);border-color:rgb(1,155,121);padding:7px 10px 7px 10px;margin-top:20px;">
                                    <asp:LinkButton ID="btnClose" runat="server">
                                        <span class="close">&times;</span>
                                    </asp:LinkButton>
									<h5 style="float:none;">
                                        <asp:Label ID="lblCourseID" runat="server" Visible="false"></asp:Label>
										<asp:Label ID="lblCourseName" runat="server" ForeColor="White" Font-Size="XX-Large" Text="สำหรับแผนกเบเกอรี่ part1" ></asp:Label>
									</h5>
                                    <div class="row">
                                        <div class="span1"></div>
                                        <div class="span4 text-center"><span style="color:white">เพิ่มผู้เรียน ใส่รหัสพนักงาน</span></div>
                                        <div class="span6">
                                            <asp:TextBox id="txtCheckUser" runat="server" style="width:95%;" ></asp:TextBox>
                                        </div>
                                        <div class="span1">
                                            <asp:Button ID="btnAddUser" runat="server" style="display:none" />
                                        </div>
                                    </div>
								</div>
                                <div class="box-content nopadding" style="background-color:white;">
                                    <div class="row">
                                        <div class="span12"></div>
                                    </div>
                                    <div class="row">
                                        <div class="span1"></div>
                                        <div class="span10">
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th style="background-color:rgb(1,155,121);text-align:center;color:white;font-weight:normal;">รหัส</th>
                                                        <th style="background-color:rgb(1,155,121);text-align:center;color:white;font-weight:normal;">ชื่อ - นามสกุล</th>
                                                        <th style="background-color:rgb(1,155,121);width:50px"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptUserList" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="text-align:center">
                                                                    <asp:Label ID="lblUserCode" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblUserID" runat="server" Text="0" Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblUserFullname" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align:center">
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE">
                                                                        <img src="Assets/img/delete-icon.png" width="32" />
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="span1"></div>
                                    </div>
                                   <div class="row">
                                       <div class="span5"></div>
                                       <div class="span2">
                                            <asp:Button ID="btnStart" runat="server" CssClass="btn-block btn btn-large btn-green" Text="เริ่มเรียน"  />
                                       </div>
                                       <div class="span5"></div>
                                   </div>
                                   <div class="row">
                                       <div class="span12"></div>
                                   </div>
                                </div>
                            </div>
                        </div>
                        <div class="span3"></div>
                    </div>
                </div>  
            </div>
        </div>
    </div>


    <script>
        function clickCheckUser(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
