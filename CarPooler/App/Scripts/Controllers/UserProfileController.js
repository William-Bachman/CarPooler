app.controller('UserProfileController', ['$scope', 'UserProfileFactory', '$routeParams', '$location', function ($scope, UserProfileFactory, $routeParams, $location) {
    var username;
    if ($routeParams.Username == localStorage.getItem('username')) {
        username = localStorage.getItem('username');
    }
    else {
        username = $routeParams.Username;
    }
    
    UserProfileFactory.GetUserProfile(username).then(function (data) {
        if(data != 'Not Found') $scope.userProfile = data;
        
    });
    
    $scope.EditProfile = function () {
        $location.path('/EditProfile')
        if ($scope.profile) UserProfileFactory.EditProfile($scope.profile);
    }
   
    

    //   $scope.UserProfiles = UserProfileFactory.UserProfiles;
    //    UserProfileFactory.getUserProfiles();

}]);