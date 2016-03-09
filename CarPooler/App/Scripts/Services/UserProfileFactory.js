app.factory('UserProfileFactory', ['$http', '$q', '$location', function ($http, $q, $location) {

    var o = { userProfile: {} };

    o.GetUserProfile = function (username) {
        var q = $q.defer();
        $http({
            url: 'api/UserProfileApi/?username='+ username,
            method: 'get',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') }
        }).success(function (data) {
            q.resolve(data);
        }).error(function () {
            $location.path('/');
        })

        return q.promise;
    };

    o.EditProfile = function (profile) {
        profile.username = localStorage.getItem('username');
        $http({
            url: 'api/UserProfileApi',
            method: 'post',
            headers: { Authorization: 'Bearer ' + localStorage.getItem('token') },
            data: profile
        }).success(function (data) {
            alert("You have sucsessfully updated your profile!")
            $location.path('/UserProfile/' + profile.username)
            //$('#myTripsModal').modal('hide');

            //o.getDriverTrips();
            //$location.path('#/MyTrips');
            //window.location.assign('#/MyTrips');           

        }).error(function () {
            alert('the db is gone');
        })
    };


    return o;

}]);