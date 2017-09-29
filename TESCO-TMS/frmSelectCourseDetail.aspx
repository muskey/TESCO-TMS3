<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectCourseDetail.aspx.vb" Inherits="TESCO_TMS.frmSelectCourseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                // $("#dialog").html(message);
                var wWidth = $(window).width();
                var dWidth = wWidth * 0.8;
                var wHeight = $(window).height();
                var dHeight = wHeight * 0.8;
             
                $("#dialog").dialog({
                    title : "xxxxxxx",
                    width: dWidth,
                    height: dHeight,
                    buttons: [
                          {
                              text: "เริ่มเรียน",
                              "class": 'saveButtonClass',
                              click: function () {
                                  $("#dialog").dialog("close");
                              }
                          }
                    ],
                    modal: true
                });
            });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container-fluid nav-hidden" id="content">

        <div id="main" style="background: #29363f">
            <div class="breadcrumbs" style="background: #29363f">
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
            </div>
            <div class="container-fluid">
                <br />
                <br />
                <div class="box-content">
                    <div class="row-fluid">
                        <asp:Label runat="server" ID="lblMain"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5"></div>
                <div class="span2 text-center">

                    <asp:Button ID="btnRegister" runat="server" CssClass="btn-block btn btn-large btn-green" Text="ลงทะเบียน" />
                    <asp:TextBox ID="txtCoursetID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtCourseTitle" runat="server" Visible="false">></asp:TextBox>
                </div>
                <div class="span5"></div>
            </div>
        </div>

    </div>
    <div id="dialog" style="display: none;">
        <div class="row-fluid">
            <div class="span1"> </div>
            <div class="span10">
                <font color="black">

                       <asp:Label runat="server" ID="lblShow" Text="xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx dfdfdfdfer ytytyty dfdfdfdfduyiu  rt rtrtryt ertet teywtereteryeryurtutyiwereretreytert"></asp:Label>
                    
                </font>
            </div>
                <div class="span1"> </div>
        </div>
        	
    </div>
</asp:Content>
