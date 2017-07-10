angular.module("module").controller("updateTask", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {

        var id = $location.search().TaskId;
        var therapistId = $location.search().therapistId;
        $scope.obj = {};
        $scope.show = false;
        getAjax.response(serverUrl + "api/TherapistTasks/" + id).
        then((response) => {
            $scope.obj = response.data;
            $scope.originalProduct = angular.copy($scope.obj);
            $scope.obj.birthDate = new Date($filter('date')($scope.obj.birthDate, "yyyy-MM-dd"));
            $scope.show = true;
        }, (error) => { console.log(error.statusText) });


        $scope.back = function() {
            $location.path("/therapistTask")
        }

        $scope.updatetask = function(id) {
            var upObj = $("#updateform").serialize();
            upObj += "&TherapistId=" + therapistId;
            $http.put(serverUrl + "api/TherapistTasks/" + id, upObj, {
                headers: {
                    'content-type': 'application/x-www-form-urlencoded'
                }
            }).then((response) => {
                console.log(response.data);
                $location.path("/therapistTask")
            }, (error) => { console.log(error.statusText) });
        }

    }
]);