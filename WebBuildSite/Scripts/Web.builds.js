Web.Devbuilds = Web.Devbuilds || function (view, global, model, builder) {
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
        primeMVC.qa();
        primeDB.ci();
        primeDB.qa();
        primeLINK.ci();
        primeLINK.qa();
        primeWF.ci();
        primeWF.qa();
        fileTransfer.ci();
        fileTransfer.qa();
        allBuilds.ci();
        allBuilds.qa();
        //register server controls
        purgeCache.ci();
        resetIIS.ci();
        purgeCache.qa();
        resetIIS.qa();
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
            "/PrimeDev/BuildCIMVCSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };

    primeMVC.qa = function () {
        var buildArgs = ArgBuilder(
            "#QA_builds .PrimeMVC",
            "/PrimeDev/BuildQAMVCSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };

    primeDB.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeDB",
            "/PrimeDev/BuildCIDBProject"
            );
        
        var build = builder(buildArgs, view);
        build.init();
    };

    primeDB.qa = function () {
        var buildArgs = ArgBuilder(
            "#QA_builds .PrimeDB",
            "/PrimeDev/BuildQADBProject"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    primeWF.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeWF",
            "/PrimeDev/BuildCIWFSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };

    primeWF.qa = function () {
        var buildArgs = ArgBuilder(
            "#QA_builds .PrimeWF",
            "/PrimeDev/BuildQAWFSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    primeLINK.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .PrimeLINK",
            "/PrimeDev/BuildCILINKSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    primeLINK.qa = function () {
        var buildArgs = ArgBuilder(
            "#QA_builds .PrimeLINK",
            "/PrimeDev/BuildQALINKSite"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    fileTransfer.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .FileTransfer",
            "/PrimeDev/BuildCIFileTransferProject"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    fileTransfer.qa = function () {
        var buildArgs = ArgBuilder(
            "#QA_builds .FileTransfer",
            "/PrimeDev/BuildQAFileTransferProject"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    allBuilds.ci = function () {
        var buildArgs = ArgBuilder(
            "#CI_builds .all",
            "/PrimeDev/BuildCIAll"
            );

        var build = builder(buildArgs, view);
        build.init();
    };

    allBuilds.qa = function () {
        var buildArgs = ArgBuilder(
            "#QA_builds .all",
            "/PrimeDev/BuildQAAll"
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
    purgeCache.qa = function () {
        var buildArgs = serverArgBuilder(
            "#QA_builds .purgeQACache",
            "/Server/ClearQACache"
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
    resetIIS.qa = function () {
        var buildArgs = serverArgBuilder(
            "#QA_builds .resetQAIIS",
            "/Server/ResetQAIIS"
            );

        var build = builder(buildArgs, view);
        build.init();
    };
    theObject.init = init;
    return theObject;
}(Web.view, Web.global, Web.model, Web.Builder);
