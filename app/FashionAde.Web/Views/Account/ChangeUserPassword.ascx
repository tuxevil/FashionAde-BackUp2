<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Web.Controllers.MVCInteraction.UserPassword>" %>
<div id="div2" class="divContainer" style="width:95%;float:left; margin-bottom:20px; ">
<% using (Html.BeginForm("ChangePassword", "Account", FormMethod.Post, new { id = "frmChangePassword" }))
    { %>
    <table style="float:left;">
        <tr>
            <td>Enter your password</td>
            <td class="profileInfo"><%= Html.Password("OldPassword", string.Empty, new { @class = "InputForValidate" })%> </td>
            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.OldPassword) %></td>
        </tr>
        <tr>
            <td>Enter your new password</td>
            <td class="profileInfo"><%= Html.Password("NewPassword", string.Empty, new { @class = "InputForValidate" })%> </td>
            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.NewPassword) %></td>
        </tr>
        <tr>
            <td>Confirm your new password</td>
            <td class="profileInfo"><%= Html.Password("ConfirmPassword", string.Empty, new { @class = "InputForValidate" })%> </td>
            <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.ConfirmPassword) %></td>
        </tr>
    </table>
    <div class="centerDiv" style="width:215px;">
        <input type="image" src="/static/img/buttons/btn_save_changes.gif" alt="Save My Changes" />            
    </div>
    <% } %>  
</div>
     
