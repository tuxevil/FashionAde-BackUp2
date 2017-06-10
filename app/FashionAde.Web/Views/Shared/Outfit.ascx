<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<ClosetOutfitView>" %>
<%@ Import Namespace="FashionAde.Core.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>

<div class="OutfitDiv" id="<%= "div_" + Model.Id  %>">
    <div class="outfitContainer">  
        <div class="outfitHeader">     
            <a href="<%= Url.RouteUrl(new { controller = "MyOutfits", action= "OutfitResume", outfitId = Model.Id  }) %>" id="OutfitName" class="outfitName"><%= Model.ShortEventTypes + Model.ClosetOutfitId %></a>
            <% if (!Model.Disabled) {%>
                <img id="<%= "btnRemove_" + Model.Id %>" src="/static/img/MyGarments/bluex.JPG" class="OutfitRemove" />
                <div class="seeMannequin divButton">TRY THIS ON</div>
            <% } else {  %>
                <% if (Model.ShowAddToMyCloset && Membership.GetUser() != null)
                   { %>                         
                        <div type="button" class="addToMyCloset divButton" id="<%= Model.Id %>">Add To My Closet</div>
                        <img id="addToClosetLoading_<%= Model.Id %>" src="/static/img/ajax-loader-small.gif" style="margin:11px 0 0 5px; float:left; display:inline; display:none;" />
                   <% } %>
            <% } %>
        </div>        
    <%
        string images = string.Empty;
        string key = Model.ClosetOutfitId.ToString();
        int cant = 1;
        foreach (ClosetOutfitGarmentView garment in Model.OutfitGarments)
        {
            if (garment.GarmentId == 0)
                continue;
             %>        
            <div class="Outfit_Result_Garment">
                <img id="g_<%= garment.GarmentId %>" src="<%= Resources.GetClosetOutfitGarmentViewPath(garment) %>" alt="<%= garment.Title %>" title="<%= garment.Title %>" />
            </div>            
            <% if(cant <=5)
                   images += "{'type':'image','src':'" + Resources.GetClosetOutfitGarmentViewPath(garment) + "','href':'" + ConfigurationManager.AppSettings["SiteURL"] + "/outfit/rating/index/" + key + "/default.aspx'},";
            cant++;
        }
        if (images.Length > 0)
            images = images.TrimEnd(','); %>   
         
         <div class="outfitBottom">
            <% 
                string wornDate = "Add Now";
                string location = "Add Now";
                if (Model.WornDate.Year > 1)
                    wornDate = Model.WornDate.ToString("MM/dd/yyyy");
                if (!string.IsNullOrEmpty(Model.Location))
                    location = Model.Location;
             %>
            <div class="Outfit_Details" style="width: 130px;" >Last worn: <span id="<%= "LastWorn_" + Model.Id %>" style="color: #767676;"><%= wornDate%></span></div>
            <div class="Outfit_Details" >To: <span id="<%= "To_" + Model.Id %>" style="color: #767676; "><%= location%></span></div>
            
            <% if (!Model.Disabled)
               { %>
                    <div class="Outfit_Details"><div id="<%= "btnNotation_" + Model.Id %>" class="OutfitNotate divButton addDetailsButton" style="background-color: #536998; font-size:10px;" >ADD DETAILS</div></div>
            <% } %>
        </div>
        
    </div> 
    
    <div class="outfitUpdater">
        <div class="outfitHeader" style="padding-left:8px" >
            <span class="outfitTitle" style="width:80px;">Updater</span>
        </div>  
        <a class="divButton addThis" target="_blank" style="width: 50px; text-align: center;" href="<%= Model.OutfitUpdater.BuyUrl %>" >BUY</a>
        <div class="Outfit_Result_Garment" style="margin: 5px 0px 15px 5px;">            
            <a href="<%= Model.OutfitUpdater.BuyUrl %>" target="_blank">
                <img class="OutfitUpdaterImg" src="<%= Model.OutfitUpdater.ImageUrl %>" alt="<%= Model.OutfitUpdater.Name + " $" + Model.OutfitUpdater.Price %>" title="<%= Model.OutfitUpdater.Name + " $" + Model.OutfitUpdater.Price%>" />
            </a><br />
            <span><%= Model.OutfitUpdater.Partner.Name %></span><br />
            <% if (Model.OutfitUpdater.Price != null)
               {%>
            <span style="color: black; font-weight: normal;"><%= Convert.ToDecimal(Model.OutfitUpdater.Price).ToString("$#,##0.00")%></span><br />
            <a href="<%= Url.RouteUrl(new { controller = "OutfitUpdaters", action= "Index", Id = Model.PreCombination.Id }) %>" class="linkViewAll">VIEW MORE</a>   
            <%} %>
        </div>
    </div>
    
    <div class="outfitRatings">
        <div class="outfitHeader" style="text-align:left;">
            <span class="outfitTitle ratingTitle">Editor Rating</span> 
            <span class="outfitTitle ratingTitle">My Rating</span> 
            <span class="outfitTitle ratingTitle">Friends Rating</span>
        </div>
        
        <div class="starRating">
            <div class="divStar"><%= Convert.ToInt16(Model.EditorRating) %></div>
            <div class="starImg">
                <% for (float count = 1; count <= 5; count++) {                
                    if (count == Model.EditorRating) {%>
                       <input name="<%= "editorrating" + Model.Id %>" type="radio" class="myratingstar star" checked="checked" disabled="disabled"/>
                    <%} else {%>
                       <input name="<%= "editorrating" + Model.Id %>" type="radio" class="myratingstar star" disabled="disabled"/>
                    <%}
                }%>
            </div>
            <% if (!Model.Disabled){%>
            <div style="clear: both">
                <input type="image" onclick="
                    doPublish({method: 'stream.publish',attachment: {
                            'name':'FashionAde',
                            'href':'<%= ConfigurationManager.AppSettings["SiteURL"] %>/outfit/rating/index/<%= key %>/default.aspx',
                            'description':'Would you like to rate my outfit?',
                            'media':[<%= images %>]
                        },
                        action_link: [
                          {
                            text: 'Rate it now!',
                            href: '<%= ConfigurationManager.AppSettings["SiteURL"] %>/outfit/rating/index/<%= key %>/default.aspx'
                          }
                        ]});return false;" value="Share in Facebook" src="/static/img/f.PNG" class="shareFacebook" title="Share in Facebook" />
            </div>
            <%} %>
        </div>
        
        <div class="starRating">
            <form name="<%= "myRating_" + Model.ClosetOutfitId%>">
                <input id="<%= "ratingPrevious_" + Model.ClosetOutfitId %>" type="hidden" />
                <div id="<%= "MyRateValue_" + Model.ClosetOutfitId%>" class="RateNow divStar" style="background-color: #F67F07;">
                    <%= Convert.ToInt16(Model.MyRating) %>
                </div>
            
                <%  string disable = "";
                    if(Model.Disabled)
                        disable = "disabled='disabled'";
                    if (Model.MyRating > 0) { %>
                    <div class="starImg" >
                        <% for (float count = 1; count <= 5; count++) {
                            if (count == Model.MyRating) {%>
                                <input id="<%= "myrating_" + Model.ClosetOutfitId %>" name="<%= "myrating_" + Model.ClosetOutfitId %>" type="radio" checked="checked" class="myratingstar" value="<%= count %>" <%= disable %> />
                            <% } else { %>
                                <input id="<%= "myrating_" + Model.ClosetOutfitId %>" name="<%= "myrating_" + Model.ClosetOutfitId %>" type="radio" class="myratingstar" value="<%= count %>" <%= disable %> />
                            <% }
                        }%>
                    </div>
                <% } else { %>
                    <div class="starImg">
                        <input id="<%= "myrating_" + Model.ClosetOutfitId %>" name="<%= "myrating_" + Model.ClosetOutfitId %>" type="radio" class="myratingstar" value="1" title="Very Poor" <%= disable %> />
                        <input id="<%= "myrating_" + Model.ClosetOutfitId %>" name="<%= "myrating_" + Model.ClosetOutfitId %>" type="radio" class="myratingstar" value="2" title="Poor" <%= disable %> />
                        <input id="<%= "myrating_" + Model.ClosetOutfitId %>" name="<%= "myrating_" + Model.ClosetOutfitId %>" type="radio" class="myratingstar" value="3" title="Normal" <%= disable %>/>
                        <input id="<%= "myrating_" + Model.ClosetOutfitId %>" name="<%= "myrating_" + Model.ClosetOutfitId %>" type="radio" class="myratingstar" value="4" title="Good" <%= disable %>/>
                        <input id="<%= "myrating_" + Model.ClosetOutfitId %>" name="<%= "myrating_" + Model.ClosetOutfitId %>" type="radio" class="myratingstar" value="5" title="Very Good" <%= disable %>/>
                    </div>
                    <% if (!Model.Disabled)
                       {%>
                    <div id="<%= "RateNow_" + Model.ClosetOutfitId%>" class="RateNow" style="clear: both;">
                        <img src="/static/img/MyGarments/ratenow.JPG" alt="Rate Now" style="margin-top:5px; margin-left: -8px;" />
                    </div>
                    <%} %>                    
                <% } %>                
                <input class="MyRatingValue" id="<%= Model.ClosetOutfitId %>" type="hidden" value="<%= Model.MyRating %>" />
                <img id="loadingMyRating_<%= Model.ClosetOutfitId %>" src="/static/img/ajax-loader-small.gif" alt="" style="margin-top:5px; display:none; margin-left:3px;" />
            </form>            
        </div>
        
        <div class="divFriendRatings">
            <div class="divStar" style="background-color:#636363;">
                <%= Convert.ToInt16(Model.AverageFriendRating) %>
            </div>
        
            <% if (Model.AverageFriendRating > 0) { %>
                <div class="starImg">
                    <% float fraction = (float)0.5;
                        for (float count = fraction; count <= 5; count += fraction) {
                            if (count > 0 && count > Model.AverageFriendRating - (fraction / 2) && count < Model.AverageFriendRating + (fraction / 2)) {%>
                                <input name="<%="friendsrating" + Model.Id%>" type="radio" class="myratingstar star {split:2}" checked="checked" disabled="disabled" />
                            <% } else { %>
                                <input name="<%="friendsrating" + Model.Id%>" type="radio" class="myratingstar star {split:2}" disabled="disabled" /> <% } 
                        }%>
                </div> 
            <% } else { %>
                    <div style="width: 17px; margin-top: 15px; margin-left: 7px;" >
                        <input name="<%= "friendsrating" + Model.Id %>" type="radio" class="myratingstar star" disabled="disabled" />
                        <input name="<%= "friendsrating" + Model.Id %>" type="radio" class="myratingstar star" disabled="disabled" />
                        <input name="<%= "friendsrating" + Model.Id %>" type="radio" class="myratingstar star" disabled="disabled" />
                        <input name="<%= "friendsrating" + Model.Id %>" type="radio" class="myratingstar star" disabled="disabled" />
                        <input name="<%= "friendsrating" + Model.Id %>" type="radio" class="myratingstar star" disabled="disabled" />
                    </div>
            <% } %>
            <% if (!Model.Disabled){%>
            <div style="clear:both;">
                <input id="<%= "btnSendToFriends_" + Model.Id %>" class="SendToFriends" type="image" src="/static/img/MyGarments/sendnow.JPG" onClick="javascript:{return false;}" value="Send Now" style="margin-top:5px; margin-left: -4px;" />
            </div>
            <%} %>
        </div>
    </div>
</div>