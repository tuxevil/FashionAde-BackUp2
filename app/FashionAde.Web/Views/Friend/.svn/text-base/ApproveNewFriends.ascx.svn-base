<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<IList<Friend>>" %>
<%@ Import Namespace="FashionAde.Core.Accounts"%>
<div class="parentDiv">
    <span class="ContactList_Title">Friends to confirm</span>
    <a class="divButton" href="<%= Url.RouteUrl(new { controller = "Friend", action= "Index"}) %>" >BACK TO FRIENDS</a>
    <div style="clear:both" ></div>
    <div class="Contact_LeftBarContainer" style="width: 730px; padding-bottom: 5px; padding-top: 5px;">
        <div class="Contact_ContactContainer" style="height: 335px;">
            <table width="100%" style="margin: 0px 0px 0px 0px;" >
            <%  foreach (Friend friend in Model)
                {
                    Html.RenderPartial("NewFriendRow", friend);
                } %>
            </table>
        </div>
        
    </div>
</div>