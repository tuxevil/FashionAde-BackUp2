<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FriendRatingHelper>" %>
<%@ Import Namespace="FashionAde.Core.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core.ThirdParties"%>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<% Html.EnableClientValidation(); %>
<% using (Html.BeginForm())
       { %>
<input type="hidden" id="Key" name="Key" value="<%= Model.KeyCode %>" />
    <div style="clear:both; width: 580px; float:left;">
        <%  foreach (ClosetOutfitGarmentView garment in Model.ClosetOutfit.OutfitGarments)                
            { %>
                <div class="Outfit_Result_Garment"><img src="<%= Resources.GetClosetOutfitGarmentViewPath(garment) %>" alt="<%= garment.Title %>" title="<%= garment.Title %>" /></div>
        <% }
            OutfitUpdater updater;
            if (ViewData["Updater"] != null)
            {
                updater = (OutfitUpdater) ViewData["Updater"];
                %><div class="Outfit_Result_Garment"><img class="OutfitUpdaterImg" src="<%= updater.ImageUrl %>" alt="<%= updater.Pattern.Description + " " + updater.Name %>" title="<%= updater.Pattern.Description + ": " + updater.Name  %>" /></div>
            <%}%>
    </div>
    <div style="clear:both;" ></div>
    <h2 style="margin-left: 45px;">Rate this look to and share feedback with <span id="User1">User</span></h2>
    
    
    <div style="margin-left: 31px;">
        <div style="width: 17px; margin-top: 33px; margin-left: -3px; float: left" >
            <input id="<%= "friendsrating"%>" name="<%= "friendsrating"%>" type="radio" class="friendratingstar" value="1" />
            <input id="<%= "friendsrating"%>" name="<%= "friendsrating"%>" type="radio" class="friendratingstar" value="2" />
            <input id="<%= "friendsrating"%>" name="<%= "friendsrating"%>" type="radio" class="friendratingstar" value="3" />
            <input id="<%= "friendsrating"%>" name="<%= "friendsrating"%>" type="radio" class="friendratingstar" value="4" />
            <input id="<%= "friendsrating"%>" name="<%= "friendsrating"%>" type="radio" class="friendratingstar" value="5" />
        </div><%= Html.ValidationMessageFor(x => x.friendsrating)%>
        <div style="float: left">
            <span style="color:Black;">Rate this Outfit Combination now by entering a number in the box:</span>
            <p>Rating Scale:<br />
                1 = Never wear this!<br />
                2 = Wear it in pinch<br />
                3 = Wear it occasionally<br />
                4 = Love it – put it in regular rotation!<br />
                5 = This is a Signature Outfit*
            </p>
        </div>
    </div>
    <div style="clear:both;" ></div>
    
    <span style="margin-left: 30px;">Share your comments with </span><span id="User2">User</span><span>:</span><br />
    <table><tr><td><textarea id="comment" name="comment" style="margin-left: 30px; width: 500px; height: 100px;"></textarea></td><td><%= Html.ValidationMessageFor(x => x.comment)%></td></tr></table><br /><br />
    
    <center>
        <input type="submit" class="divButton" value="Submit" />
    </center>
    <% }%>
    
    


