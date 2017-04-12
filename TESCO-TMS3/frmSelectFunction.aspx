<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectFunction.aspx.vb" Inherits="TESCO_TMS3.frmSelectFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <script>

            function fselect(id, link, name, color) {
               // alert("frmSelectDepartment.aspx?function_id=" + id + '&function_title=' + name + '&color=' + color);
               // return false;
                if (link != "") {
                    // var control = $("#" + id);
                    var url = "frmSelectDepartment.aspx?id=" + id + '&title=' + name + '&color=' + color;
                    //alert(url);
                    window.location = url;
                    return false;
                } else {
                    return false;
                }

        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    	<div class="container-fluid nav-hidden" id="content">
		   
		<div id="main" style="background:#29363f">
            <div class="breadcrumbs" style="background:#29363f">
                 <asp:Label runat="server" ID="lblTitle"></asp:Label>
						
					</div>
			<div class="container-fluid"><br /><br />
				<div class="box-content">
				     <div class="row-fluid">
                        <div class="span1"></div>
					    <div class="span10">
						   
                                <asp:Label runat="server" ID="lblMain"></asp:Label>
						
						   
					    </div>
                        <div class="span1"></div>
				</div>
                    <div class="clearfix"></div><br />
                  <div class="row-fluid">
                        <div class="span1"></div>
					    <div class="span10">
						     <asp:Label runat="server" ID="lblSub"></asp:Label>
                                                    <asp:TextBox ID="txtFunctionID" runat="server"  Visible="false" ></asp:TextBox>
                          <asp:TextBox ID="txtFunctionTitle" runat="server"  Visible="false"></asp:TextBox>
					    </div>
                        <div class="span1"></div>
				</div>
               </div>
			</div>
		</div></div>
</asp:Content>
