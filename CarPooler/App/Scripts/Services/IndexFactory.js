app.factory('IndexFactory', ['$http', '$q', function ($http, $q) {

    var o = {};

    //city is the name of the city or a specific address. Returns object with lat, long and city name
    o.getCityCoords = function (city) {
        var q = $q.defer();
        $http({
            url: 'http://maps.googleapis.com/maps/api/geocode/json?address=' + city + '&sensor=true',
            method: 'GET'
        }).success(function (data) {
            if (data.status == "ZERO_RESULTS")
                q.resolve("error");
            else if (data.status == "OK")
                q.resolve({ cityName: city, latitude: data.results[0].geometry.location.lat, longitude: data.results[0].geometry.location.lng });
        }).error(function (data) {
            alert(data.Message);
        })
        return q.promise;
    }

    //position is an object with the lat and long of the city. Returns object with lat, long and city name
    o.getCity = function (position) {
        var q = $q.defer();
        $http({
            url: 'http://maps.googleapis.com/maps/api/geocode/json?latlng=' + position.coords.latitude + ',' + position.coords.longitude + '&sensor=true',
            method: 'get'
        }).success(function (data) {
            var cityName;
            var acArr = data.results[0].address_components;
            for (var i in acArr) {
                if (acArr[i].types[0] == 'locality') {
                    cityName = acArr[i].short_name;
                    break;
                }
            }
            q.resolve({ cityName: cityName, latitude: position.coords.latitude, longitude: position.coords.longitude });
        }).error(function (data) {
            console.log(data);
        })
        return q.promise;
    }


    o.getJourneys = function (depCity, destination) {
        var q = $q.defer();
        $http({
            url: 'api/IndexApi/?departureCity=' + depCity + '&destination=' + destination,
            method: 'get'
        }).success(function (data) {
            var destinations = [];
            for (i = 0; i < data.length; i++) {
                destinations[i] = data[i];
            }
            q.resolve(destinations);
        }).error(function (data) {
            alert(data.Message);
        })
        return q.promise;

    }

    return o;


}]);