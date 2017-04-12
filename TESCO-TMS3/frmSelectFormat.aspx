<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectFormat.aspx.vb" Inherits="TESCO_TMS3.frmSelectFormat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <title>FLAT - Tiles</title>
    <script>

        function fselect(id,title) {
         

            $.each($("button"), function (key, value) {
                //alert(key + ":" + value)
                $(value).css("background-color", "");
            });

            var control = $("#" + id);
            $(control).css("background-color", "aquamarine");

            //$("#txtFormatID").val = id;
           
            $('#<%=txtFormatID.ClientID %>').val(id);
            $('#<%=txtFormatTitle.ClientID %>').val(title);
          
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="container-fluid nav-hidden" id="content">
		<font color="#019b79"><h1 class="text-center">ยินดีต้อนรับเข้าสู่</h1></font>
        <font color="#fff"><h3 class="text-center">ศูนย์การเรียนรู้ เทศโก้โลตัส</h3></font> 
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
<%--                            <p> <button class="btn-block btn btn-larges" Name="btnformat" id="1" onclick="fselect(1);return false;" style="background-color:aquamarine">Block level</button></p>
                            <p> <button class="btn-block btn btn-larges" Name="btnformat" id="2" onclick="fselect(2);return false;" style="background-color:aquamarine">Block level</button></p>
                            <p> <button class="btn-block btn btn-larges">Block level</button></p>
                            <p> <button class="btn-block btn btn-larges">Block level</button></p>
                            <p> <button class="btn-block btn btn-larges">Block level</button></p>--%>
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
						
                            <asp:Button ID="btnFormat" runat="server" CssClass="btn-block btn btn-large btn-green" Text="หลักสูตร" />
                          <asp:TextBox ID="txtFormatID" runat="server"  ></asp:TextBox>
                          <asp:TextBox ID="txtFormatTitle" runat="server"></asp:TextBox>
                      </div>
                      <div class="span5"></div>
                  </div>
               </div>
			</div>
		</div>
    </div>

    
</asp:Content>
