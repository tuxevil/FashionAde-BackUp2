<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OutfitResume>" %>
<%@ Import Namespace="FashionAde.Core.Accounts"%>
<%@ Import Namespace="FashionAde.Core.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>
<%@ Import Namespace="FashionAde.Data.Repository"%>
<%@ Import Namespace="FashionAde.Core.FlavorSelection"%>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<asp:Content ID="contentOverlay" ContentPlaceHolderID="OverlayPlaceHolder" runat="server">
    <div id="fb-root"></div>
    <div class="modal" id="SendToFriends" style="width: 600px;opacity:1; "> 
        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix" unselectable="on">
            <span class="inviteFriendTitle"  unselectable="on" >Email this outfit:</span>
            <img src='/static/img/close.png' class='close close_Image' />
            
        <div style="clear:both"></div>
        </div>
        <% Html.RenderPartial("InviteFriends"); %>        
        <div style="clear:both"></div>        
        <center>
            <div id="btnInviteFriends" class="divButton" style="margin-right:10px;" >INVITE FRIENDS</div>
            <div class="divButton close">CANCEL</div>            
        </center>
        <img id="imgLoading" src="/static/img/MyGarments/Ajax-loader.gif" style="float:right; display:block; margin-top:-32px; display:none;" />
    </div>
    
    <div class="modal" id="AddNotate" style="width: 300px; opacity:1; "> 
        <img src='/static/img/close.png' class='close close_Image' />
        <center>
            <p>Location: <%= Html.TextBox("Location") %></p> 
            <p>Worn Date: <%= Html.TextBox("WornDate") %></p>
            <div id="btnAddNotate" class="divButton" >Add</div><div class="divButton close">Cancel</div>
        </center>
    </div> 
        
    <div class="modal" id="loading"> 
        <p>Loading... Please Wait</p> 
        <img src="http://l.yimg.com/a/i/us/per/gr/gp/rel_interstitial_loading.gif" />
    </div> 
 
    <input type="hidden" id="NoOne" />        
 
    <div class="modal" id="favoriteConfirmation" style="opacity:1;"> 
        <img src='/static/img/close.png' class='close close_Image' />
        <center>
            <p>Do you want to replace youre favorite outfit with this?</p> 
            <div id="btnFavoriteConfirmation" class="divButton">Yes</div><div class="divButton close" >No</div>
        </center>
    </div> 
    
    <input type="hidden" id="Favorite" />  
    
    <div class="modal" id="RemoveConfirmation" style="opacity:1;"> 
        <img src='/static/img/close.png' class='close close_Image' />    
        <center>
            <p>Are you sure to remove this item?</p> 
            <div id="btnRemove" class="divButton">Yes</div><div class="divButton close" >No</div>
        </center>
    </div> 
    
    <div class="modal" id="AddToMyClosetConfirmation" style="opacity:1;"> 
        <img src='/static/img/close.png' class='close close_Image' />
        <center>
            <p>Are you sure to add this outfit to your closet?</p> 
            <div id="btnAddToMyClosetConfirmation" class="divButton">Yes</div><div class="divButton close" >No</div>
        </center>
    </div> 
    
    <input type="hidden" id="AddToMyCloset" />
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">    
    <% 
        bool isCurrentUserOutfit = (Membership.GetUser() != null && Membership.GetUser().UserName == Model.OutfitUserName) ? true : false;
        bool isFavorite = (Model.OutfitView.ClosetOutfit.IsFavouriteOutfit);
        string userName = (isCurrentUserOutfit) ? "MY" : Model.OutfitUserName.ToUpper() + "'S";
        
        if (isCurrentUserOutfit)
        { 
            if(isFavorite) { %>
                <h1>My Favorite Outfit</h1>                
            <% } else { %>
                <h1>My Outfits</h1>                
            <% } %>        
            <a href="<%= Model.UserClosetUrl %>"  class="divButton" style="padding:3px 5px 3px 5px; margin-left:20px; background-color:#496694; text-decoration:none;" >RETURN TO <%= userName %> CLOSET</a>
            <br /><br />
        <% } else {  
            if(isFavorite) { %>
                <h1><%= Model.OutfitUserName %>'s Favorite Outfit</h1>
                <br />
            <% } else { %>
            <h1><%= userName %> Outfits</h1>
            <a href="<%= Model.UserClosetUrl %>"  class="divButton" style="padding:3px 5px 3px 5px; margin-left:20px; background-color:#496694; text-decoration:none;" >RETURN TO <%= userName %> CLOSET</a>
            <br /><br />
        <% } %>
    <% } %>
    
    
    <%  Html.RenderPartial("Outfit", Model.OutfitView); %>
    
    <% if (Model.ShowRatings){%>
    <div style="width: 100%;">
        <div class="OutfitDiv" style="width: 300px; border: solid 2px #DDE4EE; float: left;">
            <div>  
                <div class="outfitHeader" style="background-color: #DDE4EE;">     
                    <span class="outfitTitle" style="margin: 11px 0 0 7px; font-size: 16px; color: #656563; font-weight: bold;"><%= Model.TotalFriendRatings%> Friends Rated this Outfit</span>
                </div>        
                <div style="padding: 10px 30px 10px 0;">
            <% if (Model.Ratings.Count > 0)
                   foreach (OutfitResumeFriendRating rating in Model.Ratings)
                   {%>        
                    <div style="margin-bottom: 3px; margin-left: 25px;"><span style="float: left; display: block;"><%= rating.Count%> Friend<% if (rating.Count > 1)
                                                                                                                                               {%>s<%} %></span> <span style="color: #656563; font-weight: normal; float: left; display: block; margin-left: 5px;">gave this a</span> <span style="margin-left: 5px; float: left; display: block; margin-right: 5px;"><%= rating.Rating%></span> <img style="float:right;" src="<%= Resources.GetRatingLargePath() + rating.Rating + ".jpg" %>" alt="<%= rating.Rating %>" /></div>
                <%}
               else
               {%> <span style="color: #656563; font-weight: normal; margin-left: 25px;">There is no ratings from friends.</span>
               <%} %>
                </div>
            </div> 
        </div>

        <div class="OutfitDiv" style="width: 375px; margin-left: 19px; border: solid 2px #DDE4EE;  float: left; clear: none;">
            <div>  
                <div class="outfitHeader" style="background-color: #DDE4EE;">     
                    <span class="outfitTitle" style="margin: 11px 0 0 7px; font-size: 16px; color: #656563; font-weight: bold;">Friends Comments</span>
                </div> 
                <div style="padding: 10px 10px 10px 10px;">       
            <% if (Model.Comments.Count > 0)
                   foreach (OutfitResumeFriendComments comment in Model.Comments)
                   {%>        
                    <div style="margin-bottom: 5px;"><span><%= comment.Name%>:</span> <span style="color: #656563; font-weight: normal;">"<%= comment.Comment%>"</span></div>
                <%}
               else
               {%> <span style="color: #656563; font-weight: normal; margin-left: 25px;">There is no comments from friends.</span>
               <%} %>
                </div>
            </div> 
        </div>
    </div>
    <%} %>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script src='/static/js/jquery.rating.js' type="text/javascript" language="javascript"></script> 
