﻿angular.module('myApp.controllers').controller("UserCtrl",
    function ($scope,
              $http,
              userService,
              userWorkflowService)
{
    $scope.users = [];
    $scope.userWorkflows = [];
    $scope.loading = true;

    $scope.load = function()
    {
        $scope.loading = true;
        userService.list().then(function (users)
        {
            $scope.users = users;
            $scope.loading = false;
        });

        userWorkflowService.list().then(function (userWorkflows)
        {
            $scope.userWorkflows = userWorkflows;
            $scope.loading = false;
        });
    }
    $scope.load();

    $scope.edit = function ($index)
    {
        if($index == undefined)
        {
            var user =
            {
                Id: 0,
                Name: "",
                Surname: "",
                edit : true
            };
            $scope.editUser = JSON.parse(JSON.stringify(user));
            $scope.users.data.push(user);
        }
        else
        {
            for (var index = 0; index < $scope.users.data.length; index++)
            {
                if ($index == index)
                {
                    $scope.editUser = JSON.parse(JSON.stringify($scope.users.data[index]));
                }
                $scope.users.data[index].edit = $index == index ? true : undefined;
            }
        }
    }

    $scope.cancel = function()
    {
        $scope.editUser.edit = undefined;
        if ($scope.editUser.Id == 0)
            $scope.users.data.splice($scope.users.data.length - 1, 1);

        for (var index = 0; index < $scope.users.data.length; index++) {
            $scope.users.data[index].edit = undefined;
        }
    }

    $scope.save = function ()
    {
        $scope.editUser.edit = undefined;
        if ($scope.editUser.Id == 0)
            $scope.users.data.splice($scope.users.data.length - 1, 1);
     
        userService.save($scope.editUser).then(function (userWorkflows)
        {
            $scope.load();
        });
    };

    $scope.delete = function(userId)
    {
        userService.delete(userId).then(function () {
            $scope.load();
        });
    }

    $scope.approve = function (id)
    {
        userWorkflowService.approve(id)
                           .then(function (response)
                           {
                               $scope.load();
                           });
    };
    $scope.reject = function (id)
    {
        userWorkflowService.reject(id)
                           .then(function (response) {
                               $scope.load();
                           });
    };
    $scope.tableRowExpanded = false;
    $scope.tableRowIndexExpandedCurr = "";
    $scope.tableRowIndexExpandedPrev = "";

    $scope.dataCollapseFn = function () {
        $scope.dataCollapse = [];
        for (var i = 0; i < $scope.users.data.length; i += 1) {
            $scope.dataCollapse.push(false);
        }
    };

    $scope.history = function (index) {
        if (typeof $scope.dataCollapse === 'undefined') {
            $scope.dataCollapseFn();
        }

        if ($scope.tableRowExpanded === false && $scope.tableRowIndexExpandedCurr === "") {
            $scope.tableRowIndexExpandedPrev = "";
            $scope.tableRowExpanded = true;
            $scope.tableRowIndexExpandedCurr = index;
            $scope.dataCollapse[index] = true;
        }
        else if ($scope.tableRowExpanded === true) {
            if ($scope.tableRowIndexExpandedCurr === index) {
                $scope.tableRowExpanded = false;
                $scope.tableRowIndexExpandedCurr = "";
                $scope.dataCollapse[index] = false;
            } else {
                $scope.tableRowIndexExpandedPrev = $scope.tableRowIndexExpandedCurr;
                $scope.tableRowIndexExpandedCurr = index;
                $scope.dataCollapse[$scope.tableRowIndexExpandedPrev] = false;
                $scope.dataCollapse[$scope.tableRowIndexExpandedCurr] = true;
            }
        }

    };


});