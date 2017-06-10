<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<HomeRegisteredUserInfo>" %>
<%@ Import Namespace="FashionAde.Core" %>
<%@ Import Namespace="FashionAde.Core.ContentManagement" %>
<%@ Import Namespace="FashionAde.Core.Clothing" %>
<%@ Import Namespace="FashionAde.Core.MVCInteraction" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="fb-root"></div>
    <div id="tempMessage"></div>
<%
    string userName = Model.UserName;    
    string message = (TempData["message"] != null) ? TempData["message"].ToString() : string.Empty;    
    TextInfo ti = new CultureInfo("en-US", false).TextInfo;
    userName = ti.ToTitleCase(userName);        
%>
<% if (Model.HaveBeenRated)
   {%>
<div class="declaredHomeLeft">    
    <div>        
        <span class="spnWelcome" style="color:#000; float:none; font-weight:normal; margin-left:20px;">Welcome back, <strong><%= userName%></strong></span>
        <h1 style="margin:0 0 25px 0">Most Rated Viewed Outfits:</h1>
        <%  foreach (ClosetOutfitView outfitView in Model.TopRatedOutfits)
            {
                Html.RenderPartial("Outfit", outfitView);
            } %>
    </div> 
    
    <div class="OutfitDiv" style="height:200px;">
        <div class="outfitContainer" style="width:100%; text-align;left;">
            <div class="outfitHeader" style="background-color:#DDE4EE;" >
                <span class="homeSubtitle">Recently Uploaded Garments</span>
            </div>
            <div>
                <% foreach (UserGarment userGarment in Model.RecentlyUploadedGarments)
                   { %>
                    <div class="recentlyUploaded">
                        <img src="<%= Resources.GetGarmentPath() + userGarment.ImageUri %>" alt="<%=userGarment.Title %>" /><br />
                        <span class="silouhetteDescription"><%= userGarment.Title%></span>                
                    </div>            
                <% } %>
                <a class="declaredLinks" href="<%= Url.RouteUrl(new { controller = "MyGarments", action= "Index"}) %>" style="margin-top:110px;">View Garments</a>
            </div>
        </div>
    </div>
</div>
<%}
   else
  {%>
<div class="declaredHomeLeft">
    <div>
        <h1>Welcome back, <strong><%= userName%></strong></h1>        
    </div>
    <div style="clear:both" ></div>
    <img src="/static/img/Home/registeredhome.png" style="float:left;"/>
    <div class="RegisteredHomeSelector">
        <div class="RegisteredHomeSelectorItem">
            <span style="font-size: 30px; ">Add</span><span style="font-weight: normal;font-size: 30px; "> Garments</span><div class="divButton" style=" vertical-align:super; margin-left: 10px;" ><a href="<%= Url.RouteUrl(new { controller = "GarmentSelector", action= "Index"}) %>" style="color: White; text-decoration: none">GO!</a></div><br />
            Add garments from our master closet.
        </div>
        <div class="RegisteredHomeSelectorItem">
            <span style="font-size: 30px; ">Upload</span><span style="font-weight: normal;font-size: 30px; "> Garments</span><div class="divButton" style=" vertical-align:super; margin-left: 10px;" ><a href="<%= Url.RouteUrl(new { controller = "UploadGarment", action= "Index"}) %>" style="color: White; text-decoration: none">GO!</a></div><br />
            Upload your own garment photos to your Closet.
        </div>
        <div class="RegisteredHomeSelectorItem">
            <span style="font-size: 30px; ">What</span><span style="font-weight: normal;font-size: 30px; ">  are other women wearing?</span><div class="divButton" style=" vertical-align:super; margin-left: 10px;" ><a href="<%= Url.RouteUrl(new { controller = "OtherOutfits", action= "Index"}) %>" style="color: White; text-decoration: none">GO!</a></div><br />
            Peak inside other women’s closets.
        </div>
        <div class="RegisteredHomeSelectorItem" style="border: none; margin-bottom: 10px;">
            <span style="font-size: 30px; ">Add</span><span style="font-weight: normal;font-size: 30px; "> Friends</span><div class="divButton" style=" vertical-align:super; margin-left: 10px;" ><a href="<%= Url.RouteUrl(new { controller = "Friend", action= "Index"}) %>" style="color: White; text-decoration: none">GO!</a></div><br />
            You dont have any contacts yet. Start Now
        </div>
    </div>
</div>
<%}%>

<div class="declaredHomeRight">
    <div class="GarmentSelector_FilterDiv_FashionFlavorSelected" style="clear:both; margin-left:auto; margin-right:auto;">
            <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Title">Your Fashion Flavors:</span>                
            <% foreach (FashionFlavor flavor in Model.FashionFlavors)
               {  %>        
            <div class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box">
                <center>
                    <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Title" ><%=  flavor.Name %></span>
                    <img class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Image" src="<%= Resources.GetFashionFlavorThumbnailPath(flavor) %>" alt="<%= flavor.Name %>"  />
                </center>
            </div>                
        <% } %>
        <div style="clear: both" ></div>
    </div>
    
    <% Html.RenderPartial("UpdaterTrends", Model.StyleAlerts); %>     
</div> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">    
    <script src='/static/js/jquery.rating.js' type="text/javascript" language="javascript"></script> 
    <script src='/static/js/jquery.MetaData.js' type="text/javascript" language="javascript"></script>
    
    <script type="text/javascript">
    $(document).ready(function() {
        $('input.myratingstar').rating({
                required: 'hide'
	        });
	    });
        
        
        <% if (TempData["message"] != null)
           { %>
            appendMessage($("#tempMessage"), '<%= TempData["message"] %>', "success");            
        <% } %>                
        
    </script>
    
</asp:Content>
