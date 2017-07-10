angular.module("module").controller("complaintDetails", ["$scope", "getAjax", "$http", "$location", "$routeParams", "auth", function($scope, getAjax, $http, $location, $routeParams, auth) {
    var complaintId = $routeParams.id;
    console.log("complaintId", complaintId)
    getAjax.response("http://localhost:9408/api/Complaint/" + complaintId).
    then((response) => {
        $scope.complaint = response.data;
        console.log($scope.complaint)
    }, (error) => { console.log(error.statusText) });
    $scope.back = () => {
        $location.path("/complaint")
    }
}])