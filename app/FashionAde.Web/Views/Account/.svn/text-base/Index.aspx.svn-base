<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<UserFull>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Web.Controllers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<input type="hidden" id="Alert" name="Alert" value="<%= Model.Alert %>" />
<input type="hidden" id="Tab" name="Tab" value="<%= Model.Tab %>" />



<h1 style="display:block; float:left; margin:0 25px 0 0;">Modify Your Closet Profile</h1>
<div id="tabs" class="tabs">
    <ul class="accountTabs">    
        <li class="accountTab"><a id="lnkProfile" class="tablink" href="#tabProfile">Change Profile</a></li> 
	    <li class="accountTab"><a id="lnkMail" class="tablink" href="#tabMail">Change Email</a></li> 
	    <li class="accountTab"><a id="lnkPassword" class="tablink" href="#tabPassword">Change Password</a></li> 
	    <li class="accountTab"><a id="lnkAnswer" class="tablink" href="#tabAnswer">Change Question</a></li> 
    </ul>
    <% Html.EnableClientValidation(); %>
    <div class="accountTabPanes">     
        <div id="tabProfile" class="profile" >
             <% Html.RenderPartial("ChangeUserInfo", new UserUpdate(Model)); %>
        </div>   

        <div id="tabMail" class="profile" >
             <% Html.RenderPartial("ChangeUserMail", new UserEmail(Model)); %>
        </div>    

        <div id="tabPassword" class="profile" >
             <% Html.RenderPartial("ChangeUserPassword", new UserPassword(Model)); %>
        </div>
        
        <div id="tabAnswer" class="profile" >
             <% Html.RenderPartial("ChangeUserAnswer", new UserAnswer(Model)); %>
        </div>
    </div> 
</div>
<div class="tooltip"></div> 

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">

<script type="text/javascript" src="/static/js/MicrosoftAjax.js"></script>
<script type="text/javascript" src="/static/js/MicrosoftMvcValidation.js"></script>
<script type="text/javascript" src="/static/js/jquery.simplePassMeter-0.2.min.js"></script>
<script type="text/javascript">
var tab = 0;
    $(document).ready(function() {
    
        <% if (ViewData["Errors"] != null)
           { %>
            showMessage('<%= ((string[])ViewData["Errors"])[0] %>');
        <% } %>
        
        if ($("#Tab").val() > 0)
            tab = parseInt($("#Tab").val());

        if ($("#Alert").val() != "")
            showMessage($("#Alert").val());

        $("#Email").blur(CheckEmail);
        $("#ZipCode").blur(CheckZipCode);
        
        configureTabs();  
                
        $(".simplePassMeter").hide();
        
        $("#frmChangePassword").submit(function(){ 
            if(!isValidPassword() || !isValidConfirmation())
                return false;            
        });   
        
        $('#NewPassword').simplePassMeter({
            'requirements': {
                'noMatchField': {
                    'value': '#OldPassword'
                }                
            }
        });        
        
        $('#NewPassword').focus(function(){ 
            $("#NewPassword_simplePassMeter").show();
        });
        
        $('#NewPassword').blur(function(){         
            if(isValidPassword())
                $("#NewPassword_simplePassMeter").hide();            
        });
                
        $('#ConfirmPassword').focus(function(){ 
            $("#ConfirmPassword_simplePassMeter").show();
        });
        
        $('#ConfirmPassword').blur(function(){         
            if(isValidConfirmation())
                $("#ConfirmPassword_simplePassMeter").hide();            
        });
                
        $('#ConfirmPassword').simplePassMeter({
           'requirements': {
                'matchField': {
                    'value': '#NewPassword'
                }
            }
        });   
    });
    
    function isValidPassword() {
       return !$("#NewPassword_simplePassMeter").hasClass("meterFail");
    }
    
    
    function isValidConfirmation() {
       return  !$("#ConfirmPassword_simplePassMeter").hasClass("meterFail");
    }    
    
    function configureTabs(){
        var tabs = $(".accountTabs > li");
        var panes = $(".accountTabPanes > div");
        
        for(var i = 0; i < panes.length; i++)
            if(i != tab)
                    $(panes[i]).hide(); //Oculto todos menos el primero
            else  $(tabs[i]).addClass("tabSelected");
                
        $(".accountTabs").click(function(e){            
            var src = getSourceElement(e);
            
            if($(src).attr("className") != "tablink")
                src = $(src).children();  //Si hizo click en el li, traigo el vinculo
                            
            var selectedTab = $(src).parent();
            var selectedPane = $(src).attr("href");
            
            $(".accountTabs > li").removeClass("tabSelected");
            $(selectedTab).addClass("tabSelected");                        
            
            $(panes).hide();
            $(selectedPane).show();
        });
    }
    
    function CheckEmail() {
        temp = $(this).val().toString();

        if (temp != "") {
            var encoded = $.toJSON(temp);

            $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "AccountValidation", action= "CheckEmail"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    ShowEmailMessage(data);
                }
            });
        }
        else {
            $("#EmailCheck").empty();
            $("#EmailCheck").hide();
        }
    }

    function ShowEmailMessage(data) {
        if (data.Exist == false) {
            $("#EmailCheck").empty();
            $("#EmailCheck").hide();    
            $("#btnEmail").removeAttr("disabled");
        }
        else {
            $("#EmailCheck").empty();
            if (data.RegExError == false)
                {
                    $("#EmailCheck").append("<span class='field-validation-error'>" + data.Email + " is in use</span>");
                    $("#EmailCheck").show();
                }
            $("#btnEmail").attr("disabled", "disabled");
        }
    }

    function CheckZipCode() {
        temp = $(this).val().toString();

        if (temp != "") {
            var encoded = $.toJSON(temp);

            $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "AccountValidation", action= "CheckZipCode"}) %>",
                data: encoded,
                contentType: "application/json",
                dataType: "json",
                success: function(data) {
                    ShowZipCodeMessage(data);
                }
            });
        }
        else {
            $("#ZipCodeCheck").empty();
        }
    }

    function ShowZipCodeMessage(data) {
        if (data.Exist == true) {
            $("#ZipCodeCheck").empty();
            $("#btnSaveInfo").removeAttr("disabled");
        }
        else {
            $("#ZipCodeCheck").empty();
            $("#ZipCodeCheck").append("<span class='field-validation-error'>The zip code " + data.ZipCode + " is not available</span>")
            $("#ZipCodeCheck").show();
            $("#btnSaveInfo").attr("disabled", "disabled");
        }
    }
        
</script>
</asp:Content>