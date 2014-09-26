Web.Builder = Web.Builder || function (build_details, view, button_selector) {
    if (!build_details) return;
    if (build_details === null) return;
    var global = Web.global;

    var makesMultipleCalls = false;
    if (button_selector) {
        //otherwise, the button selector is a part of build_details
        makesMultipleCalls = true;
    }

    var bd = build_details;
    var exec = null; //public command

    var REFRESH = {};
    REFRESH.SINGLE = "Single";
    REFRESH.MULTI = "Multi";

    var theObject = {};
    var $Button = null;
    var $Options = null;
    var $Loader = null;

    //only used if makesMultipleCalls===true
    var currentRequestIndex = 0;
    var lastRequestIndex;
    if (makesMultipleCalls === true) {
        lastRequestIndex = bd.length - 1;
    }

    //aliases
    var enhance = Web.enhance;
    var model = Web.model;
    var global = Web.global;

    var init = function () {
        mapObjects();
        bindEvents();
        return exec;
    };

    var mapObjects = function () {
        if (makesMultipleCalls === true) {
            $Button = enhance(button_selector);
        }
        else {
            $Button = enhance(bd.buttonSelector);
        }
        if (bd.callType == global.CALLTYPES.OPTIONS_ON_DBLCLICK) {
            $doc = enhance(document);
        }
    };

    var bindEvents = function () {
        if (bd.callType == global.CALLTYPES.BUTTON_WITH_INPUT) {
            $Button.click(buildButtonWithInputHandler);
            exec = buildButtonWithInputHandler;
            return;
        }
        if (bd.callType == global.CALLTYPES.OPTIONS_ON_DBLCLICK) {
            $doc.on("dblclick", bd.buttonSelector, optionsDblClickHandler);
            exec = optionsDblClickHandler;
            return;
        }
        if (bd.eventType && bd.eventType.toUpperCase() == "SELECT") {
            $Button.change(buildListHandler);
            exec = buildListHandler;
        }
        else {
            if (makesMultipleCalls === true) {
                $Button.click(multiCallRequester);
                exec = multiCallRequester;
            }
            else {
                $Button.click(buildButtonHandler);
                exec = buildButtonHandler;
            }
        }
    };

    //handlers

    var optionsDblClickHandler = function () {
        view.showLoader();
        model.params.url = bd.serviceAddress;
        model.params.successHandler = bd.successHandler || successHandler;
        model.params.failureHandler = bd.failureHandler || failureHandler;
        model.params.completeHandler = bd.completeHandler || completeHandler;
        if (bd.dataArgsName) {
            model.params.dataArgs = bd.dataArgsName + "=" + $(this).val();
        }

        model.getData();
    };
    var buildButtonWithInputHandler = function () {
        $Button.turnYellow();
        view.showLoader();
        var args_input = "";
        model.params.url = bd.serviceAddress;
        model.params.successHandler = bd.successHandler || successHandler;
        model.params.failureHandler = bd.failureHandler || failureHandler;
        model.params.completeHandler = bd.completeHandler || completeHandler;
        for (var i = 0; i < bd.inputSelectors.length; i++)
        {
            var $input = enhance(bd.inputSelectors[i].selector);
            if (i > 0)
            {
                args_input += "&";
            }
            args_input += bd.inputSelectors[i].dataArgName;
            if (bd.inputSelectors[i].type == "checkbox")
            {
                if ($input.is(":checked")) {
                    args_input += "=true";
                }
                else {
                    args_input += "=false";
                }
            }
            if (bd.inputSelectors[i].type == "text") {
                args_input += $input.val();
            }

        }
        model.params.dataArgs = args_input;

    model.getData();
};
var buildButtonHandler = function () {
    $Button.turnYellow();
    view.showLoader();
    model.params.url = bd.serviceAddress;
    model.params.successHandler = bd.successHandler || successHandler;
    model.params.failureHandler = bd.failureHandler || failureHandler;
    model.params.completeHandler = bd.completeHandler || completeHandler;

    model.getData();
};
var buildListHandler = function () {
    model.params.url = bd.serviceAddress;
    model.params.successHandler = bd.successHandler || successHandler;
    model.params.failureHandler = bd.failureHandler || failureHandler;
    model.params.completeHandler = bd.completeHandler || completeHandler;
    if (bd.dataArgsName) {
        model.params.dataArgs = bd.dataArgsName + "=" + $Button.val();
    }

    model.getData();
};
var multiCallRequester = function () {
    if (currentRequestIndex == 0) {
        if ($Button.hasClass("build_button") || $Button.hasClass("data_button")) {
            $Button.turnYellow();
            view.showLoader();
        }
    }
    model.params.url = bd[currentRequestIndex].service;
    model.params.successHandler = multiCallSuccessHandler;
    model.params.failureHandler = failureHandler;
    model.params.completeHandler = null;

    view.showCurrentStatus({ "Detail": "Updating " + bd[currentRequestIndex].name + "...", "DateTime": getCurrentTime() });
    model.getData();
};
var successHandler = function (data) {
    if (!data) {
        data = {};
        data.Error = true;
        data.ErrorMessage = "No data received";
        data.Time = new Date().getDate();
    }
    if (data.Error) {
        $Button.turnRed();
        view.showFailureMessage({ "Detail": data.Detail, "DateTime": data.DateTime, "Links": data.Links, "Other": data.Other });
        return this;
    }
    $Button.turnGreen();
    view.showSuccessMessage({ "Detail": data.Detail || "", "DateTime": data.DateTime || "", "Links": data.Links || "", "Other": data.Filepath || "" });
    //update the status
    if (bd.refreshAddress !== null) {
        model.params.url = bd.refreshAddress;
        model.params.successHandler = bd.successHandlerRefresh || successHandlerRefresh;

        model.getData();
    }
    return this;
};
var multiCallSuccessHandler = function (data) {
    var jsNow = getCurrentTime();
    if (!data) {
        data = {};
        data.Error = true;
        data.ErrorMessage = "No data received";
        data.Time = jsNow;
    }
    if (data.Error) {
        $Button.turnRed();
        view.showFinalStatus({ "Detail": "ERROR on " + bd[currentRequestIndex].name, "DateTime": jsNow }, "Job has terminated");
        multiCallTerminate();
        return this;
    }
    if (currentRequestIndex == lastRequestIndex) {
        view.showFinalStatus({ "Detail": bd[currentRequestIndex].name + " updated", "DateTime": jsNow }, "Job is complete");
        multiCallTerminate();
        return this;
    }

    view.showCurrentStatus({ "Detail": bd[currentRequestIndex].name + " updated", "DateTime": jsNow });

    currentRequestIndex++;
    multiCallRequester();
    return this;
};

var successHandlerRefresh = function (data) {
    if (!data) {
        data = {};
        data.Error = true;
        data.ErrorMessage = "No data received";
        data.Time = new Date().getDate();
    }
    if (data.Error) {
        $Button.turnRed();
        view.showFailureMessage({ "Detail": "Error refreshing version data.", "DateTime": data.DateTime, "Links": data.Links, "Other": data.Other });
        return this;
    }
    if (data.Type == REFRESH.SINGLE) {
        view.refreshVersion($Button, data.Data.Label, data.Data.Created);
    }
    else if (data[0].Type == REFRESH.MULTI) {
        view.refreshVersions(data);
    }
};
var failureHandler = function (data) {
    $Button.turnRed();
    view.showFailureMessage({ "Detail": data.status, "Other": data.statusText });
    return;
};

var completeHandler = function () {
    view.hideLoader();
};
var multiCallTerminate = function () {
    view.hideLoader();
    currentRequestIndex = 0;
};
//utility
var getCurrentTime = function () {
    var t_script = null;
    var currentTime = new Date();
    var hours = currentTime.getHours();
    var minutes = currentTime.getMinutes();
    var seconds = currentTime.getSeconds();
    if (minutes < 10) {
        minutes = "0" + minutes;
    }
    if (seconds < 10) {
        seconds = "0" + seconds;
    }
    t_script = hours + ":" + minutes + ":" + seconds;

    return t_script;
};
theObject.init = init;
theObject.execMulti = multiCallRequester;
return theObject;
};
