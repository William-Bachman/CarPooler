app.controller('UserTripsController', ['$scope', 'UserTripsFactory', 'SingleTripFactory', 'IndexFactory', '$location', '$routeParams', function ($scope, UserTripsFactory, SingleTripFactory, IndexFactory, $location, $routeParams) {

    $scope.passengerTrips = UserTripsFactory.passengerTrips;
    UserTripsFactory.getPassengerTrips();

    $scope.getConfirmedUsersHtml = function (users) {
        var htmlEmbedded = "";
        var usersArr = [];
        var subStr = users;
        while ((subStr.indexOf('"Username":"') != -1)) {
            subStr = subStr.substring(subStr.indexOf('"Username":"') + 12);
            var username = subStr.substring(0, subStr.indexOf('"'));
            usersArr.push(username);
        }

        for (var i in usersArr) {
            htmlEmbedded += '<a href="#/UserProfile/'+usersArr[i]+'">' + usersArr[i] + '</a></br>';
        }

        return htmlEmbedded;

        
    }

    $scope.driverTrips = UserTripsFactory.driverTrips;
    var tripTemp = {};
    UserTripsFactory.getDriverTrips();

    $scope.editTrip = function () {
        UserTripsFactory.editTrip($scope.trip);
    }

    $scope.deleteTrip = function (trip) {
        UserTripsFactory.deleteTrip(trip);
    }

    $scope.cancelRequest = function (trip) {
        UserTripsFactory.cancelRequest(trip);
    }

    $scope.acceptRequest = function (username, tripId) {
        UserTripsFactory.acceptRequest(username, tripId);
        $scope.closeModal("request");
    }

    $scope.addReview = function () {
        UserTripsFactory.addReview($scope.review.driver, $scope.review.stars, $scope.review.description);
        $scope.closeModal("review");
    }

    $scope.getSingleTrip = function (trip) {
        SingleTripFactory.setTrip(trip);
        $location.path('/ViewTrip');
    }

    $scope.triggerModal = function (modal, trip) {
        if (modal == "edit") {
            $scope.trip = trip; //Modal input fields point to driverTrips
            tripTemp.DeparturePoint = $scope.trip.DeparturePoint;
            tripTemp.Date = $scope.trip.Date;
            tripTemp.ETD = $scope.trip.ETD;
            tripTemp.ETA = $scope.trip.ETA;
            $('#myTripsModal').modal('toggle');
        }
        if (modal == "review") {
            $scope.review = {};
            $scope.review.driver = trip.Driver;
            $('#myRatingModal').modal('toggle');           
        }
        if (modal == "request") {
            $scope.request = {};
            $scope.request.usersNotConfirmed = trip.UsersNotConfirmed;
            $scope.request.tripId = trip.Id;
            $('#myRequestModal').modal('toggle');
        }
        
    }

    $scope.closeModal = function (modal) {

        if (modal == "edit") {
            $('#myTripsModal').modal('hide');
            $scope.trip.DeparturePoint = tripTemp.DeparturePoint;
            $scope.trip.Date = tripTemp.Date;
            $scope.trip.ETD = tripTemp.ETD;
            $scope.trip.ETA = tripTemp.ETA;
        }
        if (modal == "review") {
            $('#myRatingModal').modal('hide');
        }
        if (modal == "request") {
            $('#myRequestModal').modal('hide');
        }
    }

}])