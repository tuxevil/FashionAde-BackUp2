<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<UserRegistration>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<map name="shapes" id="shapes">    
    <area id="lnkSignIn" shape="rect" coords="92,43,156,59" href="<%= Url.RouteUrl(new { controller = "Login", action= "Index"}) %>" title="Sign In Here" alt="" />
</map>

<div class="modal" id="loading" style="width: 275px;"> 
    <p>Please wait, we are creating your outfits, this may take a few minutes..</p> 
    <center><img src="http://l.yimg.com/a/i/us/per/gr/gp/rel_interstitial_loading.gif" /></center>
</div> 
<input type="hidden" id="NoOne" />

<div class="parentDiv">
    <h1 style="float:left">Create Your Closet Account</h1><br /><br /><br />
    <h2 style="float:left; width: 450px;">Please complete the following information to build your closet. We will send you an email to validate your email address once completed.</h2>
    <img src="/static/img/BuildYourCloset/step3.gif" alt="" style="float:right; margin-right: 16px; display: inline; margin-top:-50px;" />         
    <img src="/static/img/BuildYourCloset/already_registered.gif" alt="Already registered" style="margin-rigth:50px; display:inline; margin-top: -50px; float:right;" usemap="#shapes"/>
    
    
    <div style="clear: both; ">
    <div class="tooltip"></div> 
        <div id="GarmentsDiv"> 
            <div id="divProfContainer" style="width:95%;float:left; margin-bottom:20px; ">
            
            <% Html.EnableClientValidation(); %>
            <% using (Html.BeginForm("Register", "Registration"))
                {%>
                    <table style="margin-left: auto; margin-right: auto;">
                        <tr>
                            <td style="text-align:right;">Valid Email Address*</td>
                            <td class="profileInfo"><%=Html.TextBox("Email", Model.Email, new { title = "Please, insert a valid email", @class = "InputForValidate", maxlength = 100 })%></td>
                            <td class="tdAlerts"><div id="EmailCheck"></div><%= Html.ValidationMessageFor(x => x.Email) %></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Confirm Email Address*</td>
                            <td class="profileInfo"><%=Html.TextBox("EmailConfirmation", Model.EmailConfirmation, new { title = "Please, confirm your email", @class = "InputForValidate", maxlength = 100 })%></td>
                            <td class="tdAlerts"><div id="EmailConfirmationCheck"></div><%= Html.ValidationMessageFor(x => x.EmailConfirmation) %></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">User Name*</td>
                            <td class="profileInfo"><%= Html.TextBox("UserName", Model.UserName, new { title = "Please, insert youre user name", @class = "InputForValidate", maxlength = 50 })%></td>
                            <td class="tdAlerts"><div id="UserNameCheck"></div><%= Html.ValidationMessageFor(x => x.UserName) %></td>
                        </tr>

                        <tr>
                            <td style="text-align:right;">Password*</td>
                            <td class="profileInfo"><%= Html.Password("Password", string.Empty, new { title = "Please, insert a password", @class = "InputForValidate", maxlength = 50 })%></td>
                            <td class="tdAlerts"><span style="color:green" id='result'></span> <%= Html.ValidationMessageFor(x => x.Password) %></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Confirm Password*</td>
                            <td class="profileInfo"><%= Html.Password("ConfirmPassword", string.Empty, new { title = "Please, re type your password", @class = "InputForValidate", maxlength = 50 })%></td>                
                            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.ConfirmPassword) %></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Security Question*</td>
                            <td class="profileInfo"><%= Html.DropDownList("SecurityQuestion", ViewData["securityQuestions"] as List<SelectListItem>, new { title = "Please, select a security question" })%> </td>
                            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.SecurityQuestion) %></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Security Answer*</td>
                            <td class="profileInfo"><%= Html.TextBox("SecurityAnswer", Model.SecurityAnswer, new { title = "Please, insert your security answer", @class = "InputForValidate", maxlength = 50 })%> </td>
                            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.SecurityAnswer) %></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Zip Code*</td>            
                            <td class="profileInfo"><%= Html.TextBox("ZipCode", Model.ZipCode, new { title = "Please, insert a zipcode", @class = "InputForValidate", maxlength = 6 })%></td>
                            <td class="tdAlerts"><div id="ZipCodeCheck"></div><%= Html.ValidationMessageFor(x => x.ZipCode) %></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Size Category*</td>            
                            <td class="profileInfo"><%= Html.DropDownList("UserSize", ViewData["UserSizes"] as List<SelectListItem>, new { title = "Please, select a user size" })%> </td>
                            <td class="tdAlerts"></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Invitation Code*</td>            
                            <td class="profileInfo"><%= Html.TextBox("InvitationCode", Model.InvitationCode, new { title = "Please insert a invitation code provided.", @class = "InputForValidate", maxlength = 50 })%></td>
                            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.InvitationCode) %></td>
                        </tr>                        
                    </table>
                    <div style="width:490px; float:left;">
                        <div style="width:550px; margin-left:auto; margin-right: auto; border-top: dotted 1px gray">
                        <p style="font-size:11px; display: block; width: 425px; margin-left:auto; margin-right: auto; margin-top: 5px; color: #475F8F; font-family: Verdana; ">
                        <%= Html.CheckBox("TermOfUse", new { @class = "check" })%> I have read the <a style="color: #475F8F; display:inline; text-decoration: underline;" href="<%= Url.RouteUrl(new { controller = "Content", action= "TermsOfUse"}) %>">Terms of Use</a> and agree to comply with these terms.<br /><%= Html.ValidationMessageFor(x => x.TermOfUse) %>
                        <span style="margin-left: 15px; color: Black; font-weight:normal;">We care about your privacy - review our <a style="color: Black; text-decoration: underline; display:inline;" href="<%= Url.RouteUrl(new { controller = "Content", action= "Privacy"}) %>" target="_blank">privacy policy.</a></span>                        
                        </p>
                        </div>                        
                    </div>
                    <span class="field-validation-error" id="termsOfUseCheck" style="float:left; margin-top:10px; display:none;">You must accept to continue.</span>
                </div>
                <div style="width: 180px; margin-left: 200px;">
                                                
                <input id="btnRegister" type="image" src="/static/img/BuildYourCloset/button_build_your_closet.gif" value="Build Your Closet" title="Finish the form"/>
                </div>
                <% } %>
        </div>
    </div>
