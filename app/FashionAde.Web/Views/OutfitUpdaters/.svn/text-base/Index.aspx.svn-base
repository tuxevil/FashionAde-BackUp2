<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<List<OutfitUpdater>>" %>
<%@ Import Namespace="FashionAde.Core.ThirdParties"%>
<%@ Import Namespace="FashionAde.Core.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>
<%@ Import Namespace="FashionAde.Data.Repository"%>
<%@ Import Namespace="FashionAde.Core.FlavorSelection"%>
<%@ Import Namespace="FashionAde.Core.ContentManagement" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<asp:Content ID="contentOverlay" ContentPlaceHolderID="OverlayPlaceHolder" runat="server">
    <div class="modal" id="loading"> 
        <p>Loading... Please Wait</p> 
        <img src="http://l.yimg.com/a/i/us/per/gr/gp/rel_interstitial_loading.gif" />
    </div> 
 
    <input type="hidden" id="NoOne" />        
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<div class="parentDiv">
<% using (Html.BeginForm("ChangePage", "OutfitUpdaters"))
   { %>
<input id="Page" name="Page" type="hidden" />
<input id="Id" name="Id" type="hidden" value="<%= ViewData["Id"] %>" />
<%} %>
    <h1 id="H1" style="margin-top:15px;">Outfit Updaters</h1>
    <h2 style="display: inline; float: left; width:240px;">This outfit updaters match with your combination.</h2>
    <%
        IList<FashionFlavor> flavors = (List<FashionFlavor>)ViewData["FashionFlavors"];
        if (flavors != null)
        { %>
                  <div class="GarmentSelector_FilterDiv_FashionFlavorSelected" style="float: left; margin-left: 140px; margin-top: -40px; display: inline;">
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
        <% } %>
    <div style="clear: both; ">
        <div id="Div1" class="MyGarments_GarmentsDiv">
            <div>
                <div class="MyGarments_Panel" style="overflow-y:hidden;">
                    <% foreach (OutfitUpdater outfitUpdater in Model) { %>
                            <div class="Outfit_Result_Garment" style="margin: 5px 0px 15px 5px; height: 100px;">            
                                <a href="<%= outfitUpdater.BuyUrl %>" target="_blank">
                                    <img class="OutfitUpdaterImg" src="<%= outfitUpdater.ImageUrl %>" alt="<%= outfitUpdater.Name + " u$s" + outfitUpdater.Price %>" title="<%= outfitUpdater.Name + " u$s" + outfitUpdater.Price%>" />
                                </a><br />
                                <span><%= outfitUpdater.Partner.Name%></span><br />
                                <% if (outfitUpdater.Price != null)
                                   {%>
                                <span style="color: black; font-weight: normal;"><%= Convert.ToDecimal(outfitUpdater.Price).ToString("$#,##0.00")%></span>
                                <%} %>
                            </div>
                        <%} %>
                    <div style="clear:both"></div>
                </div>
            </div>
            <% if(ViewData["Pages"] != null) Html.RenderPartial("Paging", ViewData["Pages"]); %>
        </div>
    </div>     
</div> 
<div id="Div2" class="MyGarments_OutfitDiv" style="width: 280px; padding-top:15px;">
    <div class="MyGarments_Sponsors" style=" margin-bottom:5px;" >
        <span>Sponsored by:</span>
        <img src="/static/img/Sponsors/logo_lg.jpg" alt="Ann Taylor" />
    </div>
            
    <% Html.RenderPartial("UpdaterTrends", ViewData["styleAlerts"]); %> 
    
</div>        

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">
var apiOverlay;
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
    
        $(".Page").click(ChangePage);
        $(".Page").bind("mouseenter",function(e) {
            $(this).removeClass();
            $(this).addClass("NextPage");
        });
        $(".Page").bind("mouseleave", function(e) {
            $(this).removeClass();
            $(this).addClass("Page");
        });
    });
    
    function ChangePage(){
        apiOverlay.load();
        $("#Page").val($(this).attr("id").split('_')[1]); 
        document.forms[0].submit();
    }
</script>
</asp:Content>
