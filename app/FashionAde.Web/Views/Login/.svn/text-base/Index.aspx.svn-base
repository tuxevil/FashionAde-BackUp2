<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.Web.Controllers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Html.RenderPartial("LogIn"); %>
    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">
    $(document).ready(function() {
        $("#userName").focus();
        
        <% if (ViewData["Errors"] != null)
           { %>
            showMessage('<%= ViewData["Errors"] %>');
        <% } %>        
        
        <% if (Convert.ToBoolean(ViewData["validatedUser"])) { %>
            appendMessage($("#errorMessage"), "Your code has been successfully validated! Please sign in.", "success");
        <% } %>
    });
</script>
</asp:Content>