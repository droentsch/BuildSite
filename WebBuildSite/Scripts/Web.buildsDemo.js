﻿Web.demoBuilds = Web.demoBuilds || function (view, global, model, builder) {
    var theObject = {};
    var buildType = null;
    var RECIPIENTS = "recipients";

    var ArgBuilder = function (button, service, refresh, success, failure, complete, callType, inputSelector, dataArgsName) {
        return {
            "serviceAddress": service || null,
            "buttonSelector": button || null,
            "refreshAddress": refresh || null,
            "successHandler": success || null,
            "failureHandler": failure || null,
            "completeHandler": complete || null,
            "callType": callType || null,
            "inputSelector": inputSelector || null,
            "dataArgsName": dataArgsName || null
        }
    };
    var serverArgBuilder = function (button, service, refresh, success, failure, complete, callType, inputSelector, dataArgsName) {
        return {
            "serviceAddress": service || null,
            "buttonSelector": button || null,
            "refreshAddress": refresh || null,
            "successHandler": success || null,
            "failureHandler": failure || null,
            "completeHandler": complete || null,
            "callType": callType || null,
            "inputSelector": inputSelector || null,
            "dataArgsName": dataArgsName || null
        }
    };
    var multipleInputArgBuilder = function (button, service, refresh, success, failure, complete, callType, inputSelectors, dataArgsName) {
        return {
            "serviceAddress": service || null,
            "buttonSelector": button || null,
            "refreshAddress": refresh || null,
            "successHandler": success || null,
            "failureHandler": failure || null,
            "completeHandler": complete || null,
            "callType": callType || global.CALLTYPES.BUTTON_WITH_INPUT,
            "inputSelectors": inputSelectors ||null,
            "dataArgsNames": dataArgsName || null
        }
    };
    var init = function (add_recipients_input_selector) {
        view.init();
        //call Builder for each build here
        primeMVC.ci();
        primeDB.ci();
        primeLINK.ci();
        primeWF.ci();
        fileTransfer.ci();
        allBuilds.ci();
        //register server controls
        purgeCache.ci();
        resetIIS.ci();
    };

    var primeMVC = {};
    var primeDB = {};
    var primeAPI = {};
    var primeWF = {};
    var primeLINK = {};
    var purgeCache = {};
    var fileTransfer = {};
    var resetIIS = {};
    var allBuilds = {};

    primeMVC.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeMVC",
            "/PrimeDemo/BuildCIMVCSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    primeDB.ci = function () {
        var buildArgs = multipleInputArgBuilder(
            "#CI_builds .PrimeDB",
            "/PrimeDemo/BuildCIDBProject"
            );
        
        var build = builder(buildArgs, view);
        build.init();
    };

    primeAPI.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeAPI",
            "/PrimeDemo/BuildCIApiSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };

    primeWF.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeWF",
            "/PrimeDemo/BuildCIWFSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };

    primeLINK.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeLINK",
            "/PrimeDemo/BuildCILINKSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    fileTransfer.ci = function () {
        var buildArgs = multipleInputArgBuilder(
            "#CI_builds .FileTransfer",
            "/PrimeDemo/BuildCIFileTransferProject"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    allBuilds.ci = function () {
        var buildArgs = multipleInputArgBuilder(
            "#CI_builds .all",
            "/PrimeDemo/BuildCIAll"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    purgeCache.ci = function () {
        var buildArgs = serverArgBuilder(
            "#CI_builds .purgeCICache",
            "/Server/ClearCICache"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    resetIIS.ci = function () {
        var buildArgs = serverArgBuilder(
            "#CI_builds .resetCIIIS",
            "/Server/ResetCIIIS"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    theObject.init = init;
    return theObject;
}(Web.view, Web.global, Web.model, Web.Builder);
