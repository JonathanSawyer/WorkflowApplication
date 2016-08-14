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

    $scope.pageMode.setEdit = function ()
    {
        $scope.user =
        {
            Name: "",
            Surname: ""
        };
        $scope.pageMode.list = false;
        $scope.pageMode.edit = true;
    }

    $scope.pageMode.setList();

    $scope.save = function ()
    {
        userService.save($scope.user).then(function (userWorkflows)
        {
            $scope.userWorkflows = userWorkflows;
        });
        $scope.pageMode.setList();
    };

    $scope.approve = function (id)
    {
        userWorkflowService.approve($scope.userWorkflows.data[id].Id).then(function (userWorkflows)
        {
            $scope.userWorkflows = userWorkflows;
        });
    };
    $scope.reject = function (id)
    {
        //userWorkflowService.reject(id);
    };


});