<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<BetaInvitation>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<span class="Title">WELCOME <strong><%= ViewData["userName"] %></strong></span>
<h1>During this beta process you can invite 5 friends to build a closet.</h1>

<%  Html.EnableClientValidation();
    using (Html.BeginForm()) { %>
        <table width="100%">
            <tr>
                <td align="right">
                    <div class="Account_RightBarContainer" style="width:300px; text-align:center; margin-top:10px;">
                        <div class="Account_RightBarContainer_Title" style="margin-top: 0px;">
                            <img src="/static/img/FriendProviders/mail.JPG"alt="By Mail" style="margin-right: 5px;" />
                            Enter their emails now!
                        </div>                
                            <table cellpadding="0" cellspacing="0">
                            <% for (int i = 0; i < Model.Emails.Length; i++) { %>                        
                                <tr>
                                    <td style="text-align:left; padding:5px 0 5px 10px;">
                                        <span>Friend <%= i + 1 %></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding:0 0 0 10px;">
                                    <%
                                        Response.Write(Html.TextBoxFor(x => Model.Emails[i].EmailAddress, new { style = "width: 270px;" }));
                                        Response.Write(Html.ValidationMessageFor(x => Model.Emails[i].EmailAddress, null, new { style = "width:auto; margin-top:3px;" }));
                                    %>
                                    </td>
                                </tr>
                            <% } %>
                            </table>                    
                    </div>
                </td>
                <td align="left" style="padding:15px 0 0 20px; vertical-align:top;">
                    <span>Include a personal note(optional):</span>
                    <%
                        Response.Write(Html.TextAreaFor(x => Model.Comments, new { @class = "invitationOptionalNote" }));
                        Response.Write(Html.ValidationSummary());
                    %>                
                    <input type="image" name="submit" value="Invite Contacts" src="/static/img/Home/btnSendInvitation.png" style="margin:0 0 0 90px" />
                </td>
            </tr>
        </table>
<% } %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript" src="/static/js/MicrosoftAjax.js"></script>
<script type="text/javascript" src="/static/js/MicrosoftMvcValidation.js"></script>
</asp:Content>