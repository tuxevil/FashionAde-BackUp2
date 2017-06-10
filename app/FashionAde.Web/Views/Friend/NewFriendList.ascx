<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<IList<Friend>>" %>
<%@ Import Namespace="FashionAde.Core.Accounts"%>
<div class="parentDiv">
    <span class="ContactList_Title">Friends to confirm</span><div class="divButton" style="float:left; padding: 2px 10px 2px 10px; margin-top: 8px;"><a href="<%= Url.RouteUrl(new { controller = "Friend", action= "Index"}) %>" style="color:White; text-decoration: none;">BACK TO FRIENDS</a></div>
    <div style="clear: both" ></div>
    <div class="Contact_LeftBarContainer" style="width: 730px; padding: 5px 2px 5px 0px; margin-top: 10px;">
        <div class="Contact_ContactContainer">
            <table width="100%" style="margin: 0px 0px 0px 0px;" >
            <%  foreach (Friend friend in Model)
                {
                    Html.RenderPartial("NewFriendRow", friend);
                } %>
            </table>
        </div>
        <div style="clear: both" ></div>
    </div>
</div>
