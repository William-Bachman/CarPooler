app.controller('SingleTripController', ['$scope', 'SingleTripFactory', 'IndexFactory', function ($scope, SingleTripFactory, IndexFactory) {

    var singleTrip=SingleTripFactory.getTrip();

    IndexFactory.getCityCoords(singleTrip.DepPoint).then(function (position) {
        var directionsService = new google.maps.DirectionsService();
        $scope.mapOptions = {
            center: new google.maps.LatLng(position.latitude, position.longitude),
            zoom: 4,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        var map = new google.maps.Map(document.getElementById('googleMapViewTrip'), $scope.mapOptions);
        var directionsDisplay = new google.maps.DirectionsRenderer();
        directionsDisplay.setMap(map);
        var infoWindow = new google.maps.InfoWindow();

        var request = {
            origin: singleTrip.DepPoint,
            destination: singleTrip.Destination,
            travelMode: google.maps.TravelMode.DRIVING
        };
        directionsService.route(request, function (result, status) {
            if (status == google.maps.DirectionsStatus.OK) {               
                directionsDisplay.setDirections(result);
            }
        });

        SingleTripFactory.getWeather(singleTrip.Destination).then(function (data) {
            console.log(data);
            var today = Date.now();
            var dw = (new Date(today)).getDay();
            var daysOfTheWeek = [];
            
            switch (dw) {
                case 1:
                    daysOfTheWeek[0] = "Monday"; daysOfTheWeek[1] = "Tuesday"; daysOfTheWeek[2] = "Wednesday"; daysOfTheWeek[3] = "Thursday"; daysOfTheWeek[4] = "Friday"; daysOfTheWeek[5] = "Saturday"; daysOfTheWeek[6] = "Sunday";
                    break;
                case 2:
                    daysOfTheWeek[0] = "Tuesday"; daysOfTheWeek[1] = "Wednesday"; daysOfTheWeek[2] = "Thursday"; daysOfTheWeek[3] = "Friday"; daysOfTheWeek[4] = "Saturday"; daysOfTheWeek[5] = "Sunday"; daysOfTheWeek[6] = "Monday";
                    console.log(dw);
                    break;
                case 3:
                    daysOfTheWeek[0] = "Wednesday"; daysOfTheWeek[1] = "Thursday"; daysOfTheWeek[2] = "Friday"; daysOfTheWeek[3] = "Saturday"; daysOfTheWeek[4] = "Sunday"; daysOfTheWeek[5] = "Monday"; daysOfTheWeek[6] = "Tuesday";
                    break;
                case 4:
                    daysOfTheWeek[0] = "Thursday"; daysOfTheWeek[1] = "Friday"; daysOfTheWeek[2] = "Saturday"; daysOfTheWeek[3] = "Sunday"; daysOfTheWeek[4] = "Monday"; daysOfTheWeek[5] = "Tuesday"; daysOfTheWeek[6] = "Wednesday";
                    break;
                case 5:
                    daysOfTheWeek[0] = "Friday"; daysOfTheWeek[1] = "Saturday"; daysOfTheWeek[2] = "Sunday"; daysOfTheWeek[3] = "Monday"; daysOfTheWeek[4] = "Tuesday"; daysOfTheWeek[5] = "Wednesday"; daysOfTheWeek[6] = "Thursday";
                    break;
                case 6:
                    daysOfTheWeek[0] = "Saturday"; daysOfTheWeek[1] = "Sunday"; daysOfTheWeek[2] = "Monday"; daysOfTheWeek[3] = "Tuesday"; daysOfTheWeek[4] = "Wednesday"; daysOfTheWeek[5] = "Thursday"; daysOfTheWeek[6] = "Friday";
                    break;
                case 7:
                    daysOfTheWeek[0] = "Sunday"; daysOfTheWeek[1] = "Monday"; daysOfTheWeek[2] = "Tuesday"; daysOfTheWeek[3] = "Wednesday"; daysOfTheWeek[4] = "Thursday"; daysOfTheWeek[5] = "Friday"; daysOfTheWeek[6] = "Saturday";
                    break;
            }
            
            var weatherForecast = "";
            for (var day in data.list) {
                weatherForecast += daysOfTheWeek[day] + '<img src="http://openweathermap.org/img/w/' + data.list[day].weather[0].icon + '.png"/>   <span style="font-size:15px;text-align:center">High <span class="label label-warning">' + Math.round(data.list[day].temp.max) + '</span></span>    <span style="font-size:15px;text-align:center">Low <span class="label label-default">' + Math.round(data.list[day].temp.min) + '</span></span></br>'
            }
            var marker1 = new google.maps.Marker({
                map: map,
                title: "Weather Forecast",
                content: weatherForecast,
                position: new google.maps.LatLng(data.city.coord.lat+0.5, data.city.coord.lon),
                icon: "http://openweathermap.org/img/w/"+data.list[0].weather[0].icon+".png"
            });
            google.maps.event.addListener(marker1, 'click', function () {
                infoWindow.setContent('<h3>' + marker1.title + '</h3>' + marker1.content);
                infoWindow.open(map, marker1);
            });

        });


        /*
        var marker2 = new google.maps.Marker({
            map: map,
            title: singleTrip.Destination,
            content: '<h4 style="color:green">' + singleTrip.Destination + '</h4>',
            position: new google.maps.LatLng(singleTrip.DestLat, singleTrip.DestLong),
            icon: "/Content/Images/marker.png"
        });

        google.maps.event.addListener(marker2, 'click', function () {
            infoWindow.setContent('<h2>' + marker2.title + '</h2>' + marker2.content);
            infoWindow.open(map, marker2);
        });
        */

    });

  //  $scope.GetSingleTrip = function (tripId) {
    //    SingleTripFactory.GetSingleTrip(tripId);
   // }

    //Just Added 6/14/2015. This Map is going to need to be updated so that we only show the trip in question AND we show the road directions from the origin
    //to the destination as well as any other pertinent data we can get from the API
    /*
    var createMap = function (userLocation, DeparturePoint, destination) {
        console.log(DeparturePoint, destination);
            $scope.mapOptions = {
                center: new google.maps.LatLng(userLocation.latitude, userLocation.longitude),
                zoom: 4,
                mapTypeId: google.maps.MapTypeId.ROADMAP
              
            };

            var map = new google.maps.Map(document.getElementById('googleMapViewTrip'), $scope.mapOptions);

            var infoWindow = new google.maps.InfoWindow();

            var createMarker = function (destination, DeparturePoint, markerIndex) {
                var a = "/Content/Images/marker" + ((markerIndex == 0) ? "1" : "") + ".png";
                var marker = new google.maps.Marker({
                    map: map,
                    title: destination.Destination,
                    content: '<h4 style="color:green">' + destination.Count + 'trip(s)</h4></br><a href="#/AllTrips/' + DeparturePoint + '&' + destination.Destination + '">See trips</a>',
                    position: new google.maps.LatLng(destination.DestLatitud, destination.DestLongitud),
                    icon: "/Content/Images/marker" + ((markerIndex == 0) ? "1" : "") + ".png"
                })
                createMarker(destination, DeparturePoint, 0);
            }
   //         var createDepPointMarker = function (destination, DeparturePoint, markerIndex) {
    //            var a = "/Content/Images/marker" + ((markerIndex == 0) ? "1" : "") + ".png";
    //            var marker = new google.maps.Marker({
  //                map: map,
                //    title: destination.Destination,
               //     content: '<h4 style="color:green">' + destination.Count + 'trip(s)</h4></br><a href="#/AllTrips/' + DeparturePoint + '&' + destination.Destination + '">See trips</a>',
                    //NOTE: ONLY FOR TEST. To be replaced by actual departure point locations hard coded into DB based on actual dep point picked by Driver
       //             position: new google.maps.LatLng(userLocation.latitude, userLocation.longitude),
       //             icon: "/Content/Images/marker" + ((markerIndex == 0) ? "1" : "") + ".png"
       //         })

       //    }
           
       //     createDepPointMarker(destination, DeparturePoint, 0);
        }

        createMap({ latitude: 36.169941, longitude: -115.139830 }, SingleTripFactory.SingleTrip[0].DeparturePoint, SingleTripFactory.SingleTrip[0]);
    
        
        */
}]);