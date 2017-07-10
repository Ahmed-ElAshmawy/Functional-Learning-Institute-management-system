angular.module("module").controller("createcomplaint", ["$scope", "getAjax", "$http", "$location", "$routeParams", "auth", "serverUrl",
    function($scope, getAjax, $http, $location, $routeParams, auth, serverUrl) {
        var userId = auth.getUserId();
        $scope.sendData = function() {
            var data = {
                body: $scope.body,
                title: $scope.title,
                userId: userId
            };
            console.log(data);
            $http.post(serverUrl + "api/Complaint", data).
            then((data) => {
                    console.log(data);
                    $location.path("/complaint");
                },
                (error) => {
                    console.log(error);
                    console.log("errrror")
                });
        }
        $scope.back = () => {
            $location.path("/complaint");
        }
        $scope.reset = () => {
            $scope.body = $scope.title = "";
            $scope.error = "";
        }





    }
])