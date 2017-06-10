<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>Execute Outfit Updater</h1>
    
    <%
        if (TempData["Status"] != null) Response.Write("<h2>Feed importation started...</h2>");
        Html.BeginForm();
        Response.Write(Html.SubmitButton("Process Feeds"));
        Html.EndForm(); 
    %>

</asp:Content>
