<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<FashionAde.Core.WishList>" %>
<%@ Import Namespace="FashionAde.Core.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>
<%@ Import Namespace="FashionAde.Data.Repository"%>
<%@ Import Namespace="FashionAde.Core.FlavorSelection"%>
<%@ Import Namespace="FashionAde.Core.ContentManagement" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>


<asp:Content ID="contentOverlay" ContentPlaceHolderID="OverlayPlaceHolder" runat="server">
    <div class="modal" id="RemoveConfirmation" style="opacity: 1;"> 
        <img src='/static/img/close.png' class='close close_Image' />    
        <center>
            <p>Are you sure to remove this item?</p> 
            <div id="btnRemove" class="divButton" >Yes </div><div class="divButton close" >No</div>
        </center>
    </div>
    
    <div class="modal" id="loading"> 
        <p>Adding garment to closet...</p>
        <img src="http://l.yimg.com/a/i/us/per/gr/gp/rel_interstitial_loading.gif" />
    </div> 
        
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<div class="parentDiv">
    <h1 id="selectGarmetsTitle" style="margin-top:15px;">My Wish List</h1>
    <h2 style="display: inline; float: left; margin-bottom: 15px; margin-left: -255px; margin-top: 45px; width: 175px;">Add or remove garments to your closet.</h2>
    <%      
        IList<FashionFlavor> flavors = (List<FashionFlavor>)ViewData["FashionFlavors"];
             %>
            <div class="GarmentSelector_FilterDiv_FashionFlavorSelected" style="float: left; margin-left: 140px; margin-top: 5px; display: inline;">
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
                       {  %>
                    <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Title" ><%= flavors[1].Name%></span>
                    <img class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Image" src="<%= Resources.GetFashionFlavorThumbnailPath(flavors[1]) %>" alt="<%= flavors[1].Name %>"  />
                    <% } %>
                    </center>
                </div>
                <div style="clear: both" ></div>
            </div>            
    <div style="clear: both; ">
        <div id="GarmentsDiv" class="MyGarments_GarmentsDiv">
            <div>
                <div class="MyGarments_Panel">
                    <% foreach (WishGarment wishGarment in Model.Garments) { %>
                            <div id="<%= "div_" + wishGarment.Garment.Id  %>" class="MyGarments_Garments" wishGarmentId="<%= wishGarment.Id %>">
                                <div class="MyGarment_Tooltip_Trash">
                                    <img class="trashIcon" src='/static/img/trash.png' alt='Close' style="display:none;"  />
		                        </div>
                                <center>                                        
                                    <img id="<%= "garment_" + wishGarment.Garment.Id %>" src="<%= Resources.GetGarmentPath(wishGarment.Garment) %>" desc="<%= wishGarment.Garment.Title%>" class="GarmentDragable" /><br />
                                    <span class="GarmentTitle"><%= wishGarment.Garment.Title %></span>                                    
                                </center>
                                <a id="g_<%= wishGarment.Garment.Id %>"  class="AddClosetLink" onclick="return false" style="color:#F38333; text-align:center; float:none; margin-right:0;">Add to Closet</a>
                            </div>
                        <%} %>
                    <div style="clear:both"></div>
                </div>
            </div>
        </div>
    </div>     
    
    </div> 
    
    
    <div id="FiltersDiv" class="MyGarments_OutfitDiv" style="width: 280px;">
        <div class="MyGarments_Sponsors" style=" margin-bottom: 5px;" >
            <span>Sponsored by:</span>
            <img src="/static/img/Sponsors/logo_lg.jpg" alt="Ann Taylor"  />
        </div>
                
        <% Html.RenderPartial("UpdaterTrends", ViewData["styleAlerts"]); %> 
        
        </div>        

<input type="hidden" id="wishListId" value="<%= Model.Id %>" />
 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript" src='/static/js/jquery.MetaData.js' language="javascript"></script>
<script type="text/javascript">
    var apiOverLay;    
    var wishGarmentSelected = 0;
    var garmentSelected = 0;
        
    $(document).ready(function() {
        $("#lnkWishList").css("background-color", "#F08331");        
        $('.trashIcon').hide();
                
        ApplyFunctionsToGarments();
                
        $(".AddClosetLink").overlay({            
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#loading",
            left: "center",
            api: true,
            top: "center",
            onBeforeLoad: function() {
                apiOverLay = this;                           
            }            
        });
                
        $(".AddClosetLink").click(addToCloset);        
        $("#btnRemove").click(RemoveGarmentFromWishList);
    });
    
    function addToCloset()
    {
        var selected = {
            "GarmentId": garmentSelected,            
            "WishListId": wishGarmentSelected
        };
        
        var encoded = $.toJSON(selected);
                
        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "WishList", action= "AddToCloset"}) %>",            
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                apiOverLay.close();
                window.location = document.location.href;
            }
        });
    }
        
    function RemoveGarmentFromWishList() {
        var encoded = $.toJSON(wishGarmentSelected);
        
        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "WishList", action= "Remove"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if (data.Success == true) {
                    $("#div_" + garmentSelected).remove();
                    apiRemoveOverlay.close();
                }
            }
        });
    }
        
    function ApplyFunctionsToGarments() {        
        $(".MyGarments_Garments").bind("mouseenter",function(e) {            
            garmentSelected = $(this).attr("id").split("_")[1]
            wishGarmentSelected = $(this).attr("wishGarmentId");
            
            $(this).addClass("MyGarment_Tooltip");
            $(this).find(".trashIcon").show();           
            
            $(".trashIcon").overlay({
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
                }
            });
        });

        $(".MyGarments_Garments").bind("mouseleave", function(e) {
            $(this).removeClass("MyGarment_Tooltip");
            $(this).find(".trashIcon").hide();            
        });   
    }
    
</script>
</asp:Content>