<script src='/static/js/jquery.MetaData.js' type="text/javascript" language="javascript"></script>
<script src='/static/js/jquery.autocomplete.js' type="text/javascript" language="javascript"></script>
<script>
    window.fbAsyncInit = function() {
        FB.init({
            appId: '<%= ConfigurationManager.AppSettings["FacebookConnectId"] %>',
            xfbml: true,
            cookie: true,
            status: true
        });
    };
    (function() {
        var e = document.createElement('script'); e.async = true;
        e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
        document.getElementById('fb-root').appendChild(e);
    } ());

    function doPublish(attachment) {
        FB.ui(attachment);
    }
</script>
<script type="text/javascript">
var apiOverlay;
var closetOutfitId;
var apiAddToMyClosetOverlay;
var apiFavoriteOverlay;
var outfitSelected = 0;
var apiRemoveOverlay;
var apiAddNotateOverlay;
var apiSendToFriendsOverlay;
var closetoutfitid;
var searchText = $("#hiddenFilterText").val();
var txtEmails = "Enter email addresses, separated by commas";
    $(document).ready(function() {    
         apiOverlay = $("#NoOne").overlay({
                // some expose tweaks suitable for modal dialogs 
                expose: {
                    color: '#333',
                    loadSpeed: 200,
                    opacity: 0.3
                },
                target: "#loading",
                left: "center",
                top: "center",
                api: true,
                closeOnClick: false
            });
            
        apiFavoriteOverlay = $("#Favorite").overlay({
                // some expose tweaks suitable for modal dialogs 
                expose: {
                    color: '#333',
                    loadSpeed: 200,
                    opacity: 0.3
                },
                target: "#favoriteConfirmation",
                left: "center",
                top: "center",
                api: true,
                closeOnClick: false
            });
          
        var initialTab = "";
        switch($("#Season").val())
        {
            case "1":
                initialTab = 0;
                break;
                
            case "2":
                initialTab = 1;
                break;
        
            case "4":
                initialTab = 2;
                break;
                
            case "8":
                initialTab = 3;
                break;
                
            case "15":
                initialTab = 4;
                break;
        }
          
            
        $("ul.tabs").tabs("div.panes > div", {
            initialIndex: parseInt(initialTab)
        });
        $(".tabs a").click(ChangeSeason);
        $(".Page").click(ChangePage);
        
        //Update master menu
        $("#lnkMyOutfits").css("background-color", "#F08331");
        $("ul.tabs").tabs("div.panes > div");
        $('input.star').rating({
	        required: 'hide'
	    });
	    
	    $(".divButton").removeClass("hide"); //Buttons are hidden by default
	            
        $('input.myratingstar').rating({
            required: 'hide',
	        callback: function(value, link) {
	            var id = 0;
	            if( $(this).attr("id") != undefined)
	                id = $(this).attr("id").split('_')[1];
	            var selection = {
                    "ClosetOutfitId": id,
                    "Rate": value,
                    "Key": '00000000-0000-0000-0000-000000000000'
                };

                var encoded = $.toJSON(selection);

                $.ajax({
                    type: "POST",
                    url: "<%= Url.RouteUrl(new { controller = "MyOutfits", action= "RateOutfit"}) %>",
                    data: encoded,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        if(data.Success == true)
                        {
                            if(data.WantFavorite == true)
                            {
                                apiFavoriteOverlay.load();
                                closetoutfitid = id;
                            }
                            
                            $("#RateNow_" + id).empty();
                            $("#MyRateValue_" + id).empty();
                            $("#MyRateValue_" + id).append(value);
                            
                        }
                    }
                });
             }
	    });
        
        
        $(".SendToFriends").overlay({
            // some expose tweaks suitable for modal dialogs
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#SendToFriends",
            left: "center",
            api: true,
            top: "center",
            onBeforeLoad: function() {
                apiSendToFriendsOverlay = this;
                outfitSelected = parseInt(this.getTrigger()[0].id.split("_")[1]);
            }
        });
	    
	    $(".OutfitRemove").overlay({            
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#RemoveConfirmation",
            left: "center",
            api: true,
            top: "center",
            onBeforeLoad: function() {
                apiRemoveOverlay = this;
                outfitSelected = parseInt(this.getTrigger()[0].id.split("_")[1]);
            }
        });
        
        $(".OutfitNotate").overlay({            
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#AddNotate",
            left: "center",
            api: true,
            top: "center",
            onBeforeLoad: function() {
                $("#Location").val("");
                $("#WornDate").val("");                            
                apiAddNotateOverlay = this;
                outfitSelected = parseInt(this.getTrigger()[0].id.split("_")[1]);                
            },
            onLoad: function() {
                $("#Location").focus();
            } 
        });
            
        $("#btnRemove").click(RemoveGarmentFromCloset);
        $("#btnAddNotate").click(AddNotateToCloset);
        $("#btnInviteFriends").click(InviteFriends);
        $("#btnFavoriteConfirmation").click(SetFavorite);
        
        $(".Page").mousein
        $(".Page").bind("mouseenter",function(e) {
            $(this).removeClass();
            $(this).addClass("NextPage");
        });
        $(".Page").bind("mouseleave", function(e) {
            $(this).removeClass();
            $(this).addClass("Page");
        });
        
        $(".seeMannequin").click(function(e){            
            var src = getSourceElement(e);
            var id = $(src).parent().find(".OutfitRemove")[0].id.split("_")[1]; 
            
            var selection = {
                 "OutfitId": id,
                 "GarmentIds": new Array() 
             };
             
            var top = $(this).offset().top - 35;
            var left = $(this).offset().left - 60;            
            
            var garments = $("#div_" + id + " .Outfit_Result_Garment");
            for(var i = 0; i < garments.length -1; i++)                
                selection.GarmentIds.push($(garments[i].innerHTML)[0].id.split("_")[1]);             
                                 
            var encoded = $.toJSON(selection);

            $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "UploadGarment", action= "GetLayers"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) { 
                    if (data.Success == true) {
                        showMannequin(data, src, id, top, left);
                    }
                }
            });  
        });
        
        
        $("#WornDate").datepicker( { dateFormat: 'dd/mm/yy' } );        
        
        var $txtSearch = $("#Search");
        $txtSearch.css("color","gray");
        $txtSearch.val(searchText);
        
        $txtSearch.focus(function () {
            if($("#Search").val() == searchText)
                $("#Search").val("");
        });
        
        $txtSearch.blur(function () {
            if($("#Search").val() == "")
                $(this).val(searchText).css("color","gray");
        });
        
        $txtSearch.keydown(function() { $(this).css("color","black"); });


        var $txtEmails = $("#txtEmails");
        $txtEmails.css("color","gray");
        $txtEmails.val(txtEmails);
        
        $txtEmails.focus(function () {
            if($("#txtEmails").val() == txtEmails)
                $("#txtEmails").val("");
        });
        
        $txtEmails.blur(function () {
            if($("#txtEmails").val() == "")
                $(this).val(txtEmails).css("color","gray");
        });
        
        $txtSearch.keydown(function() { $(this).css("color","black"); });


        $("#txtEmails").autocomplete('<%= Url.Action("GetFriends", "MyOutfits") %>', 
        {
            dataType: 'json',
            width: 300,
		    multiple: true,
		    matchContains: true,
		    formatItem: formatItem,
		    formatResult: formatItem
        });
        
        $("#txtEmails").result(function(event, data, formatted) {
            var hidden = $(this).parent().next().find(">:input");
		    hidden.val( (hidden.val() ? hidden.val() + ";" : hidden.val()) + data[1]);
	    });
	    
	    apiAddToMyClosetOverlay = $("#AddToMyCloset").overlay({
                // some expose tweaks suitable for modal dialogs 
                expose: {
                    color: '#333',
                    loadSpeed: 200,
                    opacity: 0.3
                },
                target: "#AddToMyClosetConfirmation",
                left: "center",
                top: "center",
                api: true,
                closeOnClick: false
            });
            
	    
	    $(".addToMyCloset").click(function(e){
	        apiAddToMyClosetOverlay.load();
	        closetOutfitId = getSourceElement(e).id; 
	    });
	    
	    $("#btnAddToMyClosetConfirmation").click(function(e){   
	        apiAddToMyClosetOverlay.close();
            var encoded = $.toJSON(closetOutfitId);
            
            $("#addToClosetLoading_" + closetOutfitId).show();
                        
            $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "OutfitCreation", action= "CopyOutfit"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {                
                    $("#addToClosetLoading_" + closetOutfitId).hide();
                    if (data.Success == true) {                         
                        if(data.RegisteredUser)
                            showMessage("You added the outfit to you closet successfully!");
                        else
                            window.location.href = "<%= Url.RouteUrl(new { controller = "Login", action= "Index"}) %>";
                    }                   
                }
            });
	    });
	    
	    
    });

    function formatItem(row) {
		return row[0] + "\" [" + row[1] + "]";
	}
	function formatResult(row) {
	    return row[1].replace(/(<.+?>)/gi, '');
	}
	
    function showMannequin(data, src, id, top, left)
    {
        if($("#divMannequin_" + id).length > 0)
            return;
        
        var div = "";
        div = "<div id='divMannequin_" + id + "' class='mannequin' style='top:" + top + "px; left:" + left + "px;' >";
        div += "<div class='mannequinHeader'>" + $(src).parent().find("#OutfitName").text() + "</div>";
        div += "<img src='/static/img/close.png' class='closeUserGarment' />";
        div += "<div class='MannequinOrder'>";
        div += "<div id='divTops_" + id + "'></div>";
        div += "<div id='divBottom_" + id + "'></div>";
        div += "<div id='divAccesories_" + id + "'></div>";
        div += "</div>";
        div += "</div>";
        $(src).parent().append(div);
                
        for(var i = 0; i < data.Layers.length; i++)
        {
            var pos = getLayerPosition(data.Layers[i].Layer);
            switch(pos) 
            {            
                case "top":
                    
                    $("#divTops_" + id).append("<img src='<%= Resources.GetGarmentPath() %>" +  data.Layers[i].ImageUri + "'/>");
                    break;
                    
                case "bottom":
                    $("#divBottom_" + id).append("<img src='<%= Resources.GetGarmentPath() %>" + data.Layers[i].ImageUri + "'/>");
                    break;
                    
                case "accesories":
                case "":
                    $("#divAccesories_" + id).append("<img src='<%= Resources.GetGarmentPath() %>" + data.Layers[i].ImageUri + "'/>");
                    break;
            }            
        }
        
        $("#divMannequin_" + id).draggable();
        $(".closeUserGarment").click(function(e){                
            $(this).parent().remove();
        });
    }
    
    function getLayerPosition(layerCode)
    {
        if(layerCode > 0 && layerCode < 3)   
            return "bottom";        //A, Ai
        else if (layerCode > 2 && layerCode < 7 )    
            return "top";           //Aii, B, C, D
        else return "accesories";    //ACC1    
    }
    
    function RemoveGarmentFromCloset() {        
        var encoded = $.toJSON(outfitSelected);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "MyOutfits", action= "RemoveOutfitFromCloset"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {                
                if (data.Success == true) {
                    apiRemoveOverlay.close();
                    apiOverlay.load();
                    $("#btnGo").click();
                }
            }
        });
    }
    
    function getOutfitImage(id, position){
        var garments = $("#div_" + id + " .Outfit_Result_Garment");
        
        //for(var i = 0; i < garments.length; i++)
        //foreach hasta q coincida con el layer
        
            
    }
    
    function AddNotateToCloset() {
        var selection = {
                    "OutfitSelected": outfitSelected,
                    "Location": $("#Location").val(),
                    "WornDate": $("#WornDate").val()
                };
        
        var encoded = $.toJSON(selection);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "MyOutfits", action= "AddNotateToCloset"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if (data.Success == true) {
                    apiAddNotateOverlay.close();                    
                    $("#To").text($("#Location").val());
                    $("#LastWorn").text($("#WornDate").val());
                }
            }
        });
    }
    
    function InviteFriends() {
    
        $("#imgLoading").show();
    
        var selection = {
             "FriendsEmails": $("#txtEmails").val(),
             "OutfitId": outfitSelected,
             "Message": $("#txtMessage").val(),
             "SendMe": $("#SendToMe").is(':checked'),
             "SiteURL": $("#SiteURL").val()
             };
                    
        var encoded = $.toJSON(selection);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "MyOutfits", action= "InviteFriends"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if (data.Success == true) {
                    $("#txtEmails").val("");
                    $("#txtMessage").val("");
                    $("#SendToMe").removeAttr('checked');
                    $("#imgLoading").hide();
                    apiSendToFriendsOverlay.close();
                }
            }
        });
    }

    function DisableMyRating(){
        $('input.myratingstar', this.form).rating('disable');
    }
    
    function ChangeSeason()    {
        apiOverlay.load();
        $("#Season").val($(this).attr("season")); 
        $("#Page").val(1); 
        $("#btnGo").click();
    }
    
    function ChangePage(){
        apiOverlay.load();
        $("#Page").val($(this).attr("id").split('_')[1]); 
        $("#btnGo").click();
    }
    
    
    function SetFavorite() {
        var id = closetoutfitid;
        var selection = {
            "ClosetOutfitId": id,
            "Rate": 5
        };

        var encoded = $.toJSON(selection);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "MyOutfits", action= "SetFavorite"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if(data.Success == true)
                {
                    apiFavoriteOverlay.close();
                }
            }
        });
    }
    
</script>
</asp:Content>