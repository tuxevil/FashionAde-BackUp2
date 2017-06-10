<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<% using (Html.BeginForm("Import", "OUImportation"))
   {  %>
    <textarea id="bulkcsvlist" name="bulkcsvlist" rows="20" cols="105"></textarea>
    <input type="submit" value="Upload File" />
<%} %>
</asp:Content> 
