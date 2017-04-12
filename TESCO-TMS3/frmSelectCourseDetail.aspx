<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectCourseDetail.aspx.vb" Inherits="TESCO_TMS3.frmSelectCourseDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
      <div class="container-fluid nav-hidden" id="content">
		   
		<div id="main" style="background:#29363f">
            <div class="breadcrumbs" style="background:#29363f">
                 <asp:Label runat="server" ID="lblTitle"></asp:Label>
	<%--					<ul>
							<li><i class="icon icon-home"></i>
								<a href="more-login.html"><font color="#019b79">EXTRA</font></a>
							</li>
							<li>
								<a href="components-messages.html"><font color="#019b79">|&nbsp; &nbsp; HYPER</font></a>
							</li>
							<li>
								<a href="components-elements.html"><font color="#019b79">|&nbsp; &nbsp; DEPT</font></a>
							</li>
						</ul>--%>
						
					</div>
			<div class="container-fluid"><br /><br />
				<div class="box-content">
				     <div class="row-fluid">
                                <asp:Label runat="server" ID="lblMain" ></asp:Label>					   
					    </div>           
				</div>  
			</div>
                              <div class="row-fluid">
                      <div class="span5"></div>
                      <div class="span2 text-center">
						
                            <asp:Button ID="btnRegister" runat="server" CssClass="btn-block btn btn-large btn-green" Text="ลงทะเบียน" />
                          <asp:TextBox ID="txtCoursetID" runat="server" Visible="false" ></asp:TextBox>
                          <asp:TextBox ID="txtCourseTitle" runat="server"  Visible="false" >></asp:TextBox>
                      </div>
                      <div class="span5"></div>
                  </div>
		</div></div>
</asp:Content>