</div> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript" src="/static/js/MicrosoftAjax.js"></script>
<script type="text/javascript" src="/static/js/MicrosoftMvcValidation.js"></script>
<script type="text/javascript" src="/static/js/jquery.simplePassMeter-0.2.min.js"></script>
<script type="text/javascript">
    var apiOverlay;
    var emailvalid = true;
    var uservalid = true;
    var zipcodevalid = true;    
    
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
    
        $("#Email").blur(CheckEmail);
        $("#UserName").blur(CheckUsername);
        $("#ZipCode").blur(CheckZipCode);
        $(".simplePassMeter").hide();
        
        $("#TermOfUse").click(function(e){ 
            if($(this).is(":checked"))
                $("#termsOfUseCheck").hide();
        });
        
        $("form").submit(function(){ 
            if(!$("#TermOfUse").is(":checked"))
            {
                $("#termsOfUseCheck").show();
                return false;
            }
            
            if(!isValidPassword() || !isValidConfirmation())
                return false;            
        });
        
        $('#Password').simplePassMeter({
            'requirements': {
                'noMatchField': {
                    'value': '#UserName'
                }                
            }
        });        
        
        $('#Password').focus(function(){ 
            $("#Password_simplePassMeter").show();
        });
        
        $('#Password').blur(function(){         
            if(isValidPassword())
                $("#Password_simplePassMeter").hide();            
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
                    'value': '#Password'
                }
            }
        });
        
    });
    
    function isValidPassword() {
       return !$("#Password_simplePassMeter").hasClass("meterFail");
    }
    
    
    function isValidConfirmation() {
       return  !$("#ConfirmPassword_simplePassMeter").hasClass("meterFail");
    }
    
    function OpenModal(){
        apiOverlay.load();
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
            emailvalid = true;
            if (emailvalid && uservalid && zipcodevalid)
                $("#btnRegister").removeAttr("disabled");
        }
        else {
            $("#EmailCheck").empty();
            if (data.RegExError == false)
                {
                    $("#EmailCheck").append("<span class='field-validation-error'>" + data.Email + " is in use</span>");
                    $("#EmailCheck").show();
                }
                
            $("#btnRegister").attr("disabled", "disabled");
            emailvalid = false;
        }
    }

    function CheckUsername() {
        temp = $(this).val().toString();

        if (temp != "") {
            var encoded = $.toJSON(temp);

            $.ajax({
                type: "POST",
                url: "<%= Url.RouteUrl(new { controller = "AccountValidation", action= "CheckUsername"}) %>",
                data: encoded,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    ShowUsernameMessage(data);
                }
            });
        }
        else {
            $("#UserNameCheck").empty();
            $("#UserNameCheck").hide();
        }
    }

    function ShowUsernameMessage(data) {
        if (data.Exist == false) {
            if (data.Reserved == false) {
                $("#UserNameCheck").empty();
                $("#UserNameCheck").hide();
                uservalid = true;
                if (emailvalid && uservalid && zipcodevalid)
                    $("#btnRegister").removeAttr("disabled");
            }
            else {
                $("#UserNameCheck").empty();
                $("#UserNameCheck").append("<span class='field-validation-error'>The username " + data.Username + " is reserved.</span>")
                $("#UserNameCheck").show();
                $("#btnRegister").attr("disabled", "disabled");
                uservalid = false;
            }
        }
        else {
            $("#UserNameCheck").empty();
            $("#UserNameCheck").append("<span class='field-validation-error'>The username " + data.Username + " is not available but <a id='UserRecommended' style='color: #0A466E; font-weight: bold; display: inline;' href='#" + data.Recommended + "'>" + data.Recommended + "</a> is.</span>")
            $("#UserNameCheck").show();
            $("#btnRegister").attr("disabled", "disabled");
            $("#UserRecommended").click(function(e) {
                e.preventDefault();
                $("#UserName").val(data.Recommended);
                $("#UserName").blur();
            })
            uservalid = false;
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
                contentType: "application/json; charset=utf-8",
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
            zipcodevalid = true;
            if (emailvalid && uservalid && zipcodevalid)
                $("#btnRegister").removeAttr("disabled");
        }
        else {
            $("#ZipCodeCheck").empty();
            $("#ZipCodeCheck").append("<span class='field-validation-error'>The zip code " + data.ZipCode + " is not available</span>")
            $("#ZipCodeCheck").show();
            $("#btnRegister").attr("disabled", "disabled");
            zipcodevalid = false;
        }
    }
    
</script>
</asp:Content>