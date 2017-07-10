angular.module("module").controller("notifications", ["$scope", "getAjax", "$filter", "$http", "$location", "serverUrl", "auth", "$routeParams",
    function($scope, getAjax, $filter, $http, $location, serverUrl, auth, $routeParams) {
        var id = $routeParams.Id;
        var sessionId = $routeParams.sessionId;

        getAjax.response(serverUrl + "api/Notifications/" + id).
        then((response) => {
            console.log('notifications');
            $scope.obj = response.data;
            console.log($scope.obj);
        }, (error) => {
            console.log(error.statusText)
        });
        $scope.details = () => {
            $location.path('updatesession/' + sessionId)
        }


    }
]);