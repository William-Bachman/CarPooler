app.controller('IndexController', ['$scope', 'IndexFactory', function ($scope, IndexFactory) {

    $scope.markers = [];

    //NEEDS REVIEWING
    IndexFactory.getJourneys("","").then(function (destinations) {
        $scope.allDestinations = destinations;
        $scope.destinationsOnly = [], $scope.depPointsOnly = [];
        for(i=0;i<destinations.length;i++){
            $scope.destinationsOnly[i] = Object.create(destinations[i]);
            $scope.destinationsOnly[i].DeparturePoint = ""; //overrides the DeparturePoint property in __proto__
            $scope.destinationsOnly[i].index = i;
            $scope.depPointsOnly[i] = Object.create(destinations[i]);
            $scope.depPointsOnly[i].Destination = ""; //overrides the Destination property in __proto__
            $scope.depPointsOnly[i].index = i;
        }
        
    });

    $scope.getJourneys = function (trip) {
        //departurePoint: {cityName,latitude,longitude}, destinationCityName: String (Only this can be null), IsDepPointSet:bool
        //true: we have a departure point, false: we don't have a departure point
        var createMap = function (departurePoint, destinationCityName, defaultCenterCoords) {
            var map, infowindow;
            function drawMap(centerCoords) {
                $scope.mapOptions = {
                    center: new google.maps.LatLng(centerCoords.lat, centerCoords.long),
                    zoom: 4,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                map = new google.maps.Map(document.getElementById('googleMap'), $scope.mapOptions);

                infoWindow = new google.maps.InfoWindow();

               
            }

            //destination:{Destination,DestLatitud,DestLongitud, DeparturePoint, NumTripsDest, NumTripsDepDest}, depCity{cityName}), markerIndex: index of the item 
            //in the 'sorted by num of trips to specified destination' list 
            var createMarker = function (destination, depCity, markerIndex) {
                var a = "/Content/Images/marker" + ((markerIndex == 0) ? "1" : "") + ".png";
                var marker = new google.maps.Marker({
                    map: map,
                    title: destination.Destination,
                    content: '<h4 style="color:green">' + (departurePoint ? destination.NumTripsDepDest : destination.NumTripsDest) + ' trip(s) to <i>' + destination.Destination + '</i><br/>Departing from <i>' + (departurePoint ? destination.DeparturePoint : 'Anywere') + '</i><br/><br/><a href="#/AllTrips/' + (departurePoint ? depCity.cityName : '') + '&' + destination.Destination + '">See trips</a></h4>',
                    position: new google.maps.LatLng(destination.DestLatitud, destination.DestLongitud),
                    icon: "/Content/Images/marker" + ((markerIndex == 0) ? "1" : "") + ".png"
                })

                google.maps.event.addListener(marker, 'click', function () {
                    infoWindow.setContent('<h2>' + marker.title + '</h2>' + marker.content);
                    infoWindow.open(map, marker);
                });

            }

            IndexFactory.getJourneys(departurePoint?departurePoint.cityName:'', destinationCityName).then(function (destinations) {

                var centerCoords = {};
                if (departurePoint && !destinationCityName) {
                    centerCoords.lat = departurePoint.latitude, centerCoords.long = departurePoint.longitude;
                    drawMap(centerCoords);
                }
                if ((!departurePoint || (departurePoint&&destinationCityName))&&destinations.length!=0) {
                    centerCoords.lat = destinations[0].DestLatitud; centerCoords.long = destinations[0].DestLongitud;
                    drawMap(centerCoords);
                }
                if ((!destinationCityName && !departurePoint) || destinations.length == 0) {
                    
                    navigator.geolocation.getCurrentPosition(function (position) {
                        centerCoords.lat = position.coords.latitude; centerCoords.long = position.coords.longitude;
                        drawMap(centerCoords);
                    });
                }
                           
                
                for (i = 0; i < destinations.length; i++) {
                    createMarker(destinations[i], departurePoint, i);
                }
                
            });

        }

        //Get user's city name
        var getUserLocation = function () {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    IndexFactory.getCity(position).then(function (userLocation) {
                        createMap(userLocation,'');
                    });
                });
            } else {
                console.log("Geolocation not supported");
            }
        }
        
        //trip object is set to a value if we use the input fields to enter a depPoint&destination
        if (trip == undefined) {
            getUserLocation();
        } else if (trip.departurePoint == undefined || trip.departurePoint == '') {
            createMap(null, trip.destination);
        }else{
            IndexFactory.getCityCoords(trip.departurePoint).then(function (departurePointLocation) {
                createMap(departurePointLocation, trip.destination);
            });          
        }

    }


    /*(function (data) {
    google.maps.event.addListener($scope.markers[i], 'click', function () {
        //console.log(controller_data[i].Destination); //Always shows data[1]
        $('#myModal').modal('toggle');
        document.getElementsByClassName('modal-body')[0].innerHTML = controller_data[i].Destination;
    });
    //})(data[i]);
    //console.log(data[i])
    $scope.departureCity = data.city;
    */



}]);