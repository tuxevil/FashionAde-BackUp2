<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FashionAde.Core.FashionFlavor>>" %>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.FlavorSelection"%>
<%@ Import Namespace="FashionAde.Data.Repository"%>
<%@ Import Namespace="FashionAde.Core.DataInterfaces"%>
<%@ Import Namespace="FashionAde.Web.Controllers" %>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<div id="buildYourCloset" >

    <% bool updateFlavors = (bool)ViewData["updateFlavors"]; %>

    <div class="buildClosetTitle">
        <% if(updateFlavors) 
           { %>
            <h1>Change your Fashion Flavors</h1>
            <h2>Choose any <strong>two</strong> of the silhouettes below that best describe your personal style.</h2>
        <% } else {  %>
            <h1>What´s your Fashion Flavor?</h1>
            <h2>Choose any two of the silhouettes below that best describe your personal style.</h2>
        <% } %>
    </div>
    
    <% if (!updateFlavors)
       { %>
        <img id="imgBuildSteps" src="/static/img/BuildYourCloset/step1.gif" alt="Steps" />
    <% } %>   
    
<div style="width:100%;">
<div class="fashionFlavors flavorLeft" >        
<span class="fashionFlavorTitle">Which type or types are most like you?</span>
    <div id="divFlavorSelect" class="flavorContainer">        
    
        <% using (Html.BeginForm("SelectFashionFlavor", "FlavorSelect" ))
           {
               if (Model != null)
                   foreach (var fashionFlavor in Model)
                   {
                       Html.RenderPartial("FashionFlavor", fashionFlavor);
                   } %>       
    </div>    
    <input id="btnBuildNext" type="image" src="/static/img/BuildYourCloset/button_next.gif" value="Next" style="clear:both; " />
     <% } %>     
</div>

<div class="fashionFlavors flavorRigth">
    <span class="fashionFlavorTitle">Not Sure?</span>
    <div class="flavorContainer"  style="float:left;">
        <a class="startQuiz" shape="rect"  href="#QuizStep1" title="Take our quiz to pinpoint your style" alt="">
            <img id="imgQuiz"  src="/static/img/BuildYourCloset/img_take_our_quiz.jpg"  alt="Take Our Quiz!" class="startQuizButtonBanner" />
        </a> 
    </div>
    <a class="startQuiz" shape="rect"  href="#QuizStep1" title="Take our quiz to pinpoint your style" alt="" style="display:inline;" >
        <img id="imgStartQuiz"  src="/static/img/BuildYourCloset/button_next.gif" alt="Take Our Quiz!" class="startQuizButton" style="" />    
    </a>
</div>

</div>
<br style="clear:both;" /><br style="clear:both;" />

</div>

<div id="divQuizAll">     
    <div id="QuizStep1" class="quizPopup">            
        <div class="quizHeader">            
            <img src="/static/img/quiz_logo.jpg" class="quizLogo" alt="" style="" />
            <span class="quizTitle">WELCOME TO THE QUIZ</span>
            <span class="quizSubTitle">Choose the photo that best represent the clothes you actually wear each day:</span>
        </div>
        <div class="quizContent">
            <div class="quizContentItems">
                <%  int i = 0;
                    List<StylePhotograph> stylePhotographs = (List<StylePhotograph>) ViewData["StylePhotograph"];
                    if(stylePhotographs != null)
                        foreach (var stylePhotograph in stylePhotographs)
                        {
                            if (i == 4) %> 
            </div>
            <div class="quizContentItems"> 
                <%              Html.RenderPartial("StylePhotograph", stylePhotograph);
                            i++;
                        } %>
            </div>
        </div>
                    
        <div class="quizFooter">
            <img src="/static/img/BuildYourCloset/progress_step1.gif" alt="Step 1" class="quizProgress" />            
            <input id="lnkQuizStep2" type="image" href="#QuizStep2" src="/static/img/BuildYourCloset/button_next.gif" value="Next" style="float:right" />
        </div>        
    </div>    

    <div id="QuizStep2" class="quizPopup" >
        <div class="quizHeader">
            <img src="/static/img/quiz_logo.jpg" class="quizLogo" alt="" style="" />
            <span class="quizSubTitle">Which set of stores best reflects where you like to shop?</span>
        </div>

        <div class="quizContent">
            <div class="quizContentItems">
                <%  int j = 0;
                    List<BrandSet> brandSets = (List<BrandSet>) ViewData["BrandSet"];
                    if(brandSets != null)
                        foreach (var brandSet in brandSets)
                        {
                            if (j == 4) %> 
                </div>
                <div class="Quiz_Content_Item2"> 
                <%              Html.RenderPartial("BrandSet", brandSet);
                            j++;
                        } %>
            </div>
        </div>
                    
        <div class="quizFooter">            
            <img src="/static/img/BuildYourCloset/progress_step2.gif" alt="Step 2" class="quizProgress" />
            <input id="lnkQuizStep3" type="image" href="#QuizStep3" src="/static/img/BuildYourCloset/button_next.gif" value="Next" style="float:right;" />
            <input id="lnkPreviousStep1" type="image" href="#QuizStep1" src="/static/img/BuildYourCloset/button_previous.gif"  style="float:right;" />
        </div>
    </div>

    <div id="QuizStep3" class="quizPopup" >
        <div class="quizHeader">
            <img src="/static/img/quiz_logo.jpg" class="quizLogo" alt="" style="" />
            <span class="quizSubTitle">Which of this group of words best describe your favorite look?</span>
        </div>

        <div class="quizContent">
            <div>
                <%  int k = 0;
                    List<Wording> wordings = (List<Wording>) ViewData["Wording"];
                    foreach (var wording in wordings)
                    {
                        if (k == 4)
                        %> </div><div class="quizContentBelow"> <%                        
                            Html.RenderPartial("Wording", wording);                    
                        k++;
                    } %>
            </div>
        </div>
            
        <div class="quizFooter">
            <img src="/static/img/BuildYourCloset/progress_step3.gif" alt="Step 3" class="quizProgress" />
            <input id="lnkQuizComplete" type="image" href="#QuizComplete" src="/static/img/BuildYourCloset/button_next.gif" value="Next" style="float:right;" />
            <input id="lnkPreviousStep2" type="image" href="#QuizStep2" src="/static/img/BuildYourCloset/button_previous.gif"  style="float:right;" />
        </div>
        
    </div>
        
    <div id="QuizComplete" class="quizPopup" >
        <div class="quizHeader">
            <img src="/static/img/quiz_logo.jpg" class="quizLogo" alt="" style="" />
            <span class="quizTitle">QUIZ RESULTS</span>
            <span class="quizSubTitle">Given your responses the fashion flavors that best match with you are:</span>
        </div>

        <div class="quizComplete">
            <div class="quizCompleteHeader">Your Fashion Flavor results</div>
            <div style="margin-top:15px; float:left;" >
                <div class="quizCompleteBoxes" >
                    <div style="width:200px;" class="quizCompleteText">Based on your answers, you Fashion Flavor is:</div>
                    <div class="quizCompleteWhiteSpace">
                        <div id="Result1" class="quizCompleteResult">
                            <img id="FashionFlavor1Image" class="quizCompleteFashionFlavorImage" /><br />
                            <span id="FashionFlavor1Name"></span>
                        </div>
                        <div id="Result2" class="quizCompleteResult" >
                            <img id="FashionFlavor2Image" class="quizCompleteFashionFlavorImage"/><br />
                            <span id="FashionFlavor2Name"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>            
            
        <div class="quizFooter">
            <img src="/static/img/BuildYourCloset/progress_step4.gif" alt="Step 4" class="quizProgress" />
            <%if(updateFlavors) { %>
                <% using (Html.BeginForm("QuizCompleted", "FlavorChange")) { %>
                <input type="hidden" id="Flavor1Id" name="Flavor1Id" />
                <input type="hidden" id="Flavor2Id" name="Flavor2Id" />
                
                <input id="btnContinueBuilding" type="image" src="/static/img/BuildYourCloset/button_finish.gif" value="Continue" style="float:right" />
                <input id="lnkRestart" type="image" href="#QuizStep1" src="/static/img/BuildYourCloset/button_restart.gif"  style="float:right;" />
                
                <%} %>
            <% } else { %>
                <% using (Html.BeginForm("QuizCompleted", "FlavorSelect"))
                   { %>  
                <input type="hidden" id="Flavor1Id" name="Flavor1Id" />
                <input type="hidden" id="Flavor2Id" name="Flavor2Id" />
                
                <input id="btnContinueBuilding" type="image" src="/static/img/BuildYourCloset/button_finish.gif" value="Continue" style="float:right" />
                <input id="lnkRestart" type="image" href="#QuizStep1" src="/static/img/BuildYourCloset/button_restart.gif"  style="float:right;" />
                
                <% } %>            
            <%} %>
            
            
        </div>
        
    </div>
</div> 

<div id="HiddenFields">
    <input type="hidden" id="Flavor1Name" />
    <input type="hidden" id="Flavor1Image" />
    <input type="hidden" id="Flavor1Result" />
    <input type="hidden" id="Flavor2Name" />
    <input type="hidden" id="Flavor2Image" />
    <input type="hidden" id="Flavor2Result" />
</div>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">
    var quizSelectedCount = 0;
    var selectedItems = 0;

    $(document).ready(function() {
        $(".quizPopup").dialog({ autoOpen: false, height: 555, width: 720, resizable: false, modal: true, close: function(event, ui) { clearQuizCheckBoxes(); } });

        $('.startQuiz').click(function(e) {
            $("#QuizStep1").dialog('option', 'show', 'slide');
            $("#QuizStep1").dialog('open');
        });

        $('.fashionFlavorSelect').mouseover(function() {
            var $chkCount = getCheckBoxsFromDiv("divFlavorSelect", true);
            if ($chkCount.length < 2) {
                $(this).removeClass("unselectedItem");
                $(this).addClass("selectedItem");
            }
        });


        $('#btnBuildNext').click(function(e) {
            var $chk = $("#buildYourCloset input[type='checkbox']:checked"); //Get only checked
            if ($chk.length == 0) {
                e.preventDefault();
                showMessage("You must choose up to 2 types to continue.");
            }
        });

        $(".fashionFlavorSelect input[type='checkbox']").click(function(e) {
            var src = getSourceElement(e);
            var $chkCount = getCheckBoxsFromDiv("divFlavorSelect", true);
            if ($chkCount.length <= 2) {
                if (src.checked) src.checked = true;
                else src.checked = false;
            } else src.checked = false;
        });

        $('.fashionFlavorSelect').click(function(e) {
            var src = getSourceElement(e);
            if (src.tagName != 'INPUT') {
                var $chk = getCheckBoxsFromDiv(this.id, false);
                if ($chk[0].checked) $chk[0].checked = false
                else {
                    var $chkCount = getCheckBoxsFromDiv("divFlavorSelect", true);
                    if ($chkCount.length < 2) $chk[0].checked = true;
                }
            }
        });

        $('.quizPopup').click(function(e) {
            var src = getSourceElement(e);
            if ($("#quizAlert").length > 0 && src.value != "Next")
                $("#quizAlert").remove();
        });

        $('.fashionFlavorSelect').mouseout(function() {
            var $chk = getCheckBoxsFromDiv(this.id, false);
            if (!$chk[0].checked) {
                $(this).removeClass("selectedItem");
                $(this).addClass("unselectedItem");
            }
        });

        $('.Quiz_Item_Box').click(function(e) {
            var src = getSourceElement(e);
            var $chk = getCheckBoxsFromDiv(this.id, false);
            if (src.nodeName != 'INPUT')
                $chk[0].checked = ($chk[0].checked) ? false : true;

            if ($chk[0].checked) {
                $(this).removeClass("unselectedQuizItem");
                $(this).addClass("selectedQuizItem");
            } else {
                $(this).removeClass("selectedQuizItem");
                $(this).addClass("unselectedQuizItem");
            }

        });

        $('#lnkQuizStep2').click(function(e) {
            e.preventDefault();
            changeStep($(this), "QuizStep1");
        });

        $('#lnkQuizStep3').click(function(e) {
            e.preventDefault();
            changeStep($(this), "QuizStep2");
        });

        $('#lnkQuizComplete').click(function(e) {
            e.preventDefault();
            changeStep($(this), "QuizStep3");
        });

        $("#lnkPreviousStep1").click(function(e) {
            e.preventDefault();
            var src = getSourceElement(e);
            previousStep($(src), "QuizStep2");
        });


        $("#lnkPreviousStep2").click(function(e) {
            e.preventDefault();
            var src = getSourceElement(e);
            previousStep($(src), "QuizStep3");
        });


        $("#lnkRestart").click(function(e) {
            e.preventDefault();
            clearQuizCheckBoxes();
            var src = getSourceElement(e);
            previousStep($(src), "QuizComplete");
        });
        
        startSilouhettePreloading(1);
    });    
    
    function changeStep(obj, currentStep) {
        var id = obj.attr('href');        
        var $isValid = getCheckBoxsFromDiv("#" + currentStep, true);
        
        if ($isValid.length == 0) {       
            var item = "";
            switch (currentStep) {
                case "QuizStep1":
                    item = "photo";
                    break;

                case "QuizStep2":
                    item = "store";
                    break;

                case "QuizStep3":
                    item = "group of words"
                    break;
            }
            showMessage("You must choose at least one " + item + " to continue.");
            return;
        }

        if (id == "#QuizComplete") {
            var $inputs = getCheckBoxsFromDiv(".quizContent", true);
            getFashionFlavors($inputs);
        }
        
        //Restore div to his original state....
        $("#" + currentStep).dialog('destroy');
        $("#" + currentStep).dialog({ autoOpen: false, height: 555, width: 720, resizable: false, modal: true, close: function(event, ui) { clearQuizCheckBoxes(); } });
        $(id).dialog('open');
    }    

    function previousStep(obj, currentStep) {
        var id = obj.attr('href');
        $("#" + currentStep).dialog('destroy');
        $("#" + currentStep).dialog({ autoOpen: false, height: 555, width: 720, resizable: false, modal: true, close: function(event, ui) { clearQuizCheckBoxes(); } });
        $(id).dialog('open');
    }

    function selectedCheckBoxcesCount(chkObj) {
        var divId = $(chkObj).parentElement.parentElement.id;
        return (getCheckBoxsFromDiv(divId, true)).length;
    }

    function clearQuizCheckBoxes() {
        //Uncheck all checkboxs...
        $chks = $(".quizContent").find("input[type='checkbox']:checked");
        for (var i = 0; i < $chks.length; i++)
            $chks[i].checked = false;

        //Quit orange border from the checkboxs!
        $(".quizContent").find(".selectedQuizItem").addClass(".selectedQuizItem");
        $(".Quiz_Item_Box").removeClass("selectedQuizItem");
        $(".Quiz_Item_Box").addClass("unselectedQuizItem");
        
    }
    
    function getFashionFlavors($inputs) {
        temp = new Array();

        $inputs.each(function() {
            temp.push(this.id);
        });
        var selection = {
            "Ids": temp
        };
        
        $("#Result1").hide();
        $("#Result2").hide();
        
        var encoded = $.toJSON(selection);

        $.ajax({
            type: "POST",
            url: "<%= Url.RouteUrl(new { controller = "FlavorQuiz", action= "GetResult"}) %>",
            data: encoded,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                var resPath = '<% = Resources.GetFlavorsPath() %>';
                $("#Flavor1Id").val(data.FashionFlavor1Id);
                $("#Flavor1Name").val(data.FashionFlavor1Name);
                $("#FashionFlavor1Name").text(data.FashionFlavor1Name);
                $("#Flavor1Image").val("shape_" + data.FashionFlavor1Id + ".gif");
                $("#FashionFlavor1Image").attr("src", resPath + "shape_" + data.FashionFlavor1Id + ".gif");
                $("#Flavor1Result").val(data.FashionFlavor1Result);
                if (!data.Single) {
                    $("#Flavor2Id").val(data.FashionFlavor2Id);
                    $("#Flavor2Name").val(data.FashionFlavor2Name);
                    $("#FashionFlavor2Name").text(data.FashionFlavor2Name);
                    $("#Flavor2Image").val("shape_" + data.FashionFlavor2Id + ".gif");
                    $("#FashionFlavor2Image").attr("src", resPath + "shape_" + data.FashionFlavor2Id + ".gif");
                    $("#Flavor2Result").val(data.FashionFlavor2Result);
                    $("#Result1").removeAttr("style");                    
                                        
                    $("#Result1").show();
                    $("#Result2").show();
                }
                else {                    
                    $("#Result2").hide();
                    $("#Result1").show();
                    $("#Result1").attr("style", "margin-left:90px;");
                }
            }
        });
    }
    
</script>
</asp:Content>