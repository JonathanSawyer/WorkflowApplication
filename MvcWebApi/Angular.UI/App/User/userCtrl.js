angular.module('myApp.controllers').controller("UserCtrl",
    function ($scope,
              $http,
              userService,
              userWorkflowService)
{
    $scope.users = [];
    $scope.userWorkflows = [];
    userService.list().then(function (users)
    {
        $scope.users = users;
    });

    userWorkflowService.list().then(function (userWorkflows) {
        $scope.userWorkflows = userWorkflows;
    });

    $scope.pageMode = {}
    $scope.pageMode.setList = function ()
    {
        $scope.pageMode.list = true;
        $scope.pageMode.edit = false;
    }

    $scope.pageMode.setEdit = function ($index)
    {
        if($index == undefined)
        {
            $scope.user =
            {
                Id: 0,
                Name: "",
                Surname: ""
            };
        }
        else
        {
            $scope.user = $scope.users.data[$index];
        }

        $scope.pageMode.list = false;
        $scope.pageMode.edit = true;
    }

    $scope.pageMode.setList();

    $scope.save = function ()
    {
        userService.save($scope.user).then(function (userWorkflows)
        {
            $scope.userWorkflows = userWorkflows;
            userService.list().then(function (users) {
                $scope.users = users;
                $scope.pageMode.setList();
            });
        });
    };

    $scope.approve = function (id)
    {
        userWorkflowService.approve($scope.userWorkflows.data[id].Id)
                           .then(function (response)
                           {
                               $scope.userWorkflows = response.userWorkflows;
                               $scope.users = response.users;
                           });
    };
    $scope.reject = function (id)
    {
        userWorkflowService.reject($scope.userWorkflows.data[id].Id)
                           .then(function (response) {
                               $scope.userWorkflows = response.userWorkflows;
                               $scope.users = response.users;
                           });
    };


});