<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Web.Controllers.MVCInteraction.UserEmail>" %>
<div id="div1" class="divContainer" style="width:95%;float:left; margin-bottom:20px; ">
<% using (Html.BeginForm("ChangeEmail", "Account"))
    { %>
    <table style="float:left;">
        <tr>
            <td>Valid Email Address</td>
            <td class="profileInfo"><%= Model.Email %></td>
        </tr>
        <tr>
            <td>Enter your new email</td>
            <td class="profileInfo"><%= Html.TextBox("Email", string.Empty, new { title = "Please, enter your new email", @class = "InputForValidate" })%></td>
            <td class="tdAlerts"><div id="EmailCheck"></div><%= Html.ValidationMessageFor(x => x.Email) %></td>
        </tr>
    </table>
    <div class="centerDiv" style="width:215px;">
        <input id="btnEmail" type="image" src="/static/img/buttons/btn_save_changes.gif" alt="Save My Changes" />            
    </div>
    <% } %>
</div>
