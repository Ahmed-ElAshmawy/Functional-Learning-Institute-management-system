angular.module("module").controller("sessionFinshing", ["$scope", "getAjax", "$http", "$location", "serverUrl",
    function($scope, getAjax, $http, $location, serverUrl) {
        console.log("hello from finshing session")

        getAjax.get(serverUrl + "api/Sessions/GetSessionFinshing").
        then(function(data) {
            $scope.sessionFinshing = data.data
            console.log("tochange", $scope.sessionFinshing)
        }, function(error) {
            if (error.status == "401")
                $location.path("/unauthorized");
            console.log(error);
        });

        $scope.changeToFinish = (id) => {
            $http.put(serverUrl + "api/Sessions/PutChangeToFinish?id=" + id)
                .then((response) => {
                    console.log(response.data);
                    $scope.sessionFinshing = $scope.sessionFinshing.filter(item => {
                        return item.id != id;
                    });
                }, (error) => {
                    console.log(error.statusText)
                });
        }
        $scope.back = () => {
            $location.path("/session");
        }
    }
])