<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.WebAdmin.Controllers" %>
<%@ Import Namespace="FashionAde.Web.Common" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 <h1 style="margin:0 0 10px 0;">Content List</h1>

<% if (RolePermissionService.CanCreate()) { %>
    <p style="text-align:right;"><%= Html.ActionLink("Create New Content", "Create", "Editor", null, new { @class = "createContent" })%></p> 
<% } %> 

<% using (Html.BeginForm("Search", "Grid")) { %>     
<fieldset style="border:solid 1px #CCCCCC;">
<legend style="margin-left:8px;">Filters</legend>
 <table>
    <tr>
        <td>Id / Title</td>
        <td><%= Html.TextBox("searchText", ViewData["searchText"])%></td>
        <td>Content Type</td>
        <td><%= Html.DropDownList("contentType", ViewData["contentTypes"] as List<SelectListItem>, "Select..." )%></td>
        <td><input id="search" type="submit" class="seeMannequin divButton" style="padding:2px 10px 2px 10px; margin:0 0 3px 5px;" value="Search" /></td>
        <td><input id="clear"  type="submit" class="seeMannequin divButton" style="padding:2px 10px 2px 10px; margin:0 0 3px 5px;" value="Clear" /></td>
    </tr>
    <tr>
        <td>Category</td>
        <td><%= Html.DropDownList("category", ViewData["contentCategories"] as List<SelectListItem>, "Select..." )%></td>        
    </tr>    
 </table>
 </fieldset>
  
<% } %> 
 
 <% Html.Grid<FashionAde.Core.ContentManagement.Content>("contents").Columns(column =>
    {
        column.For(cont => Html.ActionLink("Review", "Edit", "Editor", new { id = cont.Id }, null)).DoNotEncode().Attributes(style => "text-align:center").CellCondition(x => RolePermissionService.CanAccess(x));
        column.For(cont => cont.Id);
        column.For(cont => cont.Status).Named("Work Status");
        column.For(cont => cont.Title);

        column.For(cont => (cont.ScheduleFrom.HasValue)
                            ? cont.ScheduleFrom.Value.ToString("MM/dd/yyyy")
                            : "N/D").Named("Schedule From");
        column.For(cont => (cont.ScheduleTo.HasValue)
                            ? cont.ScheduleTo.Value.ToString("MM/dd/yyyy")
                            : "N/D").Named("Schedule To");

        column.For(cont => cont.Type);
        column.For(cont => cont.Category.Description).Named("Category");
        column.For(cont => (cont.LastContentPublished != null) ? cont.LastContentPublished.Status.ToString() : "").Named("Publish Status");
        
        column.For(cont =>
            Html.ActionLink("Delete", "Delete", "Editor", null, new { id = "del_" + cont.Id, @class = "Delete" })).DoNotEncode().Attributes(style => "text-align:center").CellCondition(x => RolePermissionService.CanDelete(x));

        column.For(cont =>
            Html.ActionLink("Disable", "Disable", "Editor", null, new { id = "dis_" + cont.Id, @class = "Disable" })).DoNotEncode().Attributes(style => "text-align:center").CellCondition(x => RolePermissionService.CanDisable(x));

        column.For(cont =>
            Html.ActionLink("Enable", "Enable", "Editor", null, new { id = "dis_" + cont.Id, @class = "Enable" })).DoNotEncode().Attributes(style => "text-align:center").CellCondition(x => RolePermissionService.CanEnable(x));

        column.For(cont => 
            Html.ActionLink("Schedule", "Schedule", "Editor", new { id = cont.Id }, new { id = "sch_" + cont.Id, @class = "Schedule" })).DoNotEncode().Attributes(style => "text-align:center").CellCondition(x => RolePermissionService.CanSchedule(x));
      
      })
     .RowStart(row => (row.Item.LastContentPublished != null) ? "<tr class='published'>" : "<tr>")
     .Attributes(@class => "grid")
     .Empty("No records found.")
     .Render();
     
      %>
    
   
    <%= Html.Pager(Model as MvcContrib.Pagination.IPagination) %>
    
    
    <div class="modal" id="deleteDialog" style="position:relative; opacity:1;">
        <center>
            <img src='/img/close.png' class='close close_Image' />
            <p>Are you sure to remove this item?</p> 
            <div id="btnDelete" class="divButton" >Yes </div><div class="divButton close" >No</div>
        </center>
    </div>
    
    <div class="modal" id="disableDialog" style="position:relative; opacity:1;">
        <center>
            <img src='/img/close.png' class='close close_Image' />
            <p>Are you sure to disable this item?</p> 
            <div id="btnDisable" class="divButton" >Yes </div><div class="divButton close" >No</div>
        </center>
    </div>

    <div class="modal" id="enableDialog" style="position:relative; opacity:1;">
        <center>
            <img src='/img/close.png' class='close close_Image' />
            <p>Are you sure to enable this item?</p> 
            <div id="btnEnable" class="divButton" >Yes </div><div class="divButton close" >No</div>
        </center>
    </div>
    
    <div class="modal" id="scheduleDialog" style="position:relative; width:320px;">        
        <img src='/img/close.png' class='close close_Image' />
        <table border="0" cellpadding="0" cellspacing="0" style="text-align:center">
            <tr>
                <td>From</td>
                <td><input id="txtFrom" type="text" /></td>
            </tr>
            <tr>
                <td>To</td>
                <td><input id="txtTo" type="text" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="btnSchedule" class="divButton">Save</div>
                </td>
            </tr>
        </table>
    </div>
    
    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript" src="<%= Url.Content("~/js/jquery.toJSON.js") %>"></script>

