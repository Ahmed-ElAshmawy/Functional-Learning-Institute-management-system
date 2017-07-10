angular.module("module").controller("sessiondetail", ["$scope", "getAjax", "$http", "$location", "serverUrl", "$routeParams",
    function($scope, getAjax, $http, $location, serverUrl, $routeParams) {
        id = $routeParams.Id;
        getAjax.get(serverUrl + "api/Sessions/GetSession/" + id).
        then(function(data) {
            $scope.show = data.data
        }, function(error) {
            if (error.status == "401")
                $location.path("/unauthorized");
            console.log(error);
        });
        // $scope.back = () => {
        //     $location.path("/session");
        // }
    }
]);