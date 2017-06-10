<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<List<ClosetOutfitView>>" %>
<%@ Import Namespace="FashionAde.Core.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core.Accounts"%>

<%
    if (Model.Count > 0)
    {
        foreach (ClosetOutfitView outfit in Model)
            Html.RenderPartial("Outfit", outfit);
        
       if(ViewData["Pages"] != null) Html.RenderPartial("Paging", ViewData["Pages"]);
    }
    else
    {
        MembershipUser currentUser = Membership.GetUser();
        if (currentUser != null && (int)currentUser.ProviderUserKey == (int)ViewData["closetUserId"])
        { %>
            <div class="NoOutfitDiv">
                <span>There is no Outfits for this season. If you want, you can add garments
                    <a href='<%= Url.RouteUrl(new { controller = "UploadGarment", action= "Index"}) %>' style="color:#F67F07; display: inline;">here</a>
                </span>
            </div>
        <% } else { %>
            <div class="NoOutfitDiv">
                <span>The user does not have Outfits for this season.</span>
            </div>
        <% } %>
<% } %>