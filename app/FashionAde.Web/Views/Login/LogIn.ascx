<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="errorMessage"></div>
<h1>Sign In to your Closet Account</h1>
<h2 style="color:#000;">Don&#8217;t have an account yet? Create one <a href="<%= Url.RouteUrl(new { controller = "FlavorSelect", action= "Index"}) %>" style="color: #F38333; display:inline;" >here</a></h2>    
<div style="float:left; margin-top:5px;">
    <div id="divLogin">
        <% using (Html.BeginForm("Validate", "Login"))
           {%>   
        <table>
            <tr>
                <td><label for="">UserName*</label></td>
                <td><%=Html.TextBox("userName", string.Empty, new { maxlength = 50 })%></td>
            </tr>
            <tr>
                <td><label for="">Password*</label></td>
                <td><%=Html.Password("userPassword", string.Empty, new { maxlength = 50 })%></td>
            </tr>
            <tr>
                <td style="text-align: right;"><input type="checkbox" name="chkMaintain" value="true" /></td>
                <td>Remember me next time.</td>
            </tr>            
        </table>        
        <div style="margin-left: 100px;"><input type="image" src="/static/img/buttons/btn_login.gif" /></div>
        <% }%>     
    </div>
</div>
  
<div id="divRememberPass">
    <img src="/static/img/img_forgot_pass.gif" alt="" style="float:left;" />
    <a id="btnForgotPassword" href="<%= Url.RouteUrl(new { controller = "Login", action= "ForgotPassword"}) %>" >
        <img id="btnForgotPass" src="/static/img/buttons/btn_forgot_pass.gif" alt="Forgot Password?" />
    </a>
</div>