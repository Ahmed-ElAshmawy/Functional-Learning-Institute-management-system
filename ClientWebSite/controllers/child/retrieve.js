angular.module("module").controller("retrieveChild", ["$scope", "getAjax", "$http", "$location",
    function($scope, getAjax, $http, $location) {
        console.log("hello from Retrieve controller")
        getAjax.response("http://localhost:9408/api/Children/GetRetrieveChildren").
        then((response) => {
            $scope.Retc = response.data;
            console.log($scope.Ret);
        }, (error) => { console.log(error.statusText) });
        $scope.getChild = (id) => {
            $http.put("http://localhost:9408/api/Children/PutChildRetrieve?id=" + id).
            then((response) => {
                $scope.Retc = response.data;
                console.log($scope.Ret);
                $location.path("/retrieveChild")
            }, (error) => { console.log(error.statusText) });
        }

    }
]);