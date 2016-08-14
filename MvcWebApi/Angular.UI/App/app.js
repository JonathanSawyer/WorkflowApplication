angular.module('myApp.controllers', []);
var app = angular.module("MyApp", ['myApp.controllers']);

app.service('userService', function ($http, $q, userWorkflowService)
{
    this.list = function ()
    {
        return $http.get("../api/user").then(function (users)
        {
            return users;
        });
    }

    this.save = function (user) {
        return $http.post("../api/user", user).then(function ()
        {
            return userWorkflowService.list().then(function (userWorkflows) {
                return userWorkflows;
            });
        });
    }
});
app.service('userWorkflowService', function ($http, $q) {
    this.list = function ()
    {
        return $http.get("../api/userworkflow").then(function (response)
        {
            return response;
        });
    }

    this.approve = function(id)
    {
        return $http.get("../api/userworkflow/approve/" + id).then(function ()
        {
            return $http.get("../api/userworkflow").then(function (response) {
                return response;
            });
        });
        
    }
});
