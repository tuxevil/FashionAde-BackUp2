<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<List<FashionAde.Core.ContentManagement.ContentPublishedSection>>" %>
<%@ OutputCache Duration="600" VaryByParam="none" %>

<% if (Model.Count > 0) { %>
<div style="text-align:center;">        
    <span class="updaterTitle">Updater Trends</span>
    <% foreach (FashionAde.Core.ContentManagement.ContentPublishedSection section in Model)
       { %>                
        <div class="StyleAlerts">            
            <span><% = section.Title %> </span>
            <p><% = section.Body %> </p>                
            <a href="<% = section.ContentPublished.UserFriendlyTitle %>" >Read More</a>
        </div>
    <% } %>        
</div>
<% } %>        
