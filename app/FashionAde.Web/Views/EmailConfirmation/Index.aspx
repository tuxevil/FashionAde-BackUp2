<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h1>Email Account Validation</h1>

<div class="parentDiv">
    <div class="divContainer" style="width: 670px; float: left; padding:20px 0 40px 20px; margin-bottom:20px;">
        <span style="font-size:16px;">We have sent a confirmation email to your inbox.</span>
        <h3 style="margin:0;">You can click on the link on it or copy and paste your registration code in this box:</h3>
        <br />
                
        <% using (Html.BeginForm("Validate", "EmailConfirmation")){ %>
            <%= Html.Hidden("userid", Request.QueryString["userid"]) %>
            <div class="divSubContainer" style="width:300px; float:left;">
                <span>Code: </span> <%= Html.TextBox("code")  %>                
            </div>            
            <input type="submit" class="divButton" value="Confirm" style="float:left; margin:15px 0 0 10px;" />
        <%} %>
        
        <div id="errorMessage" style="clear:both; margin:0 auto 0 auto; width:350px; padding-top:10px;"></div>        
    </div>
        
    
    <div id="FiltersDiv" class="GarmentSelector_FiltersDiv">
    
    </div> 
</div> 
</asp:Content>

<asp:Content ID="contentScripts" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">
    $(document).ready(function() {        
        <% if (Convert.ToBoolean(ViewData["invalidCode"])) { %>
            appendMessage($("#errorMessage"), "Your code is not valid! Please try again.", "error");
        <% } %>
    });
</script>
</asp:Content>
