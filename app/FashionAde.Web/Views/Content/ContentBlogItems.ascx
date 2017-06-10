<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Core.FlavorSelection.BrandSet>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<% foreach (FashionAde.Core.ContentManagement.ContentPublished content in (IList<FashionAde.Core.ContentManagement.ContentPublished>)ViewData["contents"])
   { %>
    <div class="styleAlertContainer">
        <div class="styleAlertTextContainer">
            <a href="<%= content.UserFriendlyTitle %>" class="title"><%= content.Title %></a>
            <span class="text"><%= content.PromotionalText %></span>
        </div>
    </div>        
<% } %>