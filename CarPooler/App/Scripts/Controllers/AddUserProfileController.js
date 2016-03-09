app.controller('AddUserProfileController', ['$scope', 'AddUserProfileFactory', function ($scope, AddUserProfileFactory) {

    
    $scope.AddUserProfile = function (profile) {
       /* if (profile == undefined || profile.PictureUrl == "" || profile.PictureUrl == undefined || profile.PictureUrl == null || profile.DOB == "" || profile.DOB == undefined || profile.DOB == null || profile.HomeTown == "" || profile.HomeTown == undefined || profile.HomeTown == null || profile.Phone == "" || profile.Phone == undefined || profile.Phone == null) {
            alert("Please fill out all the fields before submitting");
            return;
        }*/
        if (profile == undefined)
            return;

        AddUserProfileFactory.AddUserProfile(profile);
    }


  

}]);