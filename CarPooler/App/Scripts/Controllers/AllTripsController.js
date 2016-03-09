app.controller('AllTripsController', ['$scope', 'AllTripsFactory', '$routeParams', function ($scope, AllTripsFactory, $routeParams) {

    $scope.date1 = new Date();

    $scope.showWeeks = true;

    $scope.datepickerOptions = {
        format: 'yyyy-mm-dd',
        language: 'fr',
        autoclose: true,
        weekStart: 0
    };

    $scope.tripsList = AllTripsFactory.tripsList;

    $scope.getArray = function (num) {
        arr = new Array(num);
        for (i = 0; i < arr.length; i++) {
            arr[i] = i+1;
        }
        return arr;
    };
    
    if ($routeParams.Dest == "All") {
        AllTripsFactory.getTrips();
    } else {
        AllTripsFactory.getTrips($routeParams.Dest);
    }

    $scope.addTrips = function () {
        $scope.trip.date = $scope.date1;
        AllTripsFactory.addTrips($scope.trip);
    }

    $scope.requestSpots = function (tripId, nSpots) {
        AllTripsFactory.requestSpots(nSpots, tripId);
    }

}]);