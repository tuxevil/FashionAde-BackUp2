<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Friend>" %>
<%@ Import Namespace="FashionAde.Core.Accounts"%>

<tr style="background-color:#EDEDED; border: solid 10px white; ">
    <td style="text-align: left;">
        <span style="font-weight: bold; margin-right: 10px; color: Black;" >
            <% if(!string.IsNullOrEmpty(Model.User.FirstName)){%>
            <%=Model.User.FirstName%>
            <%}
               else{%>
            N/D 
            <%}
               if (!string.IsNullOrEmpty(Model.User.LastName))
               {%>
            <%=Model.User.LastName%>
            <%}
               else{%>
            N/D 
            <%}%>
        </span>
    </td>
    <td style="text-align: right;"><div class="divButton" style=" background-color: #82A40C; padding: 2px 10px 2px 10px;" ><a href="<%= Url.RouteUrl(new { controller = "Friend", action= "Accept", friendId = Model.Id}) %>" style="color:White; text-decoration: none;">ACCEPT</a></div></td>
    <td><div class="divButton" style=" background-color: #C73825; padding: 2px 10px 2px 10px;" ><a href="<%= Url.RouteUrl(new { controller = "Friend", action= "Reject", friendId = Model.Id}) %>" style="color:White; text-decoration: none;">REJECT</a></div></td>
</tr>