Web.model = Web.model || function () {
    var theObject = {};

    var ajax_params = {
        url: "",
        successHandler: null,
        failureHandler: null,
        completeHandler: null,
        dataArgs: null
    };

    var getData = function () {
        $.ajax({
            url: ajax_params.url,
            type: "POST",
            dataType: "json",
            data: ajax_params.dataArgs,
            success: ajax_params.successHandler,
            error: ajax_params.failureHandler,
            complete: ajax_params.completeHandler
        });
    };

    theObject.params = ajax_params;
    theObject.getData = getData;
    return theObject;
} ();
