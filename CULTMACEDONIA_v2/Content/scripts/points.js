

$(document).ready(function () {




    var oCache = {
        iCacheLower: -1
    };
    
    // οριζουμε το id ως ena jquery data Table

    // ftiaxnoyme ta aoColumns ως εξης
    /*
    [
        { } απο ενα τετοιο για καθε column
        μεσα εχει key-values οπως πχ "mData" : το πεδιο του datasource

    ]

    */

    $('#myDataTable').dataTable({
        
        "bServerSide": true,
        "bSort": true,                                                                  // Turn on Sorting for all columns
        "sPaginationType": "full_numbers",                                              // 'first', 'previous', 'next' and 'last' buttons are presented, as well as the rest pages
        "aLengthMenu": [[5, 10, 15, 25, 50, 100, -1], [5, 10, 15, 25, 50, 100, "All"]], // row count options
        "iDisplayLength": 5,                                                            // default Pagination Size
        "sAjaxSource": "GetPoints",
        "sScrolly": "200px",
        "fnServerData": fnDataTablesPipeline,
        "bProcessing": true,
        "bDeferRender": true,
        "aoColumns": [
                        {
                            "mData": "PointName",
                            "sName": "PointName",
                            "sWidth": "200px"
                            
                        },
                        {
                            "mData": "PointX",
                            "sWidth": "50px"
                        },
                        {
                            "mData": "PointY",
                            "sWidth": "50px"
                        },
                        { "mData": "PointDescription" },
                        {
                            "mData":null,
                            "sWidth": "150px",
                            "mRender": function (data,type, full) {
                                return '<a class="show-point-map" data-name="'+ full.PointName  + '" data-x="' + full.PointX +
                                    '" data-y="' + full.PointY +
                                    '" data-phone="' + full.PointPhone +
                                    '" data-email="' + full.PointEmail +
                                    '" data-addr="' + full.PointAddress +
                                    '" data-town="' + full.PointPlaceNomos +

                                    '" href="' + data + "-" + full.PointName + "-" + full.PointPhone + '"><button class="btn btn-xs btn-info"><i class="icon-exclamation-sign  bigger-120"></i></button></a>';
                            }
                        }
        ]
    });


    
    



    function fnSetKey(aoData, sKey, mValue) {
        for (var i = 0, iLen = aoData.length ; i < iLen ; i++) {
            if (aoData[i].name == sKey) {
                aoData[i].value = mValue;
            }
        }
    }

    function fnGetKey(aoData, sKey) {
        for (var i = 0, iLen = aoData.length ; i < iLen ; i++) {
            if (aoData[i].name == sKey) {
                return aoData[i].value;
            }
        }
        return null;
    }

    function fnDataTablesPipeline(sSource, aoData, fnCallback) {
        var iPipe = 5; /* Ajust the pipe size */

        var bNeedServer = false;
        var sEcho = fnGetKey(aoData, "sEcho");
        var iRequestStart = fnGetKey(aoData, "iDisplayStart");
        var iRequestLength = fnGetKey(aoData, "iDisplayLength");
        var iRequestEnd = iRequestStart + iRequestLength;
        oCache.iDisplayStart = iRequestStart;

        /* outside pipeline? */
        if (oCache.iCacheLower < 0 || iRequestStart < oCache.iCacheLower || iRequestEnd > oCache.iCacheUpper) {
            bNeedServer = true;
        }

        /* sorting etc changed? */
        if (oCache.lastRequest && !bNeedServer) {
            for (var i = 0, iLen = aoData.length ; i < iLen ; i++) {
                if (aoData[i].name != "iDisplayStart" && aoData[i].name != "iDisplayLength" && aoData[i].name != "sEcho") {
                    if (aoData[i].value != oCache.lastRequest[i].value) {
                        bNeedServer = true;
                        break;
                    }
                }
            }
        }

        /* Store the request for checking next time around */
        oCache.lastRequest = aoData.slice();

        if (bNeedServer) {
            if (iRequestStart < oCache.iCacheLower) {
                iRequestStart = iRequestStart - (iRequestLength * (iPipe - 1));
                if (iRequestStart < 0) {
                    iRequestStart = 0;
                }
            }

            oCache.iCacheLower = iRequestStart;
            oCache.iCacheUpper = iRequestStart + (iRequestLength * iPipe);
            oCache.iDisplayLength = fnGetKey(aoData, "iDisplayLength");
            fnSetKey(aoData, "iDisplayStart", iRequestStart);
            fnSetKey(aoData, "iDisplayLength", iRequestLength * iPipe);

            $.getJSON(sSource, aoData, function (json) {
                /* Callback processing */
                oCache.lastJson = jQuery.extend(true, {}, json);

                if (oCache.iCacheLower != oCache.iDisplayStart) {
                    json.aaData.splice(0, oCache.iDisplayStart - oCache.iCacheLower);
                }
                json.aaData.splice(oCache.iDisplayLength, json.aaData.length);

                fnCallback(json)
            });
        }
        else {
            json = jQuery.extend(true, {}, oCache.lastJson);
            json.sEcho = sEcho; /* Update the echo for each response */
            json.aaData.splice(0, iRequestStart - oCache.iCacheLower);
            json.aaData.splice(iRequestLength, json.aaData.length);
            fnCallback(json);
            return;
        }
    }

});


