<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<FashionAde.WebAdmin.Controllers.CMS.EditorViewData>" %>
<%@ Import Namespace="FashionAde.Core" %>
<%@ Import Namespace="FashionAde.WebAdmin.Controllers" %>
<%@ Import Namespace="FashionAde.WebAdmin.Controllers.CMS" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">    
<% 
    using (Html.BeginForm("Save", "Editor", FormMethod.Post, new { id = "saveForm", name = "saveForm" }))
    {
       string title = string.Empty;
       bool create = (Model.Content.Id == 0);
   
       if (create) 
           title = "Create new Content";
       else 
           title = "Edit Content"; 
%>

    <%= Html.HiddenFor(m => m.Content.Id)%>
    
    <%= Html.HiddenFor(m => m.Content.Status)%>
           
    <%= Html.ValidationSummary() %>

    <h1 style="margin:5px 0 10px 0;"><%= title %></h1>
    <table>
        <tr>
            <td>Title</td>
            <td><%= Html.TextBoxFor(x => x.Content.Title) %><%= Html.ValidationMessageFor(x => x.Content.Title) %></td>
        </tr>        
        <tr>
            <td>Status</td>                        
            <td>
                <label name="status"><%= Model.Content.Status %></label>
            </td>        
        </tr>        
        <tr>
            <td>ContentType</td>
            <td><%= Html.DropDownListFor(c => c.Content.Type, new SelectList(Model.ContentTypes, "Value", "Text", (Model.Content.Type != null) ? Model.Content.Type.ToString() : ""))%></td>
        </tr>
        <tr>
            <td>Category</td>            
            <td><%= Html.DropDownListFor(m => m.Content.Category, new SelectList(Model.ContentCategories, "Id", "Name", (Model.Content.Category != null) ? Model.Content.Category.ToString() : ""), "Select...")%></td>
        </tr>
        <tr>
            <td>Promotional Text</td>
            <td><%= Html.TextAreaFor(c => c.Content.PromotionalText, new { style = "width:250px; height:70px;" })%></td>
        </tr>
        <tr>
            <td>Body</td>
            <td><%= Html.TextAreaFor(c => c.Content.Body, new { @class = "tinymce" })%></td>
        </tr>
        <tr>
            <td>Keywords</td>
            <td><%= Html.TextBoxFor(x => x.Content.Keywords) %></td>
        </tr>
        </table>
            
        <% 
            int i = 0;
            foreach (ContentViewSection cs in Model.Content.Sections)
            {
        %>
                <h1>Promobox <%= i %></h1>
                <table>
                <tr>
                <td>Title</td>
                <td><%= Html.TextBoxFor(m => m.Content.Sections[i].Title)%> <%= Html.ValidationMessageFor(x => x.Content.Sections[i].Title)%></td>
                </tr>
                <tr>
                <td>Body</td>
                <td><%= Html.TextAreaFor(m => m.Content.Sections[i].Body, new { @class = "tinymce_section" })%></td>
                </tr>
                <tr>
                <td>Fashion Flavor</td>
                <td><%= Html.DropDownListFor(m => m.Content.Sections[i].FashionFlavor, new SelectList(Model.Flavors, "Id", "Name", (cs.FashionFlavor != null) ? cs.FashionFlavor.Id.ToString() : ""), "Select...")%></td>
                </tr>
                </table>               
        <%
                i++;
            }
        %>
        
        <input type="submit" class="divButton" style="padding:2px 10px 2px 10px;" name="AddSection" value="AddSection"  onclick="javascript:submitSectionAdd()"/> 
        
        <p style="text-align:center">
                <input type="submit" class="divButton" style="padding:2px 10px 2px 10px;"  name="Save" value="Save" />
               <% if (Model.CanApprove) { %>
                    <input type="submit" class="divButton" style="padding:2px 10px 2px 10px;" name="Approve" value="Publish" onclick="javascript:submitApprove()" />
               <% } %>
               
               <% if (Model.CanAssign) { %>
                    <input type="submit" class="divButton" style="padding:2px 10px 2px 10px;" name="Approve" id="btnAssign" value="Send To" onclick="return false;" />
               <% } %>
               or <%=Html.ActionLink("Cancel", "Index", "Grid") %>
        </p>
<% } %>

