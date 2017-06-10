<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Core.FlavorSelection.Wording>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>
<div class="Quiz_Item_Box unselectedQuizItem" id="div_W_<%= Model.Id %>">
    <div class="Quiz_Item_Box_Text" >
    <%
        string[] words = Model.Description.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in words)
        {%>
            <span style="color: Black; font-family: Century Gothic; font-size: 16px;"><%=word %></span><br />
        <%}
        %>
    </div>
    <div class="Quiz_Item_Box_CheckBox"><%= Html.CheckBox("chb_W_" + Model.Id) %></div>
</div>