<script type="text/javascript">
    var deleteId;
    var scheduleId;
    var disabledId;
    var enabledId;
    var apiOverlay;
    
    $(document).ready(function() {
        $("#clear").click(function(e){            
            $("#searchText").val("");
            $("#category")[0].selectedIndex = 0;
            $("#contentType")[0].selectedIndex = 0;
        });
    
        $(".Schedule").overlay({  
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#scheduleDialog",
            left: 300,
            top: "center",
            api: true,
            closeOnClick: false,
            onBeforeLoad: function() {
                apiOverlay = this;
                scheduleId  = getContentId(this.getTrigger()[0].id);
            }
        });        
    
        $(".Schedule").click(function(e) {
            return false;
        });        
        
        $(".Delete").overlay({  
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#deleteDialog",
            left: 380,
            top: "center",
            api: true,
            closeOnClick: false,
            onBeforeLoad: function() {
                apiOverlay = this;
                deleteId  = getContentId(this.getTrigger()[0].id);
            }
        });
        
        $(".Delete").click(function(e) {       
            return false;
        });
        
        $(".Disable").overlay({  
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#disableDialog",
            left: 380,
            top: "center",
            api: true,
            closeOnClick: false,
            onBeforeLoad: function() {
                apiOverlay = this;
                disabledId  = getContentId(this.getTrigger()[0].id);
            }
        });

        $(".Enable").overlay({  
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#enableDialog",
            left: 380,
            top: "center",
            api: true,
            closeOnClick: false,
            onBeforeLoad: function() {
                apiOverlay = this;
                enabledId  = getContentId(this.getTrigger()[0].id);
            }
        });
        
        $(".Disable").click(function(e) {       
            return false;
        });        

        $(".Enable").click(function(e) {       
            return false;
        });        

        $("#btnDelete").click(function(e){                    
            deleteContent(e); 
        });
        
        $("#btnSchedule").click(function(e){                    
            scheduleContent(e); 
        });
        
         $("#btnDisable").click(function(e){                    
            disableContent(e); 
        });

         $("#btnEnable").click(function(e){                    
            enableContent(e); 
        });
        
        $("#txtFrom").datepicker( { dateFormat: 'mm/dd/yy' } );
        $("#txtTo").datepicker( { dateFormat: 'mm/dd/yy' } );
    });
    
    function getSourceElement(event) {
        return event.srcelement ? event.srcelement : event.target;
    }

    function getContentId(linkId) {
        return linkId.toString().split("_")[1];
    }    

    function deleteContent() {         
        var encoded = $.toJSON(deleteId);                
        $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "Editor", action= "Delete"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    apiOverlay.close();
                    window.location = document.location.href;
            }
        });        
    }
    
    function disableContent() {         
        var encoded = $.toJSON(disabledId);                
        $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "Editor", action= "Disable"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    apiOverlay.close();
                    window.location = document.location.href;
            }
        });        
    }

    function enableContent() {         
        var encoded = $.toJSON(enabledId);                
        $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "Editor", action= "Enable"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    apiOverlay.close();
                    window.location = document.location.href;
            }
        });        
    }
    
    function scheduleContent(){
    
        var schedule = {
             "ContentId": scheduleId,
             "From": $("#txtFrom").val(),
             "To": $("#txtTo").val()
         };
                          
        var encoded = $.toJSON(schedule);
        $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "Editor", action= "Schedule"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    apiOverlay.close();
                    window.location = document.location.href;
            }
        });        
    }
    
</script> 
</asp:Content>




