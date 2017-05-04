<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectFormat.aspx.vb" Inherits="TESCO_TMS3.frmSelectFormat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <title></title>
    <script>

        function fselect(id,title) {
         

            $.each($("button"), function (key, value) {
                //alert(key + ":" + value)
                $(value).css("background-color", "");
                $(value).css("color", "");
            });

            var control = $("#" + id);
            $(control).css("background-color", "#2B4354");
            $(control).css("color", "#FFFFFF");

            //$("#txtFormatID").val = id;
           
            $('#<%=txtFormatID.ClientID %>').val(id);
            $('#<%=txtFormatTitle.ClientID %>').val(title);
          
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="container-fluid nav-hidden" id="content">
    <div class="text-center">
        <img src="Assets/PC/bgSelectFormatHeader.png" height="250" />
    </div>
		<div id="main" style="background:#29363f">
			<div class="container-fluid"><br />
				<div class="box-content">
				      <div class="row-fluid">
						<div class="span2"></div>
						<div class="span3">
                          <div class="box-content" style="text-align:center" >
                            <span class="text-center" style="color:#fff;font-size:26px;">เลือกฟอร์แมท (Format) ของคุณ</span>
                            <a href="#modal-1" data-toggle="modal"></a>
                                <asp:Label runat="server" ID="lblBotton"></asp:Label>
                          </div>
						</div>
                        
						<div class="span1"></div>
						<div class="span3">
                            <div class="box" style="text-align:center">
                                <span class="text-center" style="color:#019b79;font-size:26px;" >ความเคลื่อนไหววันนี้...</span>
                                <div style="width: 350px; height: 340px; overflow-y: scroll; scrollbar-arrow-color:blue; scrollbar-face-color: #e7e7e7; scrollbar-3dlight-color: #a0a0a0; scrollbar-darkshadow-color:#888888">
								    <div class="box-content"  style="background:#29363f;text-align:left;">
                                        <asp:Label runat="server" ID="lblNEWS"></asp:Label>
								    </div>
                                </div>
							</div>
						</div>
						<div class="span3"></div>
				      </div>
                    <div class="clearfix"></div><br />
                  <div class="row-fluid">
                      <div class="span5"></div>
                      <div class="span2 text-center">
						
                          <asp:Button ID="btnFormat" runat="server" CssClass="btn-block btn btn-large btn-green" Text="ถัดไป"  />
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
