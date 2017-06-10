<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<FashionAde.Core.ContentManagement.ContentPublished>" %>
<%@ Import Namespace="FashionAde.Core.ContentManagement"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h1><%= Model.Title %></h1>
<p><%= Model.Body %></p>

</asp:Content>