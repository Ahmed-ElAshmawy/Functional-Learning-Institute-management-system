angular.module("module").controller("complaint", ["$scope", "getAjax", "$http", "$location", "$routeParams", "auth", function($scope, getAjax, $http, $location, $routeParams, auth) {
    var userId = auth.getUserId();
    console.log("userid", userId)
    getAjax.response("http://localhost:9408/api/Complaint?userId=" + userId).
    then((response) => {
        $scope.complaints = response.data;
        console.log($scope.complaints)
    }, (error) => { console.log(error.statusText) });
    $scope.create = () => {
        $location.path("/createcomplaint")
    }
    $scope.details = (id) => {
        $location.path("/complaintdetails/" + id);
    }
}])