<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="divInviteFriends" class="" style="width:99%;float:left; margin-bottom:20px; ">
    <table style="text-align: left;">
        <tr>
            <td><span style="color:#000; margin-bottom:15px; display:block;">To:</span></td>
            <td>
                <input type="text" id="txtEmails" style="width: 545px; padding-left:3px;" title="Enter email addresses, separated by commas" /><br />
                <span style="font-size: 9px; color: Gray;">Enter email addresses, separated by commas</span> 
            </td>
        </tr>
        <tr>
            <td colspan="2"><span style="color:#000;">Enter your message here (optional):</span></td>
        </tr>
        <tr>
            <td colspan="2"><textarea id="txtMessage" style="width: 580px; height: 100px; background-color:#E1E5F1;" title="Enter your message here (optional)" ></textarea></td>
        </tr>
        <tr>
            <td colspan="2" style="border-top:2px dotted #B2B2B2; border-bottom:2px dotted #B2B2B2;">
                <input type="checkbox" id="SendToMe" style="margin-right: 5px;" title="Send me a copy of this message" /><span style="font-size: 9px;">Send me a copy of this message</span>                
            </td>            
        </tr>
    </table>
</div>