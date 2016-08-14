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
        userWorkflowService.approve(id)
                           .then(function (response)
                           {
                               $scope.userWorkflows = response.userWorkflows;
                               $scope.users = response.users;
                           });
    };
    $scope.reject = function (id)
    {
        userWorkflowService.reject(id)
                           .then(function (response) {
                               $scope.userWorkflows = response.userWorkflows;
                               $scope.users = response.users;
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