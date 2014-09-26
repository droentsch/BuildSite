Web.view = Web.view || function (e, global) {
    var theObject = {};
    var $loaderContainer = null;
    var $messageBox = null;
    var $msgBoxHeader = null;
    var $msgBoxDetail = null;
    var $msgBoxLinks = null;
    var $msgBoxDatetime = null;
    var $msgBoxOther = null;
    var $modalOverlay = null;
    var $msgBoxClose = null;
    var $addRecipientsButton = null;
    var $addRecipientsInput = null;
    var $minifyInput = null;
    var msgBoxHeaderSelector = "#header";
    var msgBoxDetailSelector = "#body .detail";
    var msgBoxLinksSelector = "#body .links";
    var msgBoxDatetimeSelector = "#body .datetime";
    var msgBoxOtherSelector = "#body .other";
    var msgBoxCloseSelector = "#close";
    var loaderContainerSelector = ".home_page_container .loader";
    var messageBoxSelector = ".home_page_container .message_box";
    var modalOverlaySelector = "#modal_overlay";
    var addRecipientsButtonSelector = ".page_background #build_modification_controls .modification_button";
    var addRecipientsInputSelector = ".page_background #build_modification_controls #add_recipients_input";
    var minifyInputSelector = "#buildOptions #minifyInput";

    var COLORS = {};
    COLORS.FAIL_COLOR = "#F2FF77";
    COLORS.SUCCESS_COLOR = "#EFF8F7";

    var TEXT = {};
    TEXT.SUCCESS_HEADER = "Success!";
    TEXT.FAILURE_HEADER = "Uh-oh.";

    var MSG_TYPE = {};
    MSG_TYPE.SUCCESS = 1;
    MSG_TYPE.FAILURE = 2;

    var init = function () {
        mapObjects();
        bindEvents();
    };

    var mapObjects = function () {
        $minifyInput = e(minifyInputSelector);
        $addRecipientsButton = e(addRecipientsButtonSelector);
        $addRecipientsInput = e(addRecipientsInputSelector);
        $loaderContainer = e(loaderContainerSelector);
        $modalOverlay = e(modalOverlaySelector);
        $messageBox = e(messageBoxSelector);
        $msgBoxHeader = e(msgBoxHeaderSelector, $messageBox);
        $msgBoxDetail = e(msgBoxDetailSelector, $messageBox);
        $msgBoxLinks = e(msgBoxLinksSelector, $messageBox);
        $msgBoxDatetime = e(msgBoxDatetimeSelector, $messageBox);
        $msgBoxOther = e(msgBoxOtherSelector, $messageBox);
        $msgBoxClose = e(msgBoxCloseSelector, $messageBox);
    };

    var bindEvents = function () {
        $msgBoxClose.click(hideMessage);
        $addRecipientsButton.toggle(showAddRecipientsInput, hideAddRecipientsInput);
    };
    var showLoader = function () {
        show($loaderContainer);
    };
    var hideLoader = function () {
        hide($loaderContainer);
    };
    var showAddRecipientsInput = function ()
    {
        $addRecipientsInput.show();
        $addRecipientsInput.focus();
    };
    var hideAddRecipientsInput = function ()
    {
        $addRecipientsInput.hide();
        $addRecipientsInput.html("");
    };
    var showSuccessMessage = function (data) {
        if (!data) return this;
        return showMessage(MSG_TYPE.SUCCESS, data);
    };
    var showFailureMessage = function (data) {
        if (!data) return this;
        return showMessage(MSG_TYPE.FAILURE, data);
    };
    var showMessage = function (msg_type, data) {
        if (!msg_type) return this;
        if (!data) return this;
        if (msg_type == MSG_TYPE.SUCCESS) {
            $msgBoxHeader.addBoldText(TEXT.SUCCESS_HEADER);
            $messageBox.removeClass("error");
        }
        if (msg_type == MSG_TYPE.FAILURE) {
            $msgBoxHeader.addBoldText(TEXT.FAILURE_HEADER);
            $messageBox.addClass("error");
        }
        if (data.Detail) {
            if (data.Detail.length > 1000) {
                data.Detail = data.Detail.slice(0, 1000);
                data.Detail += "  (message truncated in client)";
            }
            $msgBoxDetail.addItalicText(data.Detail);
            $msgBoxDetail.html($msgBoxDetail.html().replace(/\n/g, "<BR />"));
            $msgBoxDetail.show();
        }
        if (data.Links) {
            $msgBoxLinks.html(data.Links);
            $msgBoxLinks.html($msgBoxLinks.html().replace(/\n/g, "<BR />"));
            $msgBoxLinks.show();
        }
        if (data.DateTime) {
            $msgBoxDatetime.html(data.DateTime);
            $msgBoxDatetime.show();
        }
        if (data.Other) {
            $msgBoxOther.html(data.Other);
            $msgBoxOther.html($msgBoxOther.html().replace(/\n/g, "<BR />"));
            $msgBoxOther.show();
        }
        return show($messageBox);
    };
    var hideMessage = function () {
        hide($messageBox);

        $msgBoxDetail.hide();
        $msgBoxLinks.hide();
        $msgBoxDatetime.hide();
        $msgBoxOther.hide();

        $modalOverlay.hide();
        return $messageBox;
    };
    var show = function ($item_to_show) {
        if (!$item_to_show) return;
        $modalOverlay.centerOnscreen();
        $modalOverlay.show();
        $item_to_show.centerOnscreen();
        $item_to_show.show();
    };
    var hide = function ($item_to_hide) {
        if (!$item_to_hide) return;
        $item_to_hide.hide();
    };

    var setBackground = function (class_to_add, class_to_remove, button_color) {
        $("body").addClass(class_to_add);
        $("body").removeClass(class_to_remove);
        $(".build_button").css({ "background-color": button_color });
    };

    theObject.init = init;
    theObject.showLoader = showLoader;
    theObject.hideLoader = hideLoader;
    theObject.showLoader = showLoader;
    theObject.showSuccessMessage = showSuccessMessage;
    theObject.showFailureMessage = showFailureMessage;
    theObject.hideMessage = hideMessage;
    theObject.addRecipientsInputSelector = addRecipientsInputSelector;
    return theObject;
} (Web.enhance, Web.global);
