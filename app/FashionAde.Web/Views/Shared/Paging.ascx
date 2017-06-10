<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Pager>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>

<% if(Model.Pages.Count > 0 ){%>
<div style="float:right; left: -50%; position: relative;">
    <div style="position: relative; left: 50%;">
        <span style="float:left;">Pages </span>
        <ul style="display: inline; float: left; margin-top: 0px;">
        <%
            foreach (SelectListItem item in Model.Pages)
            {
                if (!item.Selected)
                {
                    %><li class="Page" id="<%= "page_" + item.Value %>"><%=item.Text%></li><%
                }
                else
                {
                    %><li class="SelectedPage"><%=item.Text%></li><%
                }
            } %>
        </ul>
        <span style="float:left;"><%= Model.TotalPages %></span>
    </div>
</div><br />
<% } %>