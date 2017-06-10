<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<FriendsData>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>

<asp:Content ID="contentOverlay" ContentPlaceHolderID="OverlayPlaceHolder" runat="server">
    <div class="modal" id="RemoveConfirmation" style="opacity:1;"> 
        <img src='/static/img/close.png' class='close close_Image' />    
        <center>
            <p>Are you sure to remove this friend?</p> 
            <div id="btnRemove" class="divButton">Yes</div><div class="divButton close" >No</div>
        </center>
    </div> 
    <input type="hidden" id="userMail" value="<%= Model.UserMail %>" />
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Html.RenderPartial("FriendMaster", Model); %>
    
    <% using (Html.BeginForm("DeleteFriend", "Friend", FormMethod.Post, new { @id = "RemoveForm" }))
       {   %>
       <%= Html.Hidden("FriendId") %>
    <%} %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript" src="/static/js/jquery.validate.min.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
        $("#lnkFriends").css("background-color", "#F08331");

        $(".erasefriend").overlay({
            expose: {
                color: '#333',
                loadSpeed: 200,
                opacity: 0.3
            },
            target: "#RemoveConfirmation",
            left: "center",
            api: true,
            top: "center",
            onBeforeLoad: function() {
                $("#FriendId").val(parseInt(this.getTrigger()[0].id.split("_")[1]));
            }
        });

        jQuery.validator.addMethod("checkUserMail", function() {
            return ($("#userMail").val() != $("#FriendEmail").val());
        }, "You cannot use this email.");

        $("#AddManualFriend").validate({
            rules: {
                "FriendFirstName": { required: true },
                "FriendLastName": { required: true },
                "FriendEmail": { required: true, email: true, checkUserMail: "#FriendEmail" }
            }
        });

        $("#btnRemove").click(RemoveFriend);
    });
    
    function RemoveFriend() {
        $("#RemoveForm").submit();
    }
</script>
</asp:Content>