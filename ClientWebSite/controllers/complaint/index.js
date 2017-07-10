angular.module("module").controller("Allcomplaint", ["$scope", "getAjax", "$http", "$location", "$routeParams",
    function($scope, getAjax, $http, $location, $routeParams) {
        var id = $routeParams.id;
        console.log(id)
        getAjax.response("http://localhost:9408/api/Complaint/GetAll?id=" + id).
        then((response) => {
            $scope.complaints = response.data;
            console.log($scope.complaints)
        }, (error) => { console.log(error.statusText) });


        $scope.answer = (id) => {
            $location.path("/answercomplaint/" + id)
        }
    }
])