<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.Core.UserCloset"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>We sorry but that invitation doesn't exist</h1>
    
    <h2 style="font-size: 16px;">There have been an error with that invitation, please verified it.</h2>
    
    <span style="color:Black; margin-left: 30px;">Would you like to register?  In just 15 minutes, you can build a virtual closet that will:</span>
    <ul style="margin-left: 50px;">
        <li>Help you shop your closet and get the best value from the investments you’ve already made</li>
        <li>Inspire you to wear more of what you already own</li>
        <li>Help you identify looks you might not have otherwise considered</li>
    </ul>
    <center style=" margin-top: 30px; margin-bottom: 10px;">
        <a href="<%= Url.RouteUrl(new { controller = "FlavorSelect", action= "Index"}) %>" class="divButton" style="display:inline; text-decoration: none;" >Start</a>
    </center>
</asp:Content>