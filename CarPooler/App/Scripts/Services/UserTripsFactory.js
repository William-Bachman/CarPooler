app.factory('UserTripsFactory', ['$http', '$q', '$location', '$sce', function ($http, $q, $location, $sce) {

    var o = {
        passengerTrips: [],
        driverTrips: []
    };

    o.getPassengerTrips = function () {

        $http({
            url: 'api/TripsApi/?driver='+false+'&username='+localStorage.getItem('username'),
            method: 'get',
            headers: {Authorization:'Bearer '+localStorage.getItem('token')}
        }).success(function(data){
            o.passengerTrips.length = 0;
            console.log(data);
            for (var t in data) {
                data[t].CityVid = $sce.trustAsResourceUrl(data[t].CityVid);
                o.passengerTrips.push(data[t]);
            }
            
        }).error(function(){
            $location.path('/');
        })
    };


    o.getDriverTrips = function () {
        $http({
            url: 'api/TripsApi?driver=' + true + '&username=' + localStorage.getItem('username'),
            method: 'get',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') }
        }).success(function (data) {
            o.driverTrips.length = 0;
            for (var t in data) {
                o.driverTrips.push(data[t]);
            }
            
        }).error(function () {
            $location.path('/');
        })
    };

    o.editTrip = function (newTrip) {
        newTrip.username = localStorage.getItem('username');
        $http({
            url: 'api/EditTripApi',
            method: 'post',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
            data: newTrip
        }).success(function (data) {
            $('#myTripsModal').modal('hide');
            
            //o.getDriverTrips();
            //$location.path('#/MyTrips');
            //window.location.assign('#/MyTrips');           

        }).error(function () {
            alert('the db is gone');
        })
    };

    o.deleteTrip = function (trip) {
        $http({
            url: 'api/DeleteTripApi',
            method: 'post',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
            data: trip
        }).success(function (data) {            
            o.getDriverTrips();        

        }).error(function () {
            alert('the db is gone');
        })
    };

    o.cancelRequest = function (trip) {
        trip.username = localStorage.getItem('username');
        $http({
            url: 'api/RequestTripsApi',
            method: 'post',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
            data: trip
        }).success(function (data) {
            o.getPassengerTrips();
            o.getDriverTrips();

        }).error(function () {
            alert('the db is gone');
        })
    };

    o.acceptRequest = function (username, tripId) {
        $http({
            url: 'api/RequestTripsApi',
            method: 'PUT',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
            data: { username: username, tripId: tripId }
        }).success(function (data) {
            o.getDriverTrips();
            o.getPassengerTrips();
        }).error(function () {
            alert('the db is gone');
        })
    };

    o.addReview = function (driver, stars, description) {
        $http({
            url: 'api/ReviewDriverApi',
            method: 'POST',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
            data: { driverUsername: driver, passengerUsername:localStorage.getItem('username'), stars:stars, description:description }
        }).success(function (data) {
            alert("Your review has been successfully sent");
        }).error(function () {
            alert('the db is gone');
        })
    };

    return o;

}])