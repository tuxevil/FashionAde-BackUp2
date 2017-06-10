//Highlight Build Closet link if the user is in the registration process
function highlightBuildClosetLink() {
    var url = document.location.href;
    if (url.indexOf("BuildYourCloset") != -1 || url.indexOf("buildyourcloset") != -1)
        $("#lnkBuildCloset").css("background-color", "#F08331");
}

//Return the source element for all browsers
function getSourceElement(event) {
    return event.srcelement ? event.srcelement : event.target;
}

var ap;
function showMessage(msg, divId) {
    $("#spnMsg").html(msg);
    $("#divMessage").overlay({effect: 'apple', api: true, top: 'center', closeOnClick: false, expose: { color: '#333', loadSpeed: 200, opacity: 0.2 } }).load();
}

//TODO: TEST: Reemplazar luego por showMessage y la actual showMessage utilizar como showModalMessage.
function appendMessage(jqObj, msg, type) {
    var css = "";
    var src = "";

    removeMessages(); 
    
    switch (type) {
        case "error":
            css = "appendError";
            src = "/static/img/error.jpg";
            break;

        case "success":
            css = "appendSuccess";
            src = "/static/img/icon-ok.gif";
            break;

        case "warning":
            css = "appendWarning";
            src = "";
            break;

        case "loading":
            css = "appendLoading";
            src = "/static/img/ajax-loader-small.gif"
            break;
    }

    var divMsg = "";
    divMsg = "<div class='appendMessage " + css + "'>";
    divMsg += "<img src='" + src + "' />";
    divMsg += "<span>" + msg + "</span>";
    divMsg += "</div>";

    $(jqObj).prepend(divMsg);
}

function removeMessages() {
    $(".appendMessage").remove();
}

//Return checkboxs from a div.
function getCheckBoxsFromDiv(divId, onlyChecked) {
    if (onlyChecked)
        return $("#" + divId + " input[type='checkbox']:checked");    
    
    return $("#" + divId + " input[type='checkbox']");
}

(function($) {
    var cache = [];
    // Arguments are image paths relative to the current page.
    $.preLoadImages = function() {
        var type = arguments[0];
        var path = arguments[1];
        var args_len = arguments.length;

        if (type == 2) {
            for (var i = args_len; i--; ) {
                if (i == 1)
                    return;
                var cacheImage = document.createElement('img');
                cacheImage.src = path + arguments[i];
                cache.push(cacheImage);
            }
        }
        else {
            for (var i = 2; i < args_len; i++) {
                var cacheImage = document.createElement('img');
                cacheImage.src = path + arguments[i];
                cache.push(cacheImage);
            }
        }
    }
})(jQuery)

function startSilouhettePreloading() {
    var type = arguments[0];

    window.setTimeout(function() {
        jQuery.preLoadImages(
    type,
    "http://as2.fashion-ade.com/res/Silouhettes/",
    "100102.png",
    "100103.png",
    "100104.png",
    "100105.png",
    "100106.png",
    "100107.png",
    "100108.png",
    "100109.png",
    "100110.png",
    "100112.png",
    "100113.png",
    "100114.png",
    "100115.png",
    "100116.png",
    "100117.png",
    "100118.png",
    "100119.png",
    "100120.png",
    "100121.png",
    "100122.png",
    "200101.png",
    "200102.png",
    "200103.png",
    "200104.png",
    "200105.png",
    "200106.png",
    "200107.png",
    "200108.png",
    "200109.png",
    "200110.png",
    "200111.png",
    "300101.png",
    "300102.png",
    "300103.png",
    "300104.png",
    "300105.png",
    "300106.png",
    "300107.png",
    "300108.png",
    "400101.png",
    "400102.png",
    "400103.png",
    "400104.png",
    "400105.png",
    "400106.png",
    "400107.png",
    "400109.png",
    "400110.png",
    "400111.png",
    "400112.png",
    "400113.png",
    "400114.png",
    "400115.png",
    "400116.png",
    "400117.png",
    "500101.png",
    "500102.png",
    "500103.png",
    "500104.png",
    "500105.png",
    "500106.png",
    "500107.png",
    "500108.png",
    "500109.png",
    "500110.png",
    "500111.png",
    "500112.png",
    "500113.png",
    "500114.png",
    "500115.png",
    "500116.png",
    "500117.png",
    "500118.png",
    "500119.png",
    "500120.png",
    "500121.png",
    "500122.png",
    "600101.png",
    "600102.png",
    "600103.png",
    "600104.png",
    "600105.png",
    "600106.png",
    "600107.png",
    "600108.png",
    "600109.png",
    "600110.png",
    "600111.png",
    "600112.png",
    "600113.png",
    "600114.png",
    "600115.png",
    "600116.png",
    "700101.png",
    "700102.png",
    "700103.png",
    "700104.png",
    "700105.png",
    "700106.png",
    "700107.png",
    "700108.png",
    "700109.png",
    "700110.png",
    "800101.png",
    "800102.png",
    "800103.png",
    "800104.png",
    "800105.png",
    "800106.png",
    "800107.png",
    "800108.png",
    "800109.png",
    "800110.png",
    "800111.png",
    "900101.png",
    "900102.png",
    "900103.png",
    "900104.png",
    "900105.png",
    "900106.png",
    "900107.png",
    "900108.png",
    "900109.png",
    "900110.png",
    "900111.png",
    "900112.png",
    "900113.png",
    "900114.png",
    "900115.png",
    "900116.png",
    "900117.png",
    "900118.png",
    "900119.png",
    "900120.png",
    "900121.png",
    "900122.png",
    "900123.png",
    "900124.png",
    "900125.png",
    "900126.png",
    "900127.png",
    "900128.png",
    "900129.png",
    "900130.png",
    "900131.png",
    "900132.png",
    "900133.png",
    "222101.png",
    "222103.png",
    "222104.png",
    "222105.png",
    "222106.png",
    "222107.png",
    "222108.png",
    "222109.png",
    "222110.png",
    "222111.png",
    "222112.png",
    "333101.png",
    "333102.png",
    "333103.png",
    "333104.png",
    "333105.png",
    "444101.png",
    "444102.png",
    "555101.png",
    "555102.png",
    "555103.png",
    "555104.png",
    "555105.png",
    "555106.png",
    "555107.png",
    "555108.png",
    "666101.png",
    "666102.png",
    "666103.png",
    "666104.png",
    "666105.png",
    "666106.png",
    "111101.png",
    "111102.png",
    "111103.png",
    "111104.png",
    "111105.png",
    "111106.png",
    "111107.png",
    "111108.png",
    "111109.png",
    "111110.png",
    "111111.png",
    "111112.png",
    "111113.png",
    "111114.png",
    "111115.png",
    "111116.png",
    "111117.png",
    "111118.png",
    "111119.png",
    "111120.png",
    "111121.png",
    "111122.png",
    "111123.png",
    "111124.png",
    "111125.png",
    "111126.png",
    "777101.png",
    "777102.png",
    "777103.png",
    "777104.png",
    "777105.png",
    "777106.png",
    "777108.png",
    "777109.png",
    "777110.png"
            );
        }, 2000);
}