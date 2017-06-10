<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.Core" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>
<%@ Import Namespace="FashionAde.Core.ContentManagement" %>
<%@ Import Namespace="FashionAde.Core.Clothing" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<div style="width: 710px; float: left; margin-right:5px;">
    <h1>Peak Inside Other Women’s Closets</h1>
    <h2>Because sometimes, the best inspiration comes from our most stylish girlfriends.</h2>
    <br />
    <div class="Outfit_Sponsors" style="margin-top:-65px;">
        <span>Sponsored by:</span>
        <img src="/static/img/Sponsors/logo_lg.jpg" alt="Ann Taylor"  />
    </div>
    <div style="clear:both"></div>
    
    <div class="publicClosetQuestions" style="margin-right:10px;">
        Want to reciprocate but don’t see your Closet?  <%= Html.ActionLink("Update your profile now", "Index", "Account", null, new { style = "display:inline;", @class = "otherOutfitsLink" })%>  to share some inspiration of your own.<br />
        
    </div>
    
    <div class="publicClosetQuestions">
        Don’t have a Closet of your own yet?<br />
        <%= Html.ActionLink("Build one now.", "Index", "Home", null, new { @class = "otherOutfitsLink" })%>
    </div>
    
    <span class="PublicClosetsAvailables"><%= ViewData["totalClosets"] %> Closets available for view.</span>    
    
    <div style="clear: both; background-color: #4D679B; height: 30px; color: White; padding: 4px 10px 2px 20px; font-weight: bold; width: 670px;" >
        <% using (Html.BeginForm("Index", "OtherOutfits")) {  %>
           <input type="hidden" id="pageNumber" name="pageNumber" value="1" />
           <input type="hidden" id="Season" name="Season" value="" />
           <div style="float:left; font-size:12px;">Filter by: <%= Html.DropDownList("styleType", (List<SelectListItem>)ViewData["FilterBy"], "Select...", new { style = "width: 120px;" })%> </div>
           <div style="float:right; position:relative; width:470px;">
           <div style="float:left; margin-top:5px; margin-right:5px; font-size:12px;">Search by User Name/ZipCode:</div> 
            <%= Html.TextBox("searchText", string.Empty, new { style = "margin-right: 45px; width: 225px; font-size: 11px; padding-left:5px;" })%> <input id="btnGo" type="image" src="/static/img/MyGarments/gobutton.JPG" style="float:right; position: absolute; right: 5px; top: 3px;" /></div>
        <% } %>
    </div>
    <% if (((IList<Closet>)ViewData["closets"]).Count > 0)
       { %>    
    <table style="margin-top:15px; width:700px;">            
        <tr class="publicClosetsHeader">
            <td>User Name</td>
            <td>Primary Style Type</td>
            <td>Zip</td>
            <td style="text-align:left;">Signature Outfit</td>
            <td>Closet Status</td>
        </tr>        
        <% foreach (Closet closet in ViewData["closets"] as IList<Closet>)
           { %>
            <tr class="publicClosetRow" >
                <td class="publicClosetRow"><%= closet.User.FullName%> </td>
                <td><%= closet.FavoriteOutfit.FashionFlavor.Name %></td>
                <td><%= closet.User.ZipCode%> </td>
                <td style="text-align:left;">
                    <% foreach (Garment garment in closet.FavoriteOutfit.Components)
                       { %>
                        <img id="<%= garment.Id %>" src="<%= Resources.GetGarmentPath(garment) %>" title="<%= garment.Title %>" />
	                <% } %>
                </td>                
                <td>
                    <% if (closet.PrivacyLevel == FashionAde.Core.Accounts.PrivacyLevel.FullCloset || closet.PrivacyLevel == FashionAde.Core.Accounts.PrivacyLevel.Friends)
                       { %>
                         <%= Html.ActionLink((closet.PrivacyLevel == FashionAde.Core.Accounts.PrivacyLevel.FullCloset) ? "All Outfits" : "Friend Outfits", "PublicCloset", "MyOutfits", new { userName = closet.User.UserName }, new { @class = "otherOutfitsLink" })%>
                      <% } else { %>
                        <% if (closet.PrivacyLevel == FashionAde.Core.Accounts.PrivacyLevel.FavoriteOutfit) { %>
                            <%= Html.ActionLink("Favorite Only", "OutfitResume", "MyOutfits", new { outfitId = closet.FavoriteOutfit.Id }, new { @class = "otherOutfitsLink" })%>
                        <% } %>
                    <% } %>    
                </td>
           </tr>
        <% } %>
    </table>
    <% } else { %>        
        <span class="noClosetResults">There are no results for this query!</span>
    <% } %>
    
    <div style="float:left; margin:5px 0 10px -5px; width:100%;">
        <% if (ViewData["Pages"] != null) Html.RenderPartial("Paging", ViewData["Pages"]); %>    
    </div>
</div>

<% Html.RenderPartial("UpdaterTrends", ViewData["styleAlerts"]); %> 
    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">
    $(document).ready(function() {
    $("#lnkOthersOutfits").css("background-color", "#F08331");

        $(".Page").click(ChangePage);
        $(".Page").mousein
        $(".Page").bind("mouseenter", function(e) {
            $(this).removeClass();
            $(this).addClass("NextPage");
        });
        $(".Page").bind("mouseleave", function(e) {
            $(this).removeClass();
            $(this).addClass("Page");
        });
    });

    function ChangePage() {        
        //apiOverlay.load();
        $("#pageNumber").val($(this).attr("id").split('_')[1]);
        $("#btnGo").click();
    }
    
</script>
</asp:Content> 