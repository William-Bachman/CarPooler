app.factory('RegisterFactory', ['$http', '$q', 'LoginFactory', function ($http, $q, LoginFactory) {

    var o = {}

    o.register = function (user) {
        var q = $q.defer();
        if (user == undefined || user.name == undefined || user.lastName == undefined || user.username == undefined || user.email == undefined || user.password == undefined) {
            alert("Please fill out all the fields");
            return;
        }
        if (user.password.length < 6) {
            alert("Password must contain at least 6 characters");
            return;
        }
        var registerBindingModelObj = {
            Name: user.name,
            LastName: user.lastName,
            Username: user.username,
            Email: user.email,
            Password: user.password
        };

        $http({
            url: 'api/RegistrationApi',
            method: 'post',
            contentType: 'application/json',
            data: registerBindingModelObj
        }).success(function (data) {
            alert("You've successfully registered in CarPooler, you will receive a confirmation email very soon!");
            q.resolve();
        }).error(function (data) {
            alert(data.Message);
        })
        return q.promise;
    }

    //Why does return 'o' not work?
    return {
        register:o.register
    }
}]);