<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<FriendRatingInvitation>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core.UserCloset"%>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>Rate <%= Model.User.FullName %>'s Go-To outfit</h1>

    <div class="Outfit_Sponsors" style="margin-top: -35px;" >
        <span>Sponsored by:</span>
        <img src="/static/img/Sponsors/logo_lg.jpg" alt="Ann Taylor"  />
    </div>
    
    <% Html.RenderPartial("FriendRate", new FriendRatingHelper(Model.Outfit, Model.KeyCode));%>
    
    <input type="hidden" id="User" value="<%= Model.User.FullName %>" />
    
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
    <script src='/static/js/jquery.rating.js' type="text/javascript" language="javascript"></script> 
    <script src='/static/js/jquery.MetaData.js' type="text/javascript" language="javascript"></script>
<script type="text/javascript">
    $(document).ready(function() {

        $('input.friendratingstar').rating({
            required: 'hide'
        });
        $('#User1').text($('#User').val());
        $('#User2').text($('#User').val());
    });
</script>
</asp:Content>