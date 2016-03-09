app.factory('SingleTripFactory', ['$http', '$q', '$location', function ($http, $q, $location) {

    var o = {};
    var _trip;

    o.getWeather = function (city) {
        var q = $q.defer();
        $http({
            url: 'http://api.openweathermap.org/data/2.5/forecast/daily?q='+city+'&mode=json&units=metric&cnt=7',
            method: 'GET'
        }).success(function (data) {
            q.resolve(data);
        }).error(function (data) {
            alert(data);
        })
        return q.promise;
    }


    o.setTrip = function (trip) {
        _trip = trip;
    };

    o.getTrip = function () {
        return _trip;
    }

    return o;
}]);