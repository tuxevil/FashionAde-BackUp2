<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1 style="margin-top:15px;">Add Garments</h1>
    <div style="float:left; margin-left: 20px; margin-bottom: 100px;"><a href="<%= Url.RouteUrl(new { controller = "GarmentSelector", action= "AddGarments"}) %>"><img src="/static/img/MyGarments/AddGarments.png" alt="Add existing garments" /></a></div>
    <div style="float:left; margin-left: 30px; margin-bottom: 100px;"><a href="<%= Url.RouteUrl(new { controller = "UploadGarment", action= "Index"}) %>"><img src="/static/img/MyGarments/UploadGarments.png" alt="Upload your own" /></a></div>
</asp:Content>

<asp:Content ID="scriptsContent" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">

<script type="text/javascript">
    $(document).ready(function() {
        $("#lnkAddGarments").css("background-color", "#F08331");
    });
</script>

</asp:Content>