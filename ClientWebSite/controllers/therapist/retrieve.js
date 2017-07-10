angular.module("module").controller("retrieveTherapist", ["$scope", "getAjax", "$http", "$location", function ($scope, getAjax, $http, $location) {
console.log("hello from Retrieve controller")
    getAjax.response("http://localhost:9408/api/Therapists/GetRetrieveTherapists").
    then((response) => {
        $scope.Retthe = response.data;
        console.log($scope.Ret);
    }, (error) => { console.log(error.statusText) });

$scope.getTherapist=(id)=>
{
    $http.put("http://localhost:9408/api/Therapists/PutTherapistRerieve?id=" + id).
    then((response) => {
        $scope.Retthe = response.data;
        $location.path("/retrieveTherapist")
        console.log($scope.Ret);
    }, (error) => { console.log(error.statusText) });
}

}]);