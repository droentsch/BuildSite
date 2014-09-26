Web.CustomBuilds = Web.CustomBuilds || function (view, global, model, builder) {
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
    };

    var primeMVC = {};
    var primeDB = {};
    var primeAPI = {};
    var primeWF = {};
    var primeLINK = {};
    var purgeCache = {};
    var fileTransfer = {};
    var allBuilds = {};

    primeMVC.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeMVC",
            "/PrimeCustom/BuildCIMVCSite"
            );

        buildArgs.inputSelectors = [];
        buildArgs.inputSelectors.push({ "selector": "#branch_name", "type": "text", "dataArgName": "branch" });
        buildArgs.inputSelectors.push({ "selector": "#server_name", "type": "text", "dataArgName": "server" });
        var build = builder(buildArgs, view);
        build.init();
    };
    primeDB.ci = function () {
        var buildArgs = multipleInputArgBuilder(
            "#CI_builds .PrimeDB",
            "/PrimeCustom/BuildCIDBProject"
            );
        
        var build = builder(buildArgs, view);
        build.init();
    };

    primeAPI.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeAPI",
            "/PrimeCustom/BuildCIApiSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };

    primeWF.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeWF",
            "/PrimeCustom/BuildCIWFSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };

    primeLINK.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeLINK",
            "/PrimeCustom/BuildCILINKSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    fileTransfer.ci = function () {
        var buildArgs = multipleInputArgBuilder(
            "#CI_builds .FileTransfer",
            "/PrimeCustom/BuildCIFileTransferProject"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    allBuilds.ci = function () {
        var buildArgs = multipleInputArgBuilder(
            "#CI_builds .all",
            "/PrimeCustom/BuildCIAll"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    theObject.init = init;
    return theObject;
}(Web.view, Web.global, Web.model, Web.Builder);
