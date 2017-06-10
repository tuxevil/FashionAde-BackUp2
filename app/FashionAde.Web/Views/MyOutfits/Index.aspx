<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<FashionAde.Web.Controllers.MVCInteraction.OutfitView>" %>
<%@ Import Namespace="FashionAde.Core.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>
<%@ Import Namespace="FashionAde.Core.ContentManagement" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core"%>

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
            <p>Location: <%= Html.TextBox("Location", string.Empty, new { maxlength = 100 }) %> <span id="requiredLocation" class="hide" style="color:Red;">*</span></p>
            <p>Worn Date: <%= Html.TextBox("WornDate", string.Empty, new { maxlength = 10 })%> <span id="requiredWornDate" class="hide" style="color:Red;">*</span></p>
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
            <p>Do you want to replace your favorite outfit with this?</p> 
            <div id="btnFavoriteConfirmation" class="divButton">Yes</div><div class="divButton close" >No</div>
        </center>
    </div> 
    
    <div class="modal" id="RemoveFavoriteConfirmation" style="opacity:1;"> 
        <img src='/static/img/close.png' class='close close_Image' />
        <center>
            <p>This is your favorite outfit. Are you sure to remove it as a favorite?</p> 
            <div id="btnRemoveFavoriteConfirmation" class="divButton">Yes</div><div class="divButton close" >No</div>
        </center>
    </div> 
    
    <div class="modal" id="AddToMyClosetConfirmation" style="opacity:1;"> 
        <img src='/static/img/close.png' class='close close_Image' />
        <center>
            <p>Are you sure to add this outfit to your closet?</p> 
            <div id="btnAddToMyClosetConfirmation" class="divButton">Yes</div><div class="divButton close" >No</div>
        </center>
    </div> 
    
    <input type="hidden" id="Favorite" />  
    <input type="hidden" id="RemoveFavorite" />
    <input type="hidden" id="AddToMyCloset" />  
    
    <div class="modal" id="RemoveConfirmation" style="opacity:1;"> 
        <img src='/static/img/close.png' class='close close_Image' />    
        <center>
            <p>Are you sure to remove this item?</p> 
            <div id="btnRemove" class="divButton">Yes</div><div class="divButton close" >No</div>
        </center>
    </div> 
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <% Html.RenderPartial("OutfitList", Model); %>  
   
   <div style="float: left; width:226px;">
    <%
        IList<FashionFlavor> flavors = (List<FashionFlavor>)ViewData["FashionFlavors"];    
        if(flavors != null) 
        { %>
    
    
    <div class="GarmentSelector_FilterDiv_FashionFlavorSelected" style="float: left; ">
        <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Title">Your Fashion Flavors:</span>
        <div class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box">
            <center>
            <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Title" ><%= flavors[0].Name%></span>
            <img class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Image" src="<%= Resources.GetFashionFlavorThumbnailPath(flavors[0]) %>" alt="<%= flavors[0].Name %>"  />
            </center>
        </div>
        <div class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box">
            <center>
            <% if (flavors.Count > 1)
               {%>
            <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Title" ><%= flavors[1].Name%></span>
            <img class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Image" src="<%= Resources.GetFashionFlavorThumbnailPath(flavors[1]) %>" alt="<%= flavors[1].Name %>"  />
            <% } %>
            </center>
        </div>
    </div>
    
    <% } %>
        
    <% Html.RenderPartial("UpdaterTrends", Model.StyleAlerts); %> 
    
</div>

