<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="frmDisplayCenter.aspx.vb" Inherits="TESCO_TMS3.frmDisplayCenter" %>

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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server"> 

</asp:Content>
