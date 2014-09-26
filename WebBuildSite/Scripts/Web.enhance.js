Web.enhance = Web.enhance || function (object_to_enhance) {
    if (!Web.global.exists(object_to_enhance)) return;
    var o = object_to_enhance;
    if (!o.jquery) {
        o = $(o);
    }

    var addBoldText = function (text_to_add) {
        this.css({ "font-weight": "bold" });
        this.html(text_to_add);
        return this;
    };

    var addItalicText = function (text_to_add) {
        this.css({ "font-style": "italic" });
        this.html(text_to_add);
        return this;
    };

    var centerOnscreen = function () {
        var windowWidth = document.documentElement.clientWidth;
        var windowHeight = document.documentElement.clientHeight;
        var popupHeight = o.height();
        var popupWidth = o.width();
        this.css({
            "position": "absolute",
            "top": windowHeight / 2 - popupHeight / 2
								+ $(window).scrollTop() + "px",
            "left": windowWidth / 2 - popupWidth / 2
								+ $(window).scrollLeft() + "px",
            "height": popupHeight + "px",
            "width": popupWidth + "px"
        });
        return this;
    };

    var setWidth = function (width_in_pixels) {
        var w = null;
        if (!WebWeb.global.exists(width_in_pixels)) return;
        if (!isNaN(width_in_pixels)) {
            w = width_in_pixels + "px";
        }

        this.css("width", w);
        return this;
    };

    var turnRed = function () {
        this.css({ "background-color": "#FF0000" });
        return this;
    };
    var turnYellow = function () {
        this.css({ "background-color": "#F6C700" });
        return this;
    };
    var turnGreen = function () {
        this.css({ "background-color": "#315105" });
        return this;
    };

    o.addBoldText = addBoldText;
    o.addItalicText = addItalicText;
    o.centerOnscreen = centerOnscreen;
    o.turnRed = turnRed;
    o.turnYellow = turnYellow;
    o.turnGreen = turnGreen;
    o.eSetWidth = setWidth;
    o.hasBeenDragged = false;
    o.isEnhanced = true;
    return o;
};
