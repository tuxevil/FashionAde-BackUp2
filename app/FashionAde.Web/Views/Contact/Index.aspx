<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<IList<IContact>>" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>
<%@ Import Namespace="ContactProvider"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<div style="width: 900px; margin-left: auto; margin-right: auto;">
    <span class="ContactList_Title">Contacts</span><br /><br />
    <div class="Contact_LeftBarContainer" >
        <div class="ContactList_Header">
            <h2 style="display: inline; margin-left: 10px;">Imported contacts from your </h2><img src="<%= Resources.GetFriendProviderPath(ViewData["providerImg"].ToString()) %>" alt="<%= ViewData["providerName"] %>" />
            <span class="ContactList_Header_Item">Select:</span>
            <ul id="ulCheck" class="actions" >
                <li id="MarkedAll"><a id="markAll" class="link" href="#Actions" style="color: #4E6398; display: inline;" >All</a></li>
                <li> , </li>
                <li id="UnMarkedAll"><a id="unmarkAll" class="link" style="color: #4E6398;">None</a></li>
            </ul>
        </div>
        <div class="Contact_ContactContainer">
            <table width="100%" style="margin: 0px 0px 0px 0px;">
            <%
                foreach (IContact c in Model)
                {
            %>
                <tr style="background-color:#EDEDED; border: solid 2px white;">
                    <td><%= Html.CheckBox("chb_" + c.Index, false, new { @class = "contact", title = "Check for Add" })%></td>
                    <td><span style="font-weight: normal; font-size: 11px;"><%= c.Email%></span></td>
                    <td><span style="font-weight: bold; margin-right: 10px; font-size: 11px;" ><%= c.FirstName%> <%= c.LastName%></span></td>
                </tr>            
            <%
                }
            %>
            </table>
        </div>
        <div style="clear: both" ></div>
    </div>

    <div style="width:45%; margin-left: 20px; float: left;">

        <% using (Html.BeginForm("AddContacts", "Contact"))
           {
               Response.Write(Html.Hidden("provider", ViewData["provider"]));       
        %>
           
        <input id="TotalCount" type="hidden" value="<%= ViewData["TotalCount"] %>" />
        <input id="selectedAll" name="selectedAll" type="hidden" value="false"/>
        <input id="selectedIndexs" name="selectedIndexs" type="hidden" value=""/>
        <div style="float: left; width: 100%; margin-top: 5px; padding-top:15px;">
            <span>Include a personal note (optional):</span><br /><br />
            <textarea id="emailmessage" name="emailmessage" rows="14" style="background-color:#E1E5F1; width: 100% " ></textarea><br />
            <center><input type="submit" class="divButton" value="ADD FRIENDS" style="margin-top: 10px; margin-bottom: 10px;" /></center>
        </div>
        <%} %>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
    <script type="text/javascript">
        var items = new Array();
        var allSelected = false;
        var selected = 0;
        var total = $("#TotalCount").val();

        $(document).ready(function() {
            $(".Page").click(ChangePage);
            $(".Page").mousein
            $(".Page").bind("mouseenter", function(e) {
                $(this).removeClass();
                $(this).addClass("NextPage");
            });
            $(".Page").bind("mouseleave", function(e) {
                $(this).removeClass();
                $(this).addClass("Page");
            });

            $("#markAll").click(function() {
                allSelected = true;
                items = new Array();
                $(".contact").each(function(i) { this.checked = true; });

                updateInputHiddenFields();
            });

            $("#unmarkAll").click(unMarkAllClick);

            updateInputHiddenFields();
            $(".contact").live("click", checkboxClick);
        });
        function ChangePage() {
            window.location = '<%= Url.RouteUrl(new { controller = "Contact", action= "Index"}) %>?page=' + $(this).attr("id").split('_')[1];
        };

        //** Checkboxs functions and Items Count**
        function unMarkAllClick() {
            allSelected = false;
            items = new Array();
            $(".contact").each(function(i) { this.checked = false; });
            updateInputHiddenFields();
        }

        function checkbox_state_add(id) {
            checkbox_state(id, true);
            $("input[type='checkbox'][value='" + id + "']").attr("checked", true);
        }

        function checkbox_state_remove(id) {
            checkbox_state(id, false);
            $("input[type='checkbox'][value='" + id + "']").attr("checked", false);
        }

        function checkbox_state(id, add) {
            var value = id;
            var itemIndex = jQuery.inArray(value, items);

            if (allSelected) {
                // When allSelected is marked, the items list include the excluded items.
                if (!(add) && itemIndex == -1)
                    items.push(value);
                else if (add && itemIndex != -1) {
                    items = jQuery.grep(items, function(currentItem) {
                        return currentItem != value;
                    });
                }
            }
            else {
                // Otherwise works as expected, we maintain the list of items selected.
                if (add && itemIndex == -1)
                    items.push(value);
                else if (!(add) && itemIndex != -1) {
                    items = jQuery.grep(items, function(currentItem) {
                        return currentItem != value;
                    });
                }
            }

            updateInputHiddenFields();
        }

        function checkboxClick() {
            checkbox_state($(this).attr("id").split('_')[1], $(this).is(":checked"));
        }

        function updateInputHiddenFields() {
            $("#selectedIndexs").val(items.join(","));
            $("#selectedAll").val(allSelected);

            if (allSelected == false) {
                selected = items.length;
            }
            else {
                selected = total - items.length;
            }
            
            PressUnpress();
        }

        function PressUnpress() {
            if (selected > 0) {
                $("#MarkedAll").removeClass();
                $("#UnMarkedAll").removeClass();
            }
            else {
                $("#MarkedAll").removeClass();
                $("#UnMarkedAll").addClass("pressed");
            }

            if (selected == total) {
                $("#MarkedAll").addClass("pressed");
                $("#UnMarkedAll").removeClass();
            }
        }
    </script>
</asp:Content>