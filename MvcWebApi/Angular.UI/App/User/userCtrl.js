angular.module('myApp.controllers').controller("UserCtrl", function ($scope, $http)
{
    $scope.test = "Hello World";
    $scope.users = [];
    $scope.user =
        {
            Name: "",
            Surname: ""
        };

    $scope.pageMode = {}
    $scope.pageMode.setList = function ()
    {
        $scope.pageMode.list = true;
        $scope.pageMode.edit = false;
    }

    $scope.pageMode.setEdit = function ()
    {
        $scope.pageMode.list = false;
        $scope.pageMode.edit = true;
    }

    $scope.pageMode.setList();

    $http.get("../api/user").success(function (users)
    {
        $scope.users = users;
    });

    $scope.save = function()
    {
        //TODO: Add response
        $http.post("../api/user", $scope.user).success(function (user)
        {
            
        });
    }
});