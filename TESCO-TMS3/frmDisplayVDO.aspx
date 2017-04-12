<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPageIFRAME.Master" CodeBehind="frmDisplayVDO.aspx.vb" Inherits="TESCO_TMS3.frmDisplayVDO1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
function goFullscreen(id) {
  var element = document.getElementById(id);       
  if (element.mozRequestFullScreen) {
    element.mozRequestFullScreen();
  } else if (element.webkitRequestFullScreen) {
    element.webkitRequestFullScreen();
  }  
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <video class="video_player" runat="server" id="player"  style="height: 75vh;width:100%"  controls="controls" autoplay="autoplay">
  <source src="" type="video/mp4">

</video>

</asp:Content>
