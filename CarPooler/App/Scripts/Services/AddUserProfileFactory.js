app.factory('AddUserProfileFactory', ['$http', '$q', '$location', function ($http, $q, $location) {

    var o = {};

    o.AddUserProfile = function (profile) {       
        profile.username = localStorage.getItem('username');
        $http({
            url: 'api/UserProfileApi',
            method: 'POST',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
            data:profile
        }).success(function (data) {
            alert("Profile successfully added");
            $location.path('/UserProfile/'+profile.username)
        }).error(function () {
            $location.path('/');
            alert("Profile Not Added")
        })
    }

    return o;

}]);