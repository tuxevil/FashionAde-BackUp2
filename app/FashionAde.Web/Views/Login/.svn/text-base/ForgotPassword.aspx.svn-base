<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<ForgotPasswordView>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>Forgot your password?</h1>
    
    <% Html.EnableClientValidation(); %>
    
    <% 
        using (Html.BeginForm()) {
    %>
    
    <center>
    <fieldset>
    <div id="divUserName">
    <label>User Name:</label>
    <%= Html.TextBoxFor(x => x.UserName, new { maxlength = 50 })%>
    <% = Html.ValidationMessageFor(x => x.UserName) %>
    </div>
    <div>
    <label>Secret question:</label>
    <span id="Question" class="readonly">Enter your user name...</span>
    </div>
    <div>
    <label>Your Answer:</label>
    <%= Html.TextBoxFor(x => x.Answer, new { maxlength = 50 })%>
    <% = Html.ValidationMessageFor(x => x.Answer)%>
    </div>
    </fieldset>
    </center>
    
    <input id="SendAnswer" type="image" src="/static/img/LogIn/btnSend.jpg" value="Finish" style="margin:5px auto 5px auto; display:block;" />     

    <% } %>


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript" src="/static/js/MicrosoftAjax.js"></script>
<script type="text/javascript" src="/static/js/MicrosoftMvcValidation.js"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $('#UserName').blur(GetQuestion);
        $('#UserName').focus();
    });

    function GetQuestion() {
        $("#divUserName span.field-validation-error").remove();
        
        temp = $("#UserName").val().toString();
        
        if (temp == '') 
            return;

        var encoded = $.toJSON(temp);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "Login", action= "GetQuestion"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {                
                if(data.Exist == true){
                    ShowQuestion(data.PasswordQuestion);                    
                }
                else
                {     
                    $("#divUserName span.field-validation-error").remove();
                    var $span = $("<span>").addClass("field-validation-error").text("That username doesn't exist.");
                    $("#divUserName").append($span);
                }
            }
        });
    }

    function ShowQuestion(data) {
        $("#Question").text(data).css("color", "black");;
        $("#Answer").focus();
    }

</script>
</asp:Content>