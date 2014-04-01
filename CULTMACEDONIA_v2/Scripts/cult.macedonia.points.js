/// <reference path="_references.js" />


var showMapPoint = function (point) {
    
   

    google.maps.visualRefresh = true;
    var place = new google.maps.LatLng(point.x, point.y);

    var mapOptions = {
        zoom: 14,
        center: place,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);



    var marker = new google.maps.Marker({
        'position': new google.maps.LatLng(point.x, point.y),
        'map': map,
        'title': point.name
    });

    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png');

    var infowindow = new google.maps.InfoWindow({
        content: "<div class='infoDiv media'>"
            +    "  <a class='pull-left'><img class='media-object' style='width:50px; height:50px;' src='"+ point.img+"' alt=''></a><div class='media-body'><h5 class='media-heading'>"+point.name+"</h5>" + point.addr+"</div>"

    });


    google.maps.event.addListener(marker, 'mouseover', function () {
        infowindow.open(map, marker);
    });

    google.maps.event.addListener(marker, 'mouseout', function () {
        infowindow.close(map, marker);
    });




}

var showPointsInMap = function (data) {

    

}

function mailSucceded(data) {
    
    $("#status").removeClass();
    if (data.sent) {
        $("#status").addClass("alert alert-success alert-dismissable");
    } else
    {
        $("#status").addClass("alert alert-danger alert-dismissable");
    }
    $("#status").html(data.retValue);

}

function mailFailed() {
    $("#status").removeClass();
    $("#status").append("");
}

