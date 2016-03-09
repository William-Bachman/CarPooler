app.factory('LoginFactory',['$http', '$q', function ($http, $q) {

    var o = {};
    o.login = function (user) {
        var q = $q.defer();
        $http({
            url: '/Token',
            method: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            data:'username='+user.username+'&password='+user.password+'&grant_type=password'
        }).success(function(data){
            localStorage.setItem('token',data.access_token);
            localStorage.setItem('username', data.userName);
            q.resolve(data.userName);

        }).error(function(data){
            console.log(data);
        })
        return q.promise;
    }


    return o;
}]);