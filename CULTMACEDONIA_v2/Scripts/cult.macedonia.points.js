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


    //$.each(data, function (i, item) {
    var marker = new google.maps.Marker({
        'position': new google.maps.LatLng(point.x, point.y),
        'map': map,
        'title': point.name
    });

    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png');

    var infowindow = new google.maps.InfoWindow({
        content: "<div class='infoDiv'><h4>" + point.name + "</h4>" +
                    "<div><h5>Address: " + point.addr + " </h5></div>" +
                    "</div>"
    });


    google.maps.event.addListener(marker, 'click', function () {
        infowindow.open(map, marker);
    });

    //});


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

