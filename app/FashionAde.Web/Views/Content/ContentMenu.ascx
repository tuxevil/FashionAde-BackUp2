<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Core.FlavorSelection.BrandSet>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<h1 style="display:block; float:left; margin-right:15px;">Style Alerts - <%= SeasonHelper.CurrentSeason.ToString() + " " + DateTime.Today.Year %></h1>

<div id="btnTrendAlerts" class="divButton categoryButton">
    <a href="<%= Url.RouteUrl(new { controller = "Content", action= "TrendAlerts"}) %>">TREND ALERTS</a>
</div>
<div id="btnOrganizing" class="divButton categoryButton">
    <a href="<%= Url.RouteUrl(new { controller = "Content", action= "Organizing"}) %>">ORGANIZING</a>
</div>
<div id="btnGarmentCare" class="divButton categoryButton">
    <a href="<%= Url.RouteUrl(new { controller = "Content", action= "GarmentCare"}) %>">GARMENT CARE</a>
</div>
<br style="clear:both" />
<br style="clear:both" />