<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<FashionAde.Core.FashionFlavor>" %>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<%
        bool check = false;
        string ffclass = "fashionFlavorSelect unselectedItem";
        
        if(ViewData["FashionFlavorSelected"] != null)
        {
            List<FashionFlavor> selectedFF = (List<FashionFlavor>) ViewData["FashionFlavorSelected"];
            foreach (FashionFlavor fashionFlavor in selectedFF)
                if (fashionFlavor.Id == Model.Id)
                {
                    check = true;
                    ffclass = "fashionFlavorSelect selectedItem";
                }
        }
%>

<div id="div_FF_<%= Model.Id%>" class="<%= ffclass %>">
    <div><img src="<%= Resources.GetFashionFlavorPath(Model) %>" alt="<%= Model.Name%>" width="94" height="113" /></div>
    <div><%= Model.Name%></div>
    
    <div style="padding-top:5px;"><%= Html.CheckBox("chb_FF_" + Model.Id, check) %></div>
</div>
