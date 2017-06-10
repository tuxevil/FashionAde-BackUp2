<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<GarmentsListData>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<asp:Content ID="popupContent" ContentPlaceHolderID="OverlayPlaceHolder" runat="server">
<div class="modal" id="loading" style="width:350px; opacity:1; text-align:center;"> 
    <p>Loading - please wait - don't hit back button or refresh page until you've saved your closet items</p>
    <img src="http://l.yimg.com/a/i/us/per/gr/gp/rel_interstitial_loading.gif" />
</div> 
<input type="hidden" id="NoOne" />
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<% Html.RenderPartial("GarmentsList", Model); %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">

<script type="text/javascript">
    Array.prototype.exists = function(o) {
        for(var i = 0; i < this.length; i++)
           if(this[i] === o)
             return true;
        return false;
    }
    
    var myGarmentsItems = new Array();
    var newGarmentsItems = new Array();
    var myWishListItems = new Array();
    var newWishListItems = new Array();
    var pattern = 0;
    var fabric = 0;
    var silouhette = 0;
    var garmentselected = 0;
    var apiOverlay;
    var apiTooltip;
    var apiSlider;
    var apiTabs;

    $(document).ready(function() {        

        //****** Inialization *****
        fabric = $("#hidFabricId").val();
        silouhette = $("#hidSilouhetteId").val();
        pattern = $("#hidPatternId").val();

        //****** Slider ******
        apiSlider = $("#divGarmetsScroll").scrollable({ size: 4 }).mousewheel({ api: true, size: 3 });
        apiSlider.click(0); //Select first item by default
        apiSlider.onSeek(function(e, index) {
            var items = this.getVisibleItems();
            var cat = getCategoryFromItem(items[0]);
            setTab(cat);
        });

        //****** Tabs ******
        apiTabs = $("ul.tabs").tabs("div.panes > div",
            {
                api: true,                
                onClick: function(event, tabIndex) {
                    if (event.originalEvent){
                        setSlider(tabIndex);
                        $("ul.tabs").trigger("loadImages");
                    }
                }
            });

        //****** Drag and Drop******
        ApplyFunctionsToGarments();

        $("#divMyGarnets").droppable({
            drop: function(event, ui) {
                $(this).addClass('changeColor');
            }
        });

        $("#MyGarmentsDroppable").droppable({
            drop: function(event, ui) {
                AddToMyGarment(ui.draggable[0].id, ui.draggable);
            }
        });

        $("#MyWishListDropable").droppable({
            drop: function(event, ui) {
                AddToMyWishList(ui.draggable[0].id, ui.draggable);
            }
        });

        //****** Loading ******
        apiOverlay = $("#NoOne").overlay({
            // some expose tweaks suitable for modal dialogs 
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#loading",
            left: 430,
            top: "center",
            api: true,
            closeOnClick: false
        });

 
        $("#FabSelected").text($(".FabricSelected").text());
        $("#PatSelected").attr("src", $(".PatternSelected").attr("src"));
        
        
        $('#FabricFilters').bind("mouseleave", function(e) {
            if ($('#FabricFilters').is(':visible'))
                $('#FabricFilters').slideUp();
                
            $(".GarmentDragable").css( "position", "relative" ); //Hack for IE7 Visibility
        });
        
        $('#PatternFilters').bind("mouseleave", function(e) {
            if ($('#PatternFilters').is(':visible'))
                $('#PatternFilters').slideUp();
                
            $(".GarmentDragable").css( "position", "relative" ); //Hack for IE7 Visibility
        });
        
        
        $('#divEditGarments').bind("mouseleave", function(e) {
            if ($('#divEditGarments').is(':visible'))
                $('#divEditGarments').slideToggle();
        });
        
        $('#divEditMyWishList').bind("mouseleave", function(e) {
            if ($('#divEditMyWishList').is(':visible'))
                $('#divEditMyWishList').slideToggle();
        });
        
        myGarmentsItems = $("#myGarmentsItems").val().split(",");
        myWishListItems = $("#myWishListItems").val().split(",");
        
        addFunctionToRemoveButton();

        $("#lnkAddGarments").css("background-color", "#F08331");
        
        //****** Events ******
        $('.silouhettesContainer').click(function(e) {
            apiOverlay.load();
            silouhette = parseInt(this.id.split('_')[1]);
            
            fabric = 0;
            
            $('#FabricFilters').slideUp();            
            $('#PatternFilters').slideUp();
            
            GetSilouhette();
        });

        $('.Fabric').click(FabricsFunctions);
        $('.FabricSelected').click(FabricsFunctions);

        $('.Pattern').click(PatternsFunctions);
        $('.PatternSelected').click(PatternsFunctions);

        $('#btnAddToGarment').click(function(e) {
            AddToMyGarment(garmentselected, $('#' + garmentselected))
            apiTooltip.hide();
        });

        $('#btnAddToWishList').click(function(e) {
            AddToMyWishList(garmentselected, $('#' + garmentselected))
            apiTooltip.hide();
        });

        $('#btnNext').click(function(e) {
            e.preventDefault();            
            
            if ($('#divEditGarments')[0].childNodes.length == 0) {                
                removeMessages();
                showMessage("You must add some garments to continue.");                
                return false;                
            }
            
            appendMessage($("#frmNext"), "Validating your closet...", "loading");            
                        
            var encoded = $.toJSON($("#myGarmentsItems").val());
                        
            $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "GarmentSelector", action= "HasValidCombinations"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    removeMessages();
                    if(data.HasValidCombinations)
                        $("#frmNext").submit();
                    else    
                        showMessage("We can't create any combinations with these garments.");
                }
            });
        });

        $('#cboEditMyGarnets').click(function(e) {
            if ($('#divEditGarments')[0].childNodes.length > 0)
                $('#divEditGarments').slideToggle();

            if ($('#divEditMyWishList').is(':visible'))
                $('#divEditMyWishList').slideToggle();
        });

        $('#cboEditMyWishList').click(function(e) {
            if ($('#divEditMyWishList')[0].childNodes.length > 0)
                $('#divEditMyWishList').slideToggle();

            if ($('#divEditGarments').is(':visible'))
                $('#divEditGarments').slideToggle();

        });
        
        $('#cboFlavorSelector').click(function(e) {
            if ($('#flavorsSelectArea')[0].childNodes.length > 0)
                $('#flavorsSelectArea').slideToggle();            
        });

        $('#cboPatterns').click(function(e) {
            if ($('#PatternFilters')[0].childNodes.length > 0)
                $('#PatternFilters').slideToggle();

            if ($('#FabricFilters').is(':visible'))
                $('#FabricFilters').slideUp();
            
            $(".GarmentDragable").css( "position", "" ); //Hack for IE7 Visibility
        });

        $('#cbpFabrics').click(function(e) {
            if ($('#FabricFilters')[0].childNodes.length > 0)
                $('#FabricFilters').slideToggle();

            if ($('#PatternFilters').is(':visible'))
                $('#PatternFilters').slideUp();                                
                
            $(".GarmentDragable").css( "position", "" ); //Hack for IE7 Visibility
        });

        $('.nextPage .prevPage').click(function(e) {
            clickedTab = false;
        });


        $('.items').click(function(e) {
            $(".GarmentDragable").fadeTo("middle", 0.5);
        });
        
        
        $("#btnChangeFlavors").click(function(e){ 
            if($("#frmFlavor input[type='radio']:checked").length == 0)
            {
                showMessage("You must select a flavor to continue");
                return false;
            }
        });
        
    });

    function GetSilouhette() {
        var selection = {
            "SilouhetteId": silouhette,
            "FabricId": 0,
            "PatternId": 0,
            "Ids": new Array()
        };

        var encoded = $.toJSON(selection);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "GarmentSelector", action= "GetSilouhette"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if(data.Success == true)
                    {
                        RefreshFilters(data);
                    }
            }
        });
    };

    function Search() {
        var selection = {
            "SilouhetteId": silouhette,
            "FabricId": fabric,
            "PatternId": pattern,
            "Ids": new Array()
        };
        
        var encoded = $.toJSON(selection);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "GarmentSelector", action= "Search"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                updateGarments(data);                
            }
        });
    };

    function FabricsFunctions() {
        apiOverlay.load();
        var temp = this.id.split("_");
        fabric = parseInt(temp[1]);
        var $selectedFabric = $(".FabricSelected").removeClass();
        $selectedFabric.addClass("Fabric");
        $(this).removeClass();
        $(this).addClass("FabricSelected");

        $("#FabSelected").text($(this).text());

        if ($('#FabricFilters').is(':visible'))
            $('#FabricFilters').slideToggle();            
            
            //Hack for IE7 visibility
            $('#FabricFilters').slideUp();            
            $('#PatternFilters').slideUp();

        Search();
    }

    function PatternsFunctions() {
        apiOverlay.load();
        var temp = this.id.split("_");
        pattern = parseInt(temp[1]);
        var $selectedPattern = $(".PatternSelected").removeClass();
        $selectedPattern.addClass("Pattern");
        $(this).removeClass();
        $(this).addClass("PatternSelected");

        $("#PatSelected").attr("src", $(this).attr("src"));

        if ($('#PatternFilters').is(':visible'))
            $('#PatternFilters').slideToggle();            
            
        //Hack for IE7 visibility
        $('#FabricFilters').slideUp();            
        $('#PatternFilters').slideUp();
            
        Search();
    }

    function updateGarments(data) {
        $("#GarmentsResults").empty(); //Clear results
        var resPath = '<% = Resources.GetGarmentPath() %>';
        
        for (var i = 0; i < data.length; i++) {
            var garmetId = data[i].Id.toString();
            $("#GarmentsResults").append("<div id='" + data[i].Id + "' class='GarmentDragable GarmentSelector_Garments'><img src='" +
                resPath + data[i].ImageUri + "' desc='" + data[i].Title + "' /></div>");

            if (myGarmentsItems.exists(garmetId) || myWishListItems.exists(garmetId))
                $('#' + data[i].Id).hide();
        }
        
        //Hack for IE7 visibility
        $('#FabricFilters').slideUp();            
        $('#PatternFilters').slideUp();
            
        ApplyFunctionsToGarments();
        apiOverlay.close();
    }

    function ApplyFunctionsToGarments() {
        $(".GarmentDragable").mouseover(function(e) { 
            e.preventDefault();
        })

        $("img.Silouhettes").mousedown(function(e) { 
            if (e.preventDefault) e.preventDefault();
        })

        $(".GarmentDragable").draggable({ revert: true, stack: { group: '#set div', min: 1} });
        $(".GarmentDragable").tooltip({
            position: "center right",
            offset: [0, -60],
            effect: "fade",
            tip: '.garmenttooltip',
            relative: true,
            lazy: true,
            events: {def: 'click, mouseout' },
            onBeforeShow: function() {
                apiTooltip = this;
                garmentselected = parseInt(this.getTrigger()[0].id);
            }
        });
    }

    function AddToMyGarment(id, div) {
        if (!myGarmentsItems.exists(id)) {
            myGarmentsItems.push(id)
            $("#myGarmentsItems").val(myGarmentsItems);
            newGarmentsItems.push(id)
            $("#NewGarmentsItems").val(newGarmentsItems);

            increaseCount($("#MyGarmentCount"));

            addGarmentToEditList("divEditGarments", id);
            addFunctionToRemoveButton();
            if(div == undefined)
                deleteImage($("#" + id));
            else
                deleteImage(div);
        }
    }

    function AddToMyWishList(id, div) {
        if (!myWishListItems.exists(id)) {
            myWishListItems.push(id)
            $("#myWishListItems").val(myWishListItems);
            newWishListItems.push(id)
            $("#NewWishListItems").val(newWishListItems);

            increaseCount($('#MyWishListCount'));

            addGarmentToEditList("divEditMyWishList", id);
            addFunctionToRemoveButton();
            if(div == undefined)
                deleteImage($("#" + id));
            else
                deleteImage(div);
        }
    }

    function RefreshFilters(data) {
        
        $("#FabricFilters").empty(); //Clear filters
                
        var fabricclass;
        var found = false;
        for (var i = 0; i < data.Fabrics.length; i++) {
            if (data.Fabrics[i].Id == fabric)
                {
                    fabricclass = "FabricSelected";
                    $("#FabSelected").text(data.Fabrics[i].Description);
                    found = true;
                }
            else 
                fabricclass = "Fabric";            
            $("#FabricFilters").append("<span class='" + fabricclass + "' id='f_" + data.Fabrics[i].Id + "'>" + data.Fabrics[i].Description + "</span>");
        }
        $('.Fabric').click(FabricsFunctions);
        $('.FabricSelected').click(FabricsFunctions);
        
        // When changing silouhette if the fabric is not found, set default.
        if (!found && data.Fabrics.length > 0) {
            fabric = data.Fabrics[0].Id;
            $("#FabSelected").text(data.Fabrics[0].Description);
        }
        
        $("#PatternFilters").empty();
        var patternclass;
        var resPath = '<% = Resources.GetPatternPath() %>';
    
        found = false;    
        for (var i = 0; i < data.Patterns.length; i++) {
            if (data.Patterns[i].Id == pattern)
                {
                    patternclass = "PatternSelected";
                    $("#PatSelected").attr("src", resPath + data.Patterns[i].ImageUri);
                    found = true;       
                }
            else patternclass = "Pattern";
            $("#PatternFilters").append("<img class='" + patternclass + "' id='p_" + data.Patterns[i].Id + "' src='" + resPath + data.Patterns[i].ImageUri + "' alt='" + data.Patterns[i].Description + "' title='" + data.Patterns[i].Description + "' />");
        }
        
        // When changing silouhette if the pattern is not found, set default.
        if (!found && data.Patterns.length > 0) {
            pattern = data.Patterns[0].Id;
            $("#PatSelected").attr("src", resPath + data.Patterns[0].ImageUri);
        }
             
        $('.Pattern').click(PatternsFunctions);        
        $('.PatternSelected').click(PatternsFunctions);
        
        Search();
    }

    function addGarmentToEditList(listId, garmentId) {    
        var id = "divGarment" + garmentId;
        var div = "<div id='" + id + "' class='Garment_Item_Box'>";
        div += "<div class='Garment_Item_Box_Image'><center><img src='" + $('#' + garmentId).find("img").attr("src") + "' /></center></div>";
        div += "<div class='Garment_Item_Box_Clear'><img id='erase_" + garmentId + "' src='/static/img/remove_item.jpg' class='removeSelected' /></div></div>";
        $('#' + listId).append(div);
        
    }   
    
    function addFunctionToRemoveButton(){
        $('.removeSelected').each(function(){
            var garmentId = $(this)[0].id.split('_')[1];
            $(this).click(function(e) {
                var src = getSourceElement(e);
                $("#" + src.parentNode.parentNode.id).remove();

                if (getGarmentIndex(garmentId) != -1) {
                    myGarmentsItems.splice(getGarmentIndex(garmentId), 1); //Remove item from Array
                    decreaseCount($("#MyGarmentCount"));
                    if ($("#MyGarmentCount").text() == "0")
                        $("#divEditGarments").hide();
                }
                
                if (getWishListIndex(garmentId) != -1) {
                    myWishListItems.splice(getWishListIndex(garmentId), 1); //Remove item from Array
                    decreaseCount($("#MyWishListCount"));                
                    if ($("#MyWishListCount").text() == "0")
                        $("#divEditMyWishList").hide();
                }
                
                showGarment(garmentId);
            });
        });
    }

    function increaseCount(obj) {
        var count = parseInt(obj.text());
        count = count + 1;
        obj.text(count);
    }

    function decreaseCount(obj) {
        var count = parseInt(obj.text());
        count = count - 1;
        obj.text(count);
    }

    function getGarmentIndex(garmentId) {
        for (var i = 0; i < myGarmentsItems.length; i++) {
            if (myGarmentsItems[i] == garmentId)
                return i;
        }
        return -1;
    }
    
    function getWishListIndex(garmentId) {
        for (var i = 0; i < myWishListItems.length; i++) {
            if (myWishListItems[i] == garmentId)
                return i;
        }
        return -1;
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

    function setSlider(tabIndex) {
        var arrCat = getCategoryFromTab(tabIndex);        
        var items = apiSlider.getItems();
                
        for(var i = 0; i <= items.size(); i++){            
            var cat = getCategoryFromItem(items[i]);                        
            
            for(var j = 0; j <= arrCat.length ; j++){
                if (arrCat[j] == cat) {
                    apiSlider.seekTo(i);
                    return;
                }
            }
        }
    }


    function getCategoryFromItem(item) {
        return $(item).find("img").attr("cat");        
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

    function showGarment(garmentId) {
        $("#" + garmentId).show();    
    }
        
    function deleteImage($item) {
        $item.fadeOut(function() {
            $item.css("display", "none");
        });
    }
    
</script>

</asp:Content>