<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmSelectDepartment.aspx.vb" Inherits="TESCO_TMS.frmSelectDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>

        function fselect(id, name, count, color) {
            if (count != "0") {

                InitializeRequest();
                //var control = $("#" + id);
                var url = "frmSelectCourse.aspx?id=" + id + '&title=' + name + "&color=" + color;
                //alert(url);

                CreateTransLog('<%=UserData.LoginHistoryID %>', 'เลือก Department ' + name);
                window.location = url;
                return false;
            } else {
                return false;
            }

        }

        function InitializeRequest() {
            // call server side method
            var url = window.location.href;
            var name = getParameterByName('title', url)
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
            window.location.href = document.referrer;
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

        <div id="main" style="background: #29363f">
            <div class="breadcrumbs" style="background: #29363f">
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
            </div>
            <div class="container-fluid">

                <div class="box-content">
                    <div class="row-fluid">
                        <div class="span1">
                            <asp:ImageButton ID="btnBack" runat="server" CssClass="fixedbutton" Visible="true" Height="120px" Width="25px" ImageUrl="~/Assets/PC/btnButtonBack.png" />

                        </div>
                        <div class="span10">

                            <asp:Label runat="server" ID="lblMain"></asp:Label>


                        </div>
                        <div class="span1"></div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
