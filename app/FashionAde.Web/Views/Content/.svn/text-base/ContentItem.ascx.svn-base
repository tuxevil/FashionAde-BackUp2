<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Core.FlavorSelection.BrandSet>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<%  foreach (FashionAde.Core.ContentManagement.ContentPublished content in (List<FashionAde.Core.ContentManagement.ContentPublished>)ViewData["contents"]) { %>
        <div class="styleAlertTextContainer" style="margin-bottom:15px;">
            <span class="title"><%= content.Title  %></span>
            <p><%= content.Body%></p>
            
            <% 
                foreach (FashionAde.Core.ContentManagement.ContentPublishedSection cs in content.Sections)
                {
                    Response.Write(string.Format("<span class=\"title\">{0}</span>", cs.Title));
                    Response.Write(string.Format("<p>{0}</p>",cs.Body));
                }
            %>
        </div>
<% } %>