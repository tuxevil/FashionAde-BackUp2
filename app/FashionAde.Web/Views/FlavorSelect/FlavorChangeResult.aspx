<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.FlavorSelection"%>
<%@ Import Namespace="FashionAde.Data.Repository"%>
<%@ Import Namespace="FashionAde.Core.DataInterfaces"%>
<%@ Import Namespace="FashionAde.Web.Controllers" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">    
    
        <% if ((bool)ViewData["flavorsChanged"])
           { %>
            <h1 style="margin:0;">Flavors Changed</h1>
            <div style="margin-left:20px;">
                <h2>You have changed your flavors Succesfully!</h2><br />
                <p>Your closet was updated with new combination outfits. If you want to see them, please go to <%= Html.ActionLink("My Outfits", "Index", "MyOutfits" ) %> </p>
            </div>
        <% }
           else
           { %>
            <h1 style="margin:0;">Flavors not changed...</h1>
            <div style="margin-left:20px;">
                <h2>We cannot change your flavors!</h2><br />
                <p>We can't create any combinations with these garments</p>
                <p>Please <%= Html.ActionLink("change them", "Index", "FlavorSelect")  %> to other or update your <%=Html.ActionLink("garments", "Selector", "GarmentSelector")  %> and try again!</p>
            </div> 
        <% } %>    
    
</asp:Content>