<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
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
        
    <div class="MyGarment_Modal" id="Details" style="height:475px;">
        <img src='/static/img/close.png' class='close close_Image' />
        <h1>Garment Details:</h1>
        <h2 id="detailsTitle" style=" width: 400px; margin-bottom: 10px;"></h2>
        <div style="float: left; width: 100px;" >
        <center>
            <img id="PopUpImage"  />
            <span id="PopUpTitle" class="silouhetteDescription" style="float:left;"></span>
        </center>
        </div>
        <div style="float:left; width:510px;">
           <input id="ClosetGarmentId" name="ClosetGarmentId" type="hidden" />
            <table>
                <tr>
                    <td class="text">I purchased this garment at:</td><td><%= Html.TextBox("PurchasedAt", null, new { @class = "inputField", maxlength = 100 })%></td>
                </tr>
                <tr>
                    <td class="text">I purchased this garment on:</td><td><%= Html.TextBox("PurchasedOn", null, new { @class = "inputField", maxlength = 10 })%></td>
                </tr>
                <tr>
                    <td class="text">My garment is made by:</td><td><%= Html.TextBox("MadeBy", null, new { @class = "inputField", maxlength = 100 })%></td>
                </tr>
                <tr>
                    <td class="text">My garment is made of:</td><td><%= Html.TextBox("MadeOf", null, new { @class = "inputField", maxlength = 100 })%></td>
                </tr>
                <tr>
                    <td class="text">I've had this garment tailored:</td><td><%= Html.RadioButton("IsTailored", true, true )%> Yes <%= Html.RadioButton("IsTailored", bool.FalseString)%> No</td>
                </tr>
                <tr>                        
                    <td class="text">How I care for this garment:</td><td><%= Html.TextArea("CareConditions", string.Empty, 4, 38, new { style = "background-color: #E1E5F1; border-color: #E1E5F1; height:70px;", @class = "inputField" })%></td>
                </tr>
                <tr>
                    <td class="text">How and where I store this garment:</td><td><%= Html.TextArea("StoreConditions", string.Empty, 4, 38, new { style = "background-color: #E1E5F1; border-color: #E1E5F1; height:70px;", @class = "inputField" })%></td>
                </tr>
                <tr>
                    <td colspan="2" align="center"><div id="btnUpdate" class="divButton">Save </div></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<div class="parentDiv">
    <h1 id="selectGarmetsTitle" style="margin-top:15px;">My Garments</h1>
    <h2 style="display: inline; float:left; margin-bottom: 15px; margin-left: -255px; margin-top: 45px; width: 175px;">Review your garments and create your own outfit</h2>
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
            
            <!-- the tabs --> 

            <ul class="tabs"> 
	            <li style="margin-right:1px;"><a href="#">1. Pants, Jeans</a></li> 
	            <li style="margin-right:1px;"><a href="#">2. Skirts, Shorts</a></li> 
	            <li style="margin-right:1px;"><a href="#">3. Dresses</a></li> 
	            <li style="margin-right:1px;"><a href="#">4. Jackets</a></li> 
	            <li style="margin-right:1px;"><a href="#">5. Tops</a></li> 
	            <li style="margin-right:1px;"><a href="#">6. Accesories</a></li> 
            </ul>
            
	        <div class="panes"> 
	            <div>
	                <div class="MyGarments_Panel">
                        <% foreach (WebClosetGarment closetGarment in (List<WebClosetGarment>)ViewData["pants_jeans"])
                            {
                            %>
                                <div id="<%= "div_" + closetGarment.Id %>" class="MyGarments_Garments" >
                                    <div class="MyGarment_Tooltip_Trash">
                                        <img class="trashIcon" src='/static/img/trash.png' alt='Close' style="display:none;"  />
			                        </div>
                                    <center>                                        
                                        <img id="<%= "garment_" + closetGarment.Id %>" src="<%= Resources.GetWebClosetGarmentPath(closetGarment) %>" desc="<%= closetGarment.Title %>" class="GarmentDragable" /><br />
                                        <span class="GarmentTitle"><%= closetGarment.Title %></span>
                                    </center>
                                    <a id="pantsAdd_<%= closetGarment.Id %>"  class="AddClosetLink" onclick="return false" style="color:#F38333;">Add to Outfit</a> 
                                    <a id="pantsDetails_<%= closetGarment.Id %>"  class="DetailLink" onclick="return false" style="color:#F38333;" cat="<%= closetGarment.CatId %>">Details</a>                                    
                                </div>
                            <%} %>
                        <div style="clear:both"></div>
                    </div>
	            </div> 
	            <div>
	                <div class="MyGarments_Panel" >
                        <% foreach (WebClosetGarment closetGarment in (List<WebClosetGarment>)ViewData["skirts_shorts"])
                            {
                            %>
                                <div id="<%= "div_" + closetGarment.Id %>" class="MyGarments_Garments" >
                                    <div class="MyGarment_Tooltip_Trash">
                                        <img class="trashIcon" src='/static/img/trash.png' alt='Close' style="display:none;"  />
			                        </div>
                                    <center>
                                        <img id="<%= "garment_" + closetGarment.Id %>" src="<%= Resources.GetWebClosetGarmentPath(closetGarment) %>" desc="<%= closetGarment.Title %>" class="GarmentDragable" /><br />
                                        <span class="GarmentTitle"><%= closetGarment.Title %></span><br />
                                        <a id="skirtsAdd_<%= closetGarment.Id %>"  class="AddClosetLink" onclick="return false" style="color:#F38333;">Add to Outfit</a> 
                                        <a id="skirtsDetails_<%= closetGarment.Id %>"  class="DetailLink" onclick="return false" style="color:#F38333;" cat="<%= closetGarment.CatId %>">Details</a>
                                    </center>
                                </div>
                            <%} %>
                        <div style="clear:both"></div>
                    </div> 
	            </div> 
	            <div>
	                <div class="MyGarments_Panel" >
                        <% foreach (WebClosetGarment closetGarment in (List<WebClosetGarment>)ViewData["dresses"])
                            {
                            %>
                                <div id="<%= "div_" + closetGarment.Id %>" class="MyGarments_Garments" >
                                    <div class="MyGarment_Tooltip_Trash">
                                        <img class="trashIcon" src='/static/img/trash.png' alt='Close' style="display:none;"  />
			                        </div>
                                    <center>
                                        <img id="<%= "garment_" + closetGarment.Id %>" src="<%= Resources.GetWebClosetGarmentPath(closetGarment) %>" desc="<%= closetGarment.Title %>" class="GarmentDragable" /><br />
                                        <span class="GarmentTitle"><%= closetGarment.Title %></span><br />
                                        <a id="dressesAdd_<%= closetGarment.Id %>"  class="AddClosetLink" onclick="return false" style="color:#F38333;">Add to Outfit</a> 
                                        <a id="dressesDetails_<%= closetGarment.Id %>"  class="DetailLink" onclick="return false" style="color:#F38333;" cat="<%= closetGarment.CatId %>">Details</a>
                                    </center>
                                </div>
                            <%} %>
                        <div style="clear:both"></div>
                    </div> 
	            </div> 
	            <div>
	                <div class="MyGarments_Panel" >
                        <% foreach (WebClosetGarment closetGarment in (List<WebClosetGarment>)ViewData["jackets"])
                            {
                            %>
                                <div id="<%= "div_" + closetGarment.Id %>" class="MyGarments_Garments" >
                                    <div class  ="MyGarment_Tooltip_Trash">
                                        <img class="trashIcon" src='/static/img/trash.png' alt='Close' style="display:none;"  />
			                        </div>
                                    <center>
                                        <img id="<%= "garment_" + closetGarment.Id %>" src="<%= Resources.GetWebClosetGarmentPath(closetGarment) %>" desc="<%= closetGarment.Title %>" class="GarmentDragable" /><br />
                                        <span class="GarmentTitle"><%= closetGarment.Title %></span><br />
                                        <a id="jacketsAdd_<%= closetGarment.Id %>"  class="AddClosetLink" onclick="return false" style="color:#F38333;">Add to Outfit</a> 
                                        <a id="jacketsDetails_<%= closetGarment.Id %>"  class="DetailLink" onclick="return false" style="color:#F38333;" cat="<%= closetGarment.CatId %>">Details</a>
                                    </center>
                                </div>
                            <%} %>
                        <div style="clear:both"></div>
                    </div> 
	            </div> 
	            <div>
	                <div class="MyGarments_Panel" >
                        <% foreach (WebClosetGarment closetGarment in (List<WebClosetGarment>)ViewData["tops"])
                            {
                            %>
                                <div id="<%= "div_" + closetGarment.Id %>" class="MyGarments_Garments" >
                                    <div class="MyGarment_Tooltip_Trash">
                                        <img class="trashIcon" src='/static/img/trash.png' alt='Close' style="display:none;"  />
			                        </div>
                                    <center>
                                        <img id="<%= "garment_" + closetGarment.Id %>" src="<%= Resources.GetWebClosetGarmentPath(closetGarment) %>" desc="<%= closetGarment.Title %>" class="GarmentDragable" /><br />
                                        <span class="GarmentTitle"><%= closetGarment.Title %></span><br />
                                        <a id="topsAdd_<%= closetGarment.Id %>"  class="AddClosetLink" onclick="return false" style="color:#F38333;">Add to Outfit</a> 
                                        <a id="topsDetails_<%= closetGarment.Id %>"  class="DetailLink" onclick="return false" style="color:#F38333;" cat="<%= closetGarment.CatId %>">Details</a>
                                    </center>
                                </div>
                            <%} %>
                        <div style="clear:both"></div>
                    </div> 
	            </div> 
	            <div>
	                <div class="MyGarments_Panel" >
                        <% foreach (WebClosetGarment closetGarment in (List<WebClosetGarment>)ViewData["accesories"])
                            {
                            %>
                                <div id="<%= "div_" + closetGarment.Id %>" class="MyGarments_Garments" >
                                    <div class="MyGarment_Tooltip_Trash">
                                        <img class="trashIcon" src='/static/img/trash.png' alt='Close' style="display:none;"  />
			                        </div>
                                    <center>
                                        <img id="<%= "garment_" + closetGarment.Id %>" src="<%= Resources.GetWebClosetGarmentPath(closetGarment) %>" desc="<%= closetGarment.Title %>" class="GarmentDragable" /><br />
                                        <span class="GarmentTitle"><%= closetGarment.Title %></span><br />
                                        <a id="accAdd_<%= closetGarment.Id %>"  class="AddClosetLink" onclick="return false" style="color:#F38333;">Add to Outfit</a> 
                                        <a id="accDetails_<%= closetGarment.Id %>"  class="DetailLink" onclick="return false" style="color:#F38333;" cat="<%= closetGarment.CatId %>">Details</a>
                                    </center>
                                </div>
                            <%} %>
                        <div style="clear:both"></div>
                    </div> 
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
                
            <div class="createOutfitDropable" style="float: left;">
                <div class="createYourOutfitTitle">  
                    <div>
                        <span>Create Your Own Outfit</span>                    
                    </div>                    
                </div>   
                
                <div class="outfitDisplay" style="display:none;">
                    <img id="imgOutfitUp" src="/static/img/MyGarments/outfit_up.jpg" /> Forward
                    <img id="imgOutfitDown" src="/static/img/MyGarments/outfit_down.jpg" style="margin-left:10px;" /> Back
                </div>                
                
                <div id="MyOutfitDroppable" class="outfitDropable">                
                    <div id="garmentContainment" class="dragGarmentsMessage" >
                        <span id="garmentsMessage">Drag your garments here</span>                
                    </div>                    
                    <div class="createYourOutfitBottom" >                        
                        <div>
                            <%= Html.DropDownList("ddlSeasons", (List<SelectListItem>)ViewData["Seasons"], "Select...") %>
                            <% =Html.RadioButton("PrivacyStatus",  true, true, new { id = "PrivacyStatusPublic" })%> Save to Public Closet<br />
                            <% =Html.RadioButton("PrivacyStatus", false, false, new { id = "PrivacyStatusPrivate" })%> Save to Private Closet
                        </div>                                    
                        <input id="btnSaveUserOutfit" type="image" src="/static/img/MyGarments/img_save_to_outfits.jpg" />
                    </div>                
                </div>
            </div>
        </div>        
    
 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">
    Array.prototype.exists = function(o) {
        for (var i = 0; i < this.length; i++)
            if (this[i] === o)
            return true;
        return false;
    }

    var apiOverLay;
    var apiSlider;    
    var apiRemoveOverlay;
    var apiTooltip;
    var apiTabs;    
    var garmentSelected = 0;    
    var zIndex = 1; 
    var purchasedAtText = "(store)";
    var purchasedOnText = "(date)";
    var madeByText = "(designer)";
    var madeOfText = "(fabric content)"
    var careConditionsText = "(Laundering/maintenance details)";
    var storeConditionsText = "(Storage details)";

    $(document).ready(function() {
        $("#lnkMyGarments").css("background-color", "#F08331");
        
        $('.trashIcon').hide();        
        ApplyFunctionsToGarments();
        
        $("#MyOutfitDroppable").droppable({              
            accept: '.GarmentDragable',
            drop: function(event, ui) {
                if(allowAdd(ui.helper)){
                    $(this).append($(ui.helper).css("cursor", "move").addClass("drag_" + $(ui.helper)[0].id).clone());
                    $("#MyOutfitDroppable .GarmentDragable").addClass("item");
                    $(".item").removeClass("ui-draggable GarmentDragable");
                    $(".item").resizable({ aspectRatio: true }).parent().draggable({
                            containment: '#garmentContainment',                        
                            start: function(event, ui) {                        
                                clearDraggableItems();
                                selectDraggableItem(ui.helper);
                            }
                    });
                    
                    $("#garmentsMessage").fadeOut("normal");
                    $(".item").resizable( "option", "maxHeight", $(ui.helper)[0].height + 60 );
                    $(".item").resizable( "option", "maxWidth", $(ui.helper)[0].width + 60 );                
                    $(".item").resizable( "option", "minHeight", $(ui.helper)[0].height );
                    $(".item").resizable( "option", "minWidth", $(ui.helper)[0].width );
                    
                    $(".ui-icon").hide();
                                    
                    //TODO: Remove only selected item...
                    $(".MyGarment_Tooltip").removeClass("MyGarment_Tooltip");
                }
            }        
        });
                
        $(".DetailLink").overlay({        
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#Details",
            left: "center",
            top: "center",
            api: true,
            closeOnClick: false,
            onBeforeLoad: function() {
                apiOverLay = this;                
                garmentSelected = parseInt(this.getTrigger()[0].id.split("_")[1]);                                
                GetClosetGarmentData(this.getTrigger(), $("#garment_" + garmentSelected).attr("src"));
                $("#detailsTitle").text("Although many women own " + getCategoryName($(this.getTrigger()).attr("cat"))  + " like these, none of them is just like yours - add details!");
            }
        });        
        
        $(".AddClosetLink").click(function(e){
            var src = getSourceElement(e);            
            var img = $(src).parent().find(".GarmentDragable");
            
            if(allowAdd(img)){            
                var item = $(img).clone().css("cursor", "move").addClass("drag_" + $(img)[0].id);
                $("#garmentContainment").append(item); //Add to droppable div                
                $("#MyOutfitDroppable .GarmentDragable").addClass("item");
                                
                $(item).removeClass("ui-draggable GarmentDragable");
                $(item).resizable({ aspectRatio: true }).parent().draggable({
                        containment: '#garmentContainment',                    
                        start: function(event, ui) {                        
                            clearDraggableItems();
                            selectDraggableItem(ui.helper);                        
                        }
                });
                
                $("#garmentsMessage").fadeOut("normal");
                $(".item").resizable( "option", "maxHeight", img[0].height + 60 );
                $(".item").resizable( "option", "maxWidth", img[0].width + 60 );                
                $(".item").resizable( "option", "minHeight", img[0].height );
                $(".item").resizable( "option", "minWidth", img[0].width );
                            
                $(".ui-icon").hide();
                                
                //TODO: Remove only selected item...
                $(".MyGarment_Tooltip").removeClass("MyGarment_Tooltip");
            }
        });
        
                
        $('#MyOutfitDroppable').click(function(e) {        
            clearDraggableItems();
        });
        
        $("ul.tabs").tabs("div.panes > div");
        
        $(".inputField").css("padding-left", 5);
        $(".inputField").focus(function () {
            if($(this).val() == getDefaultText(this))
                $(this).val("");            
        });
        
        $(".inputField").blur(function () {
            if($(this).val() == "")
                $(this).val(getDefaultText(this));
        });
        
        $("#btnRemove").click(RemoveGarmentFromCloset);
        $("#btnUpdate").click(UpdateDetails);
        $("#btnSaveUserOutfit").click(saveUserOutfit);
        $("#PurchasedOn").datepicker( { dateFormat: 'mm/dd/yy', maxDate: '0' } );
        $("#PurchasedOn").keydown(function(e) 
            {e.preventDefault(); 
         });
    });
    
    function getCategoryName(catId)
    {
        if (catId < 3)  return "pants";
        if (catId < 5)  return "shorts";
        if (catId < 6)  return "dresses";
        if (catId < 9)  return "jackets";
        if (catId < 10) return "tops"; 
        if (catId < 16) return "accesories";        
    }
    
    
    function getDefaultText(txt)
    {
        switch (txt.id)
        {
            case "PurchasedAt":
                return purchasedAtText;
                
            case "PurchasedOn":
                return purchasedOnText;
                
            case "MadeBy":
                return madeByText;
                
            case "MadeOf":
                return madeOfText;
                
            case "CareConditions":
                return careConditionsText;
                
            case "StoreConditions":
                return storeConditionsText;     
                
            default:
                return "";            
        }
    }
    
    function allowAdd(item)
    {
        if(!$("#MyOutfitDroppable").find("#" +  item[0].id).is(":visible")
            && $("#MyOutfitDroppable").find(".item").length <= 10)
                return true;
                        
        return false;
    }
    
    function getCategoryFromItem(item) {
        return $(item).find("img").attr("cat");        
    }
    
    function setTab(catId) {
        catId = parseInt(catId);         
        if (catId < 3)  { apiTabs.click(0); return; }   //Pants, Jeans
        if (catId < 5)  { apiTabs.click(1); return; }   //Shorts, Skirts                
        if (catId < 6)  { apiTabs.click(2); return; }   //Dresses
        if (catId < 9)  { apiTabs.click(3); return; }   //Jackets
        if (catId < 10) { apiTabs.click(4); return; }   //Tops
        if (catId < 16) { apiTabs.click(5); return; }   //Accesories
    }


    function getCategoryFromTab(tabIndex) {
        var arrCat = new Array();
        switch (tabIndex) {
            case 0:
                arrCat.push(1);
                arrCat.push(2);
                break;
            case 1:
                arrCat.push(3);
                arrCat.push(4);
                break;
            case 2:
                arrCat.push(5);
                break;
            case 3:
                arrCat.push(6);
                arrCat.push(7);
                arrCat.push(8);
                break;
            case 4:
                arrCat.push(9);
                break;
            case 5:
                for (var i = 10; i < 16; i++ )
                    arrCat.push(i);                
                break;
        }
        
        return arrCat;
    } 
    
    
    function setSlider(tabIndex) {        
        var arrCat = getCategoryFromTab(tabIndex);
        var items = apiSlider.getItems();
        
        for(var i = 0; i <= items.size(); i++){            
            var cat = getCategoryFromItem($(items[i]).find("center"));
            
            for(var j = 0; j <= arrCat.length ; j++){
                if (arrCat[j] == cat) {
                    apiSlider.seekTo(i);
                    return;
                }
            }
        }
    }
    
    function selectDraggableItem(elm){        
        $(elm).addClass("draggableOutfit"); 
        $(elm).find(".ui-icon").show();        
        $(elm).css("zIndex", zIndex++);        
                
        if($(elm).find(".closeUserGarment").length == 0){
            $(elm).css("overflow", "visible");
            $(elm).prepend("<img src='/static/img/close.png' class='closeUserGarment' />");            
            $(".closeUserGarment").click(function(e){                
                $(this).parent().hide();  
                if($(".item:visible").length == 0)
                    $("#garmentsMessage").fadeIn("normal");
            }); 
            
        }
    }
        
    function clearDraggableItems(){        
        $(".draggableOutfit").removeClass("draggableOutfit");        
        $(".closeUserGarment").remove();
        $(".ui-icon").hide();        
    }
    
    function saveUserOutfit()    
    {
        var userOutfitSelection = {
            "ClosetOutfits": new Array(),            
            "Season": parseInt($("#ddlSeasons option:selected").val()),
            "PrivacyStatus": parseInt (($("#PrivacyStatusPublic")[0].checked) ? 1 : 0)
        };
        
        var errorMsg = isValid();        
        if(errorMsg != ""){
            showMessage(errorMsg);
            return false;
        }
                
        $("#MyOutfitDroppable .item:visible").each( function() {            
            userOutfitSelection.ClosetOutfits.push($(this)[0].id.split('_')[1]);
        });
        
        var encoded = $.toJSON(userOutfitSelection);
        
        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "OutfitCreation", action= "SaveUserOutfit"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if (data.Success == true) {
                    showMessage("You have successfully saved your outfit");
                    $(".item").remove();
                    $("#ddlSeasons option:eq(0)").attr("selected","selected");
                    $("#garmentsMessage").fadeIn("normal");
                }
                else
                {
                    showMessage(data.Message);
                }
            }
        });        
    }
    
    function isValid(){    
        var strError = "";
        var uniqueItems = true;
        var invalidSeason = $($("#ddlSeasons")[0].firstChild).is(":selected");        
        var atLeastTwoItems = ($("#MyOutfitDroppable .item:visible").length >= 2);
        
        for(var i = 0; i< $("#MyOutfitDroppable .item").length; i++)
        if($(".drag_" + $("#MyOutfitDroppable .item")[i].id + ":visible").length > 1)
            uniqueItems = false;
            
        if(invalidSeason)
            strError = "You must select a season.";
            
        if(!uniqueItems || !atLeastTwoItems)
            strError = "You must select at least two different garments.";
                        
        return strError;
    }
    
    function RemoveGarmentFromCloset() {        
        var encoded = $.toJSON(garmentSelected);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "MyGarments", action= "RemoveGarmentFromCloset"}) %>",
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
    
    function GetClosetGarmentData(element, imgSrc) {
        $("#PopUpImage").attr("src", imgSrc);
        $("#PopUpTitle").text($(element).parents().children(".GarmentTitle").text());
        
        var encoded = $.toJSON(garmentSelected);

        $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "MyGarmentsDetail", action= "GetClosetGarmentDetails"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    LoadGarmentData(data);
            }
        });
        
    }

    function LoadGarmentData(data) {
        $("#ClosetGarmentId").val(garmentSelected);
        $("#PurchasedAt").val((data.PurchasedAt != "") ? data.PurchasedAt : purchasedAtText );
        $("#PurchasedOn").val((data.PurchasedOn != "" && data.PurchasedOn != "01/01/0001") ? data.PurchasedOn : purchasedOnText );
        $("#MadeBy").val((data.MadeBy != "") ? data.MadeBy : madeByText );
        $("#MadeOf").val((data.MadeOf != "") ? data.MadeOf : madeOfText );
        $("input[name='IsTailored']").each(function() {
            if($(this).val() == data.IsTailored) {
                $(this).attr("checked", "checked");
            }
        });
        $("#CareConditions").val((data.CareConditions != "") ? data.CareConditions : careConditionsText);
        $("#StoreConditions").val((data.StoreConditions != "" ) ? data.StoreConditions : storeConditionsText );
    }

    function UpdateDetails() {        
        var detail = {
            "ClosetGarmentId": $("#ClosetGarmentId").val(),
            "PurchasedAt": ($("#PurchasedAt").val() != purchasedAtText) ? $("#PurchasedAt").val() : "",
            "PurchasedOn": ($("#PurchasedOn").val() != purchasedOnText) ? $("#PurchasedOn").val() : "",
            "MadeBy": ($("#MadeBy").val() != madeByText) ? $("#MadeBy").val() : "",
            "MadeOf": ($("#MadeOf").val() != madeOfText) ? $("#MadeOf").val() : "",
            "IsTailored": $("#IsTailored").val(),
            "CareConditions": ($("#CareConditions").val() != careConditionsText) ? $("#CareConditions").val() : "",
            "StoreConditions": ($("#StoreConditions").val() != storeConditionsText) ?  $("#StoreConditions").val() : ""
        };

        var encoded = $.toJSON(detail);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "MyGarmentsDetail", action= "SaveDetails"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if (data.Success == true) {
                    CloseDetails();
                }
            }
        });
    }

    function CloseDetails() {
        apiOverLay.close();
        $("#ClosetGarmentId").val("");
        $("#PurchasedAt").val("");
        $("#PurchasedOn").val("");
        $("#MadeBy").val("");
        $("#MadeOf").val("");
        $("#IsTailored").val("");
        $("#CareConditions").val("");
        $("#StoreConditions").val("");
    }
    
    function ApplyFunctionsToGarments() {        
        $(".GarmentDragable").draggable({ helper: 'clone', zIndex:1, stack: { group: '#set div', min: 1} });        
        
        $(".MyGarments_Garments").bind("mouseenter",function(e) {            
            garmentSelected = $(this).find(".GarmentDragable").attr("id").split("_")[1]
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