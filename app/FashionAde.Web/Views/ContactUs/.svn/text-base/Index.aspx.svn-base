<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<FeedBack>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<% Html.EnableClientValidation(); %>
    <div style="width: 710px; float: left; margin-right: 5px;">
        <h1>Contact Us</h1>
        <h2>Stay in contact with us, send us your feedback!</h2>   
        <% using (Html.BeginForm())
           { %>
        <div style="width:400px;">
        <table>
            <tr>
                <td style="white-space: nowrap;">Your Email:</td><td><input type="text" id="Email" name="Email" maxlength="100" /></td><td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.Email)%></td>
            </tr>
            <tr>
                <td>Name:</td><td><input type="text" id"Name" name="Name" maxlength="100" /></td><td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.Name)%></td>
            </tr>
            <tr>
                <td>Message:</td><td><textarea id="Message" name="Message" cols="35" rows="6" ></textarea></td><td class="tdAlerts"><%= Html.ValidationMessageFor(x => x.Message)%></td>
            </tr>
        </table>
        
        <center><input type="submit" value="Send Now" class="divButton"/></center>
        </div>
        <%} %>
    </div>
    <div style="float: left; width:226px;">
     <% Html.RenderPartial("UpdaterTrends", Model.Content); %> 
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript" src="/static/js/MicrosoftAjax.js"></script>
<script type="text/javascript" src="/static/js/MicrosoftMvcValidation.js"></script>
</asp:Content>