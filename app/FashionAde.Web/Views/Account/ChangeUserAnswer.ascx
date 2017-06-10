<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Web.Controllers.MVCInteraction.UserAnswer>" %>
<div id="div1" class="divContainer" style="width:95%;float:left; margin-bottom:20px; ">
<% using (Html.BeginForm("ChangeAnswer", "Account"))
    { %>
    <table style="float:left;">
        <tr>
            <td>Security Question</td>
            <td class="profileInfo"><%= Html.DropDownList("SecurityQuestion", ViewData["securityQuestions"] as List<SelectListItem>, new { title = "Please, select a security question", @class = "InputForValidate" })%> </td>
            <td class="tdAlerts"></td>
        </tr>
        <tr>
            <td>Security Answer</td>
            <td class="profileInfo"><%= Html.TextBox("SecurityAnswer", null, new { title = "Please, enter your security asnwer", @class = "InputForValidate" })%> </td>
            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.SecurityAnswer) %></td>
        </tr>
        <tr>
            <td>Password</td>
            <td class="profileInfo"><%= Html.Password("Password", null, new { title = "Please, enter your password to change your security question/asnwer", @class = "InputForValidate" })%></td>                
            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.Password) %></td>
        </tr>
    </table>
    <div class="centerDiv" style="width:215px;">
        <input type="image" src="/static/img/buttons/btn_save_changes.gif" alt="Save My Changes" />            
    </div>
    <% } %>
</div>
