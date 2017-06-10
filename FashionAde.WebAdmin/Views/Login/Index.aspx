<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="FashionAde.WebAdmin.Controllers" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<h1>Administration Sign In</h1>
<div id="divLogin">
<% using (Html.BeginForm("Validate", "Login"))
           {%>   
        <table>
            <tr>
                <td><label for="">UserName*</label></td>
                <td><%=Html.TextBox("userName")%></td>
            </tr>
            <tr>
                <td><label for="">Password*</label></td>
                <td><%=Html.Password("userPassword")%></td>
            </tr>
            <tr>
                <td style="text-align: right;"><input type="checkbox" name="chkMaintain" value="true" /></td>
                <td>Remember me next time.</td>
            </tr>
        </table>
        <div style="margin-left: 100px;"><input type="image" src="/img/buttons/btn_login.gif" /></div>
<% }%>    

</div> 
        

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
</asp:Content>