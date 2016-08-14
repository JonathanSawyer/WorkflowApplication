angular.module('myApp.controllers', []);
var app = angular.module("MyApp", ['myApp.controllers']);

app.service('userService', function ($http, $q, userWorkflowService)
{
    this.list = function ()
    {
        return $http.get("../api/user").then(function (response)
        {
            var users = [];
            for(var index = 0; index < response.length; index++)
            {
                var user = response[index];
                users.push
                    (
                        {
                            Name    : user.Name,
                            Surname : user.Surname,
                        }
                    );
            }
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
        $http.get("../api/userworkflow/approve/" + id).success(function ()
        {

        })
    }
});
