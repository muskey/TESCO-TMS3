<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="TESCO_TMS3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script>

        function fselect(id) {

            var control = $("#" + id);
            $(control).css("background-color", "red");
           // var color = $(control).css("background-color");
           // control.push("background-color" + ": " + "red");
          //  alert(dd);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                            <a href="#modal-1" data-toggle="modal"></a>
                                <asp:Label runat="server" ID="lblBotton"></asp:Label>
                            <p> <button class="btn-block btn btn-larges" id="1" onclick="fselect(1)" style="background-color:aquamarine">Block level</button></p>
                            <p> <button class="btn-block btn btn-larges" id="2" onclick="fselect(2)" style="background-color:aquamarine">Block level</button></p>
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
                                        
                                    <asp:Label runat="server" ID="lblNEWS"></asp:Label>
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
    </form>
</body>
</html>
