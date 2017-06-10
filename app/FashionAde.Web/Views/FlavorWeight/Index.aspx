<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FashionFlavor>>" %>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.Accounts"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">    
    <div id="buildYourCloset" >
        <div class="buildClosetTitle">
            <h1>What´s your Fashion Flavor?</h1>
            <h2>How important is each of these styles to you?<br />One style must be at least 60%.</h2>
        </div>
        
        <img id="imgBuildSteps" src="/static/img/BuildYourCloset/step1.gif" alt="Steps" />
        
        <div class="shadowUp"></div>
            <div class="styleContent">
                <div class="styleImportance" style="position:relative;">
                    <span class="sliderValues" style="top:30px; left:127px;">0%</span>
                    <span class="sliderValues" style="top:30px; left:363px;">100%</span>
                    
                    <div class="sliders" style="left: 160px;">
                        <div style="margin-bottom: 15px;">
                            <span id="spnFlavor1" class="sliderFlavor1"><%= Model[0].Name %> </span>
                            <span id="spnSlider1" class="sliderPercent">0</span>
                        </div> 
                        <div id="divSlider1" style="width:190px;background-color:#FDBF50;"></div>
                    </div>
                    
                    <span class="sliderValues" style="top:100px; left:127px;">0%</span>
                    <span class="sliderValues" style="top:100px; left:363px;">100%</span>
                    
                    <div class="sliders" style="top:70px; left:160px;">                
                        <div style="margin-bottom: 15px;">
                            <% if (Model.Count > 1) %>
                            <% { %>
                            <span id="spnFlavor2" class="sliderFlavor2"><%= Model[1].Name%> </span>
                            <span id="spnSlider2" class="sliderPercent">0</span>
                            <% } %>
                        </div> 
                        <div id="divSlider2" style="width:190px;background-color:#FDBF50;"></div>
                    </div>
                </div>
            </div>
        <div class="shadowDown"></div>
        
        <div class="divBuildBottom">
           <%  decimal fw1 = Convert.ToDecimal(50);
               decimal fw2 = Convert.ToDecimal(50);

               if (ViewData["UserFlavorSelected"] != null)
               {
                    List<UserFlavor> selectedUF = (List<UserFlavor>)ViewData["UserFlavorSelected"];
                    fw1 = (selectedUF[0].Weight != 100) ? selectedUF[0].Weight : fw1;
                    if (selectedUF.Count > 1)
                        fw2 = selectedUF[1].Weight;
               } %>
               
               
            
                <% using (Html.BeginForm("SetWeight", "FlavorWeight"))
                   { %>   
                <input type="hidden" id="Flavor1Weight" name="Flavor1Weight" value="<%= fw1 %>" />
                <input type="hidden" id="Flavor2Weight" name="Flavor2Weight" value="<%= fw2 %>" />
                
                <table style="width:100%;">
                    <tr>
                        <td align="right">                        
                            <input id="btnPrevious" type="image" src="/static/img/BuildYourCloset/button_previous2.gif" value="Previous" name="Previous" />
                        </td>
                        <td align="left"><input id="btnWhatsYourFlavor" type="image" src="/static/img/BuildYourCloset/button_next.gif" value="Next" name="Next" /></td>            
                    </tr>    
                </table>
                
            <% } %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">
    var quizSelectedCount = 0;
    var selectedItems = 0;

    $(document).ready(function() {
        $("#divSlider1").slider({
            min: 0,
            max: 100,
            step: 10,
            slide: function(event, ui) {
                if (ui.value == 100 || ui.value == 0)
                    return false;

                $("#spnSlider1").text(ui.value + '%');
                $("#Flavor1Weight").val(ui.value);
                $("#spnSlider2").text(100 - ui.value + '%');
                $("#Flavor2Weight").val(100 - ui.value);
                $("#divSlider2").slider('value', 100 - ui.value);
            }
        });

        $("#divSlider2").slider({
            min: 0,
            max: 100,
            step: 10,
            slide: function(event, ui) {
                if (ui.value == 100 || ui.value == 0)
                    return false;

                $("#spnSlider2").text(ui.value + '%');
                $("#Flavor2Weight").val(ui.value);
                $("#spnSlider1").text(100 - ui.value + '%');
                $("#Flavor1Weight").val(100 - ui.value);
                $("#divSlider1").slider('value', 100 - ui.value);
            }
        });

        $("#divSlider1").slider('option', 'value', $("#Flavor1Weight").val());
        $("#divSlider2").slider('option', 'value', $("#Flavor2Weight").val());

        $("#spnSlider1").text($("#divSlider1").slider("value") + '%');
        $("#spnSlider2").text($("#divSlider2").slider("value") + '%');

        $("#btnWhatsYourFlavor").click(function(e) {
            if ($("#divSlider2").slider("value") == 50) {
                e.preventDefault();
                showMessage("One style must be at least 60%.");
            }
        });
        
        $("#spnSlider2").text($("#divSlider2").slider("value") + '%');

        startSilouhettePreloading(2);
    });
</script>
</asp:Content>