<input type="hidden" id="hiddenFilterText" value="<%= Model.FilterText %>" />
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
var apiFavoriteOverlay;
var closetOutfitId;
var apiAddToMyClosetOverlay;
var outfitSelected = 0;
var apiRemoveOverlay;
var apiAddNotateOverlay;
var apiSendToFriendsOverlay;
var apiRemoveFavoriteOverlay;
var closetoutfitid;
var canceledFavorite = false;
var currentValue = 0;
var searchText = $("#hiddenFilterText").val();
var txtEmails = "Write a friend name or email address";
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
            
            
        apiRemoveFavoriteOverlay = $("#RemoveFavorite").overlay({
            // some expose tweaks suitable for modal dialogs 
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#RemoveFavoriteConfirmation",
            left: "center",
            top: "center",
            api: true,
            closeOnClick: false
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
            
            
        $("ul.tabs").tabs("div.panes > div", {
            initialIndex: getSeasonIndex()
        });
        $(".tabs a").click(ChangeSeason);
        $(".Page").click(ChangePage);
        
        //Update master menu
        var otherOutfits = (location.href.indexOf("PublicCloset") != -1);
        var selectedMenu = (otherOutfits)  ? "#lnkOthersOutfits" : "#lnkMyOutfits";        
        $(selectedMenu).css("background-color", "#F08331");        
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
                currentValue = value;                
                $("#RateNow_" + id).empty();
                $("#MyRateValue_" + id).empty();
                $("#MyRateValue_" + id).append(value);
                $("#loadingMyRating_" + id).show();

                $.ajax({
                    type: "POST",
                    url: "<%= Url.RouteUrl(new { controller = "MyOutfits", action= "RateOutfit"}) %>",
                    data: encoded,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        if(data.Success == true)
                        {
                            if(data.RemoveFavorite)
                            {
                                apiRemoveFavoriteOverlay.load();                                 
                                closetoutfitid = id;
                            }
                            else if(data.ReplaceFavorite)
                            {
                                apiFavoriteOverlay.load();
                                closetoutfitid = id;
                            }
                            else if(data.SetFavorite)
                            {
                                DisplayFavorite(id);
                                showMessage("This outfit now designated as your signature oufit.");
                            }
                                
                            $("#loadingMyRating_" + id).hide();
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
                $("#requiredLocation").removeClass().addClass("hide");
                $("#requiredWornDate").removeClass().addClass("hide");
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
        $("#btnRemoveFavoriteConfirmation").click(ClearFavorite);
        
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
        
        
        $("#WornDate").datepicker( { dateFormat: 'mm/dd/yy', maxDate: '0' } );        
        $("#WornDate").keydown(function(e) 
            {e.preventDefault(); 
         });
        
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
                            showMessage("You added the outfit to your closet successfully!");
                        else
                            window.location.href = "<%= Url.RouteUrl(new { controller = "Login", action= "Index"}) %>";
                    }                   
                }
            });
	    });
	    	    
	    $('#txtEmails').bind('keypress', function(e) {
	        var code = e.keyCode || e.which;
            if(code == 13) { 
                InviteFriends();
            }
	    });
    });
    
    function getSeasonIndex()
    {
        var seasonIndex = 0;
        var season = $("#Season").val();
        var tabs = $(".tabs").find("a");
        
        for(var i = 0; i < tabs.length; i++)
            if($(tabs[i]).attr("season") == season)
                seasonIndex = $(tabs[i]).attr("href").split("#")[1] - 1;
                
        return parseInt(seasonIndex);
    }
    
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
    }
        
    function AddNotateToCloset() {
        if($("#Location").val() == "" || $("#WornDate").val() == "") {
            ($("#Location").val() == "")
                        ? $("#requiredLocation").removeClass("hide") 
                        : $("#requiredLocation").addClass("hide");
                        
            ($("#WornDate").val() == "") 
                        ? $("#requiredWornDate").removeClass("hide") 
                        : $("#requiredWornDate").addClass("hide");
                        
            return;
        }
            
    
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
                    $("#LastWorn_" + outfitSelected).text(selection.WornDate);
                    $("#To_" + outfitSelected).text(selection.Location);
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
        document.forms[0].submit();
    }
    
    function ChangePage(){
        apiOverlay.load();
        
        $("#Page").val($(this).attr("id").split('_')[1]); 
        <% if(!Model.ShowAsPublicCloset){%>
            document.forms[0].action = "<%=Url.RouteUrl(new {controller = "MyOutfits", action = "ChangePage"})%>";
        <%}%>
        document.forms[0].submit();
    }
    
    function ClearFavorite()
    {
        var encoded = $.toJSON(closetoutfitid);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "MyOutfits", action= "ClearFavorite"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if(data.Success == true)
                {
                    $("#FavoriteOutfit").html("None Selected");                    
                    apiRemoveFavoriteOverlay.close();
                }
            }
        });
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
                    DisplayFavorite(id);
                    apiFavoriteOverlay.close();
                    showMessage("This outfit now designated as your signature oufit.");
                }
            }
        });
    }
    
    function DisplayFavorite(id)
    {
        $("#FavoriteOutfit").html('<a href="<%= Url.RouteUrl(new { controller = "MyOutfits", action= "OutfitResume", outfitId = "code"  }) %>" >' + id + '</a>');
        $("#FavoriteOutfit").html($("#FavoriteOutfit").html().replace("code", id));
    }
    
</script>
</asp:Content>