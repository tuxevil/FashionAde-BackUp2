<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h1>Upload images of your own clothes</h1>
<h3>Files uploaded</h3>
<h2>The following files were upload successfully:</h2>

<div class="uploadResults" style="float:left;">
    <%
        var i = 0;
        foreach (UserGarment userGarment in (List<UserGarment>)ViewData["uploadedFiles"])        
        {%>
        <div class="uploadItem">
            <a href="<%= Resources.GetGarmentLargePath() + userGarment.ImageUri %>" id="thumbnail_<%= i %>" class="viewUploadedImage" onclick="return false;" title="<%=userGarment.Title %>" >
                <img src="<%= Resources.GetGarmentLargePath() + userGarment.ImageUri %>" alt="<%=userGarment.Title %>" class="thumbnail"  />
            </a>            
        </div>        
    <% i++; } %>    
    <h2 style="clear:both;margin-left:10px;">If you don´t want to edit your garments, please return to <a href="<%= Url.RouteUrl(new { controller = "MyGarments", action= "Index" }) %>" ">My Garments</a></h2>
</div>



<div class="createOutfitDropable" style="float: left; width:400px; display:inline; margin-left:30px;">
    <div class="createYourOutfitTitle">  
        <div>
            <span>Image Edit Panel</span>                    
        </div>                    
    </div>   
    
    <div class="outfitDisplay" style="display:none;">
        <img id="imgOutfitUp" src="/static/img/MyGarments/outfit_up.jpg" /> Forward
        <img id="imgOutfitDown" src="/static/img/MyGarments/outfit_down.jpg" style="margin-left:10px;" /> Back
    </div>                
    
    <div id="MyOutfitDroppable" class="outfitDropable">
        <span style="margin-top:10px; display:block;">Do you want to crop your image, or are you ready to save it?</span>
        <div id="garmentContainment" class="dragGarmentsMessage" >            
            <div id="divImageEdit" style="margin-top:50px;"><img id="imgEdit" class="cropImage" /> </div>            
        </div>        
        Select or drag here the image that you want to edit.
        <img id="btnSave" src="/static/img/MyGarments/img_save.gif" class="cropSave" />            
    </div>
</div>

<input type="hidden" id="X" />
<input type="hidden" id="Y" />
<input type="hidden" id="W" />
<input type="hidden" id="H" />

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script src="/static/js/jquery.Jcrop.min.js"  type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var apiJcrop;    
    var zIndex = 0; 
    
    $(document).ready(function() {
        $("#lnkAddGarments").css("background-color", "#F08331");
        
        bindDraggable();        
        
        $(".outfitDropable").droppable({
            accept: '.uploadItem',
            drop: function(event, ui) {
                clearCache();
                bindThumbnail();
                $(ui.helper).find(".thumbnail").click();
            }
        });
        

        $("#btnSave").click(function(e) {
            if(!validSelection())
            {
                showMessage("You have not cropped the image!")
                return false;
            }
        
            var co = {
                "X": $("#X").val(),
                "Y": $("#Y").val(),
                "Width": $("#W").val(),
                "Height": $("#H").val(),
                "ImageUri": $("#imgEdit").attr("src").split("?")[0]
             };
                               
            var encoded = $.toJSON(co);

            $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "UploadGarment", action= "CropImage"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    if (data.Success == true) {
                        showMessage("Your image has been saved!");
                        reloadThumbnailImages();
                                                
                        var src =  $("#imgEdit").attr("src");
                        $("#imgEdit").attr("src", src + "?" + new Date().getTime());
                        $("#imgEdit").css("height", apiJcrop.tellScaled().h);
                        $("#imgEdit").css("width", apiJcrop.tellScaled().w);
                        
                        bindDraggable();
                        bindJcrop();
                        clearCoords();                        
                    }
                }
            });
        });
        
        /*
        TODO: Ver como usar las imagenes de forma simetrica (y agregar padding)
        $(".thumbnail").each(function (i) {            
            var newSize = scaleSize(50, 50, $(this)[0].width, $(this)[0].height);
            $(this).css("width", newSize[0]);
            $(this).css("height", newSize[1]);
        });
        */
        
        bindThumbnail();
                
        $("#imgEdit").load(function(e) {
            if (jQuery.browser.safari)
            {
                $(this).removeAttr("width")
                    .removeAttr("height")
                    .css({ width: "", height: "" }); 
                            
                $('#imgEdit').css("width", this.width);
                $('#imgEdit').css("height", this.height);
                
                bindJcrop();
            }
        });
        
        
        $(".thumbnail").click(function(e){ 
           $(".thumbnail").removeClass("thumbnailSelected");           
            $(this).addClass("thumbnailSelected");               
        });
        
        clearCache();
        $("#thumbnail_0").click();        
    }); 
    
    
    function scaleSize(maxW, maxH, currW, currH){
        var ratio = currH / currW;

        if(currW >= maxW && ratio <= 1){
        currW = maxW;
        currH = currW * ratio;
        } else if(currH >= maxH){
        currH = maxH;
        currW = currH / ratio;
        }
        return [currW, currH];
    }
           
    function bindThumbnail()
    {
        $(".viewUploadedImage").click(function(e){                
            var src = getSourceElement(e);            
            var imageUri = ($(src).attr("src")) ? $(src).attr("src") : $(src).attr("href");
            
            $("#imgEdit").attr("src", "");
            $('#imgEdit').attr("src", imageUri);
                        
            if (!jQuery.browser.safari)                
                bindJcrop();
        });
    }
    
    function bindDraggable(){
        $(".uploadItem").draggable({ 
            helper: 'clone',                
            zIndex: zIndex + 1,
            stop: function(event, ui) {            
                $(".thumbnail").removeClass("thumbnailSelected");
                var src = getSourceElement(event);                
                $(src).find(".thumbnail").addClass("thumbnailSelected");
            }
        });
    }
    
    function reloadThumbnailImages()
    {   
        var arrIds = new Array();
        var arrSrc = new Array();
        var arrTitle = new Array();
        
        $(".thumbnail").each(function (i) {
            arrIds.push($(this).attr("id"));
            arrSrc.push($(this).attr("src"));            
            arrTitle.push($(this).attr("alt"));
        });
        
        $(".uploadResults").empty();
                
        for(var i = 0; i < arrIds.length; i++ )
        {
            var img = "";
            img = "<div class='uploadItem'>";
            img += "<a href='" + arrSrc[i] + "' id='thumbnail_" + arrIds[i]  + "' class='viewUploadedImage' onclick='return false;' title='" + arrTitle[i]  + "' >";
            img += "<img src='" + arrSrc[i]  + "?" + new Date().getTime() + "' alt='" + arrTitle[i]  + "' class='thumbnail'  />";
            img += "</a></div>";
            
            $(".uploadResults").append(img);
        }
        
        bindThumbnail();   
        clearCache();      
    }
    
    
    function clearCache() // Triggers onload if image is cached
    {
        var src = $("#imgEdit").attr("src");
        $("#imgEdit").attr("src", "");
        $("#imgEdit").attr("src", src);
    }
        
    function bindJcrop()
    {
        if(apiJcrop != undefined)
            apiJcrop.destroy(); 
            
        apiJcrop = $.Jcrop('#imgEdit');
        apiJcrop.setOptions({ onSelect: storeCoords });
        apiJcrop.release();
    }
    
    function storeCoords(c) {
            $("#X").val(c.x);   //X Coord
            $("#Y").val(c.y);   //Y Coord
            $("#W").val(c.w);   //Width
            $("#H").val(c.h);   //Height                       
    }
    
    function clearCoords()
    {
        $("#X").val(0);   //X Coord
        $("#Y").val(0);   //Y Coord
        $("#W").val(0);   //Width
        $("#H").val(0);   //Height
    }
    
    function validSelection()
    {
        return ($("#W").val() != 0 && $("#H").val() != 0);
    }
       
</script>
</asp:Content>