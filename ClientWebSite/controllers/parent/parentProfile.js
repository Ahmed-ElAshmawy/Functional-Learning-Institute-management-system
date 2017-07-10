angular.module("module").controller("parentprofile", ["$scope", "getAjax", "$http", "$location", "$routeParams", "auth", function($scope, getAjax, $http, $location, $routeParams, auth) {

    // var id = "110a5986-2caa-4c5b-b6bb-09fe706180a8"
    var id = auth.getUserId();
    console.log("idkkkk", id)
    getAjax.response("http://localhost:9408/api/Parents/GetParentChild?id=" + id).
    then((response) => {
        $scope.childData = response.data;
        console.log($scope.childData)
    }, (error) => { console.log(error.statusText) });
    $scope.availability = (id) => {
        $location.path("/childAvailability/" + id);

    }

    $scope.schedule = (id) => {
        $location.path("/childschedule/" + id);

    }

}])