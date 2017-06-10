<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<GarmentsListData>" %>
<%@ Import Namespace="FashionAde.Core"%>
<%@ Import Namespace="FashionAde.Core.Clothing"%>
<%@ Import Namespace="FashionAde.Web.Controllers.MVCInteraction"%>

<input id="hidMyGarmentsSelected" type="hidden" name="hidMyGarmentsSelected" />
<input id="hidMyWishListSelected" type="hidden" name="hidMyWishListSelected" />

<div class="parentDiv">
    <div style="width: 690px; float: left;">
        <div class="garmenttooltip">
            <div class="garmenttooltip_buttons_zone">
                <div id="btnAddToGarment" class="garmenttooltip_buttons" style=" top:16px; left:5px;">Add to My Garments</div>
                <div id="btnAddToWishList" class="garmenttooltip_buttons" style="top:38px; left:5px;">Add to My Wish List</div>
            </div>
        </div> 
        <% if (!Model.ForEdit)
           {%>
            <h1 id="selectGarmetsTitle" style="background:none; margin:15px 0 0 0; padding:0;">Select your Garments</h1>
            <img id="imgOtherWomen" src="/static/img/BuildYourCloset/other_women.gif" alt="" style="margin: 20px 4px 4px;" />
            <img id="img1" src="/static/img/BuildYourCloset/arrow.gif" alt="" style="margin-top:30px;" />
            <h2 style="float:left; color:#000; margin:0 0 10px 0; width:100%;">Just click on a garment, choose it in the color you own and drag that piece into your closet. <br /> Yes, it’s that easy!</h2>
        <%}
           else
           { %>
            <h1 id="H1" style="margin-top:15px;">Add from our Master Closet</h1>
            <h2 style="float: left; color: Black; margin-bottom: 15px;">Don't see a garment you're looking for? Tell us and we'll <%= Html.ActionLink("add it", "Index", "ContactUs")%> or <%= Html.ActionLink("upload your garments.", "Index", "UploadGarment") %></h2>
           <%} %>
    
        <div id="GarmentsDiv" class="GarmentSelector_GarmentsDiv">            
            
            <!-- the tabs --> 
            <ul class="tabs"> 
	            <li><a href="#">1. Pants, Jeans</a></li> 
	            <li><a href="#">2. Skirts, Shorts</a></li> 
	            <li><a href="#">3. Dresses</a></li> 
	            <li><a href="#">4. Jackets</a></li> 
	            <li><a href="#">5. Tops</a></li> 
	            <li><a href="#">6. Accesories</a></li> 
            </ul>
             
             
            <!-- tab "panes" --> 
            <div class="panes"> 
	            <div></div> 
	            <div></div> 
	            <div></div> 
	            <div></div> 
	            <div></div> 
	            <div></div> 	
            </div>   


            <div class="sliderDiv"> 
                <a class="prevPage browse left"></a>     
                    <div id="divGarmetsScroll" class="scrollable">                
                        <div class="items">
                            <%  foreach (Silouhette silouhette in Model.Silouhettes)                             
                                {%>                                
                                <div id="divSil_<%= silouhette.Id %>"  class="silouhettesContainer" >
                                    <img id="si_<%= silouhette.Id %>" src="<%= Resources.GetSilouhettePath(silouhette) %>" class="Silouhettes" rel="#loading" cat="<%= silouhette.Category.Id %>" />
                                    <span class="silouhetteDescription"><%= silouhette.Description %></span> 
                                </div>
                                <%} %>
                        </div> 
                    </div>    
                <a class="nextPage browse right"></a>
            </div> 
            <div style="background-color: #F4F4F4; height: 50px; margin-top: 5px; margin-bottom: 5px; border: 1px solid lightGrey;">
                <span style="color:#F38333; float: left; margin-left:5px; display:inline; font-size: 19px; margin-top: 11px;">See this garment in another pattern or texture:</span>
                <div class="GarmentSelector_Filter" style="width:115px;">
                    <span class="GarmentSelector_Filter_Text">Patterns</span><img id="PatSelected" style="float: left; margin-left: 2px; margin-right: 2px;" /><a id="cboPatterns"  onclick="return false" class="garmetSelectorCombo"><img src="/static/img/dropdown.jpg" alt="" /></a>
                    <div id="PatternFilters" class="GarmentSelector_Filter_Items editList" style="left: 15px; max-height: 200px; top: 20px; width: 50px;">
                        <%  
                        string patternClass;
                        foreach (Pattern pattern in Model.Patterns)
                        {
                            if(pattern.Description == "Solid")
                                patternClass = "PatternSelected";
                            else patternClass = "Pattern";
                            %>
                            <img class="<%= patternClass %>" id="p_<%= pattern.Id %>" src="<%= Resources.GetPatternPath(pattern) %>" alt="<%= pattern.Description %>" title="<%= pattern.Description %>" />
                        <%} %>
                    </div>
                </div>
                <div class="GarmentSelector_Filter">
                    <span class="GarmentSelector_Filter_Text">Fabrics</span><span id="FabSelected" style="float:left; margin-left: 8px; margin-right: 2px; margin-top: 7px;"></span><a id="cbpFabrics"  onclick="return false" class="garmetSelectorCombo"><img src="/static/img/dropdown.jpg" alt="" /></a>
                    <div id="FabricFilters" class="GarmentSelector_Filter_Items editList" style="max-height: 200px; top: 20px; left: -23px; margin-left: 72px;">
                        <%
                        string fabricClass;
                        foreach (Fabric fabric in Model.Fabrics)
                        {
                            if (fabric.Id == Convert.ToInt32(Model.FabricId))
                                fabricClass = "FabricSelected";
                            else fabricClass = "Fabric";
                            %>
                            
                            <span class="<%= fabricClass %>" id="f_<%= fabric.Id %>"><%= fabric.Description %></span><br />
                        <%} %>
                    
                    </div>
                </div>
            </div>
            <div id="GarmentsResults" style="clear:both; " >
                <% foreach (Garment garment in Model.Garments)
                    {
                    %>
                        <div id="<%= garment.Id %>" class="GarmentDragable GarmentSelector_Garments" >
                            <img id="<%= garment.Id %>" src="<%= Resources.GetGarmentPath(garment) %>" desc="<%= garment.Title %>" />
                        </div>
                    <%} %>
            </div>            
            
        </div>
        
        <input id="hidSilouhetteId" type="hidden" value="<%= Model.SilouhetteId %>" />
        <input id="hidPatternId" type="hidden" value="<%= Model.PatternId %>" />
        <input id="hidFabricId" type="hidden" value="<%= Model.FabricId %>" />
        
    </div>
    <div id="FiltersDiv" class="GarmentSelector_FiltersDiv">
            <% if (!Model.ForEdit)
           {%><img src="/static/img/BuildYourCloset/step2.gif" alt="" style="float:left; margin-right:5px;" /><%}%>
    
            <div class="GarmentSelector_FilterDiv_FashionFlavorSelected">
            <%if (Model.ForEdit)
           {
               using (Html.BeginForm("ChangeFlavor", Model.Controller, FormMethod.Post, new { @id = "frmFlavor", onSubmit = "apiOverlay.load();" }))
                {%>
                    <input type="hidden" id="NewGarmentsItems" name="NewGarmentsItems" value="<%= Model.NewGarmentsIds %>"/>
                    <input type="hidden" id="NewWishListItems" name="NewWishListItems" value="<%= Model.NewWishGarmentsIds %>"/>
                    <div style="position: relative;">
                        <div class="flavorList_Title"><span>Change Fashion Flavor here</span><a id="cboFlavorSelector"  onclick="return false" class="flavorSelectorCombo"><img src="/static/img/orangearrow.JPG" alt="" /></a><div style="clear:both"></div></div>
                        <div id="flavorsSelectArea" class="flavorList" style="top:25px; left:-1px; max-height: 300px;">                        
                        <% bool firstItem = true; %>
                            <% foreach (FashionFlavor flavor in Model.FashionFlavorsAlternative)
                               { %>                                    
                                    <div class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box" style="margin-left: 10px; margin-top: 10px; margin-bottom: 5px;">
                                        <center>
                                        <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Title" ><%= flavor.Name %></span>
                                        <img class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Image" src="<%= Resources.GetFashionFlavorThumbnailPath(flavor) %>" alt="<%= flavor.Name %>"  /><br />
                                        <%=Html.RadioButton("FashionFlavorAlternative", flavor.Id, firstItem)%>
                                        <% if (firstItem) firstItem = false;  %>
                                        </center>
                                    </div>
                            <%} %>
                            <center>
                                <input id="btnChangeFlavors" type="image" src="/static/img/btngo.PNG" />
                            </center>
                        </div>
                    </div>
                    <%--<%= Html.DropDownList("FashionFlavorAlternative", (List<SelectListItem>)ViewData["FashionFlavorAlternative"], new { onchange = "$('#frmFlavor').submit();" })%>--%>
                <%}
           } %>
                <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Title">Your Fashion Flavors:</span>
                <div class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box">
                    <center>
                    <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Title" ><%= Model.FashionFlavors[0].Name %></span>
                    <img class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Image" src="<%= Resources.GetFashionFlavorThumbnailPath(Model.FashionFlavors[0]) %>" alt="<%= Model.FashionFlavors[0].Name %>"  />
                    </center>
                </div>
                <div class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box">
                    <center>
                    <% if (Model.FashionFlavors.Count > 1){  %>
                    <span class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Title" ><%= Model.FashionFlavors[1].Name%></span>
                    <img class="GarmentSelector_FilterDiv_FashionFlavorSelected_Box_Image" src="<%= Resources.GetFashionFlavorThumbnailPath(Model.FashionFlavors[1]) %>" alt="<%= Model.FashionFlavors[1].Name %>"  />
                    <% } %>
                    </center>
                </div>
                <div style="clear: both" ></div>
            </div>
            
            <div style="margin-top:10px; margin-bottom:20px; ">
                <img src="/static/img/BuildYourCloset/bubbles-animation.gif" />
            </div> 

            <div id="MyGarmentsDroppable" class="GarmentSelector_DropableArea">
                            
                <img src="/static/img/BuildYourCloset/mygarments.jpg" class="GarmentSelector_DropableArea_Image" alt="" />
                <span class="GarmentSelector_DropableArea_Text" style="margin:5px 8px 0 0;">My Garments</span>
                <a id="cboEditMyGarnets"  onclick="return false" class="garmetSelectorCombo"><img src="/static/img/dropdown.jpg" alt="" /></a>
                                
                <span class="GarmentSelector_DropableArea_Total">Total:</span>
                <span id="MyGarmentCount" class="GarmentSelector_DropableArea_Count"><%= Model.MyGarments.Count + Model.NewGarments.Count %></span>                
                
                <div class="dragYourGarmentsHere"><img src="/static/img/BuildYourCloset/img_drag_arrow.jpg" /><span>Drag your garments here!</span></div>                
                <div id="divEditGarments" class="editList" style="top:64px; left:-1px; max-height: 300px;"> 
                 <% foreach(ClosetGarment garment in Model.MyGarments)
    {
        string id = "divGarment" + garment.Garment.Id;
        string div = "<div id='" + id + "' class='Garment_Item_Box'>";
        div += "<div class='Garment_Item_Box_Image'><center><img src='" + Resources.GetGarmentPath(garment.Garment) + "' /></center></div>";
        div += "<div class='Garment_Item_Box_Clear'></div></div>"; 
                        %><%= div %>
    <%} %>   
    <% foreach(Garment garment in Model.NewGarments)
    {
        string id = "divGarment" + garment.Id;
        string div = "<div id='" + id + "' class='Garment_Item_Box'>";
        div += "<div class='Garment_Item_Box_Image'><center><img src='" + Resources.GetGarmentPath(garment) + "' /></center></div>";
        div += "<div class='Garment_Item_Box_Clear'><img id='erase_" + garment.Id + "' src='/static/img/remove_item.jpg' class='removeSelected' /></div></div>"; 
                        %><%= div %>
    <%} %>
                </div>
            </div>
            
            <div id="MyWishListDropable" class="GarmentSelector_DropableArea" >
                <img src="/static/img/BuildYourCloset/mywishlist.jpg" class="GarmentSelector_DropableArea_Image" alt="" />
                <span class="GarmentSelector_DropableArea_Text" style="margin:5px 22px 0 0;">My Wish List</span>
                <a id="cboEditMyWishList"  onclick="return false" class="garmetSelectorCombo"><img src="/static/img/dropdown.jpg" alt="" /></a>
                
                <span class="GarmentSelector_DropableArea_Total">Total:</span>
                <span id="MyWishListCount" class="GarmentSelector_DropableArea_Count"><%= Model.MyWishGarments.Count + Model.NewWishGarments.Count %></span>
                
                <div class="dragYourGarmentsHere"><img src="/static/img/BuildYourCloset/img_drag_arrow.jpg" /><span>Drag your garments here!</span></div>
                <div id="divEditMyWishList" class="editList" style="top:64px; left:-1px; max-height: 300px;">                
                    <% foreach(WishGarment garment in Model.MyWishGarments)
    {
        string id = "divGarment" + garment.Garment.Id;
        string div = "<div id='" + id + "' class='Garment_Item_Box'>";
        div += "<div class='Garment_Item_Box_Image'><center><img src='" + Resources.GetGarmentPath(garment.Garment) + "' /></center></div>";
        div += "<div class='Garment_Item_Box_Clear'></div></div>"; 
                        %><%= div %>
    <%} %>
    <% foreach(Garment garment in Model.NewWishGarments)
    {
        string id = "divGarment" + garment.Id;
        string div = "<div id='" + id + "' class='Garment_Item_Box'>";
        div += "<div class='Garment_Item_Box_Image'><center><img src='" + Resources.GetGarmentPath(garment) + "' /></center></div>";
        div += "<div class='Garment_Item_Box_Clear'><img id='erase_" + garment.Id + "' src='/static/img/remove_item.jpg' class='removeSelected' /></div></div>"; 
                        %><%= div %>
    <%} %>
                </div>
            </div>            
            
            <div class="divBuildBottom">
                <% using (Html.BeginForm(Model.Action, Model.Controller, FormMethod.Post, new { @id = "frmNext", onSubmit = "apiOverlay.load();" }))
                   {
                       string myGarmentsItems = "";
                       string myWishListItems = "";
                       if (!string.IsNullOrEmpty(Model.MyGarmentsIds) || !string.IsNullOrEmpty(Model.NewGarmentsIds))
                           myGarmentsItems = Model.MyGarmentsIds + "," + Model.NewGarmentsIds;
                       if (!string.IsNullOrEmpty(Model.MyWishGarmentsIds) || !string.IsNullOrEmpty(Model.NewWishGarmentsIds))
                           myWishListItems = Model.MyWishGarmentsIds + "," + Model.NewWishGarmentsIds;
                       %>  
                <input type="hidden" id="myGarmentsItems" name="myGarmentsItems" value="<%= myGarmentsItems %>" />
                <input type="hidden" id="myWishListItems" name="myWishListItems" value="<%= myWishListItems %>" />
                <input id="btnNext" type="image" src="/static/img/BuildYourCloset/button_next.gif" value="Next" class="centerDiv nextStep" style="padding-top:10px;" />
                <%} %>
            </div>
                    
        </div>
</div> 

