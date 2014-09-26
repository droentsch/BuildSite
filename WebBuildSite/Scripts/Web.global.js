var Web = Web || {};
Web.global = Web.gobal || function () {
    var theObject = {};
    var exists = function (ob) {
        if (typeof (ob) == "undefined") return false;
        if (ob === null) return false;
        if (ob.jquery && ob.length == 0) return false;
        return true;
    };

    var CALLTYPES = {};
    CALLTYPES.BUTTON_WITH_INPUT = 1;
    CALLTYPES.BUILD_BUTTON = 2;
    CALLTYPES.MULTI_CALL = 3;
    CALLTYPES.OPTIONS_ON_DBLCLICK = 4;

    var Refresh = {};
    Refresh.Interval = null;

    theObject.exists = exists;
    theObject.CALLTYPES = CALLTYPES;
    return theObject;
} ();