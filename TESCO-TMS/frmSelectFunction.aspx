<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectFunction.aspx.vb" Inherits="TESCO_TMS.frmSelectFunction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>

        function fselect(id, link, name, color) {
            // alert("frmSelectDepartment.aspx?function_id=" + id + '&function_title=' + name + '&color=' + color);
            // return false;
            if (link != "0") {

                InitializeRequest();
                // var control = $("#" + id);
                var url = "frmSelectDepartment.aspx?user_function_id=" + id + '&title=' + name + '&color=' + color;
                //alert(url);

                CreateTransLog('<%=UserData.LoginHistoryID %>', 'เลือก Function ' + name);
                window.location = url;

                return false;
            } else {
                return false;
            }

        }

        function InitializeRequest() {
            // call server side method
            var url = window.location.href;
            var name = getParameterByName('formar_title', url)
            PageMethods.SetBackPath(url, name);
        }

        function getParameterByName(name, url) {
            if (!url) {
                url = window.location.href;
            }
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        function goBack() {
            // window.history.back();
            //alert(document.referrer);


            window.location.href = "frmSelectFormat.aspx";
            return false;

        }
    </script>
    <style>
        .fixedbutton {
            position: fixed;
            bottom: 30%;
            left: 0;
            background-repeat: no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager EnablePageMethods="true" ID="MainSM" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>
    <div class="container-fluid nav-hidden" id="content">
        <asp:ImageButton ID="btnBack" runat="server" CssClass="fixedbutton" Visible="true" Height="120px" Width="25px" ImageUrl="~/Assets/PC/btnButtonBack.png" />

        <div id="main" style="background: #29363f">
            <div class="breadcrumbs" style="background: #29363f">
                <asp:Label runat="server" ID="lblTitle" ></asp:Label>
            </div>
            <div class="container-fluid">

                <div class="box-content">
                    <div class="row-fluid">
                        <div class="span1">
                            <div>
                            </div>
                        </div>
                        <div class="span10">
                            <asp:Label runat="server" ID="lblMain"></asp:Label>
                        </div>
                        <div class="span1">
                        </div>
                    </div>
                    <hr>
                    <div class="clearfix"></div>
                    <div class="row-fluid">
                        <div class="span1">
                        </div>
                        <div class="span10">
                            <asp:Label runat="server" ID="lblSub"></asp:Label>
                        </div>
                        <div class="span1"></div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
