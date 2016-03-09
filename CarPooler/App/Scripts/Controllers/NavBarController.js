app.controller('NavBarController', ['$scope', 'LoginFactory', 'RegisterFactory', '$location', function ($scope, LoginFactory, RegisterFactory, $location) {

    $scope.status = {};
    $scope.user = {};
    $scope.status.isLoggedIn = localStorage.getItem('token') ? true : false;
    $scope.user.username = localStorage.getItem('username') || '';

    $scope.login = function () {
        LoginFactory.login($scope.user).then(function (username) {
            $scope.user.username = username;
            $scope.status.isLoggedIn = true;
            $location.path('/');
        });

    }

    $scope.logout = function () {

        localStorage.removeItem('token');
        localStorage.removeItem('username');
        $scope.status.isLoggedIn = false;
        $scope.user.username = '';
        $scope.user.password = '';
    }

    $scope.register = function () {
        RegisterFactory.register($scope.user).then(function () {
            $scope.login();
        })
    };




}]);