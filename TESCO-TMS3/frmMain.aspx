<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmMain.aspx.vb" Inherits="TESCO_TMS3.frmMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <title>FLAT - Tiles</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid nav-hidden" id="content">
		<font color="#019b79"><h1 class="text-center">คุณกำลังเข้าสู่บท</h1></font>
        <font color="#fff"><h3 class="text-center">แบบทดสอบ</h3></font> 
		<div id="main" style="background:#29363f">
			<div class="container-fluid"><br /><br /><br />
				<div class="box-content">
				      <div class="row-fluid">
						<div class="span2"></div>
						<div class="span3">
                          <div class="box-content">
                            <font color="#fff"><h4 class="text-center">เลือกฟอร์แมท (Format) ของคุณ</h4></font>
                            <a href="#modal-1" data-toggle="modal"><p> <button class="btn-block btn btn-larges">Block level</button></p></a>
                            <p> <button class="btn-block btn btn-larges">Block level</button></p>
                            <p> <button class="btn-block btn btn-larges">Block level</button></p>
                            <p> <button class="btn-block btn btn-larges">Block level</button></p>
                            <p> <button class="btn-block btn btn-larges">Block level</button></p>
                          </div>
						</div>
                        
						<div class="span1"></div>
						<div class="span3">
                            <div class="box box-bordered">
								<font color="#019b79"><h4 class="text-center">ความเคลื่อนไหววันนี้....</h4></font>
                               <div style="width: 350px; height: 340px; overflow-y: scroll; scrollbar-arrow-color:blue; scrollbar-face-color: #e7e7e7; scrollbar-3dlight-color: #a0a0a0; scrollbar-darkshadow-color:#888888">
								<div class="box-content" style="background:#29363f">
                                    <font color="#fff">
									Lorem ipsum velit dolor veniam occaecat do eiusmod velit cillum sit. Lorem ipsum laborum sed Duis in in dolor in exercitation irure. Lorem ipsum ad proident ut in mollit id ullamco Ut. Lorem ipsum magna eiusmod in anim deserunt adipisicing. Lorem ipsum ex dolore sint consectetur eu non mollit Ut dolore aliquip anim. Lorem ipsum labore ad Ut Duis Excepteur consequat non. Lorem ipsum ut quis ea dolore esse Duis cillum amet eu officia nostrud. Lorem ipsum dolor aliquip cillum ea ut id pariatur ad aute mollit in qui. Lorem ipsum magna ut consectetur incididunt sed consectetur ut ullamco dolore Excepteur deserunt. Lorem ipsum mollit ad Excepteur dolore non dolor occaecat reprehenderit eiusmod mollit ut Excepteur dolore dolor ex. Lorem ipsum nisi exercitation cillum velit tempor aliqua nisi non. Lorem ipsum enim magna adipisicing do adipisicing aliquip ex pariatur dolore proident fugiat mollit dolor. 
								</font></div></div>
							</div>
						</div>
						<div class="span3"></div>
				      </div>
                    <div class="clearfix"></div><br />
                  <div class="row-fluid">
                      <div class="span5"></div>
                      <div class="span2 text-center">
							<button class="btn-block btn btn-large btn-green">หลักสูตร</button>
                      </div>
                      <div class="span5"></div>
                  </div>
               </div>
			</div>
		</div>
    </div>
</asp:Content>
