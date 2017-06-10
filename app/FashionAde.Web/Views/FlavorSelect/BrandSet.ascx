<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Core.FlavorSelection.BrandSet>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>
<div class="Quiz_Item_Box unselectedQuizItem" id="div_BS_<%= Model.Id %>">
    <div class="Quiz_Item_Box_Text" style="text-align:left; ">
    <%
        string[] brands = Model.Description.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string brand in brands)
        {%>
            <span style="color: Black; font-weight:normal; font-family: Century Gothic; font-size: 14px;"><%=brand %></span><br />
        <%}
        %>
    </div>
    <div class="Quiz_Item_Box_CheckBox"><%= Html.CheckBox("chb_BS_" + Model.Id) %></div>
</div>
