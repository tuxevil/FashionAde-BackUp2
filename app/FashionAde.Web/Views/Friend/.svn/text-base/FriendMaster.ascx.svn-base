<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FriendsData>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<div id="div1" style="width:100%;float:left; margin-bottom:20px; ">
    <%  if(Model.List)
            Html.RenderPartial("FriendList", Model);
        else
            Html.RenderPartial("NewFriendList", Model.Friends); %>
    <div class="Account_RightBar">
        <div class="Account_RightBarContainer" style="margin-bottom: 20px;"><div class="Account_RightBarContainer_Title" style="margin-top: 0px;">Import your contacts</div>
            <div class="Account_ContactsProvider" ><center><a href="<%= ViewData["GMailAuthUrl"] %>" ><img src="/static/img/Friends/gmail-logo.jpg" alt="Google Contacts Importation" /></a></center></div>
            <div class="Account_ContactsProvider" ><center><a href="<%= ViewData["LiveAuthUrl"] %>" ><img src="/static/img/Friends/msn_logo.jpg" alt="Live Contacts Importation" /></a></center></div>
            <%--<div class="Account_ContactsProvider" ><center><a href="<%= ViewData["YahooAuthUrl"] %>" ><img src="/static/img/Friends/yahoo_logo.jpg" alt="Yahoo Contacts Importation" /></a></center></div>--%>
            <div style="clear: both;" ></div>
        </div>
        <div class="Account_RightBarContainer"><div class="Account_RightBarContainer_Title" style="margin-top: 0px;"><img src="/static/img/FriendProviders/mail.JPG"alt="By Mail" style="margin-right: 5px;" />Or do it manually</div>
            <div class="Account_Manually" >
            <% using (Html.BeginForm("AddFriend", "Friend", FormMethod.Post, new { @id = "AddManualFriend"}))
                   {   %>
                <div class="FriendManualInputs">First Name <br />
                <%= Html.TextBox("FriendFirstName", string.Empty, new { Style= "width: 178px;", maxlenth = 100 }) %><br /></div>
                <div class="FriendManualInputs">Last Name <br />
                <%= Html.TextBox("FriendLastName", string.Empty, new { Style = "width: 178px;", maxlenth = 100 })%><br /></div>
                <div class="FriendManualInputs">Email <br />
                <%= Html.TextBox("FriendEmail", string.Empty, new { Style = "width: 178px;", maxlenth = 100 })%><br /></div>                
                <br />
                <center><input type="submit" class="divButton" style="background-color: #4E6398; color: White; width: 140px; " value="ADD FRIEND" /></center>
                <% } %>
            </div>
        </div>
    </div>
    <div style="clear: both" ><% if(ViewData["Pages"] != null) Html.RenderPartial("Paging", ViewData["Pages"]); %></div>
    <input id="TotalCount" type="hidden" value="<%= ViewData["TotalCount"] %>" />
</div>