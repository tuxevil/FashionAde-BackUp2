<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Web.Controllers.MVCInteraction.OutfitView>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>
<%@ Import Namespace="FashionAde.Core.ContentManagement" %>

<input id="SiteURL" type="hidden" value="<%= ConfigurationManager.AppSettings["SiteURL"] %>" />
<div style="width: 710px; float: left; margin-right: 5px;">
    <h1 style="font-weight: normal; "><span id="Username" style="color:#F38333; font-weight: Bold;"><%= Model.UserName %></span> - <span id="OutfitQuantity" style="color:#F38333; font-weight: normal;"><%= Model.TotalOutfits %></span> Outfits</h1>
    <h2 style="">Closet status: <% if (!Model.ShowAsPublicCloset)
                                   {%><a href="<%= Url.RouteUrl(new { controller = "Account", action= "Index"}) %>" id="ClosetStatus"><%= Model.PrivacyLevel%></a><%} else {%><%= Model.PrivacyLevel %><%} %></h2>
    <h2 style="">Favorite Outfit: <span id="FavoriteOutfit" style="color:Black;"><% if (Model.FavoriteOutfit != "None Selected")
                                                                                                                                 {%><a href='<%= Url.RouteUrl(new { controller = "MyOutfits", action= "OutfitResume", outfitId = Model.FavoriteOutfit  }) %>' ><%=  Model.FavoriteOutfit%></a><% }
                                                                                                                                 else
                                                                                                                                 {%><%=  Model.FavoriteOutfit%><%} %></span></h2>
    
    <div class="Outfit_Sponsors" style="margin-top: -40px;">
        <span>Sponsored by:</span>
        <img src="/static/img/Sponsors/logo_lg.jpg" alt="Ann Taylor"  />
    </div>

    <div style="clear: both; background-color: #4D679B; height: 30px; color: White; padding: 4px 10px 2px 20px; font-weight: bold; width: 670px;" >            
        <% string actionName = (Model.ShowAsPublicCloset) ? "PublicCloset" : "Index"; %>
        
        <% using (Html.BeginForm(actionName, "MyOutfits", (Model.ShowAsPublicCloset) ? new { userName = Request.QueryString["userName"] } : null )) { %>
            <input type="hidden" id="Page" name="Page" value="<%= Model.CurrentPage %>" />
            <input type="hidden" id="Season" name="Season" value="<%= Model.Season %>" />            
            <div style="float:left;">Sort my outfits by: <%= Html.DropDownList("SortBy", (List<SelectListItem>)ViewData["SortBy"], new { style = "width: 120px;", onchange = "$('#btnGo').click();" })%> </div>
            <div style="float:right; position:relative; width:420px;"><div style="float:left; margin-top:5px; margin-right:4px;">Garment search:</div> <%= Html.TextBox("Search", string.Empty, new { style = "margin-right: 45px; width: 255px; font-size: 11px; padding-left:5px;", maxlength = 100 })%> <input id="btnGo" type="image" src="/static/img/MyGarments/gobutton.JPG" style="float:right; position: absolute; right: 5px; top: 3px;" /></div>
        <% } %>
    </div>
    
    <div id="GarmentsDiv">
        <!-- the tabs --> 
        <ul class="tabs" style="clear: both;">                 
            <% int count = 1;
                IList<Season> seasons = SeasonHelper.ListSeasons();                
                foreach (Season season in seasons) {
                    string className = "";                
                    if (Model.Season.ToString() == ((int)season).ToString())
                        className = "current";
                    %>                    
                    <li><a season="<%=((int)season).ToString()%>" href="#<%= count %>" class="<%= className %>"><%= count + ". " + season.ToString() %></a></li> 
                    <% count++; %>
            <% } %>
            
        </ul>            
        <!-- tab "panes" --> 
        <div class="panes" style="clear: both;"> 
            <% foreach (Season season in seasons){ 
                if (Model.Season.ToString() == ((int)season).ToString()) { %>
                    <div>
                        <% Html.RenderPartial("OutfitSeason", Model.Outfits); %>
                    </div>
                    <% } else { %>
                        <div></div>
                    <% } %>
            <% } %>
        </div>
    </div>
</div>