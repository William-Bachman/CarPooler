app.factory('AllTripsFactory', ['$http', '$q', '$location', function ($http, $q, $location) {

    var o = {
        tripsList:[]
        };
    o.getTrips = function (depDest) {
        var url;
        if (depDest == null)
            url = 'api/SearchTripsApi/';
        else {
            var depCity = depDest.substring(0, depDest.indexOf('&'));
            var destCity = depDest.substring(depDest.indexOf('&') + 1, depDest.length);
            url = 'api/SearchTripsApi/?depCity=' + depCity + '&destination=' + destCity;
        }

        $http({
            url: url,
            method: 'GET'
        }).success(function (data) {
            o.tripsList.length = 0;
            for (i = 0; i < data.length; i++) {
                o.tripsList[i] = data[i];
            }
            //return data; Why doesn't it work?

        }).error(function () {
        })
            
    };
    
    o.requestSpots = function (nSpots, tripId) {
        
        $http({           
            url: 'api/AddPassengerApi',
            method: 'Post',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
            data: { Username: localStorage.getItem('username'), NumSeats: nSpots, TripId:tripId}
        }).success(function () {
            alert("You have requested"+ nSpots +"spot(s) in this trip. Go to 'My Trips' to check if your request has been accepted")
            o.getTrips();

        }).error(function (data) {
            if (data.Message == "Authorization has been denied for this request.") {
                //$location.path('#Login') Doesn't work
                window.location.assign('#/Login');
            }
            else alert(data.Message);
        })
        
    };

    var getCityCoords = function (city) {
        var q = $q.defer();
        $http({
            url: 'http://maps.googleapis.com/maps/api/geocode/json?address='+city+'&sensor=true',
            method: 'GET'
        }).success(function (data) {       
            if(data.status=="ZERO_RESULTS")
                q.resolve("error");
            else if(data.status=="OK")
                q.resolve(data.results[0].geometry.location);
        }).error(function (data) {
            alert(data.Message);
        })
        return q.promise;
    }

    o.addTrips = function (trip) {
        if (trip == undefined || trip.destination == "" || trip.destination == undefined || trip.destination == null || trip.departurePoint == "" || trip.departurePoint == undefined || trip.departurePoint == null || trip.date == "" || trip.date == undefined || trip.date == null || trip.seatsAvailable == "" || trip.seatsAvailable == undefined || trip.seatsAvailable == null) {
            alert("Please fill out all the fields before submitting");
            return;
        }
        getCityCoords(trip.destination).then(function (coords) {
            if (coords == "error") {
                alert("Please enter a valid destination");
                return;
            }
            trip.username = localStorage.getItem('username');
            trip.destLatitud = coords.lat;
            trip.destLongitud = coords.lng;
            $http({
                url: 'api/TripsApi/',
                method: 'POST',
                headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
                data: trip
            }).success(function (data) {
                alert("Trip successfully added");
                trip.destination = "";
                trip.departurePoint = "";
                trip.date = "";
                trip.etd = "";
                trip.eta = "";
                trip.seatsAvailable = "";

                o.getTrips();

            }).error(function (data) {
                alert(data.Message);
            })
        });
    };

    return o;

}]);