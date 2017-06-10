<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FriendsData>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core.Accounts"%>
<div class="parentDiv">
    <span class="ContactList_Title">Friends</span>
    <a class="divButton FriendsFilters" href="<%= Url.RouteUrl(new { controller = "Friend", action= "Index", status = 2 }) %>">ACEPTED</a>
    <a class="divButton FriendsFilters" href="<%= Url.RouteUrl(new { controller = "Friend", action= "Index", status = 1 }) %>">PENDING</a>
    <a class="divButton FriendsFilters" href="<%= Url.RouteUrl(new { controller = "Friend", action= "Index"}) %>">ALL</a>
    <% if (Model.NewFriendsRequest > 0)
       { %>
    <div class="divButton NewFriendsButton" ><a href="<%= Url.RouteUrl(new { controller = "Friend", action= "FriendsRequest"}) %>" ><img src="/static/img/greenok.png" style=" margin-top: 5px; margin-left: 7px; float: left;" /><span style="text-decoration: underline; color: Black; font-weight: normal; float: left; margin-left: 3px; margin-top: 5px; margin-right: 8px;">You have <span style="color: Black"><%=Model.NewFriendsRequest%> friends to confirm</span></span></a></div>
    <%} %>
    <div style="clear: both" ></div>
    <div class="Contact_LeftBarContainer" style="width: 730px; padding-top: 5px; padding-bottom: 5px; padding-right: 2px; margin-top: 10px;">
        <div class="Contact_ContactContainer">
            <table width="100%" style="margin: 0px 0px 0px 0px;" >
            <%  foreach (Friend friend in Model.Friends)
                {
                    Html.RenderPartial("FriendRow", friend);
                } %>
            </table>
        </div>
        <div style="clear: both" ></div>
    </div>
</div>
