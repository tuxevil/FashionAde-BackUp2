<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Core.EventType>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>
<%@ Import Namespace="FashionAde.Core" %>

<%
    bool check = false;
    string className = "eventTypeSelect";

    if (ViewData["EventTypeSelected"] != null)
    {
        List<EventType> selectedET = (List<EventType>)ViewData["EventTypeSelected"];
        foreach (EventType eventType in selectedET)
        if (eventType.Id == Model.Id)
        {
            check = true;
            className = "eventTypeSelect selectedItem";
        }
    }
%>

<div class="<%= className %>" id="div_EV<%= Model.Id %>" >
    <div><img style="height:108px;" src="<%= Resources.GetEventTypePath(Model) %>" alt="<%= Model.Description %>" width="55" height="108" /></div>
    <div><%= Model.Description %></div>
    <div><%= Html.CheckBox("chb_ET_" + Model.Id, check)%></div>
</div>
