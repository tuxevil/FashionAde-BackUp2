<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Web.Controllers.MVCInteraction.UserUpdate>" %>
<div id="divProfContainer" class="divContainer" style="width:95%;float:left; margin-bottom:20px; ">
    <% using (Html.BeginForm("ChangeInfo", "Account"))
        { %>
        <table style="float:left;">
            <tr>
                <td>Valid Email Address</td>
                <td class="profileInfo" colspan="2"><%= Model.Email %></td>
            </tr>
            <tr>
                <td>First Name</td>            
                <td class="profileInfo"><%= Html.TextBox("FirstName", Model.FirstName, new { title = "Please, insert your first name", @class = "InputForValidate" })%> </td>
                <td class="tdAlerts"></td>
            </tr>
            <tr>
                <td>Last Name</td>
                <td class="profileInfo"><%= Html.TextBox("LastName", Model.LastName, new { title = "Please, insert your last name", @class = "InputForValidate" })%> </td>
                <td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.LastName) %></td>
            </tr>
            <tr>
                <td>Zip Code</td>
                <td class="profileInfo"><%= Html.TextBox("ZipCode", Model.ZipCode, new { title = "Please, insert a zipcode", @class = "InputForValidate" })%> </td>
                <td class="tdAlerts"><div id="ZipCodeCheck"></div><%= Html.ValidationMessageFor(x => x.ZipCode) %></td>
            </tr>
            <tr>
                <td>Size Category</td>            
                <td class="profileInfo"><%= Html.DropDownList("UserSize", ViewData["UserSizes"] as List<SelectListItem>, new { title = "Please, select a user size", @class = "InputForValidate" })%>  </td>
                <td class="tdAlerts"></td>
            </tr>
        </table>
        <div class="divInfo" >
            <div class="divInfoHeader" style="margin-top:0;">Closet Privacy Status</div>
            <%
           foreach (SelectListItem listItem in (List<SelectListItem>)ViewData["PrivacyStatus"])
            {%>
                <%=Html.RadioButton("PrivacyStatus", listItem.Value, listItem.Selected) + " " + listItem.Text%> <br />
            <%} %>
            
        </div>
        <div class="centerDiv" style="width:215px;">
            <input id="btnSaveInfo" type="image" src="/static/img/buttons/btn_save_changes.gif" alt="Save My Changes" />            
        </div>
        <% } %>           
    </div>