<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPageIFRAME.Master" CodeBehind="frmDisplayPDF.aspx.vb" Inherits="TESCO_TMS3.frmDisplayPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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

        .i-am-centered { margin: auto; max-width: 100%;}
    </style>
<%--    <script src="Assets/js/plugins/responsiveiframe/responsiveiframe.js"></script>
    <script>
    var ri = responsiveIframe();
        ri.allowResponsiveEmbedding();
</script>
     <script>
  ;(function($){          
      $(function(){
          $('#<%=myIframe.ClientID %>').responsiveIframe({ xdomain: '*' });       
      });        
  })(jQuery);
</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container-fluid">
             <div class="row">
            <div class="col-md-12">
    <iframe name="myIframe" id="myIframe" runat="server" style="height: 85vh; width: 100%" align="center" frameborder="0" allowfullscreen="true"></iframe>

            </div>

             </div>
              <div class="row">
               <div class="col-md-12 align text-center">            
              <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary btn-xs" Text="เอกสารก่อนหน้า" />
                    <asp:TextBox ID="txtPre" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtCurrent" runat="server"  Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtNext" runat="server"  Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtMax" runat="server"  Visible="false"></asp:TextBox>
               <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary btn-xs" Text="เอกสารก่อนถัดไป" />
               </div>
         </div>
        </div>
       <div id="footer" style="display:none" >
        <table style="width: 100%">
            <tr>
                <td style="width: 100%">
                               

                 
                 </td>

               

            </tr>
        </table>
    </div>

</asp:Content>
