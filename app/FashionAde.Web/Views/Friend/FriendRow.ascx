<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Friend>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="FashionAde.Core.Accounts"%>

<tr style="background-color:#EDEDED; border: solid 2px white; ">
    <td style="width: 20px;">
    <% if (Model.FriendProvider != null)
       { %>
    <img src="<%= Resources.GetFriendProviderPath(Model.FriendProvider.ImageUri) %>" alt="<%= Model.FriendProvider.Name %>" style="margin: 0px 5px 0px 5px;"/>
    <% } %>
    </td>
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
    <td style="text-align: left;"><span style="font-weight: normal; color: Black;"><%= Model.User.EmailAddress%></span></td>
    <td><% = Model.Status %></td>
    <td style="text-align: right;"><img src="/static/img/trash.png" id="<%= "frd_" + Model.Id %>" class="erasefriend" /></td>
</tr>