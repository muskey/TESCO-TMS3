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
           
            $('#<%=txUsertFormatID.ClientID %>').val(id);
            $('#<%=txtFormatTitle.ClientID %>').val(title);
        }

        function fselectDDL(ddl) {
            $('#<%=txUsertFormatID.ClientID %>').val(ddl.value);
            $('#<%=txtFormatTitle.ClientID %>').val(ddl.options[ddl.selectedIndex].text);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="container-fluid nav-hidden" id="content">
    <div class="text-center">
        <img src="Assets/PC/bgSelectFormatHeader.png" style="height:60px;" />
    </div>
		<div id="main" style="background:#29363f">
			<div class="container-fluid"><br />
				<div class="box-content">
                    <div class="row-fluid">
                        <div class="row">
                            <div class="span5">
                                <div class="span4"></div>
                                <div class="span6">
                                    <div class="box-content" style="text-align: center">
                                        <span class="text-center" style="color: #fff; font-size: 26px;">เลือกฟอร์แมท (Format) ของคุณ</span>
                                        <asp:Label runat="server" ID="lblDropdownListFormat"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="span6" >
                                <div class="span2"></div>
                                <div class="span8">
                                    <div class="box" style="text-align: center">
                                        <span class="text-center" style="color: #019b79; font-size: 26px;">ความเคลื่อนไหววันนี้...</span>
                                        <asp:Label runat="server" ID="lblNEWS" ></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="clearfix"></div><br />
                  <div class="row-fluid">
                      <div class="span4"></div>
                      <div class="span3 text-center">
						
                          <asp:Button ID="btnFormat" runat="server" CssClass="btn-block btn btn-large btn-green" Text="ถัดไป"  />
                          <asp:TextBox ID="txUsertFormatID" runat="server"  ></asp:TextBox>
                          <asp:TextBox ID="txtFormatTitle" runat="server"></asp:TextBox>
                      </div>
                      <div class="span3"></div>
                  </div>
               </div>
			</div>
		</div>
    </div>
</asp:Content>