<div class="modal" id="assignDialog" style="position:relative; opacity:1; width:250px; height:90px;">
    <center>
        <img src='/img/close.png' class='close close_Image' />
        <p>Please, select an user:</p>
        <%= Html.DropDownList("publisher", Model.Publishers, "Select...", new { title = "Select..." })%>        
        <br /><br />
        <div id="btnAssignToPublisher" class="divButton">Assign</div>
    </center>
</div>
           
</asp:Content>

<asp:Content ID="scriptsContent" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">    

    <%= Html.ClientSideValidation<ContentView>("Content") %>
    
    <% 
        for (int i = 0; i < Model.Content.Sections.Count; i++)
            Response.Write(Html.ClientSideValidation<ContentViewSection>(string.Format("Content.Sections[{0}]",i)));
    %>
        
<script type="text/javascript">
    var apiOverlay;
     
    function submitApprove() {
        var form = document.getElementById('saveForm');
        form.action = "/Editor/Approve/default.aspx";        
        form.submit();
    }

    function submitSectionAdd() {
        var form = document.getElementById('saveForm');
        form.action = "/Editor/AddSection/default.aspx";        
        form.submit();
    }
        
        $(document).ready(function() {
            $('.tinymce').tinymce({
                // Location of TinyMCE script
                script_url: '/js/tiny_mce.js',

                theme : "advanced",
		        plugins : "fullscreen,imagemanager,paste",
		        theme_advanced_buttons1_add_before : "fullscreen,insertimage,pastetext,pasteword,selectall",
		        relative_urls : false,
			    imagemanager_insert_template : '<img src="{$name}" />',

		        paste_create_paragraphs : false,
		        paste_create_linebreaks : false,
		        paste_use_dialog : true,
		        paste_auto_cleanup_on_paste : true,
		        paste_convert_middot_lists : false,
		        paste_unindented_list_class : "unindentedList",
		        paste_convert_headers_to_strong : true,
		        paste_insert_word_content_callback : "convertWord",
		        document_base_url : "http://images.fashion-ade.com/Content/",
			    remove_script_host : false
            });

	        function convertWord(type, content) {
		        return content;
	        }    

            $('.tinymce_section').tinymce({
                // Location of TinyMCE script
                script_url: '/js/tiny_mce.js',

                // General options
                theme: "advanced",
                plugins: "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

                // Theme options
                theme_advanced_resizing: false,
                theme_advanced_path: false,
                
                // Example content CSS (should be your site CSS)
                content_css: "/css/tinymce.custom.css"
            });
             
            $("#btnAssign").overlay({  
                expose: {
                    color: '#333',
                    loadSpeed: 200,
                    opacity: 0.3
                },
                target: "#assignDialog",
                left: 380,
                top: "center",
                api: true,
                closeOnClick: false,
                onBeforeLoad: function() {                    
                    apiOverlay = this;                
                }
            });    
            
            $("#btnAssignToPublisher").click(function(e){                    
                assignToPublisher();                 
            });

            $("#contentType").change(function() {
                var typeId = $.toJSON($(this)[0].value);
                                                                
                $.ajax({
                    type: "POST",
                    url: "<%= Url.RouteUrl(new { controller = "Editor", action= "ListCategoriesByType"}) %>",
                    data: typeId,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {                        
                        $("#category").empty();
                        $.each(data, function(index, data) {
                            $("#category").append("<option value='" + data.Id + "'>" + data.Name + "</option>");
                        });
                    }
                });
            }).change(); //making sure the event runs on initialization for default  
        });
        
        function assignToPublisher()
        {
            if($('#publisher')[0].selectedIndex == 0)
                return;
                 
            var assign = {
                 "ContentId": $('#Content_Id').val(),
                 "PublisherId": $('#publisher :selected').val()
             };
                     
            var encoded = $.toJSON(assign);
             
            $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "Editor", action= "Assign"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    apiOverlay.close();
                    window.location = "<%= Url.RouteUrl(new { controller = "Grid", action= "Index"}) %>";
                    //Redirect home page!                    
            }
        });                        
    }
</script>
</asp:Content>