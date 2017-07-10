angular.module("module").controller("childSession", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {
        console.log("childsession")
        getAjax.get(serverUrl + "api/Sessions/GetSessionsForChild").
        then(function(data) {
            $scope.show = data.data
            console.log($scope.show)
        }, function(error) {
            if (error.status == "401")
                $location.path("/unauthorized");
            console.log(error);
        });
        $scope.create = (id, name) => {
            console.log("id", id);
            console.log("name", name);
            $location.path("/newSession/").search({
                Id: $("#childdrop").val(),
                Name: $("#childdrop :selected").text()
            });
        }
        $scope.showSchedule = (id) => {
            $location.path("/childschedule/" + id);
        }
    }
]);