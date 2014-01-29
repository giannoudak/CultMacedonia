using CULTMACEDONIA_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CULTMACEDONIA_v2.Helpers
{

    public enum GoogleMapType
    {
        ROADMAP, SATELLITE, HYBRID, TERRAIN
    }

    public static class GoogleMapHelpers
    {
        private const string GOOGLE_MAP_API = @"<script src='https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false'></script>";
        private const string GOOGLE_MAP_API_WITH_GEOMETRY = @"<script src='https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&libraries=geometry'></script>";

        /// <summary>
        /// Displays a google map based on the address user entered
        /// </summary>
        /// <param name="html"></param>
        /// <param name="address">The address you would like to mark on the map</param>
        /// <param name="width">The width of the map container in Pixels</param>
        /// <param name="height">The height of the map container in Pixels</param>
        /// <param name="zoom">The zoom (1-20) value</param>
        /// <returns></returns>
        public static MvcHtmlString GoogleMap(this HtmlHelper html, PointMapViewModel point, int width, int height, int zoom)
        {
            // Default map type is set to ROADMAP
            return GoogleMap(html, point, width, height, zoom, GoogleMapType.ROADMAP);
        }

        /// <summary>
        /// Displays a google map based on the address user entered with MapType
        /// </summary>
        /// <param name="html"></param>
        /// <param name="address">The address you would like to mark on the map</param>
        /// <param name="width">The width of the map container in Pixels</param>
        /// <param name="height">The height of the map container in Pixels</param>
        /// <param name="zoom">The zoom (1-20) value</param>
        /// <param name="mapType">The type of map</param>
        /// <returns></returns>
        public static MvcHtmlString GoogleMap(this HtmlHelper html, PointMapViewModel point, int width, int height, int zoom, GoogleMapType mapType)
        {
            string mapscript = string.Empty;
            //StringBuilder buffer = new StringBuilder();
            //buffer.Append(GOOGLE_MAP_API);
            mapscript += @"
            <script>
                var map;
                $(document).ready(function(){
                    initialize();
var currentCenter = map.getCenter();  // Get current center before resizing
        google.maps.event.trigger(map, ""resize"");
        map.setCenter(currentCenter); // Re-set previous center
                });
						
                function initialize() {
				    
                    google.maps.visualRefresh = true;

                     var myCenter = new google.maps.LatLng(" + point.GeoLong + "," + point.GeoLat + @");
                     var marker = new google.maps.Marker({
                         position: myCenter
                     });


                     var mapProp = {
                                center: myCenter,
                                zoom: 14,
                                draggable: true,
                                scrollwheel: false,
                                mapTypeId: google.maps.MapTypeId.ROADMAP
                            };

                            map = new google.maps.Map(document.getElementById('map-canvas'), mapProp);
                            marker.setMap(map);

                     marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png');

                    
                   
                    
                    var infowindow = new google.maps.InfoWindow({
                        content : ""<div class='infoDiv'><h2>" + point.PlaceAddress + @"</h2></div>""
                    });

                    google.maps.event.addListener(marker, 'click', function () {
                        infowindow.open(map, marker);
                    });

					
                }
                // google.maps.event.addDomListener(window, 'load', initialize);
           </script>";
           

            MvcHtmlString mvcHtmlString = new MvcHtmlString(mapscript);
            return mvcHtmlString;

        }

        /// <summary>
        /// Display a google map with multiple markers
        /// </summary>
        /// <param name="html"></param>
        /// <param name="address">Array of addresses to be marked on the map</param>
        /// <param name="width">The width of the google map conatiner</param>
        /// <param name="height">The height of the google map container</param>
        /// <param name="zoom">The zoom (1-20) value</param>
        /// <returns></returns>
        public static MvcHtmlString GoogleMap(this HtmlHelper html, List<PointMapViewModel> points, int? width, int height)
        {
            String mapScript = string.Empty;
            //StringBuilder buffer = new StringBuilder();
            //buffer.Append(GOOGLE_MAP_API);
            mapScript = @"
                <script>
                var map;
				var geocoder;
                var bounds;
							
                function initialize() {
				    
					google.maps.visualRefresh = true;
                    var Thessaloniki = new google.maps.LatLng(40.602305, 22.974472);

                    var mapOptions = { 
                        zoom: 14,
                        center: Thessaloniki,
                        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
                    };
								
                    map = new google.maps.Map(document.getElementById('map_canvas_new'), mapOptions);

                    ";

                    foreach(PointMapViewModel point in points){

                        mapScript += @"
                        var marker = new google.maps.Marker({
                            'position': new google.maps.LatLng('" + point.GeoLong + "','" + point.GeoLat + @"'),
                            'map': map,
                            'title': '" + point.PlaceName + @"'
                        });

                        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png');

                        var infowindow = new google.maps.InfoWindow({
                                    content: '" + point.PlaceName +@"'    });


                        google.maps.event.addListener(marker, 'click', function () {
                            infowindow.open(map, marker);
                        });";


                    }// foreach ends here



                mapScript += @"
                }
                google.maps.event.addDomListener(window, 'load', initialize);
            </script>
            <div class=""jumbotron"" id='map_canvas_new' style='width: " + width + "px; height: " + height + "px;'></div>";

            MvcHtmlString mvcHtmlString = new MvcHtmlString(mapScript);
            return mvcHtmlString;
        }

        /// <summary>
        /// Attempts to auto-detect the user's position using HTML geolocation,
        /// If that fails, will prompt user for an address.
        /// Then it will find address in radius from the address user entered.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="addresses">The address to search from</param>
        /// <param name="radius">The radius to search</param>
        /// <param name="width">The width of the map</param>
        /// <param name="height">The hight of the map</param>
        /// <returns></returns>
        public static MvcHtmlString SearchNearBy(this HtmlHelper html, string[] addresses, int radius, int width, int height)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(GOOGLE_MAP_API_WITH_GEOMETRY);
            buffer.Append(@"
            <script>                            
            	var map;
			    var geocoder;
				var bounds;
				var origin;
				var currentLocation;
				var userAddress;   
				var pictureIndex;
                var infoWindow;
							
                function initialize() {
                    pictureIndex = 0;
                    geocoder = new google.maps.Geocoder();
                    bounds = new google.maps.LatLngBounds();
                    infowindow = new google.maps.InfoWindow(); 

                    var mapOptions = {
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };

                    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
								
                    if (navigator.geolocation) {
					    navigatorError = false;
                        var location_timeout = setTimeout(function(){getUserAddress(true);}, 3000);
									
					    navigator.geolocation.getCurrentPosition(
						    function(position) {
							    clearTimeout(location_timeout);
                                origin = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                                bounds.extend(origin);
										
                                geocoder.geocode({'latLng': origin}, function(results, status) { 
                                    userAddress = results[1].formatted_address;
                                });

					            var marker = new google.maps.Marker({
                                    position: origin,
					                map: map,
					                icon: 'http://maps.google.com/mapfiles/kml/paddle/blu-blank.png'
					            });
			
								CreateMakers();
                            },

									function() {
										clearTimeout(location_timeout);
										navigatorError = true;
										getUserAddress(true);
                                    });
                                } else {
									navigatorError = true;
									getUserAddress(true);
								}
								
								function getUserAddress(navigatorError) {
									if (navigatorError == true)
									{
										userAddress = prompt('Something has gone wrong.\nPlease enter your address.\nOr close this prompt and enable geolocation tracking.', 'Your address...');
										if (navigatorError == true) {
												geocoder.geocode({ 'address': userAddress.toString() }, function (results, status) {
												origin = new google.maps.LatLng(results[0].geometry.location.lat(),
												results[0].geometry.location.lng());

													bounds.extend(origin);	
													var marker = new google.maps.Marker({

													  position: results[0].geometry.location,
													  map: map,
													  icon: 'http://maps.google.com/mapfiles/kml/paddle/blu-blank.png'
													});
											});
										}
									}									

									CreateMakers();
								}
								
								function CreateMakers() {");

            buffer.Append(CreateMarkers(addresses, radius));
            buffer.Append(@"}}
                            google.maps.event.addDomListener(window, 'load', initialize);
                        </script>
                        <div id='map-canvas' style='width: " + width + "px; height: " + height + "px;'></div>");

            MvcHtmlString mvcHtmlString = new MvcHtmlString(buffer.ToString());
            return mvcHtmlString;
        }

        /// <summary>
        /// Search an address within a radius from multiple addresses 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="addressSearching">The address to search</param>
        /// <param name="addressesSearchFrom">The address to search from</param>
        /// <param name="radius">The radius to search</param>
        /// <param name="width">The width of the map</param>
        /// <param name="height">The hight of the map</param>
        /// <returns></returns>
        public static MvcHtmlString SearchByRadius(this HtmlHelper html, string addressSearching, string[] addressesSearchFrom, int radius, int width, int height)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(GOOGLE_MAP_API_WITH_GEOMETRY);
            buffer.Append(@"
            <script>
                var map;
                var geocoder;
                var bounds;
                var origin;
                var currentLocation;
                var pictureIndex;
                var userAddress;
                var infowindow;
							
                function initialize() {
                    pictureIndex = 0;
                    infowindow = new google.maps.InfoWindow(); 
                    geocoder = new google.maps.Geocoder();
                    bounds = new google.maps.LatLngBounds();
                    userAddress = '" + addressSearching + @"';

                    var mapOptions = {
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };

                    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

                    geocoder.geocode({ 'address': '" + addressSearching + @"' }, function (results, status) {
                        origin = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());
			            bounds.extend(origin);	
	                    var marker = new google.maps.Marker({
                            position: results[0].geometry.location,
		                    map: map,
		                    icon: 'http://maps.google.com/mapfiles/kml/paddle/blu-blank.png'
		                });
                    });");

            buffer.Append(CreateMarkers(addressesSearchFrom, radius));
            buffer.Append(@"
                }
                google.maps.event.addDomListener(window, 'load', initialize);
            </script>
            <div id='map-canvas' style='width: " + width + "px; height: " + height + "px;'></div>");

            MvcHtmlString mvcHtmlString = new MvcHtmlString(buffer.ToString());
            return mvcHtmlString;
        }


        /// <summary>
        /// Override CreateMarkers ωστε να δέχεται ένα πίνακα με PointMapViewModels
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static string CreateMarkers(List<PointMapViewModel> points)
        {
            StringBuilder sb = new StringBuilder();

            foreach (PointMapViewModel point in points)
            {
                sb.Append(@"bounds.extend(new google.maps.LatLng(results[0].geometry.location.lat(),
																		results[0].geometry.location.lng()))
                    
                            var marker = new google.maps.Marker({
                                map: map,
                                position: new google.maps.LatLng('" + point.GeoLat + @"', '" + point.GeoLong + @"' )
                            });

                            var infowindow = new google.maps.InfoWindow({
                            content: '" + point.PlaceName + @"'     });

                            google.maps.event.addListener(marker, 'click', function() {
                                alert('clicked');
                                       
                                        
                                infowindow.open(map, marker);
                            });

                            map.fitBounds(bounds);
                        ");

            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates markers for each addresses in string array
        /// </summary>
        /// <param name="addresses">Array of addresses</param>
        /// <returns>Google map marker tag</returns>
        private static string CreateMarkers(string[] addresses)
        {
            StringBuilder sb = new StringBuilder();

            string s  ="petsa";

            for (int index = 0; index < addresses.Length; index++)
            {
                

                sb.Append(@"geocoder.geocode({'address': '" + addresses[index] + @"'}, function(results, status) {
                                    bounds.extend(new google.maps.LatLng(results[0].geometry.location.lat(),
																		results[0].geometry.location.lng())
                                    );
                                    var marker = new google.maps.Marker({
                                        map: map,
                                        position: results[0].geometry.location
                                    });

                                    var infowindow = new google.maps.InfoWindow({
                                    content: '" + s + @"'     });

                                    google.maps.event.addListener(marker, 'click', function() {
                                        alert('clicked');
                                       
                                        
                                        infowindow.open(map, marker);
                                    });

                            map.fitBounds(bounds);
                           });");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Creates a marker if it is within a raidus of an address user provided
        /// </summary>
        /// <param name="addresses">The array of addresses to search from</param>
        /// <param name="radius">The target radius</param>
        /// <returns></returns>
        private static string CreateMarkers(string[] addresses, int radius)
        {
            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < addresses.Length; index++)
            {
                sb.Append(@"geocoder.geocode({ 'address': '" + addresses[index] + @"' }, function (results, status) {
                                currentLocation = new google.maps.LatLng(results[0].geometry.location.lat(),
                                                                    results[0].geometry.location.lng());

                                var distance = google.maps.geometry.spherical.computeDistanceBetween(origin, currentLocation) * 0.000621371;
                                if (distance < " + radius + @") {
                                    pictureIndex++;
                                    bounds.extend(currentLocation);

                                    var marker = new google.maps.Marker({
                                        map: map,
                                        position: results[0].geometry.location,
                                        icon: 'http://maps.google.com/mapfiles/kml/paddle/' + pictureIndex.toString() + '.png'
                                    });
                                    google.maps.event.addListener(marker, 'click', function() {
                                        infowindow.setContent(" +
                                        BuildInfoWindow() +
                                        GenerateGoogleDirectionLink(addresses[index]) + "\" + userAddress + \"" +
                                        FinishBuildingInfoWindow() + @");
                                        infowindow.open(map, marker);
                                    });
                                }
                                map.fitBounds(bounds);

                            });");
            }

            return sb.ToString();
        }

        private static string BuildInfoWindow()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"<p>Distance: \" + distance.toFixed(2) + \" miles<br/><a href=");
            return sb.ToString();
        }

        private static string GenerateGoogleDirectionLink(string destination)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("'https://maps.google.com/maps?daddr=");
            sb.Append(destination.Replace(" ", "+"));
            sb.Append("&saddr=");
            return sb.ToString();
        }

        private static string FinishBuildingInfoWindow()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'>Get Direction</a></p>\"");
            return sb.ToString();
        }
    }
}