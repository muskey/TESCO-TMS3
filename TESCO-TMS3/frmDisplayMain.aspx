<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmDisplayMain.aspx.vb" Inherits="TESCO_TMS3.frmDisplayMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style media="screen" type="text/css">
        html,
        body {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        #container {
            min-height: 100%;
            position: relative;
        }

        #header {
            background: #ff0;
            padding: 10px;
        }

        #body {
            padding: 10px;
            padding-bottom: 40px; /* Height of the footer */
        }

        #footer {
            position: absolute;
            bottom: 0;
            width: 100%;
            height: 40px; /* Height of the footer */
            background: #808080;
        }
        /* other non-essential CSS */
        #header p,
        #header h1 {
            margin: 0;
            padding: 10px 0 0 10px;
        }

        #footer p {
            margin: 0;
            padding: 10px;
        }
    </style>

    <script>

        function fselect(id, file_url, rowindex) {

            var url = '';
            if (file_url.indexOf(".png") != -1 || file_url.indexOf(".jpg") != -1) {
                url = 'frmDisplayImage.aspx?id=' + id;
            } else if (file_url.indexOf(".pdf") != -1) {
                url = 'frmDisplayPDF.aspx?id=' + id;
            } else if (file_url.indexOf(".mp4") != -1) {
                url = 'frmDisplayVDO.aspx?id=' + id;
            } else if (file_url.indexOf(".html") != -1) {
                url = 'frmDisplayHTML.aspx?id=' + id;
            }

            hidecontent();

            $('#<%=txtPre.ClientID %>').val(parseInt(rowindex) - 1);
            $('#<%=txtCurrent.ClientID %>').val(parseInt(rowindex));
            $('#<%=txtNext.ClientID %>').val(parseInt(rowindex) + 1);
       

            loadIframe(url);


         if (rowindex == 1) {
             document.getElementById('<%=btnBack.ClientID %>').disabled = true;
            } else {
             document.getElementById('<%=btnBack.ClientID %>').disabled = false;
            }
 
            if ($('#<%=txtMax.ClientID %>').val() == rowindex) {
              document.getElementById('<%=btnNext.ClientID %>').disabled = true;
            } else {
              document.getElementById('<%=btnNext.ClientID %>').disabled = false;
            }
         
          
        }

        function loadIframe(url) {
     
            var $iframe = $('#myIframe');
            if ($iframe.length) {
                $iframe.attr('src', url);   
                return false;
            }
            return true;
        }

        function showcontent() {
            $("#myIframe").hide();
            $("#myContent").show();

           
            document.getElementById('<%=btnCloseContent.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=btnBack.ClientID %>').style.visibility = 'hidden';
            document.getElementById('<%=btnNext.ClientID %>').style.visibility = 'hidden';
            document.getElementById('<%=btnHome.ClientID %>').style.visibility = 'hidden';
            document.getElementById('<%=btnContent.ClientID %>').style.visibility = 'hidden';
        }

        function hidecontent() {
            $("#myIframe").show();
            $("#myContent").hide();

            document.getElementById('<%=btnCloseContent.ClientID %>').style.visibility = 'hidden';
            document.getElementById('<%=btnBack.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=btnNext.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=btnHome.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=btnContent.ClientID %>').style.visibility = 'visible';
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server"> 
    <iframe name="myIframe" id="myIframe" style="height: 85vh; width: 100%;" scrolling="no"></iframe>
    <div id="myContent" style="height: 80vh; width: 100%;overflow-y: scroll;display:none">
    <div class="row"  >
        <div class="span2"></div>
        <div class="span8">
               <div class="box-content">
                <asp:Label ID="lblContent" runat="server"></asp:Label>
               </div>          
        </div>
          <div class="span2"></div>
    </div>
    </div>

    <div class="row">

        <div class="col-md-12 align text-center">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-Normal btn-green" Text="เอกสารก่อนหน้า" />
            <asp:Button ID="btnNext" runat="server" CssClass="btn btn-Normal btn-green" Text="เอกสารก่อนถัดไป" />
            <asp:Button ID="btnContent" runat="server" CssClass="btn btn-Normal btn-green" Text="สารบัญ"/>
            
            <asp:Button ID="btnHome" runat="server" CssClass="btn btn-Normal btn-green" Text="หน้าหลัก" />
            <asp:Button ID="btnCloseContent" runat="server" CssClass="btn btn-Normal btn-green" Text="ปิด" Width=""/>
            <%--                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Assets/PC/btnPreviousDoc.png" Height="60px" Width="120px"  />
                        <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Assets/PC/btnNextDoc.png"  Height="60px" Width="120px"  />
                        <asp:ImageButton ID="ImageHome" runat="server" ImageUrl="~/Assets/PC/btnCloseDoc.png" Height="60px" Width="120px"  />
                       <asp:ImageButton ID="ImageContent" runat="server" ImageUrl="~/Assets/PC/index_icon.png"  Height="60px" Width="120px"  />--%>
            <asp:TextBox ID="txtPre" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtCurrent" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtNext" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtMax" runat="server" Visible="false"></asp:TextBox>
        </div>

    </div>

    <div id="rrrrr" style="display: none">
        <!-- Body start -->
        <%--<h3><font color="#019b79"><p>In this demo the footer is pushed to the bottom of the screen in all standards compliant web browsers even when there is only a small amount of content on the page. This with work with IE 6 and IE 5.5 too. Non-standards compliant browsers degrade gracefully by positioning the footer under the content as per normal. Read the <a href="http://matthewjamestaylor.com/blog/keeping-footers-at-the-bottom-of-the-page">full article</a> for all the details.</p></font></h3>--%>
        <!-- Body end -->
    </div>
    <div id="xxxxx" style="display: none">
        <table style="width: 100%">
            <tr>
                <td style="width: 100%"></td>

            </tr>
        </table>
    </div>
</asp:Content>
