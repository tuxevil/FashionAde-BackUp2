<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.Core.Clothing"%>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>Upload images of your own clothes</h1>
    <h2 style="float: left; width: 275px;">Get <a href="<%= Url.RouteUrl(new { controller = "Content", action= "Recommendations"}) %>" style=" color:#3C3A3B; display: inline; text-decoration: underline" >recommendations</a> for taking photos of your personal garments.</h2>
    <%
        IList<FashionFlavor> flavors = (List<FashionFlavor>)ViewData["FashionFlavors"];
    %>
    <div class="GarmentSelector_FilterDiv_FashionFlavorSelected" style="display: inline; float: right; margin-right: 10px; margin-top: -10px; ">
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
    <br /><br />    
        
        
    <% using (Html.BeginForm("UploadFile", "UploadGarment", FormMethod.Post, new { enctype = "multipart/form-data" }))
    { %>     
    <div id="MyGarments_Panel1" class="MyGarments_Panel" style="height:auto; overflow:auto;">
        <div id="divUpload1" class="upload">
            <span>Add Image</span> Click "browse" to select an Image. <br />                
            <div class="fileinputs">
            <input type="file" id="file1" name="file1" class="file" />            
            <div class="fakefile">
                <input id="Text1" type="text" />
                <img src="/static/img/browse.jpg" />
            </div>
            </div>
        </div>        
        <div id="divUploadInfo1" class="uploadInfo" style="display:none;">            
            <img id="uploadImage1" class="uploadImage" />
            <span id="uploadName1" class="uploadName"></span>            
            <img id="uploadRemove1" src="/static/img/MyGarments/remove.jpg" class="uploadRemove" />
            <div class="fileErrorContainer">
                <label for="file1" generated="false" class="labelError error"></label>
            </div>
            <br style="clear:both;" />&nbsp;&nbsp;Tell us about this garment so we can put it in the right place in your closet:<br />
                        
            <table class="uploadTable">
                <tr>
                    <td style="text-align: right;">Garment Title:*</td>
                    <td class="uploadTitle"><%= Html.DropDownList("Title1", (List<SelectListItem>)ViewData["Titles"], "Select...")%></td>
                </tr>
                <tr>
                    <td style="text-align: right;">The pattern is:*</td>
                    <td class="uploadPattern"><%= Html.DropDownList("Pattern1", (List<SelectListItem>)ViewData["Patterns"], "Select...")%></td>
                </tr>
                <tr>
                    <td style="text-align: right;">The fabric is:*</td>
                    <td class="uploadFabric"><%= Html.DropDownList("Fabric1", (List<SelectListItem>)ViewData["Fabrics"], "Select...")%></td>
                </tr>
                <tr>
                    <td style="text-align: right;">The primary color of this garment is:*</td>
                    <td class="uploadPrimaryColor"><%= Html.DropDownList("PrimaryColor1", (List<SelectListItem>)ViewData["Colors"], "Select...")%></td>
                </tr>                
                <tr>
                    <td style="text-align: right;">I primarily wear this garment in:*</td>
                    <td class="uploadSeason"><%= Html.DropDownList("Season1", (List<SelectListItem>)ViewData["Seasons"], "Select...")%></td>
                </tr>
                <tr>
                    <td style="text-align: right;">I primarily wear this garment to:*</td>
                    <td class="uploadEventType"><%= Html.DropDownList("EventType1", (List<SelectListItem>)ViewData["EventTypes"], "Select...")%></td>
                </tr>
                <tr>
                    <td style="text-align: right;">This garmet should be stored in:</td>
                    <td style="font-size: 10px;" class="uploadPrivacy">
                        <% =Html.RadioButton("PrivacyStatus1", false, false)%> My Private Closet - only I will see it <br />
                        <% =Html.RadioButton("PrivacyStatus1", true, true)%> The Public Closet - anyone with my style type can see it                    
                    </td>
                </tr>
            </table>            
        </div> 
    </div>
    
    <input id="btnUpload" type="image" src="/static/img/MyGarments/img_upload_garments.jpg" class="uploadButton"  />        
    <img id="imgLoading" src="/static/img/MyGarments/Ajax-loader.gif" class="uploadLoading" /> 
    
    <% } %>  
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script src="/static/js/jquery.validate.min.js" type="text/javascript"></script>
<script type="text/javascript">
    var filesCount = 1;
    var maxFiles = 5;

    $(document).ready(function() {
        $("#lnkAddGarments").css("background-color", "#F08331");
        
        $(".file").change(function(e) {
            $("#uploadInfo").focus();
            $("#divUpload" + filesCount).hide();            
            $("#divUploadInfo" + filesCount).show();

            var src = getSourceElement(e);
            var filename = /([^\\]+)$/.exec(src.value)[1];
            var ext = filename.split('.').pop();
            $("#uploadName" + filesCount).text(filename);
            $("#uploadImage" + filesCount).attr("src", getFileIcon(ext));

            if ($(".MyGarments_Panel").length < maxFiles)
                cloneUploadPanel();
        });

        $(".uploadRemove").click(function(e) {
            var showNewPanel = !$(".upload").is(':visible');
            if (showNewPanel) cloneUploadPanel();
            $(this.parentNode.parentNode).remove();
        });

        $("#btnUpload").click(function(e) {
            if (!$(".uploadInfo").is(":visible"))
                e.preventDefault();

            if ($(".uploadInfo").is(":visible") && $("form").validate().checkForm())
                $("#imgLoading").show();
        });

        jQuery.validator.addMethod("selectNone", function(value, element) {
            return (!$(element).is(":visible")) ? true : !$(element.firstChild).is(":selected");
        }, "Field required.");

        //TODO: Try to make the rules dynamic...        
        $("form").validate({
            rules: {
                "file1": { accept: 'jpg|jpeg|gif|png' },
                "file2": { accept: 'jpg|jpeg|gif|png' },
                "file3": { accept: 'jpg|jpeg|gif|png' },
                "file4": { accept: 'jpg|jpeg|gif|png' },
                "file5": { accept: 'jpg|jpeg|gif|png' },

                "Title1": { selectNone: "#Title1:visible" },
                "Title2": { selectNone: "#Title2:visible" },
                "Title3": { selectNone: "#Title3:visible" },
                "Title4": { selectNone: "#Title4:visible" },
                "Title5": { selectNone: "#Title5:visible" },

                "Pattern1": { selectNone: "#Pattern1:visible" },
                "Pattern2": { selectNone: "#Pattern2:visible" },
                "Pattern3": { selectNone: "#Pattern3:visible" },
                "Pattern4": { selectNone: "#Pattern4:visible" },
                "Pattern5": { selectNone: "#Pattern5:visible" },

                "Fabric1": { selectNone: "#Fabric1:visible" },
                "Fabric2": { selectNone: "#Fabric2:visible" },
                "Fabric3": { selectNone: "#Fabric3:visible" },
                "Fabric4": { selectNone: "#Fabric4:visible" },
                "Fabric5": { selectNone: "#Fabric5:visible" },

                "PrimaryColor1": { selectNone: "#PrimaryColor1:visible" },
                "PrimaryColor2": { selectNone: "#PrimaryColor2:visible" },
                "PrimaryColor3": { selectNone: "#PrimaryColor3:visible" },
                "PrimaryColor4": { selectNone: "#PrimaryColor4:visible" },
                "PrimaryColor5": { selectNone: "#PrimaryColor5:visible" },

                "Season1": { selectNone: "#Season1:visible" },
                "Season2": { selectNone: "#Season2:visible" },
                "Season3": { selectNone: "#Season3:visible" },
                "Season4": { selectNone: "#Season4:visible" },
                "Season5": { selectNone: "#Season5:visible" },


                "EventType1": { selectNone: "#EventType1:visible" },
                "EventType2": { selectNone: "#EventType2:visible" },
                "EventType3": { selectNone: "#EventType3:visible" },
                "EventType4": { selectNone: "#EventType4:visible" },
                "EventType5": { selectNone: "#EventType5:visible" }
            },
            messages: {
                "Title1": { required: "Field required." },
                "Title2": { required: "Field required." },
                "Title3": { required: "Field required." },
                "Title4": { required: "Field required." },
                "Title5": { required: "Field required." }
            }
        });

    });

    function cloneUploadPanel() {
        var cloned = $("#MyGarments_Panel" + filesCount).clone(true);
        cloned.find(".upload").show();
        cloned.find(".uploadInfo").hide();

        //Update cloned Ids
        var nextId = filesCount + 1;
        cloned.attr("id", "MyGarments_Panel" + nextId);
        cloned.find(".labelError").attr("for", "file" + nextId);        
        cloned.find(".file").attr("id", "file" + nextId);
        cloned.find(".file").attr("name", "file" + nextId);
        cloned.find(".upload").attr("id", "divUpload" + nextId);
        cloned.find(".uploadName").attr("id", "uploadName" + nextId);
        cloned.find(".uploadImage").attr("id", "uploadImage" + nextId);
        cloned.find(".uploadInfo").attr("id", "divUploadInfo" + nextId);
        cloned.find(".uploadTitle").find("select").attr("id", "Title" + nextId);
        cloned.find(".uploadTitle").find("select").attr("name", "Title" + nextId);
        cloned.find(".uploadPattern").find("select").attr("id", "Pattern" + nextId);
        cloned.find(".uploadPattern").find("select").attr("name", "Pattern" + nextId);
        cloned.find(".uploadFabric").find("select").attr("id", "Fabric" + nextId);
        cloned.find(".uploadFabric").find("select").attr("name", "Fabric" + nextId);
        cloned.find(".uploadPrimaryColor").find("select").attr("id", "PrimaryColor" + nextId);
        cloned.find(".uploadPrimaryColor").find("select").attr("name", "PrimaryColor" + nextId);
        cloned.find(".uploadSeason").find("select").attr("id", "Season" + nextId);
        cloned.find(".uploadSeason").find("select").attr("name", "Season" + nextId);
        cloned.find(".uploadEventType").find("select").attr("id", "EventType" + nextId);
        cloned.find(".uploadEventType").find("select").attr("name", "EventType" + nextId);
        cloned.find(".uploadPrivacy").find("input[type='radio']").attr("id", "PrivacyStatus" + nextId);
        cloned.find(".uploadPrivacy").find("input[type='radio']").attr("name", "PrivacyStatus" + nextId);
        
        //Update DOM
        cloned.insertAfter($("#MyGarments_Panel" + filesCount));
        filesCount++;
    }

    function getFileIcon(ext) {
        ext = ext.toLowerCase();
        switch (ext) {
            case "gif":
                return "/static/img/UploadIcons/icon_GIF_big.png";
                
            case "jpg":
                return "/static/img/UploadIcons/icon_JPG_big.png";

            case "jpeg":
                return "/static/img/UploadIcons/icon_JPG_big.png";
                
            case "png":
                return "/static/img/UploadIcons/icon_PNG_big.png";

            default:
                return "/static/img/UploadIcons/icon_Generic_big.png";
        }
    }
        
</script>
</asp:Content